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
    var addSeller = require('add-seller');
    var addlic = require('add-lic');
    var addpermit = require('add-permit');
    var addauth = require('add-auth');
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
    var SellerController = $.extend({
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
        /**
         * 页面数据列表绑定
         * @param  {[JSON]} data
         */
        dataBinding: function (data) {
            var _self = this
            items = '';
            doT.exec('order/list-item.html', function (templateFun) {
                items = templateFun(data);
                items = $(items);
                _self.bindItemEvent(items);
                _self.orderArea.append(items);
            });
        },
        bindEvent: function () {
            var _self = this;
            
            $(".card-delete-icon").click(function () {
                var dataId = this.getAttribute("dataid");
                Global.post('DelSeller', {
                    id: dataId
                },
                    function (data) {
                        //重新加载页面
                        window.location.href = '/Default/ShopManager';
                    }
                );
            });
            $("#btnSearch").click(function () {   
                var productId = $("#productId").val();
                var sellerId = $("#sellerId").val();
                var type = $("#selleType").val();
                window.location.href = 'ShopManager?productId=' + productId + '&sellerId=' + sellerId + '&type=' + type;
            });
            $("#btnAdd").click(function (obj) {
                //弹出添加的操作框
                addSeller.init({
                    stepOne: true,
                    dialogID: 'wizard',
                    header: '添加商家',
                    callBack: function (result) {
                        switch (result.status) {
                            case 0:
                                //重新加载页面
                                window.location.href = '/Default/ShopManager';
                                break;
                            case 1:
                                easydialog.open({
                                    container: {
                                        content: '该用户已经是其它店铺的配送人员，不能成为商户'
                                    }
                                });
                                setTimeout(function () { easydialog.close(); }, 1500);
                                break;
                            case 2:
                                easydialog.open({
                                    container: {
                                        content: '该用户已经是商户'
                                    }
                                });
                                setTimeout(function () { easydialog.close(); }, 1500);
                                break;
                            case -1:
                            default:
                                easydialog.open({
                                    container: {
                                        content: '添加商家失败'
                                    }
                                });
                                setTimeout(function () { easydialog.close(); }, 1500);
                                break;
                        }
                    }
                });
            });

            $(".card-body").click(function (obj) {
                console.log(this.getAttribute("dataid"));
                if (this.getAttribute("dataid") > 0) {                    
                    window.location.href = '/Default/SellerDetails?sellerId=' + this.getAttribute("dataid");
                }
            });
            $(".card-footer").find(".lic").click(function (obj) {
                console.log(this.getAttribute("dataid"));
                //弹出添加的操作框
                addlic.init({
                    stepOne: true,
                    dialogID: 'wizard',
                    header: '添加商家营业执照',
                    objId: this.getAttribute("dataid"),
                    callBack: function (result) {
                        alert("result=" + result)
                        if (result) {
                            //重新加载页面
                            window.location.href = '/Default/ShopManager';
                        } else {
                            alert('添加失败');
                        }
                    }
                });
            });
            $(".card-footer").find(".permit").click(function (obj) {
                console.log(this.getAttribute("id"));
                //弹出添加的操作框
                addpermit.init({
                    stepOne: true,
                    dialogID: 'wizard',
                    header: '添加商家营业许可',
                    objId: this.getAttribute("dataid"),
                    callBack: function (result) {
                        alert("result=" + result)
                        if (result) {
                            //重新加载页面
                            window.location.href = '/Default/ShopManager';
                        } else {
                            alert('添加失败');
                        }
                    }
                });
            });
            $(".card-footer").find(".auth").click(function (obj) {
                console.log(this.getAttribute("id"));
                //弹出添加的操作框
                addauth.init({
                    stepOne: true,
                    dialogID: 'wizard',
                    header: '添加商家授权函',
                    objId: this.getAttribute("dataid"),
                    callBack: function (result) {
                        alert("result=" + result)
                        if (result) {
                            //重新加载页面
                            window.location.href = '/Default/ShopManager';
                        } else {
                            alert('添加失败');
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
            completeInfo.createComplete({
                element: "#sellerSearch",
                url: 'SearchSellerData',
                selectCallback: function (value, text) {
                    _self.bindSellerInfo(value, text);
                },
                asyncCallback: function (response, data) {

                    response($.map(data.items, function (item) {
                        return {
                            label: item.SuppliersName,
                            valueKey: item.Id
                        }
                    }));
                },
                data: function (request) {
                    $(".clipLoader").show();
                    return { name: request.term };
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
        //绑定商家信息
        bindSellerInfo: function (sellerId, name) {
            $("#sellerId").val(sellerId);
        },
    }, base);

    exports.jQuery = $;
    exports.SellerController = SellerController;
});