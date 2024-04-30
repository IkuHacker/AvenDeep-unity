using UnityEngine;
using UnityEngine.UI;


public class PickUpItem : MonoBehaviour
{
    public Inventory inventory;
    public PickUpBehavior playerPickUpBehavior;
    public AudioClip pickUp;

    public Text InteractText;
    public Image interactImage;
    public Image interactImage1;

    private bool isInRange;

    private void Awake()
    {

        InteractText = GameObject.FindGameObjectWithTag("InteractText").GetComponent<Text>();
        interactImage = GameObject.FindGameObjectWithTag("InteractImage").GetComponent<Image>();
        interactImage1 = GameObject.FindGameObjectWithTag("InteractImage1").GetComponent<Image>();

    }
    void Update()
    {
        if (Input.GetButtonDown("Interact") && isInRange)
        {
            TakeItem();
        }
    }

    void TakeItem()
    {
        playerPickUpBehavior.DoPickUp(gameObject.GetComponent<Item>());
        AudioManager.instance.PlayClipAt(pickUp, transform.position);
        inventory.AddItem(gameObject.GetComponent<Item>().itemData);
        Destroy(gameObject);
    }
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InteractText.text = " /                    To   	take";
            InteractText.enabled = true;
            interactImage.enabled = true;
            interactImage1.enabled = true;
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InteractText.enabled = false;
            interactImage.enabled = false;
            interactImage1.enabled = false;
            isInRange = false;
        }
    }

}
    
