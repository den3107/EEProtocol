using System;
using System.Collections.Generic;
using System.Linq;

namespace EEProtocol
{
    /// <summary>Container class for a receivable messages's name and it's parameters.</summary>
    public class ReceivingMessage
    {
        /// <summary>Name of message (lower case).</summary>
        public String Name { get; private set; }

        /// <summary>A list containing all ReceiveParameter objects.</summary>
        public List<ReceiveParameter> AllParameters
        {
            get
            {
                return Parameters.Values.ToList();
            }
        }

        /// <summary>A dictionary containing all ReceiveParameter objects, indexed by name.</summary>
        public Dictionary<String, ReceiveParameter> Parameters { get; private set; }

        /// <summary>Returns the id of the parameter with specified name. Name is not case-sensitive.</summary>
        /// <param name="name">Name of parameter id to find.</param>
        /// <returns>Id of parameter. Max value of an unsigned integer if id is variable or a list.</returns>
        public uint this[String name]
        {
            get
            {
                return Parameters[name.ToLower()].Id;
            }
        }

        public ReceivingMessage(String name, List<ReceiveParameter> parameters)
        {
            Name = name.ToLower();
            Parameters = new Dictionary<String, ReceiveParameter>();
            foreach (ReceiveParameter rp in parameters)
            {
                Parameters.Add(rp.Name, rp);
            }
        }
    }
}
