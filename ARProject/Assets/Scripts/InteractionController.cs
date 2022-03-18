using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class InteractionController : MonoBehaviour
{
    public GameObject _WarriorPrefab;
    public Animator _anim, _PopupAnim;
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

    public  void  OnTouch()
    {
        // If the warrior takes 5 hits he will die
        if(hitCounter == 5)
        {
            _anim.SetInteger("Dead", 1);
            _WarriorPrefab.GetComponentInChildren<Canvas>().enabled = true;
            _sfx.Defeat(0);
            _mainCanvas.enabled = false;
            hitCounter = 0;
        }
        else if(_anim.GetInteger("Dead") == 1)
        {           
            Invoke("Revive", 0.1f);
            _mainCanvas.enabled = true;
        }
        else
        {
            _mainCanvas.enabled = true;
            int random = Random.Range(1, 4);

            // Play random animation when the warrior gets hit on tap
            _anim.SetInteger("GetHit", random);
            Invoke("setToDefaultState", 0.2f);
            _sfx.Punch(random - 1);

            // Store the number of hit the warrior takes
            hitCounter += 1;
        }
    }

    // Bring the warrior back to life
    public void Revive()
    {
         Debug.Log("REVIVED");
        _anim.SetInteger("Dead", 2);
        _WarriorPrefab.GetComponentInChildren<Canvas>().enabled = false;
 
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
    public void setToDefaultState()
    {
        _anim.SetInteger("GetHit", 0);
        _anim.SetInteger("Attack", 0);
        _anim.SetInteger("Jump", 0);
    }

    public void ExitPopup()
    {
        _PopupAnim.SetBool("Exit", true);
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
        }
        else
        {
            isLoaded = false;
            _mainCanvas.enabled = false;
        }
    }
}
