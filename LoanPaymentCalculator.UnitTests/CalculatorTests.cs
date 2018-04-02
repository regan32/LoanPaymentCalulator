namespace LoanPaymentCalculator.UnitTests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class CalculatorTests
    {
        [TestCase(-1, 1, 1, 1)] // Negative amount
        [TestCase(0, 1, 1, 1)] // Zero amount
        [TestCase(1, 1, 2, 1)] // DownPayment is greater then amount
        [TestCase(1, -1, 1, 1)] // Negative interest
        [TestCase(1, 1, -1, 1)] // Negative downPayment
        [TestCase(1, 1, 1, 0)] // Zero term
        public void InvalidArguments_ShouldThrowArgumentOutOfRangeException(double amount, double interest, double downPayment, int term)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Calculator.Calulate(amount, interest, downPayment, term));
        }


        [TestCase(1000, 0.2, 0, 12, 92.63)] //No downpayment
        [TestCase(100000, 0.055, 20000, 360, 454.23)] // with downpayment
        [TestCase(100000, 0.055, 100000, 360, 0)]// downpayment is equal to amount :)
        [TestCase(100000, 0, 0, 10, 10000)]// zero interest
        [TestCase(100000, 0.1, 0, 1, 100833.33)]// Minimal term
        public void CalculateMonthlyPayment_ShouldBeOk(double amount, double interest, double downPayment, int term, double expected)
        {
            var payment = Calculator.Calulate(amount, interest, downPayment, term);
            Assert.AreEqual(expected, payment.MonthlyPaiment);
        }
    }
}
