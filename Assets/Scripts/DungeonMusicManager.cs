using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonMusicManager : MonoBehaviour
{
    [Header("In Game Music Clips")]
    public AudioClip[] inGameMusic;
    public Text nowPlayingText;

    private AudioSource audioSource;
    private List<AudioClip> unplayedClips;
    private List<AudioClip> playedClips;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        unplayedClips = new List<AudioClip>();
        playedClips = new List<AudioClip>();

        foreach (AudioClip clip in inGameMusic)
        {
            unplayedClips.Add(clip);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInGameMusic();
    }

    private void UpdateInGameMusic()
    {
        // Recycle clips once all have been played
        if (playedClips.Count == inGameMusic.Length)
        {
            foreach (AudioClip clip in playedClips)
            {
                unplayedClips.Add(clip);
            }

            playedClips.Clear();
        }

        if (!audioSource.isPlaying)
        {
            var random = new System.Random();
            int index = random.Next(unplayedClips.Count);
            AudioClip nextClip = unplayedClips[index];

            audioSource.clip = nextClip;
            audioSource.Play();

            StartCoroutine(TriggerNowPlayingText());

            unplayedClips.Remove(nextClip);
            playedClips.Add(nextClip);
        }
    }

    private IEnumerator TriggerNowPlayingText()
    {
        nowPlayingText.text = string.Format("Now Playing:\n{0}", audioSource.clip.name);
        nowPlayingText.gameObject.SetActive(true);

        yield return new WaitForSeconds(5f);

        nowPlayingText.gameObject.SetActive(false);
    }
}
