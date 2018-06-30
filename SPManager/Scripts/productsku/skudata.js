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
    var addsku = require('add-sku');
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
    var SkuController = $.extend({
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
            '#J_ItemList': 'SkuArea',
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
            _self.SkuArea.find(".contenttr").remove();
            doT.exec('productsku/list-item.html', function (templateFun) {
                items = templateFun(data);
                items = $(items);
                _self.bindItemEvent(items);
                _self.SkuArea.append(items);
            });
        },
        bindEvent: function () {
            var _self = this;
            $('#btnAddButton').bind('click', function () {
                //弹出添加的操作框
                addsku.init({
                    stepOne: true,
                    dialogID: 'wizard',
                    header: '添加库存',
                    callBack: function (result) {
                        if (result) {
                            //重新加载页面
                            window.location.href = '/Product/SkuIndex';
                        } else {
                            alert('添加库存失败');
                        }
                    }
                });
            });
            
            $(".btn_table_selectAll").click(function () {
                $('.contenttr').find("input").prop("checked", true);
            });
            $(".btn_table_Cancle").click(function () {
                $('.contenttr').find("input").prop("checked", false);
            });
            $(".btn_AllDel").click(function () {
                $('.contenttr').find("input[type=checkbox]").each(function () {
                    //由于复选框一般选中的是多个,所以可以循环输出
                    var item = $(this).parent("td").parent("tr");
                    if ($(this).prop("checked") == true) {
                        Global.post('DeleteBrand', {
                            brandId: $(this).val()
                        },
                            function (data) {
                                if (data && data.result) {
                                    item.remove();
                                }
                            }
                        );
                    }
                });
            });
            var completeInfo = require('complete');
            completeInfo.createComplete({
                element: "#inputSearch",
                url: '/Product/SearchRegionData',
                selectCallback: function (value, text) {
                    _self.bindCompanyInfo(value, text);
                },
                asyncCallback: function (response, data) {

                    response($.map(data.items, function (item) {
                        return {
                            label: item.DataName,
                            valueKey: item.DataId
                        }
                    }));
                },
                data: function (request) {
                    $(".clipLoader").show();
                    return { dataName: request.term };
                }
            });

            $("#inputSchool").change(function () {
                _self.bindSchoolInfo($(this).val());
            });
            $("#inputDistrict").change(function () {
                _self.bindDistrictInfo($(this).val());
            });
            $("#inputShop").change(function () {
                _self.bindShopInfo($(this).val());
            });
            $("#btnSearch").click(function () {
                _self.searchProductInfo(1, {});
            });
        },
        /**
         * 生成模版时绑定删除，失去焦点事件
         * @param  {[数据项文本]} item
         */
        bindItemEvent: function (item) {
            var _self = this;

            item.find('button.delete_item').click(function (event) {
                _self.deleteProductSku(this);
                event.stopPropagation();
                return false;
            });
            item.find('button.addOne_item').click(function (event) {
                _self.addOneProductSku(this);
                event.stopPropagation();
                return false;
            });
            item.find('button.delOne_item').click(function (event) {
                _self.delOneProductSku(this);
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
        deleteProductSku: function (ele) {
            ele = $(ele);
            if (!ele.hasClass('delete_item')) return;
            var res = confirm('确定要删除这个库存吗?');
            if (res) {

                var input = ele.parent('td'),
                    id = input.attr('data-item_id'),
                    item = input.parent('tr');

                Global.post('DeleteProductSku', {
                    skuId: id
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
        addOneProductSku: function (ele) {
            ele = $(ele);
            if (!ele.hasClass('addOne_item')) return;
            
            var input = ele.parent('td'),
                id = input.attr('data-item_id'),
                item = input.parent('tr');

            Global.post('AddOneProductSku', {
                skuId: id
            },
                function (data) {
                    //重新加载页面
                    window.location.href = '/Product/SkuIndex';
                }
            );
            event.stopPropagation();
            return false;
        },
        delOneProductSku: function (ele) {
            ele = $(ele);
            if (!ele.hasClass('delOne_item')) return;

            var input = ele.parent('td'),
                id = input.attr('data-item_id'),
                item = input.parent('tr');

            Global.post('DelOneProductSku', {
                skuId: id
            },
                function (data) {
                    //重新加载页面
                    window.location.href = '/Product/SkuIndex';
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
            $.ajax({
                url: 'GetProducSkuList',
                type: 'GET',
                cache: false,
                data: {
                    pageIndex: index,
                    pageSize:20
                }, 
                success: function (msg) {
                    if (msg.items && msg.items.length > 0) {
                        _self.dataBinding(msg.items);
                        _self.pager.paginate({
                            total_count: msg.data.Total,
                            count: msg.data.Pages,
                            start: msg.data.Index,
                            display: 10,
                            onChange: function (page) {
                                _self.getDataSource(page);
                            }
                        });
                    }
                },
                error: function (err) {

                }
            });
        },
        //绑定学区信息
        bindSchoolInfo: function (parentId) {
            $("#inputDistrict").empty();
            $("#inputDistrict").append($("<option/>").text("院区名称").attr("value", "0"));
            $("#inputShop").empty();
            $("#inputShop").append($("<option/>").text("店铺名称").attr("value", "0"));
            $("#inputProduct").empty();
            $("#inputProduct").append($("<option/>").text("产品名称").attr("value", ""));
            var data = {
                parentId: parentId
            }
            data = $.param(data, true);
            Global.get("/RegionData/GetChildRegionDataBorShopMenu", data, function (msg) {
                $(msg.items).each(function (index, itemData) {
                    $("#inputDistrict").append($("<option/>").text(itemData.DataName).attr("value", itemData.DataId));
                });
            })
        },
        //绑定店铺信息
        bindDistrictInfo: function (parentId) {
            $("#inputShop").empty();
            $("#inputShop").append($("<option/>").text("店铺名称").attr("value", "0"));
            $("#inputProduct").empty();
            $("#inputProduct").append($("<option/>").text("产品名称").attr("value", ""));
            var data = {
                regionId: parentId
            }
            data = $.param(data, true);
            Global.get("/Shop/GetFoodShopListByRegionId", data, function (msg) {
                $(msg.items).each(function (index, itemData) {
                    $("#inputShop").append($("<option/>").text(itemData.ShopName).attr("value", itemData.Id));
                });
            })
        },
        //绑定产品信息
        bindShopInfo: function (parentId) {
            $("#inputProduct").empty();
            $("#inputProduct").append($("<option/>").text("产品名称").attr("value", ""));
            var data = {
                shopId: parentId
            }
            data = $.param(data, true);
            Global.get("/Product/GetAllShopProductList", data, function (msg) {
                $(msg.items).each(function (index, itemData) {
                    $("#inputProduct").append($("<option/>").text(itemData.ProductName).attr("value", itemData.ProductId));
                });
            })
        },
        //搜寻产品信息
        searchProductInfo: function (parentId) {
            var _self = this;
            var data = {
                schoolId: $("#inputSchool").val(),
                districtId: $("#inputDistrict").val(),
                shopId: $("#inputShop").val(),
                productId: $("#inputProduct").val(),
                skuStatus: $("#inputSku").val(),
            }
            data = $.param(data, true);
            Global.get("/Product/SearchProducSku", data, function (msg) {
                _self.dataBinding(msg.items);                
            })
        },
    }, base);

    exports.jQuery = $;
    exports.SkuController = SkuController;
});