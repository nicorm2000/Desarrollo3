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
    [SerializeField] private GameObject dashLogo;
    [SerializeField] private GameObject dashText;
    [SerializeField] private Image dashImage;
    [SerializeField] private Color dashColor = Color.cyan;
    [SerializeField] private ParticleSystem dustParticles;
    private float dashCooldown = 3f;
    private float dashCounter = 0;
    private float dashCoolDownCounter = 0;

    [Header("Slower")]
    [SerializeField] private GameObject slowerLogo;
    [SerializeField] private GameObject slowerText;
    [SerializeField] private Image slowerImage;
    [SerializeField] private Color slowerColor = Color.cyan;
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private float slowerLifetime = 5f;
    [SerializeField] private float slowerZOffset = 5f;
    [SerializeField] private int waveToUnlockSlower;
    private float slowerCooldown = 3f;
    private bool isCooldownSlower = false;


    [Header("Laser")]
    [SerializeField] private GameObject laserLogo;
    [SerializeField] private GameObject laserText;
    [SerializeField] private Image laserImage;
    [SerializeField] private Color laserColor = Color.cyan;
    [SerializeField] private GameObject laserObject;
    [SerializeField] private int waveToUnlockLaser;  

    [Header("Audio Manager")]
    [SerializeField] AudioManager audioManager;
    [SerializeField] string roll;
    [SerializeField] string splat;
    [SerializeField] string laser;

    private float laserCooldown = 3f;
    private bool isCooldownLaser = false;

    private int targetLayer;

    private void Start()
    {
        targetLayer = LayerMask.NameToLayer("Slower");
        dashCooldown = playerData.dashCooldown;
        slowerCooldown = playerData.slowerCooldown;
        laserCooldown = playerData.laserCooldown;

        dashImage.fillAmount = 0f;
        slowerImage.fillAmount = 0f;
        laserImage.fillAmount = 0f;
    }

    private void Update()
    {
        DashCounter();
        SlowerCounter();
        LaserCounter();
    }

    private void LaserCounter()
    {
        if (waveManager.currentWaveIndex >= waveToUnlockLaser)
        {
            StartCoroutine(EnableAnimatorComponent(laserLogo));

            laserText.SetActive(true);

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

    public void LaserLogic()
    {
        if (waveManager.currentWaveIndex >= waveToUnlockLaser)
        {
            if (!isCooldownLaser)
            {
                if (!AudioManager.muteSFX)
                {
                    audioManager.PlaySound(laser);
                }
                isCooldownLaser = true;
                laserImage.fillAmount = 1f;

                StartCoroutine(ActivateLaser());
            }
        }
    }

    private IEnumerator ActivateLaser()
    {
        laserObject.SetActive(true);
        yield return new WaitForSeconds(playerData.laserDuration);
        laserObject.SetActive(false);
    }

    private void SlowerCounter()
    {
        if (waveManager.currentWaveIndex >= waveToUnlockSlower)
        {
            StartCoroutine(EnableAnimatorComponent(slowerLogo));

            slowerText.SetActive(true);

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

    public void SlowerLogic()
    {
        if (waveManager.currentWaveIndex >= waveToUnlockSlower)
        {
            if (!isCooldownSlower)
            {
                if (!AudioManager.muteSFX)
                {
                    audioManager.PlaySound(splat);
                }
                isCooldownSlower = true;
                slowerImage.fillAmount = 1f;

                GameObject spawnedObject = Instantiate(prefabToSpawn, new Vector3(transform.position.x, transform.position.y, transform.position.z + slowerZOffset), Quaternion.Euler(-90f, 0f, 0f));

                StartCoroutine(DestroyAfterTime(spawnedObject));
            }
        }
    }

    private IEnumerator DestroyAfterTime(GameObject obj)
    {
        yield return new WaitForSeconds(slowerLifetime);
    }

    private void DashCounter()
    {
        dashLogo.SetActive(true);
        dashText.SetActive(true);

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

    public void DashLogic()
    {
        if (dashCoolDownCounter <= 0 && dashCounter <= 0)
        {
            if (!AudioManager.muteSFX)
            {
                audioManager.PlaySound(roll);
            }
            playerData.isDashing = true;
            dustParticles.Play();
            onPlayerDashChange?.Invoke(playerData.isDashing);
            dashCoolDownCounter = playerData.dashCooldown;
            dashCounter = playerData.dashLength;
            playerData.activeMoveSpeed = playerData.dashSpeed;
            dashImage.fillAmount = 1f;
        }
    }

    public void DestroySlowers()
    {
        GameObject[] slowerObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in slowerObjects)
        {
            if (obj.layer == targetLayer)
            {
                Destroy(obj);
            }
        }
    }

    private IEnumerator EnableAnimatorComponent(GameObject objectAnimator) 
    {
        float timeToWait = 5;

        objectAnimator.SetActive(true);
        yield return new WaitForSeconds(timeToWait);
        objectAnimator.GetComponent<Animator>().enabled = false;
    }
}