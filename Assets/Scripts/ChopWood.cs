using UnityEngine;
using System.Collections;

public class ChopWood : MonoBehaviour
{
    public bool isInRange = false;
    public LootTable thisLoot;

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            PlayerMovement.instance.interactUI.enabled = false;
            StartCoroutine(ChoppingRoutine());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerMovement.instance.interactUI.enabled = true;
            isInRange = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            isInRange = false;
            PlayerMovement.instance.isChopping = false;
        }
    }

    private void MakeLoot()
    {
        if(thisLoot != null)
        {
            GameObject current = thisLoot.LootObject();
            if(current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }

    IEnumerator ChoppingRoutine()
    {
        PlayerMovement.instance.enabled = false;
        PlayerMovement.instance.animator.SetBool("isChopping", true);
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
        PlayerMovement.instance.enabled = true;
        MakeLoot();
    }
}