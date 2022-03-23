using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameraKontrol : MonoBehaviour
{
    public GameObject camera;
    

    float hassasiyet = 5f;
    float yumusaklik = 2f;
    Vector2 gecisPos;
    Vector2 camPos;
    public GameObject oyuncu;
    void Start()
    {
        oyuncu = transform.parent.gameObject;
    }

    
    void Update()
    {
        Vector2 farepos = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        farepos = Vector2.Scale(farepos, new Vector2(hassasiyet*yumusaklik,hassasiyet*yumusaklik));
        gecisPos.x = Mathf.Lerp(gecisPos.x, farepos.x, 1f / yumusaklik);
        gecisPos.y = Mathf.Lerp(gecisPos.y, farepos.y, 1f / yumusaklik);
        camPos += gecisPos;
        transform.localRotation = Quaternion.AngleAxis(-camPos.y, Vector3.right);
        oyuncu.transform.localRotation = Quaternion.AngleAxis(camPos.x, oyuncu.transform.up);
    }
}
