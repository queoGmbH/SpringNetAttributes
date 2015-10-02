using Spring.Objects.Factory.Config;

namespace Com.QueoFlow.Spring.Attributes {
    public class ViewModelAttribute : ComponentAttribute {
        public ViewModelAttribute() {
            AutoWiringMode = AutoWiringMode.AutoDetect;
        }
    }
}