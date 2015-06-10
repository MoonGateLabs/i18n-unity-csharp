using Lib.SimpleJSON;
using System.Linq;
using UnityEngine;

namespace Mgl
{
    sealed class I18n
    {
        private static JSONNode config;

        private static readonly I18n instance = new I18n();

        private static string[] locales = new string[] { "en-US", "fr-FR", "es-ES" };

        private static string _defaultLocale = "en-US";

        private static string _extension = ".json";

        private static string _localePath = "Assets/Resources/Locales/";

        private static bool _isLoggingMissing = true;

        static I18n()
        {
        }

        private I18n()
        {
        }

        public static I18n Instance
        {
            get
            {
                return instance;
            }
        }

        static void InitConfig()
        {
            if (locales.Contains(_defaultLocale))
            {
                string localConfigPath = _localePath + _defaultLocale + _extension;
                // Read the file as one string.
                System.IO.StreamReader configFile = new System.IO.StreamReader(localConfigPath);
                string configString = configFile.ReadToEnd();
                configFile.Close();
                config = JSON.Parse(configString);
            }
            else if (_isLoggingMissing)
            {
                Debug.Log("Missing: locale [" + _defaultLocale + "] not found in supported list");
            }
        }

        public static void Configure(string localePath = null, string defaultLocale = null, bool logMissing = true)
        {
            _isLoggingMissing = logMissing;
            if (localePath != null)
            {
                _localePath = localePath;
            }
            if (defaultLocale != null)
            {
                _defaultLocale = defaultLocale;
            }
            InitConfig();
        }

        public string __(string key, params object[] args)
        {
            string translation = key;
            if (config[key] != null)
            {
                // if this key is a direct string
                if (config[key].Count == 0)
                {
                    translation = config[key];
                }
                else
                {
                    translation = FindSingularOrPlural(key, args);
                }
                // check if we have embeddable data
                if (args.Length > 0)
                {
                    translation = string.Format(translation, args);
                }
            }
            else if (_isLoggingMissing)
            {
                Debug.Log("Missing translation for:" + key);
            }
            return translation;
        }

        string FindSingularOrPlural(string key, object[] args)
        {
            string translation = key;
            string singPlurKey;
            int argOne = 0;
            JSONClass translationOptions = config[key].AsObject;
            // if arguments passed, try to parse first one to use as count
            if (args.Length > 0 && args[0] is int)
            {
                argOne = (int)args[0];
            }
            // find format to try to use
            switch (argOne)
            {
                case 0:
                    singPlurKey = "zero";
                    break;
                case 1:
                    singPlurKey = "one";
                    break;
                default:
                    singPlurKey = "other";
                    break;
            }
            // try to use this plural/singular key
            if (translationOptions[singPlurKey] != null)
            {
                translation = translationOptions[singPlurKey];
            }
            else if (_isLoggingMissing)
            {
                Debug.Log("Missing singPlurKey:" + singPlurKey + " for:" + key);
            }
            return translation;
        }

    }
}
