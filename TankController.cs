using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public Transform hedef;

    AudioSource audioSource;
    Animator anim;

    public float donusHizi = 50f;
    public float ileriHareketHizi = 2f;
    public float geriHareketHizi = 0.5f;

    float yatay, dikey, speed;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        Donus();
        Gaz();
        Anim();
    }
    void Anim()
    {
        
        if(yatay<0)
        {
            yatay *= -1;          
        }
        if(dikey < 0)
        {
            dikey *= -1;
        }
        speed = yatay + dikey;
        anim.SetFloat("Speed", speed);
    }
    void Donus()
    {
        float yatayHareket = Input.GetAxis("Horizontal");
        yatay = yatayHareket;
        transform.Rotate(Vector3.forward * -yatayHareket * donusHizi * Time.deltaTime);
    }

    void Gaz()
    {
        float ileriGeriHareket = Input.GetAxis("Vertical");
        dikey = ileriGeriHareket;
        if (ileriGeriHareket > 0)
        {
            // ileri gitme
            Vector3 hedefYon = hedef.position - transform.position;
            float hedefAcisi = Mathf.Atan2(hedefYon.y, hedefYon.x) * Mathf.Rad2Deg - 90f;
            Quaternion hedefRotasyon = Quaternion.Euler(new Vector3(0, 0, hedefAcisi));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, hedefRotasyon, donusHizi * Time.deltaTime);

            transform.Translate(Vector3.up * ileriHareketHizi * Time.deltaTime);
            audioSource.pitch = 1.3f;
        }
        else if (ileriGeriHareket < 0)
        {
            // geri gitme
            Vector3 hedefYon = transform.position - hedef.position;
            float hedefAcisi = Mathf.Atan2(hedefYon.y, hedefYon.x) * Mathf.Rad2Deg - 90f;
            Quaternion hedefRotasyon = Quaternion.Euler(new Vector3(0, 0, hedefAcisi));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, hedefRotasyon, donusHizi * Time.deltaTime);

            transform.Translate(Vector3.down * geriHareketHizi * Time.deltaTime);
            audioSource.pitch = 1.2f;
        }
        else if (yatay != 0)
        {
            audioSource.pitch = 1.15f;
        }
        else
        {
            audioSource.pitch = 1;
        }
    }
}