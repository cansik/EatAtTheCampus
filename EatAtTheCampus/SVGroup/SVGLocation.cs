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
				Id = "7560",
				Name = "FHNW Muttenz"
			},
			new SVGLocation {
				Id = "7090",
				Name = "Universität Basel"
			},
		};
	}
}

