using UnityEngine;
using System.Collections;

public class TrackingSystem : MonoBehaviour
{
    public float speed = 3.0f;
    public GameObject m_target = null;
    Vector3 m_lastKnownPosition = Vector3.zero;
    Quaternion m_lookAtRotation;
    GameObject nearTarget = null;

    void Update()
    {
        nearTarget = FindClosestPlayer(m_target);

        float dis = Vector3.Distance(nearTarget.transform.position,this.transform.position);

        if (dis < 10)
        {
            if (m_lastKnownPosition != nearTarget.transform.position)
            {
                m_lastKnownPosition = nearTarget.transform.position;
                m_lookAtRotation = Quaternion.LookRotation(m_lastKnownPosition - transform.position);
            }

            if (transform.rotation != m_lookAtRotation)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, m_lookAtRotation, speed * Time.deltaTime);
            }
        }
    }

    GameObject FindClosestPlayer(GameObject closestPlayer)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
        float distance = Mathf.Infinity;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - transform.position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestPlayer = go;
                distance = curDistance;
            }
        }
        return closestPlayer;
    }
}
