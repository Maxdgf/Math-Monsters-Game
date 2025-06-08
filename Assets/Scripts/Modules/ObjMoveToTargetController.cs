using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMoveToTargetController : MonoBehaviour
{
    public void MoveObjToTarget(GameObject target, float speed)
    {
        Transform tr = target.transform;
        Rigidbody rb = GetComponent<Rigidbody>();

        Vector3 direction = (tr.position - transform.position).normalized;
        rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
    }
}
