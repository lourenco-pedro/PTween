using UnityEngine;
using System.Collections.Generic;

namespace PTween
{
    public static class PTweenUtil
    {

        private static List<PTweenPlayerInstance> m_AllPlayerInstances = new List<PTweenPlayerInstance>();
        public static List<PTweenPlayerInstance> AllPlayerInstances { get { return m_AllPlayerInstances; } }

        ///<summary>Updates PTween Animation System</summary>
        public static void Update()
        {
            for (int i = 0; i < m_AllPlayerInstances.Count; i++)
            {
                var curPlayer = m_AllPlayerInstances[i];
                UpdatePlayerInstance(curPlayer);
            }
        }

        ///<summary>Returns player instance that will be playing</summary>
        public static PTweenPlayerInstance StartPTweenPlayerComponent(PTweenPlayerComponent playerComponent, PTweenAnimationDirection animationDirection)
        {
            PTweenPlayerInstance instance = TryCreateNewPlayerInstance(playerComponent, animationDirection);
            return instance;
        }

        ///<summary>Try create a new Player instance if it hasn't being created before</summary>
        public static PTweenPlayerInstance TryCreateNewPlayerInstance(PTweenPlayerComponent playerComponent, PTweenAnimationDirection animationDirection)
        {
            PTweenPlayerInstance instance = null;

            for (int i = 0; i < m_AllPlayerInstances.Count; i++)
            {
                var curPlayer = m_AllPlayerInstances[i];
                if (curPlayer.PlayerTarget == playerComponent)
                {
                    instance = curPlayer;
                    break;
                }
            }

            if (instance == null)
            {
                instance = new PTweenPlayerInstance();
                instance.PlayerTarget = playerComponent;
                m_AllPlayerInstances.Add(instance);
            }

            instance.AnimationDirection = animationDirection;

            instance.PtweenInstances = new PTweenInstance[playerComponent.Targets.Length];

            for (int i = 0; i < instance.PtweenInstances.Length; i++)
            {
                instance.PtweenInstances[i] = PlayTweenComponent(instance.PlayerTarget.Targets[i], animationDirection);
            }

            return instance;
        }

        ///<summary>Updates player instance PtweensInstances' position, Rotation and Scale</summary>
        public static void UpdatePlayerInstance(PTweenPlayerInstance instance)
        {
            instance.IsPlayerFinished = true;

            for (int i = 0; i < instance.PtweenInstances.Length; i++)
            {
                var curPtweenInstance = instance.PtweenInstances[i];

                if (!IsPTweenInstanceFinished(curPtweenInstance))
                    instance.IsPlayerFinished = false;
                else
                    continue;

                if (curPtweenInstance.Position.Animate)
                {
                    curPtweenInstance.Target.transform.localPosition = UpdatePTweenClipVector(curPtweenInstance.Position, curPtweenInstance.AnimationDirection);
                }

                if (curPtweenInstance.Rotation.Animate)
                {
                    curPtweenInstance.Target.transform.rotation = Quaternion.Euler(UpdatePTweenClipVector(curPtweenInstance.Rotation, curPtweenInstance.AnimationDirection));
                }

                if (curPtweenInstance.Scale.Animate)
                {
                    curPtweenInstance.Target.transform.localScale = UpdatePTweenClipVector(curPtweenInstance.Scale, curPtweenInstance.AnimationDirection);
                }

                if (curPtweenInstance.Alpha.Animate)
                {
                    curPtweenInstance.Alpha.CanvasGroup.alpha = UpdatePTweenClipAlpha(curPtweenInstance.Alpha, curPtweenInstance.AnimationDirection);
                }
            }
        }

        ///<summary>Returns the first PlayerInstance with his player target equal than PlayerComponent parameter</summary>
        public static PTweenPlayerInstance GetPlayerInstanceByOriginalComponent(PTweenPlayerComponent originalComponent)
        {
            for (int i = 0; i < AllPlayerInstances.Count; i++)
            {
                PTweenPlayerInstance curInstance = AllPlayerInstances[i];
                if (curInstance.PlayerTarget == originalComponent)
                    return curInstance;
            }

            return null;
        }

