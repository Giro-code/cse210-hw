using System;
using System.Collections.Generic;

public class PromptGenerator
{
    private readonly List<string> _prompts = new List<string>
    {
        "What small step helped you learn something today?",
        "Who helped you today, and what did you appreciate?",
        "What problem did you work through, and what would you try next time?",
        "What moment of peace or gratitude would you like to remember?",
        "How did you help someone become more confident today?",
        "What meaningful conversation would you like to remember?"
    };
    private readonly Random _random = new Random();

    public string GetRandomPrompt()
    {
        return _prompts[_random.Next(_prompts.Count)];
    }
}
