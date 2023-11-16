using System.Collections;
using UnityEngine;

public class ZoneTriggeredEffect : MonoBehaviour
{
    public EnemyDropData dropData;

    public void TriggerEffect()
    {
        int splashIndex = Random.Range(0, dropData.splashSprites.Count);
        int materialIndex = Random.Range(0, dropData.materials.Count);

        GameObject splashPrefab = dropData.splashSprites[splashIndex];
        GameObject splashObject = Instantiate(splashPrefab, transform.position, Quaternion.identity);

        SpriteRenderer spriteRenderer = splashObject.GetComponent<SpriteRenderer>();
        spriteRenderer.material = dropData.materials[materialIndex];

        StartCoroutine(FadeOutAndDestroy(splashObject, spriteRenderer));
    }

    private IEnumerator FadeOutAndDestroy(GameObject splashObject, SpriteRenderer spriteRenderer)
    {
        float startTime = Time.time;
        float elapsedTime = 0f;
        Color originalColor = spriteRenderer.color;

        while (elapsedTime <= dropData.objectLifespan)
        {
            float normalizedTime = elapsedTime / dropData.objectLifespan;
            Color fadedColor = new Color(originalColor.r, originalColor.g, originalColor.b, 1f - normalizedTime);
            spriteRenderer.color = fadedColor;

            elapsedTime = Time.time - startTime;
            Debug.Log(elapsedTime);
            yield return null;
        }
        Debug.Log("Coroutine completed");
        Destroy(splashObject);
    }
}