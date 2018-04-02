/*添加引用
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
    require('brand/style.css');
    //默认参数
    var Defaults = {
        dialogID: "addBrand",//弹出框ID
        orderID: ''
    };

    var AddBrand = function (options) {
        var _this = this;
        _this.setting = $.extend({}, Defaults, options);
        _this.init(function () {
            _this.showDialog();
        });
        return this;
    }
    AddBrand.prototype = {
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
            doT.exec('brand/addbrand.html', function (templateFun) {
                var innerText;
                innerText = templateFun(elementID);
                easydialog.open({
                    container: {
                        id: "addBrand",
                        header: '添加供货商家',
                        content: innerText
                    }
                });
                $('#wizard').css('height', $('#addBrand').find('.page').height());

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

            //_this.bindEvent();
        },
        Submit: function (elementID) {
            var _this = this;
            var divElement = $('#' + elementID + '');
            var type = {
                brandName: divElement.find('.txtName').val(),
                displaySequence: divElement.find('.sltContact').val()

            };
            type = $.param(type, true);
            Global.post("/Brand/AddBrand", type, function (data) {
                _this.setting.callBack(data);
            });
        },

    };

    //对外公布方法
    exports.init = function (options) {
        return new AddBrand(options);
    }
    return exports
});