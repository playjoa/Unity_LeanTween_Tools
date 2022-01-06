using UnityEngine;

namespace Utils.Tweens
{
    public class LeanTweenPingPongMove : MonoBehaviour
    {
        [Header("Animation Configuration")]
        [SerializeField] private PingPongDirection pingPongDirection = PingPongDirection.UpDown;
        [SerializeField] private float distanceToPingPong = 35;
        [SerializeField] private float pingPongDuration = 0.75f;
        [SerializeField] private LeanTweenType easeType = LeanTweenType.easeInOutSine;
        [SerializeField] private bool activateOnEnable = true;

        private Vector3 localStartingPosition;

        private void Awake()
        {
            localStartingPosition = transform.localPosition;
        }

        private void OnEnable()
        {
            if (activateOnEnable)
                StartAnimation();
        }

        private void OnDisable()
        {
            StopAnimation();
        }

        private void ResetTransform()
        {
            switch (pingPongDirection)
            {
                case PingPongDirection.UpDown:
                    transform.localPosition = new Vector3(localStartingPosition.x,
                        localStartingPosition.y - distanceToPingPong / 2f, localStartingPosition.z);
                    break;
                case PingPongDirection.LeftRight:
                    transform.localPosition = new Vector3(localStartingPosition.x - distanceToPingPong / 2f,
                        localStartingPosition.y, localStartingPosition.z);
                    break;
                case PingPongDirection.FrontBack:
                    transform.localPosition = new Vector3(localStartingPosition.x,
                        localStartingPosition.y, localStartingPosition.z - distanceToPingPong / 2f);
                    break;
                default:
                    Debug.LogError("Ping Ping Type Not registered");
                    break;
            }
        }

        public void StartAnimation()
        {
            ResetAnimation();
            
            switch (pingPongDirection)
            {
                case PingPongDirection.UpDown:
                    LeanTween.moveLocalY(gameObject, transform.localPosition.y + distanceToPingPong, pingPongDuration)
                        .setLoopPingPong(-1).setEase(easeType);
                    break;
                case PingPongDirection.LeftRight:
                    LeanTween.moveLocalX(gameObject,transform.localPosition.x + distanceToPingPong ,pingPongDuration)
                        .setLoopPingPong(-1).setEase(easeType);
                    break;
                case PingPongDirection.FrontBack:
                    LeanTween.moveLocalZ(gameObject,transform.localPosition.z + distanceToPingPong ,pingPongDuration)
                        .setLoopPingPong(-1).setEase(easeType);
                    break;
                default:
                    Debug.LogError("Ping Ping Type Not registered");
                    break;
            }
        }

        private void ResetAnimation()
        {
            StopAnimation();
            ResetTransform();
        }

        public void StopAnimation()
        {
            if(LeanTween.isTweening(gameObject))
                LeanTween.cancel(gameObject);
        }
    }

    public enum PingPongDirection
    {
        UpDown,
        LeftRight,
        FrontBack
    }
}