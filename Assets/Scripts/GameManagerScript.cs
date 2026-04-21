using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameEndUI;

    [YarnCommand("end_game")]
    public void gameEnd()
    {
        gameEndUI.SetActive(true);
    }
}
