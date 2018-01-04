using System;
using System.Web.UI.WebControls;
using System.Collections.Specialized;

using BaiRong.Core;
using SiteServer.CMS.BackgroundPages;

using SiteServer.WeiXin.Core;
using SiteServer.WeiXin.Model;

namespace SiteServer.WeiXin.BackgroundPages.Modal
{
    public class CouponSNSetting : BackgroundBasePage
	{
        public DropDownList ddlStatus;

        private int couponID;

        public static string GetOpenWindowString(int publishmentSystemID, int couponID)
        {
            var arguments = new NameValueCollection();
            arguments.Add("publishmentSystemID", publishmentSystemID.ToString());
            arguments.Add("couponID", couponID.ToString());
            return PageUtilityWX.GetOpenWindowStringWithCheckBoxValue("�����Ż݄�����", "modal_couponSNSetting.aspx", arguments, "IDCollection", "��ѡ����Ҫ���õ�SN��", 400, 300);
        }

		public void Page_Load(object sender, EventArgs e)
        {
            if (IsForbidden) return;

            couponID = TranslateUtils.ToInt(GetQueryString("couponID"));

			if (!IsPostBack)
			{
                ECouponStatusUtils.AddListItems(ddlStatus);
			}
		}

        public override void Submit_OnClick(object sender, EventArgs e)
        {
            var isChanged = false;

            try
            {

                DataProviderWX.CouponSNDAO.UpdateStatus(ECouponStatusUtils.GetEnumType(ddlStatus.SelectedValue), TranslateUtils.StringCollectionToIntList(Request.QueryString["IDCollection"]));

                isChanged = true;
            }
            catch (Exception ex)
            {
                FailMessage(ex, "ʧ�ܣ�" + ex.Message);
            }

            if (isChanged)
            {
                JsUtils.OpenWindow.CloseModalPage(Page);
            }
		}
	}
}
