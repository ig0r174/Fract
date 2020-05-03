using System;
using System.Collections.Generic;

namespace ExamN1
{
    public class Parser
    {
        private readonly char[] operations = { '+', '-', '/', '*' };
        private string expression;
        public List<char> actions;
        public List<Fraction> operands;
        public Parser(string expression)
        {
            this.expression = expression;
            operands = ParseOperands();
            actions = ParseActions();
        }

        private List<Fraction> ParseOperands()
        {
            var withoutAction = expression;
            var result = new List<Fraction>();
            
            foreach (var operation in operations)
            {
                withoutAction = withoutAction.Replace(" " + operation + " ", ",");
            }

            foreach (var item in withoutAction.Split(','))
            {
                var split = item.Split(' ', '/');
                if( split.Length == 1 && item.IndexOf('/') == -1 )
                    result.Add(new Fraction(0, Convert.ToInt32(split[0]), 1));
                else if( split.Length == 2 && item.IndexOf(' ') == -1 )
                    result.Add(new Fraction(0, Convert.ToInt32(split[0]), Convert.ToInt32(split[1])));
                else 
                    result.Add(new Fraction(Convert.ToInt32(split[0]), Convert.ToInt32(split[1]), Convert.ToInt32(split[2])));
            }

            return result;
        }

        private List<char> ParseActions()
        {
            var result = new List<char>();
            foreach (var item in expression.Split(' '))
            {
                if ( char.TryParse(item, out _) && Array.IndexOf(operations, Convert.ToChar(item)) > -1)
                    result.Add(Convert.ToChar(item));
            }
            return result;
        }

        public Fraction GetActionBySymbol(Fraction fraction1, Fraction fraction2, char action)
        {
            return action switch
            {
                '+' => fraction1.Add(fraction2),
                '-' => fraction1.Minus(fraction2),
                '*' => fraction1.Multiply(fraction2),
                _ => fraction1.Divide(fraction2)
            };
        }
    }
}
