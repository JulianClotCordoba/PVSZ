using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsController : MonoBehaviour
{
    public Slider volumeSlider;

    private MusicManager musicManager;

    void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();

        float saved = PlayerPrefsManager.GetMasterVolume();
        volumeSlider.value = saved;

        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

        // aplicar al entrar
        OnVolumeChanged(saved);
    }

    void OnDestroy()
    {
        volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
    }

    private void OnVolumeChanged(float value)
    {
        PlayerPrefsManager.SetMasterVolume(value);

        if (musicManager != null)
            musicManager.SetVolume(value);
    }

    public void SaveAndExit()
    {
        SceneManager.LoadScene("01a Start");
    }

    public void SetDefaults()
    {
        volumeSlider.value = 0.8f; // dispara OnVolumeChanged
    }
}
