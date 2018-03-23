using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using BaiRong.Core;
using BaiRong.Core.Tabs;
using SiteServer.CMS.Core;
using SiteServer.CMS.Core.Security;
using SiteServer.CMS.Model;

namespace SiteServer.BackgroundPages.Core
{

	/// <summary>
	/// Renders a Tab + Submenus based on the tab configuration file.
	/// </summary>
	public abstract class TabDrivenTemplatedWebControl : Control
	{
		#region Public Properties

		/// <summary>
		/// Returns the currently selected tab
		/// </summary>
		public virtual string Selected
		{
			get 
			{
				return (string) ViewState["Selected"]; 
			}
			set 
			{  
				ViewState["Selected"] = value; 
			}
		}
        
		/// <summary>
		/// returns the location of the current tab configuration file
		/// </summary>
		public virtual string FileLocation
		{
			get
			{
				var path = Context.Server.MapPath(ResolveUrl(FileName));
				return path;
			}
		}

		/// <summary>
		/// returns the file name containing the tab configuration
		/// </summary>
		public virtual string FileName
		{
			get 
			{
				var state = ViewState["FileName"];
				if ( state != null ) 
				{
					return (String)state;
				}
                return null;
				//return "tabs.config";
			}
			set 
			{
				ViewState["FileName"] = value;
			}
		}

        TabCollection tabs;
        public virtual TabCollection Tabs
        {
            get
            {
                return tabs;
            }
            set
            {
                tabs = value;
            }
        }

        List<string> permissionList;
		public virtual List<string> PermissionList
		{
			get 
			{
				return permissionList;
			}
			set 
			{
                permissionList = value;
			}
		}

		/// <summary>
		/// if true, the url is written into the control label
		/// </summary>
		public virtual bool UseDirectNavigation
		{
			get 
			{
				var state = ViewState["UseDirectNavigation"];
				if ( state != null ) 
				{
					return (bool)state;
				}
				return false;
			}
			set 
			{
				ViewState["UseDirectNavigation"] = value;
			}
		}
		#endregion

