namespace CSC160_Final
{
    public partial class MainPage : ContentPage
    {
        private readonly GameOfLifeFacade facade;
        public MainPage()
        {
            InitializeComponent();
            facade = new(GameGrid);
            facade.PopulateGrid();
        }

        private void HeightChange(object sender, ValueChangedEventArgs e)
        {
            facade.HeightChange(e);
        }

        private void WidthChange(object sender, ValueChangedEventArgs e)
        {
            facade.WidthChange(e);
        }

        private void RandomizeCells(object sender, EventArgs e)
        {
            facade.RandomizeCells(RandomPercent);
        }

        private void AdvanceOne(object sender, EventArgs e)
        {
            facade.Advance();
        }

        private void OnPausePlayClicked(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                facade.PausePlay(button);
            }
        }

        private void RulePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            facade.ChangeRuleset(RulePicker);
        }
    }
}