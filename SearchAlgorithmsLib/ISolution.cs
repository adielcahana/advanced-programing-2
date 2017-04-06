using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    public interface ISolution<T> : IEnumerable<State<T>>
    {
        void Add(State<T> state);
        State<T> Get();
    }
}