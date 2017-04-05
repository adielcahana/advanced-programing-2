using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    class StackSolution<T> : ISolution<T>
    {
        private Stack<T> _states;
        public StackSolution()
        {
            _states = new Stack<T>();
        }

        public void Add(T state)
        {
            _states.Push(state);
        }

        public T Get()
        {
            return _states.Pop();
        }
    }
}
