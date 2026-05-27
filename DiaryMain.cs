using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Text;

namespace Курсова_Робота__Щоденник_
{
    public partial class DiaryMain : Form
    {
        private string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "diary_data.xml");

        private object? _oldCellValue;
        private bool _isProcessing = false;
        private DiaryManager _diaryManager = new DiaryManager();

        public DiaryMain()
        {
            InitializeComponent();

            this.WindowState = FormWindowState.Maximized;
            PanelOfDiary.Visible = false;

            DiaryTable.CellBeginEdit += DiaryTable_CellBeginEdit;

           
            DiaryTable.CellValidating += DiaryTable_CellValidating;
            DiaryTable.EditMode = DataGridViewEditMode.EditOnF2;

            
            DiaryTable.AllowUserToAddRows = true;
            DiaryTable.AutoGenerateColumns = false;

            LoadDataFromXml();
            this.FormClosing += DiaryMain_FormClosing;
        }

        private void DiaryMain_FormClosing(object? sender, FormClosingEventArgs e)
        {
            SaveDataToXml();
        }

        
        private void SaveDataToXml()
        {
            try
            {
                DiaryTable.EndEdit();
                DataTable dt = new DataTable("DiaryEntries");

                
                dt.Columns.Add("TitleColumn");
                dt.Columns.Add("DescColumn");
                dt.Columns.Add("PlaceColumn");
                dt.Columns.Add("DateColumn");
                dt.Columns.Add("TimeColumn");
                dt.Columns.Add("DurationColumn");
                dt.Columns.Add("DateOfEndColumn");

                foreach (DataGridViewRow row in DiaryTable.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        DataRow dr = dt.NewRow();
                        dr["TitleColumn"] = row.Cells["TitleColumn"].Value?.ToString() ?? "";
                        dr["DescColumn"] = row.Cells["DescColumn"].Value?.ToString() ?? "";
                        dr["PlaceColumn"] = row.Cells["PlaceColumn"].Value?.ToString() ?? "";
                        dr["DateColumn"] = row.Cells["DateColumn"].Value?.ToString() ?? "";
                        dr["TimeColumn"] = row.Cells["TimeColumn"].Value?.ToString() ?? "";
                        dr["DurationColumn"] = row.Cells["DurationColumn"].Value?.ToString() ?? "";
                        dr["DateOfEndColumn"] = row.Cells["DateOfEndColumn"].Value?.ToString() ?? "";
                        dt.Rows.Add(dr);
                    }
                }
                dt.WriteXml(_filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка збереження: " + ex.Message);
            }
        }

        
        private void LoadDataFromXml()
        {
            if (!File.Exists(_filePath)) return;
            try
            {
                DataTable dt = new DataTable("DiaryEntries");

                
                dt.Columns.Add("TitleColumn");
                dt.Columns.Add("DescColumn");
                dt.Columns.Add("PlaceColumn");
                dt.Columns.Add("DateColumn");
                dt.Columns.Add("TimeColumn");
                dt.Columns.Add("DurationColumn");
                dt.Columns.Add("DateOfEndColumn");

                dt.ReadXml(_filePath);

                DiaryTable.Rows.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    DiaryTable.Rows.Add(
                        dr["TitleColumn"],
                        dr["DescColumn"],
                        dr["PlaceColumn"],
                        dr["DateColumn"],
                        dr["TimeColumn"],
                        dr["DurationColumn"],
                        dr["DateOfEndColumn"]
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка завантаження: " + ex.Message);
            }
        }

        private void PictureBoxButton_Click(object sender, EventArgs e)
        {
            PanelOfDiary.Visible = !PanelOfDiary.Visible;
        }

        private void DiaryMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }



        private void DiaryTable_CellBeginEdit(object? sender, DataGridViewCellCancelEventArgs e)
        {
            _oldCellValue = DiaryTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
        }

        private void DiaryTable_CellValidating(object? sender, DataGridViewCellValidatingEventArgs e)
        {
            
            if (_isProcessing) return;

            
            string newValue = e.FormattedValue?.ToString() ?? "";
            string oldValue = DiaryTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() ?? "";

            
            if (newValue == oldValue) return;

            
            _isProcessing = true;

            DialogResult result = MessageBox.Show("Ви впевнені, що хочете зберегти зміни?",
                                                  "Підтвердження",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true; 
                DiaryTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = oldValue;
            }

            
            this.BeginInvoke(new Action(() =>
            {
                _isProcessing = false;
            }));
        }

