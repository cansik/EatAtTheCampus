using System;
using System.Linq;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Diagnostics;
using SVGroupWrapper;
using EatAtTheCampus.Helpers;

namespace EatAtTheCampus
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent ();

			MainPage = new NavigationPage (new LocationSelectionPage ());

			//check if default location is already set
			/*
			var location = Settings.Location;
			Debug.WriteLine ("Default Location: " + location);

			if (location == String.Empty)
				MainPage = new NavigationPage (new LocationSelectionPage ());
			else {

				var l = SVGLocation.Locations.Single (e => e.Id == location);
				MainPage = new NavigationPage (new MenuPage (l));	
			}
			*/
		}

		protected override async void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

