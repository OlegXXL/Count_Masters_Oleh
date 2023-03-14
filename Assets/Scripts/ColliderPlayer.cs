using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColliderPlayer : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    private int numberOfStickmans = 1;
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Gate"))
        {
            Debug.Log("Gate Trigger");
            other.transform.parent.gameObject.SetActive(false);
            string countNewMembers = other.transform.GetChild(0).GetComponent<TextMeshPro>().text.ToString();
            numberOfStickmans = transform.parent.transform.childCount;
            Debug.Log(countNewMembers);
            if (countNewMembers[0] == '+')
            {
                Debug.Log("+");
                countNewMembers = countNewMembers.Substring(1);
                playerManager.MakeStickMan(numberOfStickmans + int.Parse(countNewMembers) - 1);
            }
            else if (countNewMembers[0] == 'X')
            {
                Debug.Log("*");
                countNewMembers = countNewMembers.Substring(1);
                Debug.Log(int.Parse(countNewMembers));
                playerManager.MakeStickMan(numberOfStickmans * int.Parse(countNewMembers) - 1);
            }
        }
        if (other.CompareTag("Destroy"))
        {
            Debug.Log("Destroy");
            transform.gameObject.SetActive(false) ;
            playerManager.ChangeMembers();
        }

    }

}
