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
            _self.getSellerChart(7, 1);
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
            $(".fa-close").click(function (obj) {
                var dataId = this.getAttribute("dataid");
                var leaderId = $("#leaderId").val();
                Global.post('DelProduct', {
                    productId: dataId,
                    suplierId: leaderId
                },
                    function (data) {
                        switch (data.status) {
                            case 0:
                                //重新加载页面
                                window.location.href = '/Default/SellerDetails?sellerId=' + $("#sellerId").val();
                                break;
                            case 1:
                                easydialog.open({
                                    container: {
                                        content: '该产品不是这个负责人的'
                                    }
                                });
                                setTimeout(function () { easydialog.close(); }, 1500);
                                break;
                            
                            default:
                                easydialog.open({
                                    container: {
                                        content: '删除商家产品失败'
                                    }
                                });
                                setTimeout(function () { easydialog.close(); }, 1500);
                                break;
                        }
                    }
                );
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

            $("#orderAmount").click(function (obj) {
                _self.getSellerChart(7, 2);
            });

            $("#orderNum").click(function (obj) {
                _self.getSellerChart(7, 1);
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
        getSellerChart:function GetSellerChart(day, type) {
            //----Line Chart---------            
            $.ajax({
                url: 'GetSellerLineChartData',
                type: 'GET',
                cache: false,
                data: {
                    day: day,
                    sellerId: $("#sellerId").val(),
                    type: type
                },
                success: function (msg) {
                    var tData = msg;
                    var c = document.getElementById("SellerChart");
                    var ctx = c.getContext("2d");
                    var ticks = {};
                    if (type == 1) {
                        ticks = {
                            min: 0,
                            max: 20,
                            maxTicksLimit: 5
                        };
                    }
                    else {
                        ticks = {
                            min: 0,
                            max: 5000,
                            maxTicksLimit: 5
                        };
                    }
                    var myLineChart = new Chart(ctx, {
                        type: 'line',
                        data: tData,
                        options: {
                            scales: {
                                xAxes: [{
                                    time: {
                                        unit: 'date'
                                    },
                                    gridLines: {
                                        display: false
                                    },
                                }],
                                yAxes: [{
                                    ticks: ticks,
                                    gridLines: {
                                        color: "rgba(0, 0, 0, .125)",
                                    }
                                }],
                            },
                        }
                    });
                },
                error: function (err) {

                }
            });
        }
    }, base);

    exports.jQuery = $;
    exports.SellerDetailController = SellerDetailController;
});