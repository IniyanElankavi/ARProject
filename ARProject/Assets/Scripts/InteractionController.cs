using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class InteractionController : MonoBehaviour
{
    public GameObject WarriorPrefab;
    public Animator anim;
    public AudioController sfx;
    int hitCounter = 0;
    bool isLoaded;
    // Start is called before the first frame update
    void Start()
    {
        // Get the reference of the warrior animator
        if(WarriorPrefab)
            anim = WarriorPrefab.GetComponent<Animator>();
        sfx = GetComponent<AudioController>();
    }

    private void OnMouseDown()
    {
        //A variable to store the random number with range (1 - 4)
        int random = Random.Range(1, 4);
        // Play random animation when the warrior gets hit on tap
        anim.SetInteger("GetHit", random);
        Invoke("setToDefaultState", 0.2f);
        sfx.Punch(random - 1);

        // Store the number of hit the warrior takes
        hitCounter += 1;

        // If the warrior takes 5 hits then he will die
        if(hitCounter == 5)
        {
            anim.SetInteger("Dead", 1);
            WarriorPrefab.GetComponentInChildren<Canvas>().enabled = true;
            sfx.Defeat(0);
            hitCounter = 0;
        }
    }

    // Bring the warrior back to life
    public void Revive()
    {
        anim.SetInteger("Dead", 2);
        WarriorPrefab.GetComponentInChildren<Canvas>().enabled = false;
        print("REVIVED");
    }

    // Random Attack using UI Button
    public void Attack()
    {
        if(isLoaded)
        {
            int random = Random.Range(1, 4);
            anim.SetInteger("Attack", random);
            Invoke("setToDefaultState", 0.2f);
            sfx.Attack(random - 1);
        }
       
    }

    // Make the warrior jump
    public void Jump()
    {
        if (isLoaded)
        {
            anim.SetInteger("Jump", 1);
            Invoke("setToDefaultState", 0.2f);
            sfx.Jump(0);
        }
    }

    // Set the animator value to '0'
    // Called in animator event
    public void setToDefaultState()
    {
        anim.SetInteger("GetHit", 0);
        anim.SetInteger("Attack", 0);
        anim.SetInteger("Jump", 0);
    }

    private void Update()
    {
        //if(!WarriorPrefab)
        //{
        //    WarriorPrefab = GameObject.FindGameObjectWithTag("Warrior");
        //    anim = WarriorPrefab.GetComponent<Animator>();
        //    WarriorPrefab.GetComponentInChildren<Button>().onClick.AddListener(Revive);
        //}


        // If the warrior  visible 
        if (GameObject.FindWithTag("Warrior"))
        {
            isLoaded = true;
            WarriorPrefab = GameObject.FindGameObjectWithTag("Warrior");
            anim = WarriorPrefab.GetComponent<Animator>();
            WarriorPrefab.GetComponentInChildren<Button>().onClick.AddListener(Revive);

        }
        else
        {
            isLoaded = false;
        }

    }
}
