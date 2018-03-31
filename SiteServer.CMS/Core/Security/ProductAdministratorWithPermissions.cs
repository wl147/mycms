using System.Web.Caching;
using BaiRong.Core;
using BaiRong.Core.Configuration;
using System.Collections.Generic;
using BaiRong.Core.Model.Enumerations;
using BaiRong.Core.Permissions;
using System.Data;

namespace SiteServer.CMS.Core.Security
{
	public class ProductAdministratorWithPermissions : AdministratorWithPermissions
	{
		private Dictionary<int, List<string>> _websitePermissionDict;
		private Dictionary<int, List<string>> _channelPermissionDict;
        private Dictionary<int, List<string>> _channelPermissionDictionary;
        private Dictionary<int, List<string>> _govInteractPermissionDict;
        private List<string> _channelPermissionListIgnoreNodeId;
        private List<int> _publishmentSystemIdList;
        private List<int> _owningNodeIdList;
        private List<int> _owningNodeIdListAll;
        private List<int> _owningNodeIdListByPublishmentId;

        private readonly string _websitePermissionDictKey;
        private readonly string _channelPermissionDictKey;
        private readonly string _channelPermissionDictKeyByPublishmentId;
        private readonly string _govInteractPermissionDictKey;
        private readonly string _channelPermissionListIgnoreNodeIdKey;
        private readonly string _publishmentSystemIdListKey;
        private readonly string _owningNodeIdListKey;
        private readonly string _owningNodeIdListAllKey;
        private readonly string _owningNodeIdListKeyBypublishmentId;

        public ProductAdministratorWithPermissions(string userName)
            : base(userName)
		{
            _websitePermissionDictKey = PermissionsManager.GetWebsitePermissionDictKey(userName);
            _channelPermissionDictKey = PermissionsManager.GetChannelPermissionDictKey(userName);
            _govInteractPermissionDictKey = PermissionsManager.GetGovInteractPermissionDictKey(userName);
            _channelPermissionListIgnoreNodeIdKey = PermissionsManager.GetChannelPermissionListIgnoreNodeIdKey(userName);
            _publishmentSystemIdListKey = PermissionsManager.GetPublishmentSystemIdKey(userName);
            _owningNodeIdListKey = PermissionsManager.GetOwningNodeIdListKey(userName);
            _owningNodeIdListAllKey = PermissionsManager.GetOwningNodeIdListAllKey(userName);
            _owningNodeIdListKeyBypublishmentId=PermissionsManager.GetOwningNodeIdListKeyByPublishmentId(userName);
            _channelPermissionDictKeyByPublishmentId= PermissionsManager.GetChannelPermissionDictKeyByPublishmentId(userName);

        }

