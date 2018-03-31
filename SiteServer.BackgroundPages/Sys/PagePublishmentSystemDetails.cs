using System;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using BaiRong.Core;
using BaiRong.Core.Model.Enumerations;
using SiteServer.CMS.Core;
using SiteServer.CMS.Model;
using System.Collections.Generic;
using SiteServer.CMS.Provider;

namespace SiteServer.BackgroundPages.Sys
{
    public class PagePublishmentSystemDetails : BasePageCms
    {
        public DataGrid dgContents;
        private int _hqSiteId;

        public static string GetRedirectUrl()
        {
            return PageUtils.GetSysUrl(nameof(PagePublishmentSystem), null);
        }

        public void Page_Load(object sender, EventArgs e)
        {
            if (IsForbidden) return;
            if (!HasWebsitePermissions(AppManager.Cms.Permission.WebSite.SiteManagement))
            {
                FailMessage("无站点管理权限！");
                return;
            }        
            if (!IsPostBack)
            {
                var currentPublishmentSystemId = Body.GetQueryInt("PublishmentSystemID");
                var publishmentSystemList= DataProvider.PublishmentSystemDao.GetPublishmentSystemInfoListByParentId(currentPublishmentSystemId);
                List<int> list = new List<int>();   
                foreach(var site in publishmentSystemList)
                {
                    list.Add(site.PublishmentSystemId);
                }       
                BreadCrumbSys(AppManager.Sys.LeftMenu.Site, "组织机构", AppManager.Sys.Permission.SysSite);
                _hqSiteId = currentPublishmentSystemId;
                dgContents.DataSource = list;//DataProvider.PublishmentSystemDao.GetPublishmentSystemInfoListByParentId(currentPublishmentSystemId);
                dgContents.ItemDataBound += dgContents_ItemDataBound;
                dgContents.DataBind();
            }
        }

        void dgContents_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            var mechanismDao = new MechanismDao();
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var publishmentSystemID = (int)e.Item.DataItem;
                var publishmentSystemInfo = PublishmentSystemManager.GetPublishmentSystemInfo(publishmentSystemID);
                if (publishmentSystemInfo != null)
                {
                    var ltlPublishmentSystemName = e.Item.FindControl("ltlPublishmentSystemName") as Literal;
                    var ltlPublishmentSystemType = e.Item.FindControl("ltlPublishmentSystemType") as Literal;
                    var ltlPublishmentSystemAdress = e.Item.FindControl("ltlPublishmentSystemAdress") as Literal;
                    var ltlOperation = e.Item.FindControl("ltlOperation") as Literal;
                    //var ltlChangeType = e.Item.FindControl("ltlChangeType") as Literal;
                    //var ltlDelete = e.Item.FindControl("ltlDelete") as Literal;

                    //var ltUpLink = e.Item.FindControl("ltUpLink") as Literal;
                    //var ltDownLink = e.Item.FindControl("ltDownLink") as Literal;

                    ltlPublishmentSystemName.Text = GetPublishmentSystemNameHtml(publishmentSystemInfo);
                    ltlPublishmentSystemAdress.Text = publishmentSystemInfo.Address;
                    ltlPublishmentSystemType.Text = mechanismDao.GetMechanismTypeTextById(publishmentSystemInfo.OrganizationTypeId); //publishmentSystemInfo.OrganizationTypeId.ToString()+"--类型表未建立，此数字代表id";
                    if (HasWebsitePermissions(AppManager.Cms.Permission.WebSite.SiteEdit))
                    {
                        ltlOperation.Text = $@"<a href=""PageSiteEdit.aspx?PublishmentSystemId={publishmentSystemInfo.PublishmentSystemId}"" target=""content"">操作</a>";
                    }
                    else
                    {
                        ltlOperation.Text = $@"<a href=""javascript:void(0)"" target=""content"">无</a>";
                    }
                    
                    var upUrl = PageUtils.GetSysUrl(nameof(PagePublishmentSystem), new NameValueCollection
                    {
                        {"Up", "True" },
                        {"PublishmentSystemID", publishmentSystemID.ToString() }
                    });
                    //ltUpLink.Text = $@"<a href=""{upUrl}""><img src=""../Pic/icon/up.gif"" border=""0"" alt=""上升""/></a>";

                    var downUrl = PageUtils.GetSysUrl(nameof(PagePublishmentSystem), new NameValueCollection
                    {
                        {"Down", "True" },
                        {"PublishmentSystemID", publishmentSystemID.ToString() }
                    });
                    //ltDownLink.Text = $@"<a href=""{downUrl}""><img src=""../Pic/icon/down.gif"" border=""0"" alt=""下降""/></a>";

                    //if (publishmentSystemInfo.ParentPublishmentSystemId == 0 && (_hqSiteId == 0 || publishmentSystemID == _hqSiteId))
                    //{
                    //    ltlChangeType.Text = GetChangeHtml(publishmentSystemID, publishmentSystemInfo.IsHeadquarters);
                    //}

                    //if (publishmentSystemInfo.IsHeadquarters == false)
                    //{
                    //    ltlDelete.Text = $@"<a href=""{PagePublishmentSystemDelete.GetRedirectUrl(publishmentSystemID)}"">删除</a>";
                    //}
                }
            }
        }

        private string GetPublishmentSystemNameHtml(PublishmentSystemInfo publishmentSystemInfo)
        {
            var level = PublishmentSystemManager.GetPublishmentSystemLevel(publishmentSystemInfo.PublishmentSystemId);
            var psLogo = string.Empty;
            if (publishmentSystemInfo.IsHeadquarters)
            {
                psLogo = "siteHQ.gif";
            }
            else
            {
                psLogo = "site.gif";
                if (level > 0 && level < 10)
                {
                    psLogo = $"subsite{level + 1}.gif";
                }
            }
            psLogo = SiteServerAssets.GetIconUrl("tree/" + psLogo);

            var padding = string.Empty;
            for (var i = 0; i < level; i++)
            {
                padding += "　";
            }
            if (level > 0)
            {
                padding += "└ ";
            }

            return
                $"{padding}<img align='absbottom' border='0' src='{psLogo}'/>&nbsp;<a href='{publishmentSystemInfo.PublishmentSystemUrl}' target='_blank'>{publishmentSystemInfo.PublishmentSystemName}</a>";
        }

        private string GetChangeHtml(int publishmentSystemID, bool isHeadquarters)
        {
            var showPopWinString = ModalChangePublishmentSystemType.GetOpenWindowString(publishmentSystemID);

            if (isHeadquarters == false)
            {
                return $"<a href=\"javascript:;\" onClick=\"{showPopWinString}\">转移到根目录</a>";
            }
            else
            {
                return $"<a href=\"javascript:;\" onClick=\"{showPopWinString}\">转移到子目录</a>";
            }
        }
    }
}
