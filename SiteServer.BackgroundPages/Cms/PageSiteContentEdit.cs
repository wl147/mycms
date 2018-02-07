using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaiRong.Core;
using BaiRong.Core.Model;
using BaiRong.Core.Model.Enumerations;
using BaiRong.Core.Text;
using SiteServer.BackgroundPages.Controls;
using SiteServer.CMS.Core;
using SiteServer.CMS.Core.Create;
using SiteServer.CMS.Model;
using SiteServer.CMS.Model.Enumerations;

namespace SiteServer.BackgroundPages.Cms
{
    public class PagesiteContentEdit : BasePageCms
    {
        public TextBox PublishmentSystemArea;
        public TextBox PublishmentSystemName;
        public DropDownList PublishmentSystemType;
        public DropDownList PublishmentSystemCategory;
        public TextBox TelePhone;
        public TextBox Address;
        public TextBox BasicFacts;
        public TextBox Characteristic;
        public TextEditorControl BasicFactsContent;
        public TextEditorControl CharacteristicContent;
        public Button Submit;


        public void Page_Load(object sender, EventArgs e)
        {
            if (IsForbidden) return;

            if (!IsPostBack)
            {
                var currentPublishmentSystemId = Body.AdministratorInfo.PublishmentSystemId;
                var publishmentSystemInfo = PublishmentSystemManager.GetPublishmentSystemInfo(currentPublishmentSystemId);
                PublishmentSystemName.Text = publishmentSystemInfo.PublishmentSystemName;
                PublishmentSystemType.Items.Add(new ListItem(publishmentSystemInfo.OrganizationTypeId.ToString(), "0"));
                PublishmentSystemCategory.Items.Add(new ListItem(PublishmentSystemInfo.OrganizationCategory.ToString(), "0"));
                TelePhone.Text = publishmentSystemInfo.TelePhone;
                Address.Text = publishmentSystemInfo.Address;
                var administratorInfo = BaiRongDataProvider.AdministratorDao.GetByAccount(publishmentSystemInfo.AdministratorAccount);

                var formCollectionBasicFacts = new NameValueCollection();
                formCollectionBasicFacts[NodeAttribute.Content] = publishmentSystemInfo.BasicFacts;
                BasicFactsContent.SetParameters(PublishmentSystemInfo, NodeAttribute.Content, formCollectionBasicFacts, true, IsPostBack);

                var formCollectionCharacteristicContent = new NameValueCollection();
                formCollectionCharacteristicContent[NodeAttribute.Content] = publishmentSystemInfo.Characteristic;
                BasicFactsContent.SetParameters(PublishmentSystemInfo, NodeAttribute.Content, formCollectionCharacteristicContent, true, IsPostBack);

            }
        }


    }
}