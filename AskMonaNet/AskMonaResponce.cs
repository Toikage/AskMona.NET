using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskMonaNet
{
	/// <summary>
	/// AskMona APIから返されるステータス、エラー情報を格納します。
	/// </summary>
	public class AskMonaResponce
	{
		/// <summary>
		/// 結果。1は成功、0は失敗を意味します。
		/// </summary>
		public int status;
		/// <summary>
		/// エラーの場合の追加情報。
		/// </summary>
		public string error;
	}
}
