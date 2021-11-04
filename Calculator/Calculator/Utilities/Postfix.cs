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
            List<string> postfixEpression = ConvertInfixToPostfix(Expression);
            return EvaluatePostfix(postfixEpression);
        }

        private string EvaluatePostfix(List<string> postfix)
        {
            float a, b, ans;
            string answer;
            Stack<string> stack = new Stack<string>();

            for (int i = 0; i < postfix.Count; i++)
            {
                string c = postfix[i];
                if (c.Equals("*"))
                {
                    string sa = stack.Pop();
                    string sb = stack.Pop();
                    a = float.Parse(sb);
                    b = float.Parse(sa);
                    ans = a * b;
                    stack.Push(ans.ToString());

                }
                else if (c.Equals("/"))
                {
                    string sa = stack.Pop();
                    string sb = stack.Pop();
                    a = float.Parse(sb);
                    b = float.Parse(sa);
                    ans = a / b;
                    stack.Push(ans.ToString());
                }
                else if (c.Equals("+"))
                {
                    string sa = stack.Pop();
                    string sb = stack.Pop();
                    a = float.Parse(sb);
                    b = float.Parse(sa);
                    ans = a + b;
                    stack.Push(ans.ToString());

                }
                else if (c.Equals("-"))
                {
                    string sa = stack.Pop();
                    string sb = stack.Pop();
                    a = float.Parse(sb);
                    b = float.Parse(sa);
                    ans = a - b;
                    stack.Push(ans.ToString());
                }
                else
                {
                    stack.Push(postfix[i]);
                }
            }
            answer = stack.Pop().ToString();
            return answer;
        }

        private List<string> ConvertInfixToPostfix(string expression)
        {
            Stack<string> stack = new Stack<string>();
            List<string> postfix = new List<string>();
            for (int i = 0; i < expression.Length; ++i)
            {
                char c = expression[i];
                if (char.IsLetterOrDigit(c) || (i == 0 && c == '-'))
                {
                    string number = c.ToString();
                    if (i + 1 < expression.Length)
                    {
                        while (char.IsLetterOrDigit(expression[i + 1]) || expression[i + 1] == '.')
                        {
                            number += expression[i + 1].ToString();
                            i++;
                            if (i + 1 == expression.Length )
                                break;
                        }
                    }
                    postfix.Add(number);
                }
                else
                {
                    while (stack.Count > 0 && GetOperandPrecedence(c.ToString()) <= GetOperandPrecedence(stack.Peek()))
                        postfix.Add(stack.Pop());
                    stack.Push(c.ToString());
                }

            }
            while (stack.Count > 0)
                postfix.Add(stack.Pop());
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
