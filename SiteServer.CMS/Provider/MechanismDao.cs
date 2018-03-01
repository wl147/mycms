using System;
using System.Data;
using BaiRong.Core.Data;
using BaiRong.Core.Model.Enumerations;
using SiteServer.CMS.Model;

namespace SiteServer.CMS.Provider
{
    public class MechanismDao: DataProviderBase
    {
        private const string SqlSelectTypeName = "SELECT Name FROM bairong_MechanismType  WHERE  ID=@MechanismTypeId";

        private const string SqlSelectCategoryName = "SELECT Name FROM bairong_MechanismCategory  WHERE  ID=@MechanismCategoryId";

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

    }
}
