namespace SearchAlgorithmsLib
{
    public interface ISearcher<T>
    {
        ISolution<T> Search(ISearchable<T> searchable);
        int GetNumberOfNodesEvaluated();
    }
}
