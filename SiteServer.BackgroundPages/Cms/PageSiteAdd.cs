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
using SiteServer.CMS.Core.Security;
using BaiRong.Core.Configuration;
using System.Data;
using BaiRong.Core.Model.Enumerations;

namespace SiteServer.BackgroundPages.Cms
{
   public class PageSiteAdd : BasePageCms
    {
        public TextBox PublishmentSystemArea;
        public RadioButtonList CblPublishmentSystemType;
        public RadioButtonList CblPublishmentSystemCategory;
        public TextBox PublishmentSystemName;
        public TextBox TelePhone;
        public TextBox ImageUrl;
        public TextBox Address;
        public TextBox BasicFacts;
        public TextBox Characteristic;
        public TextBox AdministratorAccount;
        public TextBox AdministratorPassWord;
        public DropDownList AdministratorRoles;
        public Button Submit;
        public Button UploadImage;
        public Repeater rptContents;
        public CheckBoxList ChannelPermissions;
        public Literal ChannelName;


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

            //rptContents.ItemDataBound += rptContents_ItemDataBound;

            var channerPermissions = ProductPermissionsManager.Current.ChannelPermissionDict;
            DataTable dt = DataProvider.SystemPermissionsDao.GetList(ProductPermissionsManager.Current.Roles[1]);
            rptContents.DataSource = dt;
            //rptContents.ItemDataBound += rptContents_ItemDataBound;

            rptContents.DataBind();           

        }


        public override void Submit_OnClick(object sender, EventArgs e)
        {
            SiteServer.CMS.Model.PublishmentSystemInfo newPublishmentSystemInfo = new CMS.Model.PublishmentSystemInfo();
            if (Page.IsPostBack && Page.IsValid)
            {
                newPublishmentSystemInfo.PublishmentSystemName = PublishmentSystemName.Text;
                newPublishmentSystemInfo.Area = PublishmentSystemArea.Text;
                newPublishmentSystemInfo.OrganizationTypeId = TranslateUtils.ToInt(CblPublishmentSystemType.SelectedValue);
                newPublishmentSystemInfo.OrganizationCategory = TranslateUtils.ToInt(CblPublishmentSystemCategory.SelectedValue);
                newPublishmentSystemInfo.TelePhone = TelePhone.Text;
                newPublishmentSystemInfo.Address = Address.Text;
                newPublishmentSystemInfo.BasicFacts = BasicFacts.Text;
                newPublishmentSystemInfo.Characteristic = Characteristic.Text;
                newPublishmentSystemInfo.AdministratorAccount = AdministratorAccount.Text;
                newPublishmentSystemInfo.ImageUrl = ImageUrl.Text;
                newPublishmentSystemInfo.ParentPublishmentSystemId = PublishmentSystemInfo.PublishmentSystemId;
                newPublishmentSystemInfo.ParentsCount = PublishmentSystemInfo.ParentsCount + 1; //PublishmentSystemManager.GetPublishmentSystemLevel(PublishmentSystemInfo.PublishmentSystemId)+1;
                try
                {
                    var thePublishmentSystemId = DataProvider.NodeDao.InsertPublishmentSystemInfo(newPublishmentSystemInfo,PublishmentSystemInfo, Body.AdministratorName);
                    if (thePublishmentSystemId > 0)
                    {
                        Body.AddAdminLog("添加站点属性", $"站点:{PublishmentSystemInfo.PublishmentSystemName}");
                        SuccessMessage("站点添加成功！");
                        // AddWaitAndRedirectScript(Sys.PagePublishmentSystem.GetRedirectUrl());
                        // AddWaitAndRedirectScript($@"/siteserver/loading.aspx?RedirectType=Loading&RedirectUrl=cms/siteManagement.aspx?PublishmentSystemID={PublishmentSystemId}");
                        AddWaitAndRedirectScript($@"/siteserver/cms/PagePublishmentSystem.aspx?PublishmentSystemID={PublishmentSystemId}");
                    }
                    else
                    {
                        FailMessage("站点添加失败！");
                    }                   
                }
                catch (Exception ex)
                {
                    FailMessage(ex, "站点添加失败！");
                }
            }
        }

