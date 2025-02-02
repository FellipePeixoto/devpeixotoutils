using UnityEngine;

namespace DevPeixoto.Mobile.SafeArea
{
    [RequireComponent(typeof(RectTransform))]
    public class OutterSafeAreaFiller : MonoBehaviour
    {
        [SerializeField]
        float _height = 0;

        RectTransform TargetRect
        {
            get
            {
                return GetComponent<RectTransform>();
            }
        }

        public void CalculateFillArea(bool fillTop, RectTransform _safeArea)
        {
            if (fillTop)
            {
                TargetRect.anchoredPosition = new Vector2(TargetRect.anchorMin.x, -_safeArea.offsetMax.y);
                TargetRect.sizeDelta = new Vector2(TargetRect.sizeDelta.x, _height - _safeArea.offsetMax.y);
            }
            else
            {
                TargetRect.anchoredPosition = new Vector2(TargetRect.anchorMin.x, -_safeArea.offsetMin.y);
                TargetRect.sizeDelta = new Vector2(TargetRect.sizeDelta.x, _height + _safeArea.offsetMin.y);
            }
        }

        private void OnValidate()
        {
            var rootHelper = transform.root.GetComponent<CanvasHelper>();
            if (rootHelper == null)
            {
                Debug.LogError($"{nameof(CanvasHelper)} component not Found! {nameof(OutterSafeAreaFiller)} require a Root element {nameof(CanvasHelper)} attached.");
            }
        }
    }
}