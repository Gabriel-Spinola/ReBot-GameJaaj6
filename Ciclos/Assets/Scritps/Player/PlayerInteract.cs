using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private TimeGauntlet timeGauntlet;
    [SerializeField] private bool canUseGauntlet = false;

    private void Update()
    {
        if (canUseGauntlet)
            timeGauntlet.UseGauntlet();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key")) {
            other.gameObject.GetComponent<Keys>().OpenDoor();
        }

        if (other.CompareTag("KeyGauntlet")) {
            Destroy(other.gameObject);

            canUseGauntlet = true;
        }
    }
}
