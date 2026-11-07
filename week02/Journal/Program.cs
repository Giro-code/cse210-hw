using System;
using System.IO;
using System.Text;
using System.Text.Json;

class Program
{
    // Exceeds requirements: JSON preserves commas, quotes, Unicode, and newlines;
    // keyword search helps revisit entries, and failed loads keep the current journal.
    // AI assistance: OpenAI Codex assisted with implementation and verification.
    static void Main(string[] args)
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;
        Journal journal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();
        Console.WriteLine("Welcome to the Journal Program!");

        while (true)
        {
            Console.WriteLine("\nPlease select one of the following choices:");
            Console.WriteLine("1. Write\n2. Display\n3. Load\n4. Save\n5. Quit\n6. Search");
            Console.Write("What would you like to do? ");
            string choice = Console.ReadLine();
            if (choice == null || choice == "5")
            {
                break;
            }

            try
            {
                switch (choice)
                {
                    case "1":
                        string prompt = promptGenerator.GetRandomPrompt();
                        Console.WriteLine(prompt);
                        Console.Write("> ");
                        string response = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(response))
                        {
                            Console.WriteLine("An empty entry was not added.");
                            break;
                        }
                        journal.AddEntry(new Entry
                        {
                            _date = DateTime.Now.ToShortDateString(),
                            _promptText = prompt,
                            _entryText = response
                        });
                        Console.WriteLine("Entry added. Choose Save to keep your entries in a file.");
                        break;
                    case "2":
                        journal.DisplayAll();
                        break;
                    case "3":
                        Console.Write("Filename to load: ");
                        journal.LoadFromFile(ReadFilename());
                        Console.WriteLine("Journal loaded. It replaces the previous entries.");
                        break;
                    case "4":
                        Console.Write("Filename to save (for example, journal.json): ");
                        journal.SaveToFile(ReadFilename());
                        Console.WriteLine("Journal saved.");
                        break;
                    case "6":
                        Console.Write("Search for a word or phrase: ");
                        string keyword = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(keyword))
                        {
                            Console.WriteLine("Enter a word or phrase to search.");
                            break;
                        }
                        journal.Search(keyword);
                        break;
                    default:
                        Console.WriteLine("Please enter a menu number from 1 to 6.");
                        break;
                }
            }
            catch (Exception error) when (error is IOException ||
                error is UnauthorizedAccessException || error is JsonException ||
                error is ArgumentException || error is NotSupportedException)
            {
                Console.WriteLine($"The file operation could not be completed: {error.Message}");
                Console.WriteLine("Your current journal entries have been kept.");
            }
        }
        Console.WriteLine("Goodbye!");
    }

    static string ReadFilename()
    {
        string filename = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(filename))
        {
            throw new ArgumentException("A filename is required.");
        }
        return filename.Trim();
    }
}
