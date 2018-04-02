﻿/*添加引用
var addContact = require('addContact');

初始化插件并返回对象实例
var addOrder = addOrder.init({
    dialogID: 弹出框ID
        orderID: 新增成功后返回订单ID
});
*/

define(function (require, exports, module) {
    var $ = require('jquery'),
        doT = require('dot'),
        Global = require('global').Global,
        easydialog = require("easydialog");
    require('attribute/style.css');
    //默认参数
    var Defaults = {
        dialogID: "editattribute",//弹出框ID
        orderID: ''
    };

    var EditType = function (options) {
        var _this = this;
        _this.setting = $.extend({}, Defaults, options);
        _this.init(function () {
            _this.showDialog();
        });
        return this;
    }
    EditType.prototype = {
        init: function (callback) {
            var _this = this;
            callback();
        },
        /*
        *刚调用方法，弹出框
        */
        showDialog: function () {
            var _this = this;
            var elementID = _this.setting.dialogID;
            $.ajax({
                url: 'GetAttributeDetail',
                type: 'GET',
                cache: false,
                data: {
                    id: _this.setting.Id
                },
                success: function (msg) {
                    if (msg.result ) {
                        doT.exec('attribute/editattribute.html', function (templateFun) {
                            var innerText;
                            innerText = templateFun(msg.result);
                            easydialog.open({
                                container: {
                                    id: "editattribute",
                                    header: '编辑分类',
                                    content: innerText
                                }
                            });
                            $('#wizard').css('height', $('#' + msg.result.Id).find('.page').height());

                            $(".page").show();
                            //取消
                            $("#cacel").click(function () {
                                easydialog.close();
                            });

                            //提交
                            $("#sub").click(function () {
                                _this.Submit(elementID);
                            });

                        });
                    }
                },
                error: function (err) {

                }
            });
            

            //_this.bindEvent();
        },
        Submit: function (elementID) {
            var _this = this;
            var divElement = $('#' + elementID + '');
            var user = {
                Id: divElement.find('.txtId').val(),
                AttributeName: divElement.find('.txtName').val(),
                DisplaySequence: divElement.find('.sltContact').val()

            };
            user = $.param(user, true);
            Global.post("/Attribute/EditAttribute", user, function (data) {
                _this.setting.callBack(data);
            });
        },
        
    };
    
    //对外公布方法
    exports.init = function (options) {
        return new EditType(options);
    }
    return exports
});