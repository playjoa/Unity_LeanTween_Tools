using System;
using UnityEngine;
using UnityEngine.Events;

namespace TweenerSystem.Data
{
    [Serializable]
    public class TweenAnimationData
    {
        [SerializeField] private LeanTweenType easeType = LeanTweenType.easeOutBack;
        [SerializeField] private float animDelay = 0f;
        [SerializeField] private float animDuration = 0.5f;
        [SerializeField] private UnityEvent onAnimationComplete;

        public LeanTweenType EaseType => easeType;
        public float Delay => animDelay;
        public float Duration => animDuration;
        public UnityEvent OnAnimationComplete => onAnimationComplete;
    }
}