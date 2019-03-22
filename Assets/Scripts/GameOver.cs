using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public PlayerBehaviour playerhealth;
    public float restartDelay = 5f;
    public Animator anim;
    public Animator anim2;
    float restartTimer;
    
    void Update()
    {
        if (playerhealth.health <= 0 && playerhealth.isdead)
        {
            anim.SetTrigger("Death");
            anim2.SetTrigger("Death");

            restartTimer += Time.deltaTime;

            if (restartTimer >= restartDelay)
            {
                SceneManager.LoadSceneAsync("Level1",LoadSceneMode.Single);
            }
        }
    }
}
