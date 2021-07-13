using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed = 10f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        CheckLimit();
    }

    private void CheckLimit()
    {
        if (transform.position.y > GameManager.Instance.maxPosition.y + 2.5f)
        {
            Despawn();
        }
        if (transform.position.y < GameManager.Instance.minPosition.y - 2.5f)
        {
            Despawn();
        }
        if (transform.position.x > GameManager.Instance.maxPosition.x + 2f)
        {
            Despawn();
        }
        if (transform.position.x < GameManager.Instance.minPosition.x - 2f)
        {
            Despawn();
        }
    }

    public void Despawn()
    {
        if(gameObject.CompareTag("Bullet"))
        {
            transform.SetParent(GameManager.Instance.poolManager.transform, false);
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

