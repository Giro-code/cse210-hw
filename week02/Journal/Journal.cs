using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class Journal
{
    private List<Entry> _entries = new List<Entry>();
    private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        IncludeFields = true,
        WriteIndented = true
    };

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void DisplayAll()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("Your journal is empty. Choose Write to add an entry.");
            return;
        }
        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }

    public void SaveToFile(string filename)
    {
        string json = JsonSerializer.Serialize(_entries, _jsonOptions);
        File.WriteAllText(filename, json);
    }

    public void LoadFromFile(string filename)
    {
        string json = File.ReadAllText(filename);
        List<Entry> loadedEntries = JsonSerializer.Deserialize<List<Entry>>(json, _jsonOptions);
        if (loadedEntries == null)
        {
            throw new JsonException("The file must contain a JSON list of journal entries.");
        }
        foreach (Entry entry in loadedEntries)
        {
            if (entry == null || string.IsNullOrWhiteSpace(entry._date) ||
                string.IsNullOrWhiteSpace(entry._promptText) ||
                string.IsNullOrWhiteSpace(entry._entryText))
            {
                throw new JsonException("Every entry needs a date, prompt, and response.");
            }
        }
        // Replace entries only after the entire file has been validated.
        _entries = loadedEntries;
    }

    public void Search(string keyword)
    {
        int matches = 0;
        foreach (Entry entry in _entries)
        {
            if (entry._entryText.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                entry._promptText.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            {
                entry.Display();
                matches++;
            }
        }
        Console.WriteLine($"Matching entries: {matches}");
    }
}
