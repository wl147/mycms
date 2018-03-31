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
using System.Data;
using BaiRong.Core.Permissions;
using BaiRong.Core.Model.Enumerations;
using BaiRong.Core.Configuration;
using SiteServer.CMS.Core.Security;
using SiteServer.CMS.Model;

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
        public Repeater rptContents;
        public Repeater rptWebSite;

        public void Page_Load(object sender, EventArgs e)
        {
            var mechanismDao = new MechanismDao();
            if (IsForbidden) return;
            if (!HasWebsitePermissions(AppManager.Cms.Permission.WebSite.SiteEdit))
            {
                FailMessage("无站点修改权限");
                return;
            }
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
                AdministratorAccount.Enabled = false;
                //AdministratorPassWord.Text = administratorInfo.Password;
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

                DataTable dt = new DataTable();
                if (PermissionsManager.GetPermissions(publishmentSystemInfo.AdministratorAccount).IsAdministrator)
                {
                    dt = DataProvider.SystemPermissionsDao.GetAllList();
                }
                else
                {
                    dt = DataProvider.SystemPermissionsDao.GetList(PermissionsManager.GetPermissions(publishmentSystemInfo.AdministratorAccount).Roles);
                }
                var permissions = new ProductAdministratorWithPermissions(PublishmentSystemInfo.AdministratorAccount);
                var channelPermision = permissions.ChannelPermissionDict;
                var webSitePermission = permissions.WebsitePermissionDict;
                rptContents.DataSource = dt;
                rptContents.DataBind();
                for (int i = 0; i < rptContents.Items.Count; i++)
                {
                    string nodeId = ((HiddenField)rptContents.Items[i].FindControl("hidNodeId")).Value;
                    string navName = ((HiddenField)rptContents.Items[i].FindControl("hidName")).Value;
                    CheckBoxList cblActionType = (CheckBoxList)rptContents.Items[i].FindControl("cblActionType");
                    foreach(KeyValuePair<int,List<string>> kv in channelPermision)
                    {
                        if (kv.Key.ToString().Equals(nodeId,StringComparison.OrdinalIgnoreCase))
                        {
                            for (int n = 0; n < cblActionType.Items.Count; n++)
                            {
                                if (kv.Value.Contains(cblActionType.Items[n].Value)) cblActionType.Items[n].Selected = true;
                            }
                        }
                    }                    
                }
                //rptContents.ItemDataBound += rptContents_ItemDataBound;
               
                List<PermissionConfig> webSitePermissions = new List<PermissionConfig>();
                //var currentWebsitePermissions=
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
                for (int i = 0; i < rptWebSite.Items.Count; i++)
                {
                    CheckBoxList cblActionType = (CheckBoxList)rptWebSite.Items[i].FindControl("cblWebSiteType");
                    for (int n = 0; n < cblActionType.Items.Count; n++)
                    {
                        foreach(List<string> list in webSitePermission.Values)
                        {
                            if (list.Contains(cblActionType.Items[n].Value)) cblActionType.Items[n].Selected = true;
                        }
                       
                    }
                                      
                }
            }
        }
        public override void Submit_OnClick(object sender, EventArgs e)
        {
            if (Page.IsPostBack && Page.IsValid)
            {
                var publishmentSystemInfo= PublishmentSystemManager.GetPublishmentSystemInfo( Body.GetQueryInt("PublishmentSystemId"));
                publishmentSystemInfo.PublishmentSystemName = PublishmentSystemName.Text;
                publishmentSystemInfo.Area = PublishmentSystemArea.Text;
                publishmentSystemInfo.OrganizationTypeId = TranslateUtils.ToInt(CblPublishmentSystemType.SelectedValue);
                publishmentSystemInfo.OrganizationCategory = TranslateUtils.ToInt(CblPublishmentSystemCategory.SelectedValue);

                publishmentSystemInfo.TelePhone = TelePhone.Text;
                publishmentSystemInfo.Address = Address.Text;
                publishmentSystemInfo.BasicFacts = BasicFacts.Text;
                publishmentSystemInfo.Characteristic = Characteristic.Text;
                publishmentSystemInfo.AdministratorAccount = AdministratorAccount.Text;
                publishmentSystemInfo.ImageUrl = ImageUrl.Text;
                //更新站点信息
                try
                {
                    DataProvider.PublishmentSystemDao.UpdateAll(PublishmentSystemInfo);                 
                    Body.AddAdminLog("修改站点属性", $"站点:{PublishmentSystemInfo.PublishmentSystemName}");
                    SuccessMessage("站点修改成功！");
                    // AddWaitAndRedirectScript(Sys.PagePublishmentSystem.GetRedirectUrl());
                    // AddWaitAndRedirectScript($@"/siteserver/loading.aspx?RedirectType=Loading&RedirectUrl=cms/siteManagement.aspx?PublishmentSystemID={PublishmentSystemId}");
                    var parentId = DataProvider.PublishmentSystemDao.GetParentId(PublishmentSystemId);
                    AddWaitAndRedirectScript($@"/siteserver/cms/PagePublishmentSystem.aspx?PublishmentSystemID={(parentId==0?1:parentId)}");
                }
                catch (Exception ex)
                {
                    FailMessage(ex, "站点修改失败！");
                }
                //更新管理员信息
                if(!string.IsNullOrEmpty(AdministratorPassWord.Text))
                {
                    var _userName = AdministratorAccount.Text;
                    string errorMessage = string.Empty;

                    if (!string.IsNullOrEmpty(_userName) && BaiRongDataProvider.AdministratorDao.IsUserNameExists(_userName))
                    {
                        try
                        {
                            if (!BaiRongDataProvider.AdministratorDao.ChangePassword(_userName, EPasswordFormat.Encrypted, AdministratorPassWord.Text, out errorMessage))
                            {
                                FailMessage(errorMessage);
                                return;
                            }

                            Body.AddAdminLog("重设管理员密码", $"管理员:{_userName}");
                        }
                        catch
                        {
                            SuccessMessage("修改管理员密码失败！");
                        }                   
                    }
                    else
                    {
                        FailMessage("此站点超级管理员不存在！");
                    }
                }
                //更新角色
                string[]roles=  PermissionsManager.GetPermissions(publishmentSystemInfo.AdministratorAccount).Roles;
                string mainPermission = string.Empty;
                var systemPermissionlist = new List<SystemPermissionsInfo>();
                //获取栏目角色
                int publishmentSystemId = publishmentSystemInfo.PublishmentSystemId;
                string roleName = "superManager_" + publishmentSystemId;
                for (int i = 0; i < rptContents.Items.Count; i++)
                {
                    string nodeId = ((HiddenField)rptContents.Items[i].FindControl("hidNodeId")).Value;
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
                    if (!string.IsNullOrEmpty(channelPermissions)) systemPermissionlist.Add(new SystemPermissionsInfo(roleName, publishmentSystemId, nodeId, channelPermissions, string.Empty));
                }
                //获取站点相关权限角色
                for (int i = 0; i < rptWebSite.Items.Count; i++)
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
                foreach (string role in roles)
                {
                    if (role.StartsWith("superManager_"))
                    {
                        mainPermission = role;
                        break;
                    }
                }

                try
                {
                    DataProvider.PermissionsDao.UpdatePublishmentPermissions(mainPermission, systemPermissionlist);
                }
                catch
                {
                    FailMessage("角色修改失败！");
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
            }
        }
        public void rptWebSite_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string permissionName = ((HiddenField)e.Item.FindControl("hidPermission")).Value;
                CheckBoxList cblWebSiteType = (CheckBoxList)e.Item.FindControl("cblWebSiteType");
                ListItem itemView = new ListItem("浏览", permissionName + "_view");
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
