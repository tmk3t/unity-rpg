using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{

    private float rotateCounter = 0f;
    private float chargeCounter = 0f;
    private bool isCharging = false;

    public GameObject target = null;

    public GameObject bulletPrefab = null;
    public float shootPower = 300f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (rotateCounter >= 5f)
        {
            transform.LookAt(target.transform);
            isCharging = true;
            rotateCounter = 0;
        }


        if (chargeCounter >= 2f)
        {
            Shoot();
            isCharging = false;
            chargeCounter = 0f;
        }


        rotateCounter += Time.deltaTime;
        if (isCharging == true)
        {
            chargeCounter += Time.deltaTime;
        }
    }

    public void Shoot()
    {

        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * shootPower);


    }


}
