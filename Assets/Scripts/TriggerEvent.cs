using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent UnityEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("I collided with something");
        UnityEvent.Invoke();
    }
}
