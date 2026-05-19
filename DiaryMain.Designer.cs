namespace Курсова_Робота__Щоденник_
{
    partial class DiaryMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            PictureBoxButton = new PictureBox();
            PanelOfDiary = new Panel();
            panel2 = new Panel();
            SearchOfDateButton = new Button();
            DiaryTimePicker = new DateTimePicker();
            label1 = new Label();
            panel1 = new Panel();
            DeleteRowButton = new Button();
            AddRowButton = new Button();
            EditButton = new Button();
            DiaryTable = new DataGridView();
            TitleColumn = new DataGridViewTextBoxColumn();
            DescColumn = new DataGridViewTextBoxColumn();
            PlaceColumn = new DataGridViewTextBoxColumn();
            DateColumn = new DataGridViewTextBoxColumn();
            TimeColumn = new DataGridViewTextBoxColumn();
            DurationColumn = new DataGridViewTextBoxColumn();
            DateOfEndColumn = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)PictureBoxButton).BeginInit();
            PanelOfDiary.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DiaryTable).BeginInit();
            SuspendLayout();
            // 
            // PictureBoxButton
            // 
            PictureBoxButton.Image = Properties.Resources.diarybutton2;
            PictureBoxButton.Location = new Point(12, 40);
            PictureBoxButton.Name = "PictureBoxButton";
            PictureBoxButton.Size = new Size(127, 106);
            PictureBoxButton.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBoxButton.TabIndex = 1;
            PictureBoxButton.TabStop = false;
            PictureBoxButton.Click += PictureBoxButton_Click;
            // 
            // PanelOfDiary
            // 
            PanelOfDiary.BackColor = Color.FromArgb(255, 248, 230);
            PanelOfDiary.Controls.Add(panel2);
            PanelOfDiary.Controls.Add(label1);
            PanelOfDiary.Controls.Add(panel1);
            PanelOfDiary.Controls.Add(DiaryTable);
            PanelOfDiary.Location = new Point(168, 25);
            PanelOfDiary.Name = "PanelOfDiary";
            PanelOfDiary.Size = new Size(1726, 1044);
            PanelOfDiary.TabIndex = 2;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(255, 253, 214);
            panel2.Controls.Add(SearchOfDateButton);
            panel2.Controls.Add(DiaryTimePicker);
            panel2.Location = new Point(15, 51);
            panel2.Name = "panel2";
            panel2.Size = new Size(206, 180);
            panel2.TabIndex = 2;
            // 
            // SearchOfDateButton
            // 
            SearchOfDateButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            SearchOfDateButton.Location = new Point(32, 56);
            SearchOfDateButton.Name = "SearchOfDateButton";
            SearchOfDateButton.Size = new Size(146, 29);
            SearchOfDateButton.TabIndex = 1;
            SearchOfDateButton.Text = "Пошук за датою";
            SearchOfDateButton.UseVisualStyleBackColor = true;
            SearchOfDateButton.Click += SearchOfDateButton_Click;
            // 
            // DiaryTimePicker
            // 
            DiaryTimePicker.Location = new Point(3, 12);
            DiaryTimePicker.Name = "DiaryTimePicker";
            DiaryTimePicker.Size = new Size(200, 27);
            DiaryTimePicker.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.ForeColor = Color.FromArgb(115, 0, 8);
            label1.Location = new Point(82, 15);
            label1.Name = "label1";
            label1.Size = new Size(67, 23);
            label1.TabIndex = 2;
            label1.Text = "Пошук";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(255, 253, 214);
            panel1.Controls.Add(DeleteRowButton);
            panel1.Controls.Add(AddRowButton);
            panel1.Controls.Add(EditButton);
            panel1.Location = new Point(15, 272);
            panel1.Name = "panel1";
            panel1.Size = new Size(206, 167);
            panel1.TabIndex = 1;
            // 
            // DeleteRowButton
            // 
            DeleteRowButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            DeleteRowButton.Location = new Point(32, 114);
            DeleteRowButton.Name = "DeleteRowButton";
            DeleteRowButton.Size = new Size(142, 29);
            DeleteRowButton.TabIndex = 2;
            DeleteRowButton.Text = "Видалити рядок";
            DeleteRowButton.UseVisualStyleBackColor = true;
            DeleteRowButton.Click += DeleteRowButton_Click;
            // 
            // AddRowButton
            // 
            AddRowButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            AddRowButton.Location = new Point(32, 69);
            AddRowButton.Name = "AddRowButton";
            AddRowButton.Size = new Size(142, 29);
            AddRowButton.TabIndex = 1;
            AddRowButton.Text = "Додати рядок";
            AddRowButton.UseVisualStyleBackColor = true;
            AddRowButton.Click += AddRowButton_Click;
            // 
            // EditButton
            // 
            EditButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            EditButton.Location = new Point(43, 21);
            EditButton.Name = "EditButton";
            EditButton.Size = new Size(112, 29);
            EditButton.TabIndex = 0;
            EditButton.Text = "Редагувати";
            EditButton.UseVisualStyleBackColor = true;
            EditButton.Click += EditButton_Click;
            // 
            // DiaryTable
            // 
            DiaryTable.AllowUserToResizeColumns = false;
            DiaryTable.AllowUserToResizeRows = false;
            DiaryTable.Anchor = AnchorStyles.None;
            DiaryTable.BackgroundColor = SystemColors.ButtonFace;
            DiaryTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DiaryTable.Columns.AddRange(new DataGridViewColumn[] { TitleColumn, DescColumn, PlaceColumn, DateColumn, TimeColumn, DurationColumn, DateOfEndColumn });
            DiaryTable.Location = new Point(238, 0);
            DiaryTable.Name = "DiaryTable";
            DiaryTable.RowHeadersWidth = 51;
            DiaryTable.Size = new Size(1488, 1044);
            DiaryTable.TabIndex = 0;
            DiaryTable.CellBeginEdit += DiaryTable_CellBeginEdit;
            
            // 
            // TitleColumn
            // 
            TitleColumn.HeaderText = "Назва справи";
            TitleColumn.MinimumWidth = 6;
            TitleColumn.Name = "TitleColumn";
            TitleColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            TitleColumn.Width = 150;
            // 
            // DescColumn
            // 
            DescColumn.HeaderText = "Опис справи";
            DescColumn.MinimumWidth = 6;
            DescColumn.Name = "DescColumn";
            DescColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            DescColumn.Width = 600;
            // 
            // PlaceColumn
            // 
            PlaceColumn.HeaderText = "Місце проведення";
            PlaceColumn.MinimumWidth = 6;
            PlaceColumn.Name = "PlaceColumn";
            PlaceColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            PlaceColumn.Width = 250;
            // 
            // DateColumn
            // 
            DateColumn.HeaderText = "Дата ";
            DateColumn.MinimumWidth = 6;
            DateColumn.Name = "DateColumn";
            DateColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            DateColumn.Width = 125;
            // 
            // TimeColumn
            // 
            TimeColumn.HeaderText = "Час ";
            TimeColumn.MinimumWidth = 6;
            TimeColumn.Name = "TimeColumn";
            TimeColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            TimeColumn.Width = 80;
            // 
            // DurationColumn
            // 
            DurationColumn.HeaderText = "Тривалість";
            DurationColumn.MinimumWidth = 6;
            DurationColumn.Name = "DurationColumn";
            DurationColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            DurationColumn.Width = 125;
            // 
            // DateOfEndColumn
            // 
            DateOfEndColumn.HeaderText = "Дата закінчення";
            DateOfEndColumn.MinimumWidth = 6;
            DateOfEndColumn.Name = "DateOfEndColumn";
            DateOfEndColumn.Width = 125;
            // 
            // DiaryMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 237, 243);
            ClientSize = new Size(1924, 1175);
            Controls.Add(PanelOfDiary);
            Controls.Add(PictureBoxButton);
            Name = "DiaryMain";
            Text = "Щоденник";
            FormClosed += DiaryMain_FormClosed;
            ((System.ComponentModel.ISupportInitialize)PictureBoxButton).EndInit();
            PanelOfDiary.ResumeLayout(false);
            PanelOfDiary.PerformLayout();
            panel2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DiaryTable).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox PictureBoxButton;
        private Panel PanelOfDiary;
        private DataGridView DiaryTable;
        private DataGridViewTextBoxColumn TitleColumn;
        private DataGridViewTextBoxColumn DescColumn;
        private DataGridViewTextBoxColumn PlaceColumn;
        private DataGridViewTextBoxColumn DateColumn;
        private DataGridViewTextBoxColumn TimeColumn;
        private DataGridViewTextBoxColumn DurationColumn;
        private DataGridViewTextBoxColumn DateOfEndColumn;
        private Panel panel1;
        private Button EditButton;
        private Panel panel2;
        private Label label1;
        private Button SearchOfDateButton;
        private DateTimePicker DiaryTimePicker;
        private Button AddRowButton;
        private Button DeleteRowButton;
    }
}