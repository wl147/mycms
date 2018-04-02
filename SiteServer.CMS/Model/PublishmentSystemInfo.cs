using System;
using System.Xml.Serialization;
using BaiRong.Core.Model.Enumerations;
using BaiRong.Core.Data;

namespace SiteServer.CMS.Model
{
    public class PublishmentSystemAttribute
    {
        protected PublishmentSystemAttribute()
        {
        }

        public const string PublishmentSystemId = "PublishmentSystemId";
        public const string PublishmentSystemName = "PublishmentSystemName";
        public const string PublishmentSystemType = "PublishmentSystemType";
        public const string AuxiliaryTableForContent = "AuxiliaryTableForContent";
        public const string AuxiliaryTableForGovPublic = "AuxiliaryTableForGovPublic";
        public const string AuxiliaryTableForGovInteract = "AuxiliaryTableForGovInteract";
        public const string AuxiliaryTableForJob = "AuxiliaryTableForJob";
        public const string AuxiliaryTableForVote = "AuxiliaryTableForVote";
        public const string IsCheckContentUseLevel = "IsCheckContentUseLevel";
        public const string CheckContentLevel = "CheckContentLevel";
        public const string PublishmentSystemDir = "PublishmentSystemDir";
        public const string PublishmentSystemUrl = "PublishmentSystemUrl";
        public const string IsHeadquarters = "IsHeadquarters";
        public const string ParentPublishmentSystemId = "ParentPublishmentSystemId";
        public const string Taxis = "Taxis";
        public const string SettingsXml = "SettingsXml";
    }

	[Serializable]
	public class PublishmentSystemInfo
	{
		private int _publishmentSystemId;
		private string _publishmentSystemName = string.Empty;
        private EPublishmentSystemType _publishmentSystemType = EPublishmentSystemType.WCM;
		private string _auxiliaryTableForContent = string.Empty;
        private string _auxiliaryTableForGovPublic = string.Empty;
        private string _auxiliaryTableForGovInteract = string.Empty;
        private string _auxiliaryTableForVote = string.Empty;
        private string _auxiliaryTableForJob = string.Empty;
		private bool _isCheckContentUseLevel;
		private int _checkContentLevel;
		private string _publishmentSystemDir = string.Empty;
		private string _publishmentSystemUrl = string.Empty;
        private bool _isHeadquarters;
        private int _parentPublishmentSystemId;
        private int _taxis;
        private string _settingsXml = string.Empty;
        private PublishmentSystemInfoExtend _additional;
        //ÐÂÔö×Ö¶Î
        private int _parentsCount;
        private int _childrentCount;
        private int _areaId;
        private string _areaName;
        private int _organizationTypeId;
        private string _organizationTypeName;
        private int _organizationCategoryId;
        private string _organizationCategoryName;
        private string _telePhone;
        private string _imageUrl;
        private string _address;
        private string _basicFacts;
        private string _characteristic;
        private string _administratorAccount;
        private string _area;

        public PublishmentSystemInfo()
		{
		}
        public PublishmentSystemInfo(object dataItem)
        {
            if (dataItem == null) return;
            PublishmentSystemId = SqlUtils.EvalInt(dataItem, "PublishmentSystemId");
            PublishmentSystemName = SqlUtils.EvalString(dataItem, "PublishmentSystemName");
          
        }

