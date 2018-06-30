/*添加引用
var addContact = require('addContact');

初始化插件并返回对象实例
var addOrder = addOrder.init({
    dialogID: 弹出框ID
        orderID: 新增成功后返回订单ID
});
*/

define(function (require, exports, module) {
    var $ = require('jquery'),
        doT = require('dot'),
        Global = require('global').Global,
        easydialog = require("easydialog");
    require('regiondata/style.css');
    //默认参数
    var Defaults = {
        dialogID: "genderDorm",//弹出框ID
        orderID: ''
    };
    var setting = {};
    var GenderDorm = function (options) {
        var _this = this;
        setting = $.extend({}, Defaults, options);
        _this.setting = $.extend({}, Defaults, options);
        _this.init(function () {
            _this.showDialog();
        });
        return this;
    }
    GenderDorm.prototype = {
        init: function (callback) {
            var _this = this;
            callback();
        },
        /*
        *刚调用方法，弹出框
        */
        showDialog: function (options) {
            var _this = this;
            
            var elementID = _this.setting.dialogID;
            $.ajax({
                url: 'GetRegionDataDetail',
                type: 'GET',
                cache: false,
                width: 500,
                data: {
                    dataId: _this.setting.Id
                },
                success: function (msg) {
                    if (msg.result) {
                        doT.exec('regiondata/genderdorm.html', function (templateFun) {
                            var innerText;
                            innerText = templateFun(msg);
                            easydialog.open({
                                container: {
                                    id: "genderDorm",
                                    header: '生成宿舍',
                                    content: innerText
                                }
                            });
                            $('#wizard').css('height', $('#addRegion').find('.page').height());

                            $(".page").show();
                            //取消
                            $("#cacel").click(function () {
                                easydialog.close();
                            });

                            //提交
                            $("#sub").click(function () {
                                _this.Submit(elementID);
                            });
                        });
                    }
                }
             });
            //_this.bindEvent();
        },
        Submit: function (elementID) {
            var _this = this;
            var divElement = $('#' + elementID + '');
            var type = {
                parentId: divElement.find('#parentdataId').val(),
                start: divElement.find('#start').val(),
                end: divElement.find('#end').val(),

            };
            type = $.param(type, true);
            Global.post("/RegionData/GenderDormNum", type, function (data) {
                window.location.href = '/RegionData/Index';
            });
        },

    };

    //对外公布方法
    exports.init = function (options) {
        return new GenderDorm(options);
    }
    return exports
});