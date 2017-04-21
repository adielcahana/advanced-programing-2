using Ex1;
using MazeLib;
using System;
using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// the model interface
    /// </summary>
    interface IModel
    {
        /// <summary>
        /// Generates the maze.
        /// </summary>
        /// <param name="name">The maze name.</param>
        /// <param name="row">The row.</param>
        /// <param name="col">The col.</param>
        /// <returns>
        /// maze</returns>
        Maze GenerateMaze(string name, int row, int col);

        /// <summary>
        /// Solves the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="algorithm">The algorithm.</param>
        /// <returns> the maze solution</returns>
        MazeSolution SolveMaze(string name, Algorithm algorithm);

        /// <summary>
        /// create new game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="player1">client.</param>
        /// <returns> the maze detailes</returns>
        string NewGame(String name, int rows, int cols, TcpClient player1);

        /// <summary>
        /// player 2 join the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="player2">The player2.</param>
        /// <returns> the maze detailes </returns>
        string JoinGame(String name, TcpClient player2);

        /// <summary>
        /// Creates list of active game.
        /// </summary>
        /// <returns> list of games names</returns>
        string CreateList();

        /// <summary>
        /// Finishes the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="client">The client.</param>
        void finishGame(string name, TcpClient client);
    }
}
