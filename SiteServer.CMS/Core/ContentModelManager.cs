using System.Collections.Generic;
using BaiRong.Core;
using BaiRong.Core.Model;
using BaiRong.Core.Model.Enumerations;
using SiteServer.CMS.Model;

namespace SiteServer.CMS.Core
{
    public class ContentModelManager
    {
        private ContentModelManager()
        {
        }

        public static ContentModelInfo GetContentModelInfo(PublishmentSystemInfo publishmentSystemInfo, string contentModelId)
        {
            ContentModelInfo retval = null;
            var list = GetContentModelInfoList(publishmentSystemInfo);
            foreach (var modelInfo in list)
            {
                if (modelInfo.ModelId == contentModelId)
                {
                    retval = modelInfo;
                    break;
                }
            }
            if (retval == null)
            {
                retval = EContentModelTypeUtils.GetContentModelInfo(publishmentSystemInfo.PublishmentSystemType, publishmentSystemInfo.PublishmentSystemId, publishmentSystemInfo.AuxiliaryTableForContent, EContentModelType.Content);
            }
            return retval;
        }

        public static List<ContentModelInfo> GetContentModelInfoList(PublishmentSystemInfo publishmentSystemInfo)
        {
            var list = new List<ContentModelInfo>
            {
                EContentModelTypeUtils.GetContentModelInfo(publishmentSystemInfo.PublishmentSystemType,
                    publishmentSystemInfo.PublishmentSystemId, publishmentSystemInfo.AuxiliaryTableForContent,
                    EContentModelType.Content),
                EContentModelTypeUtils.GetContentModelInfo(publishmentSystemInfo.PublishmentSystemType,
                    publishmentSystemInfo.PublishmentSystemId, publishmentSystemInfo.AuxiliaryTableForContent,
                    EContentModelType.Photo)
            };

            if (publishmentSystemInfo.PublishmentSystemType == EPublishmentSystemType.WCM)
            {
                list.Add(EContentModelTypeUtils.GetContentModelInfo(publishmentSystemInfo.PublishmentSystemType, publishmentSystemInfo.PublishmentSystemId, publishmentSystemInfo.AuxiliaryTableForGovPublic, EContentModelType.GovPublic));

                list.Add(EContentModelTypeUtils.GetContentModelInfo(publishmentSystemInfo.PublishmentSystemType, publishmentSystemInfo.PublishmentSystemId, publishmentSystemInfo.AuxiliaryTableForGovInteract, EContentModelType.GovInteract));
            }

            list.Add(EContentModelTypeUtils.GetContentModelInfo(publishmentSystemInfo.PublishmentSystemType, publishmentSystemInfo.PublishmentSystemId, publishmentSystemInfo.AuxiliaryTableForVote, EContentModelType.Vote));

            list.Add(EContentModelTypeUtils.GetContentModelInfo(publishmentSystemInfo.PublishmentSystemType, publishmentSystemInfo.PublishmentSystemId, publishmentSystemInfo.AuxiliaryTableForJob, EContentModelType.Job));

            list.AddRange(ContentModelUtils.GetContentModelInfoList(publishmentSystemInfo.PublishmentSystemId));

            list.Add(new ContentModelInfo("Study",1,"学习课件",false,"model_Study",EAuxiliaryTableType.Study,"",""));
            list.Add(new ContentModelInfo("TeacherLib", 1, "师资库", false, "siteserver_teacherlibrary", EAuxiliaryTableType.TeacherLib, "", ""));
            list.Add(new ContentModelInfo("Service", 1, "志愿服务", false, "model_voluntaryservice", EAuxiliaryTableType.Service, "", ""));
            list.Add(new ContentModelInfo("Box", 1, "领导信箱", false, "siteserver_leadermailbox", EAuxiliaryTableType.Service, "", ""));
            list.Add(new ContentModelInfo("Wish", 1, "心愿墙", false, "siteserver_wishwall", EAuxiliaryTableType.Wish, "", ""));
            list.Add(new ContentModelInfo("Organization", 1, "组织活动", false, "model_organizationactivity", EAuxiliaryTableType.Organization, "", ""));
            list.Add(new ContentModelInfo("Mien", 1, "党员风采", false, "siteserver_partymembermien", EAuxiliaryTableType.Organization, "", ""));
            list.Add(new ContentModelInfo("Examination", 1, "考题", false, "siteserver_examination", EAuxiliaryTableType.Examination, "", ""));
            list.Add(new ContentModelInfo("Review", 1, "测评", false, "siteserver_examinationpaper", EAuxiliaryTableType.Review, "", ""));


            return list;
        }
    }
}
