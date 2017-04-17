using Priority_Queue;

namespace SearchAlgorithmsLib
{
    public abstract class Searcher<T> : ISearcher<T>
    {
        private int _evaluatedNodes;
        private readonly SimplePriorityQueue<State<T>> _openList;

        public Searcher()
        {
            _openList = new SimplePriorityQueue<State<T>>();
            _evaluatedNodes = 0;
        }

        public int GetNumberOfNodesEvaluated()
        {
            return _evaluatedNodes;
        }

        public abstract ISolution<T> Search(ISearchable<T> searchable);

        public State<T> Pop()
        {
            _evaluatedNodes++;
            return _openList.Dequeue();
        }

        public void Push(State<T> state, float priority)
        {
            _openList.Enqueue(state, priority);
        }

        public bool IsEmpty()
        {
            return _openList.Count == 0;
        }

        public void Update(State<T> state)
        {
            _openList.UpdatePriority(state, state.Cost);
        }

        public bool Contains(State<T> state)
        {
            return _openList.Contains(state);
        }

        public State<T> Get(State<T> state)
        {
            foreach (State<T> s in _openList)
                if (s.Equals(state)) return s;
            return null;
        }

        protected ISolution<T> BackTrace(State<T> state)
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

        protected void Reset()
        {
            _evaluatedNodes = 0;
        }
    }
}