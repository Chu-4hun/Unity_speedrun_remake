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
    public bool isUIScene;

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus && !isUIScene)
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
        if (!isUIScene)
            Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (isUIScene)return;
        slider.value = player.HP;
        Score.text = "Score: " + player.coins;
        PlayerNickname.text = "Name: " + GlobalVariables.Get<string>("name");
    }
}
