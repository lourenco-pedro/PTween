using System;
using UnityEngine;
using UnityEngine.UI;

namespace PTween
{
    [System.Serializable]
    public class PTweenTransformClip : PClip
    {
        public Vector3 From;
        public Vector3 To;

        public PTweenTransformClip()
        {
            From = new Vector3();
            To = new Vector3();
            Curve = new AnimationCurve();
        }
    }
}