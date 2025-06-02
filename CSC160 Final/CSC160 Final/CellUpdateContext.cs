using CSC160_Final.CellUpdateStrategies;

namespace CSC160_Final
{
    public class CellUpdateContext
    {
        public ICellUpdateStrategy UpdateStrategy { get; set; } = new ConwayUpdateStrategy();
        public void Advance(List<List<Cell>> cells, Grid gameGrid)
        {
            UpdateStrategy.Update(cells, gameGrid);
        }
    }
}