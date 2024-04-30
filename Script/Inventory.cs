using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    public List<ItemInInventory> content = new List<ItemInInventory>();

    public GameObject inventoryPanel;

    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private Transform inventorySlotsParent;

    const int InventorySize = 24;

    public int amariniteCount;
    public Text amariniteCountText;

    public int auroriumCount;
    public Text auroriumCountText;

    public int celanthiteCount;
    public Text celanthiteCountText;


    public Sprite emptySlotVisual;

    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Inventory dans la scène");
            return;
        }

        instance = this;
    }


    private void Start()
    {
        RefreshContent();
        UpdateTextUI();
    }

    
    public void AddItem(ItemData item)
    {
        ItemInInventory[] itemInInventory = content.Where(elem => elem.itemData == item).ToArray();

        bool itemAdded = false;

        if (itemInInventory.Length > 0 && item.stackable)
        {
            for (int i = 0; i < itemInInventory.Length; i++)
            {
                if (itemInInventory[i].count < item.maxStack)
                {
                    itemAdded = true;
                    itemInInventory[i].count++;
                    break;
                }
            }

            if (!itemAdded)
            {
                content.Add(
                    new ItemInInventory
                    {
                        itemData = item,
                        count = 1
                    }
                );
            }
        }
        else
        {
            content.Add(
                new ItemInInventory
                {
                    itemData = item,
                    count = 1
                }
            );
        }

        RefreshContent();
    }

    public void RemoveItem(ItemData item)
    {
        ItemInInventory itemInInventory = content.Where(elem => elem.itemData == item).FirstOrDefault();

        if (itemInInventory != null && itemInInventory.count > 1)
        {
            itemInInventory.count--;
        }
        else
        {
            content.Remove(itemInInventory);
        }

        RefreshContent();

    }

    public void AddAmarinite(int count)
    {
        amariniteCount += count;
        UpdateTextUI();
    }


    public void AddAurorium(int count)
    {
        auroriumCount += count;
        UpdateTextUI();

    }
    public void AddCelanthite(int count)
    {
        celanthiteCount += count;
        UpdateTextUI();

    }

    public void RemoveAmarinite(int count)
    {
        amariniteCount += count;
        UpdateTextUI();
    }


    public void RemoveAurorium(int count)
    {
        auroriumCount += count;
        UpdateTextUI();

    }
    public void RemoveCelanthite(int count)
    {
        celanthiteCount += count;
        UpdateTextUI();
    }



    public void UpdateTextUI()
    {
        amariniteCountText.text = amariniteCount.ToString();
        auroriumCountText.text = amariniteCount.ToString();
        celanthiteCountText.text = amariniteCount.ToString();
    }



    private void Update()
    {
        if (Input.GetButtonDown("OpenInventory"))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            playerMovement.enabled = !inventoryPanel.activeSelf;
        }
    }

    public void RefreshContent()
    {

        for (int i = 0; i < content.Count; i++)
        {
            Slot currentSlot = inventorySlotsParent.GetChild(i).GetComponent<Slot>();

            currentSlot.item = content[i].itemData;
            currentSlot.itemVisual.sprite = content[i].itemData.visual;
            if (currentSlot.item.stackable)
            {
                currentSlot.countText.enabled = true;
                currentSlot.countText.text = content[i].count.ToString();
            }


        }

        ItemAction.instance.UpdateEquipmentsDesequipButtons();
    }



   

    public bool isFull()
    {
        return InventorySize == content.Count;
    }

}

[System.Serializable]
public class ItemInInventory 
{
    public ItemData itemData;
    public int count;
}
