using UnityEngine;

namespace PTween
{
    [System.Serializable]
    public class PtweenAlphaClip : PClip
    {

        [Space(10f)]

        public CanvasGroup CanvasGroup;

        [Space(10f)]

        [Range(0, 1)]
        public float From;
        [Range(0, 1)]
        public float To;
    }
}