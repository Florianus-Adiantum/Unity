using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDetection : MonoBehaviour
{
    //bu kodu aracinizi hareket ettirdiginiz koda eklerseniz anlik hizinizi kontrol etmeyi saglar
    public Rigidbody rb; 
    public float speed;
    public float topSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //aracin rigidbodysine erisiyoruz
    }

    
    void Update()
    {
        speed = CarSpeed(); //fonksiyonu oyun icinde calistiriyoruz
    }
     public float CarSpeed()
    {
        //bu metod ile aracin anlik hizini inspector kisminda gorebilirsiniz
        float speed = rb.velocity.magnitude;
        speed *= 3.6f;
        if (speed < topSpeed)
        {
            rb.velocity = (topSpeed / 3.6f) * rb.velocity.normalized;
        }
        return speed;
    }
}
