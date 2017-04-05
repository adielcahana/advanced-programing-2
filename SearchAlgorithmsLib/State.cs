namespace SearchAlgorithmsLib
{
    public class State<T>
    {

        public State(T data)
        {
            this.Data = data;
            this.Cost = 0;
            this.CameFrom = null;
        }

        public T Data { get; }

        public float Cost { get; set; }

        public State<T> CameFrom { get; set; }

        public bool Equals(State<T> s)
        {
            return Data.Equals(s.Data);
        }

        public override int GetHashCode()
        {
            return Data.ToString().GetHashCode();
        }
    }
}
