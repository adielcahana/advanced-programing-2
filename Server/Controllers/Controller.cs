using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Server.Commands;

namespace Server
{
    /// <summary>
    /// class of base controller
    /// </summary>
    /// <seealso cref="Server.IController" />
    class Controller : IController
    {
        /// <summary>
        /// dicitonary of name and command class
        /// </summary>
        protected Dictionary<string, ICommand> commands;

        /// <summary>
        /// constructor of the <see cref="Controller"/> class.
        /// </summary>
        public Controller()
        {
            commands = new Dictionary<string, ICommand>();
        }

        /// <summary>
        /// exectue command according the arguments.
        /// </summary>
        /// <param name="commandLine"></param>
        /// <param name="client">The client that send the command.</param>
        /// <returns>
        /// the answer of the execute
        /// </returns>
        public virtual string ExecuteCommand(string commandLine, TcpClient client = null)
        {
            // get the command name
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
            if (!commands.ContainsKey(commandKey))
                return "Command not found\n";
            // get the args for the command
            string[] args = arr.Skip(1).ToArray();
            ICommand command = commands[commandKey];
            // execute the command
            return command.Execute(args, client);
        }
    }
}