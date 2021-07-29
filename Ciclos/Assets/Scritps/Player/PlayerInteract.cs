using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private TimeGauntlet timeGauntlet;

    [SerializeField] private float gauntletCooldown = 1f;
    [SerializeField] private float gauntletDelay = .6f;

    [SerializeField] private bool canUseGauntlet = false;
    

    private Player player = null;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (canUseGauntlet && player.InputManager.keyGauntlet) {
            StartCoroutine(StartTimeTravel(gauntletDelay));
            StartCoroutine(player.DisablePlayer(gauntletDelay));
            StartCoroutine(player.playerGraphics.DisableAnimation(gauntletDelay));
            player.playerGraphics.SetUsingGauntlet(true);

            StartCoroutine(DisableGauntlet(gauntletCooldown));
        }   
    }

    private IEnumerator StartTimeTravel(float time)
    {
        player.canMove = false;
        player.SetUseBetterJump(false);
        
        player.GetRigidbody().gravityScale = 0f;
        player.GetRigidbody().velocity = Vector2.zero;

        yield return new WaitForSeconds(time);

        player.canMove = true;
        player.SetUseBetterJump(true);
        player.GetRigidbody().gravityScale = 2f;
        player.playerGraphics.SetUsingGauntlet(false);
        timeGauntlet.UpdateGauntlet();
    }

    private IEnumerator DisableGauntlet(float time)
    {
        canUseGauntlet = false;

        yield return new WaitForSeconds(time);

        canUseGauntlet = true;
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
