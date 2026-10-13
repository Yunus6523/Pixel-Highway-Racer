using System;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    enum FollowMode { SmoothDamp, Lerp };

    [SerializeField] public Transform target;
    [SerializeField] FollowMode followMode;

    [SerializeField] float smoothTime;
    [SerializeField] float lerpSpeed;

    Vector3 offset;
    Vector3 velocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(followMode == FollowMode.SmoothDamp)
        {
            transform.position = Vector3.SmoothDamp(
                transform.position, target.position + offset, ref velocity, smoothTime
                );
        }
        else if(followMode == FollowMode.Lerp)
        {
            transform.position = Vector3.Lerp(
                transform.position, target.position + offset, lerpSpeed * Time.deltaTime
                );
        }
    }
}
