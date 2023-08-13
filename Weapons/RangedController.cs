using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RangedController : MonoBehaviour, IDataPersistance
{
    [SerializeField] private string id; //DO NOT CHANGE

    [ContextMenu("Generate GUID for ID")]
    private void GenerateGUID()
    {
        id = System.Guid.NewGuid().ToString();
    }

    [Header("References")]
    [SerializeField] GunData gunData;
    [SerializeField] GameManager gameManager;
    [SerializeField] AmmoCounter ammoCounter;
    [SerializeField] Transform playerCamera;

    [Header("Gun Data")]
    [SerializeField] int ammoInClip;
    [SerializeField] int reloadTime;

    [Header("Key Binds")]
    [SerializeField] KeyCode reloadKey;

    float timeSinceLastShot;

    private void OnDisable()
    {
        gunData.isReloading = false;
    }

    public void StartReload()
    {
        if (!gunData.isReloading && gameManager.currentAmmo > 0 && this.gameObject.activeSelf)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        gunData.isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        if (gameManager.currentAmmo + ammoInClip - gunData.magazineSize >= 0)
        {
            gameManager.currentAmmo -= gunData.magazineSize - ammoInClip;
            ammoInClip = gunData.magazineSize;
        }
        else
        {
            ammoInClip += gameManager.currentAmmo;
            gameManager.currentAmmo = 0;
        }

        ammoCounter.UpdateAmmoUI(ammoInClip, gameManager.currentAmmo);
        gunData.isReloading = false;

    }

    private bool CanShoot()
    {
        return !gunData.isReloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);
    }

    public void Shoot()
    {
        if(ammoInClip > 0)
        {
            if(CanShoot())
            {
                for (int i = 0; i < gunData.bulletsPerShot; i++)
                {
                    Vector3 shotDirection = transform.forward + new Vector3(Random.Range(-gunData.spread * 0.1f, gunData.spread * 0.1f), Random.Range(-gunData.spread * 0.1f, gunData.spread * 0.1f), 0);

                    if (Physics.Raycast(playerCamera.position, shotDirection, out RaycastHit hitInfo, gunData.maxDistance))
                    {
                        IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                        damageable?.TakeDamage(gunData.damage);
                    }

                    if (gameManager.showShotDirection)
                    {
                        Debug.DrawRay(playerCamera.position, shotDirection, Color.red, 10);
                    }
                }

                ammoInClip--;
                timeSinceLastShot = 0;
                OnGunShot();
            }
        }   
    }

    private void OnGunShot()
    {
        //TODO - Recoil
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if (Input.GetKeyDown(reloadKey))
        {
            Invoke("StartReload", 0);
        }

        if (gunData.isAutomatic)
        {
            if (Input.GetMouseButton(0))
            {
                Invoke("Shoot", 0);
            }
        } 
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Invoke("Shoot", 0);
            }
        }

        ammoCounter.UpdateAmmoUI(ammoInClip, gameManager.currentAmmo);
    }

    public void LoadData(GameData data)
    {
        this.ammoInClip = data.weaponsInClipAmmo[id];
    }

    public void SaveData(GameData data)
    {
        if (data.weaponsInClipAmmo.ContainsKey(id))
        {
            data.weaponsInClipAmmo.Remove(id);
        }
        data.weaponsInClipAmmo.Add(id, this.ammoInClip);
    }
}
