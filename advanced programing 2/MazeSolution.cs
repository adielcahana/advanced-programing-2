using System.Collections.Generic;
using System.Linq;
using System.Text;
using MazeLib;
using Newtonsoft.Json.Linq;
using SearchAlgorithmsLib;

namespace advanced_programing_2
{
    public class MazeSolution
    {
        private readonly string _name;
        private readonly int _nodesEvaluated;
        private readonly Stack<Direction> _solution;


        public MazeSolution(string name, ISolution<Position> sol, int nodesEvaluated)
        {
            _name = name;
            _solution = ToDirections(sol);
            _nodesEvaluated = nodesEvaluated;
        }

        private MazeSolution(string name, Stack<Direction> sol, int nodesEvaluated)
        {
            _name = name;
            _solution = sol;
            _nodesEvaluated = nodesEvaluated;
        }

        public float Cost { get; set; }

        public static MazeSolution FromJSON(string str)
        {
            JObject mazeSolution = JObject.Parse(str);
            string name = (string) mazeSolution["Name"];
            string directions = (string) mazeSolution["Solution"];
            List<Direction> sol = new List<Direction>(directions.Length);
            foreach (char direction in directions)
                switch (direction)
                {
                    case '0':
                        sol.Add(Direction.Left);
                        break;
                    case '1':
                        sol.Add(Direction.Right);
                        break;
                    case '3':
                        sol.Add(Direction.Up);
                        break;
                    case '4':
                        sol.Add(Direction.Down);
                        break;
                }
            int nodesEvaluated = (int) mazeSolution["NodesEvaluated"];
            return new MazeSolution(name, new Stack<Direction>(sol), nodesEvaluated);
        }

        public string ToJSON()
        {
            JObject mazeSolution = new JObject();
            mazeSolution["Name"] = _name;
            StringBuilder solution = new StringBuilder();
            foreach (Direction d in _solution)
                solution.Append(d);
            mazeSolution["Solution"] = solution.ToString();
            mazeSolution["NodesEvaluated"] = _nodesEvaluated;
            return mazeSolution.ToString();
        }

        private Stack<Direction> ToDirections(ISolution<Position> sol)
        {
            List<Direction> directions = new List<Direction>(sol.Count() - 1);
            Position last = sol.Get().Data;
            foreach (State<Position> state in sol)
            {
                Position next = state.Data;
                int nextRow = next.Row;
                int nextCol = next.Col;
                int lastRow = last.Row;
                int lastCol = last.Col;

                if (nextRow > lastRow)
                    directions.Add(Direction.Down);
                else if (nextRow < lastRow)
                    directions.Add(Direction.Up);
                else if (nextCol < lastCol)
                    directions.Add(Direction.Left);
                else
                    directions.Add(Direction.Right);
                last = next;
            }
            return new Stack<Direction>(directions);
        }
    }
}