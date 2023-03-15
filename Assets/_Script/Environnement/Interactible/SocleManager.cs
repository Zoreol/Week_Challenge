using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SocleManager : MonoBehaviour
{
    [SerializeField] GameObject bouclier;
    [SerializeField] GameObject target;
    [SerializeField] GameObject Cube;
    bool isCube;
    private void Start()
    {
        isCube = false;
    }
    private void Update()
    {
        if (isCube)
        {
            Cube.transform.position = target.transform.position;    
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cube"))
        {
            bouclier.SetActive(false);
            FindObjectOfType<Hitting>().hitObject.SetActive(false);
            collision.transform.SetParent(target.transform, false);
            //collision.transform.parent.GetComponent<Hitting>().hitObject.SetActive(false);
            
            collision.transform.position = target.transform.position;
            collision.transform.parent = target.transform;
            collision.GetComponent<BoxCollider2D>().enabled = false;
            Cube = collision.gameObject;
            isCube = true;
        }
    }
}
