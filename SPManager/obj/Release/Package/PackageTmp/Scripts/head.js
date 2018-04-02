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
     * 角色设置模块
     */
    var HeadController = $.extend({
        /**
         * 模块入口
         */
        init: function (para) {
            var _self = this;

            $.extend(this, para);

            //获取数据
            _self.bindEvent();
        },
        bindEvent: function () {
            var _self = this;
            $(".header-nav-list").find(".fl").click(function () {
                $(".btn_Aheader-nav-list").find(".fl").removeClass("active");
                $(this).addClass("active");
            });
        }
    }, base);

    exports.jQuery = $;
    exports.HeadController = HeadController;
});