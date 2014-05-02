using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskMonaNet
{
	/// <summary>
	/// ユーザープロフィール情報を含んだレスポンスです。
	/// </summary>
	public class UserProfileResponse : AskMonaResponse
	{
		/// <summary>
		/// ユーザーの名前。
		/// </summary>
		public string u_name;
		/// <summary>
		/// ユーザーの段位。
		/// </summary>
		public string u_dan;
		/// <summary>
		/// ユーザーのプロフィール。
		/// </summary>
		public string profile;
	}
}
