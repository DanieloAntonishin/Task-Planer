using System;
using System.Collections.ObjectModel;
using TaskPlaner.ViewModel;

namespace TaskPlaner.Model
{
    public delegate void ProjectDelegate(Project prj);

    public class Project
    {
        // List ob Tasks in project
        public ObservableCollection<TaskViewModel> Tasks;

        public string Name { get; set; } = "Project";
        public string Description { get; set; } = string.Empty;

        public Project() => Tasks = new();

        // Add task to project. Also gives callback method to delete task
        public void AddTask() => Tasks.Add(new TaskViewModel(DeleteTask));
        public void AddTask(Task taskToAdd) => Tasks.Add(new TaskViewModel(taskToAdd, DeleteTask));

        // Remove Task from project
        public void DeleteTask(TaskViewModel taskToDelete) => Tasks.Remove(taskToDelete);
    }
}
