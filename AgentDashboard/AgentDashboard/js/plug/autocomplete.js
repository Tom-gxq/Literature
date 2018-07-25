/*   
 * 文本框自动加载下拉框数据
 * @authors Allen
 * @date    2014-06-06 09:15:28
 * @version 1.0
 *
     var complete = require('complete'); 页面加载
     附加事件
     complete.createComplete({
        文本框ID
        element: "#search", 

        ajax请求地址
        url: '/Customer/SearchProjects', 
        
        选择下拉值的回调函数
        selectCallback: function (value, text) {
            CustomerControler.BindProjectInfo(value, text);
        },

        绑定下拉列表的回调函数
        asyncCallback: function (response, data) {
            response($.map(data.items, function (item) {
                return {
                    label: item.CompanyName,
                    valueKey: item.ProjectID
                }
            }));
        },
        请求参数
        data: function (request) {
            return { keywords: request.term };
        }
    });
*/

define(function (require, exports, module) {
    var $ = require('jqueryui');
    var common = require('global');

        //默认配置
    var defaults = {
        element: "",
        url: '',
    };
    var complete = function (options) {
        this.setting = {};
        this.init(options);
    };

    complete.prototype = {
        init: function (options) {
            var _self = this;
            _self.setting = $.extend({}, defaults, options);
            _self.setting.element = $(_self.setting.element);
            _self.bindEvent();
        },
        bindEvent: function () {
            var _self = this;
            _self.setting.element.autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: _self.setting.url,
                        data: _self.setting.data(request),
                        cache: false,
                        success: function (data) {
                            $(".clipLoader").hide();
                            (!!_self.setting.asyncCallback) && (_self.setting.asyncCallback(response, data));
                           
                           } 
                    });
                },
                minLength: 1,
                autoFocus: true,
                focus: function (event, ui) {
                    return false;
                },
                select: function (event, ui) {
                    if (ui.item) {
                        $(this).val(ui.item.label);
                        (!!_self.setting.selectCallback) && _self.setting.selectCallback(ui.item.valueKey, ui.item.label,ui.item);
                        return false;
                    }
                }
            });
        }
    };
    exports.createComplete = function (options) {
        return new complete(options);
    };
})