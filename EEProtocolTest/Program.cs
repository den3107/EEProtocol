using System;
using System.Collections.Generic;
using EEProtocol;
using PlayerIOClient;
using System.Threading;

namespace EEProtocolExample
{
    class Program
    {
        /*==================================================================================================================================================================
         * ==================================================================================================================================================================
         * 
         * Mind that the code in this project is just to test EEProtocol and should not be seen as a guide on how to make bots.
         * The important explenation part is in the OnMessage method.
         * 
         * ==================================================================================================================================================================
         * ==================================================================================================================================================================
         */

        private static AutoResetEvent resetEvent;

        static void Main(string[] args)
        {
            #region Nothing interesting here, look at OnMessage!
            // Create new ProtocolReader to fire it's static method. This will make him retrieve and parse the protocol.
            new ProtocolReader();

            // Connect to room...
            Client client = PlayerIO.QuickConnect.SimpleConnect("everybody-edits-su9rn58o40itdbnw69plyw", "guest", "guest", null);
            String serverVersion = "Everybodyedits" + client.BigDB.Load("config", "config")["version"];
            Connection conn = client.Multiplayer.CreateJoinRoom("PWpkTh6uNVa0I", serverVersion, true, null, null);
            conn.Send("init");
            conn.OnMessage += OnMessage;

            // Wait for user to exit...
            resetEvent = new AutoResetEvent(false);
            resetEvent.WaitOne();

            // Disconnect...
            conn.Disconnect();
            conn.OnMessage -= OnMessage;
            #endregion
        }
        
        private static void OnMessage(object sender, Message m)
        {
            if (m.Type == "init")
            {
                #region Get init receivable message specifications.
                #region Method #1: Using the indexer of the ReceivableMessages field.
                ReceivingMessage init = ProtocolReader.ReceivableMessages["iNiT"]; // Not case-sensitive.
                #endregion

                #region Method #2: Put the the ReceivableMessages field in a local variable, so you don't have to add "ProtocolReader." everytime.
                ReceivableMessageList receivableMessageList = ProtocolReader.ReceivableMessages;
                init = receivableMessageList["InIt"]; // Still case-insensitive
                #endregion

                #region Method #3: Use the dictionary in the ReceivableMessages field, this will make the name case-sensetive, though.
                Dictionary<String, ReceivingMessage> receivableMessages = ProtocolReader.ReceivableMessages.ReceivableMessages;
                init = receivableMessages["init"]; // This IS case-sensitive, meaning it has to be entirely lower-case.
                #endregion
                #endregion



                #region Print out parameters of init.
                #region Method #1: Using the ReceivingMessage's indexer.
                Console.WriteLine("Owner's username: " + m.GetString(init["owner username"]));
                Console.WriteLine("World name: " + m.GetString(init["wOrLd NaMe"])); // Not case-sensitive.
                #endregion

                #region Method #2: Using the static Parameters field from the ReceivingMessage and getting the id from the returned object (see method #3).
                Console.WriteLine("Joined player's x: " + m.GetInt(init.Parameters["x"].Id)); // This IS case-sensitive, meaning it has to be entirely lower-case.
                Console.WriteLine("Joined player's y: " + m.GetInt(init.Parameters["y"].Id));
                #endregion

                #region Method #3: Get ReceiveParameter object. Also gives you additional information.
                Parameter smiley = init.Parameters["smiley"]; // Once again, still case-sensitive.
                Console.WriteLine("Parameter \"{0}\" has id \"{1}\", value \"{2}\", is of type \"{3}\" and is described in the following way: \"{4}\".", smiley.Name, smiley.Id, m.GetInt(smiley.Id), smiley.Type, smiley.Description);
                #endregion
                #endregion


                Console.WriteLine("");


                #region Check if parameter exists.
                #region Method #1: Using an if statement.
                Console.WriteLine("Does the \"potion\" parameter exist using method 1?");
                if (init.Parameters.ContainsKey("potion")) // Again, still case-sensitive.
                {
                    Console.WriteLine("Yes! ... Wait wha... This excists?! What is this wizardry?!?!");
                }
                else
                {
                    Console.WriteLine("Nope!");
                }
                #endregion

                #region Method #2: Try-catching that sh*t.
                Console.WriteLine("Does the \"potion\" parameter exist using method 2?");
                try
                {
                    Parameter potion = init.Parameters["potion"];
                    Console.WriteLine("Yes! ... Wait wha... This excists?! What is this wizardry?!?!");
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine("Nope!");
                }
                #endregion
                #endregion


                Console.WriteLine("");


                #region Print all parameter names. No idea when it'd be useful though.
                Console.WriteLine("All parameter names of \"init\":");
                for (int i = 0; i < init.AllParameters.Count; i++)
                {
                    Parameter parameter = init.AllParameters[i];

                    Console.Write(parameter.Name);
                    if (i < init.AllParameters.Count - 1)
                    {
                        Console.Write(" --- ");
                    }
                }
                #endregion


                // Wait for user to exit...
                Console.WriteLine("\n\n\nPress enter to exit...");
                Console.ReadLine();
                resetEvent.Set();
            }
        }
    }
}
