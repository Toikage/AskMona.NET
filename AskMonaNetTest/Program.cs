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
			AskMonaClient am = new AskMonaClient(338, "A+sj/Q3CFpHfoY1TrjliTwJtjIzwTzZ9fiY+1cfWSoX0=");

			/*
			var t = am.GetTopicList(TopicOrder.updated);
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

			var r = am.GetTopicResponceList(537, 1, 1000, 1);
			if (r.status == 1)
			{
				Console.WriteLine(r.topic.title);
				foreach (var item in r.responces)
				{
					Console.WriteLine("{0} {1} by {2}{3}", item.r_id, item.responce, item.u_name, item.u_dan);
				}
			}
			else
			{
				if (r.status == 2) Console.WriteLine("更新なし");
				else Console.WriteLine(r.error);
			}

			var u = am.GetUserProfile(338);
			if (u.status == 1)
			{
				Console.WriteLine("No.338 {0}{1}\n{2}", u.u_name, u.u_dan, u.profile);
			}
			else
			{
				Console.WriteLine(u.error);
			}*/

			/*var t = am.GetVerifyUser(AskMonaUser.CreateFromAuthCode(Console.ReadLine()));
			Console.WriteLine(t.status);
			Console.WriteLine(t.error);*/
		}
	}
}
