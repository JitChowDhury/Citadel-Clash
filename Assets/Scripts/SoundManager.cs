using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance { get; private set; }
    public enum Sound
    {
        BuildingPlaced,
        BuildingDamaged,
        BuildingDestroyed,
        EnemyDie,
        EnemyHit,
        EnemyWaveStarting,
        GameOver,
    }
    private AudioSource audioSource;
    private Dictionary<Sound, AudioClip> soundAudioDictionary;
    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        soundAudioDictionary = new Dictionary<Sound, AudioClip>();
        foreach (Sound sound in System.Enum.GetValues(typeof(Sound)))
        {
            soundAudioDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }
    }

    public void PlaySound(Sound sound)
    {
        audioSource.PlayOneShot(soundAudioDictionary[sound]);
    }
}
