using System.Collections;
using UnityEngine;

namespace DevPeixoto.Mobile.SafeArea
{
    ///Really big Thanks @_Adriaan_1, for providing such a good tool https://adriaan.games
    [RequireComponent(typeof(Canvas))]
    [ExecuteInEditMode]
    public class CanvasHelper : MonoBehaviour
    {
        Canvas _canvas;
        InnerSafeArea[] _safeAreas = new InnerSafeArea[0];

        [ExecuteInEditMode]
        void Awake()
        {
            SetupHelperItems();
        }

        private void Start()
        {
            StartCoroutine(WaitForEndOfFrame());
        }

        IEnumerator WaitForEndOfFrame()
        {
            yield return new WaitForEndOfFrame();
            ApplySafeArea();
        }

        void SetupHelperItems()
        {
            _canvas = GetComponent<Canvas>();
            _safeAreas = GetComponentsInChildren<InnerSafeArea>();
        }

        [ExecuteInEditMode]
        void Update()
        {
#if UNITY_EDITOR
            SetupHelperItems();
            SafeAreaChanged();
#endif
        }

        void ApplySafeArea()
        {
            if (_safeAreas.Length <= 0)
                return;

            var safeArea = Screen.safeArea;

            var topOffset = (_canvas.pixelRect.height - Screen.safeArea.height) - Screen.safeArea.y;
            var bottomOffset = Screen.safeArea.y;

            foreach (var area in _safeAreas)
            {
                area.RectTransform.offsetMax = new Vector2(0, -topOffset);
                area.RectTransform.offsetMin = new Vector2(0, bottomOffset);

                area.CalculateFillTop();
                area.CalculateFillBottom();
            }
        }

        private void SafeAreaChanged()
        {
            ApplySafeArea();
        }
    }
}
