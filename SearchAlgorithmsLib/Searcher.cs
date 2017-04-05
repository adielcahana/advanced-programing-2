using Priority_Queue;

namespace SearchAlgorithmsLib
{
    public abstract class Searcher<T> : ISearcher<T> where T : State<T>
    {
        private SimplePriorityQueue<T> _openList;
        private int _evaluatedNodes;

        public Searcher()
        {
            _openList = new SimplePriorityQueue<T>();
            _evaluatedNodes = 0;
        }

        public int GetNumberOfNodesEvaluated()
        {
            return _evaluatedNodes;
        }

        public T Pop()
        {
            _evaluatedNodes++;
            return _openList.Dequeue();
        }

        public void Push(T state)
        {

            _openList.Enqueue(state , state.Cost);
        }

        public bool IsEmpty()
        {
            return _openList.Count == 0;
        }

        public void Update(T state)
        {
            _openList.UpdatePriority(state, state.Cost);
        }

        public bool Contains(T state)
        {
            return _openList.Contains(state);
        }

        public T Get(T state)
        {
            foreach(T s in _openList)
            {
                if (s.Equals(state)) return s;
            }
            return null;
        }

        public abstract ISolution<T> Search(ISearchable<T> searchable);
    }
}
