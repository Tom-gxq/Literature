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
    base: "/Scripts/",
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

        'daterangepicker': 'plug/daterange/js/daterangepicker.jquery.js',
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
        'qiniucarousel': 'plug/qiniu/carouse.js',
        // 七牛上传
        'qiniushop': 'plug/qiniu/shop.js',
        // 七牛上传
        'qiniutype': 'plug/qiniu/producttype.js',
        // 七牛上传
        'qiniusellerlogo': 'plug/qiniu/sellerlogo.js',
        // 七牛上传
        'qiniusellerlicense': 'plug/qiniu/sellerlicense.js',
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
        // 添加用户
        'add-admin': 'admin/addadmin.js', 
        // 编辑用户
        'edit-admin': 'admin/editadmin.js', 
        // 添加分类
        'add-type': 'type/addtype.js',
        // 编辑分类
        'edit-type': 'type/edittype.js',
        // 添加品牌
        'add-brand': 'brand/addbrand.js',
        // 编辑品牌
        'edit-brand': 'brand/editbrand.js',
        // 添加属性
        'add-attribute': 'attribute/addattribute.js',
        // 编辑属性
        'edit-attribute': 'attribute/editattribute.js',
        // 编辑属性值
        'edit-attributevalue': 'attribute/editattributevalue.js',
        // 添加商品
        'add-product': 'product/addproduct.js',
        // 编辑商品
        'edit-product': 'product/editproduct.js',
        // 添加店铺
        'add-shop': 'shop/addshop.js',
        // 编辑店铺
        'edit-shop': 'shop/editshop.js',
        // 添加区域
        'add-region': 'regiondata/addregiondata.js',
        // 编辑区域
        'edit-region': 'regiondata/editregiondata.js',
        // 添加区域种类
        'add-regiontype': 'regiontype/addregiontype.js',
        // 生成栋
        'gender-building': 'regiondata/genderbuilding.js',
        // 生成宿舍
        'gender-dorm': 'regiondata/genderdorm.js',
        // 添加会员优惠折扣
        'add-associator': 'discount/addassociatordata.js',
        // 编辑会员优惠折扣
        'edit-associator': 'discount/editassociatordata.js',
        // 添加优惠折扣
        'add-coupons': 'discount/addcouponsdata.js',
        // 编辑会员优惠折扣
        'edit-coupons': 'discount/editcouponsdata.js',
        // 添加响应事件
        'add-resevent': 'resevent/addreseventdata.js',
        // 添加响应事件
        'add-event': 'eventsetting/addeventdata.js',
        // 添加响应事件
        'add-sku': 'productsku/addskudata.js',
        // 添加轮播图
        'add-carousel': 'carousel/addcarouseldata.js',
        // 添加会员优惠折扣
        'add-associatorItem': 'associator/addassociator.js',
        //zrender，图标插件echarts的底层组件
        'zrender': 'plug/zrender/zrender.js',
        //region
        'add-seller-region': 'seller/addregion.js',
        'edit-seller-region': 'seller/editregion.js',
        //leader
        'add-seller-leader': 'seller/addleader.js',
        'edit-seller-leader': 'seller/editleader.js',
        //seller
        'add-seller': 'seller/addseller.js',
        'edit-seller': 'seller/editseller.js',
        'edit-seller-license': 'seller/editsellerlicense.js',
        'edit-seller-permit': 'seller/editsellerpermit.js',
        'edit-seller-authorization': 'seller/editsellerauthorization.js',
        //
        'add-seller-product': 'seller/addproduct.js',
        'edit-seller-product': 'seller/editproduct.js',
        
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
