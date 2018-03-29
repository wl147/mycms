﻿using System.Collections.Generic;
using System.Data;
using BaiRong.Core;
using BaiRong.Core.Data;
using BaiRong.Core.Model.Enumerations;
using SiteServer.CMS.Model;

namespace SiteServer.CMS.Provider
{
    public class SystemPermissionsDao : DataProviderBase
    {
        private const string SqlSelectAllByRoleName = "SELECT RoleName, PublishmentSystemID, NodeIDCollection, ChannelPermissions, WebsitePermissions FROM siteserver_SystemPermissions WHERE RoleName = @RoleName ORDER BY PublishmentSystemID DESC";
        private const string SqlSelectAllByRoleNameByPublishmentId = "SELECT RoleName, PublishmentSystemID, NodeIDCollection, ChannelPermissions, WebsitePermissions FROM siteserver_SystemPermissions WHERE RoleName = @RoleName and PublishmentSystemId=@ParmPublishmentId";

        private const string SqlInsert = "INSERT INTO siteserver_SystemPermissions (RoleName, PublishmentSystemID, NodeIDCollection, ChannelPermissions, WebsitePermissions) VALUES (@RoleName, @PublishmentSystemID, @NodeIDCollection, @ChannelPermissions, @WebsitePermissions)";
        private const string SqlDelete = "DELETE FROM siteserver_SystemPermissions WHERE RoleName = @RoleName";

        private const string ParmRoleRoleName = "@RoleName";
        private const string ParmPublishmentSystemId = "@PublishmentSystemID";
        private const string ParmNodeIdCollection = "@NodeIDCollection";
        private const string ParmChannelPermissions = "@ChannelPermissions";
        private const string ParmWebsitePermissions = "@WebsitePermissions"; 
        private const string ParmPublishmentId = "@ParmPublishmentId";


        public void InsertWithTrans(SystemPermissionsInfo info, IDbTransaction trans)
        {
            if (IsExists(info.RoleName, info.PublishmentSystemId, trans))
            {
                DeleteWithTrans(info.RoleName, info.PublishmentSystemId, trans);
            }

            var insertParms = new IDataParameter[]
			{
				GetParameter(ParmRoleRoleName, EDataType.NVarChar, 255, info.RoleName),
				GetParameter(ParmPublishmentSystemId, EDataType.Integer, info.PublishmentSystemId),
				GetParameter(ParmNodeIdCollection, EDataType.Text, info.NodeIdCollection),
				GetParameter(ParmChannelPermissions, EDataType.Text, info.ChannelPermissions),
				GetParameter(ParmWebsitePermissions, EDataType.Text, info.WebsitePermissions)
			};

            ExecuteNonQuery(trans, SqlInsert, insertParms);
        }
        public void InsertWithTransNew(SystemPermissionsInfo info, IDbTransaction trans)
        {
            //if (IsExists(info.RoleName, info.PublishmentSystemId, trans))
            //{
            //    DeleteWithTrans(info.RoleName, info.PublishmentSystemId, trans);
            //}

            var insertParms = new IDataParameter[]
            {
                GetParameter(ParmRoleRoleName, EDataType.NVarChar, 255, info.RoleName),
                GetParameter(ParmPublishmentSystemId, EDataType.Integer, info.PublishmentSystemId),
                GetParameter(ParmNodeIdCollection, EDataType.Text, info.NodeIdCollection),
                GetParameter(ParmChannelPermissions, EDataType.Text, info.ChannelPermissions),
                GetParameter(ParmWebsitePermissions, EDataType.Text, info.WebsitePermissions)
            };

            ExecuteNonQuery(trans, SqlInsert, insertParms);
        }


        public void DeleteWithTrans(string roleName, IDbTransaction trans)
        {
            var parms = new IDataParameter[]
			{
				GetParameter(ParmRoleRoleName, EDataType.NVarChar, 255, roleName)
			};

            ExecuteNonQuery(trans, SqlDelete, parms);
        }

        private void DeleteWithTrans(string roleName, int publishmentSystemId, IDbTransaction trans)
        {
            var sqlString = "DELETE FROM siteserver_SystemPermissions WHERE RoleName = @RoleName AND PublishmentSystemID = @PublishmentSystemID";

            var parms = new IDataParameter[]
			{
				GetParameter(ParmRoleRoleName, EDataType.NVarChar, 255, roleName),
                GetParameter(ParmPublishmentSystemId, EDataType.Integer, publishmentSystemId)
			};

            ExecuteNonQuery(trans, sqlString, parms);
        }