        private void SearchOfDateButton_Click(object sender, EventArgs e)
        {
            
            _diaryManager.Entries.Clear();
            foreach (DataGridViewRow row in DiaryTable.Rows)
            {
                if (row.IsNewRow) continue;

              
                if (DateTime.TryParse(row.Cells["DateColumn"].Value?.ToString(), out DateTime d))
                {
                    _diaryManager.Entries.Add(new DiaryEntry { Id = row.Index, Date = d });
                }
            }

            
            DateTime searchDate = DiaryTimePicker.Value;
            List<int> foundIds = _diaryManager.GetEntryIdsByDate(searchDate);

            
            DiaryTable.ClearSelection();
            bool found = false;
            foreach (int id in foundIds)
            {
                if (id >= 0 && id < DiaryTable.Rows.Count)
                {
                    DiaryTable.Rows[id].Selected = true;
                    found = true;
                }
            }

            if (!found) MessageBox.Show("Справ на цю дату не знайдено.");
        }

        private void SearchByNameButton_Click(object sender, EventArgs e)
        {
            
            _diaryManager.Entries.Clear();
            foreach (DataGridViewRow row in DiaryTable.Rows)
            {
                if (row.IsNewRow) continue;

                var entry = new DiaryEntry();
                entry.Id = row.Index;
                entry.Title = row.Cells["TitleColumn"].Value?.ToString() ?? "";
                _diaryManager.Entries.Add(entry);
            }

           
            string searchText = DiaryTextBox.Text;

            List<int> foundIds = _diaryManager.GetEntryIdsByTitle(searchText);

           
            DiaryTable.ClearSelection();
            bool found = false;

            foreach (int id in foundIds)
            {
                if (id >= 0 && id < DiaryTable.Rows.Count)
                {
                    DiaryTable.Rows[id].Selected = true;
                    found = true;
                }
            }

            if (!found)
                MessageBox.Show("Справ з такою назвою не знайдено.");
        }

        private void DiaryTomorrowButton_Click(object sender, EventArgs e)
        {
            
            _diaryManager.Entries.Clear();
            foreach (DataGridViewRow row in DiaryTable.Rows)
            {
                if (row.IsNewRow) continue;

                
                if (DateTime.TryParse(row.Cells["DateColumn"].Value?.ToString(), out DateTime parsedDate))
                {
                    _diaryManager.Entries.Add(new DiaryEntry
                    {
                        Id = row.Index,
                        Date = parsedDate
                    });
                }
            }

            
            List<int> foundIds = _diaryManager.GetTomorrowEntryIds();

            
            DiaryTable.ClearSelection();
            bool found = false;

            foreach (int id in foundIds)
            {
                if (id >= 0 && id < DiaryTable.Rows.Count)
                {
                    DiaryTable.Rows[id].Selected = true;
                    found = true;
                }
            }

            if (!found)
            {
                MessageBox.Show("На завтра справ не заплановано.");
            }
        }

        private void DiaryYesterdayButton_Click(object sender, EventArgs e)
        {
            
            _diaryManager.Entries.Clear();
            foreach (DataGridViewRow row in DiaryTable.Rows)
            {
                if (row.IsNewRow) continue;

                if (DateTime.TryParse(row.Cells["DateColumn"].Value?.ToString(), out DateTime parsedDate))
                {
                    _diaryManager.Entries.Add(new DiaryEntry
                    {
                        Id = row.Index,
                        Date = parsedDate
                    });
                }
            }

            
            List<int> foundIds = _diaryManager.GetYesterdayEntryIds();

            
            DiaryTable.ClearSelection();
            bool found = false;

            foreach (int id in foundIds)
            {
                if (id >= 0 && id < DiaryTable.Rows.Count)
                {
                    DiaryTable.Rows[id].Selected = true;
                    found = true;
                }
            }

            if (!found)
            {
                MessageBox.Show("На вчора справ не знайдено.");
            }
        }

