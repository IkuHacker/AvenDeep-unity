using UnityEngine;
using UnityEngine.UI;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;
    public GameObject inventoryPanel;

    public bool IsInRange;
    public bool IsDialogStart;

    public Text InteractText;
    public Image interactImage;
    public Image interactImage1;


    public static DialogTrigger instance;


    private void Awake()
    {

        InteractText = GameObject.FindGameObjectWithTag("InteractText").GetComponent<Text>();
        interactImage = GameObject.FindGameObjectWithTag("InteractImage").GetComponent<Image>();
        interactImage1 = GameObject.FindGameObjectWithTag("InteractImage1").GetComponent<Image>();


        if (instance != null)
        {

            return;
        }

        instance = this;

        
    }
    


    void Update()
    {
        
        if(IsInRange && Input.GetButtonDown("Interact")) 
        {
            IsDialogStart = true;
            InteractText.enabled = false;
            interactImage.enabled = false;
            interactImage1.enabled = false;
            TriggerDialog();
        }


    
        if (IsDialogStart) 
        {
            PlayerMovement.instance.isGrounded = false;
            Inventory.instance.enabled = false;
        }

        






    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InteractText.text = " /                    To   	interact";
            IsInRange = true;
            InteractText.enabled = true;
            interactImage.enabled = true;
            interactImage1.enabled = true;
            inventoryPanel.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            IsInRange = false;
            InteractText.enabled = false;
            interactImage.enabled = false;
            interactImage1.enabled = false;
            DialogManager.instance.EndDialog();
            Inventory.instance.enabled = true;


        }
    }


    public void TriggerDialog() 
    {
        DialogManager.instance.StartDialog(dialog);

      
    }
}
