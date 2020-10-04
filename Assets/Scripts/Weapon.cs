using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public AudioClip sfx;
    private AudioSource _audioSource;
    public Transform shootPosition;
    public Transform anchorRotationPoint;

    public GameObject bulletPrefab;
    public GameObject weaponSprite;

    public Transform target;
    public float bulletSpeed = 50f;
    public SimpleCamShake simpleCamShake;

    // Start is called before the first frame update
    void Start()
    {
        simpleCamShake = GetComponent<SimpleCamShake>();
        transform.SetParent(transform.parent, true);
        _audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position;
        if (Input.GetMouseButtonDown(0))
        {
            ShootBullet();
        }

    }
    
    public void ShootBullet()
    {
        simpleCamShake.ShakeCamera();
        _audioSource.PlayOneShot(sfx);
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, rotZ);

        float distance = difference.magnitude;
        Vector2 direction = difference / distance;
        direction.Normalize();

        GameObject bulletObject = Instantiate(bulletPrefab, transform.position, rotation);
        bulletObject.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
    
    public static Vector3 MouseWorldPosition
    {
        get { return Camera.main.ScreenToWorldPoint(Input.mousePosition); }
    }
    
    public void LateUpdate()
    {
        // Rotate the Weapon
        Vector3 crosshairPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        
        float angle = Mathf.Atan2(crosshairPos.y, crosshairPos.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, bulletSpeed * Time.deltaTime);
        if (angle > 90.0f && angle > -90.0f)
        {
            transform.localScale = new Vector3(1, -1,1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1,1 );
        }
    }
    
}
