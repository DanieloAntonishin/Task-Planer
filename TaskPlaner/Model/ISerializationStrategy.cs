using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPlaner.Model
{
    // Interface for strategy to serialization (xml, json, etc)
    public interface ISerializationStrategy
    {
        string Ending { get; }
        void Serialize(Project projectToSerialize, string path);
        Project Deserialize(string path);
    }
}
