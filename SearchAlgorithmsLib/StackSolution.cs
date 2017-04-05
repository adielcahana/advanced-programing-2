using System;
using System.Collections;
using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    class StackSolution<T> : ISolution<T>
    {
        private Stack<State<T>> _states;
        public StackSolution()
        {
            _states = new Stack<State<T>>();
        }

        public void Add(State<T> state)
        {
            _states.Push(state);
        }

        public State<T> Get()
        {
            return _states.Pop();
        }

        public IEnumerator<State<T>> GetEnumerator()
        {
            return _states.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _states.GetEnumerator();
        }
    }
}
