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
            components = new System.ComponentModel.Container();
            PictureBoxButton = new PictureBox();
            PanelOfDiary = new Panel();
            panel4 = new Panel();
            DiaryStopButton = new Button();
            DiaryStartButton = new Button();
            DiaryTimeBox = new TextBox();
            label4 = new Label();
            label3 = new Label();
            panel3 = new Panel();
            DiaryOverlayButton = new Button();
            DiaryRescheduleYesterday = new Button();
            DiaryRescheduleButton = new Button();
            label2 = new Label();
            panel2 = new Panel();
            panel5 = new Panel();
            button1 = new Button();
            DiaryYesterdayButton = new Button();
            DiaryTomorrowButton = new Button();
            SearchByNameButton = new Button();
            DiaryTextBox = new TextBox();
            SearchOfDateButton = new Button();
            DiaryTimePicker = new DateTimePicker();
            label1 = new Label();
            panel1 = new Panel();
            DiaryCopyButton = new Button();
            DiaryDeleteRowButton = new Button();
            DiaryCleanButton = new Button();
            DiaryTable = new DataGridView();
            TitleColumn = new DataGridViewTextBoxColumn();
            DescColumn = new DataGridViewTextBoxColumn();
            PlaceColumn = new DataGridViewTextBoxColumn();
            DateColumn = new DataGridViewTextBoxColumn();
            TimeColumn = new DataGridViewTextBoxColumn();
            DurationColumn = new DataGridViewTextBoxColumn();
            DateOfEndColumn = new DataGridViewTextBoxColumn();
            DiaryTimer = new System.Windows.Forms.Timer(components);
            DiaryDownloadButton = new Button();
            ((System.ComponentModel.ISupportInitialize)PictureBoxButton).BeginInit();
            PanelOfDiary.SuspendLayout();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            panel5.SuspendLayout();
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
            PanelOfDiary.Controls.Add(DiaryDownloadButton);
            PanelOfDiary.Controls.Add(panel4);
            PanelOfDiary.Controls.Add(label4);
            PanelOfDiary.Controls.Add(label3);
            PanelOfDiary.Controls.Add(panel3);
            PanelOfDiary.Controls.Add(label2);
            PanelOfDiary.Controls.Add(panel2);
            PanelOfDiary.Controls.Add(label1);
            PanelOfDiary.Controls.Add(panel1);
            PanelOfDiary.Controls.Add(DiaryTable);
            PanelOfDiary.Location = new Point(168, 25);
            PanelOfDiary.Name = "PanelOfDiary";
            PanelOfDiary.Size = new Size(1726, 1044);
            PanelOfDiary.TabIndex = 2;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(255, 253, 214);
            panel4.Controls.Add(DiaryStopButton);
            panel4.Controls.Add(DiaryStartButton);
            panel4.Controls.Add(DiaryTimeBox);
            panel4.Location = new Point(15, 839);
            panel4.Name = "panel4";
            panel4.Size = new Size(206, 142);
            panel4.TabIndex = 7;
            // 
            // DiaryStopButton
            // 
            DiaryStopButton.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            DiaryStopButton.Location = new Point(67, 86);
            DiaryStopButton.Name = "DiaryStopButton";
            DiaryStopButton.Size = new Size(75, 30);
            DiaryStopButton.TabIndex = 2;
            DiaryStopButton.Text = "Стоп";
            DiaryStopButton.UseVisualStyleBackColor = true;
            DiaryStopButton.Click += DiaryStopButton_Click;
            // 
            // DiaryStartButton
            // 
            DiaryStartButton.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            DiaryStartButton.Location = new Point(66, 50);
            DiaryStartButton.Name = "DiaryStartButton";
            DiaryStartButton.Size = new Size(75, 30);
            DiaryStartButton.TabIndex = 1;
            DiaryStartButton.Text = "Старт";
            DiaryStartButton.UseVisualStyleBackColor = true;
            DiaryStartButton.Click += DiaryStartButton_Click;
            // 
            // DiaryTimeBox
            // 
            DiaryTimeBox.Location = new Point(67, 13);
            DiaryTimeBox.Name = "DiaryTimeBox";
            DiaryTimeBox.Size = new Size(74, 27);
            DiaryTimeBox.TabIndex = 0;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label4.ForeColor = Color.FromArgb(115, 0, 8);
            label4.Location = new Point(6, 813);
            label4.Name = "label4";
            label4.Size = new Size(229, 23);
            label4.TabIndex = 8;
            label4.Text = "Автоматичне нагадування";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label3.ForeColor = Color.FromArgb(115, 0, 8);
            label3.Location = new Point(46, 564);
            label3.Name = "label3";
            label3.Size = new Size(162, 23);
            label3.TabIndex = 7;
            label3.Text = "Планування подій";
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(255, 253, 214);
            panel3.Controls.Add(DiaryOverlayButton);
            panel3.Controls.Add(DiaryRescheduleYesterday);
            panel3.Controls.Add(DiaryRescheduleButton);
            panel3.Location = new Point(12, 590);
            panel3.Name = "panel3";
            panel3.Size = new Size(206, 217);
            panel3.TabIndex = 6;
            // 
            // DiaryOverlayButton
            // 
            DiaryOverlayButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            DiaryOverlayButton.Location = new Point(32, 142);
            DiaryOverlayButton.Name = "DiaryOverlayButton";
            DiaryOverlayButton.Size = new Size(153, 55);
            DiaryOverlayButton.TabIndex = 5;
            DiaryOverlayButton.Text = "Аналіз накладок";
            DiaryOverlayButton.UseVisualStyleBackColor = true;
            DiaryOverlayButton.Click += DiaryOverlayButton_Click;
            // 
            // DiaryRescheduleYesterday
            // 
            DiaryRescheduleYesterday.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            DiaryRescheduleYesterday.Location = new Point(31, 81);
            DiaryRescheduleYesterday.Name = "DiaryRescheduleYesterday";
            DiaryRescheduleYesterday.Size = new Size(153, 55);
            DiaryRescheduleYesterday.TabIndex = 4;
            DiaryRescheduleYesterday.Text = "Перенести на день назад";
            DiaryRescheduleYesterday.UseVisualStyleBackColor = true;
            DiaryRescheduleYesterday.Click += DiaryRescheduleYesterday_Click;
            // 
            // DiaryRescheduleButton
            // 
            DiaryRescheduleButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            DiaryRescheduleButton.Location = new Point(32, 15);
            DiaryRescheduleButton.Name = "DiaryRescheduleButton";
            DiaryRescheduleButton.Size = new Size(153, 60);
            DiaryRescheduleButton.TabIndex = 3;
            DiaryRescheduleButton.Text = "Перенести на завтра";
            DiaryRescheduleButton.UseVisualStyleBackColor = true;
            DiaryRescheduleButton.Click += DiaryRescheduleButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label2.ForeColor = Color.FromArgb(115, 0, 8);
            label2.Location = new Point(72, 354);
            label2.Name = "label2";
            label2.Size = new Size(105, 23);
            label2.TabIndex = 3;
            label2.Text = "Управління";
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(255, 253, 214);
            panel2.Controls.Add(panel5);
            panel2.Controls.Add(DiaryYesterdayButton);
            panel2.Controls.Add(DiaryTomorrowButton);
            panel2.Controls.Add(SearchByNameButton);
            panel2.Controls.Add(DiaryTextBox);
            panel2.Controls.Add(SearchOfDateButton);
            panel2.Controls.Add(DiaryTimePicker);
            panel2.Location = new Point(15, 51);
            panel2.Name = "panel2";
            panel2.Size = new Size(206, 287);
            panel2.TabIndex = 2;
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(255, 253, 214);
            panel5.Controls.Add(button1);
            panel5.Location = new Point(-44, 819);
            panel5.Name = "panel5";
            panel5.Size = new Size(206, 132);
            panel5.TabIndex = 7;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button1.Location = new Point(32, 15);
            button1.Name = "button1";
            button1.Size = new Size(153, 60);
            button1.TabIndex = 3;
            button1.Text = "Перенести на завтра";
            button1.UseVisualStyleBackColor = true;
            // 
            // DiaryYesterdayButton
            // 
            DiaryYesterdayButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            DiaryYesterdayButton.Location = new Point(31, 244);
            DiaryYesterdayButton.Name = "DiaryYesterdayButton";
            DiaryYesterdayButton.Size = new Size(146, 29);
            DiaryYesterdayButton.TabIndex = 5;
            DiaryYesterdayButton.Text = "Вчорашні справи";
            DiaryYesterdayButton.UseVisualStyleBackColor = true;
            DiaryYesterdayButton.Click += DiaryYesterdayButton_Click;
            // 
            // DiaryTomorrowButton
            // 
            DiaryTomorrowButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            DiaryTomorrowButton.Location = new Point(31, 200);
            DiaryTomorrowButton.Name = "DiaryTomorrowButton";
            DiaryTomorrowButton.Size = new Size(146, 29);
            DiaryTomorrowButton.TabIndex = 4;
            DiaryTomorrowButton.Text = "Справи на завтра";
            DiaryTomorrowButton.UseVisualStyleBackColor = true;
            DiaryTomorrowButton.Click += DiaryTomorrowButton_Click;
            // 
            // SearchByNameButton
            // 
            SearchByNameButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            SearchByNameButton.Location = new Point(31, 146);
            SearchByNameButton.Name = "SearchByNameButton";
            SearchByNameButton.Size = new Size(146, 29);
            SearchByNameButton.TabIndex = 3;
            SearchByNameButton.Text = "Пошук за назвою";
            SearchByNameButton.UseVisualStyleBackColor = true;
            SearchByNameButton.Click += SearchByNameButton_Click;
            // 
            // DiaryTextBox
            // 
            DiaryTextBox.Location = new Point(3, 104);
            DiaryTextBox.Name = "DiaryTextBox";
            DiaryTextBox.Size = new Size(200, 27);
            DiaryTextBox.TabIndex = 2;
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
            panel1.Controls.Add(DiaryCopyButton);
            panel1.Controls.Add(DiaryDeleteRowButton);
            panel1.Controls.Add(DiaryCleanButton);
            panel1.Location = new Point(15, 380);
            panel1.Name = "panel1";
            panel1.Size = new Size(206, 168);
            panel1.TabIndex = 1;
            // 
            // DiaryCopyButton
            // 
            DiaryCopyButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            DiaryCopyButton.Location = new Point(50, 118);
            DiaryCopyButton.Name = "DiaryCopyButton";
            DiaryCopyButton.Size = new Size(112, 29);
            DiaryCopyButton.TabIndex = 5;
            DiaryCopyButton.Text = "Дублювати";
            DiaryCopyButton.UseVisualStyleBackColor = true;
            DiaryCopyButton.Click += DiaryCopyButton_Click;
            // 
            // DiaryDeleteRowButton
            // 
            DiaryDeleteRowButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            DiaryDeleteRowButton.Location = new Point(29, 18);
            DiaryDeleteRowButton.Name = "DiaryDeleteRowButton";
            DiaryDeleteRowButton.Size = new Size(156, 29);
            DiaryDeleteRowButton.TabIndex = 4;
            DiaryDeleteRowButton.Text = "Видалити рядок";
            DiaryDeleteRowButton.UseVisualStyleBackColor = true;
            DiaryDeleteRowButton.Click += DiaryDeleteRowButton_Click;
            // 
            // DiaryCleanButton
            // 
            DiaryCleanButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            DiaryCleanButton.Location = new Point(50, 64);
            DiaryCleanButton.Name = "DiaryCleanButton";
            DiaryCleanButton.Size = new Size(112, 29);
            DiaryCleanButton.TabIndex = 3;
            DiaryCleanButton.Text = "Очистити";
            DiaryCleanButton.UseVisualStyleBackColor = true;
            DiaryCleanButton.Click += DiaryCleanButton_Click;
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
            DiaryTable.CellValidating += DiaryTable_CellValidating;
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
            DateOfEndColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            DateOfEndColumn.Width = 110;
            // 
            // DiaryTimer
            // 
            DiaryTimer.Tick += DiaryTimer_Tick;
            // 
            // DiaryDownloadButton
            // 
            DiaryDownloadButton.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            DiaryDownloadButton.Location = new Point(15, 998);
            DiaryDownloadButton.Name = "DiaryDownloadButton";
            DiaryDownloadButton.Size = new Size(203, 30);
            DiaryDownloadButton.TabIndex = 3;
            DiaryDownloadButton.Text = "Завантажити";
            DiaryDownloadButton.UseVisualStyleBackColor = true;
            DiaryDownloadButton.Click += DiaryDownloadButton_Click;
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
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel3.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel5.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DiaryTable).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox PictureBoxButton;
        private Panel PanelOfDiary;
        private DataGridView DiaryTable;
        private Panel panel1;
        private Panel panel2;
        private Label label1;
        private Button SearchOfDateButton;
        private DateTimePicker DiaryTimePicker;
        private Button SearchByNameButton;
        private TextBox DiaryTextBox;
        private Button DiaryTomorrowButton;
        private Button DiaryYesterdayButton;
        private Label label2;
        private Button DiaryCleanButton;
        private Button DiaryDeleteRowButton;
        private Button DiaryCopyButton;
        private Panel panel3;
        private Button DiaryRescheduleButton;
        private Button DiaryRescheduleYesterday;
        private Label label3;
        private Button DiaryOverlayButton;
        private DataGridViewTextBoxColumn TitleColumn;
        private DataGridViewTextBoxColumn DescColumn;
        private DataGridViewTextBoxColumn PlaceColumn;
        private DataGridViewTextBoxColumn DateColumn;
        private DataGridViewTextBoxColumn TimeColumn;
        private DataGridViewTextBoxColumn DurationColumn;
        private DataGridViewTextBoxColumn DateOfEndColumn;
        private Label label4;
        private Panel panel4;
        private Button DiaryStopButton;
        private Button DiaryStartButton;
        private TextBox DiaryTimeBox;
        private Panel panel5;
        private Button button1;
        private System.Windows.Forms.Timer DiaryTimer;
        private Button DiaryDownloadButton;
    }
}