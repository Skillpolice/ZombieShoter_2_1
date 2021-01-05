using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Text textTimer;
    public int resGame;


    private void Awake()
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
