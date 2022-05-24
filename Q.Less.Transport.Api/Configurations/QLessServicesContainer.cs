using Q.Less.Transport.Api.Abstractions;
using Q.Less.Transport.Api.Controller;

namespace Q.Less.Transport.Api.Configurations {
    public class QLessServicesContainer {
        private static QLessConfiguredServices _configurations;

        public static void ConfigureService<T>(T config) where T : QLessConfig {
            _configurations = GetConfiguration();
            config.ConfigreQLessContainer(_configurations);
        }

        private static QLessConfiguredServices GetConfiguration() {
            return new QLessConfiguredServices();
        }

        internal static IQLessInterface GetQLessInterface() {
            return _configurations.QLessInterface;
        }

        internal static QLessController GetQLessController() {
            return _configurations.QLessController;
        }
    }
}
