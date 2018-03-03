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
    public class PagesiteContentEdit:BasePageCms
    {
        public TextBox PublishmentSystemArea;
        public Label PublishmentSystemName;
        public Label PublishmentSystemType;
        public TextBox ImageUrl;
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
                ImageUrl.Text = publishmentSystemInfo.ImageUrl;
                PublishmentSystemType.Text=DataProvider.MechanismDao.GetMechanismTypeTextById(PublishmentSystemInfo.OrganizationTypeId)+"|"+ DataProvider.MechanismDao.GetMechanismCategoryTextById(PublishmentSystemInfo.OrganizationCategory);
                TelePhone.Text = publishmentSystemInfo.TelePhone;
                Address.Text = publishmentSystemInfo.Address;
                BasicFacts.Text = publishmentSystemInfo.BasicFacts;
                Characteristic.Text = publishmentSystemInfo.Characteristic;
                //    var administratorInfo = BaiRongDataProvider.AdministratorDao.GetByAccount(publishmentSystemInfo.AdministratorAccount);
                //try
                //{
                //    var formCollectionBasicFacts = new NameValueCollection();
                //    formCollectionBasicFacts[NodeAttribute.Content] = publishmentSystemInfo.BasicFacts;
                //    BasicFactsContent.SetParameters(new PublishmentSystemInfo(), "Content", new NameValueCollection(), true, false);

                //    var formCollectionCharacteristicContent = new NameValueCollection();
                //    formCollectionCharacteristicContent[NodeAttribute.Content] = publishmentSystemInfo.Characteristic;
                //    BasicFactsContent.SetParameters(PublishmentSystemInfo, NodeAttribute.Content, formCollectionCharacteristicContent, true, IsPostBack);
                //}
                //catch(Exception ex)
                //{
                //}


            }
            }
        public override void Submit_OnClick(object sender, EventArgs e)
        {
            if (Page.IsPostBack && Page.IsValid)
            {

                PublishmentSystemInfo.TelePhone = TelePhone.Text;
                PublishmentSystemInfo.Address = Address.Text;
                PublishmentSystemInfo.BasicFacts = BasicFacts.Text;
                PublishmentSystemInfo.Characteristic = Characteristic.Text;
                PublishmentSystemInfo.ImageUrl = ImageUrl.Text;
                try
                {
                    DataProvider.PublishmentSystemDao.UpdateAll(PublishmentSystemInfo);
                    Body.AddAdminLog("保存基本信息属性", $"站点:{PublishmentSystemInfo.PublishmentSystemName}");
                    SuccessMessage("站点基本信息保存成功！");
                    // AddWaitAndRedirectScript(Sys.PagePublishmentSystem.GetRedirectUrl());
                    // AddWaitAndRedirectScript($@"/siteserver/loading.aspx?RedirectType=Loading&RedirectUrl=cms/siteManagement.aspx?PublishmentSystemID={PublishmentSystemId}");
                    //AddWaitAndRedirectScript($@"/siteserver/cms/PagePublishmentSystem.aspx?PublishmentSystemID={PublishmentSystemId}");
                }
                catch (Exception ex)
                {
                    FailMessage(ex, "基本信息保存失败！");
                }
            }
        }


    }
}