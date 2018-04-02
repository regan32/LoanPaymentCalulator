
namespace LoanPaymentCalculator
{
    using System;
    using Guards;

    public class Calculator
    {
        public static Payment Calulate(double amount, double interest, double downPayment, int term)
        {
            Guard.ArgumentIsGreaterThan(() => amount, 0);
            Guard.ArgumentIsGreaterOrEqual(() => downPayment, 0);
            Guard.ArgumentIsGreaterOrEqual(() => interest, 0);
            Guard.ArgumentIsGreaterOrEqual(() => term, 1);

            //Guard.ArgumentIsGreaterOrEqual(() => amount, downPayment);
            if (downPayment > amount)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), amount, $"{nameof(amount)} must be greater or equal {nameof(downPayment)}");
            }

            double body = amount - downPayment;
            double monthlyInterest = interest / 12f;
            double monthlyPaiment;
            if (Math.Abs(monthlyInterest) < Double.Epsilon)
            {
                monthlyPaiment = body / term;
            }
            else
            {
                monthlyPaiment = body * monthlyInterest / (1 - Math.Pow(1 + monthlyInterest, -term));
            }

            var totalPayment = monthlyPaiment * term;

            return new Payment()
            {
                MonthlyPaiment = Math.Round(monthlyPaiment, 2 ),
                TotalPayment = Math.Round(totalPayment, 2),
                TotalInterest = Math.Round(totalPayment - body, 2)
            };
        }
    }
}
