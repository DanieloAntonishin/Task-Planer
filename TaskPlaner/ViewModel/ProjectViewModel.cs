using System;
using TaskPlaner.Model;
using TaskPlaner.Commands;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace TaskPlaner.ViewModel
{
    public delegate void ProjectViewModelDelegate(ProjectViewModel PVM);

    public class ProjectViewModel : ViewModelBase
    {
        private event ProjectViewModelDelegate _DeleteProject;
        private event ProjectViewModelDelegate DeleteProject
        {
            add { _DeleteProject += value; }
            remove { _DeleteProject -= value; }
        }

        // Project. init not to change value after constructor 
        public Project ViewedProject { get; init; }
        public ICommand AddTask { get; set; }
        public ICommand Delete { get; set; }
        public ICommand Save { get; set; }


        #region Access to Project props
        public string Name
        {
            get => ViewedProject.Name;
            set
            {
                if (ViewedProject.Name != value)
                {
                    ViewedProject.Name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string Description
        {
            get => ViewedProject.Description;
            set
            {
                if (ViewedProject.Description != value)
                {
                    ViewedProject.Description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        // Getting tasks to show in view
        public ObservableCollection<TaskViewModel> Tasks
        {
            get => ViewedProject.Tasks;
            set { ViewedProject.Tasks = value; }
        }
        #endregion

        public ProjectViewModel(ProjectDelegate SaveAction, ProjectViewModelDelegate DeleteAction, Project projectToView = null)
        {
            ViewedProject = projectToView ?? new();
            AddTask = new DelegateCommand(param => ViewedProject.AddTask());
            Save = new DelegateCommand(param => SaveAction(ViewedProject));
            Delete = new DelegateCommand(param => _DeleteProject.Invoke(this));
            DeleteProject += DeleteAction;
        }
    }
}
