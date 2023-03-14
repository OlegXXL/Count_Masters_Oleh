using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyControll : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            gameObject.SetActive(false);
            transform.parent = null;
        }
    }
}
