using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [System.Serializable]
    public class SceneMusic
    {
        public string sceneName;
        public AudioClip clip;
        public bool loop = true;
    }

    public SceneMusic[] musicByScene;

    private AudioSource audioSource;

    void Awake()
    {
        // evitar duplicados
        MusicManager[] all = FindObjectsOfType<MusicManager>();
        if (all.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f; // 2D

        audioSource.volume = PlayerPrefsManager.GetMasterVolume();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
    }

    private void PlayMusicForScene(string sceneName)
    {
        AudioClip chosen = null;
        bool chosenLoop = true;

        for (int i = 0; i < musicByScene.Length; i++)
        {
            if (musicByScene[i].sceneName == sceneName)
            {
                chosen = musicByScene[i].clip;
                chosenLoop = musicByScene[i].loop;
                break;
            }
        }

        if (chosen == null) return;

        if (audioSource.clip == chosen && audioSource.isPlaying) return;

        audioSource.clip = chosen;
        audioSource.loop = chosenLoop;

        // aplicar SIEMPRE el volumen guardado
        audioSource.volume = PlayerPrefsManager.GetMasterVolume();

        audioSource.Play();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp01(volume);
    }
}
