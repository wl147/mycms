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
    public class PageRoleAdd : BasePageCms
    {
        public TextBox TbRoleName;
        public TextBox TbDescription;
        public Repeater rptContents;
        public Repeater rptWebSite;



        public void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataTable dt = new DataTable();
                if (PermissionsManager.GetPermissions(Body.AdministratorName).IsConsoleAdministrator)
                {
                    dt = DataProvider.SystemPermissionsDao.GetAllList();
                }
                else
                {
                    dt = DataProvider.SystemPermissionsDao.GetList(PermissionsManager.GetPermissions(Body.AdministratorInfo.UserName).Roles);
                }

                rptContents.DataSource = dt;
                rptContents.DataBind();
                List<PermissionConfig> webSitePermissions = new List<PermissionConfig>();
                foreach (PermissionConfig permission in PermissionConfigManager.Instance.WebsitePermissions)
                {
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
            if (Page.IsPostBack && Page.IsValid)
            {
                var systemPermissionlist = new List<SystemPermissionsInfo>();
                int publishmentSystemId = PublishmentSystemId;
                string description = TbDescription.Text;
                string displayRoleName = TbRoleName.Text;
                string insertRoleName = publishmentSystemId + "_" + displayRoleName;
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
                    if (!string.IsNullOrEmpty(channelPermissions)) systemPermissionlist.Add(new SystemPermissionsInfo(insertRoleName, publishmentSystemId, nodeId, channelPermissions, string.Empty));
                }
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
                    if (!string.IsNullOrEmpty(webSitePermissions)) systemPermissionlist.Add(new SystemPermissionsInfo(insertRoleName, publishmentSystemId, string.Empty, string.Empty, webSitePermissions));
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

                BaiRongDataProvider.RoleDao.InsertRole(insertRoleName, Body.AdministratorName, description, publishmentSystemId);
                Body.AddAdminLog("添加角色成功", $"角色名称:{displayRoleName}");
            }
            SuccessMessage("角色添加成功！");
            AddWaitAndRedirectScript($@"/siteserver/cms/PageRoleAdd.aspx?PublishmentSystemID={PublishmentSystemId}");
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
                        ListItem item = new ListItem(EPermissionUtils.GetChnanelPermissionText(actionTypeArr[i]) + " ", actionTypeArr[i]);
                        //item.Selected = true;
                        cblActionType.Items.Add(item);
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
