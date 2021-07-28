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
        switch (other.tag) {
            case "Key":
                other.gameObject.GetComponent<Keys>().OpenDoor();
            break;

            case "KeyGauntlet":
                Destroy(other.gameObject);

                canUseGauntlet = true;
            break;

            case "NextScene":
                LevelsManager.GoToNextLevel();
                LevelsManager.CurrentLevel++;
            break;
        }
    }
}
