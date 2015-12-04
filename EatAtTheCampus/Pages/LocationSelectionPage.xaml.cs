using System;
using System.Collections.Generic;

using Xamarin.Forms;
using SVGroupWrapper;

namespace EatAtTheCampus
{
	public partial class LocationSelectionPage : ContentPage
	{
		public LocationSelectionPage ()
		{
			InitializeComponent ();

			NavigationPage.SetHasNavigationBar (this, false);
		}

		void OnCampusButtonClicked (object sender, EventArgs args)
		{
			var btn = sender as Button;

			//todo: get location by tag
			var location = (SVGLocation)Enum.Parse (typeof(SVGLocation), btn.Text.Replace (" ", ""));

			Navigation.PushAsync (new MenuPage (location));
		}
	}
}

