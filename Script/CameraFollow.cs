
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    public GameObject player;
    public float timeOfset;
    public Vector3 posOfset;

    private Vector3 velocity;
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + posOfset, ref velocity, timeOfset);
    }
}
