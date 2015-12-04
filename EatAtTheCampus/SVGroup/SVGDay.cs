using System;
using System.Collections.Generic;

namespace SVGroupWrapper
{
	public class SVGDay
	{
		public List<SVGMenu> Menus { get; set; }
		public DateTime Date { get; set; }
		public string WeekdayName {
			get{
				return Date.DayOfWeek.ToString ();
			}
		}

		public SVGDay ()
		{
			Menus = new List<SVGMenu> ();
		}
	}
}

