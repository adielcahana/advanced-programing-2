using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    public class BestFirstSearch<T> : Searcher<T>
    {
        public override ISolution<T> Search(ISearchable<T> searchable)
        {
            Dictionary<int, State<T>> states = new Dictionary<int, State<T>>();
            HashSet<State<T>> closed = new HashSet<State<T>>();
            State<T> current = searchable.GetInintialState();
            State<T> goal = searchable.GetGoalState();

            current.Cost = 0;
            Push(current);
            
            while (!IsEmpty())
            {
                current = Pop();
                closed.Add(current);
                states.Remove(current.GetHashCode());

                if (current.Equals(goal)) return BackTrace(current);

                List<State<T>> succesors = searchable.GetAllPossibleState(current);
                foreach (State<T> s in succesors)
                {
                    if (!closed.Contains(s) && !Contains(s))
                    {
                        Push(s);
                        states.Add(s.GetHashCode(), s);
                    }
                    else 
                    {
                        State<T> lastPath;
                        states.TryGetValue(s.GetHashCode(), out lastPath);
                        if (s.Cost < lastPath.Cost)
                        {
                            lastPath.Cost = s.Cost;
                            lastPath.CameFrom = s.CameFrom;
                            Update(lastPath);
                        } 
                    }
                }
            }
            return null;
        }

        private ISolution<T> BackTrace(State<T> state)
        {
            ISolution<T> solution = new StackSolution<T>();
            solution.Add(state);
            while (state.CameFrom != null)
            {
                state = state.CameFrom;
                solution.Add(state);
            }
            return solution;
        }
    }
}
