namespace Server
{
    /// <summary>
    /// main method class of the server
    /// </summary>
    class Program
    {
        /// <summary>
        /// Mains.
        /// </summary>
        /// <param name="args">The arguments for the main.</param>
        private static void Main(string[] args)
        {
            IModel model = new MazeModel();
            IController controller = new ServerController(model);
            IClientHandler ch = new ClientHandler(controller);
            Server server = new Server(56789, ch);
            server.Start();
            server.Stop();
        }
    }
}
