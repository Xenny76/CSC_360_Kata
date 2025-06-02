using CSC160_Final.CellUpdateStrategies;
namespace CSC160_Final
{
    public partial class MainPage : ContentPage
    {
        private List<List<Cell>> cells = [];
        private bool running = false;
        private CancellationTokenSource? cancellationTokenSource = null;
        private CellUpdateContext cellUpdateContext = new();
        public MainPage()
        {
            InitializeComponent();
            PopulateGrid();
        }
        private void HeightChange(object sender, ValueChangedEventArgs e)
        {
            if (e.OldValue < e.NewValue)
            {
                GameGrid.RowDefinitions.Add(new RowDefinition());
                AddRow();
            }
            else if (e.OldValue > e.NewValue)
            {
                RemoveRow();
                GameGrid.RowDefinitions.RemoveAt(GameGrid.RowDefinitions.Count - 1);
            }
        }
        private void WidthChange(object sender, ValueChangedEventArgs e)
        {
            if (e.OldValue < e.NewValue)
            {
                GameGrid.ColumnDefinitions.Add(new ColumnDefinition());
                AddColumn();
            }
            else if (e.OldValue > e.NewValue)
            {
                RemoveColumn();
                GameGrid.ColumnDefinitions.RemoveAt(GameGrid.ColumnDefinitions.Count - 1);
            }

        }
        private void PopulateGrid()
        {
            for (int i = 0; i < GameGrid.RowDefinitions.Count; i++)
            {
                cells.Add([]);
                for (int j = 0; j < GameGrid.ColumnDefinitions.Count; j++)
                {
                    Cell cell = new();
                    Button c = new()
                    {
                        AutomationId = $"Cell{i}_{j}",
                        MinimumHeightRequest = 15,
                        MinimumWidthRequest = 15,
                        Padding = 0,
                        Margin = 0,
                        CornerRadius = 0,
                        BorderColor = Color.FromArgb("#333333"),
                        BorderWidth = 0.4,
                        BindingContext = cell
                    };
                    c.SetBinding(Button.BackgroundColorProperty, new Binding("IsAlive", BindingMode.Default, new BoolToColorConverter()));
                    c.Clicked += (s, e) => cell.ToggleCommand.Execute(null);
                    Grid.SetRow(c, i);
                    cell.Row = (byte)i;
                    Grid.SetColumn(c, j);
                    cell.Column = (byte)j;
                    GameGrid.Children.Add(c);
                    cells[i].Add(cell);
                }
            }
        }
        private void AddRow()
        {
            // Get to the last row and populate all grid cells
            int i = GameGrid.RowDefinitions.Count - 1;
            cells.Add([]);
            for (int j = 0; j < GameGrid.ColumnDefinitions.Count; j++)
            {
                Cell cell = new();
                Button c = new()
                {
                    AutomationId = $"Cell{i}_{j}",
                    MinimumHeightRequest = 15,
                    MinimumWidthRequest = 15,
                    Padding = 0,
                    Margin = 0,
                    CornerRadius = 0,
                    BorderColor = Color.FromArgb("#333333"),
                    BorderWidth = 0.4,
                    BindingContext = cell
                };
                c.SetBinding(Button.BackgroundColorProperty, new Binding("IsAlive", BindingMode.Default, new BoolToColorConverter()));
                c.Clicked += (s, e) => cell.ToggleCommand.Execute(null);
                Grid.SetRow(c, i);
                cell.Row = (byte)i;
                Grid.SetColumn(c, j);
                cell.Column = (byte)j;
                GameGrid.Children.Add(c);
                cells[i].Add(cell);
            }
        }
        private void RemoveRow()
        {
            // Get to the last row and remove all checkboxes
            int i = GameGrid.RowDefinitions.Count - 1;
            for (int j = 0; j < GameGrid.ColumnDefinitions.Count; j++)
            {
                GameGrid.Remove(GameGrid.Children.Where(c => c.AutomationId.Equals($"Cell{i}_{j}")).ToArray()[0]);
            }
            cells.RemoveAt(i);
        }
        private void AddColumn()
        {
            // Get to the last column and populate all grid cells
            int j = GameGrid.ColumnDefinitions.Count - 1;
            for (int i = 0; i < GameGrid.RowDefinitions.Count; i++)
            {
                Cell cell = new();
                Button c = new()
                {
                    AutomationId = $"Cell{i}_{j}",
                    MinimumHeightRequest = 15,
                    MinimumWidthRequest = 15,
                    Padding = 0,
                    Margin = 0,
                    CornerRadius = 0,
                    BorderColor = Color.FromArgb("#333333"),
                    BorderWidth = 0.4,
                    BindingContext = cell
                };
                c.SetBinding(Button.BackgroundColorProperty, new Binding("IsAlive", BindingMode.Default, new BoolToColorConverter()));
                c.Clicked += (s, e) => cell.ToggleCommand.Execute(null);
                Grid.SetRow(c, i);
                cell.Row = (byte)i;
                Grid.SetColumn(c, j);
                cell.Column = (byte)j;
                GameGrid.Children.Add(c);
                cells[i].Add(cell);
            }
        }
        private void RemoveColumn()
        {
            // Get to the last column and remove all checkboxes
            int j = GameGrid.ColumnDefinitions.Count - 1;
            for (int i = 0; i < GameGrid.RowDefinitions.Count; i++)
            {
                GameGrid.Remove(GameGrid.Children.Where(c => c.AutomationId.Equals($"Cell{i}_{j}")).ToArray()[0]);
                cells[i].RemoveAt(j);
            }
        }
        private void RandomizeCells(object sender, EventArgs e)
        {
            Random r = new();
            foreach (List<Cell> row in cells)
            {
                foreach (Cell cell in row)
                {
                    cell.IsAlive = false;
                    double numEntered;
                    bool entered = double.TryParse(RandomPercent.Text, out numEntered);
                    cell.IsAlive = entered ? r.NextDouble() < numEntered : r.NextDouble() < 0.5;
                }
            }
        }
        private void Advance()
        {
            cellUpdateContext.Advance(cells, GameGrid);
        }
        private void AdvanceOne(object sender, EventArgs e)
        {
            Advance();
        }
        [Obsolete]
        private void OnPausePlayClicked(object sender, EventArgs e)
        {
            if (running)
            {
                cancellationTokenSource?.Cancel();
                running = false;
                (sender as Button).Text = "Play";
            }
            else
            {
                cancellationTokenSource = new CancellationTokenSource();
                running = true;
                (sender as Button).Text = "Pause";
                Device.StartTimer(TimeSpan.FromMilliseconds(1000 / 15), () =>
                {
                    if (cancellationTokenSource.Token.IsCancellationRequested) return false;
                    Advance();
                    return true;
                });
            }
        }
        private void RulePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            cellUpdateContext.UpdateStrategy = RulePicker.SelectedIndex switch
            {
                0 => new ConwayUpdateStrategy(),
                1 => new HighLifeUpdateStrategy(),
                2 => new DayAndNightUpdateStrategy(),
                _ => new ConwayUpdateStrategy(),
            };
        }
    }
}