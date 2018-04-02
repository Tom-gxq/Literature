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
    require('eventsetting/style.css');
    //默认参数
    var Defaults = {
        dialogID: "addEvent",//弹出框ID
        orderID: ''
    };
    var setting = {};
    var AddEvent = function (options) {
        var _this = this;
        setting = $.extend({}, Defaults, options);
        _this.setting = $.extend({}, Defaults, options);
        _this.init(function () {
            _this.showDialog();
        });
        return this;
    }
    AddEvent.prototype = {
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
                url: 'GetEventDetail',
                type: 'GET',
                cache: false,                
                success: function (msg) {
                    if (msg.result) {
                        doT.exec('eventsetting/addevent.html', function (templateFun) {
                            var innerText;
                            innerText = templateFun(msg.result);
                            easydialog.open({
                                container: {
                                    id: "addEvent",
                                    header: '添加事件管理',
                                    content: innerText
                                }
                            });
                            $('#wizard').css('height', $('#addEvent').find('.page').height());

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
                            $("#inputResEvent").change(function () {
                                _this.bindSysKindInfo($(this).val());
                            })
                        });
                    }
                },
                error: function (err) {

                }
            });
            //_this.bindEvent();
        },
        Submit: function (elementID) {
            var _this = this;
            var divElement = $('#' + elementID + '');
            var type = {
                SysEventId: divElement.find('#inputSysEvent').val(),
                ResEventId: divElement.find('#inputResEvent').val(),
                Quantity: divElement.find('.txtName').val(),
                KindId: divElement.find('#inputSysKind').val(),
            };
            type = $.param(type, true);
            Global.post("/Discount/AddEvent", type, function (data) {
                window.location.href = '/Discount/EventSettingIndex';
            });
        },
        //绑定学区信息
        bindSysKindInfo: function (resId) {
            $("#inputSysKind").empty();
            $("#inputSysKind").append($("<option/>").text("").attr("value", "0"));
            var data = {
                resEventId: resId
            }
            data = $.param(data, true);
            Global.get("/Discount/GetSysKindList", data, function (msg) {
                $(msg.items).each(function (index, itemData) {
                    $("#inputSysKind").append($("<option/>").text(itemData.Description).attr("value", itemData.KindId));
                });
            })
        }

    };

    //对外公布方法
    exports.init = function (options) {
        return new AddEvent(options);
    }
    return exports
});