using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSetting : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] Transform bulletFuturPosition;

    void Update()
    {
        transform.position = new Vector2(bulletFuturPosition.position.x , transform.position.y);
    }
}
