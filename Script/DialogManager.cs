using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text npcName;
    public Text dialogContent;
    public Animator animator;

    public float speedTypeSentence;

    public Queue<string> sentences;

    public static DialogManager instance;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de DialogManager dans la scène");
            return;
        }

        instance = this;

        sentences = new Queue<string>();
    }

    public void Update()
    {

        if (Input.GetButtonDown("Pass"))
        {
            DisplayNextSentence();

        }

        

        if (Input.GetButtonDown("Cancel"))
        {
            
            EndDialog();
            return;

        }
    }




    public void StartDialog(Dialog dialogue) 
    {
        animator.SetBool("IsOpen", true);
        npcName.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        

       

        DisplayNextSentence();
    }

    public void DisplayNextSentence() 
    {
        if (sentences.Count == 0) 
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentece(sentence));
   
    }


    public void EndDialog() 
    {
        DialogTrigger.instance.IsDialogStart = false;
        animator.SetBool("IsOpen", false);
    }

    IEnumerator TypeSentece(string sentence) 
    {
        dialogContent.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogContent.text += letter;
            yield return new WaitForSeconds(speedTypeSentence);
        }
    }

}
