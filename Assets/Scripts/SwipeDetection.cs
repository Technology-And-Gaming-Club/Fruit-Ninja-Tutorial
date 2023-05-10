using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{

    private InputManager manager;

    public GameObject trail;

    [SerializeField] private float minSwipeDistance = .5f;
    [SerializeField] private float sphereCastRadius = 0.1f;

    private Vector3 prevFramePos;
 
    void Awake()
    {
        manager = InputManager.Instance;
    }

    private void OnEnable()
    {
        manager.onTouchStarted += SwipeStarted;
        manager.onTouchEnded += SwipeEnded;
        manager.onTouchDelta += SwipeDelta;
    }

    private void OnDisable()
    {
        manager.onTouchStarted -= SwipeStarted;
        manager.onTouchEnded -= SwipeEnded;
    }

    void SwipeStarted(Vector2 position)
    {
        trail.SetActive(true);
        StartCoroutine(UpdateTrailPosition());
        prevFramePos = manager.PrimaryPosition();
    }

    void SwipeEnded(Vector2 position)
    {
        trail.SetActive(false);
        StopCoroutine(UpdateTrailPosition());
    }

    void SwipeDelta(Vector2 posiion)
    {
        Debug.Log("Swipe detected");
        Vector3 currentPosition = manager.PrimaryPosition();
        Vector3 direction = currentPosition - prevFramePos;
        float difference = direction.magnitude;

        

        if (difference >= minSwipeDistance)
        {
            Vector3 sphereStart = new Vector3(prevFramePos.x, prevFramePos.y, 0);

            RaycastHit[] raycastHits = Physics.SphereCastAll(sphereStart, sphereCastRadius, direction.normalized, sphereCastRadius);
            foreach (RaycastHit hit in raycastHits)
            {
                Debug.Log(hit);
                hit.transform.gameObject.SetActive(false);
            }
        }

        prevFramePos = currentPosition;
    }

    IEnumerator UpdateTrailPosition()
    {
        while (true)
        {
            trail.transform.position = manager.PrimaryPosition();
            yield return null;
        }
    }


}
