using System;
using System.Text;
using System.Web.UI;
using BaiRong.Core;
using SiteServer.BackgroundPages.Core;
using SiteServer.CMS.Core;
using SiteServer.CMS.Core.Security;
using SiteServer.CMS.Model;
using SiteServer.CMS.Model.Enumerations;

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
                    var publishmentSystemIdList = PublishmentSystemManager.GetPublishmentSystemIdListByParentId(publishmentSystemId);

                    //var nodeIdList = DataProvider.NodeDao.GetNodeIdListByParentId(_publishmentSystemInfo.PublishmentSystemId, 0);
                    foreach (var publishmentSystem in publishmentSystemIdList)
                    {
                        var publishmentSystemInfo= PublishmentSystemManager.GetPublishmentSystemInfo(publishmentSystem);
                        //var nodeInfo = NodeManager.GetNodeInfo(_publishmentSystemInfo.PublishmentSystemId, nodeId);
                        //var enabled = AdminUtility.IsOwningNodeId(body.AdministratorName, nodeInfo.NodeId);
                        //var enabled2 = AdminUtility.IsOwningNodeIdByPublishmentSystem(body.AdministratorName, nodeInfo.NodeId);
                        //if (!enabled)
                        //{
                        //    if (!AdminUtility.IsHasChildOwningNodeId(body.AdministratorName, nodeInfo.NodeId)) continue;
                        //}
                        //if (nodeId == 1)
                        //{
                        //    builder.Append(
                        //        @"<a href="" /siteserver/loading.aspx?RedirectType=Loading&amp;RedirectUrl=cms/pageContentMain.aspx?PublishmentSystemID=1""target=""right""onclick=""openFolderByA(this); ""istreelink=""true"" style=""padding-left:20px;font-size:30px;"">内容管理</a>");
                        //}
                        //else
                        //{
                        builder.Append(SiteLoading.GetChannelRowHtml(_publishmentSystemInfo, true, ELoadingType.ContentTree, null, body.AdministratorName));
                        //}                          
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