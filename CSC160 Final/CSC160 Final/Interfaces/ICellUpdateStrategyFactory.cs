namespace CSC160_Final.Interfaces
{
    public interface ICellUpdateStrategyFactory<T>
    {
        T CreateStrategy(string strategyName);
    }
}