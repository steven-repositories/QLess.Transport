using System.Collections.Generic;

namespace Q.Less.Transport.Api.Abstractions {
    public interface IQLessCommunicationInterface {
        // Connect will be used to intialzie
        // The start date of the commuter's card (Lifetime)
        void Connect();
        IQLessResponse Process(Dictionary<string, string> dictionary);
    }
}
