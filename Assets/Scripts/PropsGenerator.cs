using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PropsGenerator : MonoBehaviour
{
    int[,] Props_coord;
    public Tile[] Tiles;
    public Tilemap Props;

    [Range(0, 100)]
    public int propsRandomFillPercent;

    public static PropsGenerator instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance PropsGenerator");
            return;
        }

        instance = this;
    }

    public void Generate_props(int x, int y)
    {
        int spawn = (Random.Range(0,100) < propsRandomFillPercent) ? 1 : 0;

        if(spawn == 1 && (x != 50 || y != 50))
        {
            int tile_index = Random.Range(0, Tiles.Length-1);
            Props.SetTile(new Vector3Int(x,y,0), Tiles[tile_index]);
        }
    }


}
