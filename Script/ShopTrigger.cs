using UnityEngine;
using UnityEngine.UI;

public class ShopTrigger : MonoBehaviour
{

    public GameObject inventoryPanel;
    public GameObject shopPanel;

    private bool IsInRange;

    [HideInInspector]
    public bool IsShopOpen;

    private Text InteractText;
    private Image interactImage;
    private Image interactImage1;

    public string PNJName;
    public ItemData[] itemToSell;


    public static ShopTrigger instance;


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

        if (IsInRange && Input.GetButtonDown("Interact"))
        {
            IsShopOpen = true;
            InteractText.enabled = false;
            interactImage.enabled = false;
            interactImage1.enabled = false;
            shopPanel.SetActive(!shopPanel.activeSelf);
            ShopManager.instance.OpenShop(itemToSell, PNJName);
            if (!shopPanel.activeSelf) 
            {
                IsShopOpen = false;
            }
            else 
            {
                inventoryPanel.SetActive(false);
            }

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


        }
    }

}
