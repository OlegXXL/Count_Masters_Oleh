using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamesManager : MonoBehaviour
{
    [SerializeField] private Button tapToStart_btn;
    [SerializeField] private Text money_txt;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private GameObject loseGameUI;
    [SerializeField] private GameObject winGameUI;
    [SerializeField] private Button restartGame_btn;
    [SerializeField] private GameObject CountPeopole_t;

    private void Awake()
    {
        money_txt.text = PlayerPrefs.GetInt("money", 0).ToString();
    }
    public void PressStart()
    {
        menuUI.SetActive(false);
        playerManager.onGameState();
        CountPeopole_t.SetActive(true);
    }
    public void LoseGame()
    {
        loseGameUI.SetActive(true);
    }
    public void ClickRestart()
    {
        SceneManager.LoadScene(0);
    }
}
