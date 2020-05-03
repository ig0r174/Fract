using System;

namespace ExamN1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите выражение");
            var expression = Console.ReadLine();
            
            var parser = new Parser(expression);
            var result = parser.operands[0];
            parser.operands.RemoveAt(0);

            var actionId = 0;
            foreach (var fraction in parser.operands)
            {
                result = parser.GetActionBySymbol(result, fraction, parser.actions[actionId]);
                actionId++;
            }
            
            Console.WriteLine(result.ToString());
        }
    }
}