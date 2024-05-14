using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ShootController : MonoBehaviour
{
    [Header("COMPONENTS")]
    public GameObject bulletPrefab;//muhimmat
    public GameObject bulletcasing;//bos kovan
    public GameObject bulletLight;//mermi isigi
    public GameObject bulletSmoke;//duman
    public Transform namlu, casingPoint;//mermi cikis noktasi ve mermi hedefi
    private GameObject target, guns;//hedef
    CameraAnims mainCamera;//main camera

    [Header("AUDIO")]
    AudioSource audioSource;
    public AudioClip shootSound, reloadSound, emptySound;

    [Header("BULLET MAGAZINE")]
    public int maxBullets = 32;//mermi kapasitesi
    public int maxMagazines = 4;//sarjor kapasitesi
    public int bulletsInMagazine;
    public int magazines;

    [Header("WEAPON SETTINGS")]
    public float fireSpeedAuto = 0.08f;//otomatik modda mermi atis araligi
    public float fireSpeedSemi = 0f;//yari otomatk modda mermi atis araligi
    public float reloadSpeed = 1.4f;//sarjor yenileme hizi
    public float vibratoForce = 0.5f;//tepme kuvveti
    public float vibratoTimer = 1f;//sacilma suresi
    public float recoilForce = 1f;//sacilma kuvveti
    public int weight = 5;//agirlik

    [Header("BOOL SETTINGS")]
    public bool isAuto = false;//silah otomatik modda mi
    public bool isRevolver = false;//toplu tabanca mi
    private bool isReloading = false;
    private bool isShooting = false;
    private bool canShoot = true;
    private Animator anim;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        mainCamera = FindObjectOfType<CameraAnims>();
        target = GameObject.Find("ShootTarget");
        guns = GameObject.Find("Hand");
        bulletsInMagazine = maxBullets;
        magazines = maxMagazines;
    }
    private void OnDisable()//obje setactive false oldugunda hatalari onlemek icin degiskenleri sifirla
    {
        isReloading = false;
        isShooting = false;
        canShoot = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isShooting && isAuto && !Player_Movements.isCrouching)//sol tik, atis yapilmiyorsa ve otomatik modda ise
            {
                StartCoroutine(ContinuousShooting());//otomatik modda ates
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0) && !isAuto && canShoot && !Player_Movements.isCrouching)//sol tik, atis yapilabilirse ve yari otomatik modda ise
            {
                ShootSemi();//yari otomatik atis
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))//sol tik bittiginde
            {
                
                StopCoroutine(ContinuousShooting());//otomatik modda atisi kes
                isShooting = false;
                guns.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            }
            if (Input.GetKeyDown(KeyCode.R) && magazines > 0 && bulletsInMagazine < maxBullets && !isReloading && !Player_Movements.isCrouching)
            {
                Reload();
            }
    }
    IEnumerator ContinuousShooting()
    {
        isShooting = true;
        float elapsedTime = 0f;//sayac

        while (isShooting && bulletsInMagazine > 0)
        {
            float randomizedTarget;
            elapsedTime += Time.deltaTime;
            //ilk saniyede duzgun atis yap
            if (elapsedTime <= vibratoTimer/4)
            {
                Shoot();
            }
            //burdan sonra sacilma baslasin
            else if (elapsedTime <= vibratoTimer)
            {
                randomizedTarget = Random.Range(recoilForce/4 * -1, recoilForce/2);
                guns.transform.localRotation = Quaternion.Euler(0f, 0f, randomizedTarget);
                Shoot();
            }
            else
            {
                randomizedTarget = Random.Range(recoilForce/2 * -1, recoilForce);
                guns.transform.localRotation = Quaternion.Euler(0f, 0f, randomizedTarget);
                Shoot();
            }
            yield return new WaitForSeconds(fireSpeedAuto); //belli araliklarla mermi at   
        }
        if (bulletsInMagazine == 0 && magazines > 0 && !isReloading)//mermi biterse ve sarjor varsa
        {
            Reload();
        }
        else if (bulletsInMagazine == 0 && magazines == 0)//mermi ve sarjor bitmisse
        {
            Empty();
        }
    }
    void Shoot()
    {
        if (!isRevolver)
        {
            GameObject bulletCasing = Pooling.SpawnObject(bulletcasing, casingPoint.position);
            Rigidbody2D rb = bulletCasing.GetComponent<Rigidbody2D>();
        }
        
        StartCoroutine(BulletLight());

        GameObject bullet = Pooling.SpawnObject(bulletPrefab, namlu.position); // mermi uret
        Bullet bulletComponent = bullet.GetComponent<Bullet>();

        if (bulletComponent)
        {
            bulletComponent.target = target.GetComponent<Transform>();//hedefi belirle
        }

        audioSource.PlayOneShot(shootSound, 1f);//atis sesi
        mainCamera.Shake(0.2f, vibratoForce);//kamera shake
        bulletsInMagazine -= 1;//sarjorden mermi azalt
    }
    void ShootSemi()
    {
        if (bulletsInMagazine > 0 && canShoot)
        {
            Shoot();//atis yap
            StartCoroutine(DelayNextShot());//atisa hazir olana kadar bekle
        }
        else if (bulletsInMagazine == 0 && magazines > 0 && !isReloading)//mermi biterse ve sarjor varsa
        {
            Reload();
        }
        else if (bulletsInMagazine == 0 && magazines == 0)//mermi ve sarjor bitmisse
        {
            Empty();
        }
    }
    void Reload()
    {
        StartCoroutine(ReloadWithDelay(reloadSpeed));//reload baslat
        if (isRevolver)
        {
            for(int i = 0; i < maxBullets; i++)
            {
                GameObject bulletCasing = Pooling.SpawnObject(bulletcasing, casingPoint.position);
                Rigidbody2D rb = bulletCasing.GetComponent<Rigidbody2D>();
                bulletcasing.GetComponent<Bullet_Casing>().horizontalForce = 5f;
            }
        }
    }
    void Empty()
    {
        audioSource.PlayOneShot(emptySound, 1f);//bosa tetik dusurme sesi
    }
    IEnumerator ReloadWithDelay(float delayTime)
    {
        isReloading = true;
        audioSource.PlayOneShot(reloadSound, 1f);//reload sesi

        yield return new WaitForSeconds(delayTime);//muhimmati doldurmayi bekle

        int bulletsToReload = maxBullets - bulletsInMagazine;//sarjordeki eksik mermiyi hesapla
        bulletsInMagazine += bulletsToReload;//eksigini sarjore ekle
        magazines -= 1;//yedek sarjorden bir adet azalt
        isReloading = false;
    }
    IEnumerator DelayNextShot()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireSpeedSemi);//yari otomatik modda her atista bekle
        canShoot = true;
    }
    IEnumerator BulletLight()
    {
        bulletLight.SetActive(true);
        GameObject smoke = Instantiate(bulletSmoke, namlu.position, Quaternion.identity);
        Destroy(smoke, 1f);
        yield return new WaitForSeconds(0.02f);
        bulletLight.SetActive(false);
    }
}
