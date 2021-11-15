using UnityEngine;

public class gun : MonoBehaviour
{
    public float rotationSpeed = 6;
    public float returnSpeed = 25;

    public Vector3 RecoilRotation = new Vector3(10f, 10f, 10f);

    private Vector3 currentRotation;
    private Vector3 Rot;

    public float damage = 10f;
    public float range = 70f;


    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

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
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            Fire();
        }
    }


    void Shoot()
    {
        muzzleFlash.Play();
        

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
