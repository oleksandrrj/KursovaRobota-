using System;

namespace Курсова_Робота__Щоденник_
{
    public class DiaryEntry
    {
        public int Id { get; set; }

        
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int DurationMinutes { get; set; }
        public DateTime EndDate { get; set; }

        // Конструктор без параметрів 
        public DiaryEntry() { }

        // Головний конструктор. 
        public DiaryEntry(int id, string title, string description, string location,
                          DateTime date, TimeSpan time, int durationMinutes, DateTime endDate)
        {
            Id = id;
            Title = title ?? string.Empty;
            Description = description ?? string.Empty;
            Location = location ?? string.Empty;
            Date = date;
            Time = time;
            DurationMinutes = durationMinutes;
            EndDate = endDate;
        }
    }
}