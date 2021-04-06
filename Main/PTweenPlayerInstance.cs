using System;
using UnityEngine;
using System.Collections.Generic;

namespace PTween
{
    [Serializable]
    public class PTweenPlayerInstance
    {
        public PTweenPlayerComponent PlayerTarget;
        public PTweenInstance[] PtweenInstances;

        [Space(10f)]
        public PTweenAnimationDirection AnimationDirection;
        public bool IsPlayerFinished;
    }
}