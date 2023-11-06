using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    public event Action<bool> onPlayerDashChange;

    [Header("Player Data")]
    [SerializeField] private PlayerData playerData;

    [Header("Wave Data")]
    [SerializeField] private WaveManager waveManager;

    [Header("Dash")]
    [SerializeField] private Image dashImage;
    [SerializeField] private KeyCode dash = KeyCode.F2;
    [SerializeField] private Color dashColor = Color.cyan;
    private float dashCooldown = 3f;
    private float dashCounter = 0;
    private float dashCoolDownCounter = 0;

    [Header("Slower")]
    [SerializeField] private GameObject slowerLogo;
    [SerializeField] private Image slowerImage;
    [SerializeField] private KeyCode slower = KeyCode.F2;
    [SerializeField] private Color slowerColor = Color.cyan;
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private float slowerLifetime = 5f;
    [SerializeField] private float slowerZOffset = 5f;
    private float slowerCooldown = 3f;
    private bool isCooldownSlower = false;

    [Header("Laser")]
    [SerializeField] private GameObject laserLogo;
    [SerializeField] private Image laserImage;
    [SerializeField] private KeyCode laser = KeyCode.F3;
    [SerializeField] private Color laserColor = Color.cyan;
    [SerializeField] private GameObject laserObject;
    private float laserCooldown = 3f;
    private bool isCooldownLaser = false;

    private int targetLayer;

    private void Start()
    {
        targetLayer = LayerMask.NameToLayer("Slower");
        dashCooldown = playerData.dashCooldown;
        slowerCooldown = playerData.shieldCooldown;
        laserCooldown = playerData.laserCooldown;

        dashImage.fillAmount = 0f;
        slowerImage.fillAmount = 0f;
        laserImage.fillAmount = 0f;
    }

    private void Update()
    {
        Dash();
        Slower();
        Laser();
    }

    private void Laser()
    {
        if (waveManager.currentWaveIndex >= 9)
        {
            laserLogo.SetActive(true);
            if (Input.GetKeyDown(laser) && !isCooldownLaser)
            {
                isCooldownLaser = true;
                laserImage.fillAmount = 1f;

                StartCoroutine(ActivateLaser());
            }

            if (isCooldownLaser)
            {
                laserImage.fillAmount -= 1 / laserCooldown * Time.deltaTime;

                if (laserImage.fillAmount <= 0f)
                {
                    laserImage.fillAmount = 0f;
                    isCooldownLaser = false;
                }
            }
        }
    }

    private IEnumerator ActivateLaser()
    {
        laserObject.SetActive(true);
        yield return new WaitForSeconds(playerData.laserDuration);
        laserObject.SetActive(false);
    }

    private void Slower()
    {
        if (waveManager.currentWaveIndex >= 4)
        {
            slowerLogo.SetActive(true);
            if (Input.GetKeyDown(slower) && !isCooldownSlower)
            {
                isCooldownSlower = true;
                slowerImage.fillAmount = 1f;

                GameObject spawnedObject = Instantiate(prefabToSpawn, new Vector3(transform.position.x, transform.position.y, transform.position.z + slowerZOffset), Quaternion.Euler(-90f, 0f, 0f));

                StartCoroutine(DestroyAfterTime(spawnedObject));
            }

            if (isCooldownSlower)
            {
                slowerImage.fillAmount -= 1 / slowerCooldown * Time.deltaTime;

                if (slowerImage.fillAmount <= 0f)
                {
                    slowerImage.fillAmount = 0f;

                    isCooldownSlower = false;
                }
            }
        }
    }

    private IEnumerator DestroyAfterTime(GameObject obj)
    {
        yield return new WaitForSeconds(slowerLifetime);
    }

    private void Dash()
    {
        if (Input.GetKeyDown(dash) && dashCoolDownCounter <= 0 && dashCounter <= 0)
        {
            playerData.isDashing = true;
            onPlayerDashChange?.Invoke(playerData.isDashing);
            dashCoolDownCounter = playerData.dashCooldown;
            dashCounter = playerData.dashLength;
            playerData.activeMoveSpeed = playerData.dashSpeed;
            dashImage.fillAmount = 1f;
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                playerData.isDashing = false;
                onPlayerDashChange?.Invoke(playerData.isDashing);
                playerData.activeMoveSpeed = playerData.speed;
            }
        }

        if (dashCoolDownCounter > 0)
        {
            dashCoolDownCounter -= Time.deltaTime;
            dashImage.fillAmount = dashCoolDownCounter / playerData.dashCooldown;
        }
    }

    public void DestroySlowers()
    {
        GameObject[] slowerObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in slowerObjects)
        {
            if (obj.layer == targetLayer)
            {
                Destroy(obj);
            }
        }
    }
}