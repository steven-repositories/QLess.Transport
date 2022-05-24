using Q.Less.Transport.Api.Abstractions;
using Q.Less.Transport.Api.Entities;

namespace Q.Less.Transport.Api.Builders {
    public abstract class Builder<T> : BaseBuilder<IQLessResponse> where T : Builder<T> {
        internal decimal LoadAmount { get; set; }

        internal Builder(TransactionCommands command) : base(command) { }

        public T WithLoadAmount(decimal loadAmount) {
            LoadAmount = loadAmount;
            return (T)this;
        }
    }
}
