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
			Debug.WriteLine ("Loaded");
		}
	}
}