        private bool IsExists(string roleName, int publishmentSystemId, IDbTransaction trans)
        {
            var isExists = false;

            var sqlString = "SELECT RoleName FROM siteserver_SystemPermissions WHERE RoleName = @RoleName AND PublishmentSystemID = @PublishmentSystemID";

            var parms = new IDataParameter[]
			{
				GetParameter(ParmRoleRoleName, EDataType.NVarChar, 255, roleName),
                GetParameter(ParmPublishmentSystemId, EDataType.Integer, publishmentSystemId)
			};

            using (var rdr = ExecuteReader(trans, sqlString, parms))
            {
                if (rdr.Read())
                {
                    isExists = true;
                }
                rdr.Close();
            }

            return isExists;
        }

        public List<SystemPermissionsInfo> GetSystemPermissionsInfoList(string roleName)
        {
            var list = new List<SystemPermissionsInfo>();

            var parms = new IDataParameter[]
			{
				GetParameter(ParmRoleRoleName, EDataType.NVarChar, 255, roleName)
			};

            using (var rdr = ExecuteReader(SqlSelectAllByRoleName, parms))
            {
                while (rdr.Read())
                {
                    var i = 0;
                    var info = new SystemPermissionsInfo(GetString(rdr, i++), GetInt(rdr, i++), GetString(rdr, i++), GetString(rdr, i++), GetString(rdr, i));
                    list.Add(info);
                }
                rdr.Close();
            }

            return list;
        }
        public List<SystemPermissionsInfo> GetSystemPermissionsInfoListByPublishmentId(string roleName,int publishmentId)
        {
            var list = new List<SystemPermissionsInfo>();

            var parms = new IDataParameter[]
            {
                GetParameter(ParmRoleRoleName, EDataType.NVarChar, 255, roleName),
                GetParameter(ParmPublishmentId, EDataType.Integer, 255, publishmentId)
            };

            using (var rdr = ExecuteReader(SqlSelectAllByRoleNameByPublishmentId, parms))
            {
                while (rdr.Read())
                {
                    var i = 0;
                    var info = new SystemPermissionsInfo(GetString(rdr, i++), GetInt(rdr, i++), GetString(rdr, i++), GetString(rdr, i++), GetString(rdr, i));
                    list.Add(info);
                }
                rdr.Close();
            }

            return list;
        }

        public Dictionary<int, List<string>> GetWebsitePermissionSortedList(string[] roles)
        {
            var sortedlist = new Dictionary<int, List<string>>();
            foreach (var roleName in roles)
            {
                var systemPermissionsList = GetSystemPermissionsInfoList(roleName);
                foreach (var systemPermissionsInfo in systemPermissionsList)
                {
                    var list = new List<string>();
                    var websitePermissionList = TranslateUtils.StringCollectionToStringList(systemPermissionsInfo.WebsitePermissions);
                    foreach (var websitePermission in websitePermissionList)
                    {
                        if (!list.Contains(websitePermission)) list.Add(websitePermission);
                    }
                    sortedlist[systemPermissionsInfo.PublishmentSystemId] = list;
                }
            }

            return sortedlist;
        }

        public Dictionary<int, List<string>> GetChannelPermissionSortedList(string[] roles)
        {
            var sortedlist = new Dictionary<int, List<string>>();

            //ArrayList allNodeIDArrayList = DataProvider.NodeDAO.GetNodeIDArrayList();//所有存在的栏目ID
            foreach (var roleName in roles)
            {
                var systemPermissionsInfoList = GetSystemPermissionsInfoList(roleName);
                foreach (var systemPermissionsInfo in systemPermissionsInfoList)
                {
                    var nodeIdStrList = TranslateUtils.StringCollectionToStringList(systemPermissionsInfo.NodeIdCollection);
                    foreach (var nodeIdStr in nodeIdStrList)
                    {
                        var nodeId = TranslateUtils.ToInt(nodeIdStr);
                        //if (!allNodeIDArrayList.Contains(nodeID))
                        //{
                        //    continue;//此角色包含的栏目已被删除
                        //}
                        var list = new List<string>();
                        if (sortedlist.ContainsKey(nodeId))
                        {
                            list = sortedlist[nodeId];
                        }

                        var channelPermissionList = TranslateUtils.StringCollectionToStringList(systemPermissionsInfo.ChannelPermissions);
                        foreach (var channelPermission in channelPermissionList)
                        {
                            if (!list.Contains(channelPermission)) list.Add(channelPermission);
                        }
                        sortedlist[nodeId] = list;
                    }
                }
            }

            return sortedlist;
        }
        public Dictionary<int, List<string>> GetChannelPermissionListByPublishmentId(string[] roles,int publishmentId)
        {
            var sortedlist = new Dictionary<int, List<string>>();

            //ArrayList allNodeIDArrayList = DataProvider.NodeDAO.GetNodeIDArrayList();//所有存在的栏目ID
            foreach (var roleName in roles)
            {
                var systemPermissionsInfoList = GetSystemPermissionsInfoListByPublishmentId(roleName,publishmentId);
                foreach (var systemPermissionsInfo in systemPermissionsInfoList)
                {
                    var nodeIdStrList = TranslateUtils.StringCollectionToStringList(systemPermissionsInfo.NodeIdCollection);
                    foreach (var nodeIdStr in nodeIdStrList)
                    {
                        var nodeId = TranslateUtils.ToInt(nodeIdStr);
                        //if (!allNodeIDArrayList.Contains(nodeID))
                        //{
                        //    continue;//此角色包含的栏目已被删除
                        //}
                        var list = new List<string>();
                        if (sortedlist.ContainsKey(nodeId))
                        {
                            list = sortedlist[nodeId];
                        }

                        var channelPermissionList = TranslateUtils.StringCollectionToStringList(systemPermissionsInfo.ChannelPermissions);
                        foreach (var channelPermission in channelPermissionList)
                        {
                            if (!list.Contains(channelPermission)) list.Add(channelPermission);
                        }
                        sortedlist[nodeId] = list;
                    }
                }
            }

            return sortedlist;
        }

