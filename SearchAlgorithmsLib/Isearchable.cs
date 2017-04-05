using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    public interface ISearchable<T>
    {
        State<T> GetInintialState();
        State<T> GetGoalState();
        List<State<T>> GetAllPossibleState(State<T> state);
    }
}
