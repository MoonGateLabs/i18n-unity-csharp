using Lib.SimpleJSON;
using System;
using UnityEngine;
using System.Linq;

namespace Mgl
{
    public class I18n
    {
        private static JSONNode translationData = null;

        protected static readonly I18n instance = new I18n();

        protected static string[] locales = new string[] { "en-US", "fr-FR", "es-ES" };

        private static string _currentLocale = "en-US";

        private static string _localePath = "Locales/";

        private static bool _isLoggingMissing = true;

        static I18n()
        {
           
        }

        protected I18n()
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
            if (locales.Contains(_currentLocale))
            {
                string localConfigPath = _localePath + _currentLocale;
                // Read the file as one string.
                TextAsset configText = Resources.Load(localConfigPath) as TextAsset;
                translationData = JSON.Parse(configText.text);
            }
            else if (_isLoggingMissing)
            {
                Debug.Log("Missing: locale [" + _currentLocale + "] not found in supported list");
            }
        }

        public static string GetLocale()
        {
            return _currentLocale;
        }

        public static void SetLocale(string newLocale = null)
        {
            Configure (newLocale: newLocale);
        }

        public static void SetPath(string localePath = null)
        {
            Configure (localePath: localePath);
        }

        public static void Configure(string localePath = null, string newLocale = null, bool? logMissing = null, string[] locales = null)
        {
            if (localePath != null) {
                _localePath = localePath;
            }
            if (newLocale != null) {
                _currentLocale = newLocale;
            }
            if (logMissing.HasValue) {
                _isLoggingMissing = logMissing.Value;
            }
            if (locales != null) {
                I18n.locales = (string[])locales.Clone ();
            }
            InitConfig();
        }

        public string __(string key, params object[] args)
        {
            if (translationData == null)
            {
                InitConfig();
            }
            string translation = key;
            if (translationData[key] != null)
            {
                // if this key is a direct string
                if (translationData[key].Count == 0)
                {
                    translation = translationData[key];
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
            JSONClass translationOptions = translationData[key].AsObject;
            string translation = key;
            string singPlurKey;
            // find format to try to use
            switch (GetCountAmount(args))
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

        int GetCountAmount(object[] args)
        {
            int argOne = 0;
            // if arguments passed, try to parse first one to use as count
            if (args.Length > 0 && IsNumeric(args[0]))
            {
                argOne = Math.Abs(Convert.ToInt32(args[0]));
                if (argOne == 1 && Math.Abs(Convert.ToDouble(args[0])) != 1)
                {
                    // check if arg actually equals one
                    argOne = 2;
                }
                else if (argOne == 0 && Math.Abs(Convert.ToDouble(args[0])) != 0)
                {
                    // check if arg actually equals one
                    argOne = 2;
                }
            }
            return argOne;
        }

        bool IsNumeric(System.Object Expression)
        {
            if (Expression == null || Expression is DateTime)
                return false;

            if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
                return true;

            return false;
        }

    }
}
