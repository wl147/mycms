using System;
using BaiRong.Core.Data;

namespace BaiRong.Core.Model
{
    public class UserInfo
    {
        public UserInfo()
        {
            UserId = 0;
            UserName = string.Empty;
            Password = string.Empty;
            PasswordFormat = string.Empty;
            PasswordSalt = string.Empty;
            GroupId = 0;
            CreateDate = DateUtils.SqlMinValue;
            LastResetPasswordDate = DateUtils.SqlMinValue;
            LastActivityDate = DateUtils.SqlMinValue;
            CountOfLogin = 0;
            CountOfFailedLogin = 0;
            CountOfWriting = 0;
            IsChecked = true;
            IsLockedOut = false;
            DisplayName = string.Empty;
            Email = string.Empty;
            Mobile = string.Empty;
            AvatarUrl = string.Empty;
            Organization = string.Empty;
            Department = string.Empty;
            Position = string.Empty;
            Gender = string.Empty;
            Birthday = string.Empty;
            Education = string.Empty;
            Graduation = string.Empty;
            Address = string.Empty;
            WeiXin = string.Empty;
            Qq = string.Empty;
            WeiBo = string.Empty;
            Interests = string.Empty;
            Signature = string.Empty;
        }

        public UserInfo(object dataItem)
        {
            if (dataItem == null) return;
            UserId = SqlUtils.EvalInt(dataItem, "UserID");
            UserName = SqlUtils.EvalString(dataItem, "UserName");
            Password = SqlUtils.EvalString(dataItem, "Password");
            PasswordFormat = SqlUtils.EvalString(dataItem, "PasswordFormat");
            PasswordSalt = SqlUtils.EvalString(dataItem, "PasswordSalt");
            GroupId = SqlUtils.EvalInt(dataItem, "GroupID");
            CreateDate = SqlUtils.EvalDateTime(dataItem, "CreateDate");
            LastResetPasswordDate = SqlUtils.EvalDateTime(dataItem, "LastResetPasswordDate");
            LastActivityDate = SqlUtils.EvalDateTime(dataItem, "LastActivityDate");
            CountOfLogin = SqlUtils.EvalInt(dataItem, "CountOfLogin");
            CountOfFailedLogin = SqlUtils.EvalInt(dataItem, "CountOfFailedLogin");
            CountOfWriting = SqlUtils.EvalInt(dataItem, "CountOfWriting");
            IsChecked = SqlUtils.EvalBool(dataItem, "IsChecked");
            IsLockedOut = SqlUtils.EvalBool(dataItem, "IsLockedOut");
            DisplayName = SqlUtils.EvalString(dataItem, "DisplayName");
            Email = SqlUtils.EvalString(dataItem, "Email");
            Mobile = SqlUtils.EvalString(dataItem, "Mobile");
            AvatarUrl = SqlUtils.EvalString(dataItem, "AvatarUrl");
            Organization = SqlUtils.EvalString(dataItem, "Organization");
            Department = SqlUtils.EvalString(dataItem, "Department");
            Position = SqlUtils.EvalString(dataItem, "Position");
            Gender = SqlUtils.EvalString(dataItem, "Gender");
            Birthday = SqlUtils.EvalString(dataItem, "Birthday");
            Education = SqlUtils.EvalString(dataItem, "Education");
            Graduation = SqlUtils.EvalString(dataItem, "Graduation");
            Address = SqlUtils.EvalString(dataItem, "Address");
            WeiXin = SqlUtils.EvalString(dataItem, "WeiXin");
            Qq = SqlUtils.EvalString(dataItem, "QQ");
            WeiBo = SqlUtils.EvalString(dataItem, "WeiBo");
            Interests = SqlUtils.EvalString(dataItem, "Interests");
            Signature = SqlUtils.EvalString(dataItem, "Signature");
            PositiveEnergyValue= SqlUtils.EvalInt(dataItem, "PositiveEnergyValue");
            FlowPartyMember= SqlUtils.EvalInt(dataItem, "FlowPartyMember");
            PublishmentSystemId = SqlUtils.EvalInt(dataItem, "PublishmentSystemId");
            //public int UserId { get; set; }
            //public string TelePhone { get; set; }
            //public string EmergencyName { get; set; }
            //public string EmergencyMobile { get; set; }
            //public string EmergencyRalationShip { get; set; }
            //public string PostalAddress { get; set; }
            //public string ExtraContactWay { get; set; }
            //public string PersonalSatus { get; set; }
            //public string Education { get; set; }
            //public string Degree { get; set; }
            //public string WorkTime { get; set; }
            //public string FirstSituation { get; set; }
            //public string NewStratum { get; set; }
            //public string TechnicalPost { get; set; }
            //public string WorkPlace { get; set; }
            //public string WorkAttribute { get; set; }
            //public string EnterPriseType { get; set; }
            //public string EnterPriseScale { get; set; }
            //public string MediumOrganizationType { get; set; }
            //public DateTime JoinPartyTime { get; set; }
            //public DateTime JoinTime { get; set; }
            //public DateTime ApplyJoinPartyTime { get; set; }
            //public DateTime ConfirmActiveTime { get; set; }
            //public DateTime ConversionTime { get; set; }
            //public DateTime GoOutTime { get; set; }
            //public DateTime GoOutIssuingTime { get; set; }
            //public string GoOutPlace { get; set; }
            //public string GoOutReason { get; set; }
            //public string PartyMemberAdd { get; set; }
            //public DateTime PartyMemberAddTime { get; set; }
            //public string HomeAddress { get; set; }
            //public string Resume { get; set; }
            //public string Remarks { get; set; }
            Additional.UserId= SqlUtils.EvalInt(dataItem, "UserID");
            Additional.TelePhone = SqlUtils.EvalString(dataItem, "TelePhone");
            Additional.EmergencyName = SqlUtils.EvalString(dataItem, "EmergencyName");
            Additional.EmergencyMobile = SqlUtils.EvalString(dataItem, "EmergencyMobile");
            Additional.EmergencyRalationShip = SqlUtils.EvalString(dataItem, "EmergencyRalationShip");
            Additional.PostalAddress = SqlUtils.EvalString(dataItem, "PostalAddress");
            Additional.ExtraContactWay = SqlUtils.EvalString(dataItem, "ExtraContactWay");
            Additional.PersonalSatus = SqlUtils.EvalString(dataItem, "PersonalSatus");
            Additional.Education = SqlUtils.EvalString(dataItem, "Education");
            Additional.Degree = SqlUtils.EvalString(dataItem, "Degree");
            Additional.WorkTime = SqlUtils.EvalString(dataItem, "WorkTime");
            Additional.NewStratum = SqlUtils.EvalString(dataItem, "NewStratum");
            Additional.TechnicalPost = SqlUtils.EvalString(dataItem, "TechnicalPost");
            Additional.WorkPlace = SqlUtils.EvalString(dataItem, "WorkPlace");
            Additional.WorkAttribute = SqlUtils.EvalString(dataItem, "WorkAttribute");
            Additional.EnterPriseType = SqlUtils.EvalString(dataItem, "EnterPriseType");
            Additional.EnterPriseScale = SqlUtils.EvalString(dataItem, "EnterPriseScale");
            Additional.MediumOrganizationType = SqlUtils.EvalString(dataItem, "MediumOrganizationType");
            Additional.JoinPartyTime = SqlUtils.EvalString(dataItem, "JoinPartyTime");
            Additional.JoinTime = SqlUtils.EvalString(dataItem, "JoinTime");
            Additional.ApplyJoinPartyTime = SqlUtils.EvalString(dataItem, "ApplyJoinPartyTime");
            Additional.ConfirmActiveTime = SqlUtils.EvalString(dataItem, "ConfirmActiveTime");
            Additional.ConversionTime = SqlUtils.EvalString(dataItem, "ConversionTime");
            Additional.GoOutTime = SqlUtils.EvalString(dataItem, "GoOutTime");
            Additional.GoOutIssuingTime = SqlUtils.EvalString(dataItem, "GoOutIssuingTime");
            Additional.GoOutPlace = SqlUtils.EvalString(dataItem, "GoOutPlace");
            Additional.GoOutReason = SqlUtils.EvalString(dataItem, "GoOutReason");
            Additional.PartyMemberAdd = SqlUtils.EvalString(dataItem, "PartyMemberAdd");
            Additional.PartyMemberAddTime = SqlUtils.EvalString(dataItem, "PartyMemberAddTime");
            Additional.HomeAddress = SqlUtils.EvalString(dataItem, "HomeAddress");
            Additional.Resume = SqlUtils.EvalString(dataItem, "Resume");
            Additional.Remarks = SqlUtils.EvalString(dataItem, "Remarks");

        }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string PasswordFormat { get; set; }

