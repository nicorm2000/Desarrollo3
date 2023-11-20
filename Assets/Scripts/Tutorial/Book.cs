using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField] private float pageSpeed = 0.5f;
    [SerializeField] private float waitTime = 1f;
    [SerializeField] private List<Transform> pages;
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject playButton;

    [Header("Audio Manager")]
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private string turnPage;

    private int _index = -1;
    private bool _rotate = false;

    private void Start()
    {
        InitialState();
    }

    public void InitialState()
    {
        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].transform.rotation = Quaternion.identity;
        }
        pages[0].SetAsLastSibling();
        backButton.SetActive(false);
    }

    public void RotateNext()
    {
        if (_rotate)
        {
            return;
        }
        if (!AudioManager.muteSFX)
        {
            audioManager.PlaySound(turnPage);
        }
        _index++;
        float angle = 180;
        NextButtonActions();
        pages[_index].SetAsLastSibling();
        StartCoroutine(Rotate(angle, true));
    }

    public void NextButtonActions()
    {
        if (!backButton.activeInHierarchy)
        {
            backButton.SetActive(true);
        }
        if (_index == pages.Count - 1)
        {
            nextButton.SetActive(false);
            StartCoroutine(ActivatePlayButton());
        }
    }

    public void RotateBack()
    {
        if (_rotate)
        {
            return;
        }

        if (!AudioManager.muteSFX)
        {
            audioManager.PlaySound(turnPage);
        }
        float angle = 0;
        pages[_index].SetAsLastSibling();
        BackButtonActions();
        StartCoroutine(Rotate(angle, false));
    }

    public void BackButtonActions()
    {
        if (!nextButton.activeInHierarchy)
        {
            nextButton.SetActive(true);
            playButton.SetActive(false);
        }
        if (_index - 1 == -1)
        {
            backButton.SetActive(false);
        }
    }

    private IEnumerator ActivatePlayButton()
    {
        yield return new WaitForSeconds(waitTime);
        playButton.SetActive(true);
    }

    private IEnumerator Rotate(float angle, bool forward)
    {
        float value = 0f;

        while (true)
        {
            _rotate = true;
            Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
            value += Time.deltaTime * pageSpeed;
            pages[_index].rotation = Quaternion.Slerp(pages[_index].rotation, targetRotation, value);
            float angle1 = Quaternion.Angle(pages[_index].rotation, targetRotation);
            if (angle1 < 0.1f)
            {
                if (!forward)
                {
                    _index--;
                }
                _rotate = false;
                break;
            }
            yield return null;
        }
    }
}