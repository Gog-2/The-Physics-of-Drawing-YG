using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField]private AudioMixer audioMixer;
    
    [Header("Sliders")]
    [SerializeField]private Slider _masterSlider;
    [SerializeField]private Slider musicSlider;
    [SerializeField]private Slider vfxSlider;
    [Header("VFX Test Sound")]
    [SerializeField]private AudioClip testVFXClip;
    [SerializeField]private AudioSource testAudioSource;

    void Start()
    {
        _masterSlider.value = PlayerPrefs.GetFloat("MasterVol", 0.75f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVol", 0.75f);
        vfxSlider.value = PlayerPrefs.GetFloat("VFXVol", 0.75f);
        
        SetMasterVolume(_masterSlider.value);
        SetMusicVolume(musicSlider.value);
        SetVFXVolume(vfxSlider.value);
        
        _masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        vfxSlider.onValueChanged.AddListener(SetVFXVolume);
    }

    public void SetMasterVolume(float sliderValue)
    {
        float volume = Mathf.Log10(sliderValue) * 20;
        audioMixer.SetFloat("MasterVolume", volume);
        PlayerPrefs.SetFloat("MasterVol", sliderValue);
    }

    public void SetMusicVolume(float sliderValue)
    {
        float volume = Mathf.Log10(sliderValue) * 20;
        audioMixer.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetFloat("MusicVol", sliderValue);
    }

    public void SetVFXVolume(float sliderValue)
    {
        float volume = Mathf.Log10(sliderValue) * 20;
        audioMixer.SetFloat("VFXVolume", volume);
        PlayerPrefs.SetFloat("VFXVol", sliderValue);
        testAudioSource.PlayOneShot(testVFXClip);
    }

    void OnDestroy()
    {
        PlayerPrefs.Save();
        
        _masterSlider.onValueChanged.RemoveListener(SetMasterVolume);
        musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
        vfxSlider.onValueChanged.RemoveListener(SetVFXVolume);
    }
}
