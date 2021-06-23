using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed = 10f;
    private GameManager gameManager = null;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        CheckLimit();
    }

    private void CheckLimit()
    {
        if (transform.position.y > gameManager.maxPosition.y + 2.5f)
        {
            Despawn();
        }
        if (transform.position.y < gameManager.minPosition.y - 2.5f)
        {
            Despawn();
        }
        if (transform.position.x > gameManager.maxPosition.x + 2f)
        {
            Despawn();
        }
        if (transform.position.x < gameManager.minPosition.x - 2f)
        {
            Despawn();
        }
    }

    public void Despawn()
    {
        if(gameObject.CompareTag("Bullet"))
        {
            transform.SetParent(gameManager.poolManager.transform, false);
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

