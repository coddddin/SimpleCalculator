using System;
using System.Windows.Forms;

namespace SimpleCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void calc_Form_Load(object sender, EventArgs e)
        {

        }

        // 숫자 버튼들의 Click 이벤트 핸들러를 추가합니다.
        private void NumberButton_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                // result_Textbox에 클릭된 버튼의 텍스트(숫자)를 추가합니다.
                // 현재 텍스트가 비어있거나 "0"이면 대체합니다.
                if (string.IsNullOrEmpty(state_Textbox.Text) || state_Textbox.Text == "0")
                    state_Textbox.Text = btn.Text;
                else
                    state_Textbox.Text += btn.Text;
            }
        }
    }
}
