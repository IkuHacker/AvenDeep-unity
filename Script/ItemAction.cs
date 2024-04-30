using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class ItemAction : MonoBehaviour
{
    private Text InteractText;
    private Image interactImage;
    private Image interactImage1;

    [Header("DataDescrption")]
    public GameObject descriptionPanel;
    [Space]
    public Text itemName;
    public Text itemDescrption;
    public Image itemVisual;
    public Text itemType;
    public Text itemRarity;
    [Space]
    public Sprite emptySlotVisual;
    [Space]
    public Color commonColor = new Color(135f / 255f, 135f / 255f, 135f / 255f);
    public Color uncommonColor = new Color(105f / 255f, 166f / 255f, 107f / 255f);
    public Color rareColor = new Color(89f / 255f, 125f / 255f, 173f / 255f);
    public Color mythicalColor = new Color(120f / 255f, 78f / 255f, 143f / 255f);
    public Color legendaryColor = new Color(1f, 199f / 255f, 67f / 255f);
    [Space]
    private ItemData selectedItem;
    private Transform selectedSlot;

    [Header("Eqipment")]
    [SerializeField]
    private Image headSlotImage;
    [SerializeField]
    private Image chestSlotImage;
    [SerializeField]
    private Image legsSlotImage;
    [SerializeField]
    private Image feetSlotImage;

    [HideInInspector]
    public ItemData equipedHeadItem;
    [HideInInspector]
    public ItemData equipedChestItem;
    [HideInInspector]
    public ItemData equipedHandsItem;
    [HideInInspector]
    public ItemData equipedLegsItem;
    [HideInInspector]
    public ItemData equipedFeetItem;
    [HideInInspector]
    public ItemData equipedWeaponItem;
    [Space]
    [SerializeField]
    private Button headSlotDesequipButton;

    [SerializeField]
    private Button chestSlotDesequipButton;


    [SerializeField]
    private Button legsSlotDesequipButton;

    [SerializeField]
    private Button feetSlotDesequipButton;

    [Header("PotionEffect")]
    public PlayerEffect playerEffect;
    public PotionTimer potionTimer;
    

    [Header("Other")]
    [SerializeField]
    private Transform dropPoint;





    public static ItemAction instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Inventory dans la scène");
            return;
        }

        instance = this;

        InteractText = GameObject.FindGameObjectWithTag("UseText").GetComponent<Text>();
        interactImage = GameObject.FindGameObjectWithTag("UseImage").GetComponent<Image>();
        interactImage1 = GameObject.FindGameObjectWithTag("UseImage1").GetComponent<Image>();
    }

   
    public void SetDescrpion(int slotIndex) 
    {
       

        ItemData currentItem = gameObject.transform.GetChild(slotIndex).GetComponent<Slot>().item;
        Transform slot = gameObject.transform.GetChild(slotIndex);

        if (currentItem == null) 
        {
            descriptionPanel.SetActive(false);
            return;

        }
        else 
        {
            descriptionPanel.SetActive(true);
        }
        itemName.text = currentItem.itemName;
        itemDescrption.text = currentItem.description;
        itemType.text = currentItem.type.ToString();
        itemRarity.text = currentItem.rarity.ToString();
        itemVisual.sprite = currentItem.visual;

        switch (currentItem.rarity)
        {
            case ItemData.Rarity.Common:
                itemRarity.color = commonColor;
                break;
            case ItemData.Rarity.Uncommon:
                itemRarity.color = uncommonColor;
                break;
            case ItemData.Rarity.Rare:
                itemRarity.color = rareColor;
                break;
            case ItemData.Rarity.Mythical:
                itemRarity.color = mythicalColor;
                break;
            case ItemData.Rarity.Legendary:
                itemRarity.color = legendaryColor;
                break;
            default:
                itemRarity.color = Color.white; 
                break;
        }

        selectedItem = currentItem;
        selectedSlot = slot;

        if (currentItem.type == ItemData.Type.Equipment)
        {
            InteractText.text = " /                    To   	equip";
            InteractText.enabled = true;
            interactImage.enabled = true;
            interactImage1.enabled = true;
        }

        if (currentItem.type == ItemData.Type.Consumable)
        {
            InteractText.text = " /                    To   	consume";
            InteractText.enabled = true;
            interactImage.enabled = true;
            interactImage1.enabled = true;
        }

        if (currentItem.type == ItemData.Type.Resource)
        {
            InteractText.enabled = false;
            interactImage.enabled = false;
            interactImage1.enabled = false;
        }

    }

    private void Update()
    {
        if (Input.GetButtonDown("UseItem") && selectedItem != null)
        {
            if(selectedItem.type == ItemData.Type.Consumable) 
            {
                UseSelectedItem();
            }
            if (selectedItem.type == ItemData.Type.Equipment)
            {
                EquipItem(selectedItem.equipmentType);
            }

        }

        if (Input.GetButtonDown("DropItem") && selectedItem != null)
        {
            DropActionButton();
        }

        if (Input.GetButtonDown("DeleteItem") && selectedItem != null)
        {
            DestroyActionButton();
        }


    }

    public void EquipItem(ItemData.EquipmentType equipmentType) 
    {


        switch (equipmentType)
        {
            case ItemData.EquipmentType.Head:
                headSlotImage.sprite = selectedItem.visual;
                equipedHeadItem = selectedItem;
                break;

            case ItemData.EquipmentType.Chest:
                chestSlotImage.sprite = selectedItem.visual;
                equipedChestItem = selectedItem;
                break;

            case ItemData.EquipmentType.Legs:
                legsSlotImage.sprite = selectedItem.visual;
                equipedLegsItem = selectedItem;
                break;

            case ItemData.EquipmentType.Feet:
                feetSlotImage.sprite = selectedItem.visual;
                equipedFeetItem = selectedItem;
                break;

           
        }

        PlayerHealth.instance.curentArmoPoint += selectedItem.armorPoint;
        Debug.Log(PlayerHealth.instance.curentArmoPoint);
        int selectedIndex = Inventory.instance.content.FindIndex(item => item.itemData == selectedItem);

        if (selectedSlot.transform.GetChild(1).GetComponent<Text>().text == "1")
        {

            selectedSlot.transform.GetChild(0).GetComponent<Image>().sprite = emptySlotVisual;
            selectedSlot.transform.GetChild(1).GetComponent<Text>().enabled = false;
            Inventory.instance.RemoveItem(selectedItem);
            gameObject.transform.GetChild(selectedIndex).GetComponent<Slot>().item = null;
            Inventory.instance.RefreshContent();


            for (int i = Inventory.instance.content.Count; i < 23; i++)
            {
                gameObject.transform.GetChild(i).GetComponent<Slot>().item = null;
                gameObject.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = emptySlotVisual;
                gameObject.transform.GetChild(i).GetChild(1).GetComponent<Text>().enabled = false;
            }


        }

    }

    public void UpdateEquipmentsDesequipButtons()
    {
        headSlotDesequipButton.onClick.RemoveAllListeners();
        headSlotDesequipButton.onClick.AddListener(delegate { DesequipItem(ItemData.EquipmentType.Head); });
        headSlotDesequipButton.gameObject.SetActive(equipedHeadItem);

        chestSlotDesequipButton.onClick.RemoveAllListeners();
        chestSlotDesequipButton.onClick.AddListener(delegate { DesequipItem(ItemData.EquipmentType.Chest); });
        chestSlotDesequipButton.gameObject.SetActive(equipedChestItem);

        legsSlotDesequipButton.onClick.RemoveAllListeners();
        legsSlotDesequipButton.onClick.AddListener(delegate { DesequipItem(ItemData.EquipmentType.Legs); });
        legsSlotDesequipButton.gameObject.SetActive(equipedLegsItem);

        feetSlotDesequipButton.onClick.RemoveAllListeners();
        feetSlotDesequipButton.onClick.AddListener(delegate { DesequipItem(ItemData.EquipmentType.Feet); });
        feetSlotDesequipButton.gameObject.SetActive(equipedFeetItem);

    }


    public void DesequipItem(ItemData.EquipmentType equipmentType)
    {

        if (Inventory.instance.isFull())
        {
            Debug.Log("L'inventaire est plein, impossible de se déséquiper de cet élément");
            return;
        }

        switch (equipmentType)
        {
            case ItemData.EquipmentType.Head:
                headSlotImage.sprite = emptySlotVisual;
                selectedItem = equipedHeadItem;
                equipedHeadItem = null;
                break;

            case ItemData.EquipmentType.Chest:
                chestSlotImage.sprite = Inventory.instance.emptySlotVisual;
                selectedItem = equipedChestItem;
                equipedChestItem = null;
                break;

            case ItemData.EquipmentType.Legs:
                legsSlotImage.sprite = Inventory.instance.emptySlotVisual;
                selectedItem = equipedLegsItem;
                equipedLegsItem = null;
                break;

            case ItemData.EquipmentType.Feet:
                feetSlotImage.sprite = Inventory.instance.emptySlotVisual;
                selectedItem = equipedFeetItem;
                equipedFeetItem = null;
                break;


        }

        PlayerHealth.instance.curentArmoPoint -= selectedItem.armorPoint;
        Debug.Log(PlayerHealth.instance.curentArmoPoint);
        Inventory.instance.AddItem(selectedItem);
        Inventory.instance.RefreshContent();


    }


    public void UseSelectedItem()
    {
        if (selectedItem != null && selectedItem.type != ItemData.Type.Resource)
        {
            int selectedIndex = Inventory.instance.content.FindIndex(item => item.itemData == selectedItem);

            if(selectedSlot.transform.GetChild(1).GetComponent<Text>().text == "1")
            {
                UseItem();

                selectedSlot.transform.GetChild(0).GetComponent<Image>().sprite = emptySlotVisual;
                selectedSlot.transform.GetChild(1).GetComponent<Text>().enabled = false;
                Inventory.instance.RemoveItem(selectedItem);
                gameObject.transform.GetChild(selectedIndex).GetComponent<Slot>().item = null;
                Inventory.instance.RefreshContent();

                
                for (int i = Inventory.instance.content.Count; i < 23; i++)
                {
                    gameObject.transform.GetChild(i).GetComponent<Slot>().item = null;
                    gameObject.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = emptySlotVisual;
                    gameObject.transform.GetChild(i).GetChild(1).GetComponent<Text>().enabled = false;
                }

               
            }
            else
            {
                UseItem();

                int count = int.Parse(selectedSlot.transform.GetChild(1).GetComponent<Text>().text);
                count--;
                selectedSlot.transform.GetChild(1).GetComponent<Text>().text = count.ToString();
                Inventory.instance.RemoveItem(selectedItem);

                for (int i = Inventory.instance.content.Count; i < 23; i++)
                {
                    gameObject.transform.GetChild(i).GetComponent<Slot>().item = null;
                    gameObject.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = emptySlotVisual;
                    gameObject.transform.GetChild(i).GetChild(1).GetComponent<Text>().enabled = false;
                }


            }

            

        }
        
    }
    public void DropActionButton()
    {
        GameObject instantiatedItem = Instantiate(selectedItem.prefab);
        instantiatedItem.transform.position = dropPoint.position;

        int selectedIndex = Inventory.instance.content.FindIndex(item => item.itemData == selectedItem);

        if (selectedSlot.transform.GetChild(1).GetComponent<Text>().text == "1")
        {

            selectedSlot.transform.GetChild(0).GetComponent<Image>().sprite = emptySlotVisual;
            selectedSlot.transform.GetChild(1).GetComponent<Text>().enabled = false;
            Inventory.instance.RemoveItem(selectedItem);
            gameObject.transform.GetChild(selectedIndex).GetComponent<Slot>().item = null;
            Inventory.instance.RefreshContent();


            for (int i = Inventory.instance.content.Count; i < 23; i++)
            {
                gameObject.transform.GetChild(i).GetComponent<Slot>().item = null;
                gameObject.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = emptySlotVisual;
                gameObject.transform.GetChild(i).GetChild(1).GetComponent<Text>().enabled = false;
            }


        }
        else
        {

            int count = int.Parse(selectedSlot.transform.GetChild(1).GetComponent<Text>().text);
            count--;
            selectedSlot.transform.GetChild(1).GetComponent<Text>().text = count.ToString();
            Inventory.instance.RemoveItem(selectedItem);

            for (int i = Inventory.instance.content.Count; i < 23; i++)
            {
                gameObject.transform.GetChild(i).GetComponent<Slot>().item = null;
                gameObject.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = emptySlotVisual;
                gameObject.transform.GetChild(i).GetChild(1).GetComponent<Text>().enabled = false;
            }


        }
        Inventory.instance.RefreshContent();
    }


    public void UseItem()
    {
        if (selectedItem.type == ItemData.Type.Consumable)
        {
            PlayerHealth.instance.HealPlayer(selectedItem.hpGiven);    
            playerEffect.AddSpeed(selectedItem.speedGiven, selectedItem.speedDuration);
            potionTimer.StartTimer(selectedItem.speedDuration);
        }
    }

    public void DestroyActionButton()
    {
       int selectedIndex = Inventory.instance.content.FindIndex(item => item.itemData == selectedItem);

            if (selectedSlot.transform.GetChild(1).GetComponent<Text>().text == "1")
            {

                selectedSlot.transform.GetChild(0).GetComponent<Image>().sprite = emptySlotVisual;
                selectedSlot.transform.GetChild(1).GetComponent<Text>().enabled = false;
                Inventory.instance.RemoveItem(selectedItem);
                gameObject.transform.GetChild(selectedIndex).GetComponent<Slot>().item = null;
                Inventory.instance.RefreshContent();


                for (int i = Inventory.instance.content.Count; i < 23; i++)
                {
                    gameObject.transform.GetChild(i).GetComponent<Slot>().item = null;
                    gameObject.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = emptySlotVisual;
                    gameObject.transform.GetChild(i).GetChild(1).GetComponent<Text>().enabled = false;
                }


            }
            else
            {

                int count = int.Parse(selectedSlot.transform.GetChild(1).GetComponent<Text>().text);
                count--;
                selectedSlot.transform.GetChild(1).GetComponent<Text>().text = count.ToString();
                Inventory.instance.RemoveItem(selectedItem);

                for (int i = Inventory.instance.content.Count; i < 23; i++)
                {
                    gameObject.transform.GetChild(i).GetComponent<Slot>().item = null;
                    gameObject.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = emptySlotVisual;
                    gameObject.transform.GetChild(i).GetChild(1).GetComponent<Text>().enabled = false;
                }


            }
        

    }

}
