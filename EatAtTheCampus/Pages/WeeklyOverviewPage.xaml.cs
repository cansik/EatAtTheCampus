using System;
using System.Linq;
using System.Collections.Generic;

using Xamarin.Forms;
using SVGroupWrapper;
using EatAtTheCampus.Helpers;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EatAtTheCampus
{
	public partial class WeeklyOverviewPage : CarouselPage
	{
		SVGLocation location;
		readonly SVGClient client;

		public WeeklyOverviewPage ()
		{
			InitializeComponent ();
			NavigationPage.SetHasNavigationBar (this, false);

			client = new SVGClient ();
			LoadCurrentLocation ();
		}

		async void LoadCurrentLocation ()
		{
			//load saved settings
			var locationId = Settings.Location;
			Debug.WriteLine ("Default Location: " + location);

			location = SVGLocation.Locations.SingleOrDefault (e => e.Id == locationId);
			if (location == null) {
				var locationSelectionPage = new LocationSelectionPage ();
				await Navigation.PushAsync (locationSelectionPage);
				location = locationSelectionPage.SelectedLocation;
			}
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			if (location != null)
				LoadMenu ();
		}

		async void LoadMenu ()
		{
			var data = await client.LoadMenuPlan (location);

			if (data.Count == 0) {
				resultLayout.Children.Add (new Label{ Text = "No menu today!" });
				activityIndicator.IsRunning = false;
				return;
			}


			foreach (var day in data) {
				Children.Add (new MenuPage (day));
			}
			activityIndicator.IsRunning = false;

			Children.RemoveAt (0);
		}
	}
}

