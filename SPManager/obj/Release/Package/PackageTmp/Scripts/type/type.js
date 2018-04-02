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
    var addtype = require('add-type');
    var edittype = require('edit-type');
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
    var TypeController = $.extend({
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
            '#J_ItemList': 'typeArea',
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
            doT.exec('type/list-item.html', function (templateFun) {
                items = templateFun(data);
                items = $(items);
                _self.bindItemEvent(items);
                _self.typeArea.append(items);
            });
        },
        bindEvent: function () {
            var _self = this;
            $('#btnAddButton').bind('click', function () {
                //弹出添加的操作框
                addtype.init({
                    stepOne: true,
                    dialogID: 'wizard',
                    header: '添加人员',
                    callBack: function (result) {
                        if (result) {
                            //重新加载页面
                            window.location.href = '/ProductType/Index';
                        } else {
                            alert('添加人员失败');
                        }
                    }
                });
            });

            $("#btnSearch").click(function () {
                _self.getTypeList(1, {});
            });
            $("#inputSearch").keypress(function (event) {
                if (event.keyCode == 13)
                    _self.getTypeList(1, {});

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
                    var item =$(this).parent("td").parent("tr");
                    if ($(this).prop("checked") == true) {
                        Global.post('DelProductType', {
                            id: $(this).val()
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
            item.find('button.edit_item').click(function (event) {
                edittype.init({
                    stepOne: true,
                    dialogID: 'wizard',
                    header: '编辑分类',
                    Id: this.id,
                    callBack: function (orderID) {
                        if (orderID && orderID != '') {
                            //重新加载页面
                            window.location.href = '/ProductType/Index';
                        } else {
                            alert('编辑失败');
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
        deleteUser: function (ele) {
            ele = $(ele);
            if (!ele.hasClass('delete_item')) return;
            var res = confirm('确定要删除这个分类吗?');
            if (res) {

                var input = ele.parent('td'),
                    id = input.attr('data-item_id'),
                    item = input.parent('tr');

                Global.post('DelProductType', {
                    id: id
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
                url: 'GetProductTypeList',
                type: 'GET',
                cache: false,
                data: {
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
                                _self.getDataSource(page);
                            }
                        });
                    }
                },
                error: function (err) {

                }
            });
        },
        getTypeList: function (index) {
            var _self = this;
            $.ajax({
                url: 'SearchProductTypeByName',
                type: 'GET',
                cache: false,
                data: {
                    typeName: $("#inputSearch").val(),
                    pageIndex: index,
                    pageSize: 20
                },
                success: function (msg) {
                    _self.typeArea.find(".contenttr").remove();
                    if (msg.result && msg.result.length > 0) {
                        _self.dataBinding(msg.result);
                        _self.pager.paginate({
                            total_count: msg.data.Total,
                            count: msg.data.Pages,
                            start: msg.data.Index,
                            display: 10,
                            onChange: function (page) {
                                _self.getTypeList(page);
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
    exports.TypeController = TypeController;
});