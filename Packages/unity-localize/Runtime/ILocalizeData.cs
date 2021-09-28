using UnityEngine;

namespace UNKO.Localize
{
    /// <summary>
    /// 로컬라이즈 텍스트 데이터
    /// </summary>
    public interface ILocalizeData
    {
        string GetLocalizeID();
        string GetLocalizeText(SystemLanguage systemLanguage);
    }

    public class SimpleLocalizeData : ILocalizeData
    {
        string _localizeID;
        System.Func<SystemLanguage, string> _OnGetLocalizeText;

        public SimpleLocalizeData(string localizeID, System.Func<SystemLanguage, string> OnGetLocalizeText)
        {
            _localizeID = localizeID;
            _OnGetLocalizeText = OnGetLocalizeText;
        }

        public string GetLocalizeID() => _localizeID;
        public string GetLocalizeText(SystemLanguage systemLanguage) => _OnGetLocalizeText(systemLanguage);
    }

    /// <summary>
    /// 로컬라이즈 폰트 데이터
    /// </summary>
    public interface ILocalizeFontData
    {
        SystemLanguage GetLanguage();

        Font GetFont();
    }
}
