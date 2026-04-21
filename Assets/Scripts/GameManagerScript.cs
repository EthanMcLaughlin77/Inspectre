using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameEndUI;

    [YarnCommand("end_game")]
    public void gameEnd()
    {
        Cursor.visible = true;
        gameEndUI.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
