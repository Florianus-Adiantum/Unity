using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GunShootScript : MonoBehaviour
{
    //Bu kodu kullanabilmek icin bir silah ve mermi prefablari olusturun
    //Mermi icin rigidbody ekleyin
    //Silahin ucunda merminin spawn olup firlamasi icin namlu adinda bir obje olusturun ve silah prefabi icinde oldugundan emin olun
    //Bu kodu oyun karakterinize baglayin
    //Mermi kismina mermi prefabini mermiPos kismina namlu objesini baglayin
    //Artik sol mouse tusuna bastiginizda mermi silahinizin ucundaki namlu noktasinda spawn olup dumduz sekilde ilerleyip 2 saniye sonra yok olacaktir
    //Mermi hizini ve yok olma süresini asagidan degistirebilirsiniz
    public GameObject mermi;
    public Transform mermiPos;
    void Start()
    {
       
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject go = Instantiate(mermi, mermiPos.position, mermiPos.rotation) as GameObject;
            go.GetComponent<Rigidbody>().velocity = mermiPos.transform.forward * 200f;
            Destroy(go.gameObject, 2f);
        }
          
            
    }
}
