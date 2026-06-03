using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace Курсова_Робота__Щоденник_
{
    public class DiaryManager
    {
        private BindingList<DiaryEntry> _entries = new BindingList<DiaryEntry>();

        public BindingList<DiaryEntry> Entries => _entries;

        public DiaryManager() 
        {

        }


        // Метод пошуку за датою
        public List<int> GetEntryIdsByDate(DateTime targetDate)
        {
            List<int> ids = new List<int>();
            string targetDateString = targetDate.ToString("dd.MM.yyyy"); 

            for (int i = 0; i < _entries.Count; i++)
            {
                // Перевіряємо, чи дата в записі (теж як текст) збігається
                if (_entries[i].Date.ToString("dd.MM.yyyy") == targetDateString)
                {
                    ids.Add(_entries[i].Id);
                }
            }
            return ids;
        }

        // Метод пошуку за назвою
        public List<int> GetEntryIdsByTitle(string titlePart)
        {
            
            if (string.IsNullOrWhiteSpace(titlePart)) return new List<int>();

            
            string cleanQuery = titlePart.Trim();

            return _entries.Where(e => e.Title != null &&
                                       e.Title.Contains(cleanQuery, StringComparison.OrdinalIgnoreCase))
                           .Select(e => e.Id)
                           .ToList();
        }

        // Метод пошуку завтрашніх справ
        public List<int> GetTomorrowEntryIds()
        {
            DateTime tomorrow = DateTime.Today.AddDays(1);

            return _entries.Where(e => e.Date.Date == tomorrow.Date)
                           .Select(e => e.Id)
                           .ToList();
        }

        // Метод пошуку вчорашніх справ
        public List<int> GetYesterdayEntryIds()
        {
            DateTime yesterday = DateTime.Today.AddDays(-1);

            return _entries.Where(e => e.Date.Date == yesterday.Date)
                           .Select(e => e.Id)
                           .ToList();
        }

        // Метод видалення рядка
        public void RemoveEntryAt(int index)
        {
            if (index >= 0 && index < _entries.Count)
            {
                _entries.RemoveAt(index);
            }
        }

        // Метод очищення даних
        public void ClearEntryField(int rowIndex, string columnName)
        {
            
            var entry = _entries.FirstOrDefault(e => e.Id == rowIndex);
            if (entry == null) return;

            switch (columnName)
            {
                case "TitleColumn": entry.Title = ""; break;
                case "DescColumn": entry.Description = ""; break;
                case "PlaceColumn": entry.Location = ""; break;
            }

        }



        // Метод дублювання
        public void UpdateEntryField(int rowId, string columnName, string value)
        {
            
            var entry = _entries.FirstOrDefault(e => e.Id == rowId);

            
            if (entry == null)
            {
                entry = new DiaryEntry { Id = rowId };
                _entries.Add(entry);
            }

            
            switch (columnName)
            {
                case "TitleColumn": entry.Title = value; break;
                case "DescColumn": entry.Description = value; break;
                case "PlaceColumn": entry.Location = value; break;
                case "DateColumn": 
                    if (DateTime.TryParse(value, out DateTime d)) entry.Date = d;
                    break;
            }
        }


       

        // Метод для переносу дати на завтра
        public void UpdateDate(int rowId, string columnName, string newDateString)
        {
            var entry = _entries.FirstOrDefault(e => e.Id == rowId);
            if (entry == null) return;

            
            if (DateTime.TryParse(newDateString, out DateTime newDate))
            {
                
                if (columnName == "DateColumn") entry.Date = newDate;
                else if (columnName == "EndDateColumn") entry.EndDate = newDate;
                
            }
        }
        // Метод для аналізу накладок
        public List<string> CheckForOverlaps(List<(int RowIndex, string Title, DateTime Start, DateTime End)> entries)
        {
            List<string> overlaps = new List<string>();

            for (int i = 0; i < entries.Count; i++)
            {
                for (int j = i + 1; j < entries.Count; j++)
                {
                    var a = entries[i];
                    var b = entries[j];

                    
                    if (a.Start == DateTime.MinValue || b.Start == DateTime.MinValue) continue;

                    
                    if (a.Start.Date == b.Start.Date && a.Start < b.End && b.Start < a.End)
                    {
                        overlaps.Add($"⚠️ НАКЛАДКА: рядок {a.RowIndex} та {b.RowIndex}\n" +
                                     $"   \"{a.Title}\" ({a.Start:HH:mm} - {a.End:HH:mm})\n" +
                                     $"   \"{b.Title}\" ({b.Start:HH:mm} - {b.End:HH:mm})");
                    }
                }
            }
            return overlaps;
        }

        // Метод нагадування
        public (string Title, string Date, string Time, string Remaining)? GetNearestEntry(IEnumerable<DataGridViewRow> rows)
        {
            DateTime now = DateTime.Now;
            string bestTitle = "";
            string bestDate = "";
            string bestTime = "";
            TimeSpan minDiff = TimeSpan.MaxValue;
            bool found = false;

            foreach (var row in rows)
            {
                if (row.IsNewRow) continue;

                string dateStr = row.Cells["DateColumn"].Value?.ToString() ?? "";
                string timeStr = row.Cells["TimeColumn"].Value?.ToString() ?? "";

                if (DateTime.TryParse($"{dateStr} {timeStr}", out DateTime entryTime))
                {
                    if (entryTime > now)
                    {
                        TimeSpan diff = entryTime - now;
                        if (diff < minDiff)
                        {
                            minDiff = diff;
                            bestTitle = row.Cells["TitleColumn"].Value?.ToString() ?? "Без назви";
                            bestDate = dateStr;
                            bestTime = timeStr;
                            found = true;
                        }
                    }
                }
            }

            if (found)
            {
                
                DateTime targetTime = DateTime.Parse($"{bestDate} {bestTime}");
                TimeSpan remaining = targetTime - now;

                string remainingText = remaining.Days > 0
                    ? $"{remaining.Days} днів, {remaining.Hours} год, {remaining.Minutes} хв"
                    : $"{remaining.Hours} год, {remaining.Minutes} хв";

                return (bestTitle, bestDate, bestTime, remainingText);
            }
            return null;
        }

        // Метод завантаження 

        public string GenerateExportReport(IEnumerable<DataGridViewRow> rows)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("==================================================");
            sb.AppendLine("            ПОВНИЙ СПИСОК ВАШИХ СПРАВ            ");
            sb.AppendLine("==================================================");
            sb.AppendLine($"Дата експорту: {DateTime.Now:dd.MM.yyyy HH:mm}");
            sb.AppendLine();

            foreach (var row in rows)
            {
                if (row.IsNewRow) continue;

                string title = row.Cells["TitleColumn"].Value?.ToString() ?? "---";
                string desc = row.Cells["DescColumn"].Value?.ToString() ?? "---";
                string place = row.Cells["PlaceColumn"].Value?.ToString() ?? "Не вказано";
                string dateStart = row.Cells["DateColumn"].Value?.ToString() ?? "";
                string timeStart = row.Cells["TimeColumn"].Value?.ToString() ?? "";
                string duration = row.Cells["DurationColumn"].Value?.ToString() ?? "---";
                string dateEnd = row.Cells["DateOfEndColumn"].Value?.ToString() ?? "---";

                string timePart = !string.IsNullOrWhiteSpace(timeStart) ? " о " + timeStart : "";
                string startInfo = (string.IsNullOrWhiteSpace(dateStart) && string.IsNullOrWhiteSpace(timePart))
                                   ? "Не вказано"
                                   : (dateStart + timePart).Trim();

                sb.AppendLine($"📌 СПРАВА: {title.ToUpper()}");
                sb.AppendLine($"📝 Опис:     {desc}");
                sb.AppendLine($"📍 Місце:    {place}");
                sb.AppendLine($"🕒 Початок:  {startInfo}");
                sb.AppendLine($"⏳ Тривалість: {duration}");
                sb.AppendLine($"🏁 Завершення: {dateEnd}");
                sb.AppendLine(new string('-', 40));
            }
            return sb.ToString();
        }
    }

}