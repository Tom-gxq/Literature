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
    require('resevent/style.css');
    //默认参数
    var Defaults = {
        dialogID: "addResEvent",//弹出框ID
        orderID: ''
    };
    var setting = {};
    var AddResEvent = function (options) {
        var _this = this;
        setting = $.extend({}, Defaults, options);
        _this.setting = $.extend({}, Defaults, options);
        _this.init(function () {
            _this.showDialog();
        });
        return this;
    }
    AddResEvent.prototype = {
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
            doT.exec('resevent/addresevent.html', function (templateFun) {
                var innerText;
                innerText = templateFun(elementID);
                easydialog.open({
                    container: {
                        id: "addResEvent",
                        header: '添加事件管理',
                        content: innerText
                    }
                });
                $('#wizard').css('height', $('#addResEvent').find('.page').height());

                $(".page").show();
                //取消
                $("#cacel").click(function () {
                    easydialog.close();
                });

                //提交
                $("#sub").click(function () {
                    _this.Submit(elementID);
                });

                var completeInfo = require('complete');
                completeInfo.createComplete({
                    element: "#parent",
                    url: '/Discount/SearchRegionData',
                    selectCallback: function (value, text) {
                        _this.bindCompanyInfo(value, text);
                    },
                    asyncCallback: function (response, data) {
                        
                        response($.map(data.items, function (item) {
                            return {
                                label: item.DataName,
                                valueKey: item.DataId
                            }
                        }));
                    },
                    data: function (request) {
                        $(".clipLoader").show();
                        return { dataName: request.term };
                    }
                });

            });

            //_this.bindEvent();
        },
        Submit: function (elementID) {
            var _this = this;
            var divElement = $('#' + elementID + '');
            var type = {
                EventName: divElement.find('.txtName').val(),
                Kind: divElement.find('.newselect').val(),
            };
            type = $.param(type, true);
            Global.post("/Discount/AddResEvent", type, function (data) {
                window.location.href = '/Discount/ResEventIndex';
            });
        }

    };

    //对外公布方法
    exports.init = function (options) {
        return new AddResEvent(options);
    }
    return exports
});