using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEProtocol
{
    public class ReceivableMessageList
    {
        public Dictionary<String, ReceivingMessage> ReceivableMessages { get; private set; }

        /// <summary>Get the message with specific name. Name is not case-sensitive.</summary>
        /// <param name="name">Name of message to obtain.</param>
        /// <returns>ReceivingMessage object with specified name.</returns>
        public ReceivingMessage this[String name]
        {
            get
            {
                return ReceivableMessages[name.ToLower()];
            }
        }

        public ReceivableMessageList()
        {
            ReceivableMessages = new Dictionary<string, ReceivingMessage>();
        }

        public void Add(String typeName, ReceivingMessage messageBody)
        {
            ReceivableMessages.Add(typeName, messageBody);
        }
    }
}
