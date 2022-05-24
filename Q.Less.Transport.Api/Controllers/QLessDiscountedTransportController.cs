using Q.Less.Transport.Api.Abstractions;
using Q.Less.Transport.Api.Controller;
using Q.Less.Transport.Api.Processors;
using Q.Less.Transport.Api.Substructures;

namespace Q.Less.Transport.Api.Controllers {
    internal class QLessDiscountedTransportController : QLessController {
        public QLessDiscountedTransportController(IQLessConfiguration configuration) : base(configuration) { }

        internal override IQLessCommunicationInterface ConfigureQLessCommunication() {
            return new QLessDiscountedTransportProcessor();
        }

        internal override IQLessInterface ConfigureQLessInterface() {
            return new QLessInterface<QLessController>(this);
        }
    }
}
