using Q.Less.Transport.Api.Abstractions;
using System;

namespace Q.Less.Transport.Api.Substructures {
    public class QLessResponse : IQLessResponse {
        public bool IsTransportationSuccess { get; private set; }
        public decimal NewLoadAmount { get; private set; }
        public DateTime NewCardExpiration { get; private set; }
        public decimal CustomerReloadChange { get; private set; }

        internal QLessResponse(bool isTransportationSucess, decimal newLoadAmount, DateTime newCardExpiration, decimal customerReloadChange = default(decimal)) {
            IsTransportationSuccess = isTransportationSucess;
            NewLoadAmount = newLoadAmount;
            NewCardExpiration = newCardExpiration;
            CustomerReloadChange = customerReloadChange;
        }
    }
}
