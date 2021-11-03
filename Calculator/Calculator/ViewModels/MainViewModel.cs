using Calculator.Bases;
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

        public string[] Expression
        {
            get;
            set;
        }

        public Command OnClickItemCommand { get; }
        public Command OnClearMathematicalSentenceCommand { get; }
        public Command OnCalculateAnswerCommand { get; }
        public Command OnClickMemoryFeaturesCommand { get; }

        public MainViewModel()
        {
            OnClickItemCommand = new Command<string>((value) => { OnClickItem(value); });
            OnClickMemoryFeaturesCommand = new Command<string>((feature) => { OnClickMemoryFeatures(feature); });
            OnClearMathematicalSentenceCommand = new Command(OnClearMathematicalSentence);
            OnCalculateAnswerCommand = new Command(OnCalculateAnswer);
        }

        public string MathematicalSentence
        {
            get {
                return mathematicalSentence;
            }
            set
            {
                mathematicalSentence = value;
                OnPropertyChanged("MathematicalSentence");
            }
        }

        private void OnClickItem(string value)
        {
            if (MathematicalSentence == "0")
                MathematicalSentence = "";
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
            MathematicalSentence = feature;
        }

        private bool IsOperand(string value)
        {
            string operands = "+-/*";
            return operands.Contains(value);
        }

    }
}
