define(function (require, exports, module) {
    var Global = {};
    /*
        根据url获取参数值
    */
    Global.getQueueItem = function (url, param) {
        var queueItem = url;
        if (param) {
            if (typeof (param) == "String") {
                queueItem = queueItem + "&" + param;
            } else {
                for (var name in param) {
                    queueItem = queueItem + "&" + name + "=";
                    queueItem = queueItem + param[name];
                }
            }
        }
        return queueItem;
    }
    
    Global.loadding = function () {
        return "<div align='center' class='pAll10'><img src='/modules/images/loadding.gif'/><div>"
    }
    /*
        执行GET请求
        async:是否是同步请求，默认为异步请求
    */
    Global.getqueue = new Array();
    Global.get = function (url, param, callback, async) {
        var queueItem = Global.getQueueItem(url, param);
        if (Global.getqueue.contains(queueItem)) return;
        else Global.getqueue.push(queueItem);
        jQuery.ajax({
            url: url,
            type: "GET",
            data: param,
            async: (!async),
            cache: false,
            dataType: "JSON",
            success: function (data) {
                Global.getqueue.remove(queueItem);
                if (data.error) {
                    switch (Number(data.error)) {
                        case 10000://没有登录
                            location.href = "/";
                            break;
                        case 10001:
                            //alert("没有权限操作");
                            break;

                    }

                } else {
                    (!!callback) && callback(data);
                }
            }
        });
    }

    /*
        执行POST请求
        async:是否是同步请求，默认为异步请求
    */
    Global.postqueue = new Array();
    Global.post = function (url, param, callback, async) {
        var queueItem = Global.getQueueItem(url, param);
        if (Global.postqueue.contains(queueItem)) return;
        else Global.postqueue.push(queueItem);
        jQuery.ajax({
            url: url,
            type: "POST",
            data: param,
            async: (!async),
            cache: false,
            dataType: "JSON",
            success: function (data) {
                Global.postqueue.remove(queueItem);
                if (data.error) {
                    switch (Number(data.error)) {
                        case 10000://没有登录
                            location.href = "/";
                            break;
                        case 10001:
                            //alert("没有权限操作");
                            break;

                    }
                } else {
                    (!!callback) && callback(data);
                }
            }
        });
    }

    /*
        获取#后参数
    */
    Global.getParam = function () {
        var param = '', url = location.href.split('/')[location.href.split('/').length - 1];
        if (url.indexOf('#') != -1) {
            param = location.href.split('#')[1];
            if (param.indexOf('#') != -1) {
                param = param.split('#')[0];
            }
        }
        return unescape(param);
    }
    /*
        获取url后参数
    */
    Global.getUrlParam = function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return null;
    }

    Global.setHashUrl = function (param) {
        location.hash = escape(param);
    }
    exports.Global = Global;

    /*
        重写alert
    */
    window.alert = function (msg) {
        require("jquery");
        $("#alert").remove();
        var left = 0, top = 250, alertwidth = 0,
            alert = $("<div style='z-index:99999'/>").attr("id", "alert").addClass("alert Hidden"),
            wrap = $("<div/>").addClass("alertwrap"),
            close = $("<div/>").text("×").addClass("close");
        alert.appendTo($("body"));
        wrap.append(close);
        wrap.append(msg);
        alert.append(wrap);

        left = $(window).width() / 2 - (alert.width() / 2);
        top = $(window).scrollTop() + top;
        alert.show();
        alert.offset({ left: left });

        close.click(function () { alert.remove() });
        setTimeout(function () { alert.remove(); }, 2000);
    }

    /*
        封装StringBuilder
    */
    function StringBuilder() {
        this._string_ = new Array();
    }
    StringBuilder.prototype.append = function (str) {
        this._string_.push(str);
    }
    StringBuilder.prototype.toString = function () {
        return this._string_.join("");
    }
    StringBuilder.prototype.appendFormat = function () {
        if (arguments.length > 1) {
            var TString = arguments[0];
            if (arguments[1] instanceof Array) {
                for (var i = 0, iLen = arguments[1].length; i < iLen; i++) {
                    var jIndex = i;
                    var re = eval("/\\{" + jIndex + "\\}/g;");
                    TString = TString.replace(re, arguments[1][i]);
                }
            } else {
                for (var i = 1, iLen = arguments.length; i < iLen; i++) {
                    var jIndex = i - 1;
                    var re = eval("/\\{" + jIndex + "\\}/g;");
                    TString = TString.replace(re, arguments[i]);
                }
            }
            this.Append(TString);
        } else if (arguments.length == 1) {
            this.Append(arguments[0]);
        }
    };

    exports.StringBuilder = StringBuilder;

    /*
        trim去掉字符串两边的指定字符，默认去空格
    */
    String.prototype.trim = function (str) {
        if (!str) {
            str = '\\s';
        } else {
            if (str == '\\') {
                str = '\\\\';
            } else if (str == ',' || str == '|' || str == ';' || str == '-') {
                str = '\\' + str;
            } else {
                str = '\\s';
            }
        }
        eval('var reg=/(^' + str + '+)|(' + str + '+$)/g;');
        return this.replace(reg, '');
    };

    /*
        判断一个字符串是否为NULL或者空字符串
    */
    String.prototype.isNull = function () {
        return this == null || this.trim().length == 0;
    }
    /*
        以下两个为数组方法提供
    */
    String.prototype.equals = function (str) {
        return this == str;
    }
    String.prototype.contains = function (str) {
        if (str) return this.indexOf(str) != -1;
        else return false;
    }

    /* 
        获得一个字符串的字节数
    */
    String.prototype.bytes = function () {
        var strLength = 0;
        for (var i = 0; i < this.length; i++) {
            if (this.charAt(i) > '~') strLength += 2;
            else strLength += 1;
        }
        return strLength;
    }

    /*
        根据指定的字节数截取字符串,
        after：截取字符串后面的字符，没有则不传
    */
    String.prototype.cut = function (len, after) {
        if (!len) {
            len = this.bytes();
        }
        var strLength = 0;
        var cutStr = "";
        if (len > this.bytes()) {
            cutStr = this;
        } else {
            for (var i = 0; i < this.length; i++) {
                if (this.charAt(i) > '~') {
                    strLength += 2;
                } else {
                    strLength += 1;
                } if (strLength >= len) {
                    cutStr = this.substring(0, i + 1);
                    break;
                }
            }
            if (after) {
                cutStr += after;
            }
        }
        return cutStr;
    };

    /*
        将一个字符串用给定的字符变成数组
    */
    String.prototype.toArray = function (str) {
        if (this.indexOf(str) != -1) {
            return this.split(str);
        } else {
            if (this != '') {
                return [this.toString()];
            } else {
                return [];
            }
        }
    };
    String.prototype.replaceAll = function (s1, s2) {
        return this.replace(new RegExp(s1, "gm"), s2);
    }

    /*
        根据数据取得再数组中的索引
    */
    Array.prototype.getIndex = function (obj) {
        for (var i = 0, len = this.length; i < len; i++) {
            if (obj == this[i] || obj.equals(this[i])) {
                return i;
            }
        }
        return -1;
    }

    /*
        移除数组中的某元素
    */
    Array.prototype.remove = function (obj) {
        for (var i = 0, len = this.length; i < len; i++) {
            if (obj.equals(this[i])) {
                this.splice(i, 1);
                break;
            }
        }
        return this;
    };

    /*
        判断元素是否在数组中
    */
    Array.prototype.contains = function (obj) {
        for (var i = 0, len = this.length; i < len; i++) {
            if (obj == this[i] || obj.equals(this[i])) {
                return true;
            }
        }
        return false;
    };

    /*
        从数组里查抄匹配到的所有元素
    */
    Array.prototype.findAll = function (fn) {
        var arr = [];
        for (var i = 0, len = this.length; i < len; i++) {
            var o = this[i];
            if (fn.call(o)) {
                arr.push(o);
            }
        }
        return arr;
    };
    /*
        从数组里查找匹配到的某个元素
    */
    Array.prototype.find = function (fn) {
        var obj;
        for (var i = 0, len = this.length; i < len; i++) {
            var o = this[i];
            if (fn.call(o)) {
                obj = o;
                break;
            }
        }
        return obj;
    };

    var File = typeof (File) == 'undefined' ? {} : File;

    /*
        获取后缀名
    */
    File.GetExt = function (fileName) {
        var t = fileName.split(".");
        return t[t.length - 1];
    }
    /*
        判断后缀是否是图片
    */
    File.isPicture = function (fileExt) {
        var fileExts = [".jpg", ".gif", ".png", ".jpeg", ".bmp"];
        if (fileExt) {
            fileExt = fileExt.toLowerCase();
            return fileExts.contains(fileExt);
        }
        return false;
    }
    /*
        判断后缀是否是文档
    */
    File.isDocument = function (fileExt) {
        var fileExts = [".doc", ".docx", ".ppt", ".pot", ".pps", ".pptx", ".xls", ".xlsx", ".pdf", ".txt"];
        if (fileExt) {
            fileExt = fileExt.toLowerCase();
            return fileExts.contains(fileExt);
        }
        return false;
    }
    /*
        判断后缀是否是压缩文件
    */
    File.isCompress = function (fileExt) {
        var fileExts = [".zip", ".rar", ".7z", ".mm", ".vsd"];
        if (fileExt) {
            fileExt = fileExt.toLowerCase();
            return fileExts.contains(fileExt);
        }
        return false;
    }

    exports.File = File;

    //对连接进行操作
    var Link = {};

    /*
        把带有连接的字符串，把连接部分加上连接
    */
    Link.Add = function (str) {
        var urlReg = /http(s)?:\/\/([\w-]+\.)+[\w-]+(\/[\w- .\/?%&=])?[^ <>\[\]*(){}\u4E00-\u9FA5]+/gi; //lio 2012-4-25 eidt   //         /^[\u4e00-\u9fa5\w]+$/;\u4E00-\u9FA5
        return str.replace(urlReg, function (m) {
            return '<a target="_blank" href="' + m + '">' + m + '</a>';
        });
    }
    exports.Link = Link;
    /*
        验证一个字符串是否包含特殊字符
    */
    RegExp.isContainSpecial = function (str) {
        var containSpecial = RegExp(/[(\,)(\\)(\/)(\:)(\*)(\')(\?)(\\\)(\<)(\>)(\|)]+/);
        return (containSpecial.test(str));
    }

    /*
        验证一个字符串时候是email
    */
    RegExp.isEmail = function (str) {
        var emailReg = /^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)*\.[\w-]+$/i;
        return emailReg.test(str);
    }

    /*
        验证一个字符串是否是URL
    */
    RegExp.isUrl = function (str) {
        var patrn = /^http(s)?:\/\/[A-Za-z0-9\-]+\.[A-Za-z0-9\-]+[\/=\?%\-&_~`@[\]\:+!]*([^<>])*$/;
        return patrn.exec(str);
    }

    /*
        验证一个字符串是否是电话或传真
    */
    RegExp.isTel = function (str) {
        var pattern = /^[+]?((\d){3,4}([ ]|[-]))?((\d){7,8})(([ ]|[-])(\d){1,12})?$/;
        return pattern.exec(str);
    }

    /*
        验证一个字符串是否是手机号码
    */
    RegExp.isMobile = function (str) {
        var patrn = /^(1[3-8]{1})\d{9}$/;
        return patrn.exec(str);

    }

    /*
        验证一个字符串是否为外国手机号码
    */
    RegExp.isElseMobile = function (str) {
        var patrn = /^\d{5}\d*$/;
        return patrn.exec(str);
    }

    /*
        验证一个字符串是否是传真号
    */
    RegExp.isFax = function (str) {
        var patrn = /^[+]{0,1}(\d){1,3}[ ]?([-]?((\d)|[ ]){1,12})+$/;
        return patrn.exec(str);

    }

    /*
        验证一个字符串是否是汉字
    */
    RegExp.isZHCN = function (str) {
        var p = /^[\u4e00-\u9fa5\w]+$/;
        return p.exec(str);
    }

    /*
        验证一个字符串是否是数字
    */
    RegExp.isNum = function (str) {
        var p = /^\d+$/;
        return p.exec(str);
    }

    /*
        验证一个字符串是否是纯英文
    */
    RegExp.isEnglish = function (str) {
        var p = /^[a-zA-Z., ]+$/;
        return p.exec(str);
    }

    /*
        判断是否为对象类型
    */
    RegExp.isObject = function (obj) {
        return (typeof obj == 'object') && obj.constructor == Object;
    }

    /*
        日期格式化
    */
    Date.prototype.format = function (format) {
        var o = {
            "M+": this.getMonth() + 1,
            "d+": this.getDate(),
            "h+": this.getHours(),
            "m+": this.getMinutes(),
            "s+": this.getSeconds(),
            "q+": Math.floor((this.getMonth() + 3) / 3),
            "S": this.getMilliseconds()
        }

        if (/(y+)/.test(format)) {
            format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        }

        for (var k in o) {
            if (new RegExp("(" + k + ")").test(format)) {
                format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
            }
        }
        return format;
    };
    /*
        JSON返回的日期类型转换为字符串
    */
    String.prototype.toDate = function (format) {
        var d = new Date();
        d.setTime(this.match(/-?\d+/)[0]);
        if (d.getYear() < 0)
            return "--";
        return (!!format) ? d.format(format) : d;
    };

    /*
        当前日期加N天
    */
    Date.prototype.addSomeDay = function (n) {
        var uom = new Date(this - 0 + n * 86400000);
        uom = (uom.getMonth() + 1) + "/" + uom.getDate() + "/" + uom.getFullYear();
        return new Date(uom);
    };
    /*
        获得某月的天数
    */
    Global.getMonthDays = function (myYear, myMonth) {
        //var monthStartDate = new Date(myYear, myMonth, 1);
        //var monthEndDate = new Date(myYear, parseInt(myMonth)+ 1, 1);
        //var days = (monthEndDate - monthStartDate) / (1000 * 60 * 60 * 24);
        //return days;
        var days = 30
        if (parseInt(myMonth) == 2)
            days = 28
        if ((",1,3,5,7,8,10,12,").indexOf("," + parseInt(myMonth) + ",") > -1)
            days = 31
        if (parseInt(myMonth) == "2" && (parseInt(myYear) % 4 == 0 && (parseInt(myYear) % 100 != 0 || (parseInt(myYear) % 400 == 0))))
            days = 29
        return days
    }
    /*
        昨天日期
    */
    Date.prototype.getLastDay = function () {
        this.setDate(this.getDate() - 1);
        return this.getFullYear() + '-' + (this.getMonth() + 1) + '-' + this.getDate();
    };

    /*
        明天日期
    */
    Date.prototype.getThisTomorrowEnd = function () {
        this.setDate(this.getDate() + 1);
        return this.getFullYear() + '-' + (this.getMinutes() + 1) + '-' + this.getDate();
    };
    /*
        上周开始日期
    */
    Date.prototype.getLastWeekStart = function () {
        var nowDay = (this.getDay() == 0 ? 7 : this.getDay());
        this.setDate(this.getDate() - nowDay - 6);
        return this.getFullYear() + "-" + (this.getMonth() + 1) + "-" + this.getDate();
    };
    /*
        上周结束日期
    */
    Date.prototype.getLastWeekEnd = function () {
        var nowDay = (this.getDay() == 0 ? 7 : this.getDay());
        this.setDate(this.getDate() - nowDay);
        return this.getFullYear() + "-" + (this.getMonth() + 1) + "-" + this.getDate();
    };
    /*
        本周结束日期
    */
    Date.prototype.getThisWeekEnd = function () {
        var nowDay = (this.getDay() == 0 ? 7 : this.getDay());
        this.setDate(this.getDate() + (7 - nowDay));
        return this.getFullYear() + "-" + (this.getMonth() + 1) + "-" + this.getDate();
    };
    /*
        本周开始日期
    */
    Date.prototype.getThisWeekStart = function () {
        var nowDay = (this.getDay() == 0 ? 7 : this.getDay());
        this.setDate(this.getDate() - (nowDay - 1));
        return this.getFullYear() + "-" + (this.getMonth() + 1) + "-" + this.getDate();
    };
    /*
        本周结束日期
    */
    Date.prototype.getOneWeekEnd = function () {
        var nowDay = (this.getDay() == 0 ? 7 : this.getDay());
        this.setDate(this.getDate() + (5 - nowDay));
        var now = this.addSomeDay(7);
        return now.getFullYear() + "-" + (now.getMonth() + 1) + "-" + now.getDate();
    };
    /*
        上月结束日期
    */
    Date.prototype.getLastMonthEnd = function () {
        this.setDate(0);
        return this.getFullYear() + "-" + (this.getMonth() + 1) + "-" + this.getDate();

    };
    /*
       上月开始日期
   */
    Date.prototype.getLastMonthStart = function () {
        this.setDate(1);
        return this.getFullYear() + "-" + (this.getMonth()) + "-" + this.getDate();
    };
    /*
        本月开始日期
    */
    Date.prototype.getThisMonthStart = function () {
        this.setDate(1);
        return this.getFullYear() + "-" + ((this.getMonth() + 1) < 10 ? "0" + (this.getMonth() + 1) : this.getMonth() + 1) + "-" + (this.getDate() < 10 ? "0" + this.getDate() : this.getDate());
    };
    /*
        本月结束日期
    */
    Date.prototype.getThisMonthEnd = function () {
        return this.getFullYear() + "-" + ((this.getMonth() + 1) < 10 ? "0" + (this.getMonth() + 1) : this.getMonth() + 1) + "-" + Global.getMonthDays(this.getFullYear(), this.getMonth() + 1);
    };
    /*
        一月后开始日期
    */
    Date.prototype.getOneMonthStart = function () {
        return this.getFullYear() + "-" + (this.getMonth() + 1) + "-" + this.getDate();
    };
    /*
        一月后结束日期
    */
    Date.prototype.getOneMonthEnd = function () {
        this.setMonth(this.getMonth() + 1);
        return this.getFullYear() + "-" + (this.getMonth() + 1) + "-" + this.getDate();
    };


    String.prototype.HtmlEncode = function () {
        var text = this;
        return text.replace(/&/g, '&amp').replace(/\"/g, '&quot;').replace(/</g, '&lt;').replace(/>/g, '&gt;');
    };

    String.prototype.HtmlDecode = function () {
        var text = this;
        return text.replace(/&amp;/g, '&').replace(/&quot;/g, '\"').replace(/&lt;/g, '<').replace(/&gt;/g, '>');
    };
    //数字转化为千位
    Number.prototype.toMoney = function () {
        var _value, _arr, _int, _decimal, _re;
        _value = this.toString();
        _arr = _value.split(".");
        _int = _arr[0], _decimal = "00";
        if (_arr.length > 1) {
            _decimal = _arr[1];
        }
        _int = _int.replace(/^(-?\d*)$/, "$1,");

        _re = /(\d)(\d{3},)/;
        while (_re.test(_int)) {
            _int = _int.replace(_re, "$1,$2");
        }
        _value = _int + _decimal;
        _value = _value.replace(/,(\d*)$/, ".$1");
        return _value;
    };
    //字符格式数字转化为千位
    String.prototype.toMoney = function () {
        var _value, _arr, _int, _decimal, _re;
        _value = this;
        _arr = _value.split(".");
        _int = _arr[0], _decimal = "00";
        if (_arr.length > 1) {
            _decimal = _arr[1];
        }
        _int = _int.replace(/^(-?\d*)$/, "$1,");

        _re = /(\d)(\d{3},)/;
        while (_re.test(_int)) {
            _int = _int.replace(_re, "$1,$2");
        }
        _value = _int + _decimal;
        _value = _value.replace(/,(\d*)$/, ".$1");
        return _value;
    };
    //截取字符串
    String.prototype.subStr = function (len) {
        if (this.length <= len) {
            return this;
        } else {
            return this.substr(0, len) + "...";
        }
    };

});