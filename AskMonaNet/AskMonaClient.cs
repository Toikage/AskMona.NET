using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;

namespace AskMonaNet
{
	public partial class AskMonaClient
	{
		static private string HttpCall(Uri uri)
		{


			WebRequest request = WebRequest.Create(uri);

			request.Method = WebRequestMethods.Http.Get;

			using (WebResponse response = request.GetResponse())
			using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.ASCII))
			{
				return sr.ReadToEnd();
			}
		}


		static private string HttpPost(Uri uri, string req)
		{
			byte[] data = Encoding.ASCII.GetBytes(req);

			WebRequest request = WebRequest.Create(uri);
			request.Method = WebRequestMethods.Http.Post;

			request.ContentType = "application/x-www-form-urlencoded";

			request.ContentLength = data.Length;

			using (var s = request.GetRequestStream())
			{
				s.Write(data, 0, data.Length);
			}

			using (WebResponse response = request.GetResponse())
			using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.ASCII))
			{
				return sr.ReadToEnd();
			}
		}

		private T Call<T>(string url, Dictionary<string, string> prm)
		{
			UriBuilder ub = new UriBuilder(url);
			{
				StringBuilder sb = new StringBuilder();
				bool isFirst = true;
				foreach (var item in prm)
				{
					if (!isFirst) sb.Append("&");
					else isFirst = false;
					sb.Append(item.Key);
					sb.Append('=');
					sb.Append(item.Value);
				}
				ub.Query = sb.ToString();
			}
			return JsonConvert.DeserializeObject<T>(HttpCall(ub.Uri));
		}

		private T Post<T>(string url, Dictionary<string, string> prm)
		{
			string data;
			{
				StringBuilder sb = new StringBuilder();
				bool isFirst = true;
				foreach (var item in prm)
				{
					if (!isFirst) sb.Append("&");
					else isFirst = false;
					sb.Append(item.Key);
					sb.Append('=');
					sb.Append(WebUtility.UrlEncode(item.Value));
				}
				data = sb.ToString();
			}
			return JsonConvert.DeserializeObject<T>(HttpPost(new Uri(url), data));
		}

		private T CallAuth<T>(string url, AskMonaUser user)
		{
			var ak = user.GenerateAuthKey(app_secretkey);

			return Call<T>(url,
				new Dictionary<string, string>{
					{"app_id",app_id.ToString()},
					{"u_id",user.u_id.ToString()},
					{"nonce",ak.nonce},
					{"time",ak.time},
					{"auth_key",ak.auth_key}
				}
			);
		}

		private T PostAuth<T>(string url, AskMonaUser user, Dictionary<string, string> data)
		{
			var ak = user.GenerateAuthKey(app_secretkey);

			data.Add("app_id", app_id.ToString());
			data.Add("u_id", user.u_id.ToString());
			data.Add("nonce", ak.nonce);
			data.Add("time", ak.time);
			data.Add("auth_key", ak.auth_key);
			return Post<T>(url, data);
		}


		private static readonly DateTime UnixTimeOrigin = new DateTime(1970, 1, 1, 0, 0, 0, 0);

		/// <summary>
		/// Unix時間からDateTime型へ変換します。
		/// </summary>
		/// <param name="timestamp"></param>
		/// <returns></returns>
		public static DateTime ConvertFromUnixTime(int timestamp)
		{
			return UnixTimeOrigin.AddSeconds(timestamp);
		}

		/// <summary>
		/// DateTime型からUnix時間へ変換します。
		/// </summary>
		/// <param name="date"></param>
		/// <returns></returns>

		public static int ConvertToUnixTimestamp(DateTime date)
		{
			return (int)(date - UnixTimeOrigin).TotalSeconds;
		}
	}
}
