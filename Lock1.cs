using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock1 : MonoBehaviour
{
    public GameObject book; // 대화창 패널


    private void Start()
    {
        book.SetActive(false); // 게임 시작 시 대화창 비활성화
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MainCamera") // 플레이어가 들어왔다면
        {
            book.SetActive(true); // 대화창 활성화
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MainCamera") // 플레이어가 나갔다면
        {
            book.SetActive(false); // 대화창 비활성화
        }
    }
}
