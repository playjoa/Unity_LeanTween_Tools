using UnityEngine;
using Utils.Tweens.Data;

namespace Utils.Tweens
{
    public class LeanTweenerAnimation : MonoBehaviour
    {
        [Header("Open Animation Config:")]
        [SerializeField] private TweenAnimationData openAnimationData = new TweenAnimationData(LeanTweenType.easeOutBack);

        [Header("Close Animation Config:")] 
        [SerializeField] private TweenAnimationData closeAnimationData = new TweenAnimationData(LeanTweenType.easeInBack);

        private Vector3 initialSize;

        private void Awake()
        {
            GetInitialSize();
        }

        private void OnEnable()
        {
            OpenAnimation();
        }

        public void OpenAnimation()
        {
            if (openAnimationData.EaseType == LeanTweenType.notUsed) return;
            
            transform.localScale = Vector3.zero;
            LeanTween.scale(gameObject, initialSize, openAnimationData.Duration)
                .setDelay(openAnimationData.Delay).setEase(openAnimationData.EaseType)
                .setOnComplete(() => openAnimationData.OnAnimationComplete?.Invoke());
        }

        public void CloseAnimation()
        {
            if (closeAnimationData.EaseType == LeanTweenType.notUsed) return;

            LeanTween.scale(gameObject, Vector3.zero, closeAnimationData.Duration)
                .setDelay(closeAnimationData.Delay).setEase(closeAnimationData.EaseType)
                .setOnComplete(ToDoAfterCloseAnimation);
        }

        private void ToDoAfterCloseAnimation()
        {
            ResetCardSize();
            closeAnimationData.OnAnimationComplete?.Invoke();
        }

        private void GetInitialSize() => initialSize = transform.localScale;
        private void ResetCardSize() => transform.localScale = initialSize;
    }
}