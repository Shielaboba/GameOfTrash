using System.Collections;   
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoaderScript : MonoBehaviour {

   public GameObject loadingScreen;
   public Slider slider;
   public Text progressText;

	void Start()
    {
        StartCoroutine(LoadAsynchronously());
    }

    IEnumerator LoadAsynchronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("trash_menu");
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;

            progressText.text = progress * 100f + "%";
            yield return null;
        }
    }
}
