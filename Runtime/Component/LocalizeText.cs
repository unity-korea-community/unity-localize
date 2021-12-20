using System.Linq;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UNKO.Localize
{
    public class LocalizeText : LocalizeComponentBase
    {
        [SerializeField]
        private Text _text; public Text TextComponent => _text;
        public string Text { get => _text.text; set => _text.text = value; }

        public void Awake()
        {
            if (_text == null)
                _text = GetComponent<Text>();
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
