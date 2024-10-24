using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject tilePrefab; // Reference to your tile prefab
    public float yPosition = 2.305f; // Y position for the tiles
    public float zPosition = 0f; // Z position for the tiles, can be adjusted

    void Start()
    {
        SpawnTiles(zPosition);
    }

    void SpawnTiles(float zPos)
    {
        // Set positions for the two tiles
        Vector3 position1 = new Vector3(-2f, yPosition, zPos); // Tile 1
        Vector3 position2 = new Vector3(2f, yPosition, zPos);  // Tile 2

        // Instantiate the tiles
        GameObject tile1 = Instantiate(tilePrefab, position1, Quaternion.identity);
        GameObject tile2 = Instantiate(tilePrefab, position2, Quaternion.identity);

        // Randomly decide which tile will fall through
        if (Random.value > 0.5f) // 50% chance
        {
            tile1.GetComponent<TileController>().SetFallThrough(); // Set tile 1 to fall through
        }
        else
        {
            tile2.GetComponent<TileController>().SetFallThrough(); // Set tile 2 to fall through
        }
    }
}