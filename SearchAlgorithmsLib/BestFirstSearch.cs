using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    class BestFirstSearch<T> : Searcher<T> where T : State<T>
    {
        public override Solution Search(Isearchable<T> searchable)
        {
            HashSet<T> closed = new HashSet<T>();
            T current = searchable.getInintialState();
            T goal = searchable.getGoalState();

            current.Cost = 0;
            this.Push(current);
            
            while (!this.IsEmpty())
            {
                current = this.Pop();
                closed.Add(current);

                if (current.Equals(goal)) return Backtrace(current);

                List<T> succesors = searchable.getAllPossibleState(current);
                foreach (T s in succesors)
                {
                    if (!closed.Contains(s) && !this.Contains(s))
                    {
                        this.Push(s);
                    }
                    else if (!this.Contains)
                    {
                       
                    }
                }

            }
            
        }
    }
}
