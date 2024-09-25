using System;
using TaskPlaner.Model;
using TaskPlaner.Commands;
using System.Windows.Input;

namespace TaskPlaner.ViewModel
{
    public delegate void TaskDelegate(TaskViewModel tvm);


    public class TaskViewModel : ViewModelBase
    {
        public Task ViewedTask { get; set; }

        //DeleteTask is made for callback to Project to delete Task from Project
        private event TaskDelegate _DeleteTask;
        private event TaskDelegate DeleteTask
        {
            add { _DeleteTask += value; }
            remove { _DeleteTask -= value; }
        }

        public ICommand Undo { get; set; }
        public ICommand Delete { get; set; }

        //Accessors use OnPropertyChanged method to update data in view
        #region Access to Viewed Task Field
        public string Title
        {
            get { return ViewedTask.Title; }
            set
            {
                if (ViewedTask.Title != value)
                {
                    ViewedTask.Title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        public string Description
        {
            get { return ViewedTask.Description; }
            set
            {
                if (ViewedTask.Description != value)
                {
                    ViewedTask.Description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        public string Status
        {
            get { return ViewedTask.Status; }
            set
            {
                if (ViewedTask.Status != value)
                {
                    ViewedTask.Status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        public string Priority
        {
            get { return ViewedTask.Priority; }
            set
            {
                if (ViewedTask.Priority != value)
                {
                    ViewedTask.Priority = value;
                    OnPropertyChanged(nameof(Priority));
                }
            }
        }

        //public DateTime DueDate
        //{
        //    get { return ViewedTask.DueDate; }
        //    set
        //    {
        //        if (ViewedTask.DueDate != value)
        //        {
        //            ViewedTask.DueDate = value;
        //            OnPropertyChanged(nameof(DueDate));
        //        }
        //    }
        //}
        #endregion

        //Commands for undo and Delete task from Project
        public TaskViewModel(TaskDelegate deleteFunction)
        {
            if (ViewedTask == null)
                ViewedTask = new Task();
            Undo = new DelegateCommand(param =>
            {
                ViewedTask.Undo();
                OnPropertyChanged(nameof(Title));
                OnPropertyChanged(nameof(Description));
                OnPropertyChanged(nameof(Status));
                OnPropertyChanged(nameof(Priority));
            }, param => ViewedTask.CanUndo());

            Delete = new DelegateCommand(param => _DeleteTask.Invoke(this));
            DeleteTask += deleteFunction;
        }

        // Constructor with task to give TaskViewModel deserialized Task
        public TaskViewModel(Task taskToView, TaskDelegate deleteFunction) : this(deleteFunction) =>
            ViewedTask = taskToView;

    }
}