        public PublishmentSystemInfo(int publishmentSystemId, string publishmentSystemName, EPublishmentSystemType publishmentSystemType, string auxiliaryTableForContent, string auxiliaryTableForGovPublic, string auxiliaryTableForGovInteract, string auxiliaryTableForVote, string auxiliaryTableForJob, bool isCheckContentUseLevel, int checkContentLevel, string publishmentSystemDir, string publishmentSystemUrl, bool isHeadquarters, int parentPublishmentSystemId, int taxis, string settingsXml) 
		{
			_publishmentSystemId = publishmentSystemId;
			_publishmentSystemName = publishmentSystemName;
            _publishmentSystemType = publishmentSystemType;
			_auxiliaryTableForContent = auxiliaryTableForContent;
            _auxiliaryTableForGovPublic = auxiliaryTableForGovPublic;
            _auxiliaryTableForGovInteract = auxiliaryTableForGovInteract;
            _auxiliaryTableForVote = auxiliaryTableForVote;
            _auxiliaryTableForJob = auxiliaryTableForJob;
			_isCheckContentUseLevel = isCheckContentUseLevel;
			_checkContentLevel = checkContentLevel;
			_publishmentSystemDir = publishmentSystemDir;
			_publishmentSystemUrl = publishmentSystemUrl;
			_isHeadquarters = isHeadquarters;
            _parentPublishmentSystemId = parentPublishmentSystemId;
            _taxis = taxis;
            _settingsXml = settingsXml;
		}
        public PublishmentSystemInfo(int publishmentSystemId, string publishmentSystemName, EPublishmentSystemType publishmentSystemType, 
            string auxiliaryTableForContent, string auxiliaryTableForGovPublic, 
            string auxiliaryTableForGovInteract, string auxiliaryTableForVote, string auxiliaryTableForJob, 
            bool isCheckContentUseLevel, int checkContentLevel, string publishmentSystemDir, string publishmentSystemUrl, 
            bool isHeadquarters, int parentPublishmentSystemId, int taxis, string settingsXml,int parentsCount,int childrenCount,
            int areaId,int organizationTypeId,int organizationCategoryId,
            string telePhone,string imageUrl,string address,string basicFacts,string characteristic,string administratorAccount,string area)
        {
            _publishmentSystemId = publishmentSystemId;
            _publishmentSystemName = publishmentSystemName;
            _publishmentSystemType = publishmentSystemType;
            _auxiliaryTableForContent = auxiliaryTableForContent;
            _auxiliaryTableForGovPublic = auxiliaryTableForGovPublic;
            _auxiliaryTableForGovInteract = auxiliaryTableForGovInteract;
            _auxiliaryTableForVote = auxiliaryTableForVote;
            _auxiliaryTableForJob = auxiliaryTableForJob;
            _isCheckContentUseLevel = isCheckContentUseLevel;
            _checkContentLevel = checkContentLevel;
            _publishmentSystemDir = publishmentSystemDir;
            _publishmentSystemUrl = publishmentSystemUrl;
            _isHeadquarters = isHeadquarters;
            _parentPublishmentSystemId = parentPublishmentSystemId;
            _taxis = taxis;
            _settingsXml = settingsXml;
            _parentsCount = parentsCount;
            _childrentCount = childrenCount;
            _areaId = areaId;
            //_areaName = areaName;
            _organizationTypeId = organizationTypeId;
           // _organizationTypeName = organizationTypeName;
            _organizationCategoryId = organizationCategoryId;
            //_organizationCategoryName = organizationCategoryName;
            _telePhone = telePhone;
            _imageUrl = imageUrl;
            _address = address;
            _basicFacts = basicFacts;
            _characteristic = characteristic;
            _administratorAccount = administratorAccount;
            _area = area;
        }

        [XmlIgnore]
		public int PublishmentSystemId
		{
			get{ return _publishmentSystemId; }
			set{ _publishmentSystemId = value; }
		}

		[XmlIgnore]
		public string PublishmentSystemName
		{
			get{ return _publishmentSystemName; }
			set{ _publishmentSystemName = value; }
		}

        [XmlIgnore]
        public EPublishmentSystemType PublishmentSystemType
        {
            get { return _publishmentSystemType; }
            set { _publishmentSystemType = value; }
        }

		[XmlIgnore]
		public string AuxiliaryTableForContent
		{
			get{ return _auxiliaryTableForContent; }
			set{ _auxiliaryTableForContent = value; }
		}

        [XmlIgnore]
        public string AuxiliaryTableForGovPublic
        {
            get { return _auxiliaryTableForGovPublic; }
            set { _auxiliaryTableForGovPublic = value; }
        }

        [XmlIgnore]
        public string AuxiliaryTableForGovInteract
        {
            get { return _auxiliaryTableForGovInteract; }
            set { _auxiliaryTableForGovInteract = value; }
        }

        [XmlIgnore]
        public string AuxiliaryTableForJob
        {
            get { return _auxiliaryTableForJob; }
            set { _auxiliaryTableForJob = value; }
        }

