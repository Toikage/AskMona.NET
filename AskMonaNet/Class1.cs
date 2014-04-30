using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AskMonaNet
{
    public static partial class AskMonaNet
    {
		JObject GetTopicList(TopicOrder order,int limit,int offset)
		{
			return Call("", string.Format("?order={0}", order.ToString(), limit, offset));
			//JsonConvert.
		}

		
    }

	/// <summary>
	/// 並べ替える条件。'updated'は更新順、'created'は作成順、'favorites'はお気に入り順、'recieve'はやり取りされたMONA順で並べ替えられます。
	/// </summary>
	public enum TopicOrder
	{
		updated,created,favorites,recieve
	}

}
