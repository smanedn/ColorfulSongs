using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AM : MonoBehaviour
{

    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string volumeCategory;
    [SerializeField] private string audioMixerCategory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!PlayerPrefs.HasKey(volumeCategory))
        {
            PlayerPrefs.SetFloat(volumeCategory, 1);
            PlayerPrefs.Save();
        }
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeVolume() {
        if (volumeSlider.value == 0)
        {
            volumeSlider.value = 0.00001f;  // piccolo controllo per evitare Log10(0)
        }
        audioMixer.SetFloat(audioMixerCategory, Mathf.Log10(volumeSlider.value) * 20);   //conversione (0.1-1) a db
        Save();
        
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat(volumeCategory);
    }

    private void Save() 
    {
        PlayerPrefs.SetFloat(volumeCategory, volumeSlider.value);
        PlayerPrefs.Save();
    }
}
