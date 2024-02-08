using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    //private PlayerControls _pc;
    [HideInInspector] public bool isDead;
    [HideInInspector] public bool isVictory;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        //_pc = GetComponent<PlayerControls>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (isDead && !isVictory) StartCoroutine(BadEnd());
        if (isVictory && !isDead) StartCoroutine(GoodEnd());
    }

    IEnumerator BadEnd()
    {
        _animator.Play("Die");
        yield return new WaitForSeconds(2.5f);
    }

    IEnumerator GoodEnd()
    {
        _animator.Play("Dance");
        yield return new WaitForSeconds(2.5f);
    }
}
