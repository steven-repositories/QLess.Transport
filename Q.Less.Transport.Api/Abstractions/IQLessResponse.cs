using System;

namespace Q.Less.Transport.Api.Abstractions {
    public interface IQLessResponse {
        bool IsTransportationSuccess { get; }
        decimal NewLoadAmount { get; }
        DateTime NewCardExpiration { get; }
        decimal CustomerReloadChange { get; }
    }
}
