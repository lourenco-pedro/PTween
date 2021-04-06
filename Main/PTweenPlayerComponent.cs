using UnityEngine;
using System.Collections.Generic;

namespace PTween
{
    public class PTweenPlayerComponent : MonoBehaviour
    {
        public string NameID;
        public PTweenComponent[] Targets;

        #region UNITY_EDTIOR
#if UNITY_EDITOR
        void OnValidate()
        {
            var ptweenComponents = GetComponentsInChildren<PTweenComponent>();

            Targets = new PTweenComponent[0];

            for (int i = 0; i < ptweenComponents.Length; i++)
            {
                var curComponent = ptweenComponents[i];
                if (curComponent.NameID == NameID)
                    UnityEditor.ArrayUtility.Add(ref Targets, curComponent);
            }
        }
#endif
        #endregion
    }
}