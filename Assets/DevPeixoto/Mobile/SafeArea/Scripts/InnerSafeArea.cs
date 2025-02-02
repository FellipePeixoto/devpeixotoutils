using UnityEngine;

namespace DevPeixoto.Mobile.SafeArea
{
    [RequireComponent(typeof(RectTransform))]
    public class InnerSafeArea : MonoBehaviour
    {
        public RectTransform RectTransform
        {
            get
            {
                return GetComponent<RectTransform>();
            }
        }

        [SerializeField]
        OutterSafeAreaFiller _topFiller;

        [SerializeField]
        OutterSafeAreaFiller _bottomFiller;

        public void CalculateFillTop()
        {
            if (_topFiller != null) _topFiller.CalculateFillArea(true, RectTransform);
        }

        public void CalculateFillBottom()
        {
            if (_bottomFiller != null) _bottomFiller?.CalculateFillArea(false, RectTransform);
        }

        private void OnValidate()
        {
            var rootHelper = transform.root.GetComponent<CanvasHelper>();
            if (rootHelper == null)
            {
                Debug.LogError($"{nameof(CanvasHelper)} component not Found! {nameof(InnerSafeArea)} require a Root element {nameof(CanvasHelper)} attached.");
            }
        }
    }
}
