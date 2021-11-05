using System.Collections.Generic;
using TweenerSystem.Data;
using UnityEngine;

namespace TweenerSystem
{
    public class LeanTweenerUISlideAnimation : MonoBehaviour
    {
        [Header("Slide In Config")] 
        [SerializeField] private TweenSlideAnimationData slideAnimationData;
        [SerializeField] private TweenSlideAnimationData slideOutData;

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

        private void SlideIn()
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