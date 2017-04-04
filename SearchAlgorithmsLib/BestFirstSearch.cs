using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class BestFirstSearch<T> : Searcher<T> where T : State<T>
    {
        public override ISolution<T> Search(Isearchable<T> searchable)
        {
            Dictionary<int, T> states = new Dictionary<int, T>();
            HashSet<T> closed = new HashSet<T>();
            T current = searchable.getInintialState();
            T goal = searchable.getGoalState();

            current.Cost = 0;
            this.Push(current);
            
            while (!this.IsEmpty())
            {
                current = this.Pop();
                closed.Add(current);

                if (current.Equals(goal)) return BackTrace(current);

                List<T> succesors = searchable.getAllPossibleState(current);
                foreach (T s in succesors)
                {
                    if (!closed.Contains(s) && !this.Contains(s))
                    {
                        this.Push(s);
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
                            this.Update(lastPath);
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
