using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AskMonaNet;

namespace AskMonaNetTest
{
	class Program
	{
		static void Main(string[] args)
		{
			
			AskMonaClient am = new AskMonaClient();
			{
				Console.WriteLine("トピックの一覧を取得します。");
				Console.ReadKey(true);
				var t = am.GetTopicList();
				if (t.status == 1)
				{
					Console.WriteLine("最新{0}トピック一覧", t.topics.Length);
					foreach (var i in t.topics)
					{
						Console.WriteLine(@"{0}({1}) {2}", i.title, i.count, i.t_id);
					}
				}
				else
				{
					Console.WriteLine(t.error);
				}
			}

			{
				Console.WriteLine("トピックNo.1(http://askmona.org/1)のトピックのレスを1から1000までを取得します。");
				Console.ReadKey(true);
				var r = am.GetResponseList(1, 1, 1000, 1);
				if (r.status == 1)
				{
					Console.WriteLine(r.topic.title);
					foreach (var item in r.Responses)
					{
						Console.WriteLine("{0} {1} by {2}{3}", item.r_id, item.Response, item.u_name, item.u_dan);
					}
				}
				else
				{
					if (r.status == 2) Console.WriteLine("更新なし");
					else Console.WriteLine(r.error);
				}
			}

			{
				Console.WriteLine("ユーザー番号338のプロフィールを取得します。");
				Console.ReadKey(true);
				var u = am.GetUserProfile(338);
				if (u.status == 1)
				{
					Console.WriteLine("{0}{1}\n{2}", u.u_name, u.u_dan, u.profile);
				}
				else
				{
					Console.WriteLine(u.error);
				}
			}
			
			Console.ReadKey(true);
		}
	}
}
