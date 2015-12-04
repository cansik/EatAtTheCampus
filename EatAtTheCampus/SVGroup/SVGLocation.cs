using System;

namespace SVGroupWrapper
{
	public class SVGLocation
	{
		public String Name { get; set; }

		public String Id { get; set; }

		public static readonly SVGLocation[] Locations = { 
			new SVGLocation {
				Id = "7824",
				Name = "FHNW Windisch"
			},
			new SVGLocation {
				Id = "1050",
				Name = "Flughafen Restaurant A"
			}
		};
	}
}

