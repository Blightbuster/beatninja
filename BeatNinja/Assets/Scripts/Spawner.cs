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
        GameObject fruit = null;

        if (e is SpawnNoteEvent note)
        {
            // Choose big fruit if more than one hit is required
            var prefab = PickRandom(note.HitsNeeded == 1 ? FruitPrefabs : BigFruitPrefabs);
            fruit = Instantiate(prefab, this.transform.position, Random.rotation);
            fruit.GetComponent<Fruit>().HitsNeeded = note.HitsNeeded;
        }
        else if (e is SpawnSpamNoteEvent spamNote)
        {
            fruit = Instantiate(PickRandom(SpamFruitPrefabs), this.transform.position, Random.rotation);
            fruit.GetComponent<SpamFruit>().Duration = spamNote.Duration;
        }

        fruit.GetComponent<Rigidbody>().AddForce(this.transform.up * Force, ForceMode.Impulse);

        Destroy(fruit, Lifetime);   // Delete instance after lifetime has expired
    }

    private T PickRandom<T>(T[] array) => array[Random.Range(0, array.Length)];
}
