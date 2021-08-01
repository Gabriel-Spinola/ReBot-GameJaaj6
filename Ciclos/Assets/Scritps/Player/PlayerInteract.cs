using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject jhon;

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
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            levelManager.GoToNextLevel();
            LevelManager.CurrentLevel++;
        }
#endif

        jhon.SetActive(DialoguesManager.IsOnADialogue);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag) {
            case "Key":
                other.gameObject.GetComponent<Keys>().OpenDoor();
            break;

            case "NextScene":
                levelManager.GoToNextLevel();
                LevelManager.CurrentLevel++;
            break;

            case "DialogueTrigger":
                other.GetComponent<DialogueTrigger>().TriggerDialogue();
            break;

            case "Roger":
                if (player.InputManager.keyUse || player.InputManager.keyUseHold) {
                    other.GetComponent<Roger>().Trigger();
                }
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
    }
}
