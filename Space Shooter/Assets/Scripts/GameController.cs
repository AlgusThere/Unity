using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject hazard;
    public int spawnCount;
    public float spawnWait;
    public float startSpawn;
    public float waveWait;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI quitText;
    public int score;

    private bool gameOver;
    private bool restart;

    void Update()
    {
        if(restart == true)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
            Debug.Log("Oyun Kapand�");
        }
    }

    IEnumerator SpawnValues()
    {
        yield return new WaitForSeconds(startSpawn);

        while (true)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-3, 3), 0, 9);
                Quaternion spawnRotation = Quaternion.identity;

                Instantiate(hazard, spawnPosition, spawnRotation);
                //Coroutine
                //1.IEnumerator d�nd�rmek zorundad�r.
                //2.En az 1 adet yield ifadesi bulunmak zorundad�r.
                //3.Coroutineler �a��r�l�rken StartCoroutine metoduyla �a��r�lmal�d�r.
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if(gameOver == true)
            {
                restartText.text = "Press 'R' for Restart";
                quitText.text = "Press 'Q' for Quit";
                restart = true;
                break;
            }
        }
    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        gameOver = true;
    }
    void Start()
    {
        gameOverText.text = "";
        restartText.text = "";
        quitText.text = "";
        gameOver = false;
        restart = false;
        StartCoroutine(SpawnValues());
    }

}
