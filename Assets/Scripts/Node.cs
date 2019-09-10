using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private GameObject cannon;
    private Vector3 cannonPositionDifference;

    public GameObject cannonPrefab;

    private void Start()
    {
        cannonPositionDifference = new Vector3(0f, 0.75f, 0f);
    }

    private void OnMouseDown()
    {
        if (cannon == null)
        {
            cannon = Instantiate(cannonPrefab, transform.position + cannonPositionDifference, transform.rotation) as GameObject;
        }
        else
        {
            Debug.Log("Already placed.");
        }
    }
}
