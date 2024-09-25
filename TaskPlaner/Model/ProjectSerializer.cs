using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskPlaner.Model
{
    public class ProjectSerializer
    {
        // common class for Project serialization
        public ISerializationStrategy SerializationStrategy;
        public string Ending => SerializationStrategy.Ending; // file name ending

        // Adds custom strategy to serializer
        public ProjectSerializer(ISerializationStrategy serializationStrategy) =>
            SerializationStrategy = serializationStrategy;

        //Serialize project
        public void Serialize(string path, Project projectToSerialize) =>
            SerializationStrategy.Serialize(projectToSerialize, path);

        //Deserialize project
        public Project Deserialize(string path) =>
            SerializationStrategy.Deserialize(path);
    }
}
