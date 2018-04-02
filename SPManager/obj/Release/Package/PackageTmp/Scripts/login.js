$(function () {
    var e = function () {
        var e = _.random(0, 1e3); $("#code-img-enti").attr("src", "/Public/verify?r=" + e)
    },
        c = function () {
            var c = $("#ipt_account"), o = $("#ipt_pwd"), r = $("#ipt_code"), i = c.val(), a = o.val(), t = r.val();
            return "" == i ? (LoginShowError(c, "请输入用户名！"), void c.select().focus()) : "" == a ? (LoginShowError(o, "请输入密码!"), void o.select().focus()) : "" == t ? (LoginShowError(r, "请输入验证码！"), void r.select().focus()) : ($.ajax({
                url: "/Public/login?v=" + Math.round(100 * Math.random()), type: "post", dataType: "json", data: { username: i, password: a, verify: t }, beforeSend: function () { $.jBox.showloading() }, success: function (t) {
                    if (1 == t.status) $.cookie("cache_account", i, { expires: 30, path: "/" }), $("#rd_remember").is(":checked") ? ($.cookie("cache_pwd", a, { expires: 30, path: "/" }), $.cookie("cache_pwd_checked", !0, { expires: 30, path: "/" })) : ($.cookie("cache_pwd", "", { expires: 30, path: "/" }), $.cookie("cache_pwd_checked", !1, { expires: 30, path: "/" })), window.location.href = t.url; else {
                        switch (t.tab) {
                            case "username": LoginShowError(c, t.msg), c.select().focus(); break;
                            case "password": LoginShowError(o, t.msg), o.select().focus(); break;
                            case "verify": LoginShowError(r, t.msg), r.select().focus(); break;
                            case "unknown": alert(t.msg)
                        }e()
                    } $.jBox.hideloading()
                }
            }), !1)
        };
    $("#ipt_account").val($.cookie("cache_account")).focus(),
        $("#ipt_pwd").val($.cookie("cache_pwd")),
        $("#rd_remember").attr("checked", "true" == $.cookie("cache_pwd_checked")),
        $(".j-codeReresh").click(e), $("#btn_login").click(c),
        $(document).on("keyup", ".acpVerify", function (e) { "13" == e.which && c() }), $(".clearError").bind("keypress click", function (e) { var o = window.event ? e.keyCode : e.which; return 13 == o ? void c() : void LoginClearError($(this)) }), _QV_ = "%E6%9D%AD%E5%B7%9E%E5%90%AF%E5%8D%9A%E7%A7%91%E6%8A%80%E6%9C%89%E9%99%90%E5%85%AC%E5%8F%B8%E7%89%88%E6%9D%83%E6%89%80%E6%9C%89"
});
