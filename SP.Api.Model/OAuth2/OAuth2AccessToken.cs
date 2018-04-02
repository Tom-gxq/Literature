using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model
{
    public class OAuth2AccessToken
    {
        public int AutoId { get; set; }

        public string AccessToken { get; set; }
        public string AccountID { get; set; }
        public string AppID { get; set; }
        public string ProjectID { get; set; }
        public DateTime? AccessTokenExpires { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpires { get; set; }
        public DateTime? CreateTime { get; set; }

        //public static OAuth2AccessToken ConvertFromSqlEntity(MD.Entity.Orm.OAuth2AccessToken ormEntity)
        //{
        //    var entity = new OAuth2AccessToken
        //    {
        //        AutoId = ormEntity.AutoId,
        //        AccessToken = ormEntity.AccessToken,
        //        AccountID = ormEntity.AccountID,
        //        AppID = ormEntity.AppID,
        //        ProjectID = ormEntity.ProjectID,
        //        AccessTokenExpires = ormEntity.AccessTokenExpires,
        //        RefreshToken = ormEntity.RefreshToken,
        //        RefreshTokenExpires = ormEntity.RefreshTokenExpires,
        //        CreateTime = ormEntity.CreateTime
        //    };
        //    return entity;
        //}

        //public static MD.Entity.Orm.OAuth2AccessToken ConvertFromSqlEntity(OAuth2AccessToken entity)
        //{
        //    var ormEntity = new MD.Entity.Orm.OAuth2AccessToken
        //    {
        //        AccessToken = entity.AccessToken,
        //        AccountID = entity.AccountID,
        //        AppID = entity.AppID,
        //        ProjectID = entity.ProjectID,
        //        AccessTokenExpires = entity.AccessTokenExpires,
        //        RefreshToken = entity.RefreshToken,
        //        RefreshTokenExpires = entity.RefreshTokenExpires,
        //        CreateTime = entity.CreateTime
        //    };
        //    return ormEntity;
        //}
    }
}
