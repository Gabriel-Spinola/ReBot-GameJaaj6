using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;

    private void Awake() => anim = GetComponent<Animator>();

    public void Open()
    {
        GetComponent<Collider2D>().isTrigger = true;

        anim.SetTrigger("Open");
    }

    public void DestroySelf() => Destroy(gameObject);
}
