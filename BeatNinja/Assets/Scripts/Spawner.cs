using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] FruitPrefabs;
    public GameObject[] BigFruitPrefabs;
    public GameObject[] SpamFruitPrefabs;

    public float Force;
    public float Lifetime = 5f;

    public void Spawn(SpawnEvent e)
    {
        GameObject[] prefabPool = null;

        if (e is SpawnNoteEvent note)
        {
            // Choose big fruit pool if more than one hit is required
            prefabPool = note.HitsNeeded == 1 ? FruitPrefabs : BigFruitPrefabs;
        }
        else if (e is SpawnSpamNoteEvent) prefabPool = SpamFruitPrefabs;

        var fruit = Instantiate(PickRandom(prefabPool), this.transform.position, Random.rotation);
        var sliceable = fruit.GetComponent<Sliceable>();
        sliceable.EventOrigin = e;
        var rb = sliceable.GetComponent<Rigidbody>();
        rb.AddForce(this.transform.up * Force, ForceMode.Impulse);
        rb.angularVelocity = Random.insideUnitSphere;

        Destroy(fruit, Lifetime);   // Delete instance after lifetime has expired
    }

    private T PickRandom<T>(T[] array) => array[Random.Range(0, array.Length)];
}
