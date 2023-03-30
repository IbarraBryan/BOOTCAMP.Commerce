namespace Api.Gateway.Models.Order.Commons
{
    public class Enums
    {
        public enum OrderStatus
        {
            Cancel,
            Pending,
            Approved
        }

        public enum OrderPayment
        {
            Cash,
            CreditCard,
            Paypal,
            BankTransfer
        }
    }
}
