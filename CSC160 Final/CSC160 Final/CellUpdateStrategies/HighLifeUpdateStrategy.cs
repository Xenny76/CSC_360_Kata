using CSC160_Final.Interfaces;
namespace CSC160_Final.CellUpdateStrategies
{
    public class HighLifeUpdateStrategy : ICellUpdateStrategy
    {
        public void Update(List<List<Cell>> cells, Grid gameGrid)
        {
            List<List<bool>> copy = [];
            foreach (List<Cell> row in cells)
            {
                List<bool> rowCopy = [];
                foreach (Cell cell in row)
                {
                    rowCopy.Add(cell.IsAlive);
                }
                copy.Add(rowCopy);
            }
            foreach (List<Cell> row in cells)
            {
                foreach (Cell cell in row)
                {
                    byte amountAlive = AmountAlive(cell, copy, gameGrid);
                    if (amountAlive < 2 || amountAlive > 3) cell.IsAlive = false;
                    else if (!cell.IsAlive && amountAlive is 3 or 6) cell.IsAlive = true;
                }
            }
        }
        private static byte AmountAlive(Cell c, List<List<bool>> copy, Grid GameGrid)
        {
            byte count = 0;
            if (c.Row != 0 && c.Column != 0 && copy[c.Row - 1][c.Column - 1]) count++; // Top Left
            if (c.Row != 0 && copy[c.Row - 1][c.Column]) count++; // Top
            if (c.Row != 0 && c.Column != GameGrid.ColumnDefinitions.Count - 1 && copy[c.Row - 1][c.Column + 1]) count++; // Top Right
            if (c.Column != GameGrid.ColumnDefinitions.Count - 1 && copy[c.Row][c.Column + 1]) count++; // Right
            if (c.Row != GameGrid.RowDefinitions.Count - 1 && c.Column != GameGrid.ColumnDefinitions.Count - 1 && copy[c.Row + 1][c.Column + 1]) count++; // Bottom Right
            if (c.Row != GameGrid.RowDefinitions.Count - 1 && copy[c.Row + 1][c.Column]) count++; // Bottom
            if (c.Row != GameGrid.RowDefinitions.Count - 1 && c.Column != 0 && copy[c.Row + 1][c.Column - 1]) count++; // Bottom Left
            if (c.Column != 0 && copy[c.Row][c.Column - 1]) count++; // Left
            return count;
        }
    }
}