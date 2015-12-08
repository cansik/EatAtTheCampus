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
			BindingContext = new {menu.MenuType, menu.Name, menu.Trimmings, menu.Price, menu.Info};
		}

		void OnItemClicked (object sender, EventArgs e)
		{
			var uri = new Uri ("https://www.google.com/search?q=" + Menu.Name + "&source=lnms&tbm=isch&sa=X");
			Device.OpenUri (uri);
		}
	}
}

