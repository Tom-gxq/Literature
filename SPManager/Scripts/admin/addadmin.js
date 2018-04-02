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
    require('admin/style.css');
    //默认参数
    var Defaults = {
        dialogID: "addAdmin",//弹出框ID
        orderID: ''
    };

    var AddAdmin = function (options) {
        var _this = this;
        _this.setting = $.extend({}, Defaults, options);
        _this.init(function () {
            _this.showDialog();
        });
        return this;
    }
    AddAdmin.prototype = {
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
            doT.exec('admin/addadmin.html', function (templateFun) {
                var innerText;
                innerText = templateFun(elementID);
                easydialog.open({
                    container: {
                        id: "addAdmin",
                        header: '添加人员',
                        content: innerText
                    }                   
                });
                $('#wizard').css('height', $('#addAdmin').find('.page').height());
                
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
            var user = {
                userName: divElement.find('.txtName').val(),
                passWord: divElement.find('.sltContact').val()

            };
            user = $.param(user, true);
            Global.post("/Admin/AddAdmin", user, function (data) {
                _this.setting.callBack(data);
            });
        },
        
    };
    
    //对外公布方法
    exports.init = function (options) {
        return new AddAdmin(options);
    }
    return exports
});