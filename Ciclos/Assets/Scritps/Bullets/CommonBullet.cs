using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonBullet : Bullet
{
    [HideInInspector] public int dir;
    
    protected override void Update()
    {
        base.Update();

        transform.Translate(Vector3.right * dir * speed * Time.deltaTime);
        transform.localScale = new Vector3(dir, transform.localScale.y, transform.localScale.z);
    }
}
