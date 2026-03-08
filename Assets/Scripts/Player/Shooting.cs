using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    [Header("Ammo")]
    public int magazineSize = 6;
    public float reloadTime = 1.5f;

    private int currentAmmo;
    private bool isReloading = false;

    void Start()
    {
        currentAmmo = magazineSize;
    }

    public void Shoot()
    {
        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            Debug.Log("Out of ammo! Reload!");
            return;
        }

        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        currentAmmo--;

        Debug.Log("Ammo: " + currentAmmo + "/" + magazineSize);
    }

    public void Reload()
    {
        if (isReloading)
            return;

        if (currentAmmo == magazineSize)
            return;

        StartCoroutine(ReloadRoutine());
    }

    System.Collections.IEnumerator ReloadRoutine()
    {
        isReloading = true;

        Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = magazineSize;

        Debug.Log("Reloaded!");

        isReloading = false;
    }
}