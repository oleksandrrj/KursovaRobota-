using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Курсова_Робота__Щоденник_
{
    public partial class DiaryMain : Form
    {
        private DiaryManager _manager = new DiaryManager();
        private bool _isEditAllowed = false;
        private DateTime _lastExecutionTime = DateTime.MinValue;

        public DiaryMain()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            PanelOfDiary.Visible = false;

            DiaryTable.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
            DiaryTable.AllowUserToAddRows = false;
            DiaryTable.ReadOnly = false;
            DiaryTable.DataSource = _manager.Entries;

            DiaryTable.CellBeginEdit += DiaryTable_CellBeginEdit;
            DiaryTable.CellEndEdit += DiaryTable_CellEndEdit;
        }

        private void DiaryTable_CellBeginEdit(object? sender, DataGridViewCellCancelEventArgs e)
        {
            if (!_isEditAllowed) e.Cancel = true;
        }

        private void DiaryTable_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            if (!_isEditAllowed || (DateTime.Now - _lastExecutionTime).TotalMilliseconds < 1000) return;

            var row = DiaryTable.Rows[e.RowIndex];
            var cell = row.Cells[e.ColumnIndex];

            
            if (row.DataBoundItem is not DiaryEntry entry) return;

            string propertyName = DiaryTable.Columns[e.ColumnIndex].DataPropertyName ?? string.Empty;
            string newValue = cell.Value?.ToString() ?? string.Empty;

            PropertyInfo? propInfo = typeof(DiaryEntry).GetProperty(propertyName);
            
            string oldValue = propInfo?.GetValue(entry)?.ToString() ?? string.Empty;

            if (newValue == oldValue)
            {
                _isEditAllowed = false;
                return;
            }

            _lastExecutionTime = DateTime.Now;

            DialogResult result = MessageBox.Show(
                "Ви впевнені, що хочете зберегти ці зміни?",
                "Підтвердження",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _manager.UpdateEntryProperty(entry.Id, propertyName, newValue);
                _isEditAllowed = false;
            }
            else
            {
                this.BeginInvoke(new Action(() =>
                {
                    DiaryTable.CurrentCell = cell;
                    DiaryTable.BeginEdit(true);
                    if (DiaryTable.EditingControl is TextBox textBox)
                    {
                        textBox.SelectionStart = textBox.Text.Length;
                    }
                }));
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (DiaryTable.CurrentCell != null)
            {
                _isEditAllowed = true;
                DiaryTable.BeginEdit(true);
            }
        }

        private void AddRowButton_Click(object sender, EventArgs e)
        {
            _isEditAllowed = true;
            int newId = _manager.Entries.Count > 0 ? _manager.Entries.Max(entry => entry.Id) + 1 : 1;
            _manager.Entries.Add(new DiaryEntry { Id = newId, Title = "" });
            DiaryTable.CurrentCell = DiaryTable.Rows[DiaryTable.Rows.Count - 1].Cells[1];
            _isEditAllowed = false;
        }

        private void DiaryMain_FormClosed(object sender, FormClosedEventArgs e) => Application.Exit();
        private void PictureBoxButton_Click(object sender, EventArgs e) => PanelOfDiary.Visible = !PanelOfDiary.Visible;

        private void DeleteRowButton_Click(object sender, EventArgs e)
        {
            
            if (DiaryTable.CurrentRow != null && DiaryTable.CurrentRow.DataBoundItem is DiaryEntry entry)
            {
                DialogResult result = MessageBox.Show(
                    "Ви впевнені, що хочете видалити цей запис?",
                    "Підтвердження видалення",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    _manager.RemoveEntry(entry);
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть рядок для видалення.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SearchOfDateButton_Click(object sender, EventArgs e)
        {
            DiaryTable.ClearSelection();
            string targetDate = DiaryTimePicker.Value.ToString("dd.MM.yyyy");
            bool found = false;

            foreach (DataGridViewRow row in DiaryTable.Rows)
            {
                var cellValue = row.Cells["DateColumn"].Value;

                
                if (cellValue != null && cellValue.ToString()?.Trim() == targetDate)
                {
                    row.Selected = true;
                    found = true;
                }
                else
                {
                    row.Selected = false;
                }
            }

            if (!found)
            {
                MessageBox.Show($"Записів на дату {targetDate} не знайдено.", "Пошук", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}