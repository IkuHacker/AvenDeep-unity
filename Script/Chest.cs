using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public Text InteractText;
    public Image interactImage;
    public Image interactImage1;

    private bool isInRange;

    public List<ItemData> possibleLoot = new List<ItemData>();

    public int nbLoot;

    public Animator animator;

    void Awake()
    {
        InteractText = GameObject.FindGameObjectWithTag("InteractText").GetComponent<Text>();
        interactImage = GameObject.FindGameObjectWithTag("InteractImage").GetComponent<Image>();
        interactImage1 = GameObject.FindGameObjectWithTag("InteractImage1").GetComponent<Image>();

    }

    void Update()
    {
        if (isInRange && Input.GetButtonDown("Interact"))
        {
            OpenChest();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            InteractText.text = " /                    To   	open";
            InteractText.enabled = true;
            interactImage.enabled = true;
            interactImage1.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            InteractText.enabled = false;
            interactImage.enabled = false;
            interactImage1.enabled = false;
        }
    }


    // Liste des objets de butin possibles

    public void OpenChest()
    {
        if (!PlayerPrefs.HasKey("ChestOpenCount"))
        {
            PlayerPrefs.SetInt("ChestOpenCount", 0);
        }

        int openCount = PlayerPrefs.GetInt("ChestOpenCount");

        if (openCount < 1) // Limite le butin � une seule fois
        {
            animator.SetTrigger("OpenChest");

            for (int i = 0; i < nbLoot; i++)
            {
                // S�lectionne un objet de butin en tenant compte de la raret�
                ItemData lootItem = GetRandomLootItem();
                Inventory.instance.AddItem(lootItem);
            }

            openCount++;
            PlayerPrefs.SetInt("ChestOpenCount", openCount);
        }
    }

    private ItemData GetRandomLootItem()
    {
        // Cr�e une liste pond�r�e en fonction de la raret� des objets
        List<ItemData> weightedItems = new List<ItemData>();

        foreach (ItemData item in possibleLoot)
        {
            int rarityWeight = GetRarityWeight(item.rarity);
            for (int i = 0; i < rarityWeight; i++)
            {
                weightedItems.Add(item);
            }
        }

        // S�lectionne un objet al�atoire parmi la liste pond�r�e
        int randomIndex = Random.Range(0, weightedItems.Count);
        return weightedItems[randomIndex];
    }

    private int GetRarityWeight(ItemData.Rarity rarity)
    {
        // D�termine le poids de la raret� pour influencer les chances de butin
        switch (rarity)
        {
            case ItemData.Rarity.Common:
                return 70; // Poids plus �lev� pour les objets communs
            case ItemData.Rarity.Uncommon:
                return 30;
            case ItemData.Rarity.Rare:
                return 10;
            case ItemData.Rarity.Mythical:
                return 5;
            case ItemData.Rarity.Legendary:
                return 1; 
            default:
                return 100;
        }
    }



}

