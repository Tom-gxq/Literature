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
    require('productsku/style.css');
    //默认参数
    var Defaults = {
        dialogID: "addProductSku",//弹出框ID
        orderID: ''
    };
    var setting = {};
    var AddProductSku = function (options) {
        var _this = this;
        setting = $.extend({}, Defaults, options);
        _this.setting = $.extend({}, Defaults, options);
        _this.init(function () {
            _this.showDialog();
        });
        return this;
    }
    AddProductSku.prototype = {
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
            
            doT.exec('productsku/addsku.html', function (templateFun) {
                var innerText;
                innerText = templateFun(elementID);
                easydialog.open({
                    container: {
                        id: "addProductSku",
                        header: '添加库存',
                        content: innerText
                    }
                });
                $('#wizard').css('height', $('#addProductSku').find('.page').height());

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
                    element: "#product",
                    url: '/Product/SearchProductByKeyWord',
                    selectCallback: function (value, text) {
                        _this.bindProductId(value, text);
                    },
                    asyncCallback: function (response, data) {

                        response($.map(data.items, function (item) {
                            return {
                                label: item.ProductName,
                                valueKey: item.ProductId
                            }
                        }));
                    },
                    data: function (request) {
                        $(".clipLoader").show();
                        return { keywords: request.term };
                    }
                });
                $("#inputResEvent").change(function () {
                    //_this.bindSysKindInfo($(this).val());
                })
            });
            
            //_this.bindEvent();
        },
        Submit: function (elementID) {
            var _this = this;
            var divElement = $('#' + elementID + '');
            var type = {
                ProductId: divElement.find('#productId').val(),
                SKU: divElement.find('#sku').val(),
                Stock: divElement.find('#stock').val(),
                AlertStock: divElement.find('#alertstock').val(),
                Price: divElement.find('#price').val(),
            };
            type = $.param(type, true);
            Global.post("/Product/AddProductSku", type, function (data) {
                window.location.href = '/Product/SkuIndex';
            });
        },
        //绑定公司信息
        bindProductId: function (productId, name) {
            $("#productId").val(productId);
        }
        

    };

    //对外公布方法
    exports.init = function (options) {
        return new AddProductSku(options);
    }
    return exports
});