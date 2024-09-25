using System;
using System.Windows.Input;
using TaskPlaner.Model;
using System.Collections.ObjectModel;
using TaskPlaner.Commands;
using System.IO;

namespace TaskPlaner.ViewModel
{
    public class MainViewModel : ViewModelBase, IDisposable
    {
        private string Path = "../../../_SavedProjects/";

        public ObservableCollection<ProjectViewModel> Projects { get; set; }
        public ICommand AddProjectCommand { get; set; }

        public ProjectSerializer Serializer = null;
        public ICommand UseXmlSerialization { get; set; }

        public MainViewModel()
        {
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);

            Serializer = new ProjectSerializer(new XmlSerialization());
            Projects = new ObservableCollection<ProjectViewModel>();

            AddProjectCommand = new DelegateCommand(param => Projects.Add(new ProjectViewModel(SaveProject, DeleteProject)));
            Projects.Add(new ProjectViewModel(SaveProject, DeleteProject));

            LoadProjects();
        }

        private void LoadProjects()
        {
            if (Serializer != null)
            {
                foreach (string el in Directory.GetFiles(Path))
                    if (el.EndsWith(Serializer.Ending))
                        Projects.Add(new ProjectViewModel(SaveProject, DeleteProject, Serializer.Deserialize(el)));
            }
        }

        private void SaveProject(Project projectToSave) =>
            Serializer?.Serialize(Path, projectToSave);

        private void DeleteProject(ProjectViewModel toDelete) =>
            Projects.Remove(toDelete);

        public void Dispose()
        {
            foreach (string el in Directory.GetFiles(Path))
                File.Delete(el);

            foreach (ProjectViewModel el in Projects)
                Serializer?.Serialize(Path, el.ViewedProject);
        }
    }
}
