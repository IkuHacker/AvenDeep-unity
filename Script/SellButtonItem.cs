using UnityEngine;
using UnityEngine.UI;


public class SellButtonItem : MonoBehaviour
{
    public Text itemName;
    public Image itemVisual;
    public Text Itemprice;

    public ItemData item;

    public void BuyItem()
    {
        Inventory inventory = Inventory.instance;
        if (inventory.celanthiteCount >= item.price) 
        {
            inventory.AddItem(item);
            inventory.RefreshContent();
            inventory.celanthiteCount -= item.price;
            inventory.UpdateTextUI();
        }
    }
}