using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Roger : MonoBehaviour
{
    [SerializeField] private GameObject door = null;
    [SerializeField] private Light2D[] additionalLights = null;

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

        if (door != null) {
            Destroy(door);
        }

        Destroy(GetComponent<Collider2D>());
    }

    public void ChangeLightColor()
    {
        lightC.color = Color.cyan;

        if (additionalLights.Length > 0) {
            foreach (Light2D lights in additionalLights) {
                lights.color = Color.cyan;
            }
        }
    }

    public void TriggerNextDialogue()
    {
        dialogueTrigger.TriggerDialogue();
    }
}
