using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("金鑰匙")]
    public GameObject key;
    [Header("開門音效")]
    public AudioClip doorsound;



    private Animator ani;
    private AudioSource aud;

    private void Start()
    {
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "戰士" && key == null)
        {
            ani.SetTrigger("開門觸發");
            aud.PlayOneShot(doorsound, Random.Range(1.2f, 1.5f));
        }
    }

}