		public Dictionary<int, List<string>> WebsitePermissionDict
		{
			get
			{
			    if (_websitePermissionDict== null)
				{
                    if (!string.IsNullOrEmpty(UserName) && !string.Equals(UserName, AdminManager.AnonymousUserName))
                    {
                        if (false)// CacheUtils.Get(_websitePermissionDictKey)== null)
                        {
                            _websitePermissionDict = CacheUtils.Get(_websitePermissionDictKey) as Dictionary<int, List<string>>;
                        }
                        else
                        {
                            if (false)//EPredefinedRoleUtils.IsSystemAdministrator(Roles))
                            {
                                var allWebsitePermissionList = new List<string>();
                                foreach (PermissionConfig permission in PermissionConfigManager.Instance.WebsitePermissions)
                                {
                                    allWebsitePermissionList.Add(permission.Name);
                                }

                                _websitePermissionDict = new Dictionary<int, List<string>>();
                                if (PublishmentSystemIdList.Count > 0)
                                {
                                    foreach (var publishmentSystemId in PublishmentSystemIdList)
                                    {
                                        _websitePermissionDict[publishmentSystemId] = allWebsitePermissionList;
                                    }
                                }
                            }
                            else
                            {
                                _websitePermissionDict = DataProvider.SystemPermissionsDao.GetWebsitePermissionSortedListForSimple(Roles);
                            }
                            CacheUtils.Insert(_websitePermissionDictKey, _websitePermissionDict, 30 * CacheUtils.MinuteFactor, CacheItemPriority.Normal);
                        }
                    }
				}
			    return _websitePermissionDict ?? (_websitePermissionDict = new Dictionary<int, List<string>>());
			}
		}
        public Dictionary<int, List<string>> WebsitePermissionDictForNodeCollection
        {
            get
            {
                if (_websitePermissionDict == null)
                {
                    if (!string.IsNullOrEmpty(UserName) && !string.Equals(UserName, AdminManager.AnonymousUserName))
                    {
                        if (CacheUtils.Get(_websitePermissionDictKey) != null)
                        {
                            _websitePermissionDict = CacheUtils.Get(_websitePermissionDictKey) as Dictionary<int, List<string>>;
                        }
                        else
                        {
                            if (EPredefinedRoleUtils.IsSystemAdministrator(Roles))
                            {
                                var allWebsitePermissionList = new List<string>();
                                foreach (PermissionConfig permission in PermissionConfigManager.Instance.WebsitePermissions)
                                {
                                    allWebsitePermissionList.Add(permission.Name);
                                }

                                _websitePermissionDict = new Dictionary<int, List<string>>();
                                if (PublishmentSystemIdList.Count > 0)
                                {
                                    foreach (var publishmentSystemId in PublishmentSystemIdList)
                                    {
                                        _websitePermissionDict[publishmentSystemId] = allWebsitePermissionList;
                                    }
                                }
                            }
                            else
                            {
                                _websitePermissionDict = DataProvider.SystemPermissionsDao.GetWebsitePermissionSortedList(Roles);
                            }
                            CacheUtils.Insert(_websitePermissionDictKey, _websitePermissionDict, 30 * CacheUtils.MinuteFactor, CacheItemPriority.Normal);
                        }
                    }
                }
                return _websitePermissionDict ?? (_websitePermissionDict = new Dictionary<int, List<string>>());
            }
        }
        //GetChannelPermissionListByPublishmentId
        public Dictionary<int, List<string>> ChannelPermissionDictionary
        {
            get
            {
                if (_channelPermissionDictionary == null)
                {
                    if (!string.IsNullOrEmpty(UserName) && !string.Equals(UserName, AdminManager.AnonymousUserName))
                    {
                        if (false)//CacheUtils.Get(_channelPermissionDictKeyByPublishmentId) != null)//)
                        {
                            _channelPermissionDictionary = CacheUtils.Get(_channelPermissionDictKeyByPublishmentId) as Dictionary<int, List<string>>;
                        }
                        else
                        {
                            if (EPredefinedRoleUtils.IsSystemAdministrator(Roles))
                            {
                                var allChannelPermissionList = new List<string>();
                                foreach (PermissionConfig permission in PermissionConfigManager.Instance.ChannelPermissions)
                                {
                                    allChannelPermissionList.Add(permission.Name);
                                }

                                _channelPermissionDictionary = new Dictionary<int, List<string>>();

                                if (PublishmentSystemIdList.Count > 0)
                                {
                                    foreach (var publishmentSystemId in PublishmentSystemIdList)
                                    {
                                        _channelPermissionDictionary[publishmentSystemId] = allChannelPermissionList;
                                    }
                                }
                            }
                            else
                            {
                                var publishmentInfo= BaiRongDataProvider.AdministratorDao.GetByUserName(UserName);
                                _channelPermissionDictionary = DataProvider.SystemPermissionsDao.GetChannelPermissionListByPublishmentId(Roles, publishmentInfo.PublishmentSystemId);
                            }
                            CacheUtils.Insert(_channelPermissionDictKeyByPublishmentId, _channelPermissionDictionary, 30 * CacheUtils.MinuteFactor, CacheItemPriority.Normal);
                        }
                    }
                }
                return _channelPermissionDictionary ?? (_channelPermissionDictionary = new Dictionary<int, List<string>>());
            }
        }
        public Dictionary<int, List<string>> ChannelPermissionDict
        {
			get
			{
			    if (_channelPermissionDict == null)
				{
                    if (!string.IsNullOrEmpty(UserName) && !string.Equals(UserName, AdminManager.AnonymousUserName))
                    {
                        if (false)// CacheUtils.Get(_channelPermissionDictKey) != null)
                        {
                            _channelPermissionDict = CacheUtils.Get(_channelPermissionDictKey) as Dictionary<int, List<string>>;
                        }
                        else
                         {
                            if (EPredefinedRoleUtils.IsSystemAdministrator(Roles))
                            {
                                var allChannelPermissionList = new List<string>();
                                foreach (PermissionConfig permission in PermissionConfigManager.Instance.ChannelPermissions)
                                {
                                    allChannelPermissionList.Add(permission.Name);
                                }

                                _channelPermissionDict = new Dictionary<int, List<string>>();

                                if (PublishmentSystemIdList.Count > 0)
                                {
                                    foreach (var publishmentSystemId in PublishmentSystemIdList)
                                    {
                                        _channelPermissionDict[publishmentSystemId] = allChannelPermissionList;
                                    }
                                }
                            }
                            else
                            {
                                _channelPermissionDict = DataProvider.SystemPermissionsDao.GetChannelPermissionSortedList(Roles);
                            }
                            CacheUtils.Insert(_channelPermissionDictKey, _channelPermissionDict, 30 * CacheUtils.MinuteFactor, CacheItemPriority.Normal);
                        }
                    }
				}
			    return _channelPermissionDict ?? (_channelPermissionDict = new Dictionary<int, List<string>>());
			}
		}

