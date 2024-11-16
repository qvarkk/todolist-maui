using System.ComponentModel;

namespace todolist_maui
{
    internal class TodoViewModel : INotifyPropertyChanged
    {
        private string _text;
        private bool _done;

        public string Text { 
            get => _text;
            set 
            { 
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged(nameof(Text));
                }
            } 
        }

        public bool Done
        {
            get => _done;
            set
            {
                if (_done != value)
                {
                    _done = value;
                    OnPropertyChanged(nameof(Done));
                }
            }
        }

        public TodoViewModel(string text, bool done) 
        {
            _text = text;
            _done = done;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
