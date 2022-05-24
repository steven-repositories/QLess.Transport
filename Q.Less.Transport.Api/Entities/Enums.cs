namespace Q.Less.Transport.Api.Entities {
    public enum TransactionTypes {
        TransportationCard,
        DiscountedTransportationCard
    }

    public enum TransactionCommands {
        ReloadTransportationCard,
        TransitNormalTransportation,
        TransitDiscountedTransportation
    }

    public enum DiscountValidIds {
        SeniorCitizenControlNumber,
        PWDIdentificationNumber
    }
}
