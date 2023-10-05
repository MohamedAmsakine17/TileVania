using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePresiste : MonoBehaviour
{
    void Awake()
    {
        int scenePresisteNumber = FindObjectsOfType<ScenePresiste>().Length;
        if (scenePresisteNumber > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void resetScenePresiste()
    {
        Destroy(gameObject);
    }
}
