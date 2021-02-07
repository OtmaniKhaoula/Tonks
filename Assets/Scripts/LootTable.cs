using UnityEngine;

[System.Serializable]
public class Loot
{
    public GameObject thisLoot;
    public int lootChance;
}


[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] loots;

    public GameObject LootObject()
    {
        int cumProb = 0;
        int currentProb = Random.Range(0, 100);
        for(int i = 0; i < loots.Length; i++)
        {
            cumProb += loots[i].lootChance;
            if(currentProb <= cumProb)
            {
                return loots[i].thisLoot;
            }
        }
        return null;
    }

}
