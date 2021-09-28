using System.Collections.Generic;
using UnityEngine;

namespace UNKO.Localize
{
    public interface ILocalizeManager
    {
        event System.Action<SystemLanguage> OnChangeLanguage;
        event System.Action<Font> OnChangeFont;
        SystemLanguage CurrentLanguage { get; }

        ILocalizeManager AddData(IEnumerable<ILocalizeData> data);
        ILocalizeManager AddFontData(IEnumerable<ILocalizeFontData> data);
        ILocalizeManager ChangeLanguage(SystemLanguage language);

        string GetLocalizeText(string languageID, params string[] param);
        bool TryGetLocalizeText(string languageID, out string result, params string[] param);
        bool TryGetFont(out Font font);
    }
}
