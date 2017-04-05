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
            State<Position> s;
            int row = state.Data.Row;
            int col = state.Data.Col;
            float cost = state.Cost + 1;

            if (row + 1 < maze.Rows && maze[row + 1, col] == 0)
            {
                s = new State<Position>(new Position(row + 1, col));
                if (!s.Equals(state))
                {
                    s.Cost = cost;
                    succesors.Add(s);
                }
            }
            if (col + 1 < maze.Cols && maze[row , col + 1] == 0)
            {
                s = new State<Position>(new Position(row, col + 1));
                if (!s.Equals(state))
                {
                    s.Cost = cost;
                    succesors.Add(s);
                }
            }
            if (row - 1 >= 0 && maze[row - 1, col] == 0)
            {
                s = new State<Position>(new Position(row - 1, col));
                if (!s.Equals(state))
                {
                    s.Cost = cost;
                    succesors.Add(s);
                }
            }
            if (col - 1 >= 0 && maze[row, col - 1] == 0)
            {
                s = new State<Position>(new Position(row, col - 1));
                if (!s.Equals(state))
                {
                    s.Cost = cost;
                    succesors.Add(s);
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
