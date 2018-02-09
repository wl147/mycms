using System;

namespace BaiRong.Core.Model
{
    public class UserInfoExtend : ExtendedAttributes
    {
        public UserInfoExtend(string extendValues)
        {
            var nameValueCollection = TranslateUtils.ToNameValueCollection(extendValues);
            SetExtendedAttribute(nameValueCollection);
        }

        public int LastWritingPublishmentSystemId
        {
            get { return GetInt("LastWritingPublishmentSystemId", 0); }
            set { SetExtendedAttribute("LastWritingPublishmentSystemId", value.ToString()); }
        }

        public int LastWritingNodeId
        {
            get { return GetInt("LastWritingNodeId", 0); }
            set { SetExtendedAttribute("LastWritingNodeId", value.ToString()); }
        }
        public int UserId { get; set; }
        public string TelePhone { get; set; }
        public string EmergencyName { get; set; }
        public string EmergencyMobile { get; set; }
        public string EmergencyRalationShip { get; set; }
        public string PostalAddress { get; set; }
        public string ExtraContactWay { get; set; }
        public string PersonalSatus { get; set; }
        public string Education { get; set; }
        public string Degree { get; set; }
        public string WorkTime { get; set; }
        public string FirstSituation { get; set; }
        public string NewStratum { get; set; }
        public string TechnicalPost { get; set; }
        public string WorkPlace { get; set; }
        public string WorkAttribute { get; set; }
        public string EnterPriseType { get; set; }
        public string EnterPriseScale { get; set; }
        public string MediumOrganizationType { get; set; }
        public string JoinPartyTime { get; set; }
        public string JoinTime { get; set; }
        public string ApplyJoinPartyTime { get; set; }
        public string ConfirmActiveTime { get; set; }
        public string ConversionTime { get; set; }
        public string GoOutTime { get; set; }
        public string GoOutIssuingTime { get; set; }
        public string GoOutPlace { get; set; }
        public string GoOutReason { get; set; }
        public string PartyMemberAdd { get; set; }
        public string PartyMemberAddTime { get; set; }
        public string HomeAddress { get; set; }
        public string Resume { get; set; }
        public string Remarks { get; set; }
    }
}
