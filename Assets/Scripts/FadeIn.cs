using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    [SerializeField]
    private Image image = null;
    private float time = 0f;
    private float F_time = 1f;

    void Start()
    {
        image.gameObject.SetActive(true);
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        Color alpha = image.color;
        while(alpha.a>0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            image.color = alpha;
            yield return null;
        }
        image.gameObject.SetActive(false);
    }

    public IEnumerator FadeOut()
    {
        image.gameObject.SetActive(true);
        time = 0;
        Color alpha = image.color;
        while (alpha.a < 1f)
        {           
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            image.color = alpha;
            yield return null;
        }
    }
}
