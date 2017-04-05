using System.Collections.Generic;
using SearchAlgorithmsLib;
using MazeLib;

namespace advanced_programing_2
{
    class MazeAdapter : ISearchable<Position>
    {
        private Maze maze;

        public MazeAdapter(Maze maze)
        {
            this.maze = maze;
        }

        public List<State<Position>> GetAllPossibleState(State<Position> state)
         {
            List<State<Position>> succesors = new List<State<Position>>();
            State<Position> succesor;
            State<Position> parent = state.CameFrom;
            CellType free = CellType.Free;
            int row = state.Data.Row;
            int col = state.Data.Col;
            float cost = state.Cost + 1;
           
            if (row + 1 < maze.Rows && maze[row + 1, col] == free)
            {
                succesor = new State<Position>(new Position(row + 1, col));
                if (parent == null || !succesor.Equals(parent))
                {
                    succesor.CameFrom = state;
                    succesor.Cost = cost;
                    succesors.Add(succesor);
                }
            }
            if (col + 1 < maze.Cols && maze[row , col + 1] == free)
            {
                succesor = new State<Position>(new Position(row, col + 1));
                if (parent == null || !succesor.Equals(parent))
                {
                    succesor.CameFrom = state;
                    succesor.Cost = cost;
                    succesors.Add(succesor);
                }
            }
            if (row - 1 >= 0 && maze[row - 1, col] == free)
            {
                succesor = new State<Position>(new Position(row - 1, col));
                if (parent == null || !succesor.Equals(parent))
                {
                    succesor.CameFrom = state;
                    succesor.Cost = cost;
                    succesors.Add(succesor);
                }
            }
            if (col - 1 >= 0 && maze[row, col - 1] == free)
            {
                succesor = new State<Position>(new Position(row, col - 1));
                if (parent == null || !succesor.Equals(parent))
                {
                    succesor.CameFrom = state;
                    succesor.Cost = cost;
                    succesors.Add(succesor);
                }
            }
            return succesors;
        }

        public State<Position> GetGoalState()
        {
            return new State<Position>(maze.GoalPos);
        }

        public State<Position> GetInintialState()
        {
            return new State<Position>(maze.InitialPos);
        }
    }
}
