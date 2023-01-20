using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthBox : MonoBehaviour
{
    private Animator boxAM;
    [SerializeField] private GameObject pineApple;
    [SerializeField] private AudioSource audioPlay;
    // Start is called before the first frame update
    void Start()
    {
        boxAM = GetComponent<Animator>();
        pineApple.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioPlay.Play();
        boxAM.SetBool("State", true);
        Invoke("Distruction", 1f);
    }
    private void Distruction()
    {
        audioPlay.Play();
        pineApple.SetActive(true) ;
        Destroy(gameObject);
    }

}
