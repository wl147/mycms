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
   public class PageSiteAdd : BasePageCms
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
        public Button UploadImage;
        public RadioButtonList CblPublishmentSystemType;
        public RadioButtonList CblPublishmentSystemCategory;

        public void Page_Load(object sender, EventArgs e)
        {
            //var excludeAttributeNames = BaiRong.Core.AuxiliaryTable.TableManager.GetExcludeAttributeNames(_tableStyle);
            //AcAttributes.AddExcludeAttributeNames(excludeAttributeNames);

            //if (excludeAttributeNames.Count == 0)
            //{
            //    PhContentAttributes.Visible = false;
            //}
            //else
            //{
            //    PhContentAttributes.Visible = true;
            //    foreach (var attributeName in excludeAttributeNames)
            //    {
            //        var styleInfo = BaiRong.Core.AuxiliaryTable.TableStyleManager.GetTableStyleInfo(_tableStyle, _tableName, attributeName, _relatedIdentities);
            //        if (styleInfo.IsVisible)
            //        {
            //            var listItem = new ListItem(styleInfo.DisplayName, styleInfo.AttributeName);
            //            if (contentId > 0)
            //            {
            //                listItem.Selected = TranslateUtils.ToBool(contentInfo?.GetExtendedAttribute(styleInfo.AttributeName));
            //            }
            //            else
            //            {
            //                if (TranslateUtils.ToBool(styleInfo.DefaultValue))
            //                {
            //                    listItem.Selected = true;
            //                }
            //            }
            //            CblContentAttributes.Items.Add(listItem);
            //        }
            //    }
            //}
            var typeDic = DataProvider.MechanismDao.GetMechanismTypeAll();
            var categoryDic = DataProvider.MechanismDao.GetMechanismCategoryAll();
            foreach (var type in typeDic)
            {
                var listItem = new ListItem( type.Value,type.Key.ToString());
                CblPublishmentSystemType.Items.Add(listItem);
            }
            foreach (var category in categoryDic)
            {
                var listItem = new ListItem(category.Value,category.Key.ToString());
                CblPublishmentSystemCategory.Items.Add(listItem);
            }

            var showPopWinString = ModalUploadImage.GetOpenWindowString(1, "NavigationPicPath");
            UploadImage.Attributes.Add("onclick", showPopWinString);

        }
    }
}
