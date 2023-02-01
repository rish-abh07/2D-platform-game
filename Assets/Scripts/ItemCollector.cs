using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    public int count = 0;
    [SerializeField] private TextMeshProUGUI cherries;
    [SerializeField] private AudioSource collectorAudio;
    // Start is called before the first frame update
    public void Start()
    {
        count = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("apple"))
        {
            collectorAudio.Play();
            Destroy(collision.gameObject);
            count += 1;
            cherries.text = "Apples :" + count;
           
        }
    }
}
