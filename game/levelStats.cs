using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelStats : MonoBehaviour
{
    private GameObject player;
    public Text killz;    

    private AudioSource thisaudio;
    public AudioClip bossSpawnedClip;
    public AudioClip bossFightMusic;
    private int killcount;

    void Start()
    {
        killcount = 0;
        player = GameObject.Find("Player");
        thisaudio = GetComponent<AudioSource>();
        thisaudio.Play();
    }

    IEnumerator changeToBossMusic()
    {
        AudioSource.PlayClipAtPoint(bossSpawnedClip, player.transform.position);
        yield return new WaitForSeconds(2);
        thisaudio.Stop();
        AudioSource.PlayClipAtPoint(bossSpawnedClip, player.transform.position);
        yield return new WaitForSeconds(3);

        thisaudio.clip = bossFightMusic;
        thisaudio.volume = 1;
        thisaudio.Play();
    }

    public void mobKilled()
    {
        killcount++;
        setKillzUI();

        if (killcount == 50)
        {
            BossStats bs = GetComponent<BossStats>();
            bs.spawn();

            StartCoroutine(changeToBossMusic());
        }
    }

    private void setKillzUI()
    {
        killz.text = "Killz: " + killcount;
    }
}
