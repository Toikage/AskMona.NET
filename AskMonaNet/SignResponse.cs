using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskMonaNet
{
	/// <summary>
	/// AskMona登録APIのレスポンス
	/// 登録したユーザー情報が作成されます。
	/// </summary>
	public class SignupResponse : AskMonaResponse
	{
		/// <summary>
		/// ユーザー情報。
		/// </summary>
		public AskMonaUser user;
	}

	/// <summary>
	/// AskMona登録APIのレスポンス
	/// </summary>
	public class SignupLowResponse : AskMonaResponse
	{
		/// <summary>
		/// 利用者のユーザーID。
		/// </summary>
		public int u_id;
		/// <summary>
		/// 認証キーの作成に使うシークレットキー。
		/// </summary>
		public string secretkey;
	}

	/// <summary>
	/// シークレットキー取得APIのレスポンス
	/// </summary>
	public class SigninResponse : SignupResponse { }

	/// <summary>
	/// シークレットキー取得APIのレスポンス
	/// </summary>
	public class SigninLowResponse : SignupLowResponse { }
}
