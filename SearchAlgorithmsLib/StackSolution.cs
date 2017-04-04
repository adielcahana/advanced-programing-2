using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    class StackSolution<T> : ISolution<T>
    {
        private Stack<T> states;
        public StackSolution()
        {
            states = new Stack<T>();
        }

        public void Add(T state)
        {
            states.Push(state);
        }

        public T Get()
        {
            return states.Pop();
        }
    }
}