        public string PasswordSalt { get; set; }

        public int GroupId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime LastResetPasswordDate { get; set; }

        public DateTime LastActivityDate { get; set; }

        public int CountOfLogin { get; set; }

        public int CountOfFailedLogin { get; set; }

        public int CountOfWriting { get; set; }

        public bool IsChecked { get; set; }

        public bool IsLockedOut { get; set; }

        public string DisplayName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string AvatarUrl { get; set; }

        public string Organization { get; set; }

        public string Department { get; set; }

        public string Position { get; set; }

        public string Gender { get; set; }

        public string Birthday { get; set; }

        public string Education { get; set; }

        public string Graduation { get; set; }

        public string Address { get; set; }

        public string WeiXin { get; set; }

        public string Qq { get; set; }

        public string WeiBo { get; set; }

        public string Interests { get; set; }

        public string Signature { get; set; }

        public string ExtendValues { get; set; }

        public int PositiveEnergyValue { get; set;}

        public int FlowPartyMember { get; set; }

        public int PublishmentSystemId { get; set; }

        public string Nation { get; set; }

        public string NativePlace { get; set; }

        public string IdCode { get; set; }

        private UserInfoExtend _additional;
        public UserInfoExtend Additional
        {
            get { return _additional ?? (_additional = new UserInfoExtend(ExtendValues)); }
            set
            {
                _additional = value;
            }
        }
    }
}
