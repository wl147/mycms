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

namespace SiteServer.BackgroundPages.Cms
{
   public class PageSiteAdd : BasePageCms
    {
        public TextBox PublishmentSystemArea;
        public TextBox PublishmentSystemName;
        public DropDownList PublishmentSystemType;
        public DropDownList PublishmentSystemCategory;
        public TextBox TelePhone;
        public TextBox Address;
        public TextBox BasicFacts;
        public TextBox Characteristic;
        public TextBox AdministratorAccount;
        public TextBox AdministratorPassWord;
        public DropDownList AdministratorRoles;
        public Button Submit;

        public void Page_Load(object sender, EventArgs e)
        {
           
            
        }
    }
}
