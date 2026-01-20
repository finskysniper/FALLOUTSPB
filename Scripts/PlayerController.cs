using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public HexGrid grid;
    public float moveSpeed = 3f;

    Vector2Int currentHex;
    Coroutine moveRoutine;

    void Start()
    {
        currentHex = grid.WorldToHex(transform.position);
    }

    public void MoveAlong(List<Vector2Int> path)
    {
        if (moveRoutine != null)
            StopCoroutine(moveRoutine);

        moveRoutine = StartCoroutine(Move(path));
    }

    IEnumerator Move(List<Vector2Int> path)
    {
        foreach (var hex in path)
        {
            Vector3 target = grid.HexToWorld(hex.x, hex.y);
            while (Vector3.Distance(transform.position, target) > 0.01f)
            {
                transform.position =
                    Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
                yield return null;
            }

            currentHex = hex;
        }
    }

    public Vector2Int CurrentHex => currentHex;
}
