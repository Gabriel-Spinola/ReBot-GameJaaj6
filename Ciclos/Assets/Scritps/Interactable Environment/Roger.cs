using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Roger : MonoBehaviour
{
    [SerializeField] private GameObject door = null;

    private Animator anim = null;
    private Light2D lightC = null;
    private DialogueTrigger dialogueTrigger = null;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        dialogueTrigger = GetComponent<DialogueTrigger>();
        lightC = GetComponentInChildren<Light2D>();
    }

    public void Trigger()
    {
        anim.SetTrigger("Activate");

        Destroy(door);
        Destroy(GetComponent<Collider2D>());
    }

    public void ChangeLightColor()
    {
        lightC.color = Color.cyan;
    }

    public void TriggerNextDialogue()
    {
        dialogueTrigger.TriggerDialogue();
    }
}
