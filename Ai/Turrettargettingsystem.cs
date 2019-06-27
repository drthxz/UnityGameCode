using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrettargettingsystem : MonoBehaviour {

    
    protected GameObject currentTarget;
    protected Creature hp;
    public ArrayList playerGameObjects;
    public int turretSpeed = 4;
    public float damagePerSecond = 40f;

    public enum TurretState { Disabled, Idle, LockingOn, Engaged }
    //enum=例 Disabled=沒有機能 Idle=閒置 
    protected TurretState currentTurretState;
    // Use this for initialization
    public virtual void Start()
    {
        playerGameObjects = new ArrayList();
        currentTurretState = TurretState.Idle;

    }

    // Update is called once per frame
    void Update()
    {
        if (currentTurretState == TurretState.Disabled)
        {

        }
        else if (currentTurretState == TurretState.Idle)
        {
            CheckForPlayerInRange();
        }
        else if (currentTurretState == TurretState.LockingOn)
        {
            LockOn();
        }
        else if (currentTurretState == TurretState.Engaged)
        {
            LookAtTarget();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerGameObjects.Add(collider.gameObject);

            Creature phm = collider.gameObject.GetComponent<Creature>();

        }

    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerGameObjects.Remove(collider.gameObject);

            if (currentTarget == collider.gameObject)
            {
                currentTarget = null;
                currentTurretState = TurretState.Idle;
            }
        }
    }


    void CheckForPlayerInRange()
    {
        if (playerGameObjects.Count > 0)
        {
            int nearestPlayerIndex = GetNearestPlayerIndex();
            currentTarget = (GameObject)playerGameObjects[nearestPlayerIndex];
            currentTurretState = TurretState.LockingOn;
        }
    }

    int GetNearestPlayerIndex()
    {
        float nearestDistance = 9999f;
        int nearestPlayerIndex = 0;
        for (int i = 0; i < playerGameObjects.Count; i++)
        {
            float distanceToObject = Vector3.Distance(transform.position, ((GameObject)playerGameObjects[i]).transform.position);
            if (distanceToObject < nearestDistance)
            {
                nearestPlayerIndex = i;
                nearestDistance = distanceToObject;
            }
        }
        return nearestPlayerIndex;
    }
    void LockOn()
    {
        Vector3 currentTargetPosition = currentTarget.transform.position;
        currentTargetPosition.y = transform.position.y;
        Vector3 targetDirection = currentTargetPosition - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDirection, Time.deltaTime * turretSpeed, 0f);
        transform.rotation = Quaternion.LookRotation(newDir);

        float remainingRotation = Mathf.Abs(Quaternion.LookRotation(transform.forward).eulerAngles.y - Quaternion.LookRotation(targetDirection).eulerAngles.y);
        if (remainingRotation < 2.5)
        {
            currentTurretState = TurretState.Engaged;
        }
    }
    void LookAtTarget()
    {

        Vector3 targetPosition = currentTarget.transform.position;
        targetPosition.y = transform.position.y;
        transform.LookAt(targetPosition);

    }

}
