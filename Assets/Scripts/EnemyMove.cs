using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    protected float speed = 10f;
    [SerializeField]
    protected int hp = 2;
    [SerializeField]
    private GameObject eBullet = null;

    protected bool isDamaged=false;
    protected GameManager gameManager = null;
    protected Collider2D col = null;
    protected SpriteRenderer spriteRenderer = null;
    private GameObject deathBullet = null;
    private NumPoolManager numPoolManager = null;
    
    protected virtual void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        numPoolManager = FindObjectOfType<NumPoolManager>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        CheckLimit();
    }

    protected virtual void CheckLimit()
    {
        if (transform.position.y < gameManager.minPosition.y - 2.5f)
        {
            Destroy(gameObject);
        }
        if (transform.position.x > gameManager.maxPosition.x + 2f)
        {
            Destroy(gameObject);
        }
        if (transform.position.x < gameManager.minPosition.x - 2f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Number"))
        {
            BulletMove bullet = collision.GetComponent<BulletMove>();
            if(bullet!=null)
            {
                bullet.Despawn();
            }
            if (isDamaged) return;
            isDamaged = true;
            hp--;
            StartCoroutine(Damaged());
            if(hp<=0)
            {
                AfterDeath();
                col.enabled = false;
                Destroy(gameObject);
            }
        }
    }
    private IEnumerator Damaged()
    {
        spriteRenderer.material.SetColor("_Color", new Color(1f, 1f, 1f, 0f));
        yield return new WaitForSeconds(0.05f);
        isDamaged = false;
        spriteRenderer.material.SetColor("_Color", new Color(0f, 0f, 0f, 0f));
    }

    private void AfterDeath()
    {
        
        if(gameObject.transform.Find("Boom"))
        {
            for (int i = 0; i < 8; i++)
            {
                deathBullet = Instantiate(eBullet, transform);
                deathBullet.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
                deathBullet.transform.Rotate(0, 0, i*45);
                deathBullet.transform.SetParent(null);
            }
        }
        if(gameObject.transform.Find("junk"))
        {
            numPoolManager.Numspawn(gameObject.transform);   
        }
        
    }
}

