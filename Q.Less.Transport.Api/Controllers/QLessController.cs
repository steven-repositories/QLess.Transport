using Q.Less.Transport.Api.Abstractions;
using Q.Less.Transport.Api.Builders;
using Q.Less.Transport.Api.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Q.Less.Transport.Api.Controller {
    internal abstract class QLessController {
        protected IQLessConfiguration _configuration;
        protected IQLessCommunicationInterface _communication;

        public QLessController(IQLessConfiguration configuration) {
            _configuration = configuration;
            _communication = ConfigureQLessCommunication();
        }

        internal TransactionTypes? TransactionMode {
            get {
                if (_configuration != null) {
                    return _configuration.TransactionMode;
                }

                return null;
            }
        }

        #region Configuration Setup Abstract Methods
        internal abstract IQLessInterface ConfigureQLessInterface();
        internal abstract IQLessCommunicationInterface ConfigureQLessCommunication();
        #endregion

        internal IQLessResponse ProcessTransportationCard(TransportationCardBuilder builder) {
            var dictionary = new Dictionary<string, string>();

            // Used switch case here for future or
            // Proper request building for each Transaction Command
            switch (builder.TransactionCommand) {
                case TransactionCommands.TransitNormalTransportation:
                case TransactionCommands.ReloadTransportationCard:
                    var discountedBuildeType = builder.GetType();

                    foreach (var prop in discountedBuildeType.GetRuntimeProperties()) {
                        var propValue = prop.GetValue(builder);

                        if (propValue != default) {
                            dictionary.Add(prop.Name, propValue.ToString());
                        }
                    }

                    break;
                default:
                    throw new Exception($"Transaction command {builder.TransactionCommand} is not supported by the Transporation Card");
            }

            return _communication.Process(dictionary);
        }

        internal IQLessResponse ProcessDiscountedTransporatationCard(DiscountedTransportationCardBuilder builder) {
            var dictionary = new Dictionary<string, string>();

            // Used switch case here for future or
            // Proper request building for each Transaction Command
            switch (builder.TransactionCommand) {
                case TransactionCommands.TransitDiscountedTransportation:
                    var discountedBuildeType = builder.GetType();

                    foreach (var prop in discountedBuildeType.GetRuntimeProperties()) {
                        var propValue = prop.GetValue(builder);

                        if (propValue != default) {
                            dictionary.Add(prop.Name, propValue.ToString());
                        }
                    }

                    break;
                default:
                    throw new Exception($"Transaction command {builder.TransactionCommand} is not supported by the Discounted Transporation Card");
            }

            return _communication.Process(dictionary);
        }
    }
}
