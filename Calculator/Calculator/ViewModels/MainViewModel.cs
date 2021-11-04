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
        private string memoryValue = "0";

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

        public string[] Expression
        {
            get;
            set;
        }

        public string MemoryValue
        {
            get
            {
                return memoryValue;
            }
            set
            {
                memoryValue = value;
            }
        }

        public Command OnClickDigitCommand { get; }
        public Command OnClearMathematicalSentenceCommand { get; }
        public Command OnCalculateAnswerCommand { get; }
        public Command OnClickMemoryFeaturesCommand { get; }

        public MainViewModel()
        {
            OnClickDigitCommand = new Command<string>((value) => { OnClickDigit(value); });
            OnClickMemoryFeaturesCommand = new Command<string>((feature) => { OnClickMemoryFeatures(feature); });
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
                    MemoryValue = "0";
                    break;
                case "MR":
                    MathematicalSentence = MemoryValue;
                    break;
                case "M+":
                    postfix = new Postfix(MathematicalSentence);
                    number = float.Parse(postfix.GetAnswer());
                    MemoryValue = (float.Parse(MemoryValue) + number).ToString();
                    break;
                case "M-":
                    postfix = new Postfix(MathematicalSentence);
                    number = float.Parse(postfix.GetAnswer());
                    MemoryValue = (float.Parse(MemoryValue) - number).ToString();
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
