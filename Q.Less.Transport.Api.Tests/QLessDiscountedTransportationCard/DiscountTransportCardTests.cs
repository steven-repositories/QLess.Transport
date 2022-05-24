using NUnit.Framework;
using Q.Less.Transport.Api.Abstractions;
using Q.Less.Transport.Api.Configurations;
using Q.Less.Transport.Api.Entities;
using System;

namespace Q.Less.Transport.Api.Tests.QLessDiscountedTransportationCard {
    public class DiscountTransportCardTests {
        private IQLessInterface _qLessInterface;

        public DiscountTransportCardTests() {
            // Assume that the connection service is via TCP/IP
            _qLessInterface = QLessService.CreateService(new QLessConnectionConfig() {
                TransactionMode = TransactionTypes.DiscountedTransportationCard
            });

            Assert.IsNotNull(_qLessInterface);
        }

        [Test]
        public void DiscountedSeniorCitizenTransportation() {
            try {
                var discountedTransport = _qLessInterface
                    .TransitDiscountedTransportation(500m, DiscountValidIds.SeniorCitizenControlNumber)
                    .WithSeniorCitizenControlNumber(1234567890)
                    .Execute();

                if (discountedTransport.IsTransportationSuccess) {
                    Assert.IsTrue(discountedTransport.NewCardExpiration == DateTime.Now.Date.AddYears(3));
                    Assert.IsTrue(discountedTransport.NewLoadAmount == 492m);

                    // Nested transactions here to verify
                    // if Discount applies correctly
                    var discountedTransport2 = _qLessInterface
                        .TransitDiscountedTransportation(discountedTransport.NewLoadAmount, DiscountValidIds.SeniorCitizenControlNumber)
                        .WithSeniorCitizenControlNumber(1234567890)
                        .Execute();

                    if (discountedTransport2.IsTransportationSuccess)
                    {
                        Assert.IsTrue(discountedTransport2.NewCardExpiration == DateTime.Now.Date.AddYears(3));
                        Assert.IsTrue(discountedTransport2.NewLoadAmount == 484.3m);

                        var discountedTransport3 = _qLessInterface
                            .TransitDiscountedTransportation(discountedTransport2.NewLoadAmount, DiscountValidIds.SeniorCitizenControlNumber)
                            .WithSeniorCitizenControlNumber(1234567890)
                            .Execute();

                        if (discountedTransport3.IsTransportationSuccess)
                        {
                            Assert.IsTrue(discountedTransport3.NewCardExpiration == DateTime.Now.Date.AddYears(3));
                            Assert.IsTrue(discountedTransport3.NewLoadAmount == 476.6m);

                            var discountedTransport4 = _qLessInterface
                                .TransitDiscountedTransportation(discountedTransport3.NewLoadAmount, DiscountValidIds.SeniorCitizenControlNumber)
                                .WithSeniorCitizenControlNumber(1234567890)
                                .Execute();

                            if (discountedTransport4.IsTransportationSuccess)
                            {
                                Assert.IsTrue(discountedTransport4.NewCardExpiration == DateTime.Now.Date.AddYears(3));
                                Assert.IsTrue(discountedTransport4.NewLoadAmount == 468.9m);

                                var discountedTransport5 = _qLessInterface
                                    .TransitDiscountedTransportation(discountedTransport4.NewLoadAmount, DiscountValidIds.SeniorCitizenControlNumber)
                                    .WithSeniorCitizenControlNumber(1234567890)
                                    .Execute();

                                if (discountedTransport5.IsTransportationSuccess)
                                {
                                    Assert.IsTrue(discountedTransport5.NewCardExpiration == DateTime.Now.Date.AddYears(3));
                                    Assert.IsTrue(discountedTransport5.NewLoadAmount == 461.2m);
                                }
                            }
                        }
                    } 
                }
            } catch (Exception e) {
                throw e;
            }
        }

        [Test]
        public void DiscountedPWDTransportation() {
            try {
                var discountedTransport = _qLessInterface
                    .TransitDiscountedTransportation(550m, DiscountValidIds.PWDIdentificationNumber)
                    .WithPWDIdentificationNumber(123456789012)
                    .Execute();

                if (discountedTransport.IsTransportationSuccess) {
                    Assert.IsTrue(discountedTransport.NewCardExpiration == DateTime.Now.Date.AddYears(3));

                    // Nested transactions can also 
                    // be applied here the same
                    // on the above transaction
                    // .. code here ..
                }
            } catch (Exception e) {
                throw e;
            }
        }
    }
}
