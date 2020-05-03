using System;
using System.Collections.Generic;
using System.Xml.Schema;

namespace ExamN1
{
    public class Fraction
    {
        private int intPart;
        private int numerator;
        private int denominator;
        private Fraction incorrectFraction;

        public Fraction(int intPart, int numerator, int denominator)
        {
            if( numerator > 0 && denominator == 0 ) throw new Exception("Знаменатель не может быть нулем");

            this.intPart = intPart;
            this.numerator = numerator;
            this.denominator = denominator;
            incorrectFraction = GetIncorrectFraction(this);
        }
        
        private static int GetGreatestCommonDivisor(int a, int b)
        {
            while (b != 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
        
        private static int GetLeastCommonMultiple(int a, int b)
        {
            return a * b / GetGreatestCommonDivisor(a, b);
        }
        
        private int[] GetMultipliers(int firstDenominator, int secondDenominator)
        {
            var leastCommonMultiple = GetLeastCommonMultiple(firstDenominator, secondDenominator); 
            return new [] { leastCommonMultiple / firstDenominator, leastCommonMultiple / secondDenominator };
        }

        public Fraction Add(Fraction b)
        {
            var multipliers = GetMultipliers(denominator, b.denominator);
            var firstIncorrect = incorrectFraction;
            var secondIncorrect = b.incorrectFraction;
            var newNumerator = firstIncorrect.numerator * multipliers[0] + secondIncorrect.numerator * multipliers[1];
            
            return new Fraction(0, newNumerator, denominator * multipliers[0]).ToProperFraction();
        }

        public Fraction Minus(Fraction b)
        {
            var multipliers = GetMultipliers(denominator, b.denominator);
            var firstIncorrect = incorrectFraction;
            var secondIncorrect = b.incorrectFraction;
            var newNumerator = firstIncorrect.numerator * multipliers[0] - secondIncorrect.numerator * multipliers[1];
            
            return new Fraction(0, newNumerator, denominator * multipliers[0]).ToProperFraction();
        }

        public Fraction Multiply(Fraction b)
        {
            var firstIncorrect = incorrectFraction;
            var secondIncorrect = b.incorrectFraction;
            var newDenominator = denominator * b.denominator;
            
            return new Fraction(0, firstIncorrect.numerator * secondIncorrect.numerator, newDenominator).ToProperFraction();
        }
        
        public Fraction Divide(Fraction b)
        {
            var firstIncorrect = incorrectFraction;
            var secondIncorrect = b.incorrectFraction;
            var newNumerator = firstIncorrect.numerator * b.denominator;
            var newDenominator = denominator * secondIncorrect.numerator;
            
            return new Fraction(0, newNumerator, newDenominator).ToProperFraction();
        }

        private Fraction GetIncorrectFraction(Fraction fraction)
        {
            return fraction.intPart == 0 ? fraction : new Fraction(0, fraction.intPart * fraction.denominator + fraction.numerator, fraction.denominator);
        }

        public override string ToString()
        {
            return (intPart > 0 ? intPart + " " : "") + (numerator > 0 ? numerator + "/" + denominator : "");
        }
        
        private Fraction ToProperFraction()
        {
            if (numerator > denominator)
            {
                intPart += numerator / denominator;
                numerator %= denominator;
            }

            var greatestDivisor = GetGreatestCommonDivisor(numerator, denominator);
            if (greatestDivisor == 1) return new Fraction(intPart, numerator, denominator);
            
            numerator /= greatestDivisor;
            denominator /= greatestDivisor;
            return new Fraction(intPart, numerator, denominator);
        }
    }
}