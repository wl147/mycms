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
using BaiRong.Core.Permissions;
using SiteServer.CMS.Model;
using BaiRong.Core.Model;

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
        public Repeater rptWebSite;
        public CheckBoxList ChannelPermissions;
        public CheckBoxList cblWebSiteType;
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
            if (!IsPostBack)
            {
                var typeDic = DataProvider.MechanismDao.GetMechanismTypeAll();
                var categoryDic = DataProvider.MechanismDao.GetMechanismCategoryAll();
                foreach (var type in typeDic)
                {
                    var listItem = new ListItem(type.Value, type.Key.ToString());
                    CblPublishmentSystemType.Items.Add(listItem);
                }
                foreach (var category in categoryDic)
                {
                    var listItem = new ListItem(category.Value, category.Key.ToString());
                    CblPublishmentSystemCategory.Items.Add(listItem);
                }

                var showPopWinString = ModalUploadImage.GetOpenWindowString(1, "NavigationPicPath");
                UploadImage.Attributes.Add("onclick", showPopWinString);

                //rptContents.ItemDataBound += rptContents_ItemDataBound;

                var channerPermissions = ProductPermissionsManager.Current.ChannelPermissionDict;
                DataTable dt = new DataTable();
                if (PermissionsManager.GetPermissions(Body.AdministratorName).IsSystemAdministrator)
                {
                    dt = DataProvider.SystemPermissionsDao.GetAllList();
                }
                else
                {
                    dt = DataProvider.SystemPermissionsDao.GetList(PermissionsManager.GetPermissions(Body.AdministratorInfo.UserName).Roles);
                }

                rptContents.DataSource = dt;
                //rptContents.ItemDataBound += rptContents_ItemDataBound;

                rptContents.DataBind();
                List<PermissionConfig> webSitePermissions = new List<PermissionConfig>();
                foreach (PermissionConfig permission in PermissionConfigManager.Instance.WebsitePermissions)
                {
                    //if (permission.Name == websitePermission)
                    //{
                    //    WebsitePermissionsPlaceHolder.Visible = true;
                    //    var listItem = new ListItem(permission.Text, permission.Name);
                    //    WebsitePermissions.Items.Add(listItem);
                    //}
                    if (StringUtils.EqualsIgnoreCase(permission.Type, "party"))
                    {
                        webSitePermissions.Add(permission);
                    }
                }
                rptWebSite.DataSource = webSitePermissions;
                rptWebSite.DataBind();
            }
           
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
                newPublishmentSystemInfo.TelePhone = TelePhone.Text.Trim();
                newPublishmentSystemInfo.Address = Address.Text;
                newPublishmentSystemInfo.BasicFacts = BasicFacts.Text;
                newPublishmentSystemInfo.Characteristic = Characteristic.Text;
                newPublishmentSystemInfo.AdministratorAccount = AdministratorAccount.Text.Trim();
                newPublishmentSystemInfo.ImageUrl = ImageUrl.Text;
                newPublishmentSystemInfo.ParentPublishmentSystemId = PublishmentSystemInfo.PublishmentSystemId;
                newPublishmentSystemInfo.ParentsCount = PublishmentSystemInfo.ParentsCount + 1; //PublishmentSystemManager.GetPublishmentSystemLevel(PublishmentSystemInfo.PublishmentSystemId)+1;


                int thePublishmentSystemId = 0;

                try
                {
                    thePublishmentSystemId = DataProvider.NodeDao.InsertPublishmentSystemInfo(newPublishmentSystemInfo, PublishmentSystemInfo, Body.AdministratorName);
                    
                }
                catch (Exception ex)
                {
                    FailMessage(ex, "站点添加失败！");
                }
                if (thePublishmentSystemId > 0)//添加站点成功
                {
                    var systemPermissionlist = new List<SystemPermissionsInfo>();
                    int publishmentSystemId = thePublishmentSystemId;
                    string roleName = "superManager_" + publishmentSystemId; 
                    for (int i = 0; i < rptContents.Items.Count; i++)
                    {
                        string nodeId = ((HiddenField)rptContents.Items[i].FindControl("hidName")).Value;
                        //int publishmentSystemId = int.Parse(((HiddenField)rptContents.Items[i].FindControl("hidPublishmentSystemId")).Value);                                           
                        string channelPermissions = string.Empty;
                        CheckBoxList cblActionType = (CheckBoxList)rptContents.Items[i].FindControl("cblActionType");
                        for (int n = 0; n < cblActionType.Items.Count; n++)
                        {
                            if (cblActionType.Items[n].Selected == true)
                            {
                                channelPermissions = channelPermissions + cblActionType.Items[n].Value + ",";
                            }
                        }
                        channelPermissions = channelPermissions.TrimEnd(',');
                        if(!string.IsNullOrEmpty(channelPermissions)) systemPermissionlist.Add(new SystemPermissionsInfo(roleName, publishmentSystemId, nodeId, channelPermissions, string.Empty));
                    }
                    for(int i=0;i< rptWebSite.Items.Count; i++)
                    {
                        string webPermission = ((HiddenField)rptWebSite.Items[i].FindControl("hidPermission")).Value;
                        //int publishmentSystemId = int.Parse(((HiddenField)rptContents.Items[i].FindControl("hidPublishmentSystemId")).Value);                                           
                        string webSitePermissions = string.Empty;
                        CheckBoxList cblActionType = (CheckBoxList)rptWebSite.Items[i].FindControl("cblWebSiteType");
                        for (int n = 0; n < cblActionType.Items.Count; n++)
                        {
                            if (cblActionType.Items[n].Selected == true)
                            {
                                webSitePermissions = webSitePermissions + cblActionType.Items[n].Value + ",";
                            }
                        }
                        webSitePermissions = webSitePermissions.TrimEnd(',');
                        if (!string.IsNullOrEmpty(webSitePermissions)) systemPermissionlist.Add(new SystemPermissionsInfo(roleName, publishmentSystemId, string.Empty, string.Empty, webSitePermissions));
                    }
                    try
                    {
                        DataProvider.PermissionsDao.InsertRoleAndPermissionsParty(systemPermissionlist);//更新权限角色表
                        PermissionsManager.ClearAllCache();
                        SuccessMessage("角色添加成功！");
                    }
                    catch (Exception ex)
                    {
                        FailMessage(ex, $"角色添加失败，{ex.Message}");                       
                    }
                    if (!string.IsNullOrEmpty(newPublishmentSystemInfo.AdministratorAccount))//创建管理员
                    {
                        var adminInfo = new AdministratorInfo
                        {
                            UserName = newPublishmentSystemInfo.AdministratorAccount,
                            Password = AdministratorPassWord.Text.Trim(),
                            CreatorUserName = Body.AdministratorName,
                            DisplayName = newPublishmentSystemInfo.AdministratorAccount,
                            Mobile = newPublishmentSystemInfo.TelePhone,
                            PublishmentSystemId = thePublishmentSystemId
                        };

                        if (!string.IsNullOrEmpty(BaiRongDataProvider.AdministratorDao.GetUserNameByMobile(newPublishmentSystemInfo.TelePhone)))
                        {
                            FailMessage("管理员添加失败，手机号码已存在");
                            return;
                        }

                        string errorMessage;
                        if (!AdminManager.CreateAdministrator(adminInfo, out errorMessage))
                        {
                            FailMessage($"管理员添加失败：{errorMessage}");
                            return;
                        }

                        Body.AddAdminLog("添加管理员", $"管理员:{newPublishmentSystemInfo.AdministratorAccount}");
                        SuccessMessage("管理员添加成功！");
                    }
                    BaiRongDataProvider.RoleDao.InsertRole(roleName, Body.AdministratorName, "supermanager_"+ newPublishmentSystemInfo.PublishmentSystemName,thePublishmentSystemId);
                    BaiRongDataProvider.RoleDao.AddUserToRole(newPublishmentSystemInfo.AdministratorAccount, roleName);
                }
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
                        ListItem item =new ListItem(EPermissionUtils.GetChnanelPermissionText(actionTypeArr[i]) + " ", actionTypeArr[i]);
                        //item.Selected = true;
                        cblActionType.Items.Add(item);
                    }
                }
            }
        }
        public void rptWebSite_ItemDataBound(object sender,RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string permissionName = ((HiddenField)e.Item.FindControl("hidPermission")).Value;
                CheckBoxList cblWebSiteType = (CheckBoxList)e.Item.FindControl("cblWebSiteType");
                ListItem itemView = new ListItem("浏览", permissionName+"_view");
                ListItem itemAdd = new ListItem("添加", permissionName + "_add");
                ListItem itemEdit = new ListItem("修改", permissionName + "_edit");
                ListItem itemDelete = new ListItem("删除", permissionName + "_delete");
                cblWebSiteType.Items.Add(itemView);
                cblWebSiteType.Items.Add(itemAdd);
                cblWebSiteType.Items.Add(itemEdit);
                cblWebSiteType.Items.Add(itemDelete);
            }
        }
        
    }
}
