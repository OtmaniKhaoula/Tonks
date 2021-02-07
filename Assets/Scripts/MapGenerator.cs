using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    int[,] map;

    public int width, height;

    public string seed;
    public bool useRandomSeed;

    [Range(0, 100)]
    public int randomFillPercent;

    public Tilemap waterTilemap, grassTilemap, Props;
    public Tile water, grass;

    int[,] Props_coord;
    public GameObject[] Tiles;

    [Range(0, 100)]
    public int propsRandomFillPercent;


    // Start is called before the first frame update
    void Start()
    {
        Generation();
    }

    void Generation()
    {
        map = new int[width, height];
        RandomFillMap();

        for (int i = 0; i < 5; i++)
        {
            Smoothing();
            placeTiles();

        }
    }

    void RandomFillMap()
    {
        if (useRandomSeed)
        {
            seed = Time.time.ToString();
        }

        System.Random prng = new System.Random(seed.GetHashCode());
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    map[x, y] = 0;
                }
                else
                {
                    map[x, y] = (prng.Next(0, 100) < randomFillPercent) ? 1 : 0;
                }

            }
        }
    }

    void placeTiles()
    {
        if (map != null)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (map[x, y] == 1)
                    {
                        int spawn = (UnityEngine.Random.Range(0, 100) < propsRandomFillPercent) ? 1 : 0;

                        if (spawn == 1 && (x != 50 || y != 50))
                        {
                            int tile_index = UnityEngine.Random.Range(0, Tiles.Length - 1);
                            //Props.SetTile(new Vector3Int(x, y, 0), Tiles[tile_index]);
                            Instantiate(Tiles[tile_index], new Vector2(x, y), Quaternion.identity);
                        }

                        grassTilemap.SetTile(new Vector3Int(x, y, 0), grass);
                    }
                    else if (map[x, y] == 0)
                    {
                        waterTilemap.SetTile(new Vector3Int(x, y, 0), water);

                    }

                }
            }
        }
    }

    void Smoothing()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int sameNeighbour = neighbourCount(x, y);
                if (sameNeighbour > 4)
                {
                    map[x, y] = 1;
                }
                else if (sameNeighbour < 4)
                {
                    map[x, y] = 0;
                }
            }
        }
    }

    int neighbourCount(int x, int y)
    {
        int sameNeighbour = 0;
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                if (i >= 0 && i < width && j >= 0 && j < height)
                {
                    if ((i != x || j != y))
                    {
                        sameNeighbour += map[i, j];
                    }
                }

            }
        }
        return sameNeighbour;
    }
}





