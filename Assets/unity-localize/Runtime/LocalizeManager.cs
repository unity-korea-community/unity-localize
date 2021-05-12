using System.Collections.Generic;
using UnityEngine;

namespace UNKO.Localize
{
    public class LocalizeManagerComponent : MonoBehaviour, ILocalizeManager
    {
        public event System.Action<SystemLanguage> OnChangeLanguage;
        public event System.Action<Font> OnChangeFont;
        public SystemLanguage currentLanguage { get; private set; }

        private Dictionary<string, ILocalizeData> _languageDictionary = new Dictionary<string, ILocalizeData>();
        private Dictionary<SystemLanguage, ILocalizeFontData> _fontDictionary = new Dictionary<SystemLanguage, ILocalizeFontData>();

        public ILocalizeManager AddData(IEnumerable<ILocalizeData> datas)
        {
            foreach (ILocalizeData data in datas)
                _languageDictionary.Add(data.LocalizeID, data);

            return this;
        }

        public ILocalizeManager AddFontData(IEnumerable<ILocalizeFontData> datas)
        {
            foreach (ILocalizeFontData data in datas)
                _fontDictionary.Add(data.language, data);

            return this;
        }

        public ILocalizeManager ChangeLanguage(SystemLanguage language)
        {
            currentLanguage = language;
            OnChangeLanguage?.Invoke(language);
            if (_fontDictionary.TryGetValue(language, out ILocalizeFontData fontData))
            {
                Font font = fontData.GetFont();
                if (font == null)
                {
                    Debug.LogError($"{name}.{nameof(ChangeLanguage)}(language({language}) font == null");
                    return this;
                }

                OnChangeFont?.Invoke(font);
            }

            return this;
        }

        public string GetLocalizeText(string languageID, params string[] param)
        {
            if (string.IsNullOrEmpty(languageID))
                return "";

            if (_languageDictionary.TryGetValue(languageID, out var data) == false)
            {
                Debug.LogError($"LanguageManager GetLocalizeText('{languageID}') == not found data");
#if UNITY_EDITOR
                return $"Error:'{languageID}'";
#else
                return "";
#endif
            }

            string text = data.GetLocalizeText(currentLanguage);
            if (param.Length > 0)
                text = string.Format(text, param);

            return text;
        }

        public bool IsValidID(string languageID)
        {
            return _languageDictionary.ContainsKey(languageID);
        }
    }
}
