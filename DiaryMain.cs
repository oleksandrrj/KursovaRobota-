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

        private DiaryManager _manager = new DiaryManager();
        public DiaryMain()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            PanelOfDiary.Visible = false;
        }

        private void DiaryMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void PictureBoxButton_Click(object sender, EventArgs e)
        {
            PanelOfDiary.Visible = !PanelOfDiary.Visible;
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (DiaryTable.CurrentCell != null)
            {
                // Розблоковуємо таблицю та поточну клітинку
                DiaryTable.ReadOnly = false;
                DiaryTable.CurrentCell.ReadOnly = false;

                // Вмикаємо режим введення тексту
                DiaryTable.BeginEdit(true);
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть клітинку для редагування!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DiaryTable_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (DiaryTable.IsCurrentCellInEditMode)
            {
                
                string newValue = e.FormattedValue?.ToString() ?? string.Empty;
                string oldValue = DiaryTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() ?? string.Empty;

                if (newValue == oldValue) return;

                DialogResult result = MessageBox.Show(
                    "Ви впевнені, що хочете зберегти ці зміни?",
                    "Підтвердження редагування",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        
                        if (DiaryTable.Rows[e.RowIndex].DataBoundItem is DiaryEntry currentEntry)
                        {
                            string propertyName = DiaryTable.Columns[e.ColumnIndex].DataPropertyName ?? string.Empty;

                            
                            _manager.UpdateEntryProperty(currentEntry.Id, propertyName, newValue);
                        }

                        this.BeginInvoke(new MethodInvoker(() => {
                            DiaryTable.ReadOnly = true;
                        }));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Помилка валідації", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}

