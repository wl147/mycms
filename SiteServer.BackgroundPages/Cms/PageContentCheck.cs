using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using BaiRong.Core;
using BaiRong.Core.AuxiliaryTable;
using BaiRong.Core.Model;
using BaiRong.Core.Model.Attributes;
using BaiRong.Core.Model.Enumerations;
using BaiRong.Core.Permissions;
using SiteServer.BackgroundPages.Controls;
using SiteServer.BackgroundPages.Core;
using SiteServer.CMS.Core;
using SiteServer.CMS.Core.Security;
using SiteServer.CMS.Core.User;
using SiteServer.CMS.Model;

namespace SiteServer.BackgroundPages.Cms
{
    public class PageContentCheck : BasePageCms
    {
        public RadioButtonList State;
        public PlaceHolder PhContentModel;
        public DropDownList DdlContentModelId;

        public Repeater RptContents;
        public SqlPager SpContents;
        public Literal LtlColumnHeadRows;
        public Literal LtlCommandHeadRows;

        public Button BtnCheck;
        public Button BtnDelete;

        private StringCollection _attributesOfDisplay;
        private List<TableStyleInfo> _tableStyleInfoList;
        private List<int> _relatedIdentities;
        private ETableStyle _tableStyle;
        private NodeInfo _nodeInfo;
        private string _tableName;
        private bool _isGovPublic;
        private readonly Hashtable _valueHashtable = new Hashtable();

        public static string GetRedirectUrl(int publishmentSystemId)
        {
            return PageUtils.GetCmsUrl(nameof(PageContentCheck), new NameValueCollection
            {
                {"PublishmentSystemID", publishmentSystemId.ToString()}
            });
        }

