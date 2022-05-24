using Q.Less.Transport.Api.Abstractions;
using Q.Less.Transport.Api.Builders;
using Q.Less.Transport.Api.Controller;
using Q.Less.Transport.Api.Entities;

namespace Q.Less.Transport.Api.Substructures {
    internal class QLessInterface<T> : IQLessInterface where T : QLessController {
        protected T _controller;

        public QLessInterface(T controller) {
            _controller = controller;
        }

        public DiscountedTransportationCardBuilder TransitDiscountedTransportation(decimal loadAmount, DiscountValidIds validId) {
            return new DiscountedTransportationCardBuilder(TransactionCommands.TransitDiscountedTransportation, validId)
                .WithLoadAmount(loadAmount);
        }

        public TransportationCardBuilder TransitNormalTransportation(decimal loadAmount) {
            return new TransportationCardBuilder(TransactionCommands.TransitNormalTransportation)
                .WithLoadAmount(loadAmount);
        }

        public TransportationCardBuilder ReloadTransportationCard(decimal reloadAmount) {
            return new TransportationCardBuilder(TransactionCommands.ReloadTransportationCard)
                .WithReloadAmount(reloadAmount);
        }
    }
}
