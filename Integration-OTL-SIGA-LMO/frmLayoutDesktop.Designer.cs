
namespace Integration_OTL_SIGA_LMO
{
    partial class frmLayoutDesktop
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlParameters;
        private FontAwesome.Sharp.IconButton btnExit;
        private System.Windows.Forms.Panel pnlResults;
        private System.Windows.Forms.DataGridView dgvStationRows;
        private System.Windows.Forms.Label lblCount;
        private FontAwesome.Sharp.IconPictureBox imgLaptopHome;
        private System.Windows.Forms.Timer timerIntegration;
        private System.Windows.Forms.Label lblNext;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlParameters = new System.Windows.Forms.Panel();
            this.txtOrderNumber = new System.Windows.Forms.TextBox();
            this.btnQueryByDates = new System.Windows.Forms.Button();
            this.chkActivaRangoFechas = new System.Windows.Forms.CheckBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.lblNext = new System.Windows.Forms.Label();
            this.imgLaptopHome = new FontAwesome.Sharp.IconPictureBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.btnExit = new FontAwesome.Sharp.IconButton();
            this.pnlResults = new System.Windows.Forms.Panel();
            this.dgvStationRows = new System.Windows.Forms.DataGridView();
            this.timerIntegration = new System.Windows.Forms.Timer(this.components);
            this.pnlParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgLaptopHome)).BeginInit();
            this.pnlResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStationRows)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlParameters
            // 
            this.pnlParameters.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.pnlParameters.Controls.Add(this.txtOrderNumber);
            this.pnlParameters.Controls.Add(this.btnQueryByDates);
            this.pnlParameters.Controls.Add(this.chkActivaRangoFechas);
            this.pnlParameters.Controls.Add(this.dtpFechaFinal);
            this.pnlParameters.Controls.Add(this.dtpFechaInicial);
            this.pnlParameters.Controls.Add(this.lblNext);
            this.pnlParameters.Controls.Add(this.imgLaptopHome);
            this.pnlParameters.Controls.Add(this.lblCount);
            this.pnlParameters.Controls.Add(this.btnExit);
            this.pnlParameters.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlParameters.Location = new System.Drawing.Point(0, 0);
            this.pnlParameters.Name = "pnlParameters";
            this.pnlParameters.Size = new System.Drawing.Size(1226, 50);
            this.pnlParameters.TabIndex = 3;
            // 
            // txtOrderNumber
            // 
            this.txtOrderNumber.ForeColor = System.Drawing.Color.LightSlateGray;
            this.txtOrderNumber.Location = new System.Drawing.Point(729, 13);
            this.txtOrderNumber.Name = "txtOrderNumber";
            this.txtOrderNumber.Size = new System.Drawing.Size(179, 23);
            this.txtOrderNumber.TabIndex = 8;
            this.txtOrderNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtOrderNumber.TextChanged += new System.EventHandler(this.txtOrderNumber_TextChanged);
            this.txtOrderNumber.Enter += new System.EventHandler(this.txtOrderNumber_Enter);
            this.txtOrderNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOrderNumber_KeyPress);
            this.txtOrderNumber.Leave += new System.EventHandler(this.txtOrderNumber_Leave);
            // 
            // btnQueryByDates
            // 
            this.btnQueryByDates.Enabled = false;
            this.btnQueryByDates.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnQueryByDates.ForeColor = System.Drawing.Color.Indigo;
            this.btnQueryByDates.Location = new System.Drawing.Point(574, 12);
            this.btnQueryByDates.Name = "btnQueryByDates";
            this.btnQueryByDates.Size = new System.Drawing.Size(97, 26);
            this.btnQueryByDates.TabIndex = 7;
            this.btnQueryByDates.Text = "Query dates";
            this.btnQueryByDates.UseVisualStyleBackColor = true;
            this.btnQueryByDates.Click += new System.EventHandler(this.btnQueryByDates_Click);
            // 
            // chkActivaRangoFechas
            // 
            this.chkActivaRangoFechas.AutoSize = true;
            this.chkActivaRangoFechas.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.chkActivaRangoFechas.ForeColor = System.Drawing.Color.Indigo;
            this.chkActivaRangoFechas.Location = new System.Drawing.Point(153, 15);
            this.chkActivaRangoFechas.Name = "chkActivaRangoFechas";
            this.chkActivaRangoFechas.Size = new System.Drawing.Size(188, 20);
            this.chkActivaRangoFechas.TabIndex = 6;
            this.chkActivaRangoFechas.Text = "Activa Rango de Fechas:";
            this.chkActivaRangoFechas.UseVisualStyleBackColor = true;
            this.chkActivaRangoFechas.CheckedChanged += new System.EventHandler(this.chkActivaRangoFechas_CheckedChanged);
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.Enabled = false;
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFinal.Location = new System.Drawing.Point(462, 13);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(105, 23);
            this.dtpFechaFinal.TabIndex = 5;
            this.dtpFechaFinal.CloseUp += new System.EventHandler(this.dtpFechaFinal_CloseUp);
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.Enabled = false;
            this.dtpFechaInicial.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicial.Location = new System.Drawing.Point(347, 13);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(109, 23);
            this.dtpFechaInicial.TabIndex = 4;
            this.dtpFechaInicial.CloseUp += new System.EventHandler(this.dtpFechaInicial_CloseUp);
            this.dtpFechaInicial.ValueChanged += new System.EventHandler(this.txtOrderNumber_Leave);
            // 
            // lblNext
            // 
            this.lblNext.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblNext.ForeColor = System.Drawing.Color.Indigo;
            this.lblNext.Location = new System.Drawing.Point(1030, 16);
            this.lblNext.Name = "lblNext";
            this.lblNext.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblNext.Size = new System.Drawing.Size(110, 16);
            this.lblNext.TabIndex = 3;
            this.lblNext.Text = "Next: 0";
            this.lblNext.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // imgLaptopHome
            // 
            this.imgLaptopHome.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.imgLaptopHome.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.imgLaptopHome.ForeColor = System.Drawing.Color.Moccasin;
            this.imgLaptopHome.IconChar = FontAwesome.Sharp.IconChar.LaptopHouse;
            this.imgLaptopHome.IconColor = System.Drawing.Color.Moccasin;
            this.imgLaptopHome.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.imgLaptopHome.IconSize = 42;
            this.imgLaptopHome.Location = new System.Drawing.Point(79, 3);
            this.imgLaptopHome.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.imgLaptopHome.Name = "imgLaptopHome";
            this.imgLaptopHome.Size = new System.Drawing.Size(49, 42);
            this.imgLaptopHome.TabIndex = 2;
            this.imgLaptopHome.TabStop = false;
            this.imgLaptopHome.Click += new System.EventHandler(this.imgLaptopHome_Click);
            // 
            // lblCount
            // 
            this.lblCount.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCount.ForeColor = System.Drawing.Color.Indigo;
            this.lblCount.Location = new System.Drawing.Point(944, 17);
            this.lblCount.Name = "lblCount";
            this.lblCount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCount.Size = new System.Drawing.Size(80, 16);
            this.lblCount.TabIndex = 1;
            this.lblCount.Text = "Count: 0";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ForeColor = System.Drawing.Color.Black;
            this.btnExit.IconChar = FontAwesome.Sharp.IconChar.SignOutAlt;
            this.btnExit.IconColor = System.Drawing.Color.Indigo;
            this.btnExit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnExit.IconSize = 32;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.Location = new System.Drawing.Point(1154, 6);
            this.btnExit.Margin = new System.Windows.Forms.Padding(0, 10, 10, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(62, 38);
            this.btnExit.TabIndex = 0;
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // pnlResults
            // 
            this.pnlResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlResults.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.pnlResults.Controls.Add(this.dgvStationRows);
            this.pnlResults.Location = new System.Drawing.Point(0, 47);
            this.pnlResults.Name = "pnlResults";
            this.pnlResults.Size = new System.Drawing.Size(1226, 559);
            this.pnlResults.TabIndex = 2;
            // 
            // dgvStationRows
            // 
            this.dgvStationRows.AllowUserToAddRows = false;
            this.dgvStationRows.AllowUserToDeleteRows = false;
            this.dgvStationRows.AllowUserToOrderColumns = true;
            this.dgvStationRows.AllowUserToResizeColumns = false;
            this.dgvStationRows.AllowUserToResizeRows = false;
            this.dgvStationRows.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStationRows.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStationRows.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvStationRows.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
            this.dgvStationRows.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvStationRows.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvStationRows.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStationRows.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvStationRows.ColumnHeadersHeight = 30;
            this.dgvStationRows.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStationRows.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvStationRows.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvStationRows.EnableHeadersVisualStyles = false;
            this.dgvStationRows.GridColor = System.Drawing.Color.SteelBlue;
            this.dgvStationRows.Location = new System.Drawing.Point(12, 20);
            this.dgvStationRows.Name = "dgvStationRows";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStationRows.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.dgvStationRows.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvStationRows.RowTemplate.Height = 25;
            this.dgvStationRows.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStationRows.Size = new System.Drawing.Size(1202, 526);
            this.dgvStationRows.TabIndex = 5;
            // 
            // timerIntegration
            // 
            this.timerIntegration.Tick += new System.EventHandler(this.timerIntegration_Tick);
            // 
            // frmLayoutDesktop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1226, 605);
            this.Controls.Add(this.pnlParameters);
            this.Controls.Add(this.pnlResults);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.Name = "frmLayoutDesktop";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trace Orders: Integration - OTL - SiGA - LMO    \\\\//    Time elapsed: 30 minutes";
            this.Load += new System.EventHandler(this.frmLayoutDesktop_Load);
            this.pnlParameters.ResumeLayout(false);
            this.pnlParameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgLaptopHome)).EndInit();
            this.pnlResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStationRows)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

    }
}