        public void Page_Load(object sender, EventArgs e)
        {
            if (IsForbidden) return;

            PageUtils.CheckRequestParameter("PublishmentSystemID");

            var permissions = PermissionsManager.GetPermissions(Body.AdministratorName);
            var nodeId = PublishmentSystemId;
            _isGovPublic = Body.GetQueryBool("IsGovPublic");
            if (_isGovPublic)
            {
                nodeId = MainSiteInfo.Additional.GovPublicNodeId;
                if (nodeId == 0)
                {
                    nodeId = PublishmentSystemId;
                }
            }
            _relatedIdentities = RelatedIdentities.GetChannelRelatedIdentities(PublishmentSystemId, nodeId);
            _nodeInfo = NodeManager.GetNodeInfo(1, 1);
            _tableName = NodeManager.GetTableName(MainSiteInfo, _nodeInfo);
            _tableStyle = NodeManager.GetTableStyle(MainSiteInfo, _nodeInfo);
            _tableStyleInfoList = TableStyleManager.GetTableStyleInfoList(_tableStyle, _tableName, _relatedIdentities);
            _attributesOfDisplay = TranslateUtils.StringCollectionToStringCollection(NodeManager.GetContentAttributesOfDisplay(PublishmentSystemId, nodeId));

            if (!IsPostBack)
            {
                BreadCrumb(AppManager.Cms.LeftMenu.IdContent, "内容审核", string.Empty);

                var checkedLevel = 5;
                var isChecked = true;
                foreach (var owningNodeId in ProductPermissionsManager.Current.OwningNodeIdList)
                {
                    int checkedLevelByNodeId;
                    var isCheckedByNodeId = CheckManager.GetUserCheckLevel(Body.AdministratorName, MainSiteInfo, owningNodeId, out checkedLevelByNodeId);
                    if (checkedLevel > checkedLevelByNodeId)
                    {
                        checkedLevel = checkedLevelByNodeId;
                    }
                    if (!isCheckedByNodeId)
                    {
                        isChecked = false;
                    }
                }

                LevelManager.LoadContentLevelToList(State, MainSiteInfo, PublishmentSystemId, isChecked, checkedLevel);

                if (_isGovPublic)
                {
                    PhContentModel.Visible = false;
                }
                else
                {
                    PhContentModel.Visible = true;
                    var contentModelInfoList = ContentModelManager.GetContentModelInfoList(MainSiteInfo);
                    foreach (var modelInfo in contentModelInfoList)
                    {
                        DdlContentModelId.Items.Add(new ListItem(modelInfo.ModelName, modelInfo.ModelId));
                    }
                    ControlUtils.SelectListItems(DdlContentModelId, _nodeInfo.ContentModelId);
                    //EContentModelTypeUtils.AddListItemsForContentCheck(this.ContentModelID);
                }

                if (!string.IsNullOrEmpty(Body.GetQueryString("State")))
                {
                    ControlUtils.SelectListItems(State, Body.GetQueryString("State"));
                }
                if (!string.IsNullOrEmpty(Body.GetQueryString("ModelID")))
                {
                    ControlUtils.SelectListItems(DdlContentModelId, Body.GetQueryString("ModelID"));
                }

                SpContents.ControlToPaginate = RptContents;
                SpContents.ItemsPerPage = MainSiteInfo.Additional.PageSize;

                var checkLevelArrayList = new ArrayList();

                if (!string.IsNullOrEmpty(Body.GetQueryString("State")))
                {
                    checkLevelArrayList.Add(Body.GetQueryString("State"));
                }
                else
                {
                    checkLevelArrayList = LevelManager.LevelInt.GetCheckLevelArrayList(MainSiteInfo, isChecked, checkedLevel);
                }
                var tableName = NodeManager.GetTableName(MainSiteInfo, DdlContentModelId.SelectedValue);
                if (_isGovPublic)
                {
                    tableName = MainSiteInfo.AuxiliaryTableForGovPublic;
                }

                var owningNodeIdList = new List<int>();
                if (!permissions.IsSystemAdministrator)
                {
                    foreach (var owningNodeId in ProductPermissionsManager.Current.OwningNodeIdList)
                    {
                        if (AdminUtility.HasChannelPermissions(Body.AdministratorName, PublishmentSystemId, owningNodeId, AppManager.Cms.Permission.Channel.ContentCheck))
                        {
                            owningNodeIdList.Add(owningNodeId);
                        }
                    }
                }
                //排除专题栏目
                string nodeAllId = string.Empty;
                var secialParentId = DataProvider.NodeDao.GetSpecialParentId();
                nodeAllId = nodeAllId + secialParentId + ",";
                var specialNodeIdList = DataProvider.NodeDao.GetNodeInfoListByParentId(1, secialParentId);
                foreach (var nodeInfo in specialNodeIdList)
                {
                    nodeAllId = nodeAllId + nodeInfo.NodeId + ",";
                    var specialChildNodeIdList = DataProvider.NodeDao.GetNodeInfoListByParentId(1, nodeInfo.NodeId);
                    var childNodeAllId = string.Empty;
                    foreach (var childNodeInfo in specialChildNodeIdList)
                    {
                        nodeAllId = nodeAllId + childNodeInfo.NodeId + ",";
                        childNodeAllId = childNodeAllId + childNodeInfo.NodeId + ",";
                    }
                    childNodeAllId = childNodeAllId.TrimEnd(',');
                    DdlContentModelId.Items.Add(new ListItem(nodeInfo.NodeName, childNodeAllId));
                }
                nodeAllId = nodeAllId.TrimEnd(',');

                SpContents.SelectCommand = BaiRongDataProvider.ContentDao.GetSelectedCommendByCheck(tableName, PublishmentSystemId, permissions.IsSystemAdministrator, owningNodeIdList, checkLevelArrayList,nodeAllId);

                SpContents.SortField = ContentAttribute.LastEditDate;
                SpContents.SortMode = SortMode.DESC;
                RptContents.ItemDataBound += rptContents_ItemDataBound;

                SpContents.DataBind();

                var showPopWinString = ModalContentCheck.GetOpenWindowStringForMultiChannels(PublishmentSystemId, PageUrl);
                BtnCheck.Attributes.Add("onclick", showPopWinString);

                LtlColumnHeadRows.Text = ContentUtility.GetColumnHeadRowsHtml(_tableStyleInfoList, _attributesOfDisplay, _tableStyle, MainSiteInfo);
                LtlCommandHeadRows.Text = ContentUtility.GetCommandHeadRowsHtml(Body.AdministratorName, _tableStyle, MainSiteInfo, _nodeInfo);
            }

            if (!HasChannelPermissions(PublishmentSystemId, AppManager.Cms.Permission.Channel.ContentDelete))
            {
                BtnDelete.Visible = false;
            }
            else
            {
                BtnDelete.Attributes.Add("onclick", PageContentDelete.GetRedirectClickStringForMultiChannels(PublishmentSystemId, false, PageUrl));
            }
        }

