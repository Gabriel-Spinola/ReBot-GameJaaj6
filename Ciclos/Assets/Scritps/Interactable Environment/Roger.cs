using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Roger : MonoBehaviour
{
    [SerializeField] private GameObject door = null;

    private Animator anim = null;
    private Light2D lightC = null;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        lightC = GetComponentInChildren<Light2D>();
    }

    public void Trigger()
    {
        anim.SetTrigger("Activate");

        Destroy(door);
    }

    public void ChangeLightColor()
    {
        lightC.color = Color.cyan;
    }
}
