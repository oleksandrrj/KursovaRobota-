using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Курсова_Робота__Щоденник_
{
    public partial class DiaryMain : Form
    {
        public class DiaryEntry
        {
            public string Number { get; set; } = "";
            public string Title { get; set; } = "";
            public string Description { get; set; } = "";
            public string Place { get; set; } = "";
            public DateTime Date { get; set; }
            public string Time { get; set; } = "";
            public string Duration { get; set; } = "";
            public string DateOfEnd { get; set; } = "";
        }

        List<DiaryEntry> entries = new List<DiaryEntry>();
        private string filePath = "diary_data.xml";

        public DiaryMain()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            PanelOfButton.Visible = false;


            dataGridView1.ReadOnly = true;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void SaveData()
        {
            try
            {
                dataGridView1.EndEdit();
                DataTable dt = new DataTable("DiaryEntries");

                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    dt.Columns.Add(col.Name);
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        DataRow dr = dt.NewRow();
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            dr[cell.ColumnIndex] = cell.Value ?? string.Empty;
                        }
                        dt.Rows.Add(dr);
                    }
                }
                dt.WriteXml(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при збереженні: " + ex.Message);
            }
        }

        private void LoadData()
        {
            if (File.Exists(filePath))
            {
                try
                {
                    DataTable dt = new DataTable("DiaryEntries");

                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        dt.Columns.Add(col.Name);
                    }

                    dt.ReadXml(filePath);

                    dataGridView1.Rows.Clear();
                    foreach (DataRow dr in dt.Rows)
                    {

                        dataGridView1.Rows.Add(dr.ItemArray!);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка при завантаженні: " + ex.Message);
                }
            }
        }
        private void DiaryMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveData();
            Application.Exit();

        }

        private void DiaryButton_Click(object sender, EventArgs e)
        {
            PanelOfButton.Visible = !PanelOfButton.Visible;
        }



        private void editButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
            {
                dataGridView1.ReadOnly = false;
                dataGridView1.CurrentCell.ReadOnly = false;
                dataGridView1.BeginEdit(true);
            }
        }



        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Робимо таблицю знову тільки для читання
            dataGridView1.ReadOnly = true;

            // Забираємо фокус, щоб клітинка не "світилася"
            dataGridView1.ClearSelection();
            editButton.Focus();
        }


        private void DiaryMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void PanelOfButton_Paint(object sender, PaintEventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void dataGridView1_CellValidating_1(object sender, DataGridViewCellValidatingEventArgs e)
        {

            if (dataGridView1.IsCurrentCellInEditMode)
            {

                DialogResult result = MessageBox.Show(
                    "Ви впевнені, що хочете зберегти ці зміни?",
                    "Підтвердження",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {

                    e.Cancel = true;
                }

            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            // Перевіряємо, чи вибрано хоча б одну клітинку
            if (dataGridView1.SelectedCells.Count > 0)
            {
                bool hasContent = false;

                // Перевіряємо, чи є хоч в одній із вибраних клітинок якийсь текст
                foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                {
                    if (cell.Value != null && !string.IsNullOrWhiteSpace(cell.Value.ToString()))
                    {
                        hasContent = true;
                        break;
                    }
                }


                if (!hasContent)
                {
                    MessageBox.Show("Видалити пусті клітинки неможливо!", "Увага",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                DialogResult result = MessageBox.Show($"Ви впевнені, що хочете видалити вміст {dataGridView1.SelectedCells.Count} виділених клітинок?",
                    "Підтвердження видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {

                    foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                    {
                        cell.Value = string.Empty;
                    }


                    dataGridView1.ClearSelection();
                    editButton.Focus();
                }
            }
            else
            {
                MessageBox.Show("Спочатку виберіть клітинки для видалення!");
            }
        }

        private void rescheduleButton_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedCells.Count > 0)
            {
                bool atLeastOneDateProcessed = false;


                foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                {
                    if (cell.Value != null && !string.IsNullOrWhiteSpace(cell.Value.ToString()))
                    {

                        if (DateTime.TryParse(cell.Value.ToString(), out DateTime currentDate))
                        {
                            // Додаємо 1 день і записуємо назад
                            cell.Value = currentDate.AddDays(1).ToShortDateString();
                            atLeastOneDateProcessed = true;
                        }
                    }
                }


                if (!atLeastOneDateProcessed)
                {
                    MessageBox.Show("Серед виділеного немає жодної коректної дати!", "Помилка");
                }


            }
            else
            {
                MessageBox.Show("Спочатку виберіть клітинку або кілька клітинок з датою!");
            }
        }
        // Кнопка аналізу накладок
        private void overlayButton_Click(object sender, EventArgs e)
        {
            var intervals = new List<(int RowIndex, string Title, DateTime Start, DateTime End)>();
            StringBuilder errorReport = new StringBuilder();

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                var row = dataGridView1.Rows[i];
                if (row.IsNewRow) continue;

                string title = row.Cells["TitleColumn"].Value?.ToString()?.Trim() ?? "Без назви";
                string dateStr = row.Cells["DateOfColumn"].Value?.ToString()?.Trim() ?? "";
                string timeStr = row.Cells["TimeOfColumn"].Value?.ToString()?.Trim() ?? "";
                string durationStr = row.Cells["DurationColumn"].Value?.ToString()?.Trim() ?? "";

                if (string.IsNullOrEmpty(dateStr) || string.IsNullOrEmpty(timeStr) || string.IsNullOrEmpty(durationStr))
                    continue;

                bool rowHasError = false;

                // 1. Перевірка дати та часу
                if (!DateTime.TryParse($"{dateStr} {timeStr}", out DateTime start))
                {
                    errorReport.AppendLine($"❌ Рядок {i + 1}: Невірний формат дати/часу (\"{dateStr} {timeStr}\")");
                    rowHasError = true;
                }

                // 2. Перевірка тривалості
                if (!TimeSpan.TryParse(durationStr, out TimeSpan duration))
                {
                    errorReport.AppendLine($"❌ Рядок {i + 1}: Невірний формат тривалості (\"{durationStr}\")");
                    rowHasError = true;
                }


                if (rowHasError) continue;

                DateTime end = start.Add(duration);
                intervals.Add((i + 1, title, start, end));
            }


            if (errorReport.Length > 0)
            {
                MessageBox.Show("Знайдено помилки у форматі даних:\n\n" + errorReport.ToString(),
                                "Помилка введення", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            StringBuilder report = new StringBuilder();
            bool hasOverlay = false;

            for (int i = 0; i < intervals.Count; i++)
            {
                for (int j = i + 1; j < intervals.Count; j++)
                {
                    var t1 = intervals[i];
                    var t2 = intervals[j];

                    if (t1.Start.Date == t2.Start.Date)
                    {
                        if (t1.Start < t2.End && t2.Start < t1.End)
                        {
                            hasOverlay = true;
                            report.AppendLine($"⚠️ НАКЛАДКА: рядок {t1.RowIndex} та {t2.RowIndex}");
                            report.AppendLine($"   \"{t1.Title}\" ({t1.Start:HH:mm} - {t1.End:HH:mm})");
                            report.AppendLine($"   \"{t2.Title}\" ({t2.Start:HH:mm} - {t2.End:HH:mm})");
                            report.AppendLine("-----------------------------------------");
                        }
                    }
                }
            }

            if (hasOverlay)
            {
                MessageBox.Show(report.ToString(), "Результат аналізу");
            }
            else if (intervals.Count > 0)
            {
                MessageBox.Show($"Перевірено {intervals.Count} справ. Накладок не виявлено.", "Успіх");
            }
        }

        private void searchTimePicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void searchButton_Click(object sender, EventArgs e) // Кнопка пошуку
        {

            DateTime targetDate = searchTimePicker.Value.Date;

            bool isFound = false;

            dataGridView1.ClearSelection();


            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                if (row.IsNewRow) continue;


                var dateCellValue = row.Cells["DateOfColumn"].Value;

                if (dateCellValue != null)
                {

                    if (DateTime.TryParse(dateCellValue.ToString(), out DateTime rowDate))
                    {

                        if (rowDate.Date == targetDate)
                        {

                            row.Selected = true;


                            if (!isFound)
                            {
                                dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                            }

                            isFound = true;
                        }
                    }
                }
            }


            if (!isFound)
            {
                MessageBox.Show("Справ на вибрану дату не знайдено.", "Пошук");
            }
            else
            {
                MessageBox.Show($"Знайдено та виділено справи на {targetDate.ToShortDateString()}", "Успіх");
            }
        }

        private void cloneButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {

                var selectedRowsGroups = dataGridView1.SelectedCells
                    .Cast<DataGridViewCell>()
                    .GroupBy(c => c.RowIndex)
                    .OrderBy(g => g.Key)
                    .ToList();


                List<DataGridViewCell> cellsToSelect = new List<DataGridViewCell>();

                foreach (var rowGroup in selectedRowsGroups)
                {
                    int sourceRowIndex = rowGroup.Key;
                    DataGridViewRow sourceRow = dataGridView1.Rows[sourceRowIndex];
                    if (sourceRow.IsNewRow) continue;

                    var copiedData = new List<(string colName, object? value)>();
                    foreach (DataGridViewCell cell in rowGroup)
                    {
                        copiedData.Add((dataGridView1.Columns[cell.ColumnIndex].Name!, cell.Value));
                    }

                    int targetRowIndex = -1;
                    for (int i = sourceRowIndex + 1; i < dataGridView1.Rows.Count; i++)
                    {
                        var row = dataGridView1.Rows[i];
                        if (row.IsNewRow) continue;

                        bool canInsertHere = true;
                        foreach (var item in copiedData)
                        {
                            var targetValue = row.Cells[item.colName].Value;
                            if (targetValue != null && !string.IsNullOrWhiteSpace(targetValue.ToString()))
                            {
                                canInsertHere = false;
                                break;
                            }
                        }

                        if (canInsertHere)
                        {
                            targetRowIndex = i;
                            break;
                        }
                    }


                    if (targetRowIndex == -1)
                    {
                        targetRowIndex = dataGridView1.Rows.Add();
                    }


                    DataGridViewRow targetRow = dataGridView1.Rows[targetRowIndex];
                    foreach (var item in copiedData)
                    {
                        targetRow.Cells[item.colName].Value = item.value;
                        cellsToSelect.Add(targetRow.Cells[item.colName]);
                    }
                }


                dataGridView1.ClearSelection();
                foreach (var newCell in cellsToSelect)
                {
                    newCell.Selected = true;
                }

                dataGridView1.Focus();
            }
        }


        private void IntervalTimeBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            // Перевіряємо формат 00:00 у полі IntervalTimeBox
            if (DateTime.TryParseExact(IntervalTimeBox.Text, "HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime parsedTime))
            {
                int totalMilliseconds = (parsedTime.Hour * 3600 + parsedTime.Minute * 60) * 1000;

                if (totalMilliseconds > 0)
                {
                    timerRemind.Interval = totalMilliseconds;
                    timerRemind.Start();
                    MessageBox.Show($"Нагадування увімкнено! Буду перевіряти справи кожні {IntervalTimeBox.Text}", "Таймер");
                }
                else
                {
                    MessageBox.Show("Час інтервалу має бути більшим за нуль.");
                }
            }
            else
            {
                MessageBox.Show("Введіть час у форматі ГГ:ХХ (наприклад, 00:10)");
            }
        }


        private void stopButton_Click(object sender, EventArgs e)
        {
            timerRemind.Stop();
            MessageBox.Show("Автоматичне нагадування вимкнено.");
        }

        private void timerRemind_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DataGridViewRow nearestRow = null!;
            TimeSpan minDiff = TimeSpan.MaxValue;


            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                var dateVal = row.Cells["DateOfColumn"].Value?.ToString();
                var timeVal = row.Cells["TimeOfColumn"].Value?.ToString();

                if (!string.IsNullOrEmpty(dateVal) && !string.IsNullOrEmpty(timeVal))
                {
                    try
                    {
                        DateTime eventTime = DateTime.Parse($"{dateVal} {timeVal}");
                        TimeSpan diff = eventTime - now;

                        if (diff.TotalSeconds > 0 && diff < minDiff)
                        {
                            minDiff = diff;
                            nearestRow = row;
                        }
                    }
                    catch { continue; }
                }
            }


            if (nearestRow != null)
            {
                string title = nearestRow.Cells["TitleColumn"].Value?.ToString() ?? "Подія";


                string timeLeft = "";
                if (minDiff.Days > 0) timeLeft += $"{minDiff.Days} дн. ";
                timeLeft += $"{minDiff.Hours} год. {minDiff.Minutes} хв.";


                timerRemind.Stop();

                MessageBox.Show(
                    $"НАЙБЛИЖЧА СПРАВА: {title}\n\nДо неї залишилось: {timeLeft}",
                    "Нагадування",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                timerRemind.Start();
            }
        }

        private void searchTomorrowButton_Click(object sender, EventArgs e)
        {
            DateTime tomorrow = DateTime.Now.AddDays(1).Date;
            bool found = false;


            dataGridView1.ClearSelection();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;


                var dateValue = row.Cells["DateOfColumn"].Value?.ToString();

                if (!string.IsNullOrEmpty(dateValue))
                {
                    try
                    {

                        DateTime rowDate = DateTime.Parse(dateValue).Date;


                        if (rowDate == tomorrow)
                        {
                            row.Selected = true;
                            found = true;
                        }
                    }
                    catch
                    {

                        continue;
                    }
                }
            }


            if (!found)
            {
                MessageBox.Show(
                    "На завтра справ немає!",
                    "Пошук",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                        break;
                    }
                }
            }

        }

        private void searchYesterdayButton_Click(object sender, EventArgs e)
        {

            DateTime yesterday = DateTime.Now.AddDays(-1).Date;
            bool found = false;


            dataGridView1.ClearSelection();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                // Отримуємо дату з колонки 
                var dateValue = row.Cells["DateOfColumn"].Value?.ToString();

                if (!string.IsNullOrEmpty(dateValue))
                {
                    try
                    {

                        DateTime rowDate = DateTime.Parse(dateValue).Date;


                        if (rowDate == yesterday)
                        {
                            row.Selected = true;
                            found = true;
                        }
                    }
                    catch
                    {

                        continue;
                    }
                }
            }

            if (!found)
            {
                MessageBox.Show(
                    "Вчорашніх справ немає!",
                    "Пошук",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                        break;
                    }
                }
            }
        }

        private void UpButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                // 1. Сортуємо від верхніх до нижніх, щоб не було "заторів"
                var selectedCells = dataGridView1.SelectedCells
                    .Cast<DataGridViewCell>()
                    .OrderBy(c => c.RowIndex)
                    .ToList();

                bool anyMoved = false;
                bool hasBlockedCell = false;

                foreach (var currentCell in selectedCells)
                {
                    int currentRowIndex = currentCell.RowIndex;
                    int columnIndex = currentCell.ColumnIndex;

                    if (currentCell.Value == null || string.IsNullOrWhiteSpace(currentCell.Value.ToString()))
                        continue;

                    int targetRowIndex = -1;

                    // 2. Шукаємо ПЕРШУ вільну клітинку вище поточної
                    // Йдемо від поточного рядка вгору до самого початку (0)
                    for (int i = currentRowIndex - 1; i >= 0; i--)
                    {
                        var cellValue = dataGridView1.Rows[i].Cells[columnIndex].Value;


                        if (cellValue == null || string.IsNullOrWhiteSpace(cellValue.ToString()))
                        {
                            targetRowIndex = i;
                            break; // Знайшли найближчу вільну лунку то зупиняємо пошук
                        }
                        // Якщо тут текст, ми його просто ігноруємо і дивимось далі вгору
                    }

                    // 3. Якщо вільне місце знайдено хоч десь вище
                    if (targetRowIndex != -1)
                    {
                        dataGridView1.Rows[targetRowIndex].Cells[columnIndex].Value = currentCell.Value;
                        currentCell.Value = null;

                        // Переносимо виділення
                        currentCell.Selected = false;
                        dataGridView1.Rows[targetRowIndex].Cells[columnIndex].Selected = true;

                        anyMoved = true;
                    }
                    else
                    {

                        hasBlockedCell = true;
                    }
                }


                if (!anyMoved && hasBlockedCell)
                {
                    MessageBox.Show(
                        "Перенести неможливо, бо вільного місця зверху немає!",
                        "Попередження",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }

                dataGridView1.Focus();
            }
        }

        private void DownButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {

                var selectedCells = dataGridView1.SelectedCells
                    .Cast<DataGridViewCell>()
                    .OrderByDescending(c => c.RowIndex)
                    .ToList();

                bool anyMoved = false;
                bool hasBlockedCell = false;

                foreach (var currentCell in selectedCells)
                {
                    int currentRowIndex = currentCell.RowIndex;
                    int columnIndex = currentCell.ColumnIndex;

                    if (currentCell.Value == null || string.IsNullOrWhiteSpace(currentCell.Value.ToString()))
                        continue;

                    int targetRowIndex = -1;


                    for (int i = currentRowIndex + 1; i < dataGridView1.Rows.Count; i++)
                    {
                        if (dataGridView1.Rows[i].IsNewRow) break;

                        var cellValue = dataGridView1.Rows[i].Cells[columnIndex].Value;


                        if (cellValue == null || string.IsNullOrWhiteSpace(cellValue.ToString()))
                        {
                            targetRowIndex = i;

                            break;
                        }

                    }


                    if (targetRowIndex != -1)
                    {
                        dataGridView1.Rows[targetRowIndex].Cells[columnIndex].Value = currentCell.Value;
                        currentCell.Value = null;

                        // Переносимо фокус
                        currentCell.Selected = false;
                        dataGridView1.Rows[targetRowIndex].Cells[columnIndex].Selected = true;

                        anyMoved = true;
                    }
                    else
                    {

                        hasBlockedCell = true;
                    }
                }

                // Повідомлення тільки якщо справді глухий кут до самого низу
                if (!anyMoved && hasBlockedCell)
                {
                    MessageBox.Show(
                        "Перенести неможливо, бо вільного місця нижче немає!",
                        "Попередження",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }

                dataGridView1.Focus();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SearchByNameBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void SearchByNameButton_Click(object sender, EventArgs e)
        {

            string? searchText = SearchByNameBox?.Text?.Trim()?.ToLower();

            if (string.IsNullOrEmpty(searchText))
            {
                MessageBox.Show("Будь ласка, введіть назву для пошуку.");
                return;
            }

            bool found = false;
            dataGridView1.ClearSelection();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                var cell = row.Cells["TitleColumn"];
                if (cell != null && cell.Value != null)
                {
                    string cellValue = cell.Value.ToString()?.ToLower() ?? "";

                    if (cellValue.Contains(searchText))
                    {
                        row.Selected = true;
                        if (!found)
                        {
                            dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                        }
                        found = true;
                    }
                }
            }

            if (!found)
            {
                MessageBox.Show("Справ з такою назвою не знайдено.");
            }
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Текстові файли (*.txt)|*.txt|Всі файли (*.*)|*.*";
                saveFileDialog.Title = "Зберегти щоденник у файл";
                saveFileDialog.FileName = "Мій_Розклад.txt";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // FileName ніколи не буде null, якщо ShowDialog повернув OK
                        using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                        {
                            writer.WriteLine("==================================================");
                            writer.WriteLine("            ПОВНИЙ СПИСОК ВАШИХ СПРАВ             ");
                            writer.WriteLine("==================================================");
                            writer.WriteLine($"Дата експорту: {DateTime.Now:dd.MM.yyyy HH:mm}");
                            writer.WriteLine();

                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (row.IsNewRow) continue;

                                
                                string title = row.Cells["TitleColumn"].Value?.ToString() ?? "---";
                                string desc = row.Cells["DescColumn"].Value?.ToString() ?? "---";
                                string place = row.Cells["PlaceColumn"].Value?.ToString() ?? "Не вказано";

                                
                                string? dateStart = row.Cells["DateOfColumn"].Value?.ToString();
                                string? timeStart = row.Cells["TimeOfColumn"].Value?.ToString();

                                string duration = row.Cells["DurationColumn"].Value?.ToString() ?? "---";
                                string dateEnd = row.Cells["DateOfEnding"].Value?.ToString() ?? "---";

                                string startInfo;
                                if (string.IsNullOrWhiteSpace(dateStart) && string.IsNullOrWhiteSpace(timeStart))
                                {
                                    startInfo = "Не вказано";
                                }
                                else
                                {
                                    
                                    string timePart = !string.IsNullOrWhiteSpace(timeStart) ? " о " + timeStart : "";
                                    startInfo = ((dateStart ?? "") + timePart).Trim();
                                }

                                writer.WriteLine($"📌 СПРАВА: {title.ToUpper()}");
                                writer.WriteLine($"📝 Опис:      {desc}");
                                writer.WriteLine($"📍 Місце:     {place}");
                                writer.WriteLine($"🕒 Початок:   {startInfo}");
                                writer.WriteLine($"⏳ Тривалість: {duration}");
                                writer.WriteLine($"🏁 Завершення: {dateEnd}");
                                writer.WriteLine(new string('-', 40));
                            }
                        }

                        MessageBox.Show("Всі справи успішно збережено у файл!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Не вдалося зберегти файл: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}