        public List<string> ChannelPermissionListIgnoreNodeId
        {
            get
            {
                if (_channelPermissionListIgnoreNodeId == null)
                {
                    if (!string.IsNullOrEmpty(UserName) && !string.Equals(UserName, AdminManager.AnonymousUserName))
                    {
                        if (CacheUtils.Get(_channelPermissionListIgnoreNodeIdKey) != null)
                        {
                            _channelPermissionListIgnoreNodeId = CacheUtils.Get(_channelPermissionListIgnoreNodeIdKey) as List<string>;
                        }
                        else
                        {
                            if (EPredefinedRoleUtils.IsSystemAdministrator(Roles))
                            {
                                _channelPermissionListIgnoreNodeId = new List<string>();
                                foreach (PermissionConfig permission in PermissionConfigManager.Instance.ChannelPermissions)
                                {
                                    _channelPermissionListIgnoreNodeId.Add(permission.Name);
                                }
                            }
                            else
                            {
                                _channelPermissionListIgnoreNodeId = DataProvider.SystemPermissionsDao.GetChannelPermissionListIgnoreNodeId(Roles);
                            }
                            CacheUtils.Insert(_channelPermissionListIgnoreNodeIdKey, _channelPermissionListIgnoreNodeId, 30 * CacheUtils.MinuteFactor, CacheItemPriority.Normal);
                        }
                    }
                }
                return _channelPermissionListIgnoreNodeId ?? (_channelPermissionListIgnoreNodeId = new List<string>());
            }
        }

        public Dictionary<int, List<string>> GovInteractPermissionDict
        {
            get
            {
                if (_govInteractPermissionDict == null)
                {
                    if (!string.IsNullOrEmpty(UserName) && !string.Equals(UserName, AdminManager.AnonymousUserName))
                    {
                        if (CacheUtils.Get(_govInteractPermissionDictKey) != null)
                        {
                            _govInteractPermissionDict = CacheUtils.Get(_govInteractPermissionDictKey) as Dictionary<int, List<string>>;
                        }
                        else
                        {
                            if (EPredefinedRoleUtils.IsSystemAdministrator(Roles))
                            {
                                var allGovInteractPermissionList = new List<string>();
                                foreach (PermissionConfig permission in PermissionConfigManager.Instance.GovInteractPermissions)
                                {
                                    allGovInteractPermissionList.Add(permission.Name);
                                }

                                _govInteractPermissionDict = new Dictionary<int, List<string>>();

                                if (PublishmentSystemIdList.Count > 0)
                                {
                                    foreach (var publishmentSystemId in PublishmentSystemIdList)
                                    {
                                        _govInteractPermissionDict[publishmentSystemId] = allGovInteractPermissionList;
                                    }
                                }
                            }
                            else
                            {
                                _govInteractPermissionDict = DataProvider.GovInteractPermissionsDao.GetPermissionSortedList(UserName);
                            }
                            CacheUtils.Insert(_govInteractPermissionDictKey, _govInteractPermissionDict, 30 * CacheUtils.MinuteFactor, CacheItemPriority.Normal);
                        }
                    }
                }
                return _govInteractPermissionDict ?? (_govInteractPermissionDict = new Dictionary<int, List<string>>());
            }
        }

        //public string[] Roles
        //{
        //    get
        //    {
        //        if (base.roles == null)
        //        {
        //            if (!string.IsNullOrEmpty(userName) && !string.Equals(userName, AdminManager.AnonymousUserName))
        //            {
        //                if (CacheUtils.Get(base.rolesKey) != null)
        //                {
        //                    base.roles = (string[])CacheUtils.Get(base.rolesKey);
        //                }
        //                else
        //                {
        //                    if (AdminFactory.IsActiveDirectory && StringUtils.EqualsIgnoreCase(userName, AdminFactory.ADAccount))
        //                    {
        //                        base.roles = new string[] { EPredefinedRoleUtils.GetValue(EPredefinedRole.ConsoleAdministrator), EPredefinedRoleUtils.GetValue(EPredefinedRole.Administrator) };
        //                    }
        //                    else
        //                    {
        //                        base.roles = RoleManager.GetRolesForUser(userName);
        //                    }
        //                    CacheUtils.Insert(rolesKey, base.roles, 30 * CacheUtils.MinuteFactor, CacheItemPriority.Normal);
        //                }
        //            }
        //        }
        //        if (roles != null && roles.Length > 0)
        //        {
        //            return base.roles;
        //        }
        //        else
        //        {
        //            return new string[]{EPredefinedRoleUtils.GetValue(EPredefinedRole.Everyone)};
        //        }
        //    }
        //}

