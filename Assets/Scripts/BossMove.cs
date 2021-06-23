using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : EnemyMove
{
    private UIManager uIManager = null;
    protected override void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        base.Start();
        gameObject.SetActive(false);
    }

    protected override void Update()
    {
    }
    public IEnumerator BossOn()
    {
        gameObject.SetActive(true);
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
}
