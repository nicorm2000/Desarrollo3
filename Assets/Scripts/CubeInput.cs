using System;
using UnityEngine;

public class CubeInput : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    public static event Action<Vector2> OnRightMouseButtonDown;
    public static event Action<Vector2> OnRightMouseButtonUp;

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 5f, 0) * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -5f, 0) * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-5f, 0, 0) * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(5f, 0, 0) * moveSpeed * Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(1))
        {
            OnRightMouseButtonDown?.Invoke(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(1))
        {
            OnRightMouseButtonUp?.Invoke(Input.mousePosition);
        }
    }
}