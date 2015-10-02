using Spring.Objects.Factory.Config;

namespace Com.QueoFlow.Spring.Attributes {
    public class RepositoryAttribute : ComponentAttribute {
        public RepositoryAttribute() {
            AutoWiringMode = AutoWiringMode.AutoDetect;
        }
    }
}