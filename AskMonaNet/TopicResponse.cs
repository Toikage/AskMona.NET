using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskMonaNet
{

	/// <summary>
	/// トピックオブジェクオのリストを含んだレスポンスです。
	/// </summary>
	public class TopicListResponse : AskMonaResponse
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
	/// トピック情報、レスポンスオブジェクトのリストを含んだレスポンスです。
	/// </summary>
	public class ResponseListResponse : AskMonaResponse
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
		public ResponseObject[] Responses;
	}

	/// <summary>
	/// レスポンスオブジェクト。
	/// </summary>
	public class ResponseObject
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
		public string Response;

	}


	/// <summary>
	/// トピック一覧の並べ替える条件を指定します。
	/// </summary>
	public enum TopicOrder
	{
		/// <summary>
		/// 更新順
		/// </summary>
		updated,
		/// <summary>
		/// 作成順
		/// </summary>
		created,
		/// <summary>
		/// お気に入り順
		/// </summary>
		favorites,
		/// <summary>
		/// やり取りされたMONA順で並べ替えられます
		/// </summary>
		recieve
	}

	/// <summary>
	/// お気に入りトピックリストの並べ替える条件を指定します。
	/// </summary>
	public enum FavoriteTopicOrder
	{
		/// <summary>
		/// お気に入りに追加された時刻順。
		/// </summary>
		added,
		/// <summary>
		/// トピックが更新された順。
		/// </summary>
		updated
	}

}
