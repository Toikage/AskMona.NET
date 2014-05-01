using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskMonaNet
{
	/// <summary>
	/// 残高オブジェクト
	/// </summary>
	public class AccountBalance : AskMonaResponce
	{
		/// <summary>
		/// 現在のMONA残高（watanabe単位　1MONAは100,000,000watanabeです）。
		/// </summary>
		public decimal balance;
		/// <summary>
		/// 個別勘定オブジェクト。detailに1を指定した場合のみ返却されます。
		/// </summary>
		public IndividualAccount[] accounts;
	}

	/// <summary>
	/// 個別勘定オブジェクト
	/// </summary>
	public class IndividualAccount
	{
		/// <summary>
		/// 入金済みのMONA（watanabe単位　1MONAは100,000,000watanabeです）。
		/// </summary>
		public decimal deposit;
		/// <summary>
		/// ばらまいたMONA（watanabe単位）。
		/// </summary>
		public decimal send;
		/// <summary>
		/// 受け取ったMONA（watanabe単位）。
		/// </summary>
		public decimal recieve;
		/// <summary>
		/// 出金済みのMONA（watanabe単位）。
		/// </summary>
		public decimal withdraw;
		/// <summary>
		/// 運営から受け取ったMONA（watanabe単位）。
		/// </summary>
		public decimal gift;
		/// <summary>
		/// 一時預かり中のMONA（watanabe単位）。
		/// </summary>
		public decimal reserved;
		/// <summary>
		/// 現在のMONA残高（watanabe単位）。なお、balance = deposit - send + recieve - withdraw + gift - reservedが成立します。
		/// </summary>
		public decimal balance;
	}
}
