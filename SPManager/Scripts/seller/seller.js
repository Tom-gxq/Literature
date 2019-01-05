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
    var addadmin = require('add-seller');
    var editadmin = require('edit-seller');
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
     * 角色设置模块
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
            _self.getDataSource(1);
            _self.bindEvent();
        },
        //元素
        elements: {
            '#J_ItemList': 'SellerArea',
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
            doT.exec('seller/seller-item.html', function (templateFun) {
                items = templateFun(data);
                items = $(items);  
                _self.bindItemEvent(items);
                _self.adminArea.append(items);
            });
        },
        bindEvent: function () {
            var _self = this;
            $('#btnAddSeller').bind('click', function () {
                //弹出添加的操作框
                addadmin.init({
                    stepOne: true,
                    dialogID: 'wizard',
                    header: '添加人员',
                    callBack: function (result) {
                        if (result) {
                            //重新加载页面
                            window.location.href = '/Admin/Index';
                        } else {
                            alert('添加人员失败');
                        }
                    }
                });
            });

            $("#btnSearch").click(function () {
                _self.getAdiminList(1, {});
            });
            $("#inputSearch").keypress(function (event) {
                if (event.keyCode == 13)
                    _self.getAdiminList(1, {});

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
                editadmin.init({
                    stepOne: true,
                    dialogID: 'wizard',
                    header: '编辑人员',
                    Id:this.id,
                    callBack: function (orderID) {
                        if (orderID && orderID != '') {
                            //重新加载页面
                            window.location.href = '/Admin/Index';
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
            var res = confirm('确定要删除这个人员吗?');
            if (res) {

                var input = ele.parent('td'),
                    id = input.attr('data-item_id'),
                    item = input.parent('tr');

                Global.post('DelAdmin', {
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
                url: 'GetAdminList',
                type: 'GET',
                cache: false,
                data: {
                    pageIndex: index,
                    pageSize:20
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
        getAdiminList: function (index) {
            var _self = this;
            $.ajax({
                url: 'SearchAdminByUserName',
                type: 'GET',
                cache: false,
                data: {
                    userName: $("#inputSearch").val(),
                    pageIndex: index,
                    pageSize: 20
                },
                success: function (msg) {
                    _self.adminArea.find(".contenttr").remove();
                    if (msg.result && msg.result.length > 0) {                        
                        _self.dataBinding(msg.result);
                        _self.pager.paginate({
                            total_count: msg.data.Total,
                            count: msg.data.Pages,
                            start: msg.data.Index,
                            display: 10,
                            onChange: function (page) {
                                _self.getAdiminList(page);
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
    exports.SellerController = SellerController;
});