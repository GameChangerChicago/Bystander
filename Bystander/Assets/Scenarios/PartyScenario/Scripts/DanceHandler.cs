using UnityEngine;
using System.Collections;

public class DanceHandler : MonoBehaviour
{
    public AudioSource BGMPlayer;
    public GameObject[] Dancers;

    // Update is called once per frame
    void Update()
    {
        if (BGMPlayer.clip.name != "IceRinkLoop")
        {
            Dancers[0].SetActive(false);
            Dancers[1].SetActive(true);
        }
        else
        {
            Dancers[0].SetActive(true);
            Dancers[1].SetActive(false);
        }
    }
}