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
    require('discount/style.css');
    //默认参数
    var Defaults = {
        dialogID: "addSysKind",//弹出框ID
        orderID: ''
    };
    var setting = {};
    var AddSysKind = function (options) {
        var _this = this;
        setting = $.extend({}, Defaults, options);
        _this.setting = $.extend({}, Defaults, options);
        _this.init(function () {
            _this.showDialog();
        });
        return this;
    }
    AddSysKind.prototype = {
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
            doT.exec('discount/addcoupons.html', function (templateFun) {
                var innerText;
                innerText = templateFun(elementID);
                easydialog.open({
                    container: {
                        id: "addSysKind",
                        header: '添加优惠券',
                        content: innerText
                    }
                });
                $('#wizard').css('height', $('#addSysKind').find('.page').height());

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
                Description: divElement.find('.txtName').val(),
                Quantity: divElement.find('.txtQuantity').val(),
                Unit: divElement.find('.newselect').val(),
                Price: divElement.find('.txtPrice').val(),
                DiscountValue: divElement.find('.txtDiscount').val(),
                Num: divElement.find('.txtNum').val(),
            };
            type = $.param(type, true);
            Global.post("/Discount/AddCoupons", type, function (data) {
                window.location.href = '/Discount/CouponsIndex';
            });
        }

    };

    //对外公布方法
    exports.init = function (options) {
        return new AddSysKind(options);
    }
    return exports
});