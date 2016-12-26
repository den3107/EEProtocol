using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace EEProtocol
{
    /// <summary>
    ///     Reads the EE protocol git page and parses it's messages.
    ///     Protocol is read and parsed upon creating a reference of accessing any static member.
    ///     All values here are according to what's read from the git.
    ///     No assumptions are made, unless mentioned otherwise.
    ///     Errors might post additional information in stderr.
    /// </summary>
    /// <exception cref="System.Net.WebException">Thrown when there was an error while retrieving the protocol from the git page.</exception>
    /// <exception cref="EEProtocol.RegexMatchException">Thrown when something went wrong while retrieving data from the protocol.</exception>
    public class ProtocolReader
    {
        private static readonly String RAW_GIT_URL = "https://raw.githubusercontent.com/Tunous/EverybodyEditsProtocol/master/README.md";
        private static readonly String RGX_RAW_RECEIVE = @"#.+Receive messages.+\n((?:.*\n)+?)#(?!#)";
        private static readonly String RGX_RAW_RECEIVE_MESSAGE = @"###.+""(?<name>.+)"".+\n(?<content>(?:.*\n)+?)(?:(?=#)|$)";
        private static readonly String RGX_RECEIVE_MESSAGE_DATA = @"\|\s*`(?<id>(?:.+?|\[...\]))`\s*\|\s*`(?<type>.+?)`\s*\|\s*(?<name>.+?)\s*\|\s*(?<description>.+)";
        
        /// <summary>Dictionary containing all receivable messages, indexed by message name (lower case).</summary>
        public static ReceivableMessageList ReceivableMessages { get; private set; }
        
        /// <summary>Dictionary containing all sendable messages, indexed by message name (lower case).</summary>
        public static SendableMessageList SendableMessages { get; private set; }

        static ProtocolReader()
        {
            ReceivableMessages = new ReceivableMessageList();
            SendableMessages = new SendableMessageList();


            String rawProtocol = "";
            #region Protocol downloading, put in rawProtocol
            try
            {
                using (WebClient client = new WebClient())
                {
                    // Prevent HttpWebRequest from looking up system proxys, increasing download time by ~2-8 seconds.
                    client.Proxy = null;
                    rawProtocol = client.DownloadString(RAW_GIT_URL);
                }
            }
            catch (WebException e)
            {
                throw new WebException("Couldn't download the protocol from git.", e);
            }
            #endregion

            String rawReceive = "";
            #region Get the recieving messages part, put in rawReceive
            Match match = Regex.Match(rawProtocol, RGX_RAW_RECEIVE);
            if (match.Success)
            {
                rawReceive = match.Value;
            }
            else
            {
                throw new RegexMatchException("Couldn't find receive portion of protocol.");
            }
            #endregion

            Dictionary<String, String> rawMessages = new Dictionary<String, String>();
            #region Get all raw messages, put in rawMessages
            MatchCollection matches = Regex.Matches(rawReceive, RGX_RAW_RECEIVE_MESSAGE);
            if (matches.Count > 0)
            {
                foreach (Match m in matches)
                {
                    if (m.Groups.Count == 3)
                    {
                        rawMessages.Add(m.Groups["name"].Value, m.Groups["content"].Value);
                    }
                    else
                    {
                        Console.Error.WriteLine(m.Value);
                        throw new RegexMatchException("Found message does not comply to format.");
                    }
                }
            }
            else
            {
                throw new RegexMatchException("Couldn't find any receive messages.");
            }
            #endregion

            #region Format all raw messages, put in field ReceiveMessages
            foreach (KeyValuePair<String, String> message in rawMessages)
            {
                matches = Regex.Matches(message.Value, RGX_RECEIVE_MESSAGE_DATA);
                List<Parameter> parameters = new List<Parameter>();

                foreach (Match m in matches)
                {
                    uint id = uint.MaxValue;
                    uint.TryParse(m.Groups["id"].Value, out id);
                    parameters.Add(new Parameter(id, m.Groups["type"].Value, m.Groups["name"].Value.ToLower(), m.Groups["description"].Value));
                }

                ReceivableMessages.Add(message.Key.ToLower(), new ReceivingMessage(message.Key.ToLower(), parameters));
            }
            #endregion
        }
    }
}
