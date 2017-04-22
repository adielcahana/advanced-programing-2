using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingLib
{
    class MessageWriter
    {
        private TextWriter _writer;

        public MessageWriter(TextWriter writer)
        {
            _writer = writer;
        }

        public void WriteMessage(string msg)
        {
            Message message = new Message(msg);
            _writer.Write(message.ToJson());
        }
    }
}
