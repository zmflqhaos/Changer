using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberMove : MonoBehaviour
{
    private float speed = 10f;
    private GameManager gameManager = null;
    private NumPoolManager numPoolManager = null;
    public bool bullet = false;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        numPoolManager = FindObjectOfType<NumPoolManager>();
    }
    private void Update()
    {
        if(!transform.parent&&bullet == false)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
            CheckWall();
        }
        else if(!transform.parent&&bullet==true)
        {
            transform.Translate(Vector2.up * (speed*1.5f) * Time.deltaTime);
            CheckBreak();
        }
    }

    private void CheckWall()
    {
            if (transform.position.y < gameManager.minPosition.y - 1.5)
            {
                gameObject.transform.Rotate(0, 0, Random.Range(-90f, 90f));
                gameObject.transform.position += new Vector3(0, 0.5f, 0);
            }
            if (transform.position.y > gameManager.maxPosition.y + 0.5)
            {
                gameObject.transform.Rotate(0, 0, Random.Range(-90f, 90f));
                gameObject.transform.position += new Vector3(0, -0.5f, 0);
            }
            if (transform.position.x < gameManager.minPosition.x)
            {
                gameObject.transform.Rotate(0, 0, Random.Range(-90f, 90f));
                gameObject.transform.position += new Vector3(0.25f, 0, 0);
            }
            if (transform.position.x > gameManager.maxPosition.x)
            {
                gameObject.transform.Rotate(0, 0, Random.Range(-90f, 90f));
                gameObject.transform.position += new Vector3(-0.25f, 0, 0);
            }
    }
    private void CheckBreak()
    {
        if (transform.position.y < gameManager.minPosition.y-2f)
        {
            backto();
        }
        if (transform.position.y > gameManager.maxPosition.y+2f)
        {
            backto();
        }
        if (transform.position.x < gameManager.minPosition.x-2f)
        {
            backto();
        }
        if (transform.position.x > gameManager.maxPosition.x+2f)
        {
            backto();
        }
    }

    public void backto()
    {
        if (gameObject.CompareTag("Number"))
        {
            bullet = false;
            transform.SetParent(numPoolManager.transform, false);
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject bulletPosition = GameObject.Find("BulletPosition");
            if (bulletPosition.transform.childCount<=0)
            {
                Collider2D follow = this.gameObject.GetComponent<Collider2D>();
                this.transform.rotation = Quaternion.identity;
                this.transform.SetParent(bulletPosition.transform, false);
                this.transform.position = bulletPosition.transform.position;
                this.bullet = true;
            }   
        }
        if (this.transform.parent&&collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            this.bullet = false;
            backto();
        }
    }
}
