namespace SearchAlgorithmsLib
{
    public interface ISearcher<T> where T : State<T>
    {
        ISolution<T> Search(ISearchable<T> searchable);
        int GetNumberOfNodesEvaluated();
    }
}
