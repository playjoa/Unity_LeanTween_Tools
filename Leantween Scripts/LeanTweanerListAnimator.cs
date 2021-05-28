using UnityEngine;

public class LeanTweanerListAnimator : MonoBehaviour
{
    [SerializeField]
    private LeanTweenType typeAnim = LeanTweenType.easeOutBack;

    [SerializeField]
    private AnimationCurve animCurve;

    [SerializeField]
    private float delayOfAnim = 0.05f, durationOfAnim = 0.3f, delayOfNextCard = 0.05f;

    private void OnEnable()
    {
        AnimateList(GetChildsInObject());
    }

    Transform[] GetChildsInObject() 
    {
        Transform[] childs = new Transform[transform.childCount];

        for (int i = 0; i < childs.Length; i++)
            childs[i] = transform.GetChild(i);

        return childs;
    }

    void AnimateList(Transform[] crds)
    {
        if (crds == null)
            return;

        if (typeAnim == LeanTweenType.animationCurve)
        {
            for (int i = 0; i < crds.Length; i++)
                AnimateCard(crds[i].gameObject, delayOfNextCard * i, animCurve);

            return;
        }

        for (int i = 0; i < crds.Length; i++)
        {
            AnimateCard(crds[i].gameObject, delayOfNextCard * i, typeAnim);
        }
    }

    void AnimateCard(GameObject currentCard, float newDelayOfNextCard, LeanTweenType typeAnim)
    {
        if (LeanTween.isTweening(currentCard))
            LeanTween.cancel(currentCard);

        currentCard.transform.localScale = Vector3.zero;
        LeanTween.scale(currentCard, Vector3.one, durationOfAnim).setDelay(delayOfAnim + newDelayOfNextCard).setEase(typeAnim);
    }

    void AnimateCard(GameObject currentCard, float newDelayOfNextCard, AnimationCurve typeAnim)
    {
        if (LeanTween.isTweening(currentCard))
            LeanTween.cancel(currentCard);

        currentCard.transform.localScale = Vector3.zero;
        LeanTween.scale(currentCard, Vector3.one, durationOfAnim).setDelay(delayOfAnim + newDelayOfNextCard).setEase(typeAnim);
    }
}