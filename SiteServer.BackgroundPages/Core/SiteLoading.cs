using SiteServer.CMS.Model;
using SiteServer.CMS.Model.Enumerations;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteServer.BackgroundPages.Core
{
   public class SiteLoading
    {
        public static string GetScript(PublishmentSystemInfo publishmentSystemInfo, ELoadingType loadingType, NameValueCollection additional)
        {
            return SiteTreeItem.GetScript(publishmentSystemInfo, loadingType, additional);
        }
        public static string GetSiteRowHtml(PublishmentSystemInfo publishmentSystemInfo,int carrentMainId)
        {
            var siteTreeItem = SiteTreeItem.CreateInstance(publishmentSystemInfo, carrentMainId);
            var title = siteTreeItem.GetItemHtml();
            var rowHtml = string.Empty;
            rowHtml = $@"
<tr treeItemLevel=""{publishmentSystemInfo.ParentsCount + 1}"">
	<td align=""left"" nowrap>
		{title}
	</td>
</tr>
";
            return rowHtml;
        }
    }
}
