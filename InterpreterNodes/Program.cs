using System;
using System.Collections.Generic;

namespace InterpreterNodes
{

    public interface IExpression
    {
        int interpret();
    }

    public class NumericExpression : IExpression
    {
        public int _number;

        public NumericExpression(int number)
        {
            _number = number;
        }

        public NumericExpression(string number)
        {
            _number = int.Parse(number);
        }

        public int interpret()
        {
            return _number;
        }
    }

    public class AdditionExpression : IExpression
    {
        private IExpression _firstExpression, _secondExpression;

        public AdditionExpression(IExpression firstExpression, IExpression secondExpression)
        {
            _firstExpression = firstExpression;
            _secondExpression = secondExpression;
        }

        public int interpret()
        {
            return _firstExpression.interpret() + _secondExpression.interpret();
        }

        public override string ToString()
        {
            return "+";
        }
    }

    public class SubstractionExpression : IExpression
    {
        private IExpression _firstExpression, _secondExpression;

        public SubstractionExpression(IExpression firstExpression, IExpression secondExpression)
        {
            _firstExpression = firstExpression;
            _secondExpression = secondExpression;
        }

        public int interpret()
        {
            return _firstExpression.interpret() - _secondExpression.interpret();
        }

        public override string ToString()
        {
            return "-";
        }
    }

    public class MultiplicationExpression : IExpression
    {
        private IExpression _firstExpression, _secondExpression;

        public MultiplicationExpression(IExpression firstExpression, IExpression secondExpression)
        {
            _firstExpression = firstExpression;
            _secondExpression = secondExpression;
        }

        public int interpret()
        {
            return _firstExpression.interpret() * _secondExpression.interpret();
        }

        public override string ToString()
        {
            return "*";
        }
    }

    public class ExpressionParser
    {
        private static bool IsOperator(string input) => (input.Equals("+") || input.Equals("-") || input.Equals("*"));

        private static IExpression GetExpressionObject(IExpression firstExpression, IExpression secondExpression, string symbol)
        {
            if (symbol.Equals("+"))
                return new AdditionExpression(firstExpression, secondExpression);
            else if (symbol.Equals("-"))
                return new SubstractionExpression(firstExpression, secondExpression);
            else
                return new MultiplicationExpression(firstExpression, secondExpression);
        }

        Stack<IExpression> stack = new Stack<IExpression>();
        public int Parse(string input)
        {
            string[] tokenList = input.Split(' ');
            foreach( string symbol in tokenList)
            {
                if(!IsOperator(symbol))
                {
                    IExpression numberExpression = new NumericExpression(symbol);
                    stack.Push(numberExpression);
                    Console.WriteLine($"Agregando al stack: {numberExpression.interpret()}");
                }
                else if (IsOperator(symbol))
                {
                    IExpression firstExpression = stack.Pop();
                    IExpression secondExpression = stack.Pop();
                    Console.WriteLine($"Operadores para: {firstExpression.interpret()} {secondExpression.interpret()}");
                    IExpression expressionObject = GetExpressionObject(firstExpression, secondExpression, symbol);
                    Console.WriteLine($"Aplicando operador {expressionObject}");

                    NumericExpression resultExpression = new NumericExpression(expressionObject.interpret());
                    stack.Push(resultExpression);
                    Console.WriteLine($"Agregando resultado al stack {resultExpression.interpret()}");

                }
            }
            return stack.Pop().interpret();
        }

    }


    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("INTERPRETER NODES" + "\n");
            Console.WriteLine("Dado un lenguaje define una representacion para su gramatica junto con su interprete para su lenguaje." + "\n");

            string input = "2 1 5 + *";
            ExpressionParser expressionParser = new ExpressionParser();
            int result = expressionParser.Parse(input);

            Console.WriteLine($"Resultado final: {result}");

            Console.ReadLine();
        }
    }
}
