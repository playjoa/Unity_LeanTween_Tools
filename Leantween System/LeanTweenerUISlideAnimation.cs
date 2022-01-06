using System.Collections.Generic;
using UnityEngine;
using Utils.Tweens.Data;

namespace Utils.Tweens
{
    
    public class LeanTweenerUISlideAnimation : MonoBehaviour
    {
        [Header("Slide In Config")] 
        [SerializeField] private TweenSlideAnimationData slideAnimationData = new TweenSlideAnimationData(LeanTweenType.easeOutBack);
        [SerializeField] private TweenSlideAnimationData slideOutData = new TweenSlideAnimationData(LeanTweenType.easeInBack);

        [HideInInspector] 
        [SerializeField] private RectTransform objectRect;
        
        private Dictionary<SlideDirection, Vector3> targetDirections = new Dictionary<SlideDirection, Vector3>();

        private void OnValidate()
        {
            objectRect = GetComponent<RectTransform>();
        }

        private void Awake()
        {
            GetDirectionsDictionary();
        }

        private void GetDirectionsDictionary()
        {
            var topScreen = Screen.currentResolution.height;
            var screenWidth = Screen.currentResolution.width;
            var rect = objectRect.rect;
            var objectWidth = rect.width;
            var objectHeight = rect.height;

            targetDirections.Add(SlideDirection.Down, new Vector3(0, - topScreen + objectHeight / 2f, 0));
            targetDirections.Add(SlideDirection.Up, new Vector3(0, topScreen + objectHeight / 2f, 0));
            targetDirections.Add(SlideDirection.Left, new Vector3(- screenWidth + objectWidth / 2f, 0, 0));
            targetDirections.Add(SlideDirection.Right, new Vector3(screenWidth + objectWidth / 2f, 0, 0));
        }

        private void OnEnable()
        {
            SlideIn();
        }

        public void SlideIn()
        {
            objectRect.localPosition = targetDirections[slideAnimationData.Direction];
            
            LeanTween.moveLocal(gameObject, slideAnimationData.Target, slideAnimationData.Duration)
                .setEase(slideAnimationData.EaseType)
                .setOnComplete(() => slideAnimationData.SlideComplete?.Invoke());
        }

        public void SlideOut()
        {
            LeanTween.moveLocal(gameObject, targetDirections[slideOutData.Direction], slideOutData.Duration)
                .setEase(slideOutData.EaseType)
                .setOnComplete(() => slideOutData.SlideComplete?.Invoke());
        }
    }
}