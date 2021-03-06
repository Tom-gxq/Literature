﻿/*添加引用
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
    require('sellerdetail/style.css');
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
            $.ajax({
                url: 'GetProductDetail',
                type: 'GET',
                cache: false,
                width: 500,
                data: {
                    productId: _this.setting.Id
                },
                success: function (msg) {
                    if (msg) {
                        doT.exec('sellerdetail/addproduct.html', function (templateFun) {
                            var innerText;
                            innerText = templateFun(msg);
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
                            qiniu.QiniuMainController.init();

                            $("#inputTypeForm").change(function () {
                                _this.bindMainTypeInfo($(this).val());
                            });

                            var completeInfo = require('complete');
                            completeInfo.createComplete({
                                element: "#leader",
                                url: 'SearchAccountByKeyWord',
                                selectCallback: function (value, text) {
                                    _this.bindAccountId(value, text);
                                },
                                asyncCallback: function (response, data) {

                                    response($.map(data.items, function (item) {
                                        return {
                                            label: item.Fullname,
                                            valueKey: item.AccountId
                                        }
                                    }));
                                },
                                data: function (request) {
                                    $(".clipLoader").show();
                                    return { keywords: request.term };
                                }
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
            var type = {
                mainType: divElement.find('#inputTypeForm').val(),
                secondType: divElement.find('#inputSecondTypeForm').val(),
                productName: divElement.find('#productName').val(),
                marketPrice: divElement.find('#marketPrice').val(),
                purchasePrice: divElement.find('#purchasePrice').val(),
                imagePath: divElement.find('#imgPath').val(),
                accountId: $('#accountId').val(),
                vipPrice: divElement.find('#vipPrice').val(),
            };
            type = $.param(type, true);
            alert(type);
            Global.post("/Default/AddProduct", type, function (data) {
                _this.setting.callBack(data);
            });
        },
        //绑定产品信息
        bindAccountId: function (accountId, name) {
            $("#leaderId").val(accountId);
        },
        //绑定学区信息
        bindMainTypeInfo: function (parentId) {
            $("#inputSecondTypeForm").empty();
            $("#inputSecondTypeForm").append($("<option/>").text("").attr("value", "0"));
            var data = {
                parentTypeId: parentId
            }
            data = $.param(data, true);
            Global.get("/Default/GetChildType", data, function (msg) {
                $(msg.items).each(function (index, itemData) {
                    $("#inputSecondTypeForm").append($("<option/>").text(itemData.TypeName).attr("value", itemData.TypeId));
                });
            })
        }
    };

    //对外公布方法
    exports.init = function (options) {
        return new AddProduct(options);
    }
    return exports
});