        public List<string> GetChannelPermissionListIgnoreNodeId(string[] roles)
        {
            var list = new List<string>();
            var roleNameCollection = new List<string>(roles);

            foreach (var roleName in roleNameCollection)
            {
                var systemPermissionsInfoList = GetSystemPermissionsInfoList(roleName);
                foreach (SystemPermissionsInfo systemPermissionsInfo in systemPermissionsInfoList)
                {
                    var channelPermissionList = TranslateUtils.StringCollectionToStringList(systemPermissionsInfo.ChannelPermissions);
                    foreach (var channelPermission in channelPermissionList)
                    {
                        if (!list.Contains(channelPermission))
                        {
                            list.Add(channelPermission);
                        }
                    }
                }
            }

            return list;
        }



        public List<SystemPermissionsInfo> GetSystemPermissionsInfoListByPublishmentSystemId(int publishmentSystemId, string whereStr)
        {
            var list = new List<SystemPermissionsInfo>();

            var parms = new IDataParameter[]
            {
                GetParameter(ParmPublishmentSystemId, EDataType.Integer,  publishmentSystemId)
            };

            using (var rdr = ExecuteReader(
                $"SELECT RoleName, PublishmentSystemID, NodeIDCollection, ChannelPermissions, WebsitePermissions FROM siteserver_SystemPermissions WHERE PublishmentSystemID = @PublishmentSystemID {whereStr} ", parms))
            {
                while (rdr.Read())
                {
                    var i = 0;
                    var info = new SystemPermissionsInfo(GetString(rdr, i++), GetInt(rdr, i++), GetString(rdr, i++), GetString(rdr, i++), GetString(rdr, i));
                    list.Add(info);
                }
                rdr.Close();
            }

            return list;
        }


        private const string SqlSelectAllByRp = "SELECT RoleName, PublishmentSystemID, NodeIDCollection, ChannelPermissions, WebsitePermissions FROM siteserver_SystemPermissions WHERE RoleName = @RoleName AND PublishmentSystemID=@PublishmentSystemID ORDER BY PublishmentSystemID DESC";
        /// <summary>
        /// 根据角色名和站点ID获取角色权限信息
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="publishmentSystemId"></param>
        /// <returns></returns>
        public SystemPermissionsInfo GetSystemPermissionsInfoByRp(string roleName, int publishmentSystemId)
        {
            SystemPermissionsInfo info = null;

            var parms = new IDataParameter[]
            {
                GetParameter(ParmRoleRoleName, EDataType.NVarChar, 255, roleName),
                GetParameter(ParmPublishmentSystemId, EDataType.Integer, publishmentSystemId)
            };

            using (var rdr = ExecuteReader(SqlSelectAllByRp, parms))
            {
                if (rdr.Read())
                {
                    var i = 0;
                    info = new SystemPermissionsInfo(GetString(rdr, i++), GetInt(rdr, i++), GetString(rdr, i++), GetString(rdr, i++), GetString(rdr, i));
                }
                rdr.Close();
            }

            return info;
        }

