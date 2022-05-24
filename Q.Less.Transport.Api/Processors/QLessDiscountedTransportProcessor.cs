using Q.Less.Transport.Api.Abstractions;
using Q.Less.Transport.Api.Substructures;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Q.Less.Transport.Api.Processors {
    public class QLessDiscountedTransportProcessor : IQLessCommunicationInterface {
        private decimal _loadAmount;
        private DateTime _lastDateCaptured;
        private int _discountCounts;

        public void Connect() {
            _lastDateCaptured = default;
        }

        public IQLessResponse Process(Dictionary<string, string> dictionary) {
            dictionary.TryGetValue("LoadAmount", out string loadAmount);
            _loadAmount = decimal.Parse(loadAmount);

            // Sleep here assuming that the commuter
            // Is still transporting from one station to another
            Thread.Sleep(5000);

            if (_lastDateCaptured.Date == DateTime.Today) {
                if (_loadAmount != default) {
                    if (_discountCounts >= 1 && _discountCounts <= 4) {
                        _loadAmount -= 10 - (10 * 0.23m);
                        _discountCounts++;
                    } else {
                        _loadAmount -= 10 - (10 * 0.20m);
                    }
                }
            } else {
                _discountCounts = 1;
                _loadAmount -= 10 - (10 * 0.20m);
            }

            _lastDateCaptured = DateTime.Now;
            var newCardExpiration = _lastDateCaptured
                .AddYears(3)
                .Date;

            return new QLessResponse(true, _loadAmount, newCardExpiration);
        }
    }
}
