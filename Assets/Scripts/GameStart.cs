using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    private FadeIn fade = null;

    private void Start()
    {
        fade = FindObjectOfType<FadeIn>();
    }
    public void LoadGame()
    {
        StartCoroutine(Go());
    }
    private IEnumerator Go()
    {
        StartCoroutine(fade.FadeOut());
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainScene");
    }
}
