namespace Server
{
    class Program
    {
        private static void Main(string[] args)
        {
            MazeModel model = new MazeModel();
            Controller controller = new Controller(model);
            ClientHandler ch = new ClientHandler(controller);
            Server server = new Server(8000, ch);
            server.Start();
            server.Stop();
        }
    }
}
