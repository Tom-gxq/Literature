/**
 * producttype.js
 * @authors 
 * @date    
 * @version $Id$
 */

define(function (require, exports, module) {

    var $ = require('jquery'),
        common = require('global'),
        doT = require('dot');
    var datapager = require('datapager');
    var easydialog = require("easydialog");
    var Global = common.Global;
    var addProduct = require('add-foodproduct');
    var addOwner = require('add-owner');
    //基础扩展
    var base = {
        bindElements: function () {
            for (var key in this.elements) {
                this[this.elements[key]] = $(key);
            }
        },
        bindEvents: function () {
            var eventSplitter = /^(\w+)\s*(.*)$/;
            for (var key in this.events) {
                var methodName = this.events[key];
                var method = $.proxy(this[methodName], this);
                var match = key.match(eventSplitter);
                var eventName = match[1],
                    selector = match[2];
                if (selector === '') {

                } else {
                    this.el.delegate(selector, eventName, function () {
                        method($(this))
                    });
                }
            }
        }
    };

    /**
     * 分类设置模块
     */
    var FoodProductController = $.extend({
        /**
         * 模块入口
         */
        init: function (para) {
            var _self = this;

            $.extend(this, para);
            _self.bindElements();

            //获取数据
            _self.bindEvent();
        },
        //元素
        elements: {
            '#dataTable': 'orderArea',
            // 分页元素
            '#pager': 'pager'
        },
        
        bindEvent: function () {
            var _self = this;
            
            $(".cardadd").click(function (obj) {
                //弹出添加的操作框
                addProduct.init({
                    stepOne: true,
                    dialogID: 'wizard',
                    header: '添加餐饮产品',
                    accountId: $(this).find(".account").attr("dataid"),
                    typeId: $("#typeId").val(),
                    callBack: function (result) {
                        
                        if (result) {
                            //重新加载页面
                            window.location.href = '/Default/ShopDetails?shopId=' + $("#shopId").val();
                        } else {
                            alert('添加餐饮产品失败');
                        }
                    }
                });
            });

            $(".addowner").click(function (obj) {
                //弹出添加的操作框
                addOwner.init({
                    stepOne: true,
                    dialogID: 'wizard',
                    header: '添加配送人员',
                    accountId: $(this).find(".account").attr("dataid"),
                    typeId: $("#typeId").val(),
                    callBack: function (result) {

                        switch (result.status)
                        {
                            case 0:
                                //重新加载页面
                                window.location.href = '/Default/ShopDetails?shopId=' + $("#shopId").val();
                                break;
                            case 1:
                                easydialog.open({
                                    container: {
                                        content: '该用户已经是其它店铺的配送人员，不能任职多个店铺'
                                    }
                                });
                                setTimeout(function () { easydialog.close(); },1500);
                                break;
                            case 2:
                                easydialog.open({
                                    container: {
                                        content: '该用户已经是商户，不能任职店铺配送人员'
                                    }
                                });
                                setTimeout(function () { easydialog.close(); }, 1500);
                                break;
                            case -1:
                            default:
                                easydialog.open({
                                    container: {
                                        content: '添加配送人员失败'
                                    }
                                });
                                setTimeout(function () { easydialog.close(); }, 1500);
                                break;
                        }
                    }
                });
            });

            var completeInfo = require('complete');
            completeInfo.createComplete({
                element: "#distribution",
                url: 'SearchAccountByKeyWord',
                selectCallback: function (value, text) {
                    _self.bindProductInfo(value, text);
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
            
        },
        
        /**
         * 验证文本框
         * @param  {[验证文本]} newVal
         */
        validate: function (newVal) {
            if (!newVal || newVal.length < 1)
                return false;
            return true;
        },
             
        //绑定产品信息
        bindAccountId: function (accountId, name) {
            $("#leaderId").val(accountId);
        },
        
    }, base);

    exports.jQuery = $;
    exports.FoodProductController = FoodProductController;
});