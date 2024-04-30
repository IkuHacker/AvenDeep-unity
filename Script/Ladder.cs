using UnityEngine;
using UnityEngine.UI;


public class Ladder : MonoBehaviour
{
    private bool isInRange;
    public BoxCollider2D topCollider;
    private PlayerMovement playerMovement;

    public  Text InteractText;
    public Image interactImage;
    public Image interactImage1;
    void Awake()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        InteractText = GameObject.FindGameObjectWithTag("InteractText").GetComponent<Text>();
        interactImage = GameObject.FindGameObjectWithTag("InteractImage").GetComponent<Image>();
        interactImage1 = GameObject.FindGameObjectWithTag("InteractImage1").GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && playerMovement.isClimbing && Input.GetButtonDown("Interact"))
        {
            playerMovement.isClimbing = false;
            topCollider.isTrigger = false;
            return;

        }

        if (isInRange && Input.GetButtonDown("Interact")) 
        {
            playerMovement.isClimbing = true;
            topCollider.isTrigger = true;
          


        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            InteractText.text = " /                    To   	climb";
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
            isInRange = false;
            playerMovement.isClimbing = false;
            topCollider.isTrigger = false;
            InteractText.enabled = false;
            interactImage.enabled = false;
            interactImage1.enabled = false;

        }
    }
}
