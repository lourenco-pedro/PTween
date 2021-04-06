using UnityEngine;

namespace PTween
{
    public class PClip
    {
        public bool Animate;

        [Space(10f)]

        public float Duration;
        public float Delay;

        [HideInInspector] public float CurrentDelayTime;
        [HideInInspector] public float CurrenTime;

        [Space(10f)]
        public AnimationCurve Curve;
    }
}