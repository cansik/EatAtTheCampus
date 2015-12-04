using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace SVGroupWrapper
{
	class MenuParser
	{
		//(details-date">(?<date>[\w\s,\.]+)</h3>)|((details-menu-type|details-menu-name|details-menu-trimmings|details-menu-price|details-menu-info)">([\w\s-\.:\/]+))
		Regex _menuRegex = new Regex (@"(details-date"">(?<date>[\w\s,\.]+)</h3>)|((details-menu-type|details-menu-name|details-menu-trimmings|details-menu-price|details-menu-info)"">(?<data>[\w\s-\.:\/,]+))");
		Regex _unicodeRegex = new Regex (@"\\u(?<code>[\d\w]{4})");

		List<SVGDay> days;

		DateTime currentDate = DateTime.Now;

		public MenuParser ()
		{
		}

		public List<SVGDay> ParseMenus (string html)
		{
			days = new List<SVGDay> ();

			html = ReplaceAndTrim (html);
			var matches = _menuRegex.Matches (html);

			foreach (Match match in matches)
				ProcessMatch (match);

			return days;
		}

		void ProcessMatch (Match m)
		{
			//date process
			if (m.Value.StartsWith ("details-date")) {
				string date = m.Groups ["date"].Value.Remove (0, m.Groups ["date"].Value.IndexOf (",") + 2).Replace (" ", string.Empty);
				currentDate = DateTime.ParseExact (date, "dd.MM.yyyy", null);

				days.Add (new SVGDay (){ Date = currentDate });
			}

			//menu start process
			if (m.Value.StartsWith ("details-menu-type")) {
				CurrentDay ().Menus.Add (new SVGMenu (m.Groups ["data"].Value.Trim ()));
				CurrentMenu ().Date = currentDate;
			}

			if (m.Value.StartsWith ("details-menu-name")) {
				CurrentMenu ().Name = m.Groups ["data"].Value.Trim ();
			}

			if (m.Value.StartsWith ("details-menu-trimmings")) {
				CurrentMenu ().Trimmings = m.Groups ["data"].Value.Trim ();
			}

			if (m.Value.StartsWith ("details-menu-price")) {
				CurrentMenu ().Price = m.Groups ["data"].Value.Trim ();
			}

			if (m.Value.StartsWith ("details-menu-info")) {
				CurrentMenu ().Info = m.Groups ["data"].Value.Trim ();
			}
		}

		SVGMenu CurrentMenu ()
		{
			if (CurrentDay ().Menus.Count > 0)
				return CurrentDay ().Menus [CurrentDay ().Menus.Count - 1];

			return null;
		}

		SVGDay CurrentDay ()
		{
			if (days.Count > 0)
				return days [days.Count - 1];

			return null;
		}

		string ReplaceAndTrim (string html)
		{
			html = html.Replace ("\\/", "/");
			html = html.Replace ("\\\"", "\"");
			html = html.Replace ("\\u2013", string.Empty);
			html = html.Replace ("<br />\\n", Environment.NewLine);

			//replace unicode entries
			html = _unicodeRegex.Replace (html, delegate(Match m) {
				var code = HexToDec (m.Groups ["code"].Value);
				return char.ConvertFromUtf32 (code);
			});

			return html;
		}

		public int HexToDec (string hexValue)
		{
			return Int32.Parse (hexValue, System.Globalization.NumberStyles.HexNumber);
		}
	}
}