        public void rptContents_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {


                string[] actionTypeArr = ((HiddenField)e.Item.FindControl("hidActionType")).Value.Split(',');
                CheckBoxList cblActionType = (CheckBoxList)e.Item.FindControl("cblActionType");
                cblActionType.Items.Clear();
                for (int i = 0; i < actionTypeArr.Length; i++)
                {
                    if (EPermissionUtils.ChannelPermissionType().ContainsKey(actionTypeArr[i]))
                    {
                        cblActionType.Items.Add(new ListItem(EPermissionUtils.GetChnanelPermissionText(actionTypeArr[i]) + " ", actionTypeArr[i]));
                    }                                     
                }























                //Repeater rptSub = e.Item.FindControl("rptSubList") as Repeater;
                //var ltlChannelName = e.Item.FindControl("ChannelName") as Literal;
                //var cblChannelPermissions = e.Item.FindControl("ChannelPermissions") as CheckBoxList;             
                //var channelPermission=(KeyValuePair<int,List<string>>)e.Item.DataItem;
                //var nodeInfo = DataProvider.NodeDao.GetNodeInfo(channelPermission.Key);
                //ltlChannelName.Text = nodeInfo.NodeName;
                //foreach (PermissionConfig permission in PermissionConfigManager.Instance.ChannelPermissions)
                //{
                //    // ChannelPermissionsPlaceHolder.Visible = true;
                //    if (!string.IsNullOrEmpty(permission.Name))
                //    {
                //        if (permission.Name == "cms_contentTranslate") break;
                //        var listItem = new ListItem(permission.Text, permission.Name);
                //        cblChannelPermissions.Items.Add(listItem);
                //    }                    
                //}
                //var nodeInfo = DataProvider.NodeDao.GetNodeInfo(nodeId);
                //ChannelName.Text = nodeInfo.NodeName;
                //foreach (var nodeId in channerPermissions.Keys)
                //{
                //    var nodeInfo = DataProvider.NodeDao.GetNodeInfo(nodeId);
                //    ChannelName.Text = nodeInfo.NodeName;
                //    foreach (PermissionConfig permission in PermissionConfigManager.Instance.ChannelPermissions)
                //    {
                //        // ChannelPermissionsPlaceHolder.Visible = true;
                //        if (permission.Name == "cms_contentTranslate") break;
                //        var listItem = new ListItem(permission.Text, permission.Name);
                //        ChannelPermissions.Items.Add(listItem);
                //    }
                //}
                //var contentInfo = new ContentInfo(e.Item.DataItem);

                //ltlItemTitle.Text = WebUtils.GetContentTitle(PublishmentSystemInfo, contentInfo, PageUrl);

                //var showPopWinString = ModalCheckState.GetOpenWindowString(PublishmentSystemId, contentInfo, PageUrl);

                //ltlItemStatus.Text =
                //    $@"<a href=""javascript:;"" title=""设置内容状态"" onclick=""{showPopWinString}"">{LevelManager.GetCheckState(
                //        PublishmentSystemInfo, contentInfo.IsChecked, contentInfo.CheckedLevel)}</a>";

                //if (HasChannelPermissions(contentInfo.NodeId, AppManager.Cms.Permission.Channel.ContentEdit) || Body.AdministratorName == contentInfo.AddUserName)
                //{
                //    ltlItemEditUrl.Text =
                //        $"<a href=\"{WebUtils.GetContentAddEditUrl(PublishmentSystemId, nodeInfo, contentInfo.Id, PageUrl)}\">编辑</a>";
                //}

                //ltlColumnItemRows.Text = TextUtility.GetColumnItemRowsHtml(styleInfoList, attributesOfDisplay, valueHashtable, tableStyle, PublishmentSystemInfo, contentInfo);

                //ltlCommandItemRows.Text = TextUtility.GetCommandItemRowsHtml(tableStyle, PublishmentSystemInfo, nodeInfo, contentInfo, PageUrl, Body.AdministratorName);
            }
        }
    }
}
