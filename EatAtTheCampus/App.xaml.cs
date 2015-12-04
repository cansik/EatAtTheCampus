using System;
using System.Collections.Generic;

using Xamarin.Forms;
using SVGroupWrapper;
using System.Diagnostics;

namespace EatAtTheCampus
{
	public partial class App : Application
	{
		SVGClient client;

		public App ()
		{
			InitializeComponent ();
			MainPage = new NavigationPage (new LocationSelectionPage ());
		}

		protected override async void OnStart ()
		{
			// Handle when your app starts
			//var data = await client.LoadMenuPlan (SVGLocation.FHNWBruggWindisch);
			Debug.WriteLine ("Loaded");

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

