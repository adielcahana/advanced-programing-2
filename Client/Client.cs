﻿using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Server;
using System.Threading;

namespace Client
{
    class Client
    {
        private NetworkStream stream;
        private StreamReader reader;
        private StreamWriter writer;
        public void start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            string answer = null;
            string command = null;
            while (true)
            {
                TcpClient client = new TcpClient();
                client.Connect(ep);
                Console.WriteLine("You are connected");
                stream = client.GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);
                {
                    answer = "";
                    Console.Write("Please enter a command: ");
                    command = Console.ReadLine();
                    writer.WriteLine(command);
                    writer.Flush();
                    // Get result from server
                    while (reader.Peek() >= 0)
                    {
                        answer += reader.ReadLine();
                        answer += "\n";
                    }
                    Console.Write(answer);
                }
                if (command.Contains("start") || command.Contains("join"))
                {
                    if (!answer.Contains("does not exist") && !answer.Contains("wrong arguments"))
                    {

                        clientMultipleGame(client, command);
                    }
                }
                stream.Close();
                reader.Close();
                writer.Close();
                client.Close();
            };
        }

        private void clientMultipleGame(TcpClient client, string state)
        {
            int ClientId = state.Contains("start") ? 0 : 1;
            string answer = "";
            string command = null;
            Console.WriteLine("start multiple game");
            Task read = new Task(() =>
            {
                do
                {
                    answer = "";
                    string msg = "";
                    do
                    {
                        msg = reader.ReadLine();
                        answer += msg;
                    } while (!msg.Equals("}") && !msg.Equals("close"));

                    if (!answer.Equals("close"))
                    {
                        Move move = Move.FromJSON(answer);
                        if (move.ClientId != ClientId)
                        {
                            Console.WriteLine(move.moveToJSON());
                        }
                    }
                } while (!answer.Equals("close"));
            });

            Task write = new Task(() =>
            {
                do
                {
                    command = Console.ReadLine();
                    writer.WriteLine(command);
                    writer.Flush();
                    // Get result from server
                } while (answer == null || !answer.Equals("close"));
            });

            read.Start();
            write.Start();

            /*var cancel = new CancellationTokenSource();
            Parallel.Invoke(() =>
            {
                write.Wait(cancel.Token);
            }, () =>
            {
                cancel.Cancel();
            });*/
            read.Wait();
            Console.WriteLine("Game end!");
        }
    }
}