        void rptContents_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var ltlItemTitle = (Literal)e.Item.FindControl("ltlItemTitle");
                var ltlChannel = (Literal)e.Item.FindControl("ltlChannel");
                var ltlColumnItemRows = (Literal)e.Item.FindControl("ltlColumnItemRows");
                var ltlItemStatus = (Literal)e.Item.FindControl("ltlItemStatus");
                var ltlItemEditUrl = (Literal)e.Item.FindControl("ltlItemEditUrl");
                var ltlCommandItemRows = (Literal)e.Item.FindControl("ltlCommandItemRows");
                var ltlItemSelect = (Literal)e.Item.FindControl("ltlItemSelect");

                var contentInfo = new ContentInfo(e.Item.DataItem);
                var nodeInfo = NodeManager.GetNodeInfo(PublishmentSystemId, contentInfo.NodeId);

                ltlItemTitle.Text = WebUtils.GetContentTitle(MainSiteInfo, contentInfo, PageUrl);
                var nodeName = _valueHashtable[contentInfo.NodeId] as string;
                if (nodeName == null)
                {
                    nodeName = NodeManager.GetNodeNameNavigation(PublishmentSystemId, contentInfo.NodeId);
                    _valueHashtable[contentInfo.NodeId] = nodeName;
                }
                ltlChannel.Text = nodeName;

                var showPopWinString = ModalCheckState.GetOpenWindowString(PublishmentSystemId, contentInfo, PageUrl);
                ltlItemStatus.Text =
                    $@"<a href=""javascript:;"" title=""设置内容状态"" onclick=""{showPopWinString}"">{LevelManager.GetCheckState(
                        MainSiteInfo, contentInfo.IsChecked, contentInfo.CheckedLevel)}</a>";

                if (HasChannelPermissions(contentInfo.NodeId, AppManager.Cms.Permission.Channel.ContentEdit) || Body.AdministratorName == contentInfo.AddUserName)
                {
                    ltlItemEditUrl.Text =
                        $"<a href=\"{WebUtils.GetContentAddEditUrl(PublishmentSystemId, nodeInfo, contentInfo.Id, PageUrl)}\">编辑</a>";
                }

                ltlItemSelect.Text =
                    $@"<input type=""checkbox"" name=""IDsCollection"" value=""{contentInfo.NodeId}_{contentInfo.Id}"" />";

                ltlColumnItemRows.Text = TextUtility.GetColumnItemRowsHtml(_tableStyleInfoList, _attributesOfDisplay, _valueHashtable, _tableStyle, PublishmentSystemInfo, contentInfo);

                ltlCommandItemRows.Text = TextUtility.GetCommandItemRowsHtml(_tableStyle, MainSiteInfo, nodeInfo, contentInfo, PageUrl, Body.AdministratorName);
            }
        }

        public void State_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(PageUrl, true);
        }

        public void DdlContentModelId_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(PageUrl, true);
        }

        private string _pageUrl;
        private string PageUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_pageUrl))
                {
                    _pageUrl = PageUtils.GetCmsUrl(nameof(PageContentCheck), new NameValueCollection
                    {
                        {"PublishmentSystemID", PublishmentSystemId.ToString()},
                        {"State", State.SelectedValue},
                        {"ModelID", DdlContentModelId.SelectedValue},
                        {"IsGovPublic", _isGovPublic.ToString()}
                    });
                }
                return _pageUrl;
            }
        }
    }
}
