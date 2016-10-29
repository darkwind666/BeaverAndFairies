using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace com.playGenesis.VkUnityPlugin
{
    public partial class VKPoll
    {
		public static VKPoll Deserialize (object poll)
		{
			var data = (Dictionary<string,object>)poll;
			var _poll = new VKPoll ();

			object id, owner_id, created, is_closed, question, votes, answer_id, answers;
			if (data.TryGetValue ("id", out id))
				_poll.id = (long)id;
			if (data.TryGetValue ("owner_id", out owner_id))
				_poll.owner_id = (long)owner_id;
			if (data.TryGetValue ("created", out created))
				_poll.created = (long)created;
			if (data.TryGetValue ("is_closed", out is_closed))
				_poll.is_closed = (int)(long)is_closed;
			if (data.TryGetValue ("question", out question))
				_poll.question = (string)question;
			if (data.TryGetValue ("votes", out votes))
				_poll.votes = (int)(long)votes;
			if (data.TryGetValue ("answer_id", out answer_id))
				_poll.answer_id = (long)answer_id;

			if (data.TryGetValue ("answers", out answers)) {
				var _answers=new List<VKPollAnswer>();
				var ans=(List<object>)answers;
				foreach(var a in ans)
				{
					_answers.Add(VKPollAnswer.Dererialize(a));
				}
				_poll.answers=_answers;
			}
				

			return _poll;
		}

        public long id { get; set; }

        public long owner_id { get; set; }

        public long created { get; set; }

        public int is_closed { get; set; }

      
		public string question { get; set; }
        

        public int votes { get; set; }

        public long answer_id { get; set; }

        public List<VKPollAnswer> answers { get; set; }
    }

    public partial class VKPollAnswer
    {
		public static VKPollAnswer Dererialize (object answer)
		{
			var data = (Dictionary<string,object>)answer;
			var _answer = new VKPollAnswer ();
			object id, text, votes, rate;
			if (data.TryGetValue ("id", out id))
				_answer.id = (long)id;
			if (data.TryGetValue ("text", out text))
				_answer.text = (string)text;
			if (data.TryGetValue ("votes", out votes))
				_answer.votes = (int)(long)votes;
			if (data.TryGetValue ("rate", out rate))
				_answer.rate = (double)rate;


			return _answer;
		}

        public long id { get; set; }

        
		public string text  { get; set; }
        public int votes { get; set; }
        public double rate { get; set; }
    }
}
