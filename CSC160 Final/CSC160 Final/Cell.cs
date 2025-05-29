using System.ComponentModel;
namespace CSC160_Final
{
    public class Cell : INotifyPropertyChanged
    {
        private bool _isAlive = false;
        public bool IsAlive
        {
            get => _isAlive;
            set
            {
                if (_isAlive != value)
                {
                    _isAlive = value;
                    OnPropertyChanged(nameof(IsAlive));
                }
            }
        }
        public byte Row { get; set; }
        public byte Column { get; set; }
        public Command ToggleCommand { get; set; }
        public Cell()
        {
            ToggleCommand = new Command(ToggleState);
        }
        private void ToggleState()
        {
            IsAlive = !IsAlive;
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}