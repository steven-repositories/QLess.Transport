using Q.Less.Transport.Api.Abstractions;
using Q.Less.Transport.Api.Configurations;
using Q.Less.Transport.Api.Entities;
using System;
using System.Reflection;

namespace Q.Less.Transport.Api.Builders {
    public class DiscountedTransportationCardBuilder : Builder<DiscountedTransportationCardBuilder> {
        private DiscountValidIds _discountValidId;

        internal string SeniorCitizenControlNumber { get; set; }
        internal string PWDIdentificationNumber { get; set; }

        internal DiscountedTransportationCardBuilder(TransactionCommands command, DiscountValidIds discountValidId) : base(command) {
            _discountValidId = discountValidId;
        }

        public DiscountedTransportationCardBuilder WithSeniorCitizenControlNumber(long number) {
            SeniorCitizenControlNumber = number
                .ToString("##-####-####");

            return this;
        }

        public DiscountedTransportationCardBuilder WithPWDIdentificationNumber(long number) {
            PWDIdentificationNumber = number
                .ToString("####-####-####");

            return this;
        }

        public override IQLessResponse Execute() {
            base.Execute();

            return QLessServicesContainer
                .GetQLessController()
                .ProcessDiscountedTransporatationCard(this);
        }

        protected override void SetupValidations() {
            if (LoadAmount == default) {
                throw new Exception("Current load amount of the commuter should be passed as parameter");
            }

            // Used switch..case statement here for
            // Prepration of upcoming or future transaction commands
            switch (TransactionCommand) {
                case TransactionCommands.TransitDiscountedTransportation:
                    var discountedBuilderType = GetType();

                    foreach (var prop in discountedBuilderType.GetRuntimeProperties()) {
                        var propValue = prop.GetValue(this);

                        if (propValue == default) {
                            if (prop.Name == _discountValidId.ToString()) {
                                throw new Exception($"{prop.Name} is required to fill");
                            }
                        }
                    }
                    break;
                default:
                    throw new Exception($"Transaction command {TransactionCommand} is not supported by the Discounted Transporation Card");
            }
        }
    }
}
