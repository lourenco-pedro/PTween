using System;
using UnityEngine;

namespace PTween
{
    [System.Serializable]
    public class PTweenInstance
    {
        public PTweenComponent Target;

        [Space(10f)]
        public PTweenTransformClip Position;
        public PTweenTransformClip Rotation;
        public PTweenTransformClip Scale;
        public PtweenAlphaClip Alpha;

        [Space(10f)]
        public PTweenAnimationDirection AnimationDirection;
        public bool Finished;
    }
}