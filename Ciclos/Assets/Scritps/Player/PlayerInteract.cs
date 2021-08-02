using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject jhon;

    [SerializeField] private float gauntletCooldown = 1f;
    [SerializeField] private float gauntletDelay = .6f;

    [SerializeField] private bool canUseGauntlet = false;

    private Player player = null;

    private bool isFinished;
    private bool b = false;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            levelManager.GoToNextLevel();
            LevelManager.CurrentLevel++;
        }
#endif

        if (jhon != null) {
            jhon.SetActive(DialoguesManager.IsOnADialogue);
        }

        if (isFinished) {
            if (!DialoguesManager.IsOnADialogue) {
                JhonMal.shoot = true;
                player.GetRigidbody().velocity = Vector2.zero;
            }

            player.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag) {
            case "Key":
                other.gameObject.GetComponent<Keys>().OpenDoor();
            break;

            case "NextScene":
                if (!DialoguesManager.IsOnADialogue) {
                    levelManager.GoToNextLevel();
                    LevelManager.CurrentLevel++;
                }
            break;

            case "DialogueTrigger":
                other.GetComponent<DialogueTrigger>().TriggerDialogue();
            break;

            case "Roger":
                if (player.InputManager.keyUse || player.InputManager.keyUseHold) {
                    other.GetComponent<Roger>().Trigger();
                }
            break;

            case "Jhon Mal":
                player.canMove = false;
                isFinished = true;
            break;

            case "BulletMal":
                StartCoroutine(player.DisablePlayer(120f));
                StartCoroutine(player.playerGraphics.DisableAnimation(120f));
                InputManager.DisableInput = true;
                player.playerGraphics.disableAnimation = true;

                player.playerGraphics.SetTrigger("Die");


                Debug.Log("Died"); 
            break;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Roger")) {
            if (player.InputManager.keyUse || player.InputManager.keyUseHold) {
                other.GetComponent<Roger>().Trigger();
            }
        }

        if (DialoguesManager.IsOnADialogue is false) {
            if (other.CompareTag("NextScene")) {
                levelManager.GoToNextLevel();
                LevelManager.CurrentLevel++;
            }
        }
    }
}
