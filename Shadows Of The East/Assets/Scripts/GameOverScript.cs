using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public void GameOver()
    {
        gameObject.SetActive(true);
        transform.GetChild(0).GetComponent<Transform>().DOScale(1.5f, 10);
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
