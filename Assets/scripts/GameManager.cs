using Character;
using UnityEngine;
using UnityEngine.UI;
using Cursor = UnityEngine.Cursor;

public class GameManager : MonoBehaviour
{
    public Slider slider;
    public Text Score;
    public Text PlayerNickname;
    public PlayerMovement player;

    private void OnApplicationFocus(bool hasFocus)
    {
        Cursor.lockState = hasFocus ? CursorLockMode.Locked : CursorLockMode.None;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        slider.value = player.HP;
        Score.text = "Score: " + player.coins;
        PlayerNickname.text = "Name: " + GlobalVariables.Get<string>("name");
    }
}
