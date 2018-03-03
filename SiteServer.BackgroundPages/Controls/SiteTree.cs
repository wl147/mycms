using System;
using System.Text;
using System.Web.UI;
using BaiRong.Core;
using SiteServer.BackgroundPages.Core;
using SiteServer.CMS.Core;
using SiteServer.CMS.Core.Security;
using SiteServer.CMS.Model;
using SiteServer.CMS.Model.Enumerations;
using System.Collections.Generic;

namespace SiteServer.BackgroundPages.Controls
{
    public class SiteTree : Control
    {
        private PublishmentSystemInfo _publishmentSystemInfo;

        protected override void Render(HtmlTextWriter writer)
        {
            var builder = new StringBuilder();

            var body = new RequestBody();

            var publishmentSystemId = body.AdministratorInfo.PublishmentSystemId;
            _publishmentSystemInfo = PublishmentSystemManager.GetPublishmentSystemInfo(publishmentSystemId);
            var scripts = SiteLoading.GetScript(_publishmentSystemInfo, ELoadingType.ContentTree, null);
            builder.Append(scripts);
            if (Page.Request.QueryString["PublishmentSystemID"] != null)
            {
                try
                {                   
                    var  publishmentSystemIdList = new List<int>() ;
                    publishmentSystemIdList.Add(publishmentSystemId);
                    publishmentSystemIdList.AddRange(PublishmentSystemManager.GetPublishmentSystemIdListByParentId(publishmentSystemId));
                    foreach (var publishmentSystem in publishmentSystemIdList)
                    {
                        var publishmentSystemInfo= PublishmentSystemManager.GetPublishmentSystemInfo(publishmentSystem);                      
                        builder.Append(SiteLoading.GetSiteRowHtml(publishmentSystemInfo, publishmentSystemId));                       
                    }
                }
                catch (Exception ex)
                {
                    PageUtils.RedirectToErrorPage(ex.Message);
                }
            }
            writer.Write(builder);
        }
    }
}