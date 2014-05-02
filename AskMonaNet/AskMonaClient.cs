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
		static internal string HttpCall(Uri uri)
		{
			WebRequest request = WebRequest.Create(uri);

			request.Method = WebRequestMethods.Http.Get;

			using (WebResponse response = request.GetResponse())
			using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.ASCII))
			{
				return sr.ReadToEnd();
			}
		}


		static internal string HttpPost(Uri uri, string req)
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

		internal T Call<T>(string url, Dictionary<string, string> prm)
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
					sb.Append(WebUtility.UrlEncode(item.Value));
				}
				ub.Query = sb.ToString();
			}
			return JsonConvert.DeserializeObject<T>(HttpCall(ub.Uri));
		}

		internal T Post<T>(string url, Dictionary<string, string> prm)
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

		internal T PostAuth<T>(string url, AskMonaUser user, Dictionary<string, string> data)
		{
			var ak = user.GenerateAuthKey(app_secretkey);
			data.Add("app_id", app_id.ToString());
			data.Add("u_id", user.u_id.ToString());
			data.Add("nonce", ak.nonce);
			data.Add("time", ak.time);
			data.Add("auth_key", ak.auth_key);
			return Post<T>(url, data);
		}

		/// <summary>
		/// Unixエポック時間を取得します。
		/// </summary>
		public static readonly DateTime UnixTimeOrigin = new DateTime(1970, 1, 1, 0, 0, 0, 0);

		/// <summary>
		/// Unix時間からDateTime時刻へ変換します。
		/// </summary>
		/// <param name="unixtime">Unix時間</param>
		/// <returns></returns>
		public static DateTime ConvertFromUnixTime(int unixtime)
		{
			return UnixTimeOrigin.AddSeconds(unixtime);
		}

		/// <summary>
		/// DateTime時刻からUnix時間へ変換します。
		/// </summary>
		/// <param name="datetime">DateTime時刻</param>
		/// <returns></returns>
		public static int ConvertToUnixTimestamp(DateTime datetime)
		{
			return (int)(datetime - UnixTimeOrigin).TotalSeconds;
		}
	}
}
