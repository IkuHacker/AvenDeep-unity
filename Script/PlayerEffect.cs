using UnityEngine;
using System.Collections;

public class PlayerEffect : MonoBehaviour
{
    public void AddSpeed(int speedGiven, float speedDuration)
    {
        PlayerMovement.instance.moveSpeed += speedGiven;
        PlayerMovement.instance.climbSpeed += PlayerMovement.instance.climbSpeed / (PlayerMovement.instance.moveSpeed / speedGiven);
        StartCoroutine(RemoveSpeed(speedGiven, speedDuration));
    }

    private IEnumerator RemoveSpeed(int speedGiven, float speedDuration)
    {
        yield return new WaitForSeconds(speedDuration);
        PlayerMovement.instance.moveSpeed -= speedGiven;
    }
}