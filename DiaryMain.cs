using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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

        public DiaryMain()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            PanelOfButton.Visible = false;


            dataGridView1.ReadOnly = true;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void DiaryMain_FormClosed(object sender, FormClosedEventArgs e)
        {
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


        private void DiaryMain_Load(object sender, EventArgs e) { }
        private void PanelOfButton_Paint(object sender, PaintEventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void dataGridView1_CellValidating_1(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // Перевіряємо, чи клітинка зараз редагується
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

                // Якщо всі вибрані клітинки порожні
                if (!hasContent)
                {
                    MessageBox.Show("Видалити пусті клітинки неможливо!", "Увага",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Якщо є що видаляти запитуємо підтвердження
                DialogResult result = MessageBox.Show($"Ви впевнені, що хочете видалити вміст {dataGridView1.SelectedCells.Count} виділених клітинок?",
                    "Підтвердження видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Видаляємо текст у кожній виділеній клітинці
                    foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                    {
                        cell.Value = string.Empty;
                    }

                    // Очищуємо виділення та повертаємо фокус
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
            // Перевіряємо, чи є виділені клітинки
            if (dataGridView1.SelectedCells.Count > 0)
            {
                bool atLeastOneDateProcessed = false;

                // Проходимо по ВСІХ виділених клітинках
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

        private void overlayButton_Click(object sender, EventArgs e)
        {
            var intervals = new List<(int RowIndex, string Title, DateTime Start, DateTime End)>();

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                var row = dataGridView1.Rows[i];
                if (row.IsNewRow) continue;

                try
                {


                    string title = row.Cells["TitleColumn"].Value?.ToString()?.Trim() ?? "Без назви";
                    string dateStr = row.Cells["DateOfColumn"].Value?.ToString()?.Trim() ?? "";
                    string timeStr = row.Cells["TimeOfColumn"].Value?.ToString()?.Trim() ?? "";
                    string durationStr = row.Cells["DurationColumn"].Value?.ToString()?.Trim() ?? "";

                    // Якщо основні дані порожні  йдемо далі
                    if (string.IsNullOrEmpty(dateStr) || string.IsNullOrEmpty(timeStr) || string.IsNullOrEmpty(durationStr))
                        continue;

                    // Решта коду без змін...
                    DateTime start = DateTime.Parse($"{dateStr} {timeStr}");
                    TimeSpan duration = TimeSpan.Parse(durationStr);
                    DateTime end = start.Add(duration);

                    intervals.Add((i + 1, title, start, end));
                }
                catch (Exception ex)
                {

                    MessageBox.Show($"Помилка в рядку {i + 1}: {ex.Message}\nПеревір, чи вірно вказано (Name) стовпців у дизайнері!");
                    return;
                }
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
                        // Головна умова накладки
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
                MessageBox.Show(report.ToString(), "Знайдено накладки!");
            }
            else
            {
                MessageBox.Show($"Перевірено {intervals.Count} справ. Накладок не виявлено.", "Результат");
            }
        }

        private void searchTimePicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void searchButton_Click(object sender, EventArgs e) // Кнопка пошуку
        {
            // 1. Отримуємо дату, яку ти вибрав у searchTimePicker (тільки число, місяць, рік)
            DateTime targetDate = searchTimePicker.Value.Date;

            bool isFound = false;

            // 2. Знімаємо виділення з усіх рядків 
            dataGridView1.ClearSelection();

            // 3. Проходимо циклом по всіх рядках таблиці
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Пропускаємо порожній рядок в кінці таблиці
                if (row.IsNewRow) continue;

                // Беремо значення з нашого стовпця з датою
                var dateCellValue = row.Cells["DateOfColumn"].Value;

                if (dateCellValue != null)
                {
                    // Намагаємося перетворити текст із клітинки в дату
                    if (DateTime.TryParse(dateCellValue.ToString(), out DateTime rowDate))
                    {
                        // 4. Порівнюємо дату в рядку з обраною датою
                        if (rowDate.Date == targetDate)
                        {

                            row.Selected = true;

                            // Якщо це перший знайдений рядок  прокручуємо таблицю до нього
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
            // 1. Перевіряємо, чи є виділені клітинки
            if (dataGridView1.SelectedCells.Count > 0)
            {
                // Беремо індекс рядка, з якого хочемо копіювати (з першої виділеної клітинки)
                int currentRowIndex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow sourceRow = dataGridView1.Rows[currentRowIndex];

                if (sourceRow.IsNewRow) return;

                // 2. Створюємо новий рядок
                int newRowIndex = dataGridView1.Rows.Add();
                DataGridViewRow newRow = dataGridView1.Rows[newRowIndex];

                // 3. Список назв усіх твоїх стовпців
                string[] columnNames = {
            "TitleColumn", "DescColumn", "PlaceColumn",
            "DateOfColumn", "TimeOfColumn", "DurationColumn", "DateOfEnding"
        };

                // 4. Проходимо по кожному стовпцю
                foreach (string colName in columnNames)
                {
                    // Перевіряємо, чи виділив користувач саме цю клітинку в початковому рядку
                    if (sourceRow.Cells[colName].Selected)
                    {
                        // Копіюємо значення, тільки якщо клітинка була виділена
                        newRow.Cells[colName].Value = sourceRow.Cells[colName].Value;
                    }
                }

                // 5. Візуальне підтвердження
                dataGridView1.ClearSelection();
                newRow.Selected = true;
            }
            else
            {
                MessageBox.Show("Виділіть клітинки, які ви хочете продублювати!", "Увага");
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
            // 1. Тимчасово зупиняємо таймер, щоб вікна не накопичувалися, поки ти читаєш перше
            timerRemind.Stop();

            DateTime now = DateTime.Now;
            DataGridViewRow? nearestRow = null;
            TimeSpan minDiff = TimeSpan.MaxValue;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                var dateCell = row.Cells["DateOfColumn"].Value;
                var timeCell = row.Cells["TimeOfColumn"].Value;

                if (dateCell != null && timeCell != null)
                {
                    string dateStr = dateCell.ToString() ?? "";
                    string timeStr = timeCell.ToString() ?? "";

                    if (DateTime.TryParse($"{dateStr} {timeStr}", out DateTime taskTime))
                    {
                        if (taskTime > now)
                        {
                            TimeSpan diff = taskTime - now;
                            if (diff < minDiff)
                            {
                                minDiff = diff;
                                nearestRow = row;
                            }
                        }
                    }
                }
            }

            // 2. Якщо знайшли справу показуємо MessageBox
            if (nearestRow != null)
            {
                string title = nearestRow.Cells["TitleColumn"].Value?.ToString() ?? "Без назви";
                string time = nearestRow.Cells["TimeOfColumn"].Value?.ToString() ?? "--:--";

                MessageBox.Show(
                    $"🔔 НАГАДУВАННЯ\n\n" +
                    $"Найближча справа: \"{title}\"\n" +
                    $"Час: {time}\n" +
                    $"Залишилося чекати: {minDiff.Hours} год. {minDiff.Minutes} хв.",
                    "Мій Щоденник",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }

            // 3. Після того, як кнатиснуто "ОК", запускаємо таймер знову для наступного кола
            timerRemind.Start();
        }
    }
}