using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskMonaNet
{
	public abstract class AAMReqObject
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