        ///<summary>Creates a new PTweenInstance with the components value to be animated</summary>
        public static PTweenInstance PlayTweenComponent(PTweenComponent ptweenComponent, PTweenAnimationDirection animationDirection)
        {
            PTweenInstance instance = CreateNewPTWeenInstance(ptweenComponent);
            instance.AnimationDirection = animationDirection;
            ResetPTweenInstance(instance);
            return instance;
        }

        ///<summary>Creates and return a new PTweenInstance with the components value</summary>
        public static PTweenInstance CreateNewPTWeenInstance(PTweenComponent ptweeComponent)
        {
            PTweenInstance instance = new PTweenInstance();
            instance.Target = ptweeComponent;

            instance.Position = new PTweenTransformClip();
            instance.Rotation = new PTweenTransformClip();
            instance.Scale = new PTweenTransformClip();
            instance.Alpha = new PtweenAlphaClip();

            instance.Position.From = ptweeComponent.Position.From;
            instance.Position.To = ptweeComponent.Position.To;
            instance.Position.Curve = ptweeComponent.Position.Curve;
            instance.Position.Duration = ptweeComponent.Position.Duration;
            instance.Position.Animate = ptweeComponent.Position.Animate;
            instance.Position.Delay = ptweeComponent.Position.Delay;

            instance.Rotation.From = ptweeComponent.Rotation.From;
            instance.Rotation.To = ptweeComponent.Rotation.To;
            instance.Rotation.Curve = ptweeComponent.Rotation.Curve;
            instance.Rotation.Duration = ptweeComponent.Rotation.Duration;
            instance.Rotation.Animate = ptweeComponent.Rotation.Animate;
            instance.Rotation.Delay = ptweeComponent.Rotation.Delay;

            instance.Scale.From = ptweeComponent.Scale.From;
            instance.Scale.To = ptweeComponent.Scale.To;
            instance.Scale.Curve = ptweeComponent.Scale.Curve;
            instance.Scale.Duration = ptweeComponent.Scale.Duration;
            instance.Scale.Animate = ptweeComponent.Scale.Animate;
            instance.Scale.Delay = ptweeComponent.Scale.Delay;

            instance.Alpha.From = ptweeComponent.Alhpa.From;
            instance.Alpha.To = ptweeComponent.Alhpa.To;
            instance.Alpha.Curve = ptweeComponent.Alhpa.Curve;
            instance.Alpha.Duration = ptweeComponent.Alhpa.Duration;
            instance.Alpha.Animate = ptweeComponent.Alhpa.Animate;
            instance.Alpha.Delay = ptweeComponent.Alhpa.Delay;
            instance.Alpha.CanvasGroup = ptweeComponent.Alhpa.CanvasGroup;

            instance.Finished = false;

            return instance;
        }

        ///<sumary>Resets the instance Position, Rotation and Scale</summary>
        public static void ResetPTweenInstance(PTweenInstance instance)
        {

            Vector3 resetVector = new Vector3();
            float resetFloat = 0;

            //RESET POSITION
            if (instance.Position.Animate)
            {

                if (instance.AnimationDirection == PTweenAnimationDirection.ANIMATE_FORWARD)
                    resetVector = instance.Position.From;
                else
                    resetVector = instance.Position.To;

                instance.Position.CurrenTime = (instance.AnimationDirection == PTweenAnimationDirection.ANIMATE_FORWARD) ? 0 : 1;
                instance.Target.transform.localPosition = resetVector;
            }
            //RESET SCALE
            if (instance.Scale.Animate)
            {

                if (instance.AnimationDirection == PTweenAnimationDirection.ANIMATE_FORWARD)
                    resetVector = instance.Scale.From;
                else
                    resetVector = instance.Scale.To;

                instance.Scale.CurrenTime = (instance.AnimationDirection == PTweenAnimationDirection.ANIMATE_FORWARD) ? 0 : 1;
                instance.Target.transform.localScale = resetVector;
            }
            //RESET ROTATION
            if (instance.Rotation.Animate)
            {

                if (instance.AnimationDirection == PTweenAnimationDirection.ANIMATE_FORWARD)
                    resetVector = instance.Rotation.From;
                else
                    resetVector = instance.Rotation.To;

                instance.Rotation.CurrenTime = (instance.AnimationDirection == PTweenAnimationDirection.ANIMATE_FORWARD) ? 0 : 1;
                instance.Target.transform.rotation = Quaternion.Euler(resetVector);
            }

            //RESET ALPHA
            if (instance.Alpha.Animate)
            {
                if (instance.AnimationDirection == PTweenAnimationDirection.ANIMATE_FORWARD)
                    resetFloat = instance.Alpha.From;
                else
                    resetFloat = instance.Alpha.To;

                instance.Alpha.CurrenTime = (instance.AnimationDirection == PTweenAnimationDirection.ANIMATE_FORWARD) ? 0 : 1;
                instance.Alpha.CanvasGroup.alpha = resetFloat;
            }
        }

