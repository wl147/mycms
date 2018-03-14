using System.Xml;

namespace BaiRong.Core.Configuration
{
	public class PermissionConfig
	{
		string name;
		string text;
        string type;

        public PermissionConfig(XmlAttributeCollection attributes) 
		{
            name = attributes["name"].Value;
            text = attributes["text"].Value;
            type = attributes["type"]==null ?"": attributes["type"].Value;
        }

        public PermissionConfig(string name, string text)
        {
            this.name = name;
            this.text = text;
        }

		public string Name 
		{
			get 
			{
				return name;
			}
            set
            {
                name = value;
            }
		}

		public string Text 
		{
			get 
			{
				return text;
			}
            set
            {
                text = value;
            }
		}
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
	}
}
