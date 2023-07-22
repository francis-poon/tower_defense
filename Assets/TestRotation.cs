using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform[] targets;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform target in targets)
        {
            float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            Debug.Log("Calculated Angle: " + angle + "\nQ Angle: " + targetRotation.eulerAngles);

            transform.rotation = targetRotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
