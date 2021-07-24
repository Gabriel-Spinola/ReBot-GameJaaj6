using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonBullet : Bullet
{
    [HideInInspector] public Vector2 dir;
    [HideInInspector] public float xScale;

    protected override void Update()
    {
        base.Update();

        transform.Translate(dir * speed * Time.deltaTime);

        transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
    }
}
