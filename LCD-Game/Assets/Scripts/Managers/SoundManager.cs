using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static bool applicationIsQuitting;

    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                Debug.LogWarning("Instance of SoundManager called when application is quitting.\n"
                    + "Returning null instead.");
                return null;
            }

            if (_instance == null)
            {
                _instance = GameObject.Find("GameManager").GetComponent<SoundManager>();
            }

            return _instance;
        }
    }

    public Sound[] sounds;
    public AudioSource mainSource;

    public void PlaySound(string soundName)
    {
        Sound sound = Array.Find(sounds, x => x.soundName == soundName);

        if (sound == null)
        {
            Debug.LogErrorFormat("Tried to play undefined sound named \"{0}\"", soundName);
            return;
        }

        mainSource.PlayOneShot(sound.clip);
    }

    private void OnDestroy()
    {
        applicationIsQuitting = true;
    }
}
