using Q.Less.Transport.Api.Abstractions;
using Q.Less.Transport.Api.Controller;
using Q.Less.Transport.Api.Controllers;
using Q.Less.Transport.Api.Entities;
using System;

namespace Q.Less.Transport.Api.Configurations {
    public class QLessConnectionConfig : QLessConfig, IQLessConfiguration {
        public TransactionTypes TransactionMode { get; set; }

        internal override void ConfigreQLessContainer(QLessConfiguredServices services) {
            services.TransactionMode = TransactionMode;

            switch (TransactionMode) {
                case TransactionTypes.TransportationCard:
                    services.QLessController = new QLessTransportController(this);
                    break;
                case TransactionTypes.DiscountedTransportationCard:
                    services.QLessController = new QLessDiscountedTransportController(this);
                    break;
                default:
                    throw new Exception("Transaction mode cannot be null");
            }
            
        }
    }
}
