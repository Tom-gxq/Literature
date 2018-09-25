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
    var completeInfo = require('complete');
    require('associator/style.css');
    //默认参数
    var Defaults = {
        dialogID: "AddAssociator",//弹出框ID
        orderID: ''
    };

    var AddAssociator = function (options) {
        var _this = this;
        _this.setting = $.extend({}, Defaults, options);
        _this.init(function () {
            _this.showDialog();
        });
        return this;
    }
    AddAssociator.prototype = {
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
            doT.exec('associator/addassociator.html', function (templateFun) {
                var innerText;
                innerText = templateFun(elementID);
                easydialog.open({
                    container: {
                        id: "AddAssociator",
                        header: '添加会员',
                        content: innerText
                    }                   
                });
                $('#wizard').css('height', $('#AddAssociator').find('.page').height());
                
                $(".page").show();
                //取消
                $("#cacel").click(function () {
                    easydialog.close();
                });
                
                //提交
                $("#sub").click(function () {
                    _this.Submit(elementID);
                });

                completeInfo.createComplete({
                    element: "#account",
                    url: '/Account/SearchAccount',
                    selectCallback: function (value, text) {
                        _this.bindAccount(value, text);
                    },
                    asyncCallback: function (response, data) {
                        console.log(data);
                        response($.map(data.items, function (item) {
                            return {
                                label: item.Fullname,
                                valueKey: item.AccountId
                            }
                        }));
                    },
                    data: function (request) {
                        $(".clipLoader").show();
                        return { keywords: request.term };
                    }
                });

                _this.bindSysKindInfo();
            });

            //_this.bindEvent();
        },
        Submit: function (elementID) {
            var _this = this;
            var divElement = $('#' + elementID + '');
            var user = {
                AccountId: $("#accountid").val(),
                KindId: $("#kind").val(),
                Quantity: $("#quantity").val(),
            };
            user = $.param(user, true);
            Global.post("/Associator/AddAssociator", user, function (data) {
                _this.setting.callBack(data);
            });
        },
        //绑定公司信息
        bindAccount: function (accountId, name) {
            $("#accountid").val(accountId);
        },
        //绑定学区信息
        bindSysKindInfo: function () {
            $("#kind").empty();
            $("#kind").append($("<option/>").text("").attr("value", "0"));
            var data={
                kind: 100,
                pageIndex: 1,
                pageSize: 1000
            };
            data = $.param(data, true);
            Global.get("/Discount/GetSysKind", data, function (msg) {
                $(msg.items).each(function (index, itemData) {
                    $("#kind").append($("<option/>").text(itemData.Description).attr("value", itemData.KindId));
                });
            })
        }
        
    };
    
    //对外公布方法
    exports.init = function (options) {
        return new AddAssociator(options);
    }
    return exports
});