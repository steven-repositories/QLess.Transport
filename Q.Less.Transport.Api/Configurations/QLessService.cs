using Q.Less.Transport.Api.Abstractions;

namespace Q.Less.Transport.Api.Configurations {
    public class QLessService {
        public static IQLessInterface CreateService(QLessConnectionConfig config) {
            QLessServicesContainer.ConfigureService(config);
            return QLessServicesContainer.GetQLessInterface();
        }
    }
}
