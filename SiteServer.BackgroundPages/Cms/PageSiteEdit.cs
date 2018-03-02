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
using SiteServer.CMS.Provider;

namespace SiteServer.BackgroundPages.Cms
{
    public class PageSiteEdit : BasePageCms
    {
        public TextBox PublishmentSystemArea;
        public TextBox PublishmentSystemName;
        public DropDownList PublishmentSystemCategory;
        public TextBox TelePhone;
        public TextBox Address;
        public TextBox BasicFacts;
        public TextBox Characteristic;
        public TextBox AdministratorAccount;
        public TextBox AdministratorPassWord;
        public TextBox ImageUrl;
        public DropDownList AdministratorRoles;
        public Button Submit;
        public RadioButtonList CblPublishmentSystemType;
        public RadioButtonList CblPublishmentSystemCategory;


        public void Page_Load(object sender, EventArgs e)
        {
            var mechanismDao = new MechanismDao();
            if (IsForbidden) return;

            if (!IsPostBack)
            {
                var currentPublishmentSystemId = Body.GetQueryInt("PublishmentSystemId");
                var publishmentSystemInfo = PublishmentSystemManager.GetPublishmentSystemInfo(currentPublishmentSystemId);
                PublishmentSystemArea.Text = publishmentSystemInfo.Area;
                PublishmentSystemName.Text = publishmentSystemInfo.PublishmentSystemName;
                //PublishmentSystemType.Items.Add(new ListItem(mechanismDao.GetMechanismTypeTextById(PublishmentSystemInfo.OrganizationTypeId), PublishmentSystemInfo.OrganizationTypeId.ToString()));
                //PublishmentSystemCategory.Items.Add(new ListItem((mechanismDao.GetMechanismCategoryTextById(PublishmentSystemInfo.OrganizationCategory)), PublishmentSystemInfo.OrganizationCategory.ToString()));
                TelePhone.Text = publishmentSystemInfo.TelePhone;
                Address.Text = publishmentSystemInfo.Address;
                BasicFacts.Text = publishmentSystemInfo.BasicFacts;
                ImageUrl.Text = publishmentSystemInfo.ImageUrl;
                Characteristic.Text = publishmentSystemInfo.Characteristic;
                var administratorInfo = BaiRongDataProvider.AdministratorDao.GetByAccount(publishmentSystemInfo.AdministratorAccount);
                AdministratorAccount.Text = administratorInfo.UserName;
                AdministratorPassWord.Text = administratorInfo.Password;
                var typeDic = DataProvider.MechanismDao.GetMechanismTypeAll();
                var categoryDic = DataProvider.MechanismDao.GetMechanismCategoryAll();
                foreach (var type in typeDic)
                {
                    var listItem = new ListItem(type.Value, type.Key.ToString());
                    if(type.Key== publishmentSystemInfo.OrganizationTypeId)
                    {
                        listItem.Selected = true;
                    }
                    CblPublishmentSystemType.Items.Add(listItem);
                }
                foreach (var category in categoryDic)
                {
                    var listItem = new ListItem(category.Value, category.Key.ToString());
                    if (category.Key == publishmentSystemInfo.OrganizationCategory)
                    {
                        listItem.Selected = true;
                    }
                    CblPublishmentSystemCategory.Items.Add(listItem);
                }
            }
        }
        public override void Submit_OnClick(object sender, EventArgs e)
        {
            if (Page.IsPostBack && Page.IsValid)
            {
                PublishmentSystemInfo.PublishmentSystemName = PublishmentSystemName.Text;
                PublishmentSystemInfo.Area = PublishmentSystemArea.Text;
                PublishmentSystemInfo.OrganizationTypeId = TranslateUtils.ToInt(CblPublishmentSystemType.SelectedValue);
                PublishmentSystemInfo.OrganizationCategory = TranslateUtils.ToInt(CblPublishmentSystemCategory.SelectedValue);

                PublishmentSystemInfo.TelePhone = TelePhone.Text;
                PublishmentSystemInfo.Address = Address.Text;
                PublishmentSystemInfo.BasicFacts = BasicFacts.Text;
                PublishmentSystemInfo.Characteristic = Characteristic.Text;
                PublishmentSystemInfo.AdministratorAccount = AdministratorAccount.Text;
                PublishmentSystemInfo.ImageUrl = ImageUrl.Text;
                try
                {
                    DataProvider.PublishmentSystemDao.UpdateAll(PublishmentSystemInfo);                 
                    Body.AddAdminLog("修改站点属性", $"站点:{PublishmentSystemInfo.PublishmentSystemName}");
                    SuccessMessage("站点修改成功！");
                   // AddWaitAndRedirectScript(Sys.PagePublishmentSystem.GetRedirectUrl());
                   // AddWaitAndRedirectScript($@"/siteserver/loading.aspx?RedirectType=Loading&RedirectUrl=cms/siteManagement.aspx?PublishmentSystemID={PublishmentSystemId}");
                    AddWaitAndRedirectScript($@"/siteserver/cms/PagePublishmentSystem.aspx?PublishmentSystemID={PublishmentSystemId}");
                }
                catch (Exception ex)
                {
                    FailMessage(ex, "站点修改失败！");
                }
            }
        }


    }
}
