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
    var ProductController = $.extend({
        /**
         * 模块入口
         */
        init: function (para) {
            var _self = this;

            $.extend(this, para);
            _self.bindElements();

            //获取数据
            _self.getDataSource(1);
            _self.bindEvent();
        },
        //元素
        elements: {
            '#J_ItemList': 'productArea',
            // 分类
            '#inputTypeForm': 'inputTypeForm',
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
            doT.exec('shop/shopproduct.html', function (templateFun) {
                items = templateFun(data);
                items = $(items);
                _self.bindItemEvent(items);
                _self.productArea.append(items);
            });
        },
        bindEvent: function () {
            var _self = this;
            
            $("#btnSearch").click(function () {
                _self.getProductList(1, {});
            });
            $("#inputSearch").keypress(function (event) {
                if (event.keyCode == 13)
                    _self.getProductList(1, {});

            });
            $(".btn_table_selectAll").click(function () {
                $('.contenttr').find("input").prop("checked", true);
            });
            $(".btn_table_Cancle").click(function () {
                $('.contenttr').find("input").prop("checked", false);
            });
            $(".btn_AllUp").click(function () {
                $('.contenttr').find("input[type=checkbox]").each(function () {
                    //由于复选框一般选中的是多个,所以可以循环输出
                    var item = $(this).parent("td").parent("tr");
                    if ($(this).prop("checked") == true) {
                        Global.post('UpdateProductSaleStatus', {
                            productId: $(this).val(),
                            saleStatus: 1
                        },
                            function (data) {
                                if (data && data.result) {
                                    //重新加载页面
                                    window.location.href = '/Product/Index';
                                } else {
                                    alert('更新失败！');
                                }
                            }
                        );
                    }
                });
            });
            $(".btn_AllDown").click(function () {
                Global.post('DelShopProductByShopId', {
                    ShopId: Global.getUrlParam("shopId")
                },
                    function (data) {
                        if (data && data.result) {
                            
                        } else {
                            alert('更新失败！');
                        }
                    }
                );
                $('.contenttr').find("input[type=checkbox]").each(function () {
                    //由于复选框一般选中的是多个,所以可以循环输出
                    var item = $(this).parent("td").parent("tr");
                    if ($(this).prop("checked") == true) {
                        Global.post('AddShopProduct', {
                            productId: $(this).val(),
                            ShopId: Global.getUrlParam("shopId")
                        },
                            function (data) {
                                if (data && data.result) {
                                    //重新加载页面
                                    window.location.href = '/Shop/ProductManager?shopId=' + Global.getUrlParam("shopId");
                                } else {
                                    alert('更新失败！');
                                }
                            }
                        );
                    }
                });
            });
        },
        /**
         * 生成模版时绑定删除，失去焦点事件
         * @param  {[数据项文本]} item
         */
        bindItemEvent: function (item) {
            var _self = this;

            item.find('button.delete_item').click(function (event) {
                _self.deleteUser(this);
                event.stopPropagation();
                return false;
            });
            item.find('button.status_item').click(function (event) {
                _self.UpdateSaleStatus(this);
                event.stopPropagation();
                return false;
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
        /**
         * 删除角色
         * @param  {[删除按钮]} ele
         */
        deleteUser: function (ele) {
            ele = $(ele);
            if (!ele.hasClass('delete_item')) return;
            var res = confirm('确定要删除这个商品吗?');
            if (res) {

                var input = ele.parent('td'),
                    id = input.attr('data-item_id'),
                    item = input.parent('tr');

                Global.post('DeleteProduct', {
                    productId: id
                },
                    function (data) {
                        if (data && data.result) {
                            item.remove();
                        } else {
                            alert('删除失败！');
                        }
                    }
                );
            }
            event.stopPropagation();
            return false;
        },
        /**
         * 更新销售状态
         * @param  {[销售状态按钮]} ele
         */
        UpdateSaleStatus: function (ele) {
            ele = $(ele);
            var saleStatus = ele.attr("data-status");
            if (saleStatus == 0) {
                saleStatus = 1;
            }
            else {
                saleStatus = 0;
            }
            Global.post('UpdateProductSaleStatus', {
                productId: ele.attr("id"),
                saleStatus: saleStatus
            },
                function (data) {
                    if (data && data.result) {
                        //重新加载页面
                        window.location.href = '/Product/Index';
                    } else {
                        alert('更新失败！');
                    }
                }
            );
            event.stopPropagation();
            return false;
        },
        /**
         * Ajax 请求获取角色列表,并调用模版进行UI显示
         */
        getDataSource: function (index) {
            var _self = this;
            var salestatus = $("#salestatus").val();
            $.ajax({
                url: '/Product/GetShopProductList',
                type: 'GET',
                cache: false,
                data: {
                    shopId: Global.getUrlParam("shopId"),
                    saleStatus: salestatus,
                    pageIndex: index,
                    pageSize: 20
                },
                success: function (msg) {
                    if (msg.result && msg.result.length > 0) {
                        _self.dataBinding(msg.result);
                        _self.pager.paginate({
                            total_count: msg.data.Total,
                            count: msg.data.Pages,
                            start: msg.data.Index,
                            display: 10,
                            onChange: function (page) {
                                $("#J_ItemList  tr:not(:first)").empty("");
                                _self.getDataSource(page);
                            }
                        });
                    }
                },
                error: function (err) {

                }
            });
        },
        getProductList: function (index) {
            var _self = this;
            var salestatus = $("#salestatus").val();
            var brandId = $("#inputBrand").val();
            var typeId = $("#inputTypeForm").val();
            $.ajax({
                url: '/Product/SearchProductList',
                type: 'GET',
                cache: false,
                data: {
                    keyWord: $("#inputSearch").val(),
                    typeId: typeId,
                    brandId: brandId,
                    saleStatus: salestatus,
                    pageIndex: index,
                    pageSize: 20
                },
                success: function (msg) {
                    _self.productArea.find(".contenttr").remove();
                    if (msg.result && msg.result.length > 0) {
                        _self.dataBinding(msg.result);
                        _self.pager.paginate({
                            total_count: msg.data.Total,
                            count: msg.data.Pages,
                            start: msg.data.Index,
                            display: 10,
                            onChange: function (page) {
                                _self.getProductList(page);
                            }
                        });
                    }
                },
                error: function (err) {

                }
            });
        }
    }, base);

    exports.jQuery = $;
    exports.ProductController = ProductController;
});