		#region GetTabs()
		/// <summary>
		/// Returns the current instance of the TabCollection
		/// </summary>
		/// <returns></returns>
		protected TabCollection GetTabs()
		{
            if (tabs != null)
            {
                return tabs;
            }
            else
            {
                if (!string.IsNullOrEmpty(FileName))
                {
                    var path = FileLocation;
                    return TabCollection.GetTabs(path);
                }
                return new TabCollection();
            }
		}
        protected TabCollection GetAllTabs(int publishmentSystemId)
        {
            if (tabs != null)
            {
                return tabs;
            }
            else
            {
                if (!string.IsNullOrEmpty(FileName))
                {
                    var path = FileLocation;
                    var tc = TabCollection.GetTabs(path);
                    foreach (Tab tab in tc.Tabs)
                    {
                        if (!tab.HasChildren &&StringUtils.EqualsIgnoreCase("cms",tab.MenuType))
                        {
                            var nodeIdList = DataProvider.NodeDao.GetHomeSiteNodeIdList();
                            foreach (var nodeId in nodeIdList)
                            {                                
                                var nodeInfo = NodeManager.GetNodeInfo(1, nodeId);
                                if (tab.Id != null && tab.Id.Equals(NodeManager.GetNodeModel(nodeInfo),StringComparison.OrdinalIgnoreCase))
                                {
                                    if (nodeInfo != null)
                                    {
                                        List<Tab> tabList = tab.ChildrenChannels;
                                        Tab tabChannel = ChannelInfoToTable(nodeInfo, publishmentSystemId);
                                        tabList.Add(tabChannel);
                                        if (tabList != null && tabList.Count > 0)
                                        {
                                            tab.Children = TabListToArray(tabList);
                                        }
                                    }
                                }                                                      
                            }
                        }
                    }
                    return tc;
                }
                return new TabCollection();
            }
        }
        /// <summary>
        /// 栏目信息转菜单
        /// </summary>
        /// <param name="nodeInfo"></param>
        /// <param name="publishmentSystemId"></param>
        /// <returns></returns>
        protected Tab ChannelInfoToTable(NodeInfo nodeInfo,int publishmentSystemId)
        {
            Tab tab = new Tab();
            tab.Text = nodeInfo.NodeName;
            tab.Permissions = "cms_contentView,cms_contentAdd";
            if (nodeInfo.NodeName.Equals("广告管理"))//菜单链接配置
            {
                tab.Href = $@"cms/pageAdArea.aspx?PublishmentSystemID={publishmentSystemId}";
            }else if (nodeInfo.NodeName.Equals("学习课件"))
            {
                tab.Href = $@"cms/pageContentStudy.aspx?PublishmentSystemID={publishmentSystemId}&NodeId={nodeInfo.NodeId}";
            }
            else if (nodeInfo.NodeName.Equals("效果测评"))
            {
                tab.Href = $@"cms/pageContentReview.aspx?PublishmentSystemID={publishmentSystemId}&NodeId={nodeInfo.NodeId}";
            }
            else if (nodeInfo.NodeName.Equals("师资库"))
            {
                tab.Href = $@"cms/pageContentTeachers.aspx?PublishmentSystemID={publishmentSystemId}&NodeId={nodeInfo.NodeId}";
            }
            else if (nodeInfo.NodeName.Equals("志愿服务"))
            {
                tab.Href = $@"cms/pageContentService.aspx?PublishmentSystemID={publishmentSystemId}&NodeId={nodeInfo.NodeId}";
            }
            else if (nodeInfo.NodeName.Equals("爱心小屋"))
            {
                tab.Href = $@"cms/pageContentCare.aspx?PublishmentSystemID={publishmentSystemId}&NodeId={nodeInfo.NodeId}";
            }
            else if (nodeInfo.NodeName.Equals("领导信箱"))
            {
                tab.Href = $@"cms/pageContentLeaderBox.aspx?PublishmentSystemID={publishmentSystemId}&NodeId={nodeInfo.NodeId}";
            }
            else if (nodeInfo.NodeName.Equals("心愿墙"))
            {
                tab.Href = $@"cms/pageContentWishWall.aspx?PublishmentSystemID={publishmentSystemId}&NodeId={nodeInfo.NodeId}";
            }
            else if (nodeInfo.NodeName.Equals("党委介绍"))
            {
                tab.Href = $@"cms/pagePartyIntroduce.aspx?PublishmentSystemID={publishmentSystemId}&NodeId={nodeInfo.NodeId}";
            }
            else if (nodeInfo.NodeName.Equals("支部介绍"))
            {
                tab.Href = $@"cms/pageBranchIntroduce.aspx?PublishmentSystemID={publishmentSystemId}&NodeId={nodeInfo.NodeId}";
            }
            else if (nodeInfo.NodeName.Equals("组织活动"))
            {
                tab.Href = $@"cms/pageContentOrganization.aspx?PublishmentSystemID={publishmentSystemId}&NodeId={nodeInfo.NodeId}";
            }
            else if (nodeInfo.NodeName.Equals("组织报道"))
            {
                tab.Href = $@"cms/pageOrganizationReach.aspx?PublishmentSystemID={publishmentSystemId}&NodeId={nodeInfo.NodeId}";
            }
            else
            {
                tab.Href = $@"cms/pagecontent.aspx?PublishmentSystemID={publishmentSystemId}&NodeID={nodeInfo.NodeId}";
            } 
            tab.IconUrl = "menu/itemContainer.png";
            tab.KeepQueryString = true;
            tab.Target = "right";
            tab.MenuType = "cmsItem";
            tab.NodeId = nodeInfo.NodeId;
            
            return tab;
        }
        protected Tab[] TabListToArray(List<Tab> tabs)
        {
            Tab[] Children = new Tab[tabs.Count];
            for (int i= 0; i < tabs.Count; i++)
            {
                Children[i] = tabs[i];
            }
            return Children;
        }
		#endregion

		#region Tab Helpers

		/// <summary>
		/// Resolves the current url and attempts to append the specified querystring
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		protected string FormatLink(Tab t)
		{
			string url = null;

			if(!t.HasHref)
				return null;

			if(t.KeepQueryString)
			{
				url = PageUtils.AddQueryString(t.Href, Context.Request.QueryString);
			}
			else
			{
				url = t.Href;
			}

		    return url;

			//return ResolveUrl(url);
		}

		protected virtual string GetText(Tab t)
		{
			return t.Text;
		}
		#endregion

		#region GetState
		/// <summary>
		/// Walks the tab and it's children to see if any of them are currently selected
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		protected SelectedState GetState(Tab t)
		{
			//Check the parent
			if(string.Compare(t.Name,Selected,true,CultureInfo.InvariantCulture) == 0)
				return SelectedState.Selected;

			//Walk each of the child tabs
			if(t.HasChildren)
			{
				foreach(var child in t.Children)
				{
					if(string.Compare(child.Name,Selected,true,CultureInfo.InvariantCulture) == 0)
						return SelectedState.ChildSelected;

					else if(child.HasChildren)
					{
						foreach(var cc in child.Children)
							if(string.Compare(cc.Name,Selected,true,CultureInfo.InvariantCulture) == 0)
								return SelectedState.ChildSelected;
					}
				}
			}

			//Nothing here is selected
			return SelectedState.Not;
		}

		#endregion

		#region SelectedState
		/// <summary>
		/// Internal enum used to track if a tab is selected
		/// </summary>
		protected enum SelectedState
		{
			Not,
			Selected,
			ChildSelected
		};
		#endregion

	}
}
