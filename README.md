# EEProtocol
Library to prevent bots from breaking after simple Everybody Edits updates.

# Currently only has support for receiving messages.
EEProtocol is a Visual Studio 2015 Enterprise project containing the source code.

EEProtocolTest is a Visual Studio 2015 Enterprise project containing some examples on how to use this library.

## In case you don't want to open up the test/example project, here is a how-to-use anyway:

### Get init receivable message specifications.
##### Method #1: Using the indexer of the ReceivableMessages field.
```C#
ReceivingMessage init = ProtocolReader.ReceivableMessages["iNiT"]; // Not case-sensitive.
```
##### Method #2: Put the the ReceivableMessages field in a local variable, so you don't have to add "ProtocolReader." everytime.
```C#
ReceivableMessageList receivableMessageList = ProtocolReader.ReceivableMessages;
init = receivableMessageList["InIt"]; // Still case-insensitive
```
##### Method #3: Use the dictionary in the ReceivableMessages field, this will make the name case-sensetive, though.
```C#
Dictionary<String, ReceivingMessage> receivableMessages = ProtocolReader.ReceivableMessages.ReceivableMessages;
init = receivableMessages["init"]; // This IS case-sensitive, meaning it has to be entirely lower-case.
```


-----
### Print out parameters of init.
##### Method #1: Using the ReceivingMessage's indexer
```C#
Console.WriteLine("Owner's username: " + m.GetString(init["owner username"]));
Console.WriteLine("World name: " + m.GetString(init["wOrLd NaMe"])); // Not case-sensitive.
```
##### Method #2: Using the static Parameters field from the ReceivingMessage and getting the id from the returned object (see method #3).
```C#
Console.WriteLine("Joined player's x: " + m.GetInt(init.Parameters["x"].Id)); // This IS case-sensitive, meaning it has to be entirely lower-case.
Console.WriteLine("Joined player's y: " + m.GetInt(init.Parameters["y"].Id));
```
##### Method #3: Get ReceiveParameter object. Also gives you additional information.
```C#
ReceiveParameter smiley = init.Parameters["smiley"]; // Once again, still case-sensitive.
Console.WriteLine("Parameter \"" + smiley.Name + "\" has id \"" + smiley.Id +
                  "\", value \"" + m.GetInt(smiley.Id) + "\", is of type \"" + smiley.Type +
                  "\" and is described in the following way: \"" + smiley.Description + "\".");
```


-----
### Check if parameter excists.
##### Method #1: Using an if statement.
```C#
Console.WriteLine("Does the \"potion\" parameter exist using method 1?");
if (init.Parameters.ContainsKey("potion")) // Again, still case-sensitive.
{
    Console.WriteLine("Yes! ... Wait wha... This excists?! What is this wizardry?!?!");
}
else
{
    Console.WriteLine("Nope!");
}
```
##### Method #2: Try-catching that sh*t.
```C#
Console.WriteLine("Does the \"potion\" parameter exist using method 2?");
try
{
    ReceiveParameter potion = init.Parameters["potion"];
    Console.WriteLine("Yes! ... Wait wha... This excists?! What is this wizardry?!?!");
}
catch (KeyNotFoundException)
{
    Console.WriteLine("Nope!");
}
```


-----
### Print all parameter names. No idea when it'd be useful though.
```C#
Console.WriteLine("All parameter names of \"init\":");
for (int i = 0; i < init.AllParameters.Count; i++)
{
    ReceiveParameter parameter = init.AllParameters[i];

    Console.Write(parameter.Name);
    if(i < init.AllParameters.Count - 1)
    {
        Console.Write(" --- ");
    }
}
```
