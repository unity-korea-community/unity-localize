using System;
using System.Collections.Generic;
using UnityEngine;

namespace UNKO.Localize
{
    public class LocalizeManagerComponent : MonoBehaviour, ILocalizeManager
    {
        public SystemLanguage currentLanguage => _manager.currentLanguage;

        public event Action<SystemLanguage> OnChangeLanguage
        {
            add => _manager.OnChangeLanguage += value;
            remove => _manager.OnChangeLanguage -= value;
        }
        public event Action<Font> OnChangeFont
        {
            add => _manager.OnChangeFont += value;
            remove => _manager.OnChangeFont -= value;
        }

        LocalizeManager _manager = new LocalizeManager();

        public ILocalizeManager AddData(IEnumerable<ILocalizeData> datas) => _manager.AddData(datas);
        public ILocalizeManager AddFontData(IEnumerable<ILocalizeFontData> datas) => _manager.AddFontData(datas);
        public ILocalizeManager ChangeLanguage(SystemLanguage language) => _manager.ChangeLanguage(language);

        public string GetLocalizeText(string languageID, params string[] param) => _manager.GetLocalizeText(languageID, param);
        public bool TryGetLocalizeText(string languageID, out string result, params string[] param) => _manager.TryGetLocalizeText(languageID, out result, param);
    }
}
