using System.Linq;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UNKO.Localize
{
    public class LocalizeTextGroup : LocalizeComponentBase
    {
        static string[] _dummyParam = new string[0];

        [SerializeField]
        int _groupIndex;

        [SerializeField]
        private Text _text; public Text TextComponent { get { Awake(); return _text; } }
        public string Text { get => TextComponent.text; set => TextComponent.text = value; }

        bool _isExecuteAwake = false;

        public void Awake()
        {
            if (_isExecuteAwake)
            {
                return;
            }
            _isExecuteAwake = true;

            if (_text == null)
                _text = GetComponent<Text>();
        }

        public string GetGroupText()
            => GetGroupText(0);

        public string GetGroupText(int groupIndex)
        {
            Awake();

            string key = _languageKey;
            if (groupIndex > 0)
            {
                key += $"_{groupIndex}";
            }

            _languageParam = _dummyParam;
            return s_manager.GetLocalizeText(key);
        }

        public string GetGroupTextWithParam(params string[] param)
            => GetGroupTextWithParam(0, param);

        public string GetGroupTextWithParam(int groupIndex, params string[] param)
        {
            Awake();

            string key = _languageKey;
            if (groupIndex > 0)
            {
                key += $"_{groupIndex}";
            }

            _languageParam = param;
            return s_manager.GetLocalizeText(key, param);
        }

        public void SetGroupText()
            => SetGroupText(0);

        public void SetGroupText(int groupIndex)
        {
            TextComponent.text = GetGroupText(groupIndex);
            _groupIndex = groupIndex;
        }

        public void SetGroupTextWithParam(params string[] param)
            => SetGroupTextWithParam(0, param);

        public void SetGroupTextWithParam(int groupIndex, params string[] param)
        {
            TextComponent.text = GetGroupTextWithParam(groupIndex, param);
            _groupIndex = groupIndex;
        }

        protected override void OnSetup()
        {
            base.OnSetup();

            s_manager.OnChangeFont -= OnChangeFont;
            s_manager.OnChangeFont += OnChangeFont;

            if (s_manager.TryGetFont(out Font font))
            {
                OnChangeFont(font);
            }
        }

        protected override void OnChangeLanguage(SystemLanguage language)
        {
            if (string.IsNullOrEmpty(_languageKey))
                return;

            if (TextComponent == null)
            {
                return;
            }

            if (_languageParam.Length > 0)
            {
                TextComponent.text = s_manager.GetLocalizeText(_languageKey, _languageParam);
            }
            else
            {
                TextComponent.text = s_manager.GetLocalizeText(_languageKey);
            }
        }
        protected override void OnDisable()
        {
            base.OnDisable();

            if (s_manager != null)
            {
                s_manager.OnChangeFont -= OnChangeFont;
            }
        }

        void OnChangeFont(Font font)
        {
            TextComponent.font = font;
        }
    }
}
