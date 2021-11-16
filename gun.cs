using UnityEngine;
using System.Collections;

public class gun : MonoBehaviour
{
    //recoil
    public float rotationSpeed = 6;
    public float returnSpeed = 25;

    public Vector3 RecoilRotation = new Vector3(10f, 10f, 10f);

    private Vector3 currentRotation;
    private Vector3 Rot;
    //recoil

    //ammo
    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;
    //ammo

    public float damage = 10f;//gun damage
    public float range = 70f;// gun range

    AudioSource m_shootingsound;//gun sound
    float timeLastShot = 0f;
    float delayBetweenShots = 0.37f;

    public Animator animator;
    AudioSource m_shootingsound;//reload sound


    public Camera fpsCam;
    public ParticleSystem muzzleFlash;//muzzle flash
    public GameObject impactEffect;//give damage
    
    void Start()
    {
        currentAmmo = maxAmmo;
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
        if (isReloading)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0) && (Time.time > timeLastShot + delayBetweenShots))
        {
            timeLastShot = Time.time;
            Shoot();
            Fire();
        }
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;

        isReloading = false;
    }

    void Shoot()
    {
        m_shootingsound = GetComponent<AudioSource>();
        currentAmmo--;
        muzzleFlash.Play();
        m_shootingsound.Play();

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
