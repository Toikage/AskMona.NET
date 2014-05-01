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
			var t = AskMona.GetTopicList(TopicOrder.favorites);
			Console.WriteLine(t.error);
			Console.WriteLine(t.topics.Length);
			foreach (var i in t.topics)
			{
				Console.WriteLine(@"{0}({1}) {2}", i.title, i.count,i.t_id);
			}

			/*var r = AskMona.GetTopicResponceList(1,1,1000,1);
			Console.WriteLine(r.error);

			foreach (var i in r.responces)
			{
				Console.WriteLine(@"{0} {1}", i.r_id,i.responce);
			}*/
			var u = AskMona.GetUserProfile(2);
			Console.WriteLine(u.error);
			Console.WriteLine(u.u_name);
			return;
		}
	}
}
