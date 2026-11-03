using System;

class Program
{
    static void Main(string[] args)
    {
        Job customerService = new Job();
        customerService._company = "Alorica";
        customerService._jobTitle = "Customer Service Representative";
        customerService._startYear = 2023;
        customerService._endYear = 2024;

        Job englishTeacher = new Job();
        englishTeacher._company = "Apostolado";
        englishTeacher._jobTitle = "English Teacher";
        englishTeacher._startYear = 2024;
        englishTeacher._endYear = 2025;

        Resume resume = new Resume();
        resume._name = "Elio Diaz Jimenez";
        resume._jobs.Add(customerService);
        resume._jobs.Add(englishTeacher);
        resume.Display();
    }
}
