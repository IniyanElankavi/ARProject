using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource source;

    public AudioClip[] AttackClip, PunchClip, DefeatClip, JumpClip;

    public void Attack(int i)
    {
        source.PlayOneShot(AttackClip[i]);
    }

    public void Punch(int i)
    {
        source.PlayOneShot(PunchClip[i]);
    }

    public void Defeat(int i)
    {
        source.PlayOneShot(DefeatClip[i]);
    }

    public void Jump(int i)
    {
        source.PlayOneShot(JumpClip[i]);
    }
}
