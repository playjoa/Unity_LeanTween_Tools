using UnityEngine;

public class LeanTweanerPopUp : MonoBehaviour
{
    [SerializeField]
    private LeanTweenType typePopUpAnim = LeanTweenType.easeOutBack, typeCloseAnim = LeanTweenType.easeInOutBack;

    [SerializeField]
    private float delayOfAnim = 0.05f, durationOfAnim = 0.3f, delayToClosePopUp = 3f;

    private Vector3 originalLocalScale;

    private void Awake()
    {
        SaveOriginalLocalScale();
    }

    private void OnEnable()
    {
        AnimatePopUp();
    }

    void SaveOriginalLocalScale()
    {
        originalLocalScale = transform.localScale;
    }

    void AnimatePopUp() 
    {
        SetSizeToZero();

        LeanTween.scale(gameObject, originalLocalScale, durationOfAnim).setDelay(delayOfAnim).setEase(typePopUpAnim);
        Invoke("CloseAnim", delayToClosePopUp);
    }

    void SetSizeToZero()
    {
        transform.localScale = Vector3.zero;
    }

    void CloseAnim()
    {
        LeanTween.scale(gameObject, Vector3.zero, durationOfAnim).setDelay(delayOfAnim).setEase(typeCloseAnim).setOnComplete(DeactivatePopUp);
    }

    void DeactivatePopUp() 
    {
        gameObject.SetActive(false);
    }
}