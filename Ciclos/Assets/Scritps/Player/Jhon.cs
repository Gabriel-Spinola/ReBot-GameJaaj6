using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jhon : MonoBehaviour
{
    public ParticleSystem spawnEffect;

    private void OnEnable()
    {
        if (DialoguesManager.IsOnADialogue)
            spawnEffect.Play();
    }

    private void OnDisable()
    {
        spawnEffect.Play();
    }
}
