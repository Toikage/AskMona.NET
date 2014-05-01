using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskMonaNet
{
	public class SendAccountResponce:AskMonaResponce
	{
		/// <summary>
		/// 送金後のMONA残高（watanabe単位　1MONAは100,000,000watanabeです）。
		/// </summary>
		public decimal balance;
	}
}
