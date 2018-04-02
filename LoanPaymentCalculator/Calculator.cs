
namespace LoanPaymentCalculator
{
    using System;
    using Guards;

    public class Calculator
    {
        private const int Round = 2;
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
            
            double monthlyPaiment;
            if (interest > 0)
            {
                double monthlyInterest = interest / 12f;
                monthlyPaiment = body * monthlyInterest / (1 - Math.Pow(1 + monthlyInterest, -term));
            }
            else
            {
                monthlyPaiment = body / term;
            }

            var totalPayment = monthlyPaiment * term;

            return new Payment()
            {
                MonthlyPaiment = Math.Round(monthlyPaiment, Round),
                TotalPayment = Math.Round(totalPayment, Round),
                TotalInterest = Math.Round(totalPayment - body, Round)
            };
        }
    }
}
