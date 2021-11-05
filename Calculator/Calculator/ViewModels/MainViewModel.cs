using Calculator.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Calculator.ViewModels
{
    class MainViewModel : BaseModel
    {
        private string mathematicalSentence = "0";
        private Stack<string> memoryValue = new Stack<string>();

        public string MathematicalSentence
        {
            get
            {
                return mathematicalSentence;
            }
            set
            {
                mathematicalSentence = value;
                OnPropertyChanged("MathematicalSentence");
            }
        }

        public Command OnClickDigitCommand { get; private set; }
        public Command OnClearMathematicalSentenceCommand { get; private set; }
        public Command OnCalculateAnswerCommand { get; private set; }
        public Command OnClickMemoryFeaturesCommand { get; private set; }

        public MainViewModel()
        {
            // OnClickDigitCommand = new Command<string>(OnClickDigit);
            OnClickDigitCommand = new Command<string>(execute: (digit) => OnClickDigit(digit));
            OnClickMemoryFeaturesCommand = new Command<string>(OnClickMemoryFeatures);
            OnClearMathematicalSentenceCommand = new Command(OnClearMathematicalSentence);
            OnCalculateAnswerCommand = new Command(OnCalculateAnswer);
        }

        private void OnClickDigit(string value)
        {
            if (MathematicalSentence == "0")
            {
                MathematicalSentence = "";
                if (value == ".")
                {
                    MathematicalSentence = "0.";
                    return;
                } else if (value == "-")
                {
                    MathematicalSentence = "-";
                    return;
                }
            }
                
            if (IsOperand(value) && IsOperand(MathematicalSentence.Substring(MathematicalSentence.Length - 1)))
            {
                MathematicalSentence = MathematicalSentence.Remove(MathematicalSentence.Length - 1, 1);
                MathematicalSentence += value;
            }
            else
                MathematicalSentence += value;
        }

        private void OnClearMathematicalSentence()
        {
            MathematicalSentence = "0";
        }

        private void OnCalculateAnswer()
        {
            if (!IsOperand(MathematicalSentence.Substring(MathematicalSentence.Length - 1))) {
                Postfix postfix = new Postfix(MathematicalSentence);
                MathematicalSentence = postfix.GetAnswer();
            }
        }

        private void OnClickMemoryFeatures(string feature)
        {
            float number;
            Postfix postfix;
            switch(feature)
            {
                case "MC":
                    memoryValue.Clear();
                    break;
                case "MR":
                    if (memoryValue.Count != 0)
                        MathematicalSentence = memoryValue.Peek();
                    else
                        MathematicalSentence = "0";
                    break;
                case "M+":
                    postfix = new Postfix(MathematicalSentence);
                    number = float.Parse(postfix.GetAnswer());
                    memoryValue.Push(number.ToString());
                    break;
                case "M-":
                    memoryValue.Pop();
                    break;
                default:
                    break;
            }
        }

        private bool IsOperand(string value)
        {
            string operands = "+-/*";
            return operands.Contains(value);
        }

    }
}
