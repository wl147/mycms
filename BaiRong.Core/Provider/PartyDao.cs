using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BaiRong.Core.Cryptography;
using BaiRong.Core.Data;
using BaiRong.Core.Model;
using BaiRong.Core.Model.Enumerations;

namespace BaiRong.Core.Provider
{
    public class PartyDao : DataProviderBase
    {
        private const string ParmId = "@ID";
        /// <summary>
        /// 获取党员转出列表
        /// </summary>
        /// <param name="transformId"></param>
        /// <returns></returns>
        public string GetPartyTransform(int transformId)
        {
            return $@"select siteserver_transformcheck.ID,siteserver_transformcheck.ApplyUserId,bairong_Users.UserName,bairong_Users.Mobile as MobilePhone,siteserver_transformcheck.TransformOutId,
                      siteserver_transformcheck.TransformInId,siteserver_PublishmentSystem.PublishmentSystemName as TransformInName,siteserver_transformcheck.ApplyDate,
                      siteserver_transformcheck.OutCheckState,siteserver_transformcheck.InCheckState
                      from siteserver_transformcheck
                      left JOIN bairong_Users on bairong_Users.UserId = siteserver_transformcheck.ApplyUserId
                      LEFT JOIN siteserver_PublishmentSystem on siteserver_transformcheck.TransformInId = siteserver_PublishmentSystem.PublishmentSystemId
                      where OutCheckState = 0 and TransformOutId = "+ transformId;
        }

        /// <summary>
        /// 获取党员转入列表
        /// </summary>
        /// <param name="transformId"></param>
        /// <returns></returns>
        public string GetPartyTransformIn(int transformId)
        {
            return $@"select siteserver_transformcheck.ID,siteserver_transformcheck.ApplyUserId,bairong_Users.UserName,bairong_Users.Mobile as MobilePhone,siteserver_transformcheck.TransformOutId,
                      siteserver_transformcheck.TransformInId,siteserver_PublishmentSystem.PublishmentSystemName as TransformOutName,siteserver_transformcheck.ApplyDate,
                      siteserver_transformcheck.OutCheckState,siteserver_transformcheck.InCheckState
                      from siteserver_transformcheck
                      left JOIN bairong_Users on bairong_Users.UserId = siteserver_transformcheck.ApplyUserId
                      LEFT JOIN siteserver_PublishmentSystem on siteserver_transformcheck.TransformOutId = siteserver_PublishmentSystem.PublishmentSystemId
                      where OutCheckState = 1 and InCheckState=0 and TransformOutId = " + transformId;
        }

        /// <summary>
        /// 转出审核
        /// </summary>
        /// <param name="transId"></param>
        public void CheckOutOK(int transId) {
            const string sqlString = "UPDATE siteserver_transformcheck SET OutCheckState=1 , OutCheckDate=now() WHERE ID = @ID";

            var updateParms = new IDataParameter[]
            {
                GetParameter(ParmId, EDataType.Integer, transId)
            };

            ExecuteNonQuery(sqlString, updateParms);
        }

        /// <summary>
        /// 转入审核
        /// </summary>
        /// <param name="transId"></param>
        public void CheckInOK(int transId)
        {
            const string sqlString = "UPDATE siteserver_transformcheck SET InCheckState=1,InCheckDate=now() WHERE ID = @ID";

            var updateParms = new IDataParameter[]
            {
                GetParameter(ParmId, EDataType.Integer, transId)
            };

            ExecuteNonQuery(sqlString, updateParms);
        }

        public string GetSortFieldDate()
        {
            return "ApplyDate";
        }
    }
}
