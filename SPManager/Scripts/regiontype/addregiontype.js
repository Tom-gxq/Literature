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
    require('regiontype/style.css');
    //默认参数
    var Defaults = {
        dialogID: "addRegionType",//弹出框ID
        orderID: ''
    };
    var setting = {};
    var AddRegionType = function (options) {
        var _this = this;
        setting = $.extend({}, Defaults, options);
        _this.setting = $.extend({}, Defaults, options);
        _this.init(function () {
            _this.showDialog();
        });
        return this;
    }
    AddRegionType.prototype = {
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
            doT.exec('regiontype/addregiontype.html', function (templateFun) {
                var innerText;
                innerText = templateFun(elementID);
                easydialog.open({
                    container: {
                        id: "addRegionType",
                        header: '添加区域种类',
                        content: innerText
                    }
                });
                $('#wizard').css('height', $('#addRegionType').find('.page').height());

                $(".page").show();
                //取消
                $("#cacel").click(function () {
                    easydialog.close();
                });

                //提交
                $("#sub").click(function () {
                    _this.Submit(elementID);
                });

                var regionInfo = require('complete');
                regionInfo.createComplete({
                    element: "#parent",
                    url: '/RegionData/SearchRegionData',
                    selectCallback: function (value, text) {
                        _this.bindRegionInfo(value, text);
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

                var typeInfo = require('complete');
                typeInfo.createComplete({
                    element: "#producttype",
                    url: '/ProductType/SearchProductTypeByKeyword',
                    selectCallback: function (value, text) {
                        _this.bindTypeInfo(value, text);
                    },
                    asyncCallback: function (response, data) {

                        response($.map(data.items, function (item) {
                            return {
                                label: item.TypeName,
                                valueKey: item.TypeId
                            }
                        }));
                    },
                    data: function (request) {
                        $(".clipLoader2").show();
                        return { keyword: request.term };
                    }
                });

            });

            //_this.bindEvent();
        },
        Submit: function (elementID) {
            var _this = this;
            var divElement = $('#' + elementID + '');
            var type = {
                RegionId: divElement.find('#regionId').val(),
                TypeId: divElement.find('#typeId').val(),
                DisplaySequence: divElement.find('#display').val(),

            };
            type = $.param(type, true);
            Global.post("/RegionData/AddRegionType", type, function (data) {
                window.location.href = '/RegionData/RegionTypeIndex';
            });
        },
        //绑定公司信息
        bindRegionInfo: function (dataId, name) {
            $("#regionId").val(dataId);
        },
        bindTypeInfo: function (dataId, name) {
            $("#typeId").val(dataId);
        },

    };

    //对外公布方法
    exports.init = function (options) {
        return new AddRegionType(options);
    }
    return exports
});