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
    require('admin/style.css');
    //默认参数
    var Defaults = {
        dialogID: "editproduct"//弹出框ID
    };

    var EditProduct = function (options) {
        var _this = this;
        _this.setting = $.extend({}, Defaults, options);
        _this.init(function () {
            _this.showDialog();
        });
        return this;
    }
    EditProduct.prototype = {
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
                url: 'GetProductDetail',
                type: 'GET',
                cache: false,
                data: {
                    id: _this.setting.Id
                },
                success: function (msg) {
                    doT.exec('seller/editproduct.html', function (templateFun) {
                        var innerText;
                        innerText = templateFun(msg);
                        easydialog.open({
                            container: {
                                id: "editproduct",
                                header: '编辑商家产品',
                                content: innerText
                            }
                        });
                        //$('#wizard').css('height', $('#' + msg.result.Id).find('.page').height());

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

                        //$('#easyDialogBox').css({
                        //    width: "500px",
                        //    height: "600px"
                        //});

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
                Id: divElement.find('#sellerProductId').val(),
                ProductId: divElement.find('#procductlist').val(),
                PurchasePrice: divElement.find('.purchaseprice').val(),
                AlertStock: divElement.find('.alertstock').val(),
                MarketPrice: divElement.find('.marketprice').val(),
                VIPPrice: divElement.find('.vipprice').val()
            };
            param = $.param(param, true);
            Global.post("/Seller/EditProduct", param, function (data) {
                _this.setting.callBack(data);
            });
        },
        
    };
    
    //对外公布方法
    exports.init = function (options) {
        return new EditProduct(options);
    };

    return exports;
});