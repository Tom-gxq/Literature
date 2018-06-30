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
    qiniu = require('qiniushop');
    require('shop/style.css');
    require('jqueryui');
  
    //默认参数
    var Defaults = {
        dialogID: "editshop",//弹出框ID
        orderID: ''
    };

    var EditShop = function (options) {
        var _this = this;
        _this.setting = $.extend({}, Defaults, options);
        _this.init(function () {
            _this.showDialog();
        });
        return this;
    }
    EditShop.prototype = {
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
                url: 'GetShopDetail',
                type: 'GET',
                cache: false,
                data: {
                    id: _this.setting.Id
                },
                success: function (msg) {
                    if (msg.result ) {
                        doT.exec('shop/editshop.html', function (templateFun) {
                            var innerText;
                            innerText = templateFun(msg);
                            easydialog.open({
                                container: {
                                    id: "editshop",
                                    header: '编辑店铺',
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
                            qiniu.QiniuMainController.init();

                            var completeInfo = require('complete');
                            completeInfo.createComplete({
                                element: "#account",
                                url: '/Account/SearchAccount',
                                selectCallback: function (value, text) {
                                    _this.bindCompanyInfo(value, text);
                                },
                                asyncCallback: function (response, data) {
                                    console.log(data);
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
                            $("#inputSchool").change(function () {
                                _this.bindSchoolInfo($(this).val());
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
            var user = {
                Id: divElement.find('.txtId').val(),
                ShopName: divElement.find('.txtName').val(),
                OwnerId: divElement.find('.owner').val(),
                AttributeId: divElement.find('#inputAttribute').val(),
                RegionId: divElement.find('#inputArea').val(),
                DisplaySequence: divElement.find('.sltContact').val(),
                StartTime: divElement.find('.startTime').val(),
                EndTime: divElement.find('.endTime').val(),
                ShopType: divElement.find('#shopType').val(),
                ShopLogo: divElement.find('#imgPath').val(),
            };
            user = $.param(user, true);
            Global.post("/Shop/EditShop", user, function (data) {
                _this.setting.callBack(data);
            });
        },
        //绑定公司信息
        bindCompanyInfo: function (accountId, name) {
            $("#accountid").val(accountId);
        },
        //绑定城市信息
        bindCityInfo: function (parentId) {
            $("#inputCity").empty();
            $("#inputArea").empty();
            $("#inputSchool").empty();
            $("#inputCity").append($("<option/>").text("").attr("value", "-1"));
            $("#inputArea").append($("<option/>").text("").attr("value", "-1"));
            $("#inputSchool").append($("<option/>").text("").attr("value", "-1"));
            var data = {
                parentId: parentId
            }
            data = $.param(data, true);
            Global.get("/RegionData/GetChildRegionDataBorShopMenu", data, function (msg) {
                $(msg.items).each(function (index,itemData) {
                    $("#inputCity").append($("<option/>").text(itemData.DataName).attr("value", itemData.DataId));
                });
            })
        },
        //绑定区信息
        bindAreaInfo: function (parentId) {
            $("#inputArea").empty();
            $("#inputSchool").empty();
            $("#inputArea").append($("<option/>").text("").attr("value", "-1"));
            $("#inputSchool").append($("<option/>").text("").attr("value", "-1"));
            var data = {
                parentId: parentId
            }
            data = $.param(data, true);
            Global.get("/RegionData/GetChildRegionDataBorShopMenu", data, function (msg) {
                $(msg.items).each(function (index, itemData) {
                    $("#inputArea").append($("<option/>").text(itemData.DataName).attr("value", itemData.DataId));
                });
            })
        },
        //绑定学区信息
        bindSchoolInfo: function (parentId) {
            $("#inputArea").empty();
            $("#inputArea").append($("<option/>").text("").attr("value", "-1"));
            var data = {
                parentId: parentId
            }
            data = $.param(data, true);
            Global.get("/RegionData/GetChildRegionDataBorShopMenu", data, function (msg) {
                $(msg.items).each(function (index, itemData) {
                    $("#inputArea").append($("<option/>").text(itemData.DataName).attr("value", itemData.DataId));
                });
            })
        },
    };
    
    //对外公布方法
    exports.init = function (options) {
        return new EditShop(options);
    }
    return exports
});