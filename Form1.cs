using System;
using System.Windows.Forms;

namespace SimpleCalculator
{
    public partial class Form1 : Form
    {
        // 덧셈을 위한 상태 필드 1차과제 완료
        private double? firstNumber = null;
        private bool awaitingSecond = false;

        public Form1()
        {
            InitializeComponent();

            // 연산자 및 결과 버튼 이벤트 연결
            plus_btn.Click += PlusButton_Click;
            minus_btn.Click += MinusButton_Click;
            mul_btn.Click += MulButton_Click;
            div_btn.Click += DivButton_Click;
            result_btn.Click += ResultButton_Click;

            // C 버튼 클릭 이벤트 연결 (전체 초기화)
            C_btn.Click += C_btn_Click;
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
            PrepareOperator("+");
        }

        // '-' 버튼 핸들러
        private void MinusButton_Click(object sender, EventArgs e)
        {
            PrepareOperator("-");
        }

        // 'x' 버튼 핸들러
        private void MulButton_Click(object sender, EventArgs e)
        {
            PrepareOperator("x");
        }

        // '÷' 또는 '/' 버튼 핸들러
        private void DivButton_Click(object sender, EventArgs e)
        {
            PrepareOperator("÷");
        }

        // 공통: 연산자 준비 및 체인 연산 처리
        private void PrepareOperator(string opSymbol)
        {
            // 이미 결과가 표시된 경우 결과를 좌항으로 사용
            if (state_Textbox.Text.Contains(" = "))
            {
                if (double.TryParse(result_Textbox.Text, out double res))
                {
                    state_Textbox.Text = res.ToString();
                    result_Textbox.Text = string.Empty;
                }
            }

            var tokens = state_Textbox.Text.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // "숫자"만 존재하면 연산자 추가
            if (tokens.Length == 1 && double.TryParse(tokens[0], out double n))
            {
                firstNumber = n;
                state_Textbox.Text = $"{FormatNumber(n)} {opSymbol} ";
                awaitingSecond = true;
                return;
            }

            // "숫자 연산자 숫자" 형태면 중간 계산하고 이어서 연산(체인)
            if (tokens.Length >= 3 && double.TryParse(tokens[0], out double left) && double.TryParse(tokens[2], out double right))
            {
                string currentOp = tokens[1];
                double intermediate = Compute(left, right, currentOp);
                result_Textbox.Text = FormatNumber(intermediate);
                firstNumber = intermediate;
                state_Textbox.Text = $"{FormatNumber(intermediate)} {opSymbol} ";
                awaitingSecond = true;
                return;
            }

            // 다른 상태(예: 빈 등)에서는 무시
        }

        // '=' 버튼 핸들러: "a op b" 계산 후 state에 " = 결과" 추가, result_Textbox에 결과만 표시
        private void ResultButton_Click(object sender, EventArgs e)
        {
            // 이미 계산된 상태면 무시
            if (state_Textbox.Text.Contains(" = "))
                return;

            var tokens = state_Textbox.Text.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length >= 3 && double.TryParse(tokens[0], out double left) && double.TryParse(tokens[2], out double right))
            {
                string op = tokens[1];
                double res = Compute(left, right, op);
                result_Textbox.Text = FormatNumber(res);
                state_Textbox.Text = $"{FormatNumber(left)} {op} {FormatNumber(right)} = {FormatNumber(res)}";
                firstNumber = null;
                awaitingSecond = false;
            }
            else
            {
                // 피연산자 부족인 경우: 현재 입력 숫자를 결과로 간주
                if (double.TryParse(state_Textbox.Text.Trim(), out double single))
                {
                    result_Textbox.Text = FormatNumber(single);
                    state_Textbox.Text = $"{FormatNumber(single)} = {FormatNumber(single)}";
                    firstNumber = null;
                    awaitingSecond = false;
                }
            }
        }

        // 실제 연산 수행 (double 지원, 나눗셈 0 처리)
        private double Compute(double left, double right, string op)
        {
            return op switch
            {
                "+" => left + right,
                "-" => left - right,
                "x" or "X" or "*" => left * right,
                "÷" or "/" => right == 0 ? double.NaN : left / right,
                _ => right
            };
        }

        // 결과 표시 포맷: 정수면 소수점 제거
        private string FormatNumber(double v)
        {
            if (double.IsNaN(v)) return "NaN";
            if (Math.Abs(v % 1) < 1e-12) return ((long)v).ToString();
            return v.ToString();
        }

        // C 버튼: 모든 내용 초기화 (텍스트박스 및 내부 상태)
        private void C_btn_Click(object sender, EventArgs e)
        {
            state_Textbox.Text = string.Empty;
            result_Textbox.Text = string.Empty;
            firstNumber = null;
            awaitingSecond = false;
        }

        private void pandm_btn_Click(object sender, EventArgs e)
        {

        }
    }
}
