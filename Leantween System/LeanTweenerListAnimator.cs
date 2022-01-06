using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utils.Tweens
{
    public class LeanTweenerListAnimator : MonoBehaviour
    {
        [Header("Animation Config:")]
        [SerializeField] private LeanTweenType easeInType = LeanTweenType.easeOutBack;
        [SerializeField] private LeanTweenType easeOutType = LeanTweenType.easeInBack;

        [SerializeField] private float delayOfAnim = 0.05f;
        [SerializeField] private float durationOfAnim = 0.3f;
        [SerializeField] private float delayOfNextCard = 0.05f;
        
        [Header("In Animation Callback:")]
        [SerializeField] private UnityEvent onInAnimationComplete;
        [Header("Out Animation Callback:")]
        [SerializeField] private UnityEvent onOutAnimationComplete;
        
        [Header("Targets to animate:")]
        [SerializeField] private List<GameObject> transformChildrenToAnimate  = new List<GameObject>();

        private bool HasTargetsToAnimate => transformChildrenToAnimate.Count > 0;
        
        private void OnValidate()
        {
            transformChildrenToAnimate = GetChildren();
        }

        private void OnEnable()
        {
            AnimateListIn();
        }

        private List<GameObject>GetChildren()
        {
            var allChildren = new List<GameObject>();
            
            var children = transform.childCount;
            for (var i = 0; i < children; ++i)
                allChildren.Add(transform.GetChild(i).gameObject);

            return allChildren;
        }

        public void AnimateListIn()
        {
            if (!HasTargetsToAnimate) return;
            
            for (var i = 0; i < transformChildrenToAnimate.Count; i++)
                AnimateCardIn(transformChildrenToAnimate[i], delayOfNextCard * i, easeInType, 
                    i == transformChildrenToAnimate.Count -1);
        }

        public void AnimateListOut()
        {            
            if (!HasTargetsToAnimate) return;
            
            for (var i = transformChildrenToAnimate.Count - 1; i >= 0; i--)
                AnimateCardOut(transformChildrenToAnimate[i], delayOfNextCard * i, easeInType, i == 0);
        }

        private void AnimateCardIn(GameObject currentCard, float animationDelay, LeanTweenType typeAnim, bool attachOnComplete)
        {
            CancelTween(currentCard);
            currentCard.transform.localScale = Vector3.zero;

            if (!attachOnComplete)
            {
                LeanTween.scale(currentCard, Vector3.one, durationOfAnim)
                    .setDelay(delayOfAnim + animationDelay)
                    .setEase(typeAnim);
                return;
            }
            
            LeanTween.scale(currentCard, Vector3.one, durationOfAnim)
                .setDelay(delayOfAnim + animationDelay)
                .setEase(typeAnim).setOnComplete(() => onInAnimationComplete?.Invoke());
        }
        
        private void AnimateCardOut(GameObject currentCard, float animationDelay, LeanTweenType typeAnim,  bool attachOnComplete)
        {
            CancelTween(currentCard);

            if (!attachOnComplete)
            {
                LeanTween.scale(currentCard, Vector3.zero, durationOfAnim)
                    .setDelay(delayOfAnim + animationDelay)
                    .setEase(typeAnim);
                return;
            }

            LeanTween.scale(currentCard, Vector3.zero, durationOfAnim)
                .setDelay(delayOfAnim + animationDelay)
                .setEase(typeAnim).setOnComplete(() => onOutAnimationComplete?.Invoke());
        }

        private void CancelTween(GameObject gameObject)
        {
            if (LeanTween.isTweening(gameObject))
                LeanTween.cancel(gameObject);
        }
    }
}