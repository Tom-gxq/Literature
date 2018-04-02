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
    require('attribute/style.css');
    //默认参数
    var Defaults = {
        dialogID: "editattributevalue",//弹出框ID
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
            _this.getDataSource();            

            //_this.bindEvent();
        },
        Submit: function (elementID) {
            var _this = this;
            var divElement = $('#' + elementID + '');
            var user = {
                AttributeId: _this.setting.Id,
                ValueStr: divElement.find('.txtName').val(),
                DisplaySequence: divElement.find('.sltContact').val()

            };
            user = $.param(user, true);
            Global.post("/AttributeValue/AddAttributeValue", user, function (data) {
                _this.getDataSource();  
            });
        },
        Delete: function (elementID) {
            var _this = this;
            $('#vallist').find("input[type=checkbox]").each(function () {
                //由于复选框一般选中的是多个,所以可以循环输出
                var item = $(this).parent("td").parent("tr");
                if ($(this).prop("checked") == true) {
                    Global.post('/AttributeValue/DeleteAttributeValue', {
                        valueId: $(this).val()
                    },
                        function (data) {
                            if (data && data.result) {
                                item.remove();
                            }
                        }
                    );
                }
            });
        },
        /**
         * Ajax 请求获取角色列表,并调用模版进行UI显示
         */
        getDataSource: function () {
            var _this = this;
            var elementID = _this.setting.dialogID;
            $.ajax({
                url: '/AttributeValue/GetAttributeValueList',
                type: 'GET',
                cache: false,
                data: {
                    attributeId: _this.setting.Id
                },
                success: function (msg) {
                    if (msg.result) {
                        doT.exec('attribute/editattributevalue.html', function (templateFun) {
                            var innerText;
                            innerText = templateFun(msg.result);
                            easydialog.open({
                                container: {
                                    id: "editattributevalue",
                                    header: '管理属性值',
                                    content: innerText
                                }
                            });
                            $('#wizard').css('height', $('#' + msg.result.Id).find('.page').height());

                            $(".page").show();
                            //删除
                            $("#del").click(function () {
                                _this.Delete(elementID);
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
        }
        
    };
    
    //对外公布方法
    exports.init = function (options) {
        return new EditType(options);
    }
    return exports
});