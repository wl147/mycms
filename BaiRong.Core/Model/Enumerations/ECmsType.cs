using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiRong.Core.Model.Enumerations
{
    public enum ECmsType
    {
        News,	    //新闻管理
        Study,	    //我要学习
        Partake,    //我要参与
        Branch,      //支部管理
        UserDefined  //自定义
    }

    public class ECmsTypeUtils
    {
        public static string GetValue(ECmsType type)
        {
            if (type == ECmsType.News)
            {
                return "News";
            }
            else if (type == ECmsType.Study)
            {
                return "Study";
            }
            else if (type == ECmsType.Partake)
            {
                return "Partake";
            }
            else if(type == ECmsType.Branch)
            {
                return "Branch";
            }          
            else
            {
                throw new Exception();
            }
        }
        public static string GetText(ECmsType type)
        {
            if (type == ECmsType.News)
            {
                return "新闻管理";
            }
            else if (type == ECmsType.Study)
            {
                return "我要学习";
            }
            else if (type == ECmsType.Partake)
            {
                return "我要参与";
            }
            else if (type == ECmsType.Branch)
            {
                return "支部管理";
            }
            else
            {
                throw new Exception();
            }
        }
        public static int GetDBType(ECmsType type)
        {
            if (type == ECmsType.News)
            {
                return 1;
            }
            else if (type == ECmsType.Study)
            {
                return 2;
            }
            else if (type == ECmsType.Partake)
            {
                return 3;
            }
            else if (type == ECmsType.Branch)
            {
                return 4;
            }
            else
            {
                throw new Exception();
            }
        }
    }
    }
