using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] float _levelLoadDelay = 1f;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            StartCoroutine("LoadLevel");
    }

    IEnumerator LoadLevel()
    {
        // befor
        yield return new WaitForSecondsRealtime(_levelLoadDelay);
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentLevel + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings)
            nextScene = 0;

        FindObjectOfType<ScenePresiste>().resetScenePresiste();
        SceneManager.LoadScene(nextScene);
    }
}
