using Character;
using UnityEngine;
using UnityEngine.UI;
using Cursor = UnityEngine.Cursor;

public class GameManager : MonoBehaviour
{
    public Slider slider;
    public Text Score;
    public PlayerMovement player;

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void Start()
    {
    }

    private void Update()
    {
        slider.value = player.HP;
        Score.text = "Score: " + player.coins;
    }
}