        //public bool IsInRole(string role)
        //{
        //    foreach (string r in this.Roles)
        //    {
        //        if (role == r) return true;
        //    }
        //    return false;
        //}

        public List<int> PublishmentSystemIdList
        {
            get
            {
                if (_publishmentSystemIdList != null) return _publishmentSystemIdList;

                if (CacheUtils.Get(_publishmentSystemIdListKey) != null)
                {
                    _publishmentSystemIdList = (List<int>)CacheUtils.Get(_publishmentSystemIdListKey);
                }
                else
                {
                    if (EPredefinedRoleUtils.IsConsoleAdministrator(Roles))
                    {
                        _publishmentSystemIdList = PublishmentSystemManager.GetPublishmentSystemIdList();
                    }
                    else if (EPredefinedRoleUtils.IsSystemAdministrator(Roles))
                    {
                        var thePublishmentSystemIdList = BaiRongDataProvider.AdministratorDao.GetPublishmentSystemIdList(UserName);
                        _publishmentSystemIdList = new List<int>();
                        foreach (var publishmentSystemId in PublishmentSystemManager.GetPublishmentSystemIdList())
                        {
                            if (thePublishmentSystemIdList != null && thePublishmentSystemIdList.Contains(publishmentSystemId))
                            {
                                _publishmentSystemIdList.Add(publishmentSystemId);
                            }
                        }
                    }
                    else
                    {
                        _publishmentSystemIdList = new List<int>();
                        foreach (var publishmentSystemId in WebsitePermissionDict.Keys)
                        {
                            _publishmentSystemIdList.Add(publishmentSystemId);
                        }
                    }

                    if (_publishmentSystemIdList == null)
                    {
                        _publishmentSystemIdList = new List<int>();
                    }

                    CacheUtils.Insert(_publishmentSystemIdListKey, _publishmentSystemIdList, 30 * CacheUtils.MinuteFactor, CacheItemPriority.Normal);
                }
                return _publishmentSystemIdList;
            }
        }