        ///<summary>Updates the given clip in given direction</summary>
        public static Vector3 UpdatePTweenClipVector(PTweenTransformClip clip, PTweenAnimationDirection animationDirection)
        {

            if (clip.Delay > 0 && clip.CurrentDelayTime < 1)
            {
                clip.CurrentDelayTime += Time.deltaTime / clip.Delay;
            }
            else
            {
                switch (animationDirection)
                {
                    case PTweenAnimationDirection.ANIMATE_FORWARD:
                        if (clip.CurrenTime < 1)
                        {
                            clip.CurrenTime += Time.deltaTime / clip.Duration;

                        }
                        break;
                    case PTweenAnimationDirection.ANIMATE_BACKWARD:
                        if (clip.CurrenTime > 0)
                        {
                            clip.CurrenTime -= Time.deltaTime / clip.Duration;

                        }
                        break;
                }
            }

            var curveValue = clip.Curve.Evaluate(clip.CurrenTime);
            Vector3 newVector = Vector3.LerpUnclamped(clip.From, clip.To, curveValue);

            return newVector;
        }

        ///<summary>Updates the given clip in given direction</summary>
        public static float UpdatePTweenClipAlpha(PtweenAlphaClip clip, PTweenAnimationDirection animationDirection)
        {
            if (clip.Delay > 0 && clip.CurrentDelayTime < 1)
            {
                clip.CurrentDelayTime += Time.deltaTime / clip.Delay;
            }
            else
            {
                switch (animationDirection)
                {
                    case PTweenAnimationDirection.ANIMATE_FORWARD:
                        if (clip.CurrenTime < 1)
                        {
                            clip.CurrenTime += Time.deltaTime / clip.Duration;

                        }
                        break;
                    case PTweenAnimationDirection.ANIMATE_BACKWARD:
                        if (clip.CurrenTime > 0)
                        {
                            clip.CurrenTime -= Time.deltaTime / clip.Duration;

                        }
                        break;
                }
            }

            var curveValue = clip.Curve.Evaluate(clip.CurrenTime);
            float newValue = Mathf.LerpUnclamped(clip.From, clip.To, curveValue);

            return newValue;
        }

        ///<summary>If the position, rotation and scale finhised animating</summary>
        public static bool IsPTweenInstanceFinished(PTweenInstance instance)
        {
            bool finished = true;

            switch (instance.AnimationDirection)
            {
                case PTweenAnimationDirection.ANIMATE_FORWARD:
                    if (instance.Position.Animate && instance.Position.CurrenTime < 1 ||
                        instance.Rotation.Animate && instance.Rotation.CurrenTime < 1 ||
                        instance.Scale.Animate && instance.Scale.CurrenTime < 1 ||
                        instance.Alpha.Animate && instance.Alpha.CurrenTime < 1)
                        finished = false;
                    break;
                case PTweenAnimationDirection.ANIMATE_BACKWARD:
                    if (instance.Position.Animate && instance.Position.CurrenTime > 0 ||
                        instance.Rotation.Animate && instance.Rotation.CurrenTime > 0 ||
                        instance.Scale.Animate && instance.Scale.CurrenTime > 0 ||
                        instance.Alpha.Animate && instance.Alpha.CurrenTime > 0)
                        finished = false;
                    break;
            }

            return finished;
        }
    }
}