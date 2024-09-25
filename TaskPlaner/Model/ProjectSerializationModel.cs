using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPlaner.Model
{
    public class ProjectSerializationModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Task> Tasks;

        public ProjectSerializationModel() { }
        public ProjectSerializationModel(Project project)
        {
            Name = project.Name;
            Description = project.Description;

            Tasks = (from el in project.Tasks
                     select el.ViewedTask).ToList();
        }
    }
}
