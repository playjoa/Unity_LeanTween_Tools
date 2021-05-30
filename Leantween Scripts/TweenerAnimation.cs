using UnityEngine;
using UnityEngine.Events;

public class TweenerAnimation : MonoBehaviour
{
    [SerializeField]
    private LeanTweenType openAnimType = LeanTweenType.easeOutBack;

    [SerializeField]
    private LeanTweenType closeAnimType = LeanTweenType.easeOutBack;

    [SerializeField]
    private float delayOfAnim = 0.05f, durationOfAnim = 0.3f;

    [SerializeField]
    private UnityEvent OnAnimationClose;

    private Vector3 initialSize;

    private void Awake()
    {
       GetInitialSize();
    }

    private void OnEnable()
    {
        OpenAnimation();
    }

    void OpenAnimation()
    {
        if (openAnimType == LeanTweenType.notUsed)
            return;

        transform.localScale = Vector3.zero;

        LeanTween.scale(gameObject, initialSize, durationOfAnim).setDelay(delayOfAnim).setEase(openAnimType);
    }

    public void CloseAnimation()
    {
        if (closeAnimType == LeanTweenType.notUsed)
            return;

        LeanTween.scale(gameObject, Vector3.zero, durationOfAnim).setEase(LeanTweenType.easeInOutQuad).setOnComplete(ToDoAfterCloseAnimation);
    }

    void ToDoAfterCloseAnimation()
    {
        ResetCardSize();
        OnAnimationClose?.Invoke();
    }

    void GetInitialSize() => initialSize = transform.localScale;
    void ResetCardSize() => transform.localScale = initialSize;
}