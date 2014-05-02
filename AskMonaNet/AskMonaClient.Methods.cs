using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AskMonaNet
{

	/// <summary>
	/// AskMona APIのラップを提供します。
	/// </summary>
	/// <remarks>
	/// Code by @Key(''
	/// monacoin:MERefjYe7hunLPtg7AKy1AoSwxjYU6X8Zr
	/// </remarks>
	public partial class AskMonaClient
	{
		/// <summary>
		/// インスタンス初期化時にアプリケーション登録情報が指定されたかどうかを示します。
		/// </summary>
		public bool isAuthApplication { get; private set; }

		/// <summary>
		/// アプリケーションIDを取得します。
		/// </summary>
		public int app_id { get { if (!isAuthApplication) throw new Exception("アプリケーション登録情報がありません。"); return _app_id; } private set { _app_id = value; } }
		private int _app_id = 0;

		/// <summary>
		/// 開発者シークレットキーを取得します。
		/// </summary>
		public string app_secretkey { get { if (!isAuthApplication) throw new Exception("アプリケーション登録情報がありません。"); return _app_secretkey; } private set { _app_secretkey = value; } }
		private string _app_secretkey = string.Empty;

		/// <summary>
		/// 新しいインスタンスを初期化します。
		/// 公開APIのみが利用可能です。
		/// </summary>
		public AskMonaClient()
		{
			isAuthApplication = false;
		}

		/// <summary>
		/// アプリケーション登録情報を使用して、新しいインスタンスを初期化します。
		/// </summary>
		/// <param name="app_id">アプリケーションID（開発者のユーザーID）。</param>
		/// <param name="app_secretkey">API利用登録の際に発行された、開発者シークレットキー。</param>
		public AskMonaClient(int app_id, string app_secretkey)
		{
			this.app_id = app_id;
			this.app_secretkey = app_secretkey;
			isAuthApplication = true;
		}

		/// <summary>
		/// トピック取得API
		/// トピックの一覧を取得することができます。
		/// </summary>
		/// <param name="order">並べ替える条件。</param>
		/// <param name="limit">取得するトピックの個数。最大値は1000です。</param>
		/// <param name="offset">取得するトピックのオフセット（取得開始位置）。最大値は9000です。</param>
		/// <returns></returns>
		public TopicListResponce GetTopicList(TopicOrder order = TopicOrder.updated, int limit = 25, int offset = 0)
		{
			return Call<TopicListResponce>(@"http://askmona.org/v1/topics/list", new Dictionary<string, string>
			{
				{"order",order.ToString()},
				{"limit", limit.ToString()},
				{"offset" , offset.ToString()}
			});
		}

		/// <summary>
		/// レス取得API
		/// 特定のトピックのレスを取得することができます。
		/// </summary>
		/// <param name="t_id">トピックID。</param>
		/// <param name="from">取得するレス番号の開始位置。最小値は1です。</param>
		/// <param name="to">取得するレス番号の終了位置。最大値は1000です。指定しなかった場合は、fromで指定した１つのレスのみを返します。（AskMonaNetではできません。）</param>
		/// <param name="topic_detail">トピック情報を取得すかどうか。0を指定した場合取得しません。1を指定した場合、トピック情報を取得します。</param>
		/// <param name="if_updated_since">前回の問い合わせ時の更新時刻（UNIX時刻）を指定してください。更新がなければ、statusに2を返して、内容は返しません。負荷低減にご協力をお願いします。</param>
		/// <returns></returns>
		public ResponceListResponce GetResponceList(int t_id, int from = 1, int to = 1, int topic_detail = 0, int if_updated_since = 0)
		{
			return Call<ResponceListResponce>(@"http://askmona.org/v1/responces/list", new Dictionary<string, string>
			{
				{"t_id",t_id.ToString()},
				{"from",from.ToString()},
				{"to",to.ToString()},
				{"topic_detail", topic_detail.ToString()},
				{"if_updated_since",if_updated_since.ToString()}
			});
		}

		/// <summary>
		/// 特定のユーザーのプロフィールを取得することができます。
		/// </summary>
		/// <param name="u_id">ユーザーID。</param>
		public UserProfileResponce GetUserProfile(int u_id)
		{
			return Call<UserProfileResponce>(@"http://askmona.org/v1/users/profile"
				, new Dictionary<string, string> { { "u_id", u_id.ToString() } });
		}

		/// <summary>
		/// レス投稿API
		/// トピックに投稿することができます。
		/// </summary>
		/// <param name="user">ユーザー認証オブジェクト。</param>
		/// <param name="t_id">投稿するトピックの、トピックID。</param>
		/// <param name="text">レス本文。文字数は1024字以内、改行は15行以内という制約があります。</param>
		/// <returns></returns>
		public AskMonaResponce PostResponce(AskMonaUser user, int t_id, string text)
		{
			return PostAuth<AskMonaResponce>("http://askmona.org/v1/responces/post", user, new Dictionary<string, string>
			{ 
				{"t_id",t_id.ToString()},
				{"text",text}
			});
		}

		/// <summary>
		/// お気に入り取得API
		/// お気に入りに登録したトピックの一覧を取得すことができます。
		/// </summary>
		/// <param name="user"></param>
		/// <param name="order">並べ替える条件。'added'はお気に入りに追加された時刻順、'updated'はトピックが更新された順で並べ替えられます。</param>
		/// <param name="limit">取得するお気に入りの個数。最大値は200です。</param>
		/// <param name="offset">取得するお気に入りのオフセット（取得開始位置）。最大値は200です。</param>
		/// <returns></returns>
		public TopicListResponce GetFavoriteTopicList(AskMonaUser user, FavoriteTopicOrder order = FavoriteTopicOrder.added, int limit = 200, int offset = 0)
		{
			return PostAuth<TopicListResponce>(@"http://askmona.org/v1/favorites/list", user, new Dictionary<string, string>
			{
				{"order",order.ToString()},
				{"limit", limit.ToString()},
				{"offset" , offset.ToString()}
			});
		}


		/// <summary>
		/// MONA送金API
		/// 特定のレスにMONAを送金することができます。
		/// </summary>
		/// <param name="user">ユーザー認証オブジェクト。</param>
		/// <param name="t_id">送金したいレスの、トピックID。</param>
		/// <param name="r_id">送金したいレスの、レス番号。</param>
		/// <param name="amount">送金したいMONAの額。watanabe単位で指定してください（1MONAは100,000,000watanabeで、amountは整数値をとります）。たとえば、12MONAを送りたい場合は、amount='1200000000'となります。また、仕様により、一度に100万MONA以上の額は送れません。</param>
		/// <param name="anonymous">匿名で送金するかどうか。0を指定した場合、ユーザーIDとユーザー名を送金相手に知らせることができます。1を指定した場合、匿名で送金します。</param>
		/// <returns></returns>
		public SendAccountResponce SendAccountToResponce(AskMonaUser user, int t_id, int r_id, decimal amount, int anonymous = 1)
		{
			return PostAuth<SendAccountResponce>(@"http://askmona.org/v1/account/send", user, new Dictionary<string, string>
			{
				{"t_id",t_id.ToString()},
				{"r_id",r_id.ToString()},
				{"amount",amount.ToString("整数")},
				{"anonymous",anonymous.ToString()}
			});
		}

		/// <summary>
		/// MONA送金API
		/// 特定のユーザーにMONAを送金することができます。
		/// </summary>
		/// <param name="user">ユーザー認証オブジェクト。</param>
		/// <param name="to_u_id">送金したい相手のユーザーID。</param>
		/// <param name="amount">送金したいMONAの額。watanabe単位で指定してください（1MONAは100,000,000watanabeで、amountは整数値をとります）。たとえば、12MONAを送りたい場合は、amount='1200000000'となります。また、仕様により、一度に100万MONA以上の額は送れません。</param>
		/// <param name="anonymous">匿名で送金するかどうか。0を指定した場合、ユーザーIDとユーザー名を送金相手に知らせることができます。1を指定した場合、匿名で送金します。</param>
		/// <returns></returns>
		public SendAccountResponce SendAccountAccountToUser(AskMonaUser user, int to_u_id, decimal amount, int anonymous = 1)
		{
			return PostAuth<SendAccountResponce>(@"http://askmona.org/v1/account/send", user, new Dictionary<string, string>
			{
				{"to_u_id",to_u_id.ToString()},
				{"amount",amount.ToString("整数")},
				{"anonymous",anonymous.ToString()}
			});
		}

		/// <summary>
		/// 残高取得API
		/// ユーザーの残高に関する情報を取得することができます。
		/// </summary>
		/// <param name="user">ユーザー認証オブジェクト。</param>
		/// <param name="detail">個別の勘定を取得するかどうか。0を指定した場合取得しません。1を指定した場合、個別の勘定を取得します。</param>
		/// <returns></returns>
		public AccountBalanceResponce GetAccountBalance(AskMonaUser user, int detail = 0)
		{
			return PostAuth<AccountBalanceResponce>("http://askmona.org/v1/account/balance", user, new Dictionary<string, string>
			{ 
				{"detail",detail.ToString()}
			});
		}

		/// <summary>
		/// シークレットキー検証API
		/// 認証キーの作成に使うシークレットキーが有効か判断できます。有効かどうかは、すべての要認証APIのエラー情報から確かめられますので、通常は他のAPIを使用して検証してください。なお、有効でない場合は、要認証API利用チャートの手順2からやり直してください。
		/// </summary>
		/// <param name="user">ユーザー認証オブジェクト。</param>
		/// <returns></returns>
		public AskMonaResponce GetVerifyUser(AskMonaUser user)
		{
			return PostAuth<AskMonaResponce>("http://askmona.org/v1/auth/verify", user, new Dictionary<string, string>());
		}

		/*Ask Mona登録API

このAPIを使うと、新しくAsk Monaにアカウントを作成することができます。また、自動的にアプリケーション連携を行い、認証キーの作成に使うシークレットキーを取得することができます。

POST http://askmona.org/v1/auth/signup
パラメータ	説明
app_id (required)	開発者のユーザーID。
app_secretkey (required)	API利用登録の際に発行された、開発者シークレットキー。
u_address (required)	利用者が登録に使うMonacoinアドレス。
u_name (optional)	利用者の名前。最大で12文字です。指定しない場合は「名無し」となります。
pass (required)	利用者が登録に使うパスワード。6字以上で指定してください。
agree (default = 0)	Ask Mona利用規約に同意するかどうか。同意する場合、1を指定して下さい。同意しない場合は登録できません。
レスポンス	説明
status (integer)	結果。1は成功、0は失敗を意味します。
error (string)	エラーの場合の追加情報。
u_id (integer)	利用者のユーザーID。
secretkey (string)	認証キーの作成に使うシークレットキー。*/

		//public AskMonaResponce PostSignupLow(string u_address, string u_name, string pass, int agree=0)
		//{
			
		//}
	}
}
