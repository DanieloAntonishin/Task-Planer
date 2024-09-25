using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace TaskPlaner.Model
{
    class XmlSerialization : ISerializationStrategy
    {
        XmlSerializer serializer;

        public string Ending { get; } = ".xml";

        public XmlSerialization() =>
            serializer = new XmlSerializer(typeof(ProjectSerializationModel));

        //Xml Deserialization
        public Project Deserialize(string path)
        {
            ProjectSerializationModel temp;
            using (StreamReader reader = new StreamReader(path))
            {
                temp = serializer.Deserialize(reader) as ProjectSerializationModel;
            }

            Project result = new Project
            {
                Name = temp.Name,
                Description = temp.Description
            };

            foreach (var el in temp.Tasks)
                result.AddTask(el);

            return result;
        }

        //Xml Serialization
        public void Serialize(Project projectToSerialize, string path)
        {
            ProjectSerializationModel model = new ProjectSerializationModel(projectToSerialize);
            if (model.Name != string.Empty)
            {
                using (StreamWriter writer = new StreamWriter(path + projectToSerialize.Name + ".xml"))
                    serializer.Serialize(writer, model);
            }
        }
    }
}
