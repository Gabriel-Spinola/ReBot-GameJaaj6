using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;

    [SerializeField] private bool destroyAfterUse;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialoguesManager>().StartDialogue(dialogue);

        if (destroyAfterUse)
            Destroy(gameObject);
    }
}
