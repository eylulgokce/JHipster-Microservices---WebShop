using MicroserviceCommon.CommonModel;

namespace PaymentService.Subscribers
{
    public interface IPaymentExchangeSubscriber
    {
        public void OnPaymentReceived(Payment payment);
    }
}
