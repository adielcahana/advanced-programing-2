using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Client
{
    /// <summary>
    ///     encapsulate the client capabilities
    /// </summary>
    internal class Client
    {
        private StreamReader _reader;
        private NetworkStream _stream;
        private StreamWriter _writer;

        /// <summary>
        ///     Starts this session
        /// </summary>
        public void Start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ConfigurationManager.AppSettings[0]),
                int.Parse(ConfigurationManager.AppSettings[1]));
            while (true)
            {
                TcpClient client = new TcpClient();
                client.Connect(ep);
                Console.WriteLine("You are connected");
                _stream = client.GetStream();
                _reader = new StreamReader(_stream);
                _writer = new StreamWriter(_stream);
                string answer;
                string command;
                {
                    answer = "";
                    Console.Write("Please enter a command: ");
                    command = Console.ReadLine();
                    _writer.WriteLine(command);
                    _writer.Flush();
                    // Get result from server
                    do
                    {
                        msg = _reader.ReadLine();
                        answer += msg;
                    } while (!msg.Equals("}") && !msg.Equals("]"));
                    Console.Write(answer);
                }
                if (command.Contains("start") || command.Contains("join"))
                    if (!answer.Contains("does not exist") && !answer.Contains("wrong arguments"))
                        StartMultipleGame(client, command);
                _stream.Close();
                _reader.Close();
                _writer.Close();
                client.Close();
            }
        }

        /// <summary>
        ///     starts a multiple game.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="state">The state - the command that started the game.</param>
        private void StartMultipleGame(TcpClient client, string state)
        {
            //define the client number
            int clientId = state.Contains("start") ? 0 : 1;
            string answer = "";
            string command;
            Console.WriteLine("start multiple game");
            //read and send next move/close from user
            Task write = new Task(() =>
            {
                do
                {
                    command = Console.ReadLine();
                    _writer.WriteLine(command);
                    _writer.Flush();
                    // Get result from server
                } while (!answer.Equals("close"));
            });
            //read the next state from the server and procces it
            Task read = new Task(() =>
            {
                do
                {
                    answer = "";
                    string msg;
                    do
                    {
                        msg = _reader.ReadLine();
                        answer += msg;
                    } while (!msg.Equals("}") && !msg.Equals("close"));
                    //print the second player last move
                    if (!answer.Equals("close"))
                    {
                        Move move = Move.FromJson(answer);
                        if (move.ClientId != clientId)
                            Console.WriteLine(move.ToString());
                    }
                } while (!answer.Equals("close"));
                _stream.Close();
                _reader.Close();
                _writer.Close();
                client.Close();
            });

            read.Start();
            write.Start();

            //wait for the last msg from server
            read.Wait();

            Console.WriteLine("Game ended!");
        }
    }
}