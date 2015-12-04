using System;
using System.Collections.Generic;
using Xamarin.Forms;
using SVGroupWrapper;

namespace EatAtTheCampus
{
	public partial class MenuView : ContentView
	{
		public SVGMenu Menu { get; set; }

		public MenuView (SVGMenu menu)
		{
			InitializeComponent ();
			Menu = menu;

			//todo: ask why the fuck?!
			BindingContext = new {MenuType = menu.MenuType, Name = menu.Name};
		}
	}
}

