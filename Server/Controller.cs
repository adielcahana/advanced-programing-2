using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Server.Commands;

namespace Server
{
    class Controller : IController
    {
        protected Dictionary<string, ICommand> commands;
        public Controller(){}

        public string ExecuteCommand(string commandLine, TcpClient client = null)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
            if (!commands.ContainsKey(commandKey))
                return "Command not found\n";
            string[] args = arr.Skip(1).ToArray();
            ICommand command = commands[commandKey];
            return command.Execute(args, client);
        }
    }
}