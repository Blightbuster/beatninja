using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] FruitPrefabs;

    public float Force;
    public float Lifetime = 5f;

    public void Spawn(SpawnEvent e)
    {
        GameObject prefab = FruitPrefabs[Random.Range(0, FruitPrefabs.Length)];
        GameObject fruit = Instantiate(prefab, this.transform.position, Random.rotation);
        fruit.GetComponent<Rigidbody>().AddForce(this.transform.up * Force, ForceMode.Impulse);

        Destroy(fruit, Lifetime);   // Delete instance after lifetime has expired
    }

}
