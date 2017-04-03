using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public interface Isearchable<T> where T : State<T>
    {
        T getInintialState();
        T getGoalState();
        List<T> getAllPossibleState(T state);
    }
}
