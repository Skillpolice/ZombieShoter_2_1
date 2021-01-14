using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    [Header("Text")]
    public Text textTimer;
    public Text playerMoneyText;

    public int resGame;
    public int scoreMoney;

    public void Awake()
    {
        GameManager[] gameManagers = FindObjectsOfType<GameManager>();
        for (int i = 0; i < gameManagers.Length; i++)
        {
            if (gameManagers[i].gameObject != gameObject)
            {
                Destroy(gameObject);
                gameObject.SetActive(false);
                break;
            }
        }
    }

    private void Start()
    {
        //playerMoneyText.text = "Money: 000";
        //DontDestroyOnLoad(gameObject);
    }

    public void AddMoney(int money)
    {
        scoreMoney += money;
        //playerMoneyText.text = "Money: " + scoreMoney.ToString();
    }

    public void RestartGame()
    {
        gameOverPanel.SetActive(true);
        StartCoroutine(RestartLevel(resGame));
    }

    IEnumerator RestartLevel(int res)
    {
        yield return new WaitForSeconds(1f);
        textTimer.text = "3";
        yield return new WaitForSeconds(1f);
        textTimer.text = "2";
        yield return new WaitForSeconds(1f);
        textTimer.text = "1";


        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }

}
