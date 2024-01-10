using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calculator
{
    public enum Operation
    {
        None,
        Add,
        Subtract,
        Multiply,
        Divide,
        Square,
        SquareRoot,
        Inverse
    }

    public partial class MainPage : ContentPage
    {
        private double a = 0;
        private double b = 0;
        private double result = 0;
        private Operation operation = Operation.None;
        private bool comma = false;
        private bool equalsClicked = false;

        public MainPage()
        {
            InitializeComponent();
        }

        private void NumberClick(object sender, EventArgs e)
        {
            try
            {
                if (operation == Operation.None)
                {
                    if (comma && !a.ToString().Contains("."))
                        a = double.Parse(a.ToString() + "." + ((Button)sender).Text);
                    else
                        a = double.Parse(a.ToString() + ((Button)sender).Text);
                    comma = false;
                }
                else
                {
                    if (comma && !b.ToString().Contains("."))
                        b = double.Parse(b.ToString() + "." + ((Button)sender).Text);
                    else
                        b = double.Parse(b.ToString() + ((Button)sender).Text);
                    comma = false;
                }
                Print();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void OperationClick(object sender, EventArgs e)
        {
            comma = false;
            if (operation != Operation.None)
                return;
            switch (((Button)sender).Text)
            {
                case "+":
                    operation = Operation.Add;
                    break;
                case "-":
                    operation = Operation.Subtract;
                    break;
                case "x":
                    operation = Operation.Multiply;
                    break;
                case "/":
                    operation = Operation.Divide;
                    break;
                case "x^2":
                    operation = Operation.Square;
                    result = a * a;
                    break;
                case "sqrt":
                    operation = Operation.SquareRoot;
                    result = Math.Sqrt(a);
                    break;
                case "1/x":
                    operation = Operation.Inverse;
                    result = 1 / a;
                    break;
            }
            Print();
        }

        private void EqualsClick(object sender, EventArgs e)
        {
            if (operation == Operation.None)
                return;
            switch (operation)
            {
                case Operation.Add:
                    result = a + b;
                    break;
                case Operation.Subtract:
                    result = a - b;
                    break;
                case Operation.Multiply:
                    result = a * b;
                    break;
                case Operation.Divide:
                    result = a / b;
                    break;
            }
            equalsClicked = true;
            Print();
            equalsClicked = false;
            a = result;
            b = 0;
            result = 0;
            comma = false;
            operation = Operation.None;
        }

        private void Print()
        {
            if (operation == Operation.None)
            {
                Output_Label.Text = a.ToString();
                OutputEquation_Label.Text = "";
                return;
            }
            switch (operation)
            {
                case Operation.Add:
                case Operation.Subtract:
                case Operation.Multiply:
                case Operation.Divide:
                    if (equalsClicked)
                    {
                        OutputEquation_Label.Text = a.ToString() + " " + OperationToSign(operation) + " " + b.ToString() + " =";
                        Output_Label.Text = result.ToString();
                    }
                    else
                    {
                        OutputEquation_Label.Text = a.ToString() + " " + OperationToSign(operation);
                        Output_Label.Text = b.ToString();
                    }
                    return;
                case Operation.Square:
                    OutputEquation_Label.Text = "sqr(" + a.ToString() + ")";
                    break;
                case Operation.SquareRoot:
                    OutputEquation_Label.Text = "sqrt(" + a.ToString() + ")";
                    break;
                case Operation.Inverse:
                    OutputEquation_Label.Text = "1/" + a.ToString();
                    break;
            }
            Output_Label.Text = result.ToString();
            a = result;
            b = 0;
            result = 0;
            comma = false;
            operation = Operation.None;
        }

        private string OperationToSign(Operation operation)
        {
            switch (operation)
            {
                case Operation.Add:
                    return "+";
                case Operation.Subtract:
                    return "-";
                case Operation.Multiply:
                    return "x";
                case Operation.Divide:
                    return "/";
            }
            return "";
        }

        private void ClearClick(object sender, EventArgs e)
        {
            a = 0;
            b = 0;
            result = 0;
            operation = Operation.None;
            comma = false;
            equalsClicked = false;
            Print();
        }

        private void CommaClick(object sender, EventArgs e)
        {
            comma = true;
        }
    }
}
