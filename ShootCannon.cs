using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCannon : MonoBehaviour
{
    AudioSource audioSource;
    Animator anim;

    public Animator cam;
    public AudioClip cannon, machineGun;

    public Transform topNamlu, tufekNamlu;
    public GameObject top, mermi;
    public GameObject duman, patlama;

    float timer = 5;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Shoot();
    }
    void Shoot()
    {
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Cannon();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            StartCoroutine(MachineGun());
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            StopAllCoroutines();
        }
    }
    void Cannon()
    {
        if (timer > 5f)
        {
            timer = 0;
            audioSource.PlayOneShot(cannon, 1f);
            anim.SetTrigger("Shoot");
            cam.SetTrigger("Shoot");

            Pooling.SpawnObject(top, topNamlu.position);
            Pooling.SpawnObject(duman, topNamlu.position);
        }        
    }
    IEnumerator MachineGun()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            audioSource.PlayOneShot(machineGun, 0.4f);
            Pooling.SpawnObject(mermi, tufekNamlu.position);
            Pooling.SpawnObject(patlama, tufekNamlu.position);
        }
    }
}
