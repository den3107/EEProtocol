using System;

namespace EEProtocol
{
    /// <summary>Container class holding all information about a single message's parameter.</summary>
    public class Parameter
    {
        /// <summary>Id of parameter.</summary>
        public uint Id { get; private set; }
        /// <summary>String representation of parameter type.</summary>
        public String Type { get; private set; }
        /// <summary>Name of parameter.</summary>
        public String Name { get; private set; }
        /// <summary>Description of parameter (lower case).</summary>
        public String Description { get; private set; }
        
        public Parameter(uint id, String type, String name, String description)
        {
            Id = id;
            Type = type;
            Name = name.ToLower();
            Description = description;
        }
    }
}
