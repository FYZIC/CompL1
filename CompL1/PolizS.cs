using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompL1
{
    internal class PolizS
    {
        public string PolizString { get; set; }
        public double Result { get; set; }

        public static double EvaluatePostfix(string postfix)
        {
            Stack<double> operandStack = new Stack<double>();
            foreach (string token in postfix.Split(' '))
            {
                if (double.TryParse(token, out double operand))
                {
                    operandStack.Push(operand);
                }
                else if (IsOperator(token))
                {
                    double operand2 = operandStack.Pop();
                    double operand1 = operandStack.Pop();
                    double result = ApplyOperator(token, operand1, operand2);
                    operandStack.Push(result);
                }
            }
            return operandStack.Pop();
        }

        static bool IsOperator(string token)
        {
            return token == "+" || token == "-" || token == "*" || token == "/" || token == "^";
        }

        static double ApplyOperator(string op, double operand1, double operand2)
        {
            switch (op)
            {
                case "+":
                    return operand1 + operand2;
                case "-":
                    return operand1 - operand2;
                case "*":
                    return operand1 * operand2;
                case "/":
                    if (operand2 == 0)
                    {
                        throw new DivideByZeroException("деление на ноль");
                    }
                    return operand1 / operand2;
                case "^":
                    return Math.Pow(operand1, operand2);
                default:
                    throw new ArgumentException("Недопустимая операция: " + op);
            }
        }
    }
}
