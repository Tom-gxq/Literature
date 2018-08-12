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
    var addProduct = require('add-product');
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
    var SellerDetailController = $.extend({
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
                    header: '添加商家商品',
                    callBack: function (result) {
                        alert("result=" + result)
                        if (result) {
                            //重新加载页面
                            window.location.href = '/Default/SellerDetails?sellerId=' + $("#sellerId").val();
                        } else {
                            alert('添加商家产品失败');
                        }
                    }
                });
            });

            
            var completeInfo = require('complete');
            completeInfo.createComplete({
                element: "#productSearch",
                url: 'SearchProductByKeyWord',
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
        bindProductInfo: function (productId, name) {
            $("#productId").val(productId);
        },
        
    }, base);

    exports.jQuery = $;
    exports.SellerDetailController = SellerDetailController;
});