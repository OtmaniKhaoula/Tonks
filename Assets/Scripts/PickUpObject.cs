using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public int Obj_index;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Inventory.instance.AddObject(Obj_index);
            Destroy(gameObject);
        }

    }
}
