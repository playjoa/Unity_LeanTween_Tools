using UnityEngine;
using UnityEngine.Events;

namespace Utils.Tweens
{
    public class LeanTweenerPopUp : MonoBehaviour
    {
        [Header("Open PopUp Config:")]
        [SerializeField] private LeanTweenType typePopUpAnim = LeanTweenType.easeOutBack;
        [SerializeField] private float delayOfOpenAnim = 0.05f;
        [SerializeField] private float durationOfOpenAnim = 0.3f;
        [SerializeField] private UnityEvent OnPopUpOpened;
        
        [Header("Close PopUp Config:")]
        [SerializeField] private LeanTweenType typeCloseAnim = LeanTweenType.easeInOutBack;
        [SerializeField] private float delayToClosePopUp = 3f;
        [SerializeField] private UnityEvent OnPopUpClosed;

        private Vector3 originalLocalScale;

        private void Awake()
        {
            SaveOriginalLocalScale();
        }

        private void OnEnable()
        {
            AnimatePopUp();
        }

        private void SaveOriginalLocalScale()
        {
            originalLocalScale = transform.localScale;
        }

        private void AnimatePopUp()
        {
            SetSizeToZero();

            LeanTween.scale(gameObject, originalLocalScale, durationOfOpenAnim)
                .setDelay(delayOfOpenAnim).setEase(typePopUpAnim)
                .setOnComplete(HandlePopUpOpened);
            Invoke(nameof(CloseAnimation), delayToClosePopUp);
        }

        private void SetSizeToZero() => transform.localScale = Vector3.zero;

        private void CloseAnimation()
        {
            LeanTween.scale(gameObject, Vector3.zero, durationOfOpenAnim)
                .setDelay(delayOfOpenAnim).setEase(typeCloseAnim)
                .setOnComplete(HandlePopUpClosed);
        }

        private void HandlePopUpOpened()
        {
            OnPopUpOpened?.Invoke();
        }

        private void HandlePopUpClosed()
        {
            OnPopUpClosed?.Invoke();
            gameObject.SetActive(false);
        }
    }
}