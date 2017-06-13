using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	class PCFObjectModel
	{
		public string TextFilePath { get; set; }
		public string TextFileName { get; set; }
		public int NumberOfComponents { get; set; }
		public int NumberOfLines { get; set; }
		public List<Component> Components { get; set; }

	}

	class Component
	{
		public string Key { get; set; }
		public string Value { get; set; }
		public List<ComponentAttribute> Attributes { get; set; }
	}

	class ComponentAttribute
	{
		//The first key-value pairs are stored as one string per attribute
		public List<JsonObject> HeadingAttributes { get; set; }

		//The least important key-value pairs are  stored as one string per attribute
		public List<KeyValuePair<String, Object>> RemainingAttributes { get; set; }

		//Not critical componentAttributes are listed below
		public string EndPoint1 {get; set;}
		public string EndPoint2 { get; set; }
		public string PipingSpec { get; set; }
		public string InsulationSpec { get; set; }
		public string IsContinue { get; set; }
		public string Weight { get; set; }


		//Attributes that are listed below are critically ordered
		public string ComponentAttribute1 { get; set; }
		public string ComponentAttribute2 { get; set; }
		public string ComponentAttribute3 { get; set; }
		public string ComponentAttribute4 { get; set; }
		public string ComponentAttribute5 { get; set; }
		public string ComponentAttribute6 { get; set; }
		public string ComponentAttribute7 { get; set; }
		public string ComponentAttribute8 { get; set; }
		public string ComponentAttribute9 { get; set; }
		public string ComponentAttribute10 { get; set; }
	}


	class JsonObject
	{
		public string Key { get; set; }
		public string Value { get; set; }

	}
	class DataTypeA
	{
		public int Id { get; set; }
		public string KeyA { get; set; }
		public string ValueA { get; set; }
	}

	class DataTypeB
	{
		public int Id { get; set; }
		public string KeyB { get; set; }
		public List<DataTypeA> ValueB { get; set; }
	}

	class DataTypeC
	{
		public int Id { get; set; }
		public string KeyC { get; set; }
		public List<DataTypeB> ValueC { get; set; }

	}

	class DataTypeD
	{
		public int Id { get; set; }
		public string KeyD { get; set; }
		public List<DataTypeC> ValueD { get; set; }
	}
}
