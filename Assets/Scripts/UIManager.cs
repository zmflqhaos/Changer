using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text stagetext = null;
    public int stage = 0;
    public bool boss = false;
    private Collider2D col = null;

    private GameManager gameManager = null;
    private BossMove bossMove = null;
    private void Start()
    {
        UpdateUI();
        StartCoroutine(StageUp());
        gameManager = FindObjectOfType<GameManager>();
        col = FindObjectOfType<Collider2D>();
        bossMove = FindObjectOfType<BossMove>();
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
    public void UpdateUI()
    {        
        stagetext.text = string.Format("LEVEL\n{0}", stage);
        if (stage >= 99&&boss==false)
        {
            boss = true;
            col.enabled = false;
            StartCoroutine(bossMove.BossOn());
        }
    }

    private void Checknumber(GameObject num)
    {
        if(stage>=9&&num.name.Contains("9"))
        {
            stage = 99;
            UpdateUI();           
        }
        if(!boss)
        {
            if (num.name.Contains("0"))
            {
                stage = 0;
                UpdateUI();
            }
            else if (num.name.Contains("1"))
            {
                stage = 1;
                UpdateUI();
            }
            else if (num.name.Contains("2"))
            {
                stage = 2;
                UpdateUI();
            }
            else if (num.name.Contains("3"))
            {
                stage = 3;
                UpdateUI();
            }
            else if (num.name.Contains("4"))
            {
                stage = 4;
                UpdateUI();
            }
            else if (num.name.Contains("5"))
            {
                stage = 5;
                UpdateUI();
            }
            else if (num.name.Contains("6"))
            {
                stage = 6;
                UpdateUI();
            }
            else if (num.name.Contains("7"))
            {
                stage = 7;
                UpdateUI();
            }
            else if (num.name.Contains("8"))
            {
                stage = 8;
                UpdateUI();
            }
            else if (num.name.Contains("9"))
            {
                stage = 9;
                UpdateUI();
            }
        }        
    }
    private IEnumerator StageUp()
    {
        while(true)
        {
            if(boss==false)
            {
                yield return new WaitForSeconds(10f);
                stage++;
                UpdateUI();
            }
            yield return null;
        }
    }
}
