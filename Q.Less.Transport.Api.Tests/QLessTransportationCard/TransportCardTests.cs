using NUnit.Framework;
using Q.Less.Transport.Api.Abstractions;
using Q.Less.Transport.Api.Configurations;
using Q.Less.Transport.Api.Entities;
using System;
using Assert = NUnit.Framework.Assert;

namespace Q.Less.Transport.Api.Tests.QLessTransportationCard {
    public class TransportCardTests {
        private IQLessInterface _qLessInterface;

        public TransportCardTests() {
            // Assume that the connection service is via TCP/IP
            _qLessInterface = QLessService.CreateService(new QLessConnectionConfig() {
                TransactionMode = TransactionTypes.TransportationCard
            });

            Assert.IsNotNull(_qLessInterface);
        }

        [Test]
        public void NormalTransportation() {
            try {
                var normalTransport = _qLessInterface
                    .TransitNormalTransportation(100m)
                    .Execute();

                if (normalTransport.IsTransportationSuccess) {
                    Assert.IsTrue(normalTransport.NewCardExpiration == DateTime.Now.Date.AddYears(5));

                    // Execute another normal transport here
                    // Assuming the commuter will go back
                    // from where he/she came from
                    var normalTransport2 = _qLessInterface
                        .TransitNormalTransportation(normalTransport.NewLoadAmount)
                        .Execute();

                    if (normalTransport2.IsTransportationSuccess) {
                        Assert.IsTrue(normalTransport2.NewCardExpiration == DateTime.Now.Date.AddYears(5));
                    }
                }
            } catch (Exception e) {
                throw e;
            }
        }

        [Test]
        public void ReloadCustomerCard() {
            try {
                var reloadCard = _qLessInterface
                    .ReloadTransportationCard(500m)
                    .WithCustomerMoney(1000m)
                    .WithLoadAmount(5m)
                    .Execute();

                if (reloadCard.IsTransportationSuccess) {
                    Assert.IsTrue(reloadCard.CustomerReloadChange == 500m);
                }
            } catch (Exception e) {
                throw e;
            }
        }
    }
}
