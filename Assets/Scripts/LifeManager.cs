using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player = null;
    [SerializeField]
    private Text lifeText = null;
    [SerializeField]
    private int life = 9;
    private UIManager uIManager = null;
    private Vector3 playerPos;
    private FadeIn fadein = null;
    void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        fadein = FindObjectOfType<FadeIn>();
        playerPos = player.transform.position;
        UpdateUI();
    }

    public void Dead()
    {
        life--;
        if(life<=0)
        {
            StartCoroutine(GameOver());
        }
    }

    public void UpdateUI()
    {
        lifeText.text = string.Format("LIFE : {0}", life);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        NumberMove number = collision.GetComponent<NumberMove>();
        GameObject num = collision.gameObject;
        NumPoolManager numPoolManager = FindObjectOfType<NumPoolManager>();
        if (collision.gameObject.CompareTag("Number"))
        {
            if (number.bullet)
            {
                Checknumber(num);
                num.transform.position = numPoolManager.transform.position;
                number.backto();
            }
        }
    }

    private void Checknumber(GameObject num)
    {
        if (num.name.Contains("0"))
        {
            life = 0;
            UpdateUI();
            Dead();
        }
        else if (num.name.Contains("1"))
        {
            life = 1;
            UpdateUI();
        }
        else if (num.name.Contains("2"))
        {
            life = 2;
            UpdateUI();
        }
        else if (num.name.Contains("3"))
        {
            life = 3;
            UpdateUI();
        }
        else if (num.name.Contains("4"))
        {
            life = 4;
            UpdateUI();
        }
        else if (num.name.Contains("5"))
        {
            life = 5;
            UpdateUI();
        }
        else if (num.name.Contains("6"))
        {
            life = 6;
            UpdateUI();
        }
        else if (num.name.Contains("7"))
        {
            life = 7;
            UpdateUI();
        }
        else if (num.name.Contains("8"))
        {
            life = 8;
            UpdateUI();
        }
        else if (num.name.Contains("9"))
        {
            life = 9;
            UpdateUI();
        }
    }
    private IEnumerator GameOver()
    {
        player.SetActive(false);
        player.transform.position = playerPos;
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(fadein.FadeOut());
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameOver");
    }
}
