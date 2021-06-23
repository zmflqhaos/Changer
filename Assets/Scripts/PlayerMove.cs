using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private Transform bulletPosition = null;
    [SerializeField]
    private GameObject bulletPrefab = null;
    [SerializeField]
    private float speed = 0.1f;
    [SerializeField]
    private float delay = 0.1f;
    [SerializeField]
    private bool isDamaged = false;

    private Vector2 targetPosition = Vector2.zero;
    public bool active = true;
    private GameManager gameManager = null;
    private LifeManager lifeManager = null;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        lifeManager = FindObjectOfType<LifeManager>();
        StartCoroutine(Fire());
    }

    void Update()
    {
        if (Input.GetMouseButton(0) == true)
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.x = Mathf.Clamp(targetPosition.x, gameManager.minPosition.x, gameManager.maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, gameManager.minPosition.y, gameManager.maxPosition.y);
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, targetPosition, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (isDamaged) return;
            StartCoroutine(Damaged(collision));
        }
    }

    private IEnumerator Damaged(Collider2D collision)
    {
        if (!isDamaged)
        {
            isDamaged = true;
            lifeManager.Dead();
            lifeManager.UpdateUI();
            for (int i = 0; i < 3; i++)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                yield return new WaitForSeconds(0.15f);
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                yield return new WaitForSeconds(0.15f);
            }
            isDamaged = false;
        }
    }

    private IEnumerator Fire()
    {
        yield return new WaitForSeconds(3f);
        while (true)
        {
            InstantiateOrSpawn();
            yield return new WaitForSeconds(delay);
        }
    }

    private void InstantiateOrSpawn()
    {
        GameObject bullet = null;
        if (gameManager.poolManager.transform.childCount > 0)
        {
            bullet = gameManager.poolManager.transform.GetChild(0).gameObject;
            bullet.SetActive(true);
        }
        else
        {
            bullet = Instantiate(bulletPrefab, bulletPosition);
        }
        bullet.transform.rotation = Quaternion.identity;
        bullet.transform.SetParent(null);
        bullet.transform.position = bulletPosition.position;
    }

}
