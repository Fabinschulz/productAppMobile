using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ProductAppMAUI.Utils
{
    internal class ProductMessenger : ValueChangedMessage<ProductMessage>
    {
        public ProductMessenger(ProductMessage value) : base(value)
        {

        }
    }
}
