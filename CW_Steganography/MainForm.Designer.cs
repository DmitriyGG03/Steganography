﻿namespace Stenography
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pb_activePhoto = new System.Windows.Forms.PictureBox();
            this.tb_codingText = new System.Windows.Forms.TextBox();
            this.bt_choosePict = new System.Windows.Forms.Button();
            this.bt_encoding = new System.Windows.Forms.Button();
            this.bt_coding = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pb_activePhoto)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_activePhoto
            // 
            this.pb_activePhoto.Location = new System.Drawing.Point(13, 13);
            this.pb_activePhoto.Margin = new System.Windows.Forms.Padding(4);
            this.pb_activePhoto.Name = "pb_activePhoto";
            this.pb_activePhoto.Size = new System.Drawing.Size(500, 500);
            this.pb_activePhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_activePhoto.TabIndex = 0;
            this.pb_activePhoto.TabStop = false;
            // 
            // tb_codingText
            // 
            this.tb_codingText.Enabled = false;
            this.tb_codingText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tb_codingText.Location = new System.Drawing.Point(521, 13);
            this.tb_codingText.Margin = new System.Windows.Forms.Padding(4);
            this.tb_codingText.Multiline = true;
            this.tb_codingText.Name = "tb_codingText";
            this.tb_codingText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_codingText.Size = new System.Drawing.Size(405, 158);
            this.tb_codingText.TabIndex = 1;
            this.tb_codingText.TextChanged += new System.EventHandler(this.textToCode_TextChanged);
            // 
            // bt_choosePict
            // 
            this.bt_choosePict.BackColor = System.Drawing.Color.SkyBlue;
            this.bt_choosePict.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bt_choosePict.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.bt_choosePict.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.bt_choosePict.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_choosePict.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bt_choosePict.Location = new System.Drawing.Point(13, 521);
            this.bt_choosePict.Margin = new System.Windows.Forms.Padding(4);
            this.bt_choosePict.Name = "bt_choosePict";
            this.bt_choosePict.Size = new System.Drawing.Size(500, 58);
            this.bt_choosePict.TabIndex = 2;
            this.bt_choosePict.Text = "Обрати зображення";
            this.bt_choosePict.UseVisualStyleBackColor = false;
            this.bt_choosePict.Click += new System.EventHandler(this.bt_choosePict_Click);
            // 
            // bt_encoding
            // 
            this.bt_encoding.BackColor = System.Drawing.Color.Salmon;
            this.bt_encoding.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bt_encoding.Enabled = false;
            this.bt_encoding.FlatAppearance.MouseDownBackColor = System.Drawing.Color.OrangeRed;
            this.bt_encoding.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Tomato;
            this.bt_encoding.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_encoding.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bt_encoding.Location = new System.Drawing.Point(572, 333);
            this.bt_encoding.Margin = new System.Windows.Forms.Padding(4);
            this.bt_encoding.Name = "bt_encoding";
            this.bt_encoding.Size = new System.Drawing.Size(290, 80);
            this.bt_encoding.TabIndex = 3;
            this.bt_encoding.Text = "Декодувати зображення";
            this.bt_encoding.UseVisualStyleBackColor = false;
            this.bt_encoding.Click += new System.EventHandler(this.buttonDecode_Click);
            // 
            // bt_coding
            // 
            this.bt_coding.BackColor = System.Drawing.Color.LightGreen;
            this.bt_coding.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bt_coding.Enabled = false;
            this.bt_coding.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGreen;
            this.bt_coding.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.bt_coding.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_coding.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bt_coding.Location = new System.Drawing.Point(572, 205);
            this.bt_coding.Margin = new System.Windows.Forms.Padding(4);
            this.bt_coding.Name = "bt_coding";
            this.bt_coding.Size = new System.Drawing.Size(290, 80);
            this.bt_coding.TabIndex = 4;
            this.bt_coding.Text = "Закодувати текст в зображення";
            this.bt_coding.UseVisualStyleBackColor = false;
            this.bt_coding.Click += new System.EventHandler(this.bt_coding_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Salmon;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Enabled = false;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.OrangeRed;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Tomato;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(572, 465);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(290, 80);
            this.button1.TabIndex = 5;
            this.button1.Text = "Видалити кодування із зображення";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Moccasin;
            this.ClientSize = new System.Drawing.Size(933, 589);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.bt_coding);
            this.Controls.Add(this.bt_encoding);
            this.Controls.Add(this.bt_choosePict);
            this.Controls.Add(this.tb_codingText);
            this.Controls.Add(this.pb_activePhoto);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(949, 628);
            this.MinimumSize = new System.Drawing.Size(949, 628);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Стеганографія";
            ((System.ComponentModel.ISupportInitialize)(this.pb_activePhoto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_activePhoto;
        private System.Windows.Forms.TextBox tb_codingText;
        private System.Windows.Forms.Button bt_choosePict;
        private System.Windows.Forms.Button bt_encoding;
        private System.Windows.Forms.Button bt_coding;
        private System.Windows.Forms.Button button1;
    }
}

