using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public interface Isearcher<T> where T : State<T>
    {
        ISolution<T> Search(Isearchable<T> searchable);
        int GetNumberOfNodesEvaluated();
    }
}
