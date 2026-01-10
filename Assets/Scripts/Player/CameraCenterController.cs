using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCenterController : MonoBehaviour {

    [SerializeField] GameObject player;
    [SerializeField] float speed;
    [SerializeField] Vector3 distance;

    private void FixedUpdate()
    {
        Vector3 positionDesired = player.transform.position + distance;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, positionDesired, speed * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
     

}
