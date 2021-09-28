using System.Collections.Generic;
using UnityEngine;

namespace UNKO.Localize
{
    public class LocalizeManagerComponentSingleton : LocalizeManagerComponent
    {
#pragma warning disable IDE1006
        public static LocalizeManagerComponentSingleton instance
#pragma warning restore IDE1006
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<LocalizeManagerComponentSingleton>();
                    LocalizeComponentBase.Init(_instance);
                }

                return _instance;
            }
        }

        static LocalizeManagerComponentSingleton _instance;
    }
}
