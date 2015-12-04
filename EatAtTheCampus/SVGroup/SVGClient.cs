using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yarx.Net;

namespace SVGroupWrapper
{
	public class SVGClient
	{
		SimpleWebClient _webClient;
		MenuParser _parser;

		public SVGClient ()
		{
			_webClient = new SimpleWebClient ();
			_parser = new MenuParser ();

			//setup webclient
			_webClient.ContentType = "application/x-www-form-urlencoded";
			_webClient.Headers ["X-Requested-With"] = "XMLHttpRequest";
		}

		public async Task<List<SVGDay>> LoadMenuPlan (SVGLocation location)
		{
			var result = _webClient.PostStringAsync (SVGUtil.ServiceUrl, 
				             String.Concat ("action=getMenuplan&params%5Bbranchidentifier%5D=", location.Id));

			var stringResult = await result;
			return _parser.ParseMenus (stringResult);
		}
	}
}

