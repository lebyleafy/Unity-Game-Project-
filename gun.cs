using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gun : MonoBehaviour
{

    AudioSource ASShootingsound;//shooting sound
    AudioSource ASReloadsound;//reload sound
    //recoil
    public float rotationSpeed = 6;
    public float returnSpeed = 25;

    public Vector3 RecoilRotation = new Vector3(10f, 10f, 10f);

    private Vector3 currentRotation;
    private Vector3 Rot;


    //ammo counter
    public int ammo;
    public bool isFiring;
    public Text ammoDisplay;


    //ammo
    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;
    public Animator animator;
    public AudioClip m_reloading;//reload sound
    //ammo


    public float damage = 10f;//gun damage
    public float range = 70f;// gun range

    
    public AudioClip m_shooting;//shooting sound
    float timeLastShot = 0f;
    float delayBetweenShots = 0.37f;


    public Camera fpsCam;
    public ParticleSystem muzzleFlash;//muzzle flash
    public GameObject impactEffect;//give damage
    
    void Start()
    {
        currentAmmo = maxAmmo;
        ASReloadsound = gameObject.AddComponent<AudioSource>();
        ASShootingsound = gameObject.AddComponent<AudioSource>();
    }
    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    void FixedUpdate()
    {
        currentRotation = Vector3.Lerp(currentRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        Rot = Vector3.Slerp(Rot, currentRotation, rotationSpeed * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(Rot);
    }
    public void Fire() {
            currentRotation += new Vector3(-RecoilRotation.x, Random.Range(-RecoilRotation.y, RecoilRotation.y), Random.Range(-RecoilRotation.z, RecoilRotation.z));
    }


    // Update is called once per frame
    void Update()
    {
        ammoDisplay.text = ammo.ToString();
        if (isReloading)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0) && (Time.time > timeLastShot + delayBetweenShots) && !isFiring && ammo > 0)
        {
            timeLastShot = Time.time;
            isFiring = true;
            Shoot();
            Fire();
            isFiring = false;
        }
        if (currentAmmo <= 0 || (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo))
        {
            timeLastShot = Time.time;
            StartCoroutine(Reload());
            return;
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;

        ASReloadsound.clip = m_reloading;
        ASReloadsound.Play();

        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime - .1f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.1f);
        currentAmmo = maxAmmo;
        ammo = maxAmmo;// goes back to max ammo after reload
        isReloading = false;
    }

    void Shoot()
    {
       
        currentAmmo--;
        ammo--;//-1 bullet every shoot
        muzzleFlash.Play();
        ASShootingsound.clip = m_shooting;
        ASShootingsound.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {

     

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }

    }


}
