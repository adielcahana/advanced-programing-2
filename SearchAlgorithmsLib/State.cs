using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class State<T>
    {
        private T state;
        private float cost;
        private State<T> cameFrom;

        public State(T state)
        {
            this.state = state;
        }

        public float Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        public State<T> CameFrom
        {
            get { return cameFrom; }
            set { cameFrom = value; }
        }
 
        public bool Equals(State<T> s)
        {
            return state.Equals(s.state);
        }
    }
}
