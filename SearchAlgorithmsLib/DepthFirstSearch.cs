using System;
using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    public class DepthFirstSearch<T> : Searcher<T>
    {

        public override ISolution<T> Search(ISearchable<T> searchable)
        {
            State<T> root = searchable.GetInintialState();
            State<T> goal = searchable.GetGoalState();
            State<T> current = null;
            root.Cost = 0;
            Push(root);

            while (!IsEmpty())
            {
                current = Pop();
                if (current.Equals(goal)) return BackTrace(current);

                List<State<T>> succesors = searchable.GetAllPossibleState(current);
                foreach (State<T> s in succesors)
                {
                    if (s.CameFrom == null && s != current) s.CameFrom = current;
                    Push(s);
                }
            }
            return null;
        }
    }
}
