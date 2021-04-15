using Reactor.API.Configuration;

namespace Centrifuge.Distance.Configuration
{
	public class Config : Settings
    {
        public Config() : base("Centrifuge.Distance")
        {
        }

        private T Get<T>(string key, T @default = default)
        {
            if (ContainsKey(key))
            {
                return GetItem<T>(key);
            }
            else
            {
                return @default;
            }
        }

        private void Set<T>(string key, T value, bool autoSave = true)
        {
            this[key] = value;

            if (autoSave)
            {
                Save();
            }
        }

        public bool ShowVersionInfo
        {
            get => Get("general.showversioninfo", true);
            set => Set("general.showversioninfo", value);
        }
    }
}
