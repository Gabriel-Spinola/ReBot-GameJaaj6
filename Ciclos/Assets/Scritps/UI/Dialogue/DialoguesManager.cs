using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

// https://youtu.be/_nRzoTzeyxU
public class DialoguesManager : MonoBehaviour
{
    public static bool IsOnADialogue = false;

    [SerializeField] private TMP_Text nameTxt;
    [SerializeField] private TMP_Text dialogueTxt;

    [SerializeField] private Animator anim;

    private Queue<string> sentences;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        IsOnADialogue = true;

        anim.SetBool("IsOpen", true);
        nameTxt.SetText(dialogue.name);
        sentences.Clear();

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0) {
            EndDialogue();

            return;
        }

        string sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueTxt.SetText("");

        foreach (char letter in sentence.ToCharArray()) {
            dialogueTxt.text += letter;

            yield return null;
        }
    }

    public void EndDialogue()
    {
        IsOnADialogue = false;

        anim.SetBool("IsOpen", false);
    }
}

[System.Serializable]
public class Dialogue
{
    public string name;

    [TextArea(3, 10)]
    public string[] sentences;
}
