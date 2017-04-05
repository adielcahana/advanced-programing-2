using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    public interface ISearchable<T> where T : State<T>
    {
        T GetInintialState();
        T GetGoalState();
        List<T> GetAllPossibleState(T state);
    }
}
