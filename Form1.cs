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
            // Ensure handlers are not added twice (designer may already wire events)
            plus_btn.Click -= PlusButton_Click;
            plus_btn.Click += PlusButton_Click;
            minus_btn.Click -= MinusButton_Click;
            minus_btn.Click += MinusButton_Click;
            mul_btn.Click -= MulButton_Click;
            mul_btn.Click += MulButton_Click;
            div_btn.Click -= DivButton_Click;
            div_btn.Click += DivButton_Click;
            result_btn.Click -= ResultButton_Click;
            result_btn.Click += ResultButton_Click;

            // C / CE / Del 버튼 이벤트 연결
            C_btn.Click -= C_btn_Click;             // 전체 초기화
            C_btn.Click += C_btn_Click;
            CE_btn.Click -= CE_btn_Click;           // 마지막 피연산자 삭제
            CE_btn.Click += CE_btn_Click;
            del_btn.Click -= DeleteButton_Click;    // 문자(한 글자) 삭제 (Del)
            del_btn.Click += DeleteButton_Click;

            // 소수점 버튼 연결 (디자이너에서 decimal_btn로 선언됨)
            decimal_btn.Click += DotButton_Click;
            // Parentheses buttons (designer created openParen_btn, closeParen_btn)
            openParen_btn.Click -= OpenParenButton_Click;
            openParen_btn.Click += OpenParenButton_Click;
            closeParen_btn.Click -= CloseParenButton_Click;
            closeParen_btn.Click += CloseParenButton_Click;
            // pandm 버튼은 디자이너에서 이미 pandm_btn_Click으로 연결되어 있음
        }

        private void calc_Form_Load(object? sender, EventArgs e)
        {

        }

        // '(' 버튼 핸들러
        private void OpenParenButton_Click(object? sender, EventArgs e)
        {
            var text = state_Textbox.Text ?? string.Empty;

            // 완료된 식이면 새로 시작
            if (text.Contains(" = "))
            {
                state_Textbox.Text = string.Empty;
                result_Textbox.Text = string.Empty;
            }

            // Cannot place '(' directly after a number or a closing parenthesis
            if (!string.IsNullOrEmpty(text))
            {
                char last = text.TrimEnd()[^1];
                if (char.IsDigit(last) || last == ')')
                {
                    // ignore (to avoid implicit multiplication)
                    return;
                }
            }

            // add a space after '(' to separate tokens reliably
            state_Textbox.Text += "( ";
            awaitingSecond = false;
        }

        // ')' 버튼 핸들러
        private void CloseParenButton_Click(object? sender, EventArgs e)
        {
            var text = state_Textbox.Text ?? string.Empty;

            // If result is shown, start new
            if (text.Contains(" = "))
            {
                state_Textbox.Text = string.Empty;
                result_Textbox.Text = string.Empty;
                return;
            }

            // Only add ')' if there is an unmatched '('
            int open = 0, close = 0;
            foreach (var ch in text)
            {
                if (ch == '(') open++;
                if (ch == ')') close++;
            }
            if (open <= close)
                return;

            // Do not close if last token is an operator or '(' or empty
            if (string.IsNullOrEmpty(text)) return;
            char lastChar = text.TrimEnd()[^1];
            if (lastChar == '(' || lastChar == '+' || lastChar == '-' || lastChar == 'x' || lastChar == 'X' || lastChar == '*' || lastChar == '/' || lastChar == '÷' || lastChar == ' ') return;

            // remove trailing spaces before closing
            state_Textbox.Text = state_Textbox.Text.TrimEnd();
            state_Textbox.Text += ")";
            awaitingSecond = false;
        }

        // 숫자 버튼들의 Click 이벤트 핸들러를 추가합니다.
        private void NumberButton_Click(object? sender, EventArgs e)
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
        private void PlusButton_Click(object? sender, EventArgs e) => PrepareOperator("+");
        // '-' 버튼 핸들러
        private void MinusButton_Click(object? sender, EventArgs e) => PrepareOperator("-");
        // 'x' 버튼 핸들러
        private void MulButton_Click(object? sender, EventArgs e) => PrepareOperator("x");
        // '÷' 또는 '/' 버튼 핸들러
        private void DivButton_Click(object? sender, EventArgs e) => PrepareOperator("÷");

        // 공통: 연산자 준비 및 체인 연산 처리
        private void PrepareOperator(string opSymbol)
        {
            // 이미 결과가 표시된 경우 결과를 좌항으로 사용
            if (state_Textbox.Text.Contains(" = "))
            {
                if (double.TryParse(result_Textbox.Text, out double res))
                {
                    state_Textbox.Text = FormatNumber(res);
                    result_Textbox.Text = string.Empty;
                }
            }       

            var text = state_Textbox.Text ?? string.Empty;
            var trimmed = text.TrimEnd();

            // 빈 상태에서 연산자 입력은 무시
            if (string.IsNullOrEmpty(trimmed))
                return;

            char lastChar = trimmed[^1];

            // If last char is '(' then don't allow operator
            if (lastChar == '(')
                return;

            // If last char is digit or decimal point or closing paren, append operator
            if (char.IsDigit(lastChar) || lastChar == '.' || lastChar == ')')
            {
                state_Textbox.Text = trimmed + " " + opSymbol + " ";
                awaitingSecond = true;
                return;
            }

            // Otherwise treat as operator replacement: replace last operator with new one
            // Find last non-space before operator (we assume operators were written with spaces around)
            int lastSpace = trimmed.LastIndexOf(' ');
            if (lastSpace >= 0)
            {
                state_Textbox.Text = trimmed.Substring(0, lastSpace + 1) + opSymbol + " ";
            }
            else
            {
                state_Textbox.Text = opSymbol + " ";
            }
            awaitingSecond = true;
        }

        // '=' 버튼 핸들러: "a op b" 계산 후 state에 " = 결과" 추가, result_Textbox에 결과만 표시
        private void ResultButton_Click(object? sender, EventArgs e)
        {
            // 이미 계산된 상태면 무시
            if (state_Textbox.Text.Contains(" = "))
                return;
            var text = state_Textbox.Text ?? string.Empty;
            text = text.TrimEnd();
            if (string.IsNullOrEmpty(text))
                return;

            // Tokenize by spaces for quick checks (but EvaluateExpression does full tokenize)
            var tokens = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length == 0)
                return;

            bool IsOperatorToken(string t) => t == "+" || t == "-" || t == "x" || t == "X" || t == "*" || t == "/" || t == "÷";

            // If expression ends with an operator (not ')' or number), remove the trailing operator
            var lastToken = tokens[^1];
            if (IsOperatorToken(lastToken))
            {
                int lastSpace = text.LastIndexOf(' ');
                if (lastSpace >= 0)
                {
                    text = text.Substring(0, lastSpace).TrimEnd();
                    tokens = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                }
            }

            if (tokens.Length == 0)
                return;

           
            int openCount = 0, closeCount = 0;
            foreach (var ch in text)
            {
                if (ch == '(') openCount++;
                if (ch == ')') closeCount++;
            }
            if (openCount > closeCount)
            {
             
                return;
            }

            double res = EvaluateExpression(text);
            result_Textbox.Text = FormatNumber(res);
            state_Textbox.Text = text + " = " + FormatNumber(res);
            firstNumber = null;
            awaitingSecond = false;
        }

        private double EvaluateExpression(string expr)
        {
     
            var tokens = new System.Collections.Generic.List<string>();
            string s = expr ?? string.Empty;
            int i = 0;
            string prev = null;
            while (i < s.Length)
            {
                if (char.IsWhiteSpace(s[i])) { i++; continue; }
                char c = s[i];
                if (c == '(' || c == ')')
                {
                    tokens.Add(c.ToString()); prev = c.ToString(); i++; continue;
                }
                if (c == '+' || c == '*' || c == '/' || c == 'x' || c == 'X' || c == '÷')
                {
                    tokens.Add(c.ToString()); prev = c.ToString(); i++; continue;
                }
                if (c == '-')
                {
                    bool unary = prev == null || prev == "(" || prev == "+" || prev == "-" || prev == "x" || prev == "X" || prev == "*" || prev == "/" || prev == "÷";
                    if (unary && i + 1 < s.Length && (char.IsDigit(s[i + 1]) || s[i + 1] == '.'))
                    {
                        int j = i + 1; while (j < s.Length && (char.IsDigit(s[j]) || s[j] == '.')) j++;
                        tokens.Add(s.Substring(i, j - i)); prev = tokens[^1]; i = j; continue;
                    }
                    tokens.Add("-"); prev = "-"; i++; continue;
                }
                if (char.IsDigit(c) || c == '.')
                {
                    int j = i; while (j < s.Length && (char.IsDigit(s[j]) || s[j] == '.')) j++;
                    tokens.Add(s.Substring(i, j - i)); prev = tokens[^1]; i = j; continue;
                }
                // unknown, skip
                i++;
            }

            var output = new System.Collections.Generic.List<string>();
            var ops = new System.Collections.Generic.Stack<string>();
            bool IsOp(string t)
            {
                if (string.IsNullOrEmpty(t)) return false;
                char c = t[0];
                return c == '+' || c == '-' || c == 'x' || c == 'X' || c == '*' || c == '/' || c == '÷' || c == '·';
            }
            int Prec(string t)
            {
                if (string.IsNullOrEmpty(t)) return 0;
                // treat tokens like "*(" (i.e., '*' followed by '(' marker) as higher precedence
                if (t.Length > 1 && t[^1] == '(')
                {
                    char baseOp = t[0];
                    if (baseOp == 'x' || baseOp == 'X' || baseOp == '*' || baseOp == '·') return 3;
                }
                char op = t[0];
                if (op == '+' || op == '-') return 1;
                if (op == 'x' || op == 'X' || op == '*' || op == '·' || op == '/' || op == '÷') return 2;
                return 0;
            }

            for (int idx = 0; idx < tokens.Count; idx++)
            {
                var tok = tokens[idx];
                if (double.TryParse(tok, out _))
                {
                    output.Add(tok);
                    continue;
                }
                if (IsOp(tok))
                {
                    // if this is a multiplication-like op and next token is '(', treat it as higher-precedence by marking
                    string opToken = tok;
                    if ((tok == "x" || tok == "X" || tok == "*" || tok == "·") && idx + 1 < tokens.Count && tokens[idx + 1] == "(")
                        opToken = tok + "("; // mark special

                    while (ops.Count > 0 && IsOp(ops.Peek()) && Prec(ops.Peek()) >= Prec(opToken))
                    {
                        var popped = ops.Pop();
                        if (popped.Length > 1 && popped[^1] == '(') popped = popped.Substring(0, popped.Length - 1);
                        output.Add(popped);
                    }
                    ops.Push(opToken);
                }
                else if (tok == "(") ops.Push(tok);
                else if (tok == ")")
                {
                    while (ops.Count > 0 && ops.Peek() != "(")
                    {
                        var popped = ops.Pop();
                        if (popped.Length > 1 && popped[^1] == '(') popped = popped.Substring(0, popped.Length - 1);
                        output.Add(popped);
                    }
                    if (ops.Count > 0 && ops.Peek() == "(") ops.Pop();
                }
            }
            while (ops.Count > 0)
            {
                var o = ops.Pop();
                if (o == "(" || o == ")") continue;
                if (o.Length > 1 && o[^1] == '(') o = o.Substring(0, o.Length - 1);
                output.Add(o);
            }

            var st = new System.Collections.Generic.Stack<double>();
            foreach (var t in output)
            {
                if (double.TryParse(t, out double v)) st.Push(v);
                else if (IsOp(t)) { if (st.Count < 2) continue; double r = st.Pop(); double l = st.Pop(); st.Push(Compute(l, r, t)); }
            }
            return st.Count > 0 ? st.Pop() : 0.0;
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
        private void C_btn_Click(object? sender, EventArgs e)
        {
            state_Textbox.Text = string.Empty;
            result_Textbox.Text = string.Empty;
            firstNumber = null;
            awaitingSecond = false;
        }

        // CE 버튼: 마지막으로 입력된 피연산자(토큰)를 통째로 삭제
        private void CE_btn_Click(object? sender, EventArgs e)
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
        private void DeleteButton_Click(object? sender, EventArgs e)
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
        private void DotButton_Click(object? sender, EventArgs e)
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
        private void pandm_btn_Click(object? sender, EventArgs e)
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
