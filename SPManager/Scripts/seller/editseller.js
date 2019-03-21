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
    qiniu = require('qiniusellerlogo');
    require('admin/style.css');
    //默认参数
    var Defaults = {
        dialogID: "editseller"//弹出框ID
    };

    var EditSeller = function (options) {
        var _this = this;
        _this.setting = $.extend({}, Defaults, options);
        _this.init(function () {
            _this.showDialog();
        });
        return this;
    }
    EditSeller.prototype = {
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
                url: 'GetSellerDetail',
                type: 'GET',
                cache: false,
                data: {
                    id: _this.setting.Id,
                },
                success: function (msg) {
                    doT.exec('seller/editseller.html', function (templateFun) {
                        var innerText;
                        innerText = templateFun(msg);
                        easydialog.open({
                            container: {
                                id: "editseller",
                                header: '编辑所见区域',
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
                            easydialog.close();
                        });

                        qiniu.QiniuMainController.init();

                        $('#easyDialogBox').css({
                            width: "500px",
                            height: "600px"
                        });

                    });
                },
                error: function (err) {

                }
            });
            

            //_this.bindEvent();
        },
        Submit: function (elementID) {
            var _this = this;
            var divElement = $('#' + elementID + '');
            var param = {
                Id: divElement.find('#Id').val(),
                SuppliersName: divElement.find('.txtName').val(),
                TelPhone: divElement.find('.telphone').val(),
                AlipayNo: divElement.find('.alipay').val(),
                LogoPath: divElement.find('#imgPath').val(),
                AccountId: divElement.find('#leader').val(),
            };
            param = $.param(param, true);
            Global.post("/Seller/EditSeller", param, function (data) {
                _this.setting.callBack(data);
            });
        },
        
    };
    
    //对外公布方法
    exports.init = function (options) {
        return new EditSeller(options);
    };

    return exports;
});