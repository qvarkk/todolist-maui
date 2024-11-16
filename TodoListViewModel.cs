using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace todolist_maui
{
    internal class TodoListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TodoViewModel> Todolist { 
            get => _todoList;
        }

        public string NewTodo
        {
            get => _newTodo;
            set
            {
                if (_newTodo != value)
                {
                    _newTodo = value;
                    OnPropertyChanged(nameof(NewTodo));
                }
            }
        }

        public ObservableCollection<TodoViewModel> _todoList;
        public string _newTodo = "";

        public ICommand AddTodoCommand { get; }
        public ICommand DeleteTodoCommand { get; }

        public TodoListViewModel()
        {
            _todoList = LoadTasks();

            AddTodoCommand = new Command(
                () => 
                {
                    _todoList.Add(new TodoViewModel(NewTodo, false));
                    SaveTasks();
                }
            );

            DeleteTodoCommand = new Command<TodoViewModel>(
                x =>
                {
                    _todoList.Remove(x);
                    SaveTasks();
                }
            );
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<TodoViewModel> LoadTasks()
        {
            var tasksJson = Preferences.Get("Tasks", "");

            if (tasksJson == "")
                return new ObservableCollection<TodoViewModel>();

            return JsonSerializer.Deserialize<ObservableCollection<TodoViewModel>>(tasksJson) ?? new ObservableCollection<TodoViewModel>();
        }

        public void SaveTasks()
        {
            var tasksJson = JsonSerializer.Serialize(Todolist);
            Preferences.Set("Tasks", tasksJson);
        }

    }
}
