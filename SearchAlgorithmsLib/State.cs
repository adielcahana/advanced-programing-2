namespace SearchAlgorithmsLib
{
    public class State<T>
    {
        private T _state;

        public State(T state)
        {
            _state = state;
        }

        public float Cost { get; set; }

        public State<T> CameFrom { get; set; }

        public bool Equals(State<T> s)
        {
            return _state.Equals(s._state);
        }

        public override int GetHashCode()
        {
            return _state.ToString().GetHashCode();
        }
    }
}
