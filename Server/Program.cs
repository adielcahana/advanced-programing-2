namespace Server
{
    class Program
    {
        private static void Main(string[] args)
        {
            IModel model = new MazeModel();
            IController controller = new ServerController(model);
            IClientHandler ch = new ClientHandler(controller);
            Server server = new Server(8000, ch);
            server.Start();
            server.Stop();
        }
    }
}
