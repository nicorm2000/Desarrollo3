using System.Collections;
using UnityEngine;

public class AnimatorUtility : MonoBehaviour
{
    public AnimationData[] animations;

    private SpriteRenderer spriteRenderer;

    private string walk = "Walk";
    private string idle = "Idle";
    private string spawn = "Spawn";
    private string death = "Death";

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            PlayAnimation(walk);
            Debug.Log("Walk Animation");
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            for (int i = 0; i < animations.Length; i++)
            {
                if (animations[i].animationName == "Idle")
                {
                    PlayAnimation(idle);
                    Debug.Log("Idle Animation");
                }
                else
                {
                    PlayAnimation(spawn);
                    Debug.Log("Spawn Animation");
                }
            }
            Debug.Log("Idle Animation");
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            PlayAnimation(death);
            Debug.Log("Death Animation");
        }
    }

    public void PlayAnimation(string animationName)
    {
        AnimationData animation = FindAnimation(animationName);
        if (animation != null)
        {
            StopAllCoroutines();
            StartCoroutine(AnimateFrames(animation.frames, animation.loop, animation.speed));
        }
        else
        {
            Debug.LogError("Animation not found: " + animationName);
        }
    }

    private AnimationData FindAnimation(string animationName)
    {
        foreach (AnimationData animation in animations)
        {
            if (animation.animationName == animationName)
            {
                return animation;
            }
        }
        return null;
    }

    private IEnumerator AnimateFrames(Sprite[] frames, bool loop, float speed)
    {
        int frameIndex = 0;
        float delay = 0.1f / speed;

        while (true)
        {
            spriteRenderer.sprite = frames[frameIndex];
            yield return new WaitForSeconds(delay);

            frameIndex++;
            if (frameIndex >= frames.Length)
            {
                if (loop)
                {
                    frameIndex = 0;
                    if (frames[frames.Length - 1])
                    {
                        Debug.Log("Animation finished: " + frames[frames.Length - 1]);
                    }
                }
                else
                {
                    Debug.Log("Animation finished: " + frames[frames.Length - 1]);
                    break;
                }
            }
        }
    }
}