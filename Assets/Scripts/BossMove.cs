using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossMove : EnemyMove
{
    [SerializeField]
    private GameObject eBullet1 = null;
    [SerializeField]
    private GameObject eBullet2 = null;
    [SerializeField]
    private GameObject eBullet3 = null;
    [SerializeField]
    private Transform bullet = null;
    [SerializeField]
    private GameObject senemy = null;

    private int pase = 1;
    private UIManager uIManager = null;
    private GameObject num = null;
    private FadeIn fadein = null;
    private IEnumerator pattern = null;
    private IEnumerator attack = null;
    protected override void Start()
    {
        pattern = Pattern();
        attack = Attack();
        uIManager = FindObjectOfType<UIManager>();
        fadein = FindObjectOfType<FadeIn>();
        num = GameObject.Find("NumPoolManager");
        base.Start();
        gameObject.SetActive(false);
    }

    protected override void Update()
    {
    }
    public IEnumerator BossOn()
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        StartCoroutine(pattern);
        StartCoroutine(attack);
        while(true)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
            yield return null;
            if(transform.position.y < 7.5f)
            {
                speed = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            BulletMove bullet = collision.GetComponent<BulletMove>();
            if (bullet != null)
            {
                bullet.Despawn();
            }
            if (isDamaged) return;
            //isDamaged = true;
            hp--;
            if(pase==1) StartCoroutine(Damaged());
            if (hp == 1&&pase==1)
            {
                hp = 100;
                pase = 2;
                StartCoroutine(CPase());
            }
            if(pase==2&&hp<=0)
            {
                col.enabled = false;
                speed = -3f; 
                spriteRenderer.material.SetColor("_Color", new Color(255f, 0f, 0f, 0f));
                StopCoroutine(pattern);
                StopCoroutine(attack);
                StartCoroutine(GameClear());
            }
        }
    }

    private IEnumerator GameClear()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(fadein.FadeOut());
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameClear");
    }
    private IEnumerator CPase()
    {
        for(int i=0; i<3; i++)
        {
            spriteRenderer.material.SetColor("_Color", new Color(0f, 0f, 0f, 0f));
            yield return new WaitForSeconds(0.5f);
            isDamaged = false;
            spriteRenderer.material.SetColor("_Color", new Color(1f, 0f, 0f, 0f));
            yield return new WaitForSeconds(0.5f);
        }
    }
    private IEnumerator Damaged()
    {
        spriteRenderer.material.SetColor("_Color", new Color(1f, 1f, 1f, 0f));
        yield return new WaitForSeconds(0.05f);
        isDamaged = false;
        spriteRenderer.material.SetColor("_Color", new Color(0f, 0f, 0f, 0f));
    }

    private IEnumerator Pattern()
    {
        yield return new WaitForSeconds(5f);
        int i = 0;
        while (true)
        {
            if (i == 0 && pase==2)
            {
                yield return new WaitForSeconds(3f);
                i++;
            }
            int rand = Random.Range(1, 5);
            switch(rand)
            {
                case 1:
                    {
                        StartCoroutine(Pattern1());
                        break;
                    }
                case 2:
                    {
                        StartCoroutine(Pattern2());
                        break;
                    }
                case 3:
                    {
                        StartCoroutine(Pattern3());
                        break;
                    }
                case 4:
                    {
                        if(pase==1)
                        {
                            StartCoroutine(Pattern4());                            
                        }
                        else
                        {
                            StartCoroutine(Pattern5());
                        }
                        break;
                    }
            }

            yield return new WaitForSeconds(Random.Range(1.3f, 2.5f)/pase);
        }
    }

    private IEnumerator Pattern1()
    {
        GameObject e;
        for (int i=0; i<8; i++)
        {
            e = Instantiate(eBullet2, bullet.transform);
            e.transform.Translate(-7+(i*2), 0, 0);
            e.transform.localScale = new Vector3(1f, 1f, 1f);
            e.transform.Rotate(0, 0, 0);
            e.transform.SetParent(null);
        }
        yield return new WaitForSeconds(0.15f);
        for (int i = 0; i < 8; i++)
        {
            e = Instantiate(eBullet2, bullet.transform);
            e.transform.Translate(-8 + (i * 2), 0, 0);
            e.transform.localScale = new Vector3(1f, 1f, 1f);
            e.transform.Rotate(0, 0, 0);
            e.transform.SetParent(null);
        }
    }
    private IEnumerator Pattern2()
    {
        GameObject e;
        for (int i = 0; i <= 7; i++)
        {
            e = Instantiate(eBullet1, bullet.transform);
            e.transform.Translate(-7 + (i*2f), 0, 0);
            e.transform.localScale = new Vector3(1f, 1f, 1f);
            e.transform.Rotate(0, 0, 0);
            e.transform.SetParent(null);
            yield return new WaitForSeconds(0.03f);
        }
        yield return new WaitForSeconds(0.15f);
        for (int i = 0; i <= 7; i++)
        {
            e = Instantiate(eBullet1, bullet.transform);
            e.transform.Translate(7 - (i * 2f), 0, 0);
            e.transform.localScale = new Vector3(1f, 1f, 1f);
            e.transform.Rotate(0, 0, 0);
            e.transform.SetParent(null);
            yield return new WaitForSeconds(0.03f);
        }

    }
    private IEnumerator Pattern3()
    {
        GameObject e;

        for (int j = 0; j < 20; j++)
        {
            e = Instantiate(eBullet3, bullet.transform);
            e.transform.localScale = new Vector3(1f, 1f, 1f);
            e.transform.Rotate(0, 0, -90 + (j * 9));
            e.transform.SetParent(null);
        }
        yield return new WaitForSeconds(0);
    }
    private IEnumerator Pattern4()
    {
        for (int i = 0; i <= 3; i++)
        {
            GameObject enemy = null;
            enemy = Instantiate(senemy, new Vector2(Random.Range(-6f, 6f), bullet.position.y), Quaternion.identity);
            enemy.transform.SetParent(null);
            yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        };
    }
    private IEnumerator Pattern5()
    {
        GameObject e;
        for (int i = 0; i < 10; i++)
        {
            e = Instantiate(eBullet3, bullet.transform);
            e.transform.Translate(-5.5f, 0, 0);
            e.transform.localScale = new Vector3(1f, 1f, 1f);
            e.transform.Rotate(0, 0, 10-(i*9));
            e.transform.SetParent(null);

            e = Instantiate(eBullet3, bullet.transform);
            e.transform.Translate(5.5f, 0, 0);
            e.transform.localScale = new Vector3(1f, 1f, 1f);
            e.transform.Rotate(0, 0, -10+(i*9));
            e.transform.SetParent(null);
        }
        yield return new WaitForSeconds(0.1f);
    }
    private IEnumerator Attack()
    {
        GameObject e;
        while(true)
        {
            e = Instantiate(eBullet1, bullet.transform);
            e.transform.localScale = new Vector3(4f, 4f, 1f);
            e.transform.Rotate(0, 0, 0);
            e.transform.SetParent(null);
            yield return new WaitForSeconds(0.8f);
        }
       
    }
}
