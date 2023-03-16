using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SocleManager : MonoBehaviour
{
    [SerializeField] GameObject[] bouclier;
    [SerializeField] GameObject message;
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
            message.SetActive(false);
            for (int i = 0; i < bouclier.Length; i++)
            {
                bouclier[i].SetActive(false);
            }
            
            FindObjectOfType<Hitting>().hitObject.SetActive(false);
            collision.transform.SetParent(target.transform, false);
            
            collision.transform.position = target.transform.position;
            collision.transform.parent = target.transform;
            collision.GetComponent<BoxCollider2D>().enabled = false;
            Cube = collision.gameObject;
            isCube = true;
        }
        if (collision.CompareTag("EnnemyCube"))
        {
            if (FindObjectOfType<PlayerManager>().usingObject)
            {
                Debug.Log("oui");
                message.SetActive(false);
                for (int i = 0; i < bouclier.Length; i++)
                {
                    bouclier[i].SetActive(false);
                }
                FindObjectOfType<Hitting>().hitObject.SetActive(false);
                collision.transform.GetComponent<EnnemyBlocking>().slimeObjectif.transform.SetParent(target.transform, false);

                collision.transform.position = target.transform.position;
                collision.transform.parent = target.transform;
                collision.GetComponent<BoxCollider2D>().enabled = false;
                Cube = collision.gameObject;
                isCube = true;
            }
            
        }
    }
}
