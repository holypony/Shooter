using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{

    [SerializeField] private Material defaultMat;
    [SerializeField] private Material deadMat;
    private MeshRenderer mr;
    void Awake()
    {
        mr = GetComponent<MeshRenderer>();
    }

    void OnEnable()
    {
        mr.material = defaultMat;
    }

    public void TurnOff()
    {
        StartCoroutine(Falling());
        IEnumerator Falling()
        {
            mr.material = deadMat;
            yield return new WaitForSeconds(.1f);
            int i = 0;
            while (i < 50)
            {
                transform.position -= Vector3.up * 0.1f;
                i++;
                yield return null;
            }
            gameObject.SetActive(false);
        }
    }
}
