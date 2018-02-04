using BaiRong.Core;
using SiteServer.CMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteServer.BackgroundPages.Core
{
   public class SiteTreeItem
    {
        private readonly string _iconFolderUrl;
        private readonly string _iconOpenedFolderUrl;
        private readonly string _iconEmptyUrl;
        private readonly string _iconMinusUrl;
        private readonly string _iconPlusUrl;
        private readonly string _iconAddSiteUrl;
        private readonly string _iconDeleteSiteUrl;

        private bool _enabled = true;
        private PublishmentSystemInfo _publishmentSystemInfo;
        private string _administratorName;
        private int _currentMainId;


        private SiteTreeItem()
        {
            var treeDirectoryUrl = SiteServerAssets.GetIconUrl("tree");
            _iconFolderUrl = PageUtils.Combine(treeDirectoryUrl, "folder.gif");
            _iconOpenedFolderUrl = PageUtils.Combine(treeDirectoryUrl, "openedfolder.gif");
            _iconEmptyUrl = PageUtils.Combine(treeDirectoryUrl, "empty.gif");
            _iconMinusUrl = PageUtils.Combine(treeDirectoryUrl, "minus.png");
            _iconPlusUrl = PageUtils.Combine(treeDirectoryUrl, "plus.png");
            _iconAddSiteUrl= PageUtils.Combine(treeDirectoryUrl, "addSite.png");
            _iconDeleteSiteUrl=PageUtils.Combine(treeDirectoryUrl, "deleteSiteUrl.png");
        }
        public static SiteTreeItem CreateInstance(PublishmentSystemInfo publishmentSystemInfo,int currentMainId)
        {
            return new SiteTreeItem
            {
                _publishmentSystemInfo = publishmentSystemInfo,
                _currentMainId = currentMainId
            };            
        }

        public  string GetItemHtml()
        {
            var htmlBuilder = new StringBuilder();
            var parentsCount = _publishmentSystemInfo.ParentsCount;
            for (var i = 0; i < parentsCount; i++)
            {
                htmlBuilder.Append($@"<img align=""absmiddle"" src=""{_iconEmptyUrl}"" />");
            }
            if (_publishmentSystemInfo.ChildrenCount > 0)
            {
                if (_publishmentSystemInfo.PublishmentSystemId == _currentMainId)
                {
                    htmlBuilder.Append(
                        $@"<img align=""absmiddle"" style=""cursor:pointer"" onClick=""displayChildren(this);"" isAjax=""true"" isOpen=""true"" id=""{_publishmentSystemInfo.PublishmentSystemId}"" src=""{_iconMinusUrl}"" />");
                }
                else
                {
                    htmlBuilder.Append(
                        $@"<img align=""absmiddle"" style=""cursor:pointer"" onClick=""displayChildren(this);"" isAjax=""false"" isOpen=""false"" id=""{_publishmentSystemInfo.PublishmentSystemId}"" src=""{_iconPlusUrl}"" />");
                }
            }
            else
            {
                htmlBuilder.Append($@"<img align=""absmiddle"" src=""{_iconEmptyUrl}"" />");
            }

            htmlBuilder.Append($@"&nbsp;<a href=""www.baidu.com"" target=""_blank"" title=""{_publishmentSystemInfo}""><img align=""absmiddle"" border=""0"" src=""{_iconFolderUrl}"" />{_publishmentSystemInfo.PublishmentSystemName}</a>&nbsp;");

            htmlBuilder.Append($@"&nbsp;<a href=""www.baidu.com"" target=""_blank"" title=""添加""><img align=""absmiddle"" border=""0"" src=""{_iconAddSiteUrl}"" /></a>&nbsp;");

            htmlBuilder.Append($@"&nbsp;<a href=""www.baidu.com"" target=""_blank"" title=""添加""><img align=""absmiddle"" border=""0"" src=""{_iconDeleteSiteUrl}"" /></a>&nbsp;");

            return htmlBuilder.ToString();
        }
    }
}
