using System.Collections;
using System.Data;
using System.Text;
using BaiRong.Core;
using BaiRong.Core.Data;
using BaiRong.Core.Model.Enumerations;
using SiteServer.CMS.Model;
using BaiRong.Core.Model;

namespace SiteServer.CMS.Provider
{
   public class OranizationReachDao : DataProviderBase
    {
        public const string SqlSelectReachDetails = @"Select a.OrganizationId,b.PublishmentSystemName,a.ReachTime FROM siteserver_organizationreach a 
LEFT JOIN siteserver_publishmentsystem b on b.PublishmentSystemId=a.ReachOrganizationId
WHERE a.OrganizationId=@OrganizationId";
        public const string ParmOrganizationId = "@OrganizationId";

        public string GetSelectCommend(string type)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(type))
            {
                if (type.Equals("Personal"))
                {
                    sql = @"SELECT a.ID,b.UserName,b.Mobile,c.PublishmentSystemName as OrganizationName ,d.PublishmentSystemName as ReachOrganizationName,a.ReachTime, b.ActivitiesCount,b.Integral FROM siteserver_personalreach a
LEFT JOIN bairong_users b on b.UserId =a.UserId
LEFT JOIN siteserver_publishmentsystem  c on c.PublishmentSystemId=a.OrganizationId
LEFT JOIN siteserver_publishmentsystem d on d.PublishmentSystemId=a.ReachOrganizationId
";
                }
                else if (type.Equals("Organization"))
                {
                    sql = $@"SELECT a.ID,b.PublishmentSystemName as OrganizationName,d.AdministratorAccount,d.TelePhone,c.PublishmentSystemName as ReachOrganizationName,a.ReachTime,b.ActivitiesCount from siteserver_organizationreach a
LEFT JOIN siteserver_publishmentsystem b on b.PublishmentSystemId=a.OrganizationId
LEFT JOIN siteserver_publishmentsystem c on c.PublishmentSystemId=a.ReachOrganizationId
LEFT JOIN siteserver_publishmentsystemdetails d on d.PublishmentSystemId=a.OrganizationId";
                }
            }
            return sql;
        }

        public string GetSortFieldName()
        {
            return "ReachTime";
        }
        public bool AlreadyReach(int publishmentSystemId)
        {
           
            string sqlString =
               $"SELECT COUNT(*) FROM siteserver_organizationreach WHERE (OrganizationId = {publishmentSystemId})";

            return BaiRongDataProvider.DatabaseDao.GetIntResult(sqlString)>0;
        }
        public bool DeleteAlreadyReach(int publishmentSystemId)
        {
            string sqlString =
               $"DElETE  FROM siteserver_organizationreach WHERE (OrganizationId = {publishmentSystemId})";

            return BaiRongDataProvider.DatabaseDao.GetIntResult(sqlString) > 0;
        }
        public OrganizationReachInfo GetReachInfoDetails(int PublishmentSystemId)
        {
            OrganizationReachInfo reachInfo = null;

            var nodeParms = new IDataParameter[]
            {
                GetParameter(ParmOrganizationId, EDataType.Integer, PublishmentSystemId)
            };

            using (var rdr = ExecuteReader(SqlSelectReachDetails, nodeParms))
            {
                if (rdr.Read())
                {
                    var i = 0;
                    reachInfo = new OrganizationReachInfo(GetInt(rdr, i++), GetString(rdr, i++), GetDateTime(rdr, i++));
                }
                rdr.Close();
            }
            return reachInfo;
        }
    }
}
