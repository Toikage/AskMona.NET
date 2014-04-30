using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AskMonaNet
{
	partial class AskMonaNet
	{
		public Encoding encoding = Encoding.GetEncoding("s-jis");

		protected JObject Call(string uri, string req)
		{
			WebRequest request = WebRequest.Create(uri);

			request.Method = "GET";

			{
				byte[] data = encoding.GetBytes(req);
				request.ContentLength = data.Length;
				using (var s = request.GetRequestStream())
				{
					s.Write(data, 0, data.Length);
				}
			}

			try
			{
				using (WebResponse response = request.GetResponse())
				using (StreamReader sr = new StreamReader(response.GetResponseStream(), encoding))
				{
					return JObject.Parse(sr.ReadToEnd());
				}
			}
			catch (WebException wex)
			{
				throw wex;
			}
		}



	}
}
