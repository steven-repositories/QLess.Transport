using Q.Less.Transport.Api.Abstractions;
using Q.Less.Transport.Api.Entities;
using Q.Less.Transport.Api.Substructures;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Q.Less.Transport.Api.Processors {
    public class QLessTransportProcessor : IQLessCommunicationInterface {
        private decimal _loadAmount;
        private DateTime _lastDateCaptured;

        public void Connect() {
            _lastDateCaptured = default;
        }

        public IQLessResponse Process(Dictionary<string, string> dictionary) {
            var customerReloadChange = default(decimal);
            var newCardExpiration = default(DateTime);

            dictionary.TryGetValue("TransactionCommand", out string command);
            var transactionCommand = (TransactionCommands)Enum.Parse(typeof(TransactionCommands), command);

            if (transactionCommand == TransactionCommands.ReloadTransportationCard) {
                dictionary.TryGetValue("ReloadAmount", out string amount);
                dictionary.TryGetValue("CustomerMoney", out string money);

                var reloadAmount = decimal.Parse(amount);
                var customerMoney = decimal.Parse(money);

                customerReloadChange = customerMoney != reloadAmount
                    ? customerMoney - reloadAmount
                    : (_loadAmount + reloadAmount) > 10000m
                        ? (_loadAmount + reloadAmount) - 10000m
                        : 0m;
            } else {
                // Sleep here assuming that the commuter
                // Is still transporting from one station to another
                Thread.Sleep(5000);

                var amount = string.Empty;

                if (_loadAmount == default(decimal)) {
                    dictionary.TryGetValue("LoadAmount", out amount);
                    _loadAmount = decimal.Parse(amount ?? "0");
                }
                
                if (_loadAmount == default) {
                    throw new Exception("Commuter's load amount should not be 0");
                }

                _loadAmount -= 15m;

                _lastDateCaptured = DateTime.Now;
                newCardExpiration = _lastDateCaptured
                    .AddYears(5)
                    .Date;
            }

            return new QLessResponse(true, _loadAmount, newCardExpiration, customerReloadChange);
        }
    }
}
