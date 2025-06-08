using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class monsterAI : MonoBehaviour
{
    [SerializeField]
    GameObject player, crystall, monsterCamera;

    [SerializeField]
    GameObject[] otherMonsterParts, ui;

    [SerializeField]
    string playerTag, shootAreaTag, safeShieldTag, monsterBodyName, headName, playerCatchingAnimation, monstersCloneName, monsterWalkAnimation;

    [SerializeField]
    int crystallsCount, gameOverSceneIndex;

    [SerializeField]
    float rotationSpeed, walkSpeed, crystallSpawnDelay, cameraShakeDuration, playerDeathDelay;

    [SerializeField]
    NavMeshAgent monsterAgent;

    [SerializeField]
    AudioClip[] explosionSounds;

    private Transform playerTransform;
    private VisibilityController vc;
    private ObjSpawnerController osc;
    private Animator animator;
    private NavMeshAgent agent;
    private bool monsterDestroyed = false;

    void Start()
    {
        osc = gameObject.GetComponent<ObjSpawnerController>();
        vc = gameObject.GetComponent<VisibilityController>();
        animator = gameObject.GetComponent<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();

        monsterAgent.speed = walkSpeed;
        monsterAgent.updateRotation = true;

        playerTransform = player.transform;

        if (player != null)
        {
            if (monsterAgent.destination != player.transform.position)
            {
                monsterAgent.destination = playerTransform.position;
            }
            else
            {
                Debug.Log("Monster AI: monster reached the player.");
            }
        }
        else
        {
            Debug.LogError("Monster AI: player is null!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (!monsterDestroyed)
            {
                animator.SetBool(monsterWalkAnimation, false);
                DisableAgentsOnMonsters(monstersCloneName);

                GameObject.Find("monsterSpawner").SetActive(false);
                GameObject.Find("playerBody").SetActive(false);
                monsterCamera.SetActive(true);

                animator.Play(playerCatchingAnimation);

                Debug.Log("Monster AI: player catched!");

                StartCoroutine(changeToGameOverSceneAfterDelay(playerDeathDelay, gameOverSceneIndex));
            } 
            else
            {
                Destroy(gameObject);
            }
        }
        else if (other.CompareTag(shootAreaTag))
        {
            monsterDestroyed = true;

            int rndIndex = Random.Range(0, explosionSounds.Length - 1);

            GameObject explosionSource = SetupExplosionAudioSource(rndIndex);
            AudioSource audioSource = explosionSource.GetComponent<AudioSource>();

            GameObject monsterBody = gameObject.transform.Find(monsterBodyName).gameObject;

            float soundDuration = audioSource.clip.length;

            audioSource.Play();

            if (osc != null)
            {
                osc.StartSpawnObj(gameObject, crystall, crystallsCount, crystallSpawnDelay);
            } 
            else
            {
                Debug.LogError("Error to spawn crystall");
            }

            monsterBody.SetActive(false);

            vc.changeVisibilityStateOfObjects(otherMonsterParts, false);

            agent.isStopped = true;
            agent.enabled = false;

            StartCoroutine(DestroyExplosionSoundSourceAndMonster(soundDuration, explosionSource));
        }
        else if (other.CompareTag(safeShieldTag))
        {
            Destroy(gameObject);
        }
    }

    private GameObject SetupExplosionAudioSource(int rndIndexSound)
    {
        GameObject explosionSource = new GameObject("monsterExplosionSource");
        AudioSource audioSource = explosionSource.AddComponent<AudioSource>();

        audioSource.transform.position = gameObject.transform.position;
        audioSource.loop = false;
        audioSource.playOnAwake = false;
        audioSource.pitch = 1.6f;
        audioSource.clip = explosionSounds[rndIndexSound];

        return explosionSource;
    }

    private void DisableAgentsOnMonsters(string name)
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Enemy");

        if (monsters.Length == 0)
        {
            Debug.Log("All monsters deleted with name: " + name);
            return; 
        }

        foreach (GameObject monster in monsters)
        {
            NavMeshAgent agent = monster.GetComponent<NavMeshAgent>();
            agent.isStopped = true;
        }
    }

    private IEnumerator DestroyExplosionSoundSourceAndMonster(float time, GameObject soundSource)
    {
        yield return new WaitForSeconds(time);

        Destroy(soundSource);
        Destroy(gameObject);

        monsterDestroyed = false;
    }

    private IEnumerator changeToGameOverSceneAfterDelay(float time, int index)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(index);
        Cursor.lockState = CursorLockMode.None;
    }
}
