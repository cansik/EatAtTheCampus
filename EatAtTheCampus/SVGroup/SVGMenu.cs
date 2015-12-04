using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SVGroupWrapper
{
	public class SVGMenu
	{
		public DateTime Date;
		public string MenuType;
		public string Name;
		public string Trimmings;
		public string Price;
		public string Info;
		public string ImageUrl;

		public SVGMenu ()
		{
		}

		public SVGMenu (string menuType)
		{
			MenuType = menuType;
		}
	}
}

