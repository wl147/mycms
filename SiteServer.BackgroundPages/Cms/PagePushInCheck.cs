using System;
using System.Web.UI.WebControls;
using BaiRong.Core;
using BaiRong.Core.Model;
using BaiRong.Core.Model.Enumerations;
using SiteServer.BackgroundPages.Controls;
using SiteServer.BackgroundPages.Core;
using SiteServer.CMS.Core;
using SiteServer.BackgroundPages.User;

namespace SiteServer.BackgroundPages.Cms
{
    public class PagePushInCheck : BasePage
    {
        public DropDownList DdlGroup;
        public DropDownList DdlPageNum;
        public DropDownList DdlLoginCount;

        public DropDownList DdlSearchType;
        public TextBox TbKeyword;
        public DropDownList DdlCreationDate;
        public DropDownList DdlLastActivityDate;

        public Repeater RptContents;
        public SqlCountPager SpContents;

        public Button BtnCheckYes;
        public Button BtnCheckNo;

        private EUserLockType _lockType = EUserLockType.Forever;

        public static string GetRedirectUrl()
        {
            return PageUtils.GetPartyTransUrl(nameof(PagePushInCheck), null);
        }

        public string GetDateTime(DateTime datetime)
        {
            var retval = string.Empty;
            if (datetime > DateUtils.SqlMinValue)
            {
                retval = DateUtils.GetDateString(datetime);
            }
            return retval;
        }

        public void Page_Load(object sender, EventArgs e)
        {
            if (IsForbidden) return;
            int userId=Body.AdministratorInfo.PublishmentSystemId;
            if (Body.IsQueryExists("CheckOK"))
            {
                var transIDList = TranslateUtils.StringCollectionToIntList(Body.GetQueryString("TransIDCollection"));
                try
                {
                    foreach (var transId in transIDList)
                    {
                        BaiRongDataProvider.PartyDao.CheckInOK(transId);
                    }

                    Body.AddAdminLog("党员转入审核", string.Empty);

                    SuccessUpdateMessage();
                }
                catch (Exception ex)
                {
                    FailDeleteMessage(ex);
                }
            }

            SpContents.ControlToPaginate = RptContents;
            SpContents.ItemsPerPage = 25;
            SpContents.SelectCommand = BaiRongDataProvider.PartyDao.GetPartyTransformIn(userId);

            RptContents.ItemDataBound += rptContents_ItemDataBound;
            SpContents.SortField = BaiRongDataProvider.PartyDao.GetSortFieldDate();
            SpContents.SortMode = SortMode.DESC;

            _lockType = EUserLockTypeUtils.GetEnumType(ConfigManager.UserConfigInfo.LoginLockingType);

            if (IsPostBack) return;
            var backgroundUrl = GetRedirectUrl();
            BtnCheckYes.Attributes.Add("onclick", PageUtils.GetRedirectStringWithCheckBoxValueAndAlert(
                $"{backgroundUrl}?CheckOK=True", "TransIDCollection", "TransIDCollection", "请选择需要审核的用户！", "确认审核通过？"));


            SpContents.DataBind();
        }

        public void rptContents_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;

            var partyInfo = new PartyTransCheckInfo(e.Item.DataItem);

            var ltlID = (Literal)e.Item.FindControl("ltlID");
            var ltlMobilePhone = (Literal)e.Item.FindControl("ltlMobilePhone");
            var ltlUserName = (Literal)e.Item.FindControl("ltlUserName");
            var ltlTransformOut = (Literal)e.Item.FindControl("ltlTransformOut");
            var ltlApplyDate = (Literal)e.Item.FindControl("ltlOutCheckDate");
            var ltlInCheckState = (Literal)e.Item.FindControl("ltlInCheckState");
            var hlEditLink = (HyperLink)e.Item.FindControl("hlEditLink");
            var ltlSelect = (Literal)e.Item.FindControl("ltlSelect");

            ltlID.Text = partyInfo.ID.ToString();
            ltlMobilePhone.Text = partyInfo.MobilePhone;
            ltlUserName.Text = partyInfo.UserName;
            ltlTransformOut.Text = partyInfo.TransformOutName;
            ltlApplyDate.Text= DateUtils.GetDateAndTimeString(partyInfo.OutCheckDate);
            ltlInCheckState.Text = partyInfo.InCheckStateString;
            ltlSelect.Text = $@"<input type=""checkbox"" name=""TransIDCollection"" value=""{partyInfo.ID}"" />";
        }

        private string _pageUrl;
        private string PageUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_pageUrl))
                {
                    _pageUrl =
                        $"{GetRedirectUrl()}?GroupID={DdlGroup.SelectedValue}&PageNum={DdlPageNum.SelectedValue}&Keyword={TbKeyword.Text}&CreationDate={DdlCreationDate.SelectedValue}&LastActivityDate={DdlLastActivityDate.SelectedValue}&loginCount={DdlLoginCount.SelectedValue}&SearchType={DdlSearchType.SelectedValue}";
                }
                return _pageUrl;
            }
        }
    }
}
