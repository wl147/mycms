using System;
using System.Web.UI.WebControls;
using System.Collections.Specialized;

using BaiRong.Core;
using SiteServer.CMS.BackgroundPages;

using SiteServer.WeiXin.Core;

namespace SiteServer.WeiXin.BackgroundPages.Modal
{
    public class KeywordPreview : BackgroundBasePage
	{
        public TextBox tbWeiXin;

        private int keywordID;

        public static string GetOpenWindowString(int publishmentSystemID, int keywordID)
        {
            var arguments = new NameValueCollection();
            arguments.Add("publishmentSystemID", publishmentSystemID.ToString());
            arguments.Add("keywordID", keywordID.ToString());
            return PageUtilityWX.GetOpenWindowString("Ԥ��", "modal_keywordPreview.aspx", arguments, 400, 300);
        }

		public void Page_Load(object sender, EventArgs e)
        {
            if (IsForbidden) return;

            keywordID = TranslateUtils.ToInt(GetQueryString("keywordID"));

			if (!IsPostBack)
			{
                //this.tbWeiXin.Text = keywordInfo.Keywords;
			}
		}

        public override void Submit_OnClick(object sender, EventArgs e)
        {
            var isChanged = false;

            try
            {
                var keywordInfo = DataProviderWX.KeywordDAO.GetKeywordInfo(keywordID);

                SuccessMessage("����Ԥ���ɹ��������������ֻ�΢������");

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
