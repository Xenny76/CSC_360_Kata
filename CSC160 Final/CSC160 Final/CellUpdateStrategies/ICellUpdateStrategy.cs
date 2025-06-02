namespace CSC160_Final.CellUpdateStrategies
{
    public interface ICellUpdateStrategy
    {
        public void Update(List<List<Cell>> cells, Grid gameGrid);
    }
}