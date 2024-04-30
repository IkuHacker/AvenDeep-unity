using UnityEngine;
using UnityEngine.UI;

public class PotionTimer : MonoBehaviour
{
    public Text timerText; 
    private float remainingTime;
    public Animator animatorEffectPanl;

    public void StartTimer(float duration)
    {
        animatorEffectPanl.SetBool("IsOpen", true);
        remainingTime = duration;
        UpdateTimerDisplay();
        InvokeRepeating(nameof(UpdateTimer), 1f, 1f); 
    }

    private void UpdateTimer()
    {
        remainingTime -= 1f;
        if (remainingTime <= 0f)
        {
            animatorEffectPanl.SetBool("IsOpen", false);
            remainingTime = 0f;
            CancelInvoke(nameof(UpdateTimer)); 
        }
        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }
}
