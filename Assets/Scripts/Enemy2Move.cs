using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Move : EnemyMove
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float bulletDelay = 0.5f;

    private Vector3 diff = Vector3.zero;
    private float rotationZ = 0f;

    private GameObject newBullet = null;
    private PlayerMove player = null;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<PlayerMove>();
        StartCoroutine(attack());
    }

    // Update is called once per frame
    protected override void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        CheckLimit();
    }
    protected override void CheckLimit()
    {
        base.CheckLimit();
    }



    private IEnumerator attack()
    {
        yield return new WaitForSeconds(bulletDelay);
        while(true)
        {
            if(gameObject.transform.position.y>player.transform.position.y)
            {
                newBullet = Instantiate(bulletPrefab, transform);

                diff = transform.position - player.transform.position;
                diff.Normalize();
                rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                newBullet.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + 90f);

                newBullet.transform.SetParent(null);
                yield return new WaitForSeconds(bulletDelay);
            }
            else
            yield return new WaitForSeconds(bulletDelay);
        }
        
    }
}
