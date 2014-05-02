using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Net;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace AskMonaNet
{
	/// <summary>
	/// AskMonaのユーザー情報です。
	/// 認証が必要なAPIに使用します。
	/// </summary>
	public class AskMonaUser
	{
		/// <summary>
		/// 利用者のユーザーID。
		/// </summary>
		public int u_id { get; set; }

		/// <summary>
		/// 認証キーの作成に使うシークレットキー。
		/// </summary>
		public string secretkey { get; set; }

		private AskMonaUser(int u_id, string secretkey)
		{
			this.u_id = u_id;
			this.secretkey = secretkey;
		}


		private static readonly Random rnd = new Random();

		internal AskMonaAuthKey GenerateAuthKey(string app_secretkey)
		{
			/*
			 * 開発者シークレットキー、nonce、time、認証キーの作成に使うシークレットキーの順に連結した文字列を
			 * SHA-256でハッシュ化し、そのバイナリデータをBase64でエンコードしたものが認証キーです。
			 */
			var ak = new AskMonaAuthKey();

			SHA256 sha = SHA256.Create();

			byte[] _nonce = new byte[32];
			rnd.NextBytes(_nonce);
			ak.nonce = Convert.ToBase64String(_nonce);

			ak.time = AskMonaClient.ConvertToUnixTimestamp(DateTime.Now.ToUniversalTime()).ToString();

			ak.auth_key = Convert.ToBase64String(sha.ComputeHash(
				Encoding.ASCII.GetBytes(
				app_secretkey + ak.nonce + ak.time + secretkey
				)));

			return ak;
		}

		/// <summary>
		/// アプリ連携ページから取得された認証コードからユーザー情報のインスタンスを初期化します。
		/// </summary>
		/// <param name="authcode"></param>
		/// <exception cref="FormatException"></exception>
		/// <returns></returns>
		static public AskMonaUser CreateFromAuthCode(string authcode)
		{
			//{"u_id":338,"secretkey":"UIpzQJbYmxSl8ECUusTIWBzifjYl5y9sI3rVYNCgW8Ro="}

			var r_u_id = Regex.Match(authcode, @"""u_id"":(\d*)");
			var secretkey = Regex.Match(authcode, @"""secretkey"":""(.*)""");
			int u_id;

			if (int.TryParse(r_u_id.Groups[1].Value, out u_id) && secretkey.Success)
			{
				return new AskMonaUser(u_id, secretkey.Groups[1].Value);
			}
			else
			{
				throw new FormatException("正しい認証コードではありません。");
			}
		}

		/// <summary>
		/// ユーザーIDとシークレットキーからユーザー情報のインスタンスを初期化します。
		/// </summary>
		/// <param name="u_id">ユーザーのID。</param>
		/// <param name="secretkey">シークレットキー。</param>
		/// <returns></returns>
		static public AskMonaUser Create(int u_id,string secretkey)
		{
			return new AskMonaUser(u_id, secretkey);
		}

	}

	internal class AskMonaAuthKey
	{
		public string auth_key;
		public string nonce;
		public string time;
	}
}
