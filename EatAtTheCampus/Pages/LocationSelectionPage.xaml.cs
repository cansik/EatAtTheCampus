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
			LoadLocations ();
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

			Navigation.PushAsync (new MenuPage (location));
		}
	}
}

