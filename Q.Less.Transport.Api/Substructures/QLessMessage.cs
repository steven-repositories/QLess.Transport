using Q.Less.Transport.Api.Abstractions;

namespace Q.Less.Transport.Api.Substructures {
    internal class QLessMessage : IQLessMessage {
        private string _messageBuffer;

        public QLessMessage(string buffer) {
            _messageBuffer = buffer;
        }

        public string GetMessageBuffer() {
            return _messageBuffer;
        }
    }
}
