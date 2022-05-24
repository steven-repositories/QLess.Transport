using Q.Less.Transport.Api.Abstractions;
using Q.Less.Transport.Api.Controller;
using Q.Less.Transport.Api.Processors;
using Q.Less.Transport.Api.Substructures;

namespace Q.Less.Transport.Api.Controllers {
    internal class QLessTransportController : QLessController {
        public QLessTransportController(IQLessConfiguration configuration) : base(configuration) { }

        internal override IQLessCommunicationInterface ConfigureQLessCommunication() {
            return new QLessTransportProcessor();
        }

        internal override IQLessInterface ConfigureQLessInterface() {
            return new QLessInterface<QLessController>(this);
        }
    }
}