        public List<int> OwningNodeIdList
        {
            get
            {
                if (_owningNodeIdList == null)
                {
                    if (!string.IsNullOrEmpty(UserName) && !string.Equals(UserName, AdminManager.AnonymousUserName))
                    {
                        if (CacheUtils.Get(_owningNodeIdListKey) != null)
                        {
                            _owningNodeIdList = CacheUtils.Get(_owningNodeIdListKey) as List<int>;
                        }
                        else
                        {
                            _owningNodeIdList = new List<int>();

                            var permissions = PermissionsManager.GetPermissions(UserName);

                            if (!permissions.IsSystemAdministrator)
                            {
                                foreach (var nodeId in ProductPermissionsManager.Current.ChannelPermissionDict.Keys)
                                {
                                    _owningNodeIdList.Add(nodeId);
                                    _owningNodeIdList.AddRange(DataProvider.NodeDao.GetNodeIdListForDescendant(nodeId));
                                }
                            }

                            CacheUtils.Insert(_owningNodeIdListKey, _owningNodeIdList, 30 * CacheUtils.MinuteFactor, CacheItemPriority.Normal);
                        }
                    }
                }
                return _owningNodeIdList ?? (_owningNodeIdList = new List<int>());
            }
        }
        public List<int> OwningNodeIdListByPublishmentId
        {
            get
            {
                if (_owningNodeIdListByPublishmentId == null)
                {
                    if (!string.IsNullOrEmpty(UserName) && !string.Equals(UserName, AdminManager.AnonymousUserName))
                    {
                        if (CacheUtils.Get(_owningNodeIdListKeyBypublishmentId) != null)
                        {
                            _owningNodeIdListByPublishmentId = CacheUtils.Get(_owningNodeIdListKeyBypublishmentId) as List<int>;
                        }
                        else
                        {
                            _owningNodeIdListByPublishmentId = new List<int>();

                            var permissions = PermissionsManager.GetPermissions(UserName);

                            if (!permissions.IsSystemAdministrator)
                            {
                                foreach (var nodeId in ProductPermissionsManager.Current.ChannelPermissionDictionary.Keys)
                                {
                                    _owningNodeIdListByPublishmentId.Add(nodeId);
                                    _owningNodeIdListByPublishmentId.AddRange(DataProvider.NodeDao.GetNodeIdListForDescendant(nodeId));
                                }
                            }
                            CacheUtils.Insert(_owningNodeIdListKeyBypublishmentId, _owningNodeIdListByPublishmentId, 30 * CacheUtils.MinuteFactor, CacheItemPriority.Normal);
                        }
                    }
                }
                return _owningNodeIdListByPublishmentId ?? (_owningNodeIdList = new List<int>());
            }
        }
        public List<int> OwningNodeIdListAll
        {
            get
            {
                if (_owningNodeIdListAll == null)
                {
                    if (!string.IsNullOrEmpty(UserName) && !string.Equals(UserName, AdminManager.AnonymousUserName))
                    {
                        if (CacheUtils.Get(_owningNodeIdListKey) != null)
                        {
                            _owningNodeIdListAll = CacheUtils.Get(_owningNodeIdListAllKey) as List<int>;
                        }
                        else
                        {
                            _owningNodeIdListAll = new List<int>();

                            var permissions = PermissionsManager.GetPermissions(UserName);

                            if (!permissions.IsSystemAdministrator)
                            {
                                foreach (var nodeId in ProductPermissionsManager.Current.ChannelPermissionDict.Keys)
                                {
                                    _owningNodeIdListAll.Add(nodeId);
                                    _owningNodeIdListAll.AddRange(DataProvider.NodeDao.GetNodeIdListForDescendant(nodeId));
                                }
                            }

                            CacheUtils.Insert(_owningNodeIdListAllKey, _owningNodeIdListAll, 30 * CacheUtils.MinuteFactor, CacheItemPriority.Normal);
                        }
                    }
                }
                return _owningNodeIdListAll ?? (_owningNodeIdList = new List<int>());
            }
        }
        public List<int> OwningNodeId
        {
            get
            {
                if (_owningNodeIdListAll == null)
                {
                    if (!string.IsNullOrEmpty(UserName) && !string.Equals(UserName, AdminManager.AnonymousUserName))
                    {
                        if (CacheUtils.Get(_owningNodeIdListKey) != null)
                        {
                            _owningNodeIdListAll = CacheUtils.Get(_owningNodeIdListAllKey) as List<int>;
                        }
                        else
                        {
                            _owningNodeIdListAll = new List<int>();

                            var permissions = PermissionsManager.GetPermissions(UserName);

                            if (!permissions.IsSystemAdministrator)
                            {
                                foreach (var nodeId in ProductPermissionsManager.Current.ChannelPermissionDict.Keys)
                                {
                                    _owningNodeIdListAll.Add(nodeId);
                                    _owningNodeIdListAll.AddRange(DataProvider.NodeDao.GetNodeIdListForDescendant(nodeId));
                                }
                            }
                            else
                            {
                                var nodeIds= DataProvider.NodeDao.GetHomeSiteNodeIdList();
                                _owningNodeIdListAll.AddRange(nodeIds);
                            }

                            CacheUtils.Insert(_owningNodeIdListAllKey, _owningNodeIdListAll, 30 * CacheUtils.MinuteFactor, CacheItemPriority.Normal);
                        }
                    }
                }
                return _owningNodeIdListAll ?? (_owningNodeIdList = new List<int>());
            }
        }
        public void ClearCache()
        {
            _websitePermissionDict = null;
            _channelPermissionDict = null;
            _govInteractPermissionDict = null;
            _channelPermissionListIgnoreNodeId = null;
            _publishmentSystemIdList = null;
            _owningNodeIdList = null;

            CacheUtils.Remove(_websitePermissionDictKey);
            CacheUtils.Remove(_channelPermissionDictKey);
            CacheUtils.Remove(_govInteractPermissionDictKey);
            CacheUtils.Remove(_channelPermissionListIgnoreNodeIdKey);
            CacheUtils.Remove(_publishmentSystemIdListKey);
            CacheUtils.Remove(_owningNodeIdListKey);
        }

        public static ProductAdministratorWithPermissions GetProductAnonymousUserWithPermissions()
        {
            var userWithPermissions = new ProductAdministratorWithPermissions(AdminManager.AnonymousUserName);
            return userWithPermissions;
        }
        public  DataTable GetChannelList()
        {
            DataTable dt = new DataTable();
            if (EPredefinedRoleUtils.IsSystemAdministrator(Roles))
            {
                dt = DataProvider.SystemPermissionsDao.GetAllList();
            }
            else
            {
                dt= DataProvider.SystemPermissionsDao.GetList(ProductPermissionsManager.Current.Roles);
            }
            return dt;
        }
	}
}
