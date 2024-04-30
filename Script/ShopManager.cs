
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Text PNJName;
    public GameObject SellButtonPrefab;
    public GameObject ShopPanel;
    public Transform SellButtonsParent;

    public static ShopManager instance;


    private void Awake()
    {

        if (instance != null)
        {

            return;
        }

        instance = this;


    }


    public void OpenShop(ItemData[] items, string NPCNme) 
    {
        
        PNJName.text = NPCNme;
        UpdateItemToSell(items);
    }


    public void CloseShop()
    {

       
    }

    public void UpdateItemToSell(ItemData[] items) 
    {
        for (int i = 0; i < SellButtonsParent.childCount; i++)
        {
            Destroy(SellButtonsParent.GetChild(i).gameObject);
        }

        for (int i = 0; i < items.Length; i++)
        {
            GameObject button = Instantiate(SellButtonPrefab, SellButtonsParent);
            SellButtonItem buttonItem = button.GetComponent<SellButtonItem>();
            buttonItem.itemName.text = items[i].itemName;
            buttonItem.itemVisual.sprite = items[i].visual;
            buttonItem.Itemprice.text = items[i].price.ToString();
            buttonItem.item = items[i];

        }
    }
}
