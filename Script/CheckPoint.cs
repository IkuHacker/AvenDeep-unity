using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public AudioClip pickUp;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            AudioManager.instance.PlayClipAt(pickUp, transform.position);
            CurrentSceneManager.instance.respawnPoint = transform.position;
            Destroy(gameObject);

        }
    }
}
