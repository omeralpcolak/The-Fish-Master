using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hook : MonoBehaviour
{
    public Transform hookedTransform;
    private Camera mainCamera;
    private Collider2D coll;

    private int length;
    private int strength;
    private int fishCount;

    private bool camMove;

    private Tweener cameraTween;
    
    void Awake()
    {
        mainCamera = Camera.main;
        coll = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (camMove && Input.GetMouseButton(0))
        {
            Vector3 vector = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 position = transform.position;
            position.x = vector.x-5;
            transform.position = position;  
        }
    }

    public void StartFishing ()
    {
        length = -50; //IdleManager
        strength = 3; //IdleManager
        fishCount = 0;
        float time = (-length) * 0.1f;

        cameraTween = mainCamera.transform.DOMoveY(length, 1f + time * 0.25f, false).OnUpdate(delegate
        {
            if (mainCamera.transform.position.y <= -11)
                transform.SetParent(mainCamera.transform);
        }).OnComplete(delegate
        {
            coll.enabled = true;
            cameraTween = mainCamera.transform.DOMoveY(0, time * 5f, false).OnUpdate(delegate
            {
                if (mainCamera.transform.position.y >= -25f)
                {
                    StopFishing();
                }
            });
        });
        //Screen (GAME)
        coll.enabled = false;
        camMove = true;
        //Clear hook

    }

    void StopFishing()
    {
        camMove = false;
        cameraTween.Kill(false);
        cameraTween = mainCamera.transform.DOMoveY(0, 2, false).OnUpdate(delegate
        {
            if (mainCamera.transform.position.y >= -11)
            {
                transform.SetParent(null);
                transform.position = new Vector2(transform.position.x, -6);
            }
        }).OnComplete(delegate
        {
            transform.position = Vector2.down * 6;
            coll.enabled = true;
            int num = 0;
            //Clearing out the hook from the fishes
            //IdleManager Totalgain = num
            //ScreenManager End Screen
        });
    }

}
