using System.Text.Json;

namespace todolist_maui
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnDoneChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.BindingContext is TodoViewModel todo)
            {
                if (BindingContext is TodoListViewModel viewModel)
                {
                    viewModel.SaveTasks();
                }
            }
        }
    }

}
