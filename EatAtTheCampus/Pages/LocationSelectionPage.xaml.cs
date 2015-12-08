using System;
using System.Collections.Generic;

using Xamarin.Forms;
using SVGroupWrapper;
using EatAtTheCampus.Helpers;
using System.Diagnostics;
using System.Linq;

namespace EatAtTheCampus
{
	public partial class LocationSelectionPage : ContentPage
	{
		public LocationSelectionPage ()
		{
			InitializeComponent ();
			NavigationPage.SetHasNavigationBar (this, false);
			LoadLocations ();

			var location = Settings.Location;
			Debug.WriteLine ("Default Location: " + location);

			if (location != String.Empty) {
				var l = SVGLocation.Locations.SingleOrDefault (e => e.Id == location);
				if (l != null)
					Navigation.PushAsync (new MenuPage (l), true);
			}
		}

		void LoadLocations ()
		{
			//todo: replace this with a nice solution!
			foreach (var location in SVGLocation.Locations) {
				var btn = new Button ();

				//set attributes
				btn.Text = location.Name;
				btn.CommandParameter = location;
				btn.Clicked += OnCampusButtonClicked;

				//styling
				btn.HorizontalOptions = LayoutOptions.Fill;
				btn.BorderRadius = 0;

				locationLayout.Children.Add (btn);
			}
		}

		void OnCampusButtonClicked (object sender, EventArgs args)
		{
			var btn = sender as Button;
			var location = btn.CommandParameter as SVGLocation;

			//add to settings
			Settings.Location = location.Id;

			Navigation.PushAsync (new MenuPage (location));
		}
	}
}

