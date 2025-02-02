using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DevPeixoto.Mobile.Screens
{
    public abstract class Screen : MonoBehaviour, IScreen
    {
        [SerializeField] protected Button buttonSubmit;
        [SerializeField] protected Button buttonBack;

        [SerializeField] UnityEvent OnScreenSetupEvnt;
        [SerializeField] UnityEvent OnScreenResumeEvnt;
        [SerializeField] UnityEvent OnScreenCloseEvnt;

        protected bool screenInitialized = false;
        protected ScreensManager screenManager;
        protected IDisposable _disposables;

        public void Awake() { }

        void OnEnable()
        {
            if (!screenInitialized)
            {
                return;
            }
            ResumeScreen();
        }

        private void OnDisable()
        {
            OnScreenClose();
        }

        void Start()
        {
            screenManager = FindAnyObjectByType<ScreensManager>(FindObjectsInactive.Include);
            screenInitialized = true;

            buttonSubmit?.onClick.AddListener(Submit);
            buttonBack?.onClick.AddListener(Back);

            SetupScreen();
            ResumeScreen();
        }

        protected virtual void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Back();
            }
        }

        public virtual void SetupScreen()
        {
            OnScreenSetupEvnt?.Invoke();
        }

        public virtual void ResumeScreen()
        {
            OnScreenResumeEvnt?.Invoke();
        }

        public void OnPauseScreen() { }

        public virtual void OnScreenClose()
        {
            OnScreenCloseEvnt?.Invoke();
        }

        public void ResetData() { }

        public void Submit() { }

        public virtual void Back()
        {
            screenManager.SwitchToLastOne();
        }

        private void OnApplicationFocus(bool focus)
        {
            if (focus)
            {
                ResumeScreen();
            }
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                OnPauseScreen();
                return;
            }
        }

        private void OnDestroy()
        {
            transform.GetComponentInChildren<Button>(true)?.onClick.RemoveListener(Submit);
            transform.GetComponentInChildren<Button>(true)?.onClick.RemoveListener(Back);

            _disposables?.Dispose();
        }

        private void OnValidate()
        {
            var requiredComp = transform.root.GetComponent<ScreensManager>();
            if (requiredComp == null)
            {
                Debug.LogError($"{nameof(ScreensManager)} component not Found! {nameof(Screen)} require a Root element {nameof(ScreensManager)} attached.");
            }
        }
    }
}