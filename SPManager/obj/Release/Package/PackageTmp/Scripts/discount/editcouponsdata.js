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
        dialogID: "editassociator",//弹出框ID
        orderID: ''
    };

    var EditAssociator = function (options) {
        var _this = this;
        _this.setting = $.extend({}, Defaults, options);
        _this.init(function () {
            _this.showDialog();
        });
        return this;
    }
    EditAssociator.prototype = {
        init: function (callback) {
            var _this = this;
            callback();
        },
        /*
        *刚调用方法，弹出框
        */
        showDialog: function () {
            var _this = this;
            var elementID = _this.setting.dialogID;
            $.ajax({
                url: 'GetAssociatorDetail',
                type: 'GET',
                cache: false,
                data: {
                    kindId: _this.setting.Id
                },
                success: function (msg) {
                    if (msg.result) {
                        doT.exec('discount/editcoupons.html', function (templateFun) {
                            var innerText;
                            innerText = templateFun(msg.result);
                            easydialog.open({
                                container: {
                                    id: "editassociator",
                                    header: '编辑优惠券',
                                    content: innerText
                                }
                            });
                            $('#wizard').css('height', $('#' + msg.result.Id).find('.page').height());

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
                },
                error: function (err) {

                }
            });


            //_this.bindEvent();
        },
        Submit: function (elementID) {
            var _this = this;
            var divElement = $('#' + elementID + '');
            var user = {
                KindId: _this.setting.Id,
                Description: divElement.find('.txtName').val(),
                Quantity: divElement.find('.txtQuantity').val(),
                Unit: divElement.find('.newselect').val(),
                Price: divElement.find('.txtPrice').val(),
                DiscountValue: divElement.find('.txtDiscount').val(),

            };
            user = $.param(user, true);
            Global.post("/Discount/EditSysKind", user, function (data) {
                _this.setting.callBack(data);
            });
        },

    };

    //对外公布方法
    exports.init = function (options) {
        return new EditAssociator(options);
    }
    return exports
});