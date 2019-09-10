using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public InputField playerName;

    private GameObject exclamationMark;
    private GameObject cannon;
    private GameObject target;

    private Transform cannonFinalDestination;
    private bool rotationInProgress;

    private void Awake()
    {
        exclamationMark = Resources.FindObjectsOfTypeAll<GameObject>().Where(x => x.name == "EmptyName").First();
        cannon = GameObject.Find("Cannon");
        target = GameObject.Find("Target");

        cannonFinalDestination = GameObject.Find("CannonFinalDestination").transform;
    }

    private void Update()
    {
        Vector3 direction = cannonFinalDestination.position - cannon.transform.position;

        cannon.transform.Translate(direction.normalized * 20 * Time.deltaTime, Space.World);

        if (Vector3.Distance(cannon.transform.position, cannonFinalDestination.position) <= 0.5f)
        {
            cannon.transform.position = cannonFinalDestination.position;
        }
    }

    IEnumerator RotateCannon(float angle, Vector3 fAxis, Vector3 sAxis, float inTime)
    {
        rotationInProgress = true;

        float rotationSpeedD = (angle/2) / inTime;
        float rotationSpeedL = angle / inTime;

        Quaternion startRotation = cannon.transform.rotation;

        float deltaAngle = 0;

        while (deltaAngle < angle)
        {
            deltaAngle += rotationSpeedL * Time.deltaTime;
            deltaAngle = Mathf.Min(deltaAngle, angle);

            cannon.transform.rotation = startRotation * Quaternion.AngleAxis(deltaAngle, sAxis);

            yield return null;
        }

        deltaAngle = 0;
        startRotation = cannon.transform.rotation;
        
        while (deltaAngle < angle/2)
        {
            deltaAngle += rotationSpeedD * Time.deltaTime;
            deltaAngle = Mathf.Min(deltaAngle, angle/2);

            cannon.transform.rotation = startRotation * Quaternion.AngleAxis(deltaAngle, fAxis);

            yield return null;
        }

        rotationInProgress = false;
    }

    IEnumerator StartGame()
    {
        Turret turret = cannon.gameObject.GetComponent<Turret>();

        while (rotationInProgress)
            yield return new WaitForSeconds(0.1f);
        
        turret.DirectTarget(target.transform);

        yield return new WaitForSeconds(0.6f);

        SceneManager.LoadScene("GameScene");
    }

    public void ChangeScene()
    {
        if (playerName.text.Length < 1)
        {
            exclamationMark.SetActive(true);
            Debug.Log("Empty name");
        }
        else
        {
            if (exclamationMark.activeInHierarchy)
                exclamationMark.SetActive(false);

            UiManager.playerName = playerName.text;

            StartCoroutine(RotateCannon(20, Vector3.down, Vector3.left, 1));
            StartCoroutine(StartGame());
        }
    }
}
