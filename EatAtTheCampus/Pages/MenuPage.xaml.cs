using System;
using System.Collections.Generic;

using Xamarin.Forms;
using EatAtTheCampus;
using SVGroupWrapper;
using System.Diagnostics;

namespace EatAtTheCampus
{
	public partial class MenuPage : ContentPage
	{
		readonly SVGLocation location;
		readonly SVGClient client;

		public MenuPage ()
		{
			InitializeComponent ();
			client = new SVGClient ();
		}

		public MenuPage (SVGLocation location) : this ()
		{
			this.location = location;
			Title = location.Name;
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
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

			var today = data [0];

			foreach (var menu in today.Menus)
				resultLayout.Children.Add (new MenuView (menu));

			activityIndicator.IsRunning = false;
		}
	}
}

