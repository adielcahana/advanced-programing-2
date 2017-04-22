using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace MessagingLib
{
    public class Message
    {
        private Int32 _msgSize;

        public string Msg { get; }

        public Message(string msg)
        {
            _msgSize = msg.Length;
            Msg = msg;
        }

        public string ToJson()
        {
            JObject msg = new JObject();
            msg["size"] = _msgSize;
            msg["message"] = Msg;
            return msg.ToString();
        }

        public static Message FromJson(string msg)
        {
            JObject msgJson = JObject.Parse(msg);
            string massege = (string) msgJson["message"];
            return new Message(massege);
        }
    }

    static class Constants
    {
        public const int StartSize = 12;
        public const int EndSize = 12;
    }
}
