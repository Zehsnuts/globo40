using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

    public string nextScene;
    public string currentScene;

    public GameObject sceneContainer;
    private GameObject currentSceneContainer;
    public List<GameObject> scenesList;

    public void ChangeScene(GameObject sC)
    {
        if(currentSceneContainer != null)
            currentSceneContainer.SetActive(false);
        currentSceneContainer = sC;
        currentSceneContainer.SetActive(true);
    }
}
