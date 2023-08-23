using System.Collections;
using UnityEngine;

public class ZoneTriggeredEffect : MonoBehaviour
{
    public EnemyDropData dropData;

    private bool isEffectTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsPlayerLayer(collision.gameObject) && !isEffectTriggered)
        {
            TriggerEffect();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsPlayerLayer(collision.gameObject))
        {
            isEffectTriggered = false;
        }
    }

    private bool IsPlayerLayer(GameObject obj)
    {
        return obj.layer == LayerMask.NameToLayer("Player");
    }

    private void TriggerEffect()
    {
        int splashIndex = Random.Range(0, dropData.splashSprites.Count); // Assuming you have a list of prefabs in dropData
        int materialIndex = Random.Range(0, dropData.materials.Count);

        GameObject splashPrefab = dropData.splashSprites[splashIndex]; // Get the correct prefab
        GameObject splashObject = Instantiate(splashPrefab, transform.position, Quaternion.identity);

        SpriteRenderer spriteRenderer = splashObject.GetComponent<SpriteRenderer>();
        spriteRenderer.material = dropData.materials[materialIndex];

        StartCoroutine(FadeOutAndDestroy(splashObject, spriteRenderer));

        isEffectTriggered = true;
    }

    private IEnumerator FadeOutAndDestroy(GameObject splashObject, SpriteRenderer spriteRenderer)
    {
        float startTime = Time.time;
        float elapsedTime = 0f;
        Color originalColor = spriteRenderer.color;

        while (elapsedTime < dropData.objectLifespan)
        {
            float normalizedTime = elapsedTime / dropData.objectLifespan;
            Color fadedColor = new Color(originalColor.r, originalColor.g, originalColor.b, 1f - normalizedTime);
            spriteRenderer.color = fadedColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(splashObject);
    }
}