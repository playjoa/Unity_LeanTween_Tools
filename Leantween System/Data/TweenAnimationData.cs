using System;
using UnityEngine;
using UnityEngine.Events;

namespace Utils.Tweens.Data
{
    [Serializable]
    public class TweenAnimationData
    {
        [SerializeField] private LeanTweenType easeType = LeanTweenType.easeOutBack;
        [SerializeField] private float animDelay = 0f;
        [SerializeField] private float animDuration = 0.5f;
        [SerializeField] [Range(0f, 1.5f)] private float animationTarget = 1f;
        [SerializeField] private UnityEvent onAnimationComplete;
        
        public TweenAnimationData(LeanTweenType leanTweenType, float animationTarget = 1f, float animDuration = 0.5f)
        {
            easeType = leanTweenType;
            this.animationTarget = animationTarget;
            this.animDuration = animDuration;
        }
        
        public LeanTweenType EaseType => easeType;
        public float Delay => animDelay;
        public float Duration => animDuration;
        public float Target => animationTarget;
        public UnityEvent OnAnimationComplete => onAnimationComplete;
    }
}