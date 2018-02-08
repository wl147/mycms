using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BaiRong.Core;
using SiteServer.CMS.Core;

namespace SiteServer.BackgroundPages.Cms
{
    public class PageSiteEdit : BasePageCms
    {
        public TextBox PublishmentSystemArea;
        public TextBox PublishmentSystemName;
        public DropDownList PublishmentSystemType;
        public DropDownList PublishmentSystemCategory;
        public TextBox TelePhone;
        public TextBox Address;
        public TextBox BasicFacts;
        public TextBox Characteristic;
        public TextBox AdministratorAccount;
        public TextBox AdministratorPassWord;
        public DropDownList AdministratorRoles;
        public Button Submit;


        public void Page_Load(object sender, EventArgs e)
        {
            if (IsForbidden) return;

            if (!IsPostBack)
            {
                var currentPublishmentSystemId = Body.GetQueryInt("PublishmentSystemId");
                var publishmentSystemInfo = PublishmentSystemManager.GetPublishmentSystemInfo(currentPublishmentSystemId);
                PublishmentSystemArea.Text = publishmentSystemInfo.AreaId.ToString();
                PublishmentSystemName.Text = publishmentSystemInfo.PublishmentSystemName;
                PublishmentSystemType.Items.Add(new ListItem(publishmentSystemInfo.OrganizationTypeId.ToString(), "0"));
                PublishmentSystemCategory.Items.Add(new ListItem(PublishmentSystemInfo.OrganizationCategory.ToString(), "0"));
                TelePhone.Text = publishmentSystemInfo.TelePhone;
                Address.Text = publishmentSystemInfo.Address;
                BasicFacts.Text = publishmentSystemInfo.BasicFacts;
                Characteristic.Text = publishmentSystemInfo.Characteristic;
                var administratorInfo = BaiRongDataProvider.AdministratorDao.GetByAccount(publishmentSystemInfo.AdministratorAccount);
                AdministratorAccount.Text = administratorInfo.UserName;
                AdministratorPassWord.Text = administratorInfo.Password;
            }
        }


    }
}
