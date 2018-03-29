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
    public class PageRoleEdit : BasePageCms
    {
        public TextBox TbRoleName;
        public TextBox TbDescription;
        public Repeater rptContents;
        public Repeater rptWebSite;

        public void Page_Load(object sender, EventArgs e)
        {
            if (IsForbidden) return;
            var roleName= Body.GetQueryString("RoleName");
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(roleName)) PageUtils.RedirectToErrorPage("参数错误!");
                TbRoleName.Text = roleName;
                TbRoleName.Enabled = false;
                TbDescription.Text = BaiRongDataProvider.RoleDao.GetRoleDescription(roleName);

                DataTable dt = new DataTable();
                //if (PermissionsManager.GetPermissions(PublishmentSystemInfo.AdministratorAccount).IsAdministrator)
                //{
                    dt = DataProvider.SystemPermissionsDao.GetAllList();
                //}
                //else
                //{
                //    dt = DataProvider.SystemPermissionsDao.GetList(PermissionsManager.GetPermissions(PublishmentSystemInfo.AdministratorAccount).Roles);
                //}
                var channelPermision = DataProvider.SystemPermissionsDao.GetChannelPermissionSortedList(new string[] { roleName});
                var webSitePermission = DataProvider.SystemPermissionsDao.GetWebsitePermissionSortedList(new string[] { roleName });
                rptContents.DataSource = dt;
                rptContents.DataBind();
                for (int i = 0; i < rptContents.Items.Count; i++)
                {
                    string nodeId = ((HiddenField)rptContents.Items[i].FindControl("hidNodeId")).Value;
                    //string navName = ((HiddenField)rptContents.Items[i].FindControl("hidName")).Value;
                    CheckBoxList cblActionType = (CheckBoxList)rptContents.Items[i].FindControl("cblActionType");
                    foreach (KeyValuePair<int, List<string>> kv in channelPermision)
                    {
                        if (kv.Key.ToString().Equals(nodeId, StringComparison.OrdinalIgnoreCase))
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
                        foreach (List<string> list in webSitePermission.Values)
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

                var roleName = Body.GetQueryString("RoleName");
                var systemPermissionlist = new List<SystemPermissionsInfo>();
                //获取栏目角色
                int publishmentSystemId = 0;// publishmentSystemInfo.PublishmentSystemId;
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
              
                try
                {
                    DataProvider.PermissionsDao.UpdatePublishmentPermissions(roleName, systemPermissionlist);
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
