using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DevPeixoto.Mobile.Screens
{
    [RequireComponent(typeof(Canvas))]
    public class ScreensManager : MonoBehaviour
    {
        [SerializeField] Screen[] m_screens;

        List<GameObject> m_screensStack;

        private void Awake()
        {
            if (m_screens.Length == 0)
            {
                Debug.LogWarning("No screens to manage");
                return;
            }

            m_screensStack = new List<GameObject>(m_screens.Length);

            for (int i = 1; i < m_screens.Length; i++)
            {
                m_screens[i].gameObject.SetActive(false);
            }

            m_screens[0].gameObject.SetActive(true);
            m_screensStack.Add(m_screens[0].gameObject);
        }

        public void SwitchTo(GameObject screen)
        {
            if (m_screensStack == null || screen == null || screen == m_screensStack.Last())
            {
                return;
            }
            else if (m_screensStack.Count > 1 && screen == m_screensStack[m_screensStack.Count - 2])
            {
                SwitchToLastOne();
                return;
            }
            else if (m_screensStack.Contains(screen))
            {
                UnstackScreensUnitlTo(screen);
            }
            else
            {
                m_screensStack.Last().SetActive(false);
                m_screensStack.Add(screen);
                m_screensStack.Last().SetActive(true);
            }
        }

        public void SwitchCleanTo(Screen view)
        {
            view.ResetData();
            SwitchTo(view.gameObject);
        }

        public void SwitchToLastOne()
        {
            if (m_screensStack.Count <= 1) return;

            m_screensStack[m_screensStack.Count - 1].SetActive(false);
            m_screensStack.RemoveAt(m_screensStack.Count - 1);
            m_screensStack[m_screensStack.Count - 1].SetActive(true);
        }

        void UnstackScreensUnitlTo(GameObject gameObject)
        {
            m_screensStack.Last().SetActive(false);
            do
            {
                m_screensStack.RemoveAt(m_screensStack.Count - 1);
            } while (gameObject != m_screensStack.Last());

            m_screensStack.Last().SetActive(true);
        }
    }
}