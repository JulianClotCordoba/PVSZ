using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float levelSeconds = 100f;

    [Header("Referencias")]
    public Slider slider;
    public GameObject winText;

    private bool isEndOfLevel = false;

    void Start()
    {
        if (winText != null)
            winText.SetActive(false);

        if (slider != null)
        {
            slider.minValue = 0f;
            slider.maxValue = 1f;
            slider.value = 0f;
        }
    }

    void Update()
    {
        if (isEndOfLevel || slider == null) return;

        slider.value = Time.timeSinceLevelLoad / levelSeconds;

        if (Time.timeSinceLevelLoad >= levelSeconds)
{
    DestroyAllTaggedObjects();

    isEndOfLevel = true;

    if (winText != null)
        winText.SetActive(true);

    Invoke(nameof(LoadNextLevel), 2f); // ⏱ tiempo para ver el texto
}

    }

    void DestroyAllTaggedObjects()
    {
        GameObject[] taggedObjectArray = GameObject.FindGameObjectsWithTag("destroyOnWin");
        foreach (GameObject taggedObject in taggedObjectArray)
            Destroy(taggedObject);
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
