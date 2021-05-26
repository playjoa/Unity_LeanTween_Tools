using UnityEngine;

public class TweenerAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject targetObjectToDeactivate;

    [SerializeField]
    private LeanTweenType animType = LeanTweenType.easeOutBack;

    [SerializeField]
    private AnimationCurve animCurve;

    [SerializeField]
    private float delayOfAnim = 0.05f, durationOfAnim = 0.3f;

    private Vector3 initialSize;

    private void Awake()
    {
        GetInitialSize();
    }

    private void OnEnable()
    {
        Animate();
    }

    void Animate()
    {
        transform.localScale = Vector3.zero;

        if (animType == LeanTweenType.animationCurve)
        {
            LeanTween.scale(gameObject, initialSize, durationOfAnim).setDelay(delayOfAnim).setEase(animCurve);
            return;
        }

        LeanTween.scale(gameObject, initialSize, durationOfAnim).setDelay(delayOfAnim).setEase(animType);
    }

    public void CloseAnimation()
    {
        LeanTween.scale(gameObject, Vector3.zero, durationOfAnim).setEase(LeanTweenType.easeInOutQuad).setOnComplete(ToDoAfterCloseAnimation);
    }

    void ToDoAfterCloseAnimation()
    {
        DeactivateTargetObjectIfSet();
        ResetCardSize();
    }

    void GetInitialSize() => initialSize = transform.localScale;

    void ResetCardSize() => transform.localScale = initialSize;

    void DeactivateTargetObjectIfSet()
    {
        if (targetObjectToDeactivate != null)
            targetObjectToDeactivate.SetActive(false);
    }
}
