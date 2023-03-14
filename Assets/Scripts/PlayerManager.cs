using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Transform player;
    public bool MoveByTouch, gameState;
    private int numberOfStickmans = 1;
    private Vector3 Direction;
    public List<Rigidbody> Rblst = new List<Rigidbody>();
    public Transform road;
    public static PlayerManager PlayerManagerCls;
    public GamesManager gamesManager;

    [SerializeField] private float runSpeed, swipeSpeed, roadSpeed;
    [SerializeField] private GameObject stickMan;
    [SerializeField] private TextMeshPro CounterTxt;

    [Range(0f, 1f)][SerializeField] private float DistanceFactor, Radius;
    void Start()
    {
        PlayerManagerCls = this;
        Rblst.Add(transform.GetChild(0).GetComponent<Rigidbody>());

    }
    public void onGameState()
    {
        gameState = true;
    }

    void Update()
    {
        if (gameState)
        {
            if (Input.GetMouseButtonDown(0))
            {
                MoveByTouch = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                MoveByTouch = true;
            }

            if (MoveByTouch)
            {

                Direction.x = Mathf.Lerp(Direction.x, Input.GetAxis("Mouse X"), Time.deltaTime * runSpeed);

                Direction = Vector3.ClampMagnitude(Direction, 1f);

                road.position = new Vector3(0f, 0f, Mathf.SmoothStep(road.position.z, -190f, Time.deltaTime * roadSpeed));

                foreach (var stickman_Anim in Rblst)
                    stickman_Anim.GetComponent<Animator>().SetFloat("run", 1f);
            }

            else
            {
                foreach (var stickman_Anim in Rblst)
                    stickman_Anim.GetComponent<Animator>().SetFloat("run", 0f);
            }
            if(numberOfStickmans <= 0) {
                gameState = false;
                gamesManager.LoseGame();
            }
        }
    }

    private void FixedUpdate()
    {
        if (gameState)
        {
            if (MoveByTouch)
            {
                Vector3 displacement = new Vector3(Direction.x, 0f, 0f) * Time.fixedDeltaTime;

                foreach (var stickman_rb in Rblst)
                    stickman_rb.velocity = new Vector3(Direction.x * Time.fixedDeltaTime * swipeSpeed, 0f, 0f) + displacement;
            }
            else
            {
                foreach (var stickman_rb in Rblst)
                    stickman_rb.velocity = Vector3.zero;
            }
        }

    }
    public void ChangeMembers()
    {
        numberOfStickmans -= 1;
    }
    public void FormatStickMan()
    {
        for (int i = 1; i < player.childCount; i++)
        {
            var x = (DistanceFactor + 0.4f) * Mathf.Sqrt(i) * Mathf.Cos(i * Radius) + Random.Range(-1f, 1f);
            var z = (DistanceFactor + 0.4f) * Mathf.Sqrt(i) * Mathf.Sin(i * Radius) + Random.Range(-1f, 1f);
            var NewPos = new Vector3(x, -0.09f, z);

            player.transform.GetChild(i).DOLocalMove(NewPos, 0.25f).SetEase(Ease.OutBack);
            
        }
        for (int i = 0; i < player.childCount; i++)
        {
            Rblst.Add(player.transform.GetChild(i).GetComponent<Rigidbody>());
        }
    }
    public void MakeStickMan(int number)
    {
        Debug.Log(number);
        for (int i = numberOfStickmans; i < number; i++)
        {
            Instantiate(stickMan, transform.position, Quaternion.identity, transform);
            Debug.Log("Create");
        }

        numberOfStickmans = transform.childCount;
        CounterTxt.text = numberOfStickmans.ToString();
        FormatStickMan();
    }

}
