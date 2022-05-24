using Q.Less.Transport.Api.Abstractions;
using Q.Less.Transport.Api.Configurations;
using Q.Less.Transport.Api.Entities;
using System;
using System.Reflection;

namespace Q.Less.Transport.Api.Builders {
    public class TransportationCardBuilder : Builder<TransportationCardBuilder> {
        internal decimal ReloadAmount { get; set; }
        internal decimal CustomerMoney { get; set; }

        internal TransportationCardBuilder(TransactionCommands command) : base(command) { }

        public TransportationCardBuilder WithReloadAmount(decimal reloadAmount) {
            ReloadAmount = reloadAmount;
            return this;
        }

        public TransportationCardBuilder WithCustomerMoney(decimal customerMoney) {
            CustomerMoney = customerMoney;
            return this;
        }

        public override IQLessResponse Execute() {
            base.Execute();

            return QLessServicesContainer
                .GetQLessController()
                .ProcessTransportationCard(this);
        }

        protected override void SetupValidations() {
            if (LoadAmount == default) {
                throw new Exception("Current load amount of the commuter should be passed as parameter");
            }

            // Used switch..case statement here for
            // Prepration of upcoming or future transaction commands
            switch (TransactionCommand) {
                case TransactionCommands.TransitNormalTransportation:
                case TransactionCommands.ReloadTransportationCard:
                    var builderType = GetType();

                    foreach (var prop in builderType.GetRuntimeProperties()) {
                        var propValue = prop.GetValue(this);

                        if (propValue == default
                            && (prop.Name == "CustomerMoney"
                                || prop.Name == "ReloadAMount")) {
                            throw new Exception($"{prop.Name} is required to fill");
                        }
                    }
                    break;
                default:
                    throw new Exception($"Transaction command {TransactionCommand} is not supported by the Transporation Card");
            }
        }
    }
}
