using UnityEngine;
using UnityEngine.UI;

namespace PTween
{
    public class PTweenComponent : MonoBehaviour
    {
        public string NameID;

        [Space(10f)]
        public PTweenTransformClip Position;
        public PTweenTransformClip Rotation;
        public PTweenTransformClip Scale;
        public PtweenAlphaClip Alhpa;

#if UNITY_EDITOR
        void OnValidate()
        {
            Position.Animate = Position.Duration > 0;
            Rotation.Animate = Rotation.Duration > 0;
            Scale.Animate = Scale.Duration > 0;

            Alhpa.CanvasGroup = GetComponent<CanvasGroup>();
            Alhpa.Animate = Alhpa.Duration > 0 && GetComponent<RectTransform>() != null && Alhpa.CanvasGroup != null;
        }
#endif
    }
}