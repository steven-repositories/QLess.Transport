using Q.Less.Transport.Api.Abstractions;
using Q.Less.Transport.Api.Controller;
using Q.Less.Transport.Api.Entities;
using System;

namespace Q.Less.Transport.Api.Configurations {
    public class QLessConfiguredServices {
        private QLessController _qLessController;

        internal QLessController QLessController {
            get {
                return _qLessController;
            }

            set {
                _qLessController = value;

                switch (TransactionMode) {
                    case TransactionTypes.TransportationCard:
                    case TransactionTypes.DiscountedTransportationCard:
                        QLessInterface = value.ConfigureQLessInterface();
                        break;
                    default:
                        throw new Exception("Transaction mode cannot be null");
                }
            }
        }

        internal TransactionTypes TransactionMode { get; set; }
        internal IQLessInterface QLessInterface { get; set; }
    }
}
