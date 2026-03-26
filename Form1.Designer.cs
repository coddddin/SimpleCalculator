namespace SimpleCalculator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            state_Textbox = new TextBox();
            result_Textbox = new TextBox();
            CE_btn = new Button();
            lblAPPName = new Label();
            C_btn = new Button();
            button3 = new Button();
            div_btn = new Button();
            mul_btn = new Button();
            btn9 = new Button();
            btn8 = new Button();
            btn7 = new Button();
            minus_btn = new Button();
            btn6 = new Button();
            btn5 = new Button();
            btn4 = new Button();
            plus_btn = new Button();
            btn3 = new Button();
            btn2 = new Button();
            btn1 = new Button();
            result_btn = new Button();
            button18 = new Button();
            btn0 = new Button();
            pandm_btn = new Button();
            SuspendLayout();
            // 
            // state_Textbox
            // 
            state_Textbox.Font = new Font("굴림", 14.25F, FontStyle.Bold);
            state_Textbox.Location = new Point(56, 121);
            state_Textbox.Name = "state_Textbox";
            state_Textbox.Size = new Size(255, 29);
            state_Textbox.TabIndex = 0;
            // 
            // result_Textbox
            // 
            result_Textbox.Font = new Font("굴림", 14.25F, FontStyle.Bold);
            result_Textbox.Location = new Point(56, 156);
            result_Textbox.Name = "result_Textbox";
            result_Textbox.Size = new Size(255, 29);
            result_Textbox.TabIndex = 1;
            // 
            // CE_btn
            // 
            CE_btn.BackColor = Color.OldLace;
            CE_btn.Font = new Font("한컴 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            CE_btn.ForeColor = Color.Black;
            CE_btn.Location = new Point(51, 193);
            CE_btn.Name = "CE_btn";
            CE_btn.Size = new Size(66, 42);
            CE_btn.TabIndex = 2;
            CE_btn.Text = "CE";
            CE_btn.UseVisualStyleBackColor = false;
            // 
            // lblAPPName
            // 
            lblAPPName.AutoSize = true;
            lblAPPName.Font = new Font("한컴 고딕", 35.9999962F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblAPPName.ForeColor = Color.Blue;
            lblAPPName.Location = new Point(51, 34);
            lblAPPName.Name = "lblAPPName";
            lblAPPName.Size = new Size(265, 62);
            lblAPPName.TabIndex = 3;
            lblAPPName.Text = "간단 계산기";
            // 
            // C_btn
            // 
            C_btn.BackColor = Color.OldLace;
            C_btn.Font = new Font("한컴 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            C_btn.Location = new Point(119, 193);
            C_btn.Name = "C_btn";
            C_btn.Size = new Size(66, 42);
            C_btn.TabIndex = 4;
            C_btn.Text = "C";
            C_btn.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            button3.BackColor = Color.OldLace;
            button3.Font = new Font("한컴 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button3.Location = new Point(185, 193);
            button3.Name = "button3";
            button3.Size = new Size(66, 42);
            button3.TabIndex = 5;
            button3.Text = "del";
            button3.UseVisualStyleBackColor = false;
            // 
            // div_btn
            // 
            div_btn.BackColor = Color.OldLace;
            div_btn.Font = new Font("한컴 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            div_btn.ForeColor = Color.Red;
            div_btn.Location = new Point(250, 193);
            div_btn.Name = "div_btn";
            div_btn.Size = new Size(66, 42);
            div_btn.TabIndex = 6;
            div_btn.Text = "÷";
            div_btn.UseVisualStyleBackColor = false;
            // 
            // mul_btn
            // 
            mul_btn.BackColor = Color.OldLace;
            mul_btn.Font = new Font("한컴 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            mul_btn.ForeColor = Color.Red;
            mul_btn.Location = new Point(250, 234);
            mul_btn.Name = "mul_btn";
            mul_btn.Size = new Size(66, 42);
            mul_btn.TabIndex = 10;
            mul_btn.Text = "x";
            mul_btn.UseVisualStyleBackColor = false;
            // 
            // btn9
            // 
            btn9.BackColor = Color.OldLace;
            btn9.Font = new Font("한컴 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn9.ForeColor = Color.Blue;
            btn9.Location = new Point(185, 234);
            btn9.Name = "btn9";
            btn9.Size = new Size(66, 42);
            btn9.TabIndex = 9;
            btn9.Text = "9";
            btn9.UseVisualStyleBackColor = false;
            btn9.Click += NumberButton_Click;
            // 
            // btn8
            // 
            btn8.BackColor = Color.OldLace;
            btn8.Font = new Font("한컴 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn8.ForeColor = Color.Blue;
            btn8.Location = new Point(119, 234);
            btn8.Name = "btn8";
            btn8.Size = new Size(66, 42);
            btn8.TabIndex = 8;
            btn8.Text = "8";
            btn8.UseVisualStyleBackColor = false;
            btn8.Click += NumberButton_Click;
            // 
            // btn7
            // 
            btn7.BackColor = Color.OldLace;
            btn7.Font = new Font("한컴 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn7.ForeColor = Color.Blue;
            btn7.Location = new Point(51, 234);
            btn7.Name = "btn7";
            btn7.Size = new Size(66, 42);
            btn7.TabIndex = 7;
            btn7.Text = "7";
            btn7.UseVisualStyleBackColor = false;
            btn7.Click += NumberButton_Click;
            // 
            // minus_btn
            // 
            minus_btn.BackColor = Color.OldLace;
            minus_btn.Font = new Font("한컴 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            minus_btn.ForeColor = Color.Red;
            minus_btn.Location = new Point(250, 275);
            minus_btn.Name = "minus_btn";
            minus_btn.Size = new Size(66, 42);
            minus_btn.TabIndex = 14;
            minus_btn.Text = "-";
            minus_btn.UseVisualStyleBackColor = false;
            // 
            // btn6
            // 
            btn6.BackColor = Color.OldLace;
            btn6.Font = new Font("한컴 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn6.ForeColor = Color.Blue;
            btn6.Location = new Point(185, 275);
            btn6.Name = "btn6";
            btn6.Size = new Size(66, 42);
            btn6.TabIndex = 13;
            btn6.Text = "6";
            btn6.UseVisualStyleBackColor = false;
            btn6.Click += NumberButton_Click;
            // 
            // btn5
            // 
            btn5.BackColor = Color.OldLace;
            btn5.Font = new Font("한컴 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn5.ForeColor = Color.Blue;
            btn5.Location = new Point(119, 275);
            btn5.Name = "btn5";
            btn5.Size = new Size(66, 42);
            btn5.TabIndex = 12;
            btn5.Text = "5";
            btn5.UseVisualStyleBackColor = false;
            btn5.Click += NumberButton_Click;
            // 
            // btn4
            // 
            btn4.BackColor = Color.OldLace;
            btn4.Font = new Font("한컴 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn4.ForeColor = Color.Blue;
            btn4.Location = new Point(51, 275);
            btn4.Name = "btn4";
            btn4.Size = new Size(66, 42);
            btn4.TabIndex = 11;
            btn4.Text = "4";
            btn4.UseVisualStyleBackColor = false;
            btn4.Click += NumberButton_Click;
            // 
            // plus_btn
            // 
            plus_btn.BackColor = Color.OldLace;
            plus_btn.Font = new Font("한컴 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            plus_btn.ForeColor = Color.Red;
            plus_btn.Location = new Point(250, 316);
            plus_btn.Name = "plus_btn";
            plus_btn.Size = new Size(66, 42);
            plus_btn.TabIndex = 18;
            plus_btn.Text = "+";
            plus_btn.UseVisualStyleBackColor = false;
            // 
            // btn3
            // 
            btn3.BackColor = Color.OldLace;
            btn3.Font = new Font("한컴 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn3.ForeColor = Color.Blue;
            btn3.Location = new Point(185, 316);
            btn3.Name = "btn3";
            btn3.Size = new Size(66, 42);
            btn3.TabIndex = 17;
            btn3.Text = "3";
            btn3.UseVisualStyleBackColor = false;
            btn3.Click += NumberButton_Click;
            // 
            // btn2
            // 
            btn2.BackColor = Color.OldLace;
            btn2.Font = new Font("한컴 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn2.ForeColor = Color.Blue;
            btn2.Location = new Point(119, 316);
            btn2.Name = "btn2";
            btn2.Size = new Size(66, 42);
            btn2.TabIndex = 16;
            btn2.Text = "2";
            btn2.UseVisualStyleBackColor = false;
            btn2.Click += NumberButton_Click;
            // 
            // btn1
            // 
            btn1.BackColor = Color.OldLace;
            btn1.Font = new Font("한컴 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn1.ForeColor = Color.Blue;
            btn1.Location = new Point(51, 316);
            btn1.Name = "btn1";
            btn1.Size = new Size(66, 42);
            btn1.TabIndex = 15;
            btn1.Text = "1";
            btn1.UseVisualStyleBackColor = false;
            btn1.Click += NumberButton_Click;
            // 
            // result_btn
            // 
            result_btn.BackColor = Color.OldLace;
            result_btn.Font = new Font("한컴 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            result_btn.Location = new Point(250, 357);
            result_btn.Name = "result_btn";
            result_btn.Size = new Size(66, 42);
            result_btn.TabIndex = 22;
            result_btn.Text = "=";
            result_btn.UseVisualStyleBackColor = false;
            // 
            // button18
            // 
            button18.BackColor = Color.OldLace;
            button18.Font = new Font("한컴 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button18.Location = new Point(185, 357);
            button18.Name = "button18";
            button18.Size = new Size(66, 42);
            button18.TabIndex = 21;
            button18.Text = ".";
            button18.UseVisualStyleBackColor = false;
            // 
            // btn0
            // 
            btn0.BackColor = Color.OldLace;
            btn0.Font = new Font("한컴 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn0.ForeColor = Color.Blue;
            btn0.Location = new Point(119, 357);
            btn0.Name = "btn0";
            btn0.Size = new Size(66, 42);
            btn0.TabIndex = 20;
            btn0.Text = "0";
            btn0.UseVisualStyleBackColor = false;
            btn0.Click += NumberButton_Click;
            // 
            // pandm_btn
            // 
            pandm_btn.BackColor = Color.OldLace;
            pandm_btn.Font = new Font("한컴 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            pandm_btn.Location = new Point(51, 357);
            pandm_btn.Name = "pandm_btn";
            pandm_btn.Size = new Size(66, 42);
            pandm_btn.TabIndex = 19;
            pandm_btn.Text = "+/-";
            pandm_btn.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientInactiveCaption;
            ClientSize = new Size(375, 434);
            Controls.Add(result_btn);
            Controls.Add(button18);
            Controls.Add(btn0);
            Controls.Add(pandm_btn);
            Controls.Add(plus_btn);
            Controls.Add(btn3);
            Controls.Add(btn2);
            Controls.Add(btn1);
            Controls.Add(minus_btn);
            Controls.Add(btn6);
            Controls.Add(btn5);
            Controls.Add(btn4);
            Controls.Add(mul_btn);
            Controls.Add(btn9);
            Controls.Add(btn8);
            Controls.Add(btn7);
            Controls.Add(div_btn);
            Controls.Add(button3);
            Controls.Add(C_btn);
            Controls.Add(lblAPPName);
            Controls.Add(CE_btn);
            Controls.Add(result_Textbox);
            Controls.Add(state_Textbox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Form1";
            Text = "계산기";
            Load += calc_Form_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox state_Textbox;
        private TextBox result_Textbox;
        private Button CE_btn;
        private Label lblAPPName;
        private Button C_btn;
        private Button button3;
        private Button div_btn;
        private Button mul_btn;
        private Button btn9;
        private Button btn8;
        private Button btn7;
        private Button minus_btn;
        private Button btn6;
        private Button btn5;
        private Button btn4;
        private Button plus_btn;
        private Button btn3;
        private Button btn2;
        private Button btn1;
        private Button result_btn;
        private Button button18;
        private Button btn0;
        private Button pandm_btn;
    }
}
