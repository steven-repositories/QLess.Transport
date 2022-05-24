using Q.Less.Transport.Api.Abstractions;
using Q.Less.Transport.Api.Entities;

namespace Q.Less.Transport.Api.Builders {
    public abstract class BaseBuilder<TResult> {
        internal TransactionCommands TransactionCommand { get; set; }

        internal BaseBuilder(TransactionCommands command) {
            TransactionCommand = command;
        }

        public virtual TResult Execute() {
            SetupValidations();
            return default;
        }

        protected abstract void SetupValidations();
    }
}