        private void DiaryDeleteRowButton_Click(object sender, EventArgs e)
        {
            
            if (DiaryTable.SelectedRows.Count == 0)
            {
                
                if (DiaryTable.SelectedCells.Count > 0)
                {
                    MessageBox.Show("Виділіть весь рядок, щоб можна було його видалити!",
                                    "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            
            if (MessageBox.Show("Ви дійсно хочете видалити вибрані рядки?",
                                "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                
                var selectedIndices = DiaryTable.SelectedRows
                                                .Cast<DataGridViewRow>()
                                                .Select(r => r.Index)
                                                .OrderByDescending(i => i)
                                                .ToList();

                
                foreach (int idx in selectedIndices)
                {
                    _diaryManager.RemoveEntryAt(idx); 
                    DiaryTable.Rows.RemoveAt(idx);    
                }

                
                SaveDataToXml();
            }
        }
        

        private void DiaryCleanButton_Click(object sender, EventArgs e)
        {
            if (DiaryTable.SelectedCells.Count == 0) return;

            if (MessageBox.Show("Ви впевнені, що хочете очистити вміст?", "Підтвердження",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (DataGridViewCell cell in DiaryTable.SelectedCells)
                {
                    if (!cell.ReadOnly)
                    {
                        
                        cell.Value = "";

                        string colName = cell.OwningColumn?.Name ?? "";

                        
                        if (!string.IsNullOrEmpty(colName))
                        {
                            _diaryManager.ClearEntryField(cell.RowIndex, colName);
                        }
                    }
                }

                
                SaveDataToXml();
            }
        }

        private void DiaryCopyButton_Click(object sender, EventArgs e)
        {
            
            if (DiaryTable.SelectedCells.Count == 0) return;

            
            var selectedCells = DiaryTable.SelectedCells
                .Cast<DataGridViewCell>()
                .OrderBy(c => c.RowIndex)
                .ToList();

            List<DataGridViewCell> newlyCreatedCells = new List<DataGridViewCell>();

            foreach (var cell in selectedCells)
            {
                
                if (cell.Value == null || string.IsNullOrWhiteSpace(cell.Value.ToString())) continue;

                string colName = DiaryTable.Columns[cell.ColumnIndex].Name;
                object valueToCopy = cell.Value;
                int sourceRowIndex = cell.RowIndex;

                bool placed = false;

                
                for (int i = sourceRowIndex + 1; i < DiaryTable.Rows.Count; i++)
                {
                    
                    if (DiaryTable.Rows[i].IsNewRow) continue;

                    var targetCell = DiaryTable.Rows[i].Cells[colName];

                    
                    if (string.IsNullOrWhiteSpace(targetCell.Value?.ToString()))
                    {
                        targetCell.Value = valueToCopy;
                        _diaryManager.UpdateEntryField(i, colName, valueToCopy?.ToString() ?? "");

                        newlyCreatedCells.Add(targetCell);
                        placed = true;
                        break; 
                    }
                }

                
                if (!placed)
                {
                    int newRowIdx = DiaryTable.Rows.Add();
                    var targetCell = DiaryTable.Rows[newRowIdx].Cells[colName];

                    targetCell.Value = valueToCopy;
                    _diaryManager.UpdateEntryField(newRowIdx, colName, valueToCopy?.ToString() ?? "");

                    newlyCreatedCells.Add(targetCell);
                }
            }

            
            if (newlyCreatedCells.Count > 0)
            {
                DiaryTable.ClearSelection();

                
                foreach (var cell in newlyCreatedCells)
                {
                    cell.Selected = true;
                }

               
                DiaryTable.CurrentCell = newlyCreatedCells.Last();
            }

            SaveDataToXml();
        }

        private void DiaryRescheduleButton_Click(object sender, EventArgs e)
        {
            if (DiaryTable.SelectedCells.Count == 0) return;

            foreach (DataGridViewCell cell in DiaryTable.SelectedCells)
            {
                string cellValue = cell.Value?.ToString() ?? "";
                if (!string.IsNullOrEmpty(cellValue) && DateTime.TryParse(cellValue, out DateTime currentDate))
                {
                    DateTime nextDay = currentDate.AddDays(1);
                    string newDateString = nextDay.ToString("dd.MM.yyyy");

                    cell.Value = newDateString;

                    
                    string colName = cell.OwningColumn?.Name ?? "";
                    if (!string.IsNullOrEmpty(colName))
                    {
                        _diaryManager.UpdateDate(cell.RowIndex, colName, newDateString);
                    }
                }
            }
            SaveDataToXml();
        }

        private void DiaryRescheduleYesterday_Click(object sender, EventArgs e)
        {
            if (DiaryTable.SelectedCells.Count == 0) return;

            foreach (DataGridViewCell cell in DiaryTable.SelectedCells)
            {
                string cellValue = cell.Value?.ToString() ?? "";
                if (!string.IsNullOrEmpty(cellValue) && DateTime.TryParse(cellValue, out DateTime currentDate))
                {
                    DateTime previousDay = currentDate.AddDays(-1);
                    string newDateString = previousDay.ToString("dd.MM.yyyy");

                    cell.Value = newDateString;

                    string colName = cell.OwningColumn?.Name ?? "";
                    if (!string.IsNullOrEmpty(colName))
                    {
                        _diaryManager.UpdateDate(cell.RowIndex, colName, newDateString);
                    }
                }
            }
            SaveDataToXml();
        }

        private void DiaryOverlayButton_Click(object sender, EventArgs e)
        {
            var entries = new List<(int, string, DateTime, DateTime)>();

            foreach (DataGridViewRow row in DiaryTable.Rows)
            {
                if (row.IsNewRow) continue;

                string title = row.Cells["TitleColumn"].Value?.ToString() ?? "Без назви";
                string dateStr = row.Cells["DateColumn"].Value?.ToString() ?? "";
                string timeStr = row.Cells["TimeColumn"].Value?.ToString() ?? "";
                string durationStr = row.Cells["DurationColumn"].Value?.ToString() ?? "";

                
                DateTime start = DateTime.MinValue;
                TimeSpan dur = TimeSpan.Zero; 

                
                bool hasData = DateTime.TryParse($"{dateStr} {timeStr}", out start) &&
                               TimeSpan.TryParse(durationStr, out dur);

                entries.Add((row.Index + 1, title, hasData ? start : DateTime.MinValue, hasData ? start.Add(dur) : DateTime.MinValue));
            }

            var overlaps = _diaryManager.CheckForOverlaps(entries);

            if (overlaps.Count > 0)
            {
                StringBuilder report = new StringBuilder();
                foreach (var overlap in overlaps)
                {
                    report.AppendLine(overlap);
                    report.AppendLine("-----------------------------------------");
                }
                MessageBox.Show(report.ToString(), "Результат аналізу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Перевірено справ. Накладок не виявлено.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DiaryStartButton_Click(object sender, EventArgs e)
        {
            
            if (TimeSpan.TryParse(DiaryTimeBox.Text, out TimeSpan interval))
            {
                
                DiaryTimer.Interval = (int)interval.TotalMilliseconds;
                DiaryTimer.Start();
                MessageBox.Show("Нагадування активовано!", "Таймер", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Введіть коректний час у форматі 00:00 (гг:хв)");
            }
        }

        private void DiaryStopButton_Click(object sender, EventArgs e)
        {
            DiaryTimer.Stop();
            MessageBox.Show("Нагадування зупинено.", "Таймер", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DiaryTimer_Tick(object sender, EventArgs e)
        {
            var rows = DiaryTable.Rows.Cast<DataGridViewRow>();
            var nearest = _diaryManager.GetNearestEntry(rows);

            if (nearest.HasValue)
            {
                var entry = nearest.Value;
                MessageBox.Show($"⏰ Нагадування про справу:\n\n" +
                                $"Назва: {entry.Title}\n" +
                                $"Дата: {entry.Date}\n" +
                                $"Час: {entry.Time}\n\n" +
                                $"⏳ Залишилось: {entry.Remaining}",
                                "Нагадування", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DiaryDownloadButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FileName = "Список_справ.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    
                    var rows = DiaryTable.Rows.Cast<DataGridViewRow>();
                    string report = _diaryManager.GenerateExportReport(rows);

                    
                    System.IO.File.WriteAllText(saveFileDialog.FileName, report);

                    MessageBox.Show("Файл успішно збережено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка при збереженні: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}