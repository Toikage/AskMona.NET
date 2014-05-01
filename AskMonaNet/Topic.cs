using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskMonaNet
{

	/// <summary>
	/// トピック取得APIのレスポンス。
	/// </summary>
	public class TopicList : AskMonaResponce
	{
		/// <summary>
		/// トピックオブジェクトのリスト。
		/// </summary>
		public TopicObject[] topics;
	}

	/// <summary>
	/// トピックオブジェクト。
	/// </summary>
	public class TopicObject
	{
		/// <summary>
		/// トピックの順位。レス取得APIでは返しません。
		/// </summary>
		public int rank;
		/// <summary>
		/// トピックID。
		/// </summary>
		public int t_id;
		/// <summary>
		/// トピックのタイトル。
		/// </summary>
		public string title;
		/// <summary>
		/// トピックのカテゴリ。
		/// </summary>
		public string category;
		/// <summary>
		/// トピック本文のリード。
		/// </summary>
		public string lead;
		/// <summary>
		/// 作成された時刻（UNIX時刻）。
		/// </summary>
		public int created;
		/// <summary>
		/// 更新された時刻（UNIX時刻）。
		/// </summary>
		public int updated;
		/// <summary>
		/// レスの数。
		/// </summary>
		public int count;
		/// <summary>
		/// やり取りされたMONA（watanabe単位　1MONAは100,000,000watanabeです）。
		/// </summary>
		public string recieve;
		/// <summary>
		/// お気に入り登録者数。
		/// </summary>
		public int favorites;

	}

	/// <summary>
	/// レス取得APIのレスポンス。
	/// </summary>
	public class ResponceList : AskMonaResponce
	{
		/// <summary>
		/// 結果。1は成功、0は失敗、2は更新なしを意味します。
		/// </summary>
		public new int status;
		/// <summary>
		/// 更新された時刻（UNIX時刻）。
		/// </summary>
		public int updated;
		/// <summary>
		/// トピックオブジェクト。topic_detailに1を指定した場合のみ返却されます。
		/// </summary>
		public TopicObject topic;
		/// <summary>
		/// レスポンスオブジェクトのリスト。
		/// </summary>
		public ResponceObject[] responces;

	}

	/// <summary>
	/// レスポンスオブジェクト。
	/// </summary>
	public class ResponceObject
	{
		/// <summary>
		/// レス番号。
		/// </summary>
		public int r_id;
		/// <summary>
		/// 投稿された時刻（UNIX時刻）。
		/// </summary>
		public int created;
		/// <summary>
		/// 投稿した人のユーザーID。
		/// </summary>
		public int u_id;
		/// <summary>
		/// 投稿した人の名前。
		/// </summary>
		public string u_name;
		/// <summary>
		/// 投稿した人の段位。
		/// </summary>
		public string u_dan;
		/// <summary>
		/// 投稿した人の投稿回数について情報。たとえば、５回中３回目の投稿の場合、文字列「3/5」で返されます。
		/// </summary>
		public string u_times;
		/// <summary>
		/// やり取りされたMONA（watanabe単位　1MONAは100,000,000watanabeです）。
		/// </summary>
		public string recieve;
		/// <summary>
		/// やり取りされたMONAをレベル分けしたもの。現状、レベルは0から7までの整数値です。
		/// </summary>
		public int res_lv;
		/// <summary>
		/// そのレスにMONAを送った人の数。
		/// </summary>
		public int rec_count;
		/// <summary>
		/// レス本文。
		/// </summary>
		public string responce;

	}

}
