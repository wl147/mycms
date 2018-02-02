using System.Text;
using System.Web.UI;
using BaiRong.Core;
using BaiRong.Core.Tabs;
using SiteServer.BackgroundPages.Core;
using SiteServer.CMS.Core;
using SiteServer.CMS.Core.Security;
using System;
using BaiRong.Core.Model.Enumerations;

namespace SiteServer.BackgroundPages.Controls
{
    public class NodeNaviTree : TabDrivenTemplatedWebControl
    {
        protected override void Render(HtmlTextWriter writer)
        {
            var builder = new StringBuilder();
            //GetCmsMenu(builder, ECmsType.News, false);
            //GetCmsMenu(builder, ECmsType.Study, false);
            //GetCmsMenu(builder, ECmsType.Partake, false);
            //GetCmsMenu(builder, ECmsType.Branch, false);
            //BuildNavigationTree(builder, GetTabs(), 0, true);
            BuildNavigationTree(builder, GetAllTabs(PublishmentSystemId), 0, true);
            writer.Write(builder);
        }
//        protected void GetCmsMenu(StringBuilder builder,ECmsType type,bool isOpen )
//        {
//            string title = $@"
//<tr style='display:' treeItemLevel='1'>
//  <td nowrap>
//	<img align=""absmiddle"" style=""cursor: pointer; "" onClick=""displayChildren(this); "" isOpen=""{isOpen}"" src=""/siteserver/assets/icons/tree/{(isOpen? "minus.png":"plus.png")}""/>
//    <img align=""absmiddle"" src=""/siteserver/assets/icons/menu/content.png""/>&nbsp;
//    {ECmsTypeUtils.GetText(type)}
//  </td >
//</tr > ";
           
//            builder.Append(title);
//            var _publishmentSystemInfo = PublishmentSystemManager.GetPublishmentSystemInfo(PublishmentSystemId);
//            try
//            {
//                var nodeIdList = DataProvider.NodeDao.GetNodeIdListByLevel(1, ECmsTypeUtils.GetDBType(type));
//                foreach (var nodeId in nodeIdList)
//                {
//                    var nodeInfo = NodeManager.GetNodeInfo(1, nodeId);
//                    if (nodeInfo != null)
//                    {
//                        builder.Append(GetChannelHtml(nodeInfo.PublishmentSystemId, nodeInfo.NodeId, nodeInfo.NodeName,isOpen));
//                    }                    
//                }

//            }
//            catch (Exception ex)
//            {
//                PageUtils.RedirectToErrorPage(ex.Message);
//            }

//        }
//        protected string GetChannelHtml(int publishmentId,int nodeId,string menuText,bool isDisplay)
//        {
//            string menuTemplete = $@"
//<tr style='{(isDisplay ? "display:" : "display: none")}' treeItemLevel='2'>
//     <td nowrap>
//         <img align = ""absmiddle"" src = ""/siteserver/assets/icons/tree/empty.gif"" />
//         <img align = ""absmiddle"" src = ""/siteserver/assets/icons/tree/empty.gif"" />
//         <img align = ""absmiddle"" src = ""/siteserver/assets/icons/menu/itemContainer.png"" /> &nbsp;
//         <a href='/siteserver/cms/pagecontent.aspx?PublishmentSystemID={publishmentId}&NodeID={nodeId}'  target = 'right' onclick = 'openFolderByA(this);' isTreeLink = 'true' >{menuText} </a> &nbsp;
//         </td >
//</tr > ";
//            return menuTemplete;
//        }        
        /// <summary>
        /// Creates the markup for the current TabCollection
        /// </summary>
        /// <returns></returns>
        protected void BuildNavigationTree(StringBuilder builder, TabCollection tc, int parentsCount, bool isDisplay)
        {
            if (tc?.Tabs == null) return;
            
            foreach (var parent in tc.Tabs)
            {
                //    var nodeInfo = NodeManager.GetNodeInfo(1,nodeId);//子站继承主站栏目
                //    var enabled = AdminUtility.IsOwningNodeIdAll(body.AdministratorName, nodeInfo.NodeId);//管理员拥有权限的栏目
                //}
                if (!TabManager.IsValid(parent, PermissionList)) continue;
                if (parent.MenuType!=null&&parent.MenuType.Equals("cmsItem", StringComparison.OrdinalIgnoreCase))
                {
                    if (!AdminUtility.IsOwningNodeIdByPublishmentSystem(UserName, parent.NodeId)) continue;
                }
                //if ((parent.MenuType != null && parent.MenuType.Equals("cms", StringComparison.OrdinalIgnoreCase) && !CmsHasChildrenPerminssion(parent))) continue;
                if (!HasFirstRootMenu(parent)) continue;

                var linkUrl = FormatLink(parent);
               
                if (!string.IsNullOrEmpty(linkUrl) && !StringUtils.EqualsIgnoreCase(linkUrl, PageUtils.UnclickedUrl))
                {
                    linkUrl = PageUtils.GetLoadingUrl(linkUrl);
                }
                if (parent.MenuType != null&&parent.MenuType.Equals("cmsItem", StringComparison.OrdinalIgnoreCase))
                {
                    linkUrl = PageUtils.GetAdminDirectoryUrl(parent.Href);
                }
                var hasChildren = parent.Children != null && parent.Children.Length > 0;
                var openWindow = !hasChildren && StringUtils.EndsWithIgnoreCase(parent.Href, "main.aspx");

                var item = NavigationTreeItem.CreateNavigationBarItem(isDisplay, parent.Selected, parentsCount, hasChildren, openWindow, parent.Text, linkUrl, parent.Target, parent.Enabled, parent.IconUrl);

                builder.Append(item.GetTrHtml());
                if (parent.Children != null && parent.Children.Length > 0)
                {
                    var tc2 = NodeNaviTabManager.GetTabCollection(parent, PublishmentSystemId);
                    BuildNavigationTree(builder, tc2, parentsCount + 1, parent.Selected);
                }
            }
        }
        public bool CmsHasChildrenPerminssion(Tab tab)
        {
            bool retval = false;
            var nodeList = ProductPermissionsManager.Current.OwningNodeIdListByPublishmentId;
            if (tab.HasChildren)
            {
                if (nodeList != null && nodeList.Count > 0)
                {
                    foreach (int nodeId in nodeList)
                    {
                        foreach(Tab  tabChildren in tab.Children)
                        {
                            if (tabChildren.NodeId == nodeId) return true;
                        }
                    }
                }
            }
            return retval;
           
        }
        public bool HasFirstRootMenu(Tab tab)
        {
            bool retval = false;
            if (tab.MenuType != null)
            {
                if (tab.MenuType.Equals("cms", StringComparison.OrdinalIgnoreCase))
                {
                    if (CmsHasChildrenPerminssion(tab)) retval = true;
                }else if (tab.MenuType.Equals("cmsItem", StringComparison.OrdinalIgnoreCase))
                {
                    retval = true;
                }
                else
                {
                    if (tab.HasChildren) retval = true;
                }
            }
            else
            {
                retval = true;
            }
            return retval;
        }

        public int PublishmentSystemId { get; set; }
        public string UserName { get; set; }

        private string _selected;
        public override string Selected
        {
            get { return _selected ?? (_selected = Context.Items["ControlPanelSelectedNavItem"] as string); }
            set
            {
                _selected = value;
            }
        }
    }
}
