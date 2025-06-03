namespace CSC160_Final.Interfaces
{
    public interface ICellUpdateStrategy // This also acts as the product interface for the Factory Pattern
    {
        public void Update(List<List<Cell>> cells, Grid gameGrid);
    }
}