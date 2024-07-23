using PixelCrew.Model;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew.UI.GameMenu
{
    public class GameMenuWindow : AnimatedWindow
    {
        private Action _closeAction;
        public void OnShowSettings()
        {
            var window = Resources.Load<GameObject>("UI/SettingsWindow");
            var canvas = FindObjectOfType<Canvas>();
            Instantiate(window, canvas.transform);
        }

        public void OnRestartLevel()
        {
            var scene = SceneManager.GetActiveScene();
            _closeAction = () => { SceneManager.LoadScene(scene.name); };
            
            var session = FindObjectOfType<GameSession>();
            Destroy(session.gameObject);           
            
            Close();
        }

        public void OnShowMainMenu()
        {
            var session = FindObjectOfType<GameSession>();
            Destroy(session.gameObject);

            SceneManager.LoadScene("MainMenu");
        }

        public void OnShowLocolize()
        {
            var window = Resources.Load<GameObject>("UI/LocalizationWindow");
            var canvas = FindObjectOfType<Canvas>();
            Instantiate(window, canvas.transform);
        }

        public override void OnCloseAnimationComplete()
        {
            base.OnCloseAnimationComplete();
            _closeAction?.Invoke();
        }
    }
}
