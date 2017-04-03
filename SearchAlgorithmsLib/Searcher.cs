using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
    public abstract class Searcher<T> : Isearcher<T> where T : State<T>
    {
        private SimplePriorityQueue<T> openList;
        private int evaluatedNodes;

        public Searcher()
        {
            openList = new SimplePriorityQueue<T>();
            evaluatedNodes = 0;
        }

        public int GetNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }

        public T Pop()
        {
            evaluatedNodes++;
            return openList.Dequeue();
        }

        public void Push(T state)
        {
            openList.Enqueue(state , state.Cost);
        }

        public bool IsEmpty()
        {
            return openList.Count == 0;
        }

        public void Update(T state, float priority)
        {
            openList.UpdatePriority(state, priority);
        }

        public bool Contains(T state)
        {
            return openList.Contains(state);
        }

        public bool Get(T state)
        {
            foreach(T s in openList)
            {
                if (s.Equals(state)) return s;
            }
        }

        public abstract Solution Search(Isearchable<T> searchable);
    }
}
