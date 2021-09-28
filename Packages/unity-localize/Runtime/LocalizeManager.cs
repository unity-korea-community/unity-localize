using System.Collections.Generic;
using UnityEngine;

namespace UNKO.Localize
{
    [System.Serializable]
    public class LocalizeManager : ILocalizeManager
    {
        public event System.Action<SystemLanguage> OnChangeLanguage;
        public event System.Action<Font> OnChangeFont;

        [SerializeField]
        private SystemLanguage _currentLanguage; public SystemLanguage CurrentLanguage => _currentLanguage;

        private Dictionary<string, ILocalizeData> _languageDictionary = new Dictionary<string, ILocalizeData>();
        private Dictionary<SystemLanguage, ILocalizeFontData> _fontDictionary = new Dictionary<SystemLanguage, ILocalizeFontData>();

        public ILocalizeManager AddData(IEnumerable<ILocalizeData> datas)
        {
            foreach (ILocalizeData data in datas)
                _languageDictionary.Add(data.GetLocalizeID(), data);

            return this;
        }

        public ILocalizeManager AddFontData(IEnumerable<ILocalizeFontData> datas)
        {
            foreach (ILocalizeFontData data in datas)
                _fontDictionary.Add(data.GetLanguage(), data);

            return this;
        }

        public ILocalizeManager ChangeLanguage(SystemLanguage language)
        {
            _currentLanguage = language;
            OnChangeLanguage?.Invoke(language);

            TryGetFont(out Font font);
            OnChangeFont?.Invoke(font);

            return this;
        }

        public string GetLocalizeText(string languageID, params string[] param)
        {
            TryGetLocalizeText(true, languageID, out string result, param);
            return result;
        }

        public bool TryGetLocalizeText(string languageID, out string result, params string[] param)
        {
            return TryGetLocalizeText(false, languageID, out result, param);
        }

        public bool IsValidID(string languageID)
        {
            return _languageDictionary.ContainsKey(languageID);
        }


        bool TryGetLocalizeText(bool printError, string languageID, out string result, params string[] param)
        {
            result = $"Error:'{languageID}'";
            if (string.IsNullOrEmpty(languageID))
                return false;

            if (_languageDictionary.TryGetValue(languageID, out ILocalizeData data) == false)
            {
                if (printError)
                    Debug.LogError($"LanguageManager GetLocalizeText('{languageID}') == not found data");

                return false;
            }

            result = data.GetLocalizeText(CurrentLanguage);
            if (param.Length > 0)
                result = string.Format(result, param);

            return true;
        }

        public bool TryGetFont(out Font font)
        {
            font = null;
            if (_fontDictionary.TryGetValue(_currentLanguage, out ILocalizeFontData fontData))
            {
                font = fontData.GetFont();
                if (font == null)
                {
                    Debug.LogError($"LocalizeManager.{nameof(ChangeLanguage)}(language({_currentLanguage}) font == null");
                    return false;
                }

            }

            return font != null;
        }
    }
}
