using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainStory : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable() =>
        // Only specifying the sceneName or sceneBuilderIndex will load the Scene with the Single mode
        SceneManager.LoadScene("Episode-1- Layla", LoadSceneMode.Single);
}
