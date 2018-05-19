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
    var addregion = require('add-region');
    var editregion = require('edit-region');
    var genderbuilding = require('gender-building');
    var genderdorm = require('gender-dorm');
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
    var RegionController = $.extend({
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
            '#J_ItemList': 'brandArea',
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
            doT.exec('regiondata/list-item.html', function (templateFun) {
                items = templateFun(data);
                items = $(items);
                _self.bindItemEvent(items);
                _self.brandArea.append(items);
            });
        },
        bindEvent: function () {
            var _self = this;
            $('#btnAddButton').bind('click', function () {
                //弹出添加的操作框
                addregion.init({
                    stepOne: true,
                    dialogID: 'wizard',
                    header: '添加区域',
                    callBack: function (result) {
                        if (result) {
                            //重新加载页面
                            window.location.href = '/RegionData/Index';
                        } else {
                            alert('添加区域失败');
                        }
                    }
                });
            });

            $("#btnSearch").click(function () {
                _self.getBrandList(1, {});
            });
            $("#inputSearch").keypress(function (event) {
                if (event.keyCode == 13)
                    _self.getBrandList(1, {});

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
                url: '/RegionData/SearchRegionData',
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
        },
        /**
         * 生成模版时绑定删除，失去焦点事件
         * @param  {[数据项文本]} item
         */
        bindItemEvent: function (item) {
            var _self = this;

            item.find('button.delete_item').click(function (event) {
                _self.deleteRegion(this);
                event.stopPropagation();
                return false;
            });
            item.find('button.edit_item').click(function (event) {
                editregion.init({
                    stepOne: true,
                    dialogID: 'wizard',
                    header: '编辑品牌',
                    Id: this.id,
                    callBack: function (orderID) {
                        if (orderID && orderID != '') {
                            //重新加载页面
                            window.location.href = '/RegionData/Index';
                        } else {
                            alert('编辑失败');
                        }
                    }
                });
                event.stopPropagation();
                return false;
            });
            item.find('button.building_item').click(function (event) {
                genderbuilding.init({
                    stepOne: true,
                    dialogID: 'wizard',
                    header: '生成栋',
                    Id: this.id,
                    callBack: function (orderID) {
                        if (orderID && orderID != '') {
                            //重新加载页面
                            window.location.href = '/RegionData/Index';
                        } else {
                            alert('生成栋失败');
                        }
                    }
                });
                event.stopPropagation();
                return false;
            });
            item.find('button.dorm_item').click(function (event) {
                genderdorm.init({
                    stepOne: true,
                    dialogID: 'wizard',
                    header: '生成宿舍',
                    Id: this.id,
                    callBack: function (orderID) {
                        if (orderID && orderID != '') {
                            //重新加载页面
                            window.location.href = '/RegionData/Index';
                        } else {
                            alert('生成宿舍失败');
                        }
                    }
                });
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
        deleteRegion: function (ele) {
            ele = $(ele);
            if (!ele.hasClass('delete_item')) return;
            var res = confirm('确定要删除这个区域吗?');
            if (res) {

                var input = ele.parent('td'),
                    id = input.attr('data-item_id'),
                    item = input.parent('tr');

                Global.post('DelRegionData', {
                    dataId: id
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
         * Ajax 请求获取角色列表,并调用模版进行UI显示
         */
        getDataSource: function (index) {
            var _self = this;
            $.ajax({
                url: 'GetRegionData',
                type: 'GET',
                cache: false,
                data: {
                    dataType: 1,
                    pageIndex: index,
                    pageSize:10
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
        //绑定公司信息
        bindCompanyInfo: function (accountId, name) {
            $("#dataId").val(accountId);
        },
        getBrandList: function (index) {
            var _self = this;
            $.ajax({
                url: 'GetChildRegionData',
                type: 'GET',
                cache: false,
                data: {
                    parentId: $("#dataId").val(),
                    pageIndex: index,
                    pageSize: 20
                },
                success: function (msg) {
                    _self.brandArea.find(".contenttr").remove();
                    if (msg.items && msg.items.length > 0) {
                        _self.dataBinding(msg.items);
                        _self.pager.paginate({
                            total_count: msg.data.Total,
                            count: msg.data.Pages,
                            start: msg.data.Index,
                            display: 10,
                            onChange: function (page) {
                                _self.getBrandList(page);
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
    exports.RegionController = RegionController;
});