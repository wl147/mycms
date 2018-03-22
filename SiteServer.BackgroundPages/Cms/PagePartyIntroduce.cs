using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BaiRong.Core;
using BaiRong.Core.AuxiliaryTable;
using BaiRong.Core.Model;
using BaiRong.Core.Model.Attributes;
using BaiRong.Core.Model.Enumerations;
using BaiRong.Core.Text;
using SiteServer.BackgroundPages.Ajax;
using SiteServer.BackgroundPages.Controls;
using SiteServer.BackgroundPages.Core;
using SiteServer.CMS.Core;
using SiteServer.CMS.Core.Create;
using SiteServer.CMS.Core.Office;
using SiteServer.CMS.Core.Security;
using SiteServer.CMS.Core.User;
using SiteServer.CMS.Model;
using SiteServer.CMS.Model.Enumerations;
using System.Linq;

namespace SiteServer.BackgroundPages.Cms
{
    public class PagePartyIntroduce : BasePageCms
    {
        public TextBox Content;

        public static string GetRedirectUrlOfAdd(int publishmentSystemId, int nodeId, string returnUrl)
        {
            return PageUtils.GetCmsUrl(nameof(PageContentAdd), new NameValueCollection
            {
                {"PublishmentSystemID", publishmentSystemId.ToString()},
                {"NodeID", nodeId.ToString()},
                {"ReturnUrl", StringUtils.ValueToUrl(returnUrl)}
            });
        }

        public static string GetRedirectUrlOfEdit(int publishmentSystemId, int nodeId, int id, string returnUrl)
        {
            return PageUtils.GetCmsUrl(nameof(PageContentAdd), new NameValueCollection
            {
                {"PublishmentSystemID", publishmentSystemId.ToString()},
                {"NodeID", nodeId.ToString()},
                {"ID", id.ToString()},
                {"ReturnUrl", StringUtils.ValueToUrl(returnUrl)}
            });
        }

        public void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Content.Text = DataProvider.PublishmentSystemDao.GetPublishmentSystemPartyIntroduce(PublishmentSystemInfo.PublishmentSystemId);
            }
        }
        public override void Submit_OnClick(object sender, EventArgs e)
        {
            if (Page.IsPostBack && Page.IsValid)
            {
              var a=  Content.Text;
                DataProvider.PublishmentSystemDao.UpdatePublishmentSystemPartyIntroduce(PublishmentSystemInfo.PublishmentSystemId, Content.Text);
                SuccessMessage("党委信息修改成功！");
            }
        }
    }
}
