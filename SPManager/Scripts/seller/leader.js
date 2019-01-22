/**
 * sysRole.js
 * @authors Aaron (aaron.yuan@mingdao.com)
 * @date    2014-04-28 13:55:04
 * @version $Id$
 */

define(function (require, exports, module) {

    var $ = require('jquery'),
        common = require('global'),
        doT = require('dot');
    var datapager = require('datapager');
    var easydialog = require("easydialog");
    var addLeader = require('add-seller-leader');
    var editLeader = require('edit-seller-leader');
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
                        method($(this));
                    });
                }
            }
        }
    };

    /**
     * 角色设置模块
     */
    var LeaderController = $.extend({
        //**         * 模块入口         */
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
            '#J_ItemList': 'leaderArea',
            // 分页元素
            '#pager': 'pager'
        },
        /**
         * 页面数据列表绑定
         * @param  {[JSON]} data
         */
        dataBinding: function (data) {
            var _self = this;
            items = '';
            doT.exec('seller/leader-item.html', function (templateFun) {
                items = templateFun(data);
                items = $(items);
                _self.bindItemEvent(items);
                _self.leaderArea.append(items);
            });
        },
        bindEvent: function () {
            var _self = this;
            $('#btnAddButton').bind('click', function () {
                //弹出添加的操作框
                addLeader.init({
                    stepOne: true,
                    dialogID: 'wizard',
                    header: '各校区负责人配置',
                    callBack: function (result) {
                        if (result) {
                            //重新加载页面
                            window.location.href = '/Seller/Leader';
                        } else {
                            alert('添加各校区负责人失败');
                        }
                    }
                });
            });

            $("#btnSearch").click(function () {
                _self.getLeaderList(1, {});
            });
            $("#inputSearch").keypress(function (event) {
                if (event.keyCode == 13)
                    _self.getLeaderList(1, {});

            });
        },
        /**
         * 生成模版时绑定删除，失去焦点事件
         * @param  {[数据项文本]} item
         */
        bindItemEvent: function (item) {
            var _self = this;

            item.find('button.delete_item').click(function (event) {
                _self.deleteLeader(this);
                event.stopPropagation();
                return false;
            });
            item.find('button.edit_item').click(function (event) {
                editLeader.init({
                    stepOne: true,
                    dialogID: 'wizard',
                    header: '编辑各校区负责人',
                    Id: this.id,
                    callBack: function (Id) {
                        if (Id && Id != '') {
                            //重新加载页面
                            window.location.href = '/Seller/Leader';
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
         * 删除区域
         * @param  {[删除按钮]} ele
         */
        deleteLeader: function (ele) {
            ele = $(ele);
            if (!ele.hasClass('delete_item')) return;
            var res = confirm('确定要删除吗?');
            if (res) {

                var input = ele.parent('td'),
                    id = input.attr('data-item_id'),
                    item = input.parent('tr');

                Global.post('DelLeader', {
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
                url: 'GetLeaderList',
                type: 'GET',
                cache: false,
                data: {
                    pageIndex: index,
                    pageSize: 10
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
        getLeaderList: function (index) {
            var _self = this;
            $.ajax({
                url: 'SearchLeaderByName',
                type: 'GET',
                cache: false,
                data: {
                    supplierName: $("#inputSearch").val(),
                    pageIndex: index,
                    pageSize: 20
                },
                success: function (msg) {
                    _self.regionArea.find(".contenttr").remove();
                    if (msg.result && msg.result.length > 0) {
                        _self.dataBinding(msg.result);
                        _self.pager.paginate({
                            total_count: msg.data.Total,
                            count: msg.data.Pages,
                            start: msg.data.Index,
                            display: 10,
                            onChange: function (page) {
                                _self.getLeaderList(page);
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
    exports.LeaderController = LeaderController;
});