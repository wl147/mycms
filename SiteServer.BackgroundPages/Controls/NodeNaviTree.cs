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
            GetCmsMenu(builder,ECmsType.News);
            GetCmsMenu(builder, ECmsType.Study);
            GetCmsMenu(builder, ECmsType.Partake);
            GetCmsMenu(builder, ECmsType.Branch);
            BuildNavigationTree(builder, GetTabs(), 0, true);
            writer.Write(builder);
        }
        protected void GetCmsMenu(StringBuilder builder,ECmsType type )
        {
            string title = $@"
<tr style='display:' treeItemLevel='1'>
  <td nowrap>
	<img align=""absmiddle"" style=""cursor: pointer; "" onClick=""displayChildren(this); "" isOpen=""true"" src=""/siteserver/assets/icons/tree/minus.png""/>
    <img align=""absmiddle"" src=""/siteserver/assets/icons/menu/content.png""/>&nbsp;
    {ECmsTypeUtils.GetText(type)}
  </td >
</tr > ";
           
            builder.Append(title);
            var _publishmentSystemInfo = PublishmentSystemManager.GetPublishmentSystemInfo(PublishmentSystemId);
            try
            {
                var nodeIdList = DataProvider.NodeDao.GetNodeIdListByLevel(1, ECmsTypeUtils.GetDBType(type));
                foreach (var nodeId in nodeIdList)
                {
                    var nodeInfo = NodeManager.GetNodeInfo(1, nodeId);
                    if (nodeInfo != null)
                    {
                        builder.Append(GetChannelHtml(nodeInfo.PublishmentSystemId, nodeInfo.NodeId, nodeInfo.NodeName));
                    }
                    
                }

            }
            catch (Exception ex)
            {
                PageUtils.RedirectToErrorPage(ex.Message);
            }

        }
        protected string GetChannelHtml(int publishmentId,int nodeId,string menuText)
        {
            string menuTemplete = $@"
<tr style='display:' treeItemLevel='2'>
    <td nowrap>
         <img align = ""absmiddle"" src = ""/siteserver/assets/icons/tree/empty.gif"" />
         <img align = ""absmiddle"" src = ""/siteserver/assets/icons/tree/empty.gif"" />
         <img align = ""absmiddle"" src = ""/siteserver/assets/icons/menu/contents.gif"" /> &nbsp;
         <a href='/siteserver/cms/pagecontent.aspx?PublishmentSystemID={publishmentId}&NodeID={nodeId}'  target = 'right' onclick = 'openFolderByA(this);' isTreeLink = 'true' >{menuText} </a> &nbsp;
         </td >
</tr > ";
            return menuTemplete;
        }        
        /// <summary>
        /// Creates the markup for the current TabCollection
        /// </summary>
        /// <returns></returns>
        protected void BuildNavigationTree(StringBuilder builder, TabCollection tc, int parentsCount, bool isDisplay)
        {
            if (tc?.Tabs == null) return;

            foreach (var parent in tc.Tabs)
            {
                if (!TabManager.IsValid(parent, PermissionList)) continue;

                var linkUrl = FormatLink(parent);
                if (!string.IsNullOrEmpty(linkUrl) && !StringUtils.EqualsIgnoreCase(linkUrl, PageUtils.UnclickedUrl))
                {
                    linkUrl = PageUtils.GetLoadingUrl(linkUrl);
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

        public int PublishmentSystemId { get; set; }

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
