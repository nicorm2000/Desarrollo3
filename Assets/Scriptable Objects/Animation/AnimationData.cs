using UnityEngine;

[CreateAssetMenu(fileName = "AnimationData", menuName = "Custom/Animation Data")]
public class AnimationData : ScriptableObject
{
    public string animationName;
    public Sprite[] frames;
    public bool loop;
    public float speed;
}