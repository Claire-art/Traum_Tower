using UnityEngine;
using System.Collections;

public class MoveObject : MonoBehaviour
{
    public Transform[] path; // 경로에 있는 위치들
    private int currentWaypointIndex = 0; // 현재 웨이포인트 인덱스

    void Start()
    {
        MoveToNextWaypoint();
    }

    void Update()
    {
        if (iTween.Count(gameObject) == 0)
        { // iTween 애니메이션이 끝나면 다음 웨이포인트로 이동
            currentWaypointIndex = (currentWaypointIndex + 1) % path.Length; // 다음 웨이포인트 인덱스 계산
            MoveToNextWaypoint();
        }
    }

    void MoveToNextWaypoint()
    {
        Hashtable ht = new Hashtable();
        ht.Add("position", path[currentWaypointIndex].position);
        ht.Add("time", 5); // 총 걸리는 시간
        ht.Add("easetype", iTween.EaseType.linear);

        iTween.MoveTo(gameObject, ht);
    }
}
