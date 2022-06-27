using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public void Init()
    {

    }
    private void OnEnable()
    {
        //StartCoroutine(BhRoutine());
    }

    private IEnumerator BhRoutine()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("col handler here");
        if (other.TryGetComponent<CollisionHandler>(out CollisionHandler zombie))
        {
            Debug.Log("col handler here");
            Vector3 punch = new Vector3(zombie.transform.position.x, zombie.transform.position.y + 10f, zombie.transform.position.z);
            zombie.enemySoldier.Death(2f, punch, false);
        }
    }


}
