using System;
using System.Collections.Generic;

public class Note
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public bool IsCompleted { get; set; }

    public Note(string title, string description, DateTime date)
    {
        Title = title;
        Description = description;
        Date = date;
        IsCompleted = false;
    }
}

public class DailyPlanner
{
    private List<Note> notes = new List<Note>();
    private DateTime currentDate = DateTime.Today;
    private int currentNoteIndex = 0;

    public DailyPlanner()
    {
        
        notes.Add(new Note("Заметка 1", "Описание заметки 1", currentDate));
        notes.Add(new Note("Заметка 2", "Описание заметки 2", currentDate));
     
        currentDate = currentDate.AddDays(1);

        notes.Add(new Note("Заметка 3", "Описание заметки 3", currentDate));
        notes.Add(new Note("Заметка 4", "Описание заметки 4", currentDate));

        currentDate = currentDate.AddDays(1);

        notes.Add(new Note("Заметка 5", "Описание заметки 5", currentDate));
        notes.Add(new Note("Заметка 6", "Описание заметки 6", currentDate));
    }


    public void AddNote()
    {
        Console.Write("Введите название заметки: ");
        string title = Console.ReadLine();
        Console.Write("Введите описание: ");
        string description = Console.ReadLine();
        Console.Write("Введите дату (гггг-мм-дд): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
        {
            notes.Add(new Note(title, description, date));
            Console.WriteLine("Заметка добавлена.");
        }
        else
        {
            Console.WriteLine("Ошибка: Неверный формат даты.");
        }
    }

    public void ShowNotes(DateTime date)
    {
        Console.WriteLine($"Заметки на {date:dd.MM.yyyy}:");
        foreach (var note in notes)
        {
            if (note.Date.Date == date.Date)
            {
                Console.WriteLine(note.Title);
            }
        }
    }

    public void ShowMenu()
    {
        ConsoleKeyInfo key;
        do
        {
            Console.Clear();
            ShowNotes(currentDate);

            Console.WriteLine("\nИспользуйте стрелки влево/вправо для навигации.");
            Console.WriteLine("Нажмите Enter, чтобы открыть заметку.");
            Console.WriteLine("Нажмите Home, чтобы добавить новую заметку.");
            key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    currentDate = currentDate.AddDays(-1);
                    break;
                case ConsoleKey.RightArrow:
                    currentDate = currentDate.AddDays(1);
                    break;
                case ConsoleKey.Enter:
                    OpenNote();
                    break;
                case ConsoleKey.Home:
                    AddNote();
                    break;
            }
        } while (key.Key != ConsoleKey.Escape);
    }

    public void OpenNote()
    {
        if (currentNoteIndex >= 0 && currentNoteIndex < notes.Count)
        {
            Console.Clear();
            var note = notes[currentNoteIndex];
            Console.WriteLine($"Название: {note.Title}");
            Console.WriteLine($"Описание: {note.Description}");
            Console.WriteLine($"Дата: {note.Date:dd.MM.yyyy}");
            Console.WriteLine($"Завершено: {note.IsCompleted}");
            Console.WriteLine("Нажмите End, чтобы отметить как завершенное.");
            Console.WriteLine("Нажмите Backspace, чтобы удалить заметку.");
            var key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    currentNoteIndex--;
                    if (currentNoteIndex < 0)
                        currentNoteIndex = 0;
                    break;
                case ConsoleKey.RightArrow:
                    currentNoteIndex++;
                    if (currentNoteIndex >= notes.Count)
                        currentNoteIndex = notes.Count - 1;
                    break;  
                case ConsoleKey.End:
                    note.IsCompleted = true;
                    break;
                case ConsoleKey.Backspace:
                    notes.RemoveAt(currentNoteIndex);
                    currentNoteIndex = 0;
                    break;
            }
        }
    }
}