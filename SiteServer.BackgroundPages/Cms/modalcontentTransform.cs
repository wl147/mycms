using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web.UI.WebControls;
using BaiRong.Core;
using BaiRong.Core.Model;
using SiteServer.CMS.Core;
using SiteServer.CMS.Core.Create;
using SiteServer.CMS.Core.User;
using SiteServer.CMS.Model;

namespace SiteServer.BackgroundPages.Cms
{
    public class ModalContentTransform : BasePageCms
    {
        public Literal ltlCurrentOrganizationName;
        public Literal ltlReachOrganizationName;


        public void Page_Load(object sender, EventArgs e)
        {
            if (IsForbidden) return;

            PageUtils.CheckRequestParameter("ReachPublishmentSystemId", "ReturnUrl", "PublishmentSystemId");
            var currentName = PublishmentSystemInfo.PublishmentSystemName;
            var reachId = Body.GetQueryInt("ReachPublishmentSystemId");
            if (reachId != 0)
            {
                var reachPublishmentSystemInfo = PublishmentSystemManager.GetPublishmentSystemInfo(reachId);
                ltlCurrentOrganizationName.Text = currentName;
                ltlReachOrganizationName.Text = reachPublishmentSystemInfo.PublishmentSystemName;
            }
            else
            {
                PageUtils.RedirectToErrorPage("到达组织机构参数有误");
            }

        }

        public override void Submit_OnClick(object sender, EventArgs e)
        {
            ReachInfo reachInfo = new ReachInfo();            
            var reachId = Body.GetQueryInt("ReachPublishmentSystemId");
            reachInfo.OrganizationId = PublishmentSystemId;
            reachInfo.TelePhone = "";
            reachInfo.ReachTime = DateTime.Now;
            reachInfo.ReachOrganizationId = reachId;
            if (DataProvider.PublishmentSystemDao.InsertOrganizationReach(reachInfo))
            {
                PageUtils.CloseModalPageAndRedirect(Page, $@"/siteserver/cms/pageReachDetails.aspx?PublishmentSystemId={PublishmentSystemId}");
            }
            else
            {
                PageUtils.RedirectToErrorPage("报道失败！");
            }

            //

        }
    }
}
