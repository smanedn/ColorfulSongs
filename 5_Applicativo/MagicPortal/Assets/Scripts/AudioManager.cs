using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----------Audio Source----------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("----------Audio Clip----------")]
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip death;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip jump;

    void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    void Update()
    {
        
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public AudioClip getDeath() 
    { 
        return death; 
    }
    public AudioClip GetHit()
    {
        return hit;
    }

    public AudioClip GetJump()
    {
        return jump;
    }
}
