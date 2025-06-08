using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject monster;

    [SerializeField]
    Material[] rareMaterials;

    [SerializeField]
    Material defaultParticle;

    [SerializeField]
    Transform spawner;

    [SerializeField]
    allSpawnedMonsters monstersStorage;

    [SerializeField]
    string monsterBodyName;

    [SerializeField]
    float delay;

    private System.Random random;

    private const double GOLD_MATERIAL_CHANCE = 0.03;
    private const double SILVER_MATERIAL_CHANCE = 0.05;
    private const double BRONZE_MATERIAL_CHANCE = 0.07;
    private const double RUBY_MATERIAL_CHANCE = 0.01;

    void Start()
    {
        random = new System.Random();

        StartCoroutine(MonsterSpawn(delay));
    }

    private IEnumerator MonsterSpawn(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);

            GameObject monsterObj = Instantiate(monster, spawner.position, spawner.rotation);

            monstersStorage.addMonsterToStorage(monsterObj);

            GameObject monsterBody = monsterObj.transform.Find(monsterBodyName).gameObject;
            Renderer bodyRenderer = monsterBody.GetComponent<Renderer>();

            if (bodyRenderer != null)
            {
                double roll = random.NextDouble();

                if (roll < GOLD_MATERIAL_CHANCE)
                {
                    bodyRenderer.material = rareMaterials[0];
                    setupRareEffect(monsterObj, Color.yellow, defaultParticle);
                } 
                else if (roll < SILVER_MATERIAL_CHANCE)
                {
                    bodyRenderer.material = rareMaterials[1];
                    setupRareEffect(monster, Color.gray, defaultParticle);
                } 
                else if (roll < BRONZE_MATERIAL_CHANCE)
                {
                    bodyRenderer.material = rareMaterials[2];
                    Color32 bronzeColor = new Color32(205, 127, 50, 255);
                    setupRareEffect(monster, bronzeColor, defaultParticle);
                } 
                else if (roll < RUBY_MATERIAL_CHANCE)
                {
                    bodyRenderer.material = rareMaterials[3];
                    setupRareEffect(monster, Color.red, defaultParticle);
                } 
                else 
                {
                    Color monsterColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                    bodyRenderer.material.color = monsterColor;

                    checkComponentts(monsterObj);
                }

                Debug.Log($"Monster spawner: monster color changed!");
            }
            else
            {
                Debug.LogError("Monster spawner: monster body renderer is null! check -monsterBodyName- parameter.");
            }
        }
    }

    private void setupRareEffect(GameObject monster, Color color, Material defaultParticle)
    {
        Light light = monster.AddComponent<Light>();
        light.type = LightType.Point;
        light.color = color;

        ParticleSystem particles = monster.AddComponent<ParticleSystem>();
        ParticleSystemRenderer renderer = particles.GetComponent<ParticleSystemRenderer>();
        var main = particles.main;
        var shape = particles.shape;
        main.startColor = color;
        shape.shapeType = ParticleSystemShapeType.Sphere;
        renderer.material = defaultParticle;
    }

    //check components, because they saving to prefab always.
    private void checkComponentts(GameObject monster)
    {
        ParticleSystem particleSystem = monster.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            Destroy(particleSystem);
        }

        Light light = monster.GetComponent<Light>();
        if (light != null)
        {
            Destroy(light);
        }
    }
}
