using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        // public void PlayGame()
        // {
        //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // }
        public void PlayGame(int levelIndex)
        {
            SceneManager.LoadScene(levelIndex);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    
    }
}