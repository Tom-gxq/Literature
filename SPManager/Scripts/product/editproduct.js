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
    qiniu = require('qiniumain');
    require('./style.css');
    //默认参数
    var Defaults = {
        dialogID: "editproduct",//弹出框ID
        orderID: ''
    };

    var EditType = function (options) {
        var _this = this;
        _this.setting = $.extend({}, Defaults, options);
        _this.init(function () {
            _this.showDialog();
        });
        return this;
    }
    EditType.prototype = {
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
                width:500,
                data: {
                    productId: _this.setting.Id
                },
                success: function (msg) {
                    if (msg.result ) {
                        doT.exec('product/editproduct.html', function (templateFun) {
                            var innerText;
                            innerText = templateFun(msg);
                            easydialog.open({
                                container: {
                                    id: "editproduct",
                                    header: '编辑产品',
                                    content: innerText
                                }
                            });
                            $('#wizard').css('height', $('#' + msg.result.Id).find('.page').height());

                            $(".page").show();
                            $("#inputType").change(function () {
                                _this.bindTypeInfo($(this).val());
                            });
                            //取消
                            $("#cacel").click(function () {
                                _this.bindTypeInfo($(this).val());
                                easydialog.close();
                            });

                            //提交
                            $("#sub").click(function () {
                                _this.Submit(elementID);
                            });
                            //添加属性
                            $("#add").click(function () {
                                _this.AddAttr(elementID);
                            });
                            //删除属性
                            $("#del").click(function () {
                                _this.DelAttr(elementID);
                            });
                            //删除图片
                            $("#picdel").click(function () {
                                _this.DelPic(elementID);
                            });
                            $('#easyDialogBox').css({                                
                                width: "600px"
                            });
                           
                            qiniu.QiniuMainController.init();
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
                ProductId: divElement.find('.txtId').val(),
                ProductName: divElement.find('.txtName').val(),
                ProductCode: divElement.find('.txtCode').val(),
                MarketPrice: divElement.find('.txtPrice').val(),
                Unit: divElement.find('.txtUnit').val(),
                ShortDescription: divElement.find('.txtTitle').val(),
                Description: divElement.find('.txtArea').val(),
                TypeId: divElement.find('#inputType').val(),
                SecondTypeId: divElement.find('#inputSecondType').val(),
                BrandId: divElement.find('#inputBrand').val(),
                DisplaySequence: divElement.find('.sltContact').val(),
                VIPPrice: divElement.find('.vipPrice').val(),
                PurchasePrice: divElement.find('.prchasePrice').val()
            };
            user = $.param(user, true);
            Global.post("/Product/EditProduct", user, function (data) {
                _this.setting.callBack(data);
            });
        },
        AddAttr: function (elementID) {
            var _this = this;
            var divElement = $('#' + elementID + '');
            var user = {
                AttributeId: divElement.find('#inputAttr').val(),
                ProductId: divElement.find('.txtId').val()

            };
            user = $.param(user, true);
            Global.post("/Product/AddProductAttribute", user, function (data) {
                var data = {
                    id: divElement.find('#inputAttr').val()
                }
                data = $.param(data, true);
                Global.get("/Attribute/GetAttributeDetail", data, function (msg) {
                    $("#vallist tr").append('<td><input type="checkbox" name="attr" class="checkbox table-ckbs" value="' + msg.result.Id + '"/>' + msg.result.AttributeName + '</td>');
                });
                
            });
        },
        DelAttr: function (elementID) {
            var _this = this;
            var divElement = $('#' + elementID + '');
            $("#vallist").find("tr").each(function () {//循环遍历每一行
                $(this).find("td input[name='attr']:checked").each(function () {                    
                    var user = {
                        AttributeId: $(this).val(),
                        ProductId: divElement.find('.txtId').val()
                    };
                    user = $.param(user, true);
                    var tdObj = $(this).parent();
                    Global.post("/Product/DeleteProductAttribute", user, function (data) {
                        if (data.result) {
                            tdObj.remove(); //删除td所在行
                        }
                    });
                    
                });                
                  
            });
        },
        DelPic: function (elementID) {
            var _this = this;
            var divElement = $('#' + elementID + '');
            $("#preview").find("input[name='picattr']:checked").each(function () {
                var user = {
                    imageId: $(this).val(),
                    ProductId: divElement.find('.txtId').val()
                };
                user = $.param(user, true);
                var tdObj = $(this);
                Global.post("/Product/DeleteProductImage", user, function (data) {
                    if (data.result) {
                        tdObj.remove(); //删除td所在行
                    }
                });

            });
        },
        //绑定学区信息
        bindTypeInfo: function (parentId) {
            $("#inputSecondType").empty();
            $("#inputSecondType").append($("<option/>").text("").attr("value", "0"));
            var data = {
                parentId: parentId
            }
            data = $.param(data, true);
            Global.get("/ProductType/GetProductTypeParentList", data, function (msg) {
                $(msg.result).each(function (index, itemData) {
                    $("#inputSecondType").append($("<option/>").text(itemData.TypeName).attr("value", itemData.TypeId));
                });
            })
        },
    };
    
    //对外公布方法
    exports.init = function (options) {
        return new EditType(options);
    }
    return exports
});