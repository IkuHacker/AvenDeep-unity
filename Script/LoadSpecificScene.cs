using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSpecificScene : MonoBehaviour
{
    public string sceneName;
    public Animator fadeSystem;

    public AudioClip achivementSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            LoadAndSaveData.instance.SaveData();
            AudioManager.instance.PlayClipAt(achivementSound, transform.position);
            StartCoroutine(loadNextScene());

        }
    }

    public IEnumerator loadNextScene()
    {
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        fadeSystem.ResetTrigger("FadeIn");
        SceneManager.LoadScene(sceneName);

    }
}
