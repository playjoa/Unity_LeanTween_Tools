using UnityEngine;
using UnityEngine.EventSystems;
using Utils.Tweens.Data;
using Utils.UI;

namespace Utils.Tweens
{
    [RequireComponent(typeof(ButtonComponent))]
    public class ButtonTweenAnimations : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Header("Button Animations Config.")] 
        [SerializeField] private TweenAnimationData clickData = new TweenAnimationData(LeanTweenType.easeOutBack, 0.85f, 0.1f);
        [SerializeField] private TweenAnimationData releaseData = new TweenAnimationData(LeanTweenType.easeOutBack, animDuration: 0.1f);
        [SerializeField] private TweenAnimationData negativeData = new TweenAnimationData(LeanTweenType.easeInBack, 1.15f, 0.15f);

        private bool givingNegativeFeedback;
        
        public void ExecuteAnimation(TweenAnimationData animationData)
        {
            if (givingNegativeFeedback) return;
            if (animationData.EaseType == LeanTweenType.notUsed) return;

            if (LeanTween.isTweening(gameObject))
                LeanTween.cancel(gameObject);

            LeanTween.scale(gameObject, Vector3.one * animationData.Target, animationData.Duration)
                .setDelay(animationData.Delay).setEase(animationData.EaseType)
                .setOnComplete(() => animationData.OnAnimationComplete?.Invoke());
        }

        public void OnPointerDown(PointerEventData eventData) => ExecuteAnimation(clickData);
        public void OnPointerUp(PointerEventData eventData) => ExecuteAnimation(releaseData);

        public void NegativeFeedBack()
        {
            if (givingNegativeFeedback) return;
            
            if (LeanTween.isTweening(gameObject))
                LeanTween.cancel(gameObject);

            givingNegativeFeedback = true;
            var duration = negativeData.Duration;
            var easeType = negativeData.EaseType;
            var target = negativeData.Target;
            var onComplete = negativeData.OnAnimationComplete;

            LeanTween.scale(gameObject, Vector3.one * target, duration).setEase(easeType);
            LeanTween.scale(gameObject, Vector3.one, duration).setDelay(duration).setEase(easeType)
                .setOnComplete(() => 
                { 
                    givingNegativeFeedback = false;
                    onComplete?.Invoke(); 
                });
        }
    }
}