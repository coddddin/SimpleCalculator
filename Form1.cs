using System;
using System.Windows.Forms;

namespace SimpleCalculator
{
    public partial class Form1 : Form
    {
        // 덧셈을 위한 상태 필드 1차과제 완료
        private int? firstNumber = null;
        private bool awaitingSecond = false;

        public Form1()
        {
            InitializeComponent();

            // 연산자 및 결과 버튼 이벤트 연결 (더하기만 구현)
            plus_btn.Click += PlusButton_Click;
            // 뺄셈 버튼 이벤트 연결 추가
            minus_btn.Click += MinusButton_Click;
            result_btn.Click += ResultButton_Click;
        }

        private void calc_Form_Load(object sender, EventArgs e)
        {

        }

        // 숫자 버튼들의 Click 이벤트 핸들러를 추가합니다.
        private void NumberButton_Click(object sender, EventArgs e)
        {
            if (!(sender is Button btn))
                return;

            // 이전에 완료된 식(" = ")이 있으면 새 식으로 시작
            if (state_Textbox.Text.Contains(" = "))
            {
                state_Textbox.Text = string.Empty;
                result_Textbox.Text = string.Empty;
                firstNumber = null;
                awaitingSecond = false;
            }

            // 만약 연산자 뒤(예: "12 + ")인 경우라면 단순히 이어붙여 피연산자 작성
            if (awaitingSecond)
            {
                state_Textbox.Text += btn.Text;
                return;
            }

            // 기본: 빈 칸 또는 "0"이면 교체, 아니면 이어붙임
            if (string.IsNullOrEmpty(state_Textbox.Text) || state_Textbox.Text == "0")
                state_Textbox.Text = btn.Text;
            else
                state_Textbox.Text += btn.Text;
        }

        // '+' 버튼 핸들러: state에 연산자 표시하고 첫 번째 숫자 저장
        private void PlusButton_Click(object sender, EventArgs e)
        {
            // 이미 결과가 표시된 경우(예: "1 + 2 = 3") 결과를 좌항으로 사용
            if (state_Textbox.Text.Contains(" = "))
            {
                if (int.TryParse(result_Textbox.Text, out int res))
                {
                    state_Textbox.Text = res.ToString();
                    result_Textbox.Text = string.Empty;
                }
            }

            var tokens = state_Textbox.Text.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // "숫자"만 존재하면 연산자 추가
            if (tokens.Length == 1 && int.TryParse(tokens[0], out int n))
            {
                firstNumber = n;
                state_Textbox.Text = $"{n} + ";
                awaitingSecond = true;
                return;
            }

            // "숫자 연산자 숫자" 형태면 중간 계산하고 이어서 더하기(체인)
            if (tokens.Length >= 3 && int.TryParse(tokens[0], out int left) && tokens[1] == "+" && int.TryParse(tokens[2], out int right))
            {
                int sum = left + right;
                result_Textbox.Text = sum.ToString();
                firstNumber = sum;
                state_Textbox.Text = $"{sum} + ";
                awaitingSecond = true;
                return;
            }

            // "숫자 연산자" 상태(예: "12 + ")이면 연산자 교체는 필요 없으므로 무시
        }

        // '-' 버튼 핸들러: state에 연산자 표시하고 첫 번째 숫자 저장 (뺄셈 구현)
        private void MinusButton_Click(object sender, EventArgs e)
        {
            // 이미 결과가 표시된 경우(예: "1 + 2 = 3") 결과를 좌항으로 사용
            if (state_Textbox.Text.Contains(" = "))
            {
                if (int.TryParse(result_Textbox.Text, out int res))
                {
                    state_Textbox.Text = res.ToString();
                    result_Textbox.Text = string.Empty;
                }
            }

            var tokens = state_Textbox.Text.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // "숫자"만 존재하면 연산자 추가
            if (tokens.Length == 1 && int.TryParse(tokens[0], out int n))
            {
                firstNumber = n;
                state_Textbox.Text = $"{n} - ";
                awaitingSecond = true;
                return;
            }

            // "숫자 연산자 숫자" 형태면 중간 계산하고 이어서 빼기(체인)
            if (tokens.Length >= 3 && int.TryParse(tokens[0], out int left) && tokens[1] == "-" && int.TryParse(tokens[2], out int right))
            {
                int diff = left - right;
                result_Textbox.Text = diff.ToString();
                firstNumber = diff;
                state_Textbox.Text = $"{diff} - ";
                awaitingSecond = true;
                return;
            }

            // "숫자 연산자" 상태(예: "12 - ")이면 연산자 교체는 필요 없으므로 무시
        }

        // '=' 버튼 핸들러: "a + b" 또는 "a - b" 계산 후 state에 " = 결과" 추가, result_Textbox에 결과만 표시
        private void ResultButton_Click(object sender, EventArgs e)
        {
            // 이미 계산된 상태면 무시
            if (state_Textbox.Text.Contains(" = "))
                return;

            var tokens = state_Textbox.Text.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length >= 3 && int.TryParse(tokens[0], out int left) && int.TryParse(tokens[2], out int right))
            {
                string op = tokens[1];
                if (op == "+")
                {
                    int sum = left + right;
                    result_Textbox.Text = sum.ToString();
                    state_Textbox.Text = $"{left} + {right} = {sum}";
                }
                else if (op == "-")
                {
                    int diff = left - right;
                    result_Textbox.Text = diff.ToString();
                    state_Textbox.Text = $"{left} - {right} = {diff}";
                }
                firstNumber = null;
                awaitingSecond = false;
            }
            else
            {
                // 피연산자 부족인 경우: 현재 입력 숫자를 결과로 간주
                if (int.TryParse(state_Textbox.Text.Trim(), out int single))
                {
                    result_Textbox.Text = single.ToString();
                    state_Textbox.Text = $"{single} = {single}";
                    firstNumber = null;
                    awaitingSecond = false;
                }
            }
        }

        private void pandm_btn_Click(object sender, EventArgs e)
        {

        }
    }
}
