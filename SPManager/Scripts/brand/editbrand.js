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
        dialogID: "editbrand",//弹出框ID
        orderID: ''
    };

    var EditAdmin = function (options) {
        var _this = this;
        _this.setting = $.extend({}, Defaults, options);
        _this.init(function () {
            _this.showDialog();
        });
        return this;
    }
    EditAdmin.prototype = {
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
                url: 'GetBrandDetail',
                type: 'GET',
                cache: false,
                data: {
                    id: _this.setting.Id
                },
                success: function (msg) {
                    if (msg.result ) {
                        doT.exec('brand/editbrand.html', function (templateFun) {
                            var innerText;
                            innerText = templateFun(msg.result);
                            easydialog.open({
                                container: {
                                    id: "editbrand",
                                    header: '编辑供货商家',
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
                BrandId: divElement.find('.txtId').val(),
                BrandName: divElement.find('.txtName').val(),
                DisplaySequence: divElement.find('.sltContact').val()

            };
            user = $.param(user, true);
            Global.post("/Brand/EditBrand", user, function (data) {
                _this.setting.callBack(data);
            });
        },
        
    };
    
    //对外公布方法
    exports.init = function (options) {
        return new EditAdmin(options);
    }
    return exports
});