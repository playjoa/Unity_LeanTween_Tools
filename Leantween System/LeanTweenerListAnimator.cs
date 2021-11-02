using UnityEngine;

namespace TweenerSystem
{
    public class LeanTweenerListAnimator : MonoBehaviour
    {
        [SerializeField] private LeanTweenType typeAnim = LeanTweenType.easeOutBack;

        [SerializeField] private AnimationCurve animCurve;

        [SerializeField] private float delayOfAnim = 0.05f;
        [SerializeField] private float durationOfAnim = 0.3f;
        [SerializeField] private float delayOfNextCard = 0.05f;

        private void OnEnable()
        {
            AnimateList(GetTransformsChild());
        }

        private Transform[] GetTransformsChild()
        {
            var childs = new Transform[transform.childCount];

            for (var i = 0; i < childs.Length; i++)
                childs[i] = transform.GetChild(i);

            return childs;
        }

        private void AnimateList(Transform[] crds)
        {
            if (crds == null)
                return;

            if (typeAnim == LeanTweenType.animationCurve)
            {
                for (var i = 0; i < crds.Length; i++)
                    AnimateCard(crds[i].gameObject, delayOfNextCard * i, animCurve);

                return;
            }

            for (var i = 0; i < crds.Length; i++)
                AnimateCard(crds[i].gameObject, delayOfNextCard * i, typeAnim);
        }

        private void AnimateCard(GameObject currentCard, float newDelayOfNextCard, LeanTweenType typeAnim)
        {
            if (LeanTween.isTweening(currentCard))
                LeanTween.cancel(currentCard);

            currentCard.transform.localScale = Vector3.zero;
            LeanTween.scale(currentCard, Vector3.one, durationOfAnim)
                .setDelay(delayOfAnim + newDelayOfNextCard)
                .setEase(typeAnim);
        }

        private void AnimateCard(GameObject currentCard, float newDelayOfNextCard, AnimationCurve typeAnim)
        {
            if (LeanTween.isTweening(currentCard))
                LeanTween.cancel(currentCard);

            currentCard.transform.localScale = Vector3.zero;
            LeanTween.scale(currentCard, Vector3.one, durationOfAnim)
                .setDelay(delayOfAnim + newDelayOfNextCard)
                .setEase(typeAnim);
        }
    }
}