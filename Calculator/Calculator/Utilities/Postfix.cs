using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Utilities
{ 
    class Postfix
    {
        public string Expression
        {
            get;
            set;
        }

        public Postfix(string expression)
        {
            Expression = expression;
        }

        public string GetAnswer()
        {
            string postfixEpression = ConvertInfixToPostfix(Expression);
            return EvaluatePostfix(postfixEpression);
        }

        private string EvaluatePostfix(string postfix)
        {
            float a, b, ans;
            string answer;
            Stack<string> stack = new Stack<string>();

            for (int j = 0; j < postfix.Length; j++)
            {
                string c = postfix.Substring(j, 1);
                if (c.Equals("*"))
                {
                    string sa = stack.Pop().ToString();
                    string sb = stack.Pop().ToString();
                    a = float.Parse(sb);
                    b = float.Parse(sa);
                    ans = a * b;
                    stack.Push(ans.ToString());

                }
                else if (c.Equals("/"))
                {
                    string sa = stack.Pop().ToString();
                    string sb = stack.Pop().ToString();
                    a = float.Parse(sb);
                    b = float.Parse(sa);
                    ans = a / b;
                    stack.Push(ans.ToString());
                }
                else if (c.Equals("+"))
                {
                    string sa = stack.Pop().ToString();
                    string sb = stack.Pop().ToString();
                    a = float.Parse(sb);
                    b = float.Parse(sa);
                    ans = a + b;
                    stack.Push(ans.ToString());

                }
                else if (c.Equals("-"))
                {
                    string sa = stack.Pop().ToString();
                    string sb = stack.Pop().ToString();
                    a = float.Parse(sb);
                    b = float.Parse(sa);
                    ans = a - b;
                    stack.Push(ans.ToString());

                }
                else
                {
                    stack.Push(postfix.Substring(j, 1));
                }
            }
            answer = stack.Pop().ToString();
            return answer;
        }

        private string ConvertInfixToPostfix(string exp)
        {
            Stack<string> stack = new Stack<string>();
            string postfix = "";

            for (int i = 0; i < exp.Length; ++i)
            {
                char c = exp[i];
                if (char.IsLetterOrDigit(c))
                    postfix += c;
                else
                {
                    while (stack.Count > 0 && GetOperandPrecedence(c.ToString()) <= GetOperandPrecedence(stack.Peek()))
                        postfix += stack.Pop();
                    stack.Push(c.ToString());
                }

            }
            while (stack.Count > 0)
                postfix += stack.Pop();
            return postfix;
        }

        private int GetOperandPrecedence(string operand)
        {
            switch (operand)
            {
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                    return 2;
            }
            return -1;
        }
}
}
