using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}