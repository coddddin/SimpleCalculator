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

            // C / CE / Del 버튼 이벤트 연결
            C_btn.Click += C_btn_Click;             // 전체 초기화
            CE_btn.Click += CE_btn_Click;           // 마지막 피연산자 삭제
            del_btn.Click += DeleteButton_Click;    // 문자(한 글자) 삭제 (Del)

            // 소수점 버튼 연결 (디자이너에서 decimal_btn로 선언됨)
            decimal_btn.Click += DotButton_Click;
            // pandm 버튼은 디자이너에서 이미 pandm_btn_Click으로 연결되어 있음
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
        private void PlusButton_Click(object sender, EventArgs e) => PrepareOperator("+");
        // '-' 버튼 핸들러
        private void MinusButton_Click(object sender, EventArgs e) => PrepareOperator("-");
        // 'x' 버튼 핸들러
        private void MulButton_Click(object sender, EventArgs e) => PrepareOperator("x");
        // '÷' 또는 '/' 버튼 핸들러
        private void DivButton_Click(object sender, EventArgs e) => PrepareOperator("÷");

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

        // CE 버튼: 마지막으로 입력된 피연산자(토큰)를 통째로 삭제
        private void CE_btn_Click(object sender, EventArgs e)
        {
            var text = state_Textbox.Text ?? string.Empty;

            // 완료된 식이면 전체 초기화 (간단 처리)
            if (text.Contains(" = "))
            {
                state_Textbox.Text = string.Empty;
                result_Textbox.Text = string.Empty;
                firstNumber = null;
                awaitingSecond = false;
                return;
            }

            var tokens = text.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length == 0)
            {
                // nothing
                return;
            }

            if (tokens.Length == 1)
            {
                // 유일한 토큰(피연산자) 전체 삭제
                state_Textbox.Text = string.Empty;
                awaitingSecond = false;
                return;
            }

            // 마지막 토큰이 숫자이면 그 숫자를 제거 (피연산자 통째로 삭제 -> 남은 문자열 끝은 연산자 + 공백)
            if (double.TryParse(tokens[^1], out _))
            {
                int lastSpace = text.LastIndexOf(' ');
                if (lastSpace >= 0)
                {
                    // Keep up to the space after operator (so "1 + 234" -> "1 + ")
                    state_Textbox.Text = text.Substring(0, lastSpace + 1);
                    // 상태: 연산자 뒤 대기 중
                    awaitingSecond = true;
                }
                else
                {
                    state_Textbox.Text = string.Empty;
                    awaitingSecond = false;
                }
            }
            else
            {
                // 마지막 토큰이 숫자가 아니면(예: 연산자만 남아있는 경우) 연산자 제거
                int lastSpace = text.LastIndexOf(' ');
                if (lastSpace >= 0)
                    state_Textbox.Text = text.Substring(0, lastSpace).TrimEnd();
            }
        }

        // Del 버튼: 마지막 입력된 글자(숫자 한 자리) 삭제
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var text = state_Textbox.Text ?? string.Empty;

            if (text.Contains(" = "))
            {
                // 결과가 있는 상태에서는 아무 동작하지 않음
                return;
            }

            if (string.IsNullOrEmpty(text))
                return;

            // 마지막 문자가 숫자(또는 소수점)일 때만 한 글자 삭제
            char last = text[^1];
            if (char.IsDigit(last) || last == '.')
            {
                state_Textbox.Text = text.Substring(0, text.Length - 1);
                // 만약 삭제로 인해 피연산자 부분이 완전히 없어지면 awaitingSecond 상태는 유지(연산자 뒤) 또는 복원
                var trimmed = state_Textbox.Text.TrimEnd();
                var tokens = trimmed.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length >= 2 && (tokens[^1] == "+" || tokens[^1] == "-" || tokens[^1] == "x" || tokens[^1] == "÷"))
                {
                    awaitingSecond = true;
                }
                else if (tokens.Length < 2)
                {
                    awaitingSecond = false;
                }
            }
            // else: 마지막 문자가 공백 또는 연산자일 경우 Del은 아무 동작 안 함
        }

        // 소수점 버튼: 현재 피연산자에 소수점 추가 (중복 허용하지 않음)
        private void DotButton_Click(object sender, EventArgs e)
        {
            var text = state_Textbox.Text ?? string.Empty;

            // 완료된 식이면 새로 시작
            if (text.Contains(" = "))
            {
                state_Textbox.Text = string.Empty;
                result_Textbox.Text = string.Empty;
                firstNumber = null;
                awaitingSecond = false;
            }

            // 마지막 토큰(피연산자) 획득
            var tokens = state_Textbox.Text.TrimEnd().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string lastToken = tokens.Length > 0 ? tokens[^1] : string.Empty;

            // 연산자 뒤에서 새 피연산자 시작하려는 경우 (state ends with operator + space)
            if (state_Textbox.Text.EndsWith(" "))
            {
                state_Textbox.Text += "0.";
                awaitingSecond = false;
                return;
            }

            // 빈 상태: "0."
            if (string.IsNullOrEmpty(lastToken))
            {
                state_Textbox.Text = "0.";
                return;
            }

            // 마지막 토큰이 숫자(또는 숫자+소수점)이고 이미 '.' 포함되어 있지 않으면 추가
            if (double.TryParse(lastToken, out _) || lastToken.Contains("."))
            {
                if (!lastToken.Contains("."))
                {
                    state_Textbox.Text += ".";
                }
                // 이미 소수점이 있으면 아무 동작 안 함
                return;
            }

            // 그 외의 경우(예: 마지막 토큰이 연산자) 새 피연산자로 "0."
            state_Textbox.Text += "0.";
        }

        // +/- 버튼: 현재 피연산자(혹은 결과)의 부호 토글
        private void pandm_btn_Click(object sender, EventArgs e)
        {
            var text = state_Textbox.Text ?? string.Empty;

            // 완료된 식이면 결과값의 부호 토글 (결과를 좌항으로 사용)
            if (text.Contains(" = "))
            {
                if (double.TryParse(result_Textbox.Text, out double r))
                {
                    double neg = -r;
                    result_Textbox.Text = FormatNumber(neg);
                    state_Textbox.Text = FormatNumber(neg);
                    firstNumber = neg;
                    awaitingSecond = false;
                }
                return;
            }

            var tokens = text.TrimEnd().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length == 0)
                return;

            string last = tokens[^1];

            // 마지막 토큰이 숫자(피연산자)면 그 값 부호 토글
            if (double.TryParse(last, out double val))
            {
                double neg = -val;
                tokens[^1] = FormatNumber(neg);
                state_Textbox.Text = string.Join(" ", tokens) + (text.EndsWith(" ") ? " " : "");
                // 상태 보정: 만약 연산자+space 뒤에 있던 피연산자였다면 awaitingSecond = false (입력중)
                if (tokens.Length >= 2 && (tokens[^2] == "+" || tokens[^2] == "-" || tokens[^2] == "x" || tokens[^2] == "÷"))
                    awaitingSecond = false;
                else
                    awaitingSecond = false;
            }
            else
            {
                // 마지막 토큰이 숫자가 아니면 무시
            }
        }
    }
}
