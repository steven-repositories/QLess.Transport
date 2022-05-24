using Q.Less.Transport.Api.Entities;

namespace Q.Less.Transport.Api.Abstractions {
    public interface IQLessConfiguration {
        TransactionTypes TransactionMode { get; set; }
    }
}
