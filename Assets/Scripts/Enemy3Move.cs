using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Move : EnemyMove
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
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
}
