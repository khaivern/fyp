using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] Slider volume;
    [SerializeField] float defaultVolume = 0.2f;
    // Start is called before the first frame update
    private void Awake()
    {
        SetUpSingleton();
    }

    void Start()
    {
        volume.value = defaultVolume;
    }


    void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var music = FindObjectOfType<MusicPlayer>();
        if (music)
        {
            music.SetVolume(volume.value);
        }
        else
        {
            Debug.Log("no music player game object in scene");
        }
    }

    
}
