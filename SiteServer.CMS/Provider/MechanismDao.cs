using System;
using System.Data;
using BaiRong.Core.Data;
using BaiRong.Core.Model.Enumerations;
using SiteServer.CMS.Model;
using System.Collections.Generic;

namespace SiteServer.CMS.Provider
{
    public class MechanismDao: DataProviderBase
    {
        private const string SqlSelectTypeName = "SELECT Name FROM bairong_MechanismType  WHERE  ID=@MechanismTypeId";

        private const string SqlSelectCategoryName = "SELECT Name FROM bairong_MechanismCategory  WHERE  ID=@MechanismCategoryId";

        private const string SqlSelectAllType = "SELECT Id,Name FROM bairong_MechanismType ";

        private const string SqlSelectAllCategory = "SELECT Id,Name FROM bairong_MechanismCategory ";

        private const string ParaMechanismTypeId = "@MechanismTypeId";

        private const string ParaMechanismCategoryId = "@MechanismCategoryId";

        public  string GetMechanismTypeTextById(int MechanismCategoryId)
        {
            string retval = string.Empty;
            var parms = new IDataParameter[]
            {
                GetParameter(ParaMechanismTypeId, EDataType.Integer, MechanismCategoryId),
            };
            using (var rdr = ExecuteReader(SqlSelectTypeName, parms))
            {
                if (rdr.Read())
                {
                    retval = GetString(rdr, 0);
                }
                rdr.Close();
            }
            return retval;
        }
        public  string GetMechanismCategoryTextById(int MechanismCategoryId)
        {
            string retval = string.Empty;
            var parms = new IDataParameter[]
            {
                GetParameter(ParaMechanismCategoryId, EDataType.Integer, MechanismCategoryId),
            };
            using (var rdr = ExecuteReader(SqlSelectCategoryName, parms))
            {
                if (rdr.Read())
                {
                    retval = GetString(rdr, 0);
                }
                rdr.Close();
            }
            return retval;
        }
        public Dictionary<int,string> GetMechanismTypeAll()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            using (var rdr = ExecuteReader(SqlSelectAllType))
            {
                while(rdr.Read())
                {
                    dic.Add(GetInt(rdr, 0),GetString(rdr, 1));
                }
                rdr.Close();
            }
            return dic;
        }
        public Dictionary<int, string> GetMechanismCategoryAll()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            using (var rdr = ExecuteReader(SqlSelectAllCategory))
            {
                while(rdr.Read())
                {
                    dic.Add(GetInt(rdr, 0), GetString(rdr, 1));
                }
                rdr.Close();
            }
            return dic;
        }
    }
}
