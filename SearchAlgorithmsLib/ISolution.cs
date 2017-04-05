namespace SearchAlgorithmsLib
{
    public interface ISolution<T>
    {
        void Add(T state);
        T Get();
    }
}
