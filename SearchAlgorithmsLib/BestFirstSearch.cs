using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    public class BestFirstSearch<T> : Searcher<T> where T : State<T>
    {
        public override ISolution<T> Search(ISearchable<T> searchable)
        {
            Dictionary<int, T> states = new Dictionary<int, T>();
            HashSet<T> closed = new HashSet<T>();
            T current = searchable.GetInintialState();
            T goal = searchable.GetGoalState();

            current.Cost = 0;
            Push(current);
            
            while (!IsEmpty())
            {
                current = Pop();
                closed.Add(current);

                if (current.Equals(goal)) return BackTrace(current);

                List<T> succesors = searchable.GetAllPossibleState(current);
                foreach (T s in succesors)
                {
                    if (!closed.Contains(s) && !Contains(s))
                    {
                        Push(s);
                        states.Add(s.GetHashCode(), s);
                    }
                    else 
                    {
                        T lastPath;
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

        private ISolution<T> BackTrace(T state)
        {
            ISolution<T> solution = new StackSolution<T>();
            solution.Add(state);
            while (state.CameFrom != null)
            {
                state = (T) state.CameFrom;
                solution.Add(state);
            }
            return solution;
        }
    }
}