        /// <summary>
        /// 根据角色名和站点ID获取角色的站点权限信息
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="publishmentSystemId"></param>
        /// <returns></returns>
        public List<string> GetWebsitePermissionListByRp(string roleName, int publishmentSystemId)
        {
            var systemPermissionsInfo = GetSystemPermissionsInfoByRp(roleName, publishmentSystemId);

            return TranslateUtils.StringCollectionToStringList(systemPermissionsInfo.WebsitePermissions);
        }

        string _sqlUpdate = "update siteserver_SystemPermissions set  NodeIDCollection=@NodeIDCollection, ChannelPermissions=@ChannelPermissions, WebsitePermissions=@WebsitePermissions where RoleName =@RoleName and PublishmentSystemID = @PublishmentSystemID";

        public void Update(SystemPermissionsInfo info)
        {
            var updateParms = new IDataParameter[]
            {
                GetParameter(ParmRoleRoleName, EDataType.NVarChar, 255, info.RoleName),
                GetParameter(ParmPublishmentSystemId, EDataType.Integer, info.PublishmentSystemId),
                GetParameter(ParmNodeIdCollection, EDataType.Text, info.NodeIdCollection),
                GetParameter(ParmChannelPermissions, EDataType.Text, info.ChannelPermissions),
                GetParameter(ParmWebsitePermissions, EDataType.Text, info.WebsitePermissions)
            };

            ExecuteNonQuery(_sqlUpdate, updateParms);
        }

        public List<SystemPermissionsInfo> GetAllPermissionList(string[] roles, int publishmentSystemId, bool iscc)
        {
            var permissionList = new List<SystemPermissionsInfo>();
            var roleNameCollection = new List<string>(roles);
            foreach (var roleName in roleNameCollection)
            {
                var systemPermissionsInfoList = GetSystemPermissionsInfoList(roleName);
                foreach (var systemPermissionsInfo in systemPermissionsInfoList)
                {
                    if (publishmentSystemId != 0)
                    {
                        if (iscc)
                        {
                            if (systemPermissionsInfo.PublishmentSystemId == publishmentSystemId &&
                                !string.IsNullOrEmpty(systemPermissionsInfo.NodeIdCollection))
                            {
                                permissionList.Add(systemPermissionsInfo);
                            }
                        }
                        else
                        {
                            if (systemPermissionsInfo.PublishmentSystemId == publishmentSystemId)
                            {
                                permissionList.Add(systemPermissionsInfo);
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(systemPermissionsInfo.NodeIdCollection))
                        {
                            permissionList.Add(systemPermissionsInfo);
                        }
                        else
                        {
                            permissionList.Add(systemPermissionsInfo);
                        }
                    }
                }
            }

            return permissionList;
        }

        public DataTable GetList(string[] roleName)
        {
            string sql = $@"SELECT a.ChannelPermissions,a.NodeId,a.ChannelPermissions,a.WebsitePermissions,b.NodeName from siteserver_systempermissions as a,siteserver_node as b where b.NodeId =a.NodeId and ({GetWhereStringForPermission(roleName)}) GROUP BY a.NodeId";
            DataSet ds = ExecuteDataset(sql);
            //重组列表
            DataTable oldData = ds.Tables[0] as DataTable;
            if (oldData == null)
            {
                return null;
            }
            //创建一个新的DataTable增加一个深度字段
            DataTable newData = oldData.Clone();
            newData.Columns.Add("class_layer", typeof(int));
            //调用迭代组合成DAGATABLE
            //GetChilds(oldData, newData, parent_id, 0);
            return oldData;
        }
        private string GetWhereStringForPermission(string[] roles)
        {
            string retval = string.Empty;
            if (roles == null || roles.Length == 0) return "a.roleName=''";
            foreach(string role in roles)
            {
                retval = retval + $" a.roleName='{role}'  or";
            }
            return retval.EndsWith("or") ? retval.Substring(0, retval.Length - 2) : retval;
        }
        public DataTable GetAllList()
        {
            string sql = $@"select NodeId,NodeName,PublishmentSystemId,('cms_contentView,cms_contentAdd,cms_contentEdit,cms_contentDelete') as ChannelPermissions from siteserver_node where NodeModelType in(1,2,3,4) and NodeId <> PublishmentSystemId and ParentsCount=1 and PublishmentSystemId=1";
            DataSet ds = ExecuteDataset(sql);
            //重组列表
            DataTable oldData = ds.Tables[0] as DataTable;
            if (oldData == null)
            {
                return null;
            }
            //创建一个新的DataTable增加一个深度字段
            DataTable newData = oldData.Clone();
            newData.Columns.Add("class_layer", typeof(int));
            //调用迭代组合成DAGATABLE
            //GetChilds(oldData, newData, parent_id, 0);
            return oldData;
        }
    }
}
