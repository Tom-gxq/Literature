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
    qiniu = require('qiniucarousel');
    require('carousel/style.css');
    //默认参数
    var Defaults = {
        dialogID: "addCarousel",//弹出框ID
        orderID: ''
    };
    var setting = {};
    var AddCarousel = function (options) {
        var _this = this;
        setting = $.extend({}, Defaults, options);
        _this.setting = $.extend({}, Defaults, options);
        _this.init(function () {
            _this.showDialog();
        });
        return this;
    }
    AddCarousel.prototype = {
        init: function (callback) {
            var _this = this;
            callback();
        },
        /*
        *刚调用方法，弹出框
        */
        showDialog: function (options) {
            var _this = this;            
            var elementID = _this.setting.dialogID;
            doT.exec('carousel/addcarousel.html', function (templateFun) {
                var innerText;
                innerText = templateFun(elementID);
                easydialog.open({
                    container: {
                        id: "addCarousel",
                        header: '添加事件管理',
                        content: innerText
                    }
                });
                $('#wizard').css('height', $('#addCarousel').find('.page').height());

                $(".page").show();
                //取消
                $("#cacel").click(function () {
                    easydialog.close();
                });

                //提交
                $("#sub").click(function () {
                    _this.Submit(elementID);
                });
                qiniu.QiniuMainController.init();

                var completeInfo = require('complete');
                completeInfo.createComplete({
                    element: "#parent",
                    url: '/Discount/SearchRegionData',
                    selectCallback: function (value, text) {
                        _this.bindCompanyInfo(value, text);
                    },
                    asyncCallback: function (response, data) {
                        
                        response($.map(data.items, function (item) {
                            return {
                                label: item.DataName,
                                valueKey: item.DataId
                            }
                        }));
                    },
                    data: function (request) {
                        $(".clipLoader").show();
                        return { dataName: request.term };
                    }
                });

            });

            //_this.bindEvent();
        },
        Submit: function (elementID) {
            var _this = this;
            var divElement = $('#' + elementID + '');
            var type = {
                Description: divElement.find('.txtName').val(),
                Url: divElement.find('.url').val(),
                ImagePath: divElement.find('#imgPath').val(),
                DisplaySequence: divElement.find('.sequence').val(),
            };
            type = $.param(type, true);
            Global.post("/Discount/AddCarousel", type, function (data) {
                window.location.href = '/Discount/CarouselIndex';
            });
        }

    };

    //对外公布方法
    exports.init = function (options) {
        return new AddCarousel(options);
    }
    return exports
});