        [XmlIgnore]
        public string AuxiliaryTableForVote
        {
            get { return _auxiliaryTableForVote; }
            set { _auxiliaryTableForVote = value; }
        }

        [XmlIgnore]
        public bool IsCheckContentUseLevel
		{
			get{ return _isCheckContentUseLevel; }
			set{ _isCheckContentUseLevel = value; }
		}

		[XmlIgnore]
		public int CheckContentLevel
		{
            get {
                return _isCheckContentUseLevel ? _checkContentLevel : 1;
            }
			set{ _checkContentLevel = value; }
		}

		[XmlIgnore]
		public string PublishmentSystemDir
		{
            get{ return _publishmentSystemDir; }
			set{ _publishmentSystemDir = value; }
		}

		[XmlIgnore]
		public string PublishmentSystemUrl
		{
            get { return _publishmentSystemUrl; }
			set{ _publishmentSystemUrl = value; }
		}

		[XmlIgnore]
        public bool IsHeadquarters
		{
			get{ return _isHeadquarters; }
			set{ _isHeadquarters = value; }
		}

        [XmlIgnore]
        public int ParentPublishmentSystemId
        {
            get { return _parentPublishmentSystemId; }
            set { _parentPublishmentSystemId = value; }
        }

        [XmlIgnore]
        public int Taxis
        {
            get { return _taxis; }
            set { _taxis = value; }
        }

        public string SettingsXml
        {
            get { return _settingsXml; }
            set
            {
                _additional = null;
                _settingsXml = value;
            }
        }
        [XmlIgnore]
        public int ParentsCount
        {
            get { return _parentsCount; }
            set { _parentsCount = value; }
        }

        [XmlIgnore]
        public int ChildrenCount
        {
            get { return _childrentCount; }
            set { _childrentCount = value; }
        }
        public PublishmentSystemInfoExtend Additional => _additional ?? (_additional = new PublishmentSystemInfoExtend(_settingsXml));

        public int AreaId
        {
            get
            {
                return _areaId;
            }

            set
            {
                _areaId = value;
            }
        }

        public string AreaName
        {
            get
            {
                return _areaName;
            }

            set
            {
                _areaName = value;
            }
        }

        public int OrganizationTypeId
        {
            get
            {
                return _organizationTypeId;
            }

            set
            {
                _organizationTypeId = value;
            }
        }

        public string OrganizationTypeName
        {
            get
            {
                return _organizationTypeName;
            }

            set
            {
                _organizationTypeName = value;
            }
        }

        public string OrganizationCategoryName
        {
            get
            {
                return _organizationCategoryName;
            }

            set
            {
                _organizationCategoryName = value;
            }
        }

        public int OrganizationCategory
        {
            get
            {
                return _organizationCategoryId;
            }

            set
            {
                _organizationCategoryId = value;
            }
        }

        public string TelePhone
        {
            get
            {
                return TelePhone1;
            }

            set
            {
                TelePhone1 = value;
            }
        }

        public string TelePhone1
        {
            get
            {
                return _telePhone;
            }

            set
            {
                _telePhone = value;
            }
        }

        public string ImageUrl
        {
            get
            {
                return _imageUrl;
            }

            set
            {
                _imageUrl = value;
            }
        }

        public string Address
        {
            get
            {
                return _address;
            }

            set
            {
                _address = value;
            }
        }

        public string BasicFacts
        {
            get
            {
                return _basicFacts;
            }

            set
            {
                _basicFacts = value;
            }
        }

        public string Characteristic
        {
            get
            {
                return _characteristic;
            }

            set
            {
                _characteristic = value;
            }
        }

        public string AdministratorAccount
        {
            get
            {
                return _administratorAccount;
            }

            set
            {
                _administratorAccount = value;
            }
        }

        public string Area
        {
            get
            {
                return _area;
            }

            set
            {
                _area = value;
            }
        }
    }

    public class ReachInfo
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string ChargeName { get; set; }
        public string TelePhone { get; set; } 
        public int ReachOrganizationId { get; set; }
        public DateTime ReachTime { get; set; }
        public int ActivitiesCount { get; set; }
    }
}
