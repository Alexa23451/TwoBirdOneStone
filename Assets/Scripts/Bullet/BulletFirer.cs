using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletFirer : MonoBehaviour
{
    public event Action OnPlayerShot;

    private Rigidbody2D rig;
    private TrailRenderer trailRenderer;
    private LineRenderer lineRenderer;
    private BulletBehaviour bulletBehaviour;
    private Camera cam;

    [SerializeField] [Range(1, 14)] private float speedMax = 1;

    [SerializeField] private PlayerMechanic playerMechanic;
    [SerializeField] private Transform bulletStartPoint;
    [SerializeField] private Rigidbody2D slingShot;
    [SerializeField] bool isDrag = false;
    [SerializeField] bool isShot = false;
    [SerializeField] float clampRadius = 1.5f;
    [SerializeField] float timeRelease = 0.5f;

    public bool IsShot => isShot;

    private void Awake()
    {
        bulletBehaviour = GetComponent<BulletBehaviour>();

        playerMechanic.OnRecharge += OnRecharge;
        UIManager.Instance.GetPanel<PlayAgainPanel>().OnWatchAds += OnRecharge;
    }

    private void OnRecharge()
    {
        gameObject.SetActive(true);
        transform.position = bulletStartPoint.position;
        transform.rotation = bulletStartPoint.rotation;
        bulletBehaviour.SetDirection(Vector2.zero, 0);
        transform.SetParent(playerMechanic.gameObject.transform);

        isShot = false;
        transform.up = Vector3.zero;
    }



    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        lineRenderer = GetComponent<LineRenderer>();

        cam = Camera.main;

        trailRenderer.enabled = false;
        lineRenderer.enabled = true;
    }

    private void OnDestroy()
    {
        playerMechanic.OnRecharge -= OnRecharge;
        UIManager.Instance.GetPanel<PlayAgainPanel>().OnWatchAds -= OnRecharge;
    }

    private void OnMouseDown()
    {
        if (!isShot)
        {
            isDrag = true;
            lineRenderer.enabled = true;
            trailRenderer.enabled = false;
            rig.isKinematic = true;
            SoundManager.Instance.Play(Sounds.Bow);
        }
    }

    private void OnMouseDrag()
    {
        if (isDrag && !isShot)
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            float distance = Vector2.Distance(mousePos, slingShot.position);
            transform.up = (Vector2) bulletStartPoint.position - mousePos;

            SetLineRenderer();
            if (distance > clampRadius)
            {
                Vector2 dir = (mousePos - slingShot.position).normalized;
                rig.position = slingShot.position + dir * clampRadius;
            }
            else
            {
                rig.position = mousePos;
            }
        }
    }

    private void OnMouseUp()
    {
        if (isDrag && !isShot)
        {

            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = ((Vector2)bulletStartPoint.position - mousePos).normalized;

            float distance = Vector2.Distance(mousePos, bulletStartPoint.position);
            if(distance < 1f)
            {
                //check if OnMouseUp too close to current pos
                lineRenderer.enabled = false;
                transform.position = bulletStartPoint.position;
                transform.up = Vector3.zero;
                return;
            }

            float force = QuanMathf.ReMap(distance, 1, clampRadius, 1, speedMax);

            bulletBehaviour.SetDirection(dir, force);
            SoundManager.Instance.Play(Sounds.SHOT);

            transform.SetParent(null);

            rig.isKinematic = false;
            trailRenderer.enabled = true;
            isDrag = false;

            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, Vector3.zero);

            TimerManager.Instance.AddTimer(timeRelease, () =>
            {
                isShot = true;
            });

        }

    }

    private void SetLineRenderer()
    {
        lineRenderer.SetPosition(0, rig.position);
        lineRenderer.SetPosition(1, bulletStartPoint.position);
    }


}
