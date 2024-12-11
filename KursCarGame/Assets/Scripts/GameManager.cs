using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject finishPanel;

    public static GameManager instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void bastanBasla()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
