using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

	class DataTypeA
	{
		public int id { get; set; }
		public string keyA { get; set; }
		public string valueA { get; set; }
	}

	class DataTypeB
	{
		public int id { get; set; }
		public string keyB { get; set; }
		public List<DataTypeA> valueB { get; set; }
	}

	class DataTypeC
	{
		public int id { get; set; }
		public string keyC { get; set; }
		public List<DataTypeB> valueC { get; set; }

	}

	class DataTypeD
	{
		public int id { get; set; }
		public string keyD { get; set; }
		public List<DataTypeC> valueD { get; set; }
	}
}
