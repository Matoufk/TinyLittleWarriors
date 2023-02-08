using UnityEngine;
using System.Collections;

public class MusicHandler : MonoBehaviour
{

    public AudioSource _AudioSource1;
    public AudioSource _AudioSource2;

    private bool battle_music = false;

    void Start()
    {

        _AudioSource1.Play();

    }


    void Update()
    {

        if(((GameObject.Find("Surroundings")).GetComponent<PlacementBehavior>()) != null && ((GameObject.Find("Surroundings")).GetComponent<PlacementBehavior>()).battle != battle_music)
        {
            battle_music = !battle_music;

            if (battle_music)
            {

                if (_AudioSource1.isPlaying)
                {

                    _AudioSource1.Stop();

                    _AudioSource2.Play();

                }

                else
                {

                    _AudioSource2.Stop();

                    _AudioSource1.Play();

                }

            }
        }

        

    }

}
