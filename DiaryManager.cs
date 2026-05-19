using System;
using System.Collections.Generic;
using System.Linq;

namespace Курсова_Робота__Щоденник_
{
    public class DiaryManager
    {
       
        private List<DiaryEntry> _entries = new List<DiaryEntry>();

        public List<DiaryEntry> Entries => _entries;

        // Метод редагування
        public bool UpdateEntryProperty(int id, string propertyName, object? newValue)
        {
            
            var entry = _entries.FirstOrDefault(e => e.Id == id);
            if (entry == null) return false;

            
            string valueStr = newValue?.ToString() ?? string.Empty;

            switch (propertyName)
            {
                case "Title": 
                    if (string.IsNullOrWhiteSpace(valueStr))
                        throw new ArgumentException("Назва справи не може бути порожньою!");
                    entry.Title = valueStr;
                    break;

                case "Description": 
                    entry.Description = valueStr;
                    break;

                case "Location": 
                    entry.Location = valueStr;
                    break;

                case "Date": 
                    entry.Date = Convert.ToDateTime(valueStr).Date;
                    break;

                case "Time": 
                    if (TimeSpan.TryParse(valueStr, out TimeSpan parsedTime))
                        entry.Time = parsedTime;
                    else
                        throw new ArgumentException("Некоректний формат часу!");
                    break;

                case "DurationMinutes": 
                    int duration = Convert.ToInt32(valueStr);
                    if (duration < 0) throw new ArgumentException("Тривалість не може бути від'ємною!");
                    entry.DurationMinutes = duration;
                    break;

                case "EndDate": 
                    DateTime end = Convert.ToDateTime(valueStr).Date;
                    if (end < entry.Date)
                        throw new ArgumentException("Дата закінчення не може бути ранішою за дату початку!");
                    entry.EndDate = end;
                    break;

                default:
                    return false;
            }

            return true;
        }
    }
}