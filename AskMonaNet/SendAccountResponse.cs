using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskMonaNet
{
	/// <summary>
	/// 残高情報を含んだレスポンスです。
	/// </summary>
	public class SendAccountResponse:AskMonaResponse
	{
		/// <summary>
		/// 送金後のMONA残高（watanabe単位　1MONAは100,000,000watanabeです）。
		/// </summary>
		public decimal balance;
	}
}
