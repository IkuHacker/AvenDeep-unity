using UnityEngine;

public class PickUpBehavior : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private Inventory inventory;

    private Item currentItem;
    public void DoPickUp(Item item)
    {
        if (inventory.isFull())
        {
            Debug.Log("Inventtory full, can pickup : " + item.name);
            return;
        }
        currentItem = item;
        
       

    }

    public void AddItemToInventory()
    {
        inventory.AddItem(currentItem.itemData);
        Destroy(currentItem.gameObject);
        currentItem = null;
    }

   
}
