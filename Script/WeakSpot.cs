using UnityEngine;

public class WeakSpot : MonoBehaviour
{
    public GameObject objectToDestroy;
    public AudioClip hit;
    public ItemData deepSnakeSkin;
    public int maxRandomSkin;
    public int minRandomSkin;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int randomSkin = Random.Range(maxRandomSkin, minRandomSkin);

            Debug.Log(randomSkin);

            for (int i = 0; i < randomSkin; i++)
            {
                Inventory.instance.AddItem(deepSnakeSkin);
            }
            
            AudioManager.instance.PlayClipAt(hit, transform.position);
            Destroy(objectToDestroy);
        }
    }
}
