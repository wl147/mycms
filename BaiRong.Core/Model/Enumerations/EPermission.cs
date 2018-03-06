using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiRong.Core.Model.Enumerations
{
    public class EPermissionUtils
    {
        public static Dictionary<string, string> ChannelPermissionType()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("cms_contentView", "浏览");
            dic.Add("cms_contentAdd", "添加");
            dic.Add("cms_contentEdit", "修改");
            dic.Add("cms_contentDelete", "删除");            
            return dic;
        }
        public static string GetChnanelPermissionText(string type)
        {
            if(string.IsNullOrEmpty(type)) throw new Exception();
            if (StringUtils.EqualsIgnoreCase(type, "cms_contentView"))
            {
                return "浏览";
            }
            else if (StringUtils.EqualsIgnoreCase(type, "cms_contentAdd"))
            {
                return "添加";
            }
            else if (StringUtils.EqualsIgnoreCase(type, "cms_contentEdit"))
            {
                return "修改";
            }
            else if (StringUtils.EqualsIgnoreCase(type, "cms_contentDelete"))
            {
                return "删除";
            }           
            else
            {
                return type;
            }
        }
    }    
}
