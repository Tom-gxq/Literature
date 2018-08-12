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
     
    require('food/style.css');
    //默认参数
    var Defaults = {
        dialogID: "addproduct",//弹出框ID
        orderID: ''
    };

    var AddProduct = function (options) {
        var _this = this;
        _this.setting = $.extend({}, Defaults, options);
        _this.init(function () {
            _this.showDialog();
        });
        return this;
    }
    AddProduct.prototype = {
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
            doT.exec('food/addproduct.html', function (templateFun) {
            var innerText;
            innerText = templateFun(elementID);
            easydialog.open({
                container: {
                    id: "addproduct",
                    header: '添加商家产品',
                    content: innerText
                }
            });
            $('#wizard').css('height', $('#addproduct').find('.page').height());

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
                element: "#productName",
                url: 'SearchTypeProductByKeyWord',
                selectCallback: function (value, text) {
                    _this.bindProductInfo(value, text);
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
                    return {
                        keywords: request.term,
                        typeId: _this.setting.typeId
                    };
                }
            });
            });
                    
         
            //_this.bindEvent();
        },        
        Submit: function (elementID) {
            var _this = this;
            var divElement = $('#' + elementID + '');
            var type = {
                shopId: $('#shopId').val(),
                productId: $("#productId").val(),
                preStock: divElement.find('#preStock').val(),
                accountId: _this.setting.accountId,
            };
            type = $.param(type, true);
            alert(type);
            Global.post("/Default/AddFoodProduct", type, function (data) {
                _this.setting.callBack(data);
            });
        },
        //绑定产品信息
        bindProductInfo: function (productId, name) {
            $("#productId").val(productId);
        },
    };

    //对外公布方法
    exports.init = function (options) {
        return new AddProduct(options);
    }
    return exports
});