namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller controller = new Controller();
            ClientHandler ch = new ClientHandler(controller);
            Server server = new Server(8000, ch);
            server.Start();
            server.Stop();
        }
    }
}
