using Q.Less.Transport.Api.Builders;
using Q.Less.Transport.Api.Entities;

namespace Q.Less.Transport.Api.Abstractions {
    public interface IQLessInterface {
        TransportationCardBuilder ReloadTransportationCard(decimal reloadAmount);
        TransportationCardBuilder TransitNormalTransportation(decimal loadAmount);
        DiscountedTransportationCardBuilder TransitDiscountedTransportation(decimal loadAmount, DiscountValidIds validId);
    }
}
