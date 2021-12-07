using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GameOverManager : MonoBehaviour
    {
        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
        }

        public void RestartClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void MenuClick()
        {
            SceneManager.LoadScene(0);
        }
    }
}
