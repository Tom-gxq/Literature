//公用jsconfig

//CSS
seajs.config({
    alias: {
        'basic-css': "css/basic.css",
        'layoutbase-css': "css/layoutbase.css"
    }
});
var version = '1.0';
seajs.config({
    base: "/js/",
    paths: {
        "echarts": 'plug/echarts/',
        "zrender": 'plug/zrender/'
    },
    alias: {
        'global': 'global.js',
        'jquery': 'plug/jquery/jquery-1.11.1.js',
        'jqueryui': 'plug/jquery/jquery-ui-1.10.2.js',
        'multiselect': 'plug/multiselect/jquery.multiselect.js',
        'json': 'plug/json.js',
        // 模版
        'dot': 'plug/dot.js',
        // 对话框
        'easydialog': 'plug/easydialog/easydialog.js',
        // 颜色选择
        'colorselecter': 'plug/color-selecter.js',
        // 省份-城市下拉框
        'district': 'plug/district-dropdown.js',
        // 文本框自动加载下拉框数据
        'complete': 'plug/autocomplete.js',
        // 分页
        'datapager': 'plug/datapager/jquery.paginate.js',
        
        // 文件上传
        'plupload': 'plug/plupload/plupload.full.min.js',
        // 文件上传
        'i18n': 'plug/plupload/i18n/zh_CN.js',
        // 七牛上传
        'qiniu': 'plug/qiniu/qiniu.js',
        // 七牛上传
        'qiniumain': 'plug/qiniu/main.js',
        // 七牛上传
        'qiniusellerlogo': 'plug/qiniu/sellerlogo.js',
        // 七牛上传
        'qiniusellerlic': 'plug/qiniu/sellerlic.js',
        // 七牛上传
        'qiniusellerpermit': 'plug/qiniu/sellerpermit.js',
        // 七牛上传
        'qiniusellerauth': 'plug/qiniu/sellerauth.js',
        // 表情插件
        'facebase': 'plug/face/facebase.js',
        // 表情插件
        'smohanface': 'plug/smohanface/smohan.face.js',
        // 选择网络插件
        'selectProject': 'plug/selectProject/select-project.js',
        //用户选择层
        'userdialog': 'plug/jquery.userdialog.js',
        // 表单验证插件
        'happy': 'plug/form/happy.js',
        // 单个修改
        'singleedit': 'plug/singleedit/singleedit.js',
        // 添加商家
        'add-seller': 'seller/addseller.js',
        // 添加商家营业执照
        'add-lic': 'seller/addlic.js',
        // 添加商家营业许可
        'add-permit': 'seller/addpermit.js',
        // 添加商家授权函
        'add-auth': 'seller/addauth.js',
        // 添加商家产品
        'add-product': 'sellerdetail/addproduct.js',
        //zrender，图标插件echarts的底层组件
        'zrender': 'plug/zrender/zrender.js'
    },
    map: [
        ['.js', '.js?v=' + version],
    ],
});

seajs.config({
    alias: {
        // 全局配置文件
        'WebConfig': 'scripts/web-config.js',
        // 
        'sellrecord': 'scripts/sellrecord/sellrecord.js',
        // 
        'logging': 'scripts/sellrecord/logging.js',
        //
        'radio': 'scripts/sellrecord/radio.js'
    }
});
