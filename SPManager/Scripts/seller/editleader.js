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
        dialogID: "editleader"//弹出框ID
    };

    var EditRegion = function (options) {
        var _this = this;
        _this.setting = $.extend({}, Defaults, options);
        _this.init(function () {
            _this.showDialog();
        });
        return this;
    }
    EditRegion.prototype = {
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
                url: 'GetLeaderDetail',
                type: 'GET',
                cache: false,
                data: {
                    regionId: _this.setting.Id.split('&')[0],
                    accountId: _this.setting.Id.split('&')[1]
                },
                success: function (msg) {
                    doT.exec('seller/editleader.html', function (templateFun) {
                        var innerText;
                        innerText = templateFun(msg);
                        easydialog.open({
                            container: {
                                id: "editleader",
                                header: '编辑所见区域',
                                content: innerText
                            }
                        });
                        //$('#wizard').css('height', $('#' + msg.result.Id).find('.page').height());

                        $(".page").show();
                        //取消
                        $("#cacel").click(function () {
                            easydialog.close();
                        });

                        //提交
                        $("#sub").click(function () {
                            _this.Submit(elementID);
                            easydialog.close();
                        });

                    });
                },
                error: function (err) {

                }
            });
            

            //_this.bindEvent();
        },
        Submit: function (elementID) {
            var _this = this;
            var divElement = $('#' + elementID + '');
            var leader = {
                oldRegionId: _this.setting.Id.split('&')[0],
                oldAccountId: _this.setting.Id.split('&')[1],
                regionId: $('#region').val(),
                accountId: $('#leader').val()
            };
            leader = $.param(leader, true);
            Global.post("/Seller/EditLeader", leader, function (data) {
                _this.setting.callBack(data);
            });
        },
        
    };
    
    //对外公布方法
    exports.init = function (options) {
        return new EditRegion(options);
    };

    return exports;
});