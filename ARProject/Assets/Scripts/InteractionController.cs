using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class InteractionController : MonoBehaviour
{
    public GameObject _WarriorPrefab;
    public Animator _anim;
    public AudioController _sfx;
    public Canvas _mainCanvas;

    private int hitCounter = 0;
    private bool isLoaded;

    // Start is called before the first frame update
    void Start()
    {
        // Get the reference of the warrior animator
        if(_WarriorPrefab)
            _anim = _WarriorPrefab.GetComponent<Animator>();

        _sfx = GetComponent<AudioController>();
    }

    private void OnMouseDown()
    {
        //A variable to store the random number with range (1 - 4)
        int random = Random.Range(1, 4);
        // Play random animation when the warrior gets hit on tap
        _anim.SetInteger("GetHit", random);
        Invoke("setToDefaultState", 0.2f);
        _sfx.Punch(random - 1);

        // Store the number of hit the warrior takes
        hitCounter += 1;

        // If the warrior takes 5 hits then he will die
        if(hitCounter == 5)
        {
            _anim.SetInteger("Dead", 1);
            _WarriorPrefab.GetComponentInChildren<Canvas>().enabled = true;
            _sfx.Defeat(0);
            hitCounter = 0;
        }
    }

    // Bring the warrior back to life
    public void Revive()
    {
        _anim.SetInteger("Dead", 2);
        _WarriorPrefab.GetComponentInChildren<Canvas>().enabled = false;
        print("REVIVED");
    }

    // Random Attack using UI Button
    public void Attack()
    {
        if(isLoaded)
        {
            int random = Random.Range(1, 4);
            _anim.SetInteger("Attack", random);
            Invoke("setToDefaultState", 0.2f);
            _sfx.Attack(random - 1);
        }
    }

    // Make the warrior jump
    public void Jump()
    {
        if (isLoaded)
        {
            _anim.SetInteger("Jump", 1);
            Invoke("setToDefaultState", 0.2f);
            _sfx.Jump(0);
        }
    }

    // Set the animator value to '0'
    // Called in animator event
    public void setToDefaultState()
    {
        _anim.SetInteger("GetHit", 0);
        _anim.SetInteger("Attack", 0);
        _anim.SetInteger("Jump", 0);
    }

    private void Update()
    {
        // If the warrior  visible 
        if (GameObject.FindWithTag("Warrior"))
        {
            isLoaded = true;
            _WarriorPrefab = GameObject.FindGameObjectWithTag("Warrior");
            _mainCanvas.enabled = true;
            _anim = _WarriorPrefab.GetComponent<Animator>();
            _WarriorPrefab.GetComponentInChildren<Button>().onClick.AddListener(Revive);

        }
        else
        {
            isLoaded = false;
            _mainCanvas.enabled = false;
        }
    }
}
