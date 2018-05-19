using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

public class ApiEnum
{
    public const string nodeName = "名称不能包含以下字符：\\ / : * ? \" < > | ";

    public enum CallApi
    {
        Null = 0,
        Post = 1,
        Task = 2,
        Calendar = 3,
        User,
        Group,
        Message,
        Passport,//账户信息
        Company,
        App,//扩展应用
        Search,//搜索
        Sensitive//敏感信息
    }

    /// <summary>
    /// 仅动态墙用 返回删除权限
    /// </summary>

    public enum PostUserRole
    {
        Admin = 3,
        CreateAccount = 2,
        Normal = 1
    }


    public enum ErrorCode
    {

        Fail = 0,
        Success = 1,
        Mobile = 3,
        MobileCode,
        ExsitAccount,

        NotFound = 404,
        MethodNotAllowed = 405,

        //通用1000开始
        ComLoseParam = 10001,
        ComBadParam = 10002,
        ComNeedAuth,
        ////[Description("需要高级网络模式")]
        ComNeedPay,
        ////[Description("数据操作无权限")]
        ComLoseAuth,
        ////[Description("数据已存在")]
        ComExist,
        //[Description("数据不存在或已经删除")]
        ComNoExist = 10007,

        //[Description("密码错误")]
        BadPassword = 10104,
        //[Description("由于您的帐号尝试多次登录失败，已被锁定，请20分钟后再试")]
        MoreLogin = 10107,
        //[Description("code已经失效")]
        CodeLose = 10108,
        //[Description("您的账号存在异常，无法正常登录使用，请联系客服")]
        BadAccount = 10109,
        //[Description("账号或密码错误")]
        BadPasswordOrAccount = 10110,

        //[Description("扩展应用未安装")]
        NotInstall = 10401,
        //[Description("扩展应用没权限直接通过用户账号获取令牌")]
        NotAppTokenAuth = 10402,

        //[Description("七牛请求参数存在错误")]
        QiniuParamError = 10403,

        //post2000
        //[Description("动态无权限或已删除")]
        PostDetailLoseAuth = 20001,
        //[Description("回复动态无权限")]
        PostReplyLoseAuth = 20002,
        //[Description("在该群组下发布动态无权限")]
        PostGroupLoseAuth = 20003,
        //[Description("操作动态无权限")]
        PostOperationLoseAuth = 20004,
        //[Description("仅限网络管理员操作")]
        PostOnlyProjectAdmin = 20005,
        //[Description("动态不存在")]
        PostNotExist = 20006,
        //[Description("网络已过期，发布动态失败")]
        PostSelectProjectOverdue = 20007,
        //[Description("评论无权限或已删除")]
        PostCommentLoseAuth = 20008,

        //task3000
        //[Description("您无权限修改或操作此任务")]
        TaskDetailLoseAuth = 30001,
        //[Description("您无权限修改或操作此项目")]
        FolderDetailLoseAuth = 30002,
        //[Description("任务不存在")]
        TaskNotExists = 30003,
        //[Description("任务评论不存在或已删除")]
        TaskTopicNotExists = 30004,
        //[Description("项目评论不存在或已删除")]
        FolderCommentNotExists = 30005,
        //[Description("操作无效，子任务日期超出母任务日期或其他原因")]
        InvalidTaskDeadline = 30006,
        //[Description("已申请或者申请已被拒绝")]
        InvalidApplicationForFolderOrTask = 30007,
        //[Description("至少要保留一个看板")]
        TheLastOneStage = 30008,
        //[Description("项目不存在")]
        FolderNotExists = 30015,

        //group4000
        //[Description("请求未加入群组数据")]
        GroupNotJoin = 40001,
        //[Description("操作群组无权限")]
        GroupNotAdminAuth = 40002,
        //[Description("您是该群组的唯一管理员,请指定一名管理员再退出群组")]
        GroupOnlyAdmin = 40003,
        //[Description("免费网络不能创建群组")]
        FreeProjectNotCreateGroup = 40004,
        //[Description("申请中,请耐心等待")]
        Applied = 40005,
        //[Description("免费网络不能转成群组")]
        FreeProjectNotChatToGroup = 40006,
        //[Description("成员不在群组中")]
        NotGroupMember = 40007,
        //[Description("群组已不存在")]
        GroupNotExist = 40008,

        //kc5000
        //[Description("文件名称存在")]
        ExitName = 50001,
        //[Description("文件已删除")]
        NoExitNode = 50002,
        //[Description("没有权限")]
        NotAdmin = 50003,
        //[Description("托付者没有加入共享文件夹所属网络")]
        DelegatorNotAuth = 50004,
        //[Description("该用户已不在此文件夹中")]
        UserNotExitRoot = 50005,
        //[Description("网络已过期或无权限添加，添加失败")]
        KCSelectProjectOverdue = 50006,
        //[Description("无权限查看文件或文件已删除")]
        NotAuthOrDeleteNode = 50007,
        //临时添加的，app上线只读权限功能干掉
        //[Description("app暂不支持查看只读权限的文件")]
        ReadOnlyAuthNotRead = 50008,
        //[Description("文件夹已删除")]
        NoExitDirectory = 50009,
        //[Description("暂时不支持上传链接类型的文件")]
        NotUploadLinkUrl = 50010,

        //[Description(nodeName)]
        NameIllegal = 50011,
        //calendar60000
        //[Description("日程不存在")]
        CalendarNotExists = 60001,

        //user 7000
        //[Description("该用户设置了不允许添加好友")]
        NoneOneFriend = 70001,
        //[Description("好友已存在")]
        AlreadyFriend = 70002,
        //[Description("账号已存在")]
        AlreadyAccount = 70003,

        //joinProject 80000
        //[Description("加入企业网络成功，等待管理员审批")]
        UserAudited = 80001,
        //[Description("用户已存在该企业网络，等待管理员审批")]
        UserExistAudited = 80002,

        //invite 90000
        //[Description("邀请失败")]
        InviteFailed = 90001,

        //token10101开始
        //[Description("请求令牌不存在")]
        BaseBadToken = 10101,
        //[Description("签名不合法")]
        BaseBadSign = 10102,
        //[Description("用户状态不正常")]
        BaseBadUser,
        //[Description("用户访问令牌失效")]
        BaseDisabledToken = 10105,
        //[Description("用户访问网络令牌受限")]
        ProjectLimited = 10106,




        //[Description("数据操作异常")]
        ComBad = 99999,
    }


}
