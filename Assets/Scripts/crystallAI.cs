using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crystallAI : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    AudioClip fall, get;

    [SerializeField]
    float speed, moveDelay;

    [SerializeField]
    string crystallCollectorName, crystallCollectorTag;

    private ObjMoveToTargetController mob;
    private Rigidbody rb;
    private AudioSource soundSource;

    void Start()
    {
        mob = gameObject.GetComponent<ObjMoveToTargetController>();
        rb = gameObject.GetComponent<Rigidbody>();
        soundSource = gameObject.GetComponent<AudioSource>();

        soundSource.clip = fall;

        if (mob != null)
        {
            StartCoroutine(StartMoveCrystall(moveDelay));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(crystallCollectorTag))
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        soundSource.Play();
    }

    IEnumerator StartMoveCrystall(float time)
    {
        yield return new WaitForSeconds(time);

        GameObject collectorObj = GameObject.Find(crystallCollectorName);

        while (true)
        {
            rb.useGravity = false;
            mob.MoveObjToTarget(collectorObj, speed);
            yield return null;
        }
    }
}
