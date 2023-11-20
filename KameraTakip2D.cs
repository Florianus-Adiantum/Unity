using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameraTakip2D : MonoBehaviour
{
    //2d oyunlar icin takip kamerasi
    public float moveSpeed;//yumusak gecis ayari
    public Vector3 offset;//kameranin konumu
    public Transform player;//karakter
    private Camera cam;
    void Start()
    {
        cam = GetComponent<Camera>();
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, moveSpeed * Time.deltaTime);//kamera surekli olarak karakteri takip eder
    }
}
