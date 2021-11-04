using System;
using UnityEngine;
using UnityEngine.Events;

namespace TweenerSystem.Data
{
    [Serializable]
    public class TweenSlideAnimationData
    {
        [SerializeField] private SlideDirection slideDirection = SlideDirection.Down;
        [SerializeField] private LeanTweenType easeType = LeanTweenType.easeOutBack;
        [SerializeField] private float slideDelay = 0f;
        [SerializeField] private float slideDuration = 0.5f;
        [SerializeField] private Vector3 slideRectTransformTarget = Vector3.zero;
        [SerializeField] private UnityEvent onSlideComplete;

        public TweenSlideAnimationData(SlideDirection slideDirection)
        {
            this.slideDirection = slideDirection;
        }
        
        public SlideDirection Direction => slideDirection;
        public LeanTweenType EaseType => easeType;
        public float Delay => slideDelay;
        public float Duration => slideDuration;
        public Vector3 Target => slideRectTransformTarget;
        public UnityEvent SlideComplete => onSlideComplete;
    }
    
    public enum SlideDirection
    {
        Up,
        Down,
        Left,
        Right
    }
}