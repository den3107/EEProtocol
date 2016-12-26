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
        public List<Parameter> AllParameters
        {
            get
            {
                return Parameters.Values.ToList();
            }
        }

        /// <summary>A dictionary containing all ReceiveParameter objects, indexed by name.</summary>
        public Dictionary<String, Parameter> Parameters { get; private set; }

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

        /// <summary>Constructor to create a ReceivingMessage and fill it's fields.</summary>
        /// <param name="name">Name of the message type.</param>
        /// <param name="parameters">All parameters that this message type contains.</param>
        public ReceivingMessage(String name, List<Parameter> parameters)
        {
            Name = name.ToLower();
            Parameters = new Dictionary<String, Parameter>();
            foreach (Parameter rp in parameters)
            {
                Parameters.Add(rp.Name.ToLower(), rp);
            }
        }
    }
}
