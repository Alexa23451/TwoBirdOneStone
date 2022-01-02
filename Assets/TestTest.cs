using System.Collections.Generic;
using UnityEngine;

public class TestTest : MonoBehaviour
{
    public List<GameObject> linkObj;
    public float timeEachChar = 0.5f;
    public float distanceEachChar = 0.5f;
    public GameObject followObj;
    private Rigidbody2D rig;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        TimerManager.Instance.Init();
    }

    private void Update()
    {
        float horizon = Input.GetAxis("Horizontal");
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rig.AddForce(Vector2.up * 6, ForceMode2D.Impulse);
            float counting = timeEachChar;
            foreach (var obj in linkObj)
            {
                TimerManager.Instance.AddTimer(counting, () => obj.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 6, ForceMode2D.Impulse));
                counting += timeEachChar;
            }
        }

        transform.position += Vector3.right * Time.deltaTime * 3f * horizon;
        foreach(var obj in linkObj)
        {
            obj.transform.position += Vector3.right * Time.deltaTime * 3f * horizon;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PICKUP")
        {
            collision.gameObject.SetActive(false);

            var newObj = Instantiate(followObj);
            newObj.gameObject.SetActive(true);
            newObj.transform.position = transform.position + (Vector3.left * distanceEachChar * (linkObj.Count + 1));

            linkObj.Add(newObj);
        }
    }
}


