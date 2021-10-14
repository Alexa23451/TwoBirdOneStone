using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    private static Pooling _instance;
    private Dictionary<string, Queue> queues = new Dictionary<string, Queue>();

    private int max_pooling_cache = 20;

    private static GameObject pooling;

    private static void Init()
    {
        if (_instance == null)
        {
            pooling = new GameObject("PoolingSystem");
            _instance = pooling.AddComponent<Pooling>();
        }
    }

    public static GameObject InstantiateObject(GameObject _object, Vector3 _position = new Vector3(), Quaternion _rotation = new Quaternion(), Transform _parent = null)
    {
        Init();

        GameObject res;
        if (!_instance.queues.ContainsKey(_object.name))
        {
            _instance.queues.Add(_object.name, new Queue());
            res = Instantiate(_object, _position, _rotation, _parent ? _parent : pooling.transform);
            res.name = res.name.Replace("(Clone)", "");
        }
        else
        {
            if (_instance.queues[_object.name].Count > 0)
            {
                res = _instance.queues[_object.name].Dequeue() as GameObject;
                res.transform.localPosition = _position;
                res.transform.localRotation = _rotation;
                res.transform.SetParent(_parent ? _parent : pooling.transform);
                res.SetActive(true);
            }
            else
            {
                res = Instantiate(_object, _position, _rotation, _parent ? _parent : pooling.transform);
                res.name = res.name.Replace("(Clone)", "");
            }
        }
        return res;
    }

    public static T InstantiateObject<T>(GameObject _object, Vector3 _position = new Vector3(), Quaternion _rotation = new Quaternion(), Transform _parent = null) where T : class
    {
        Init();

        GameObject res;
        if (!_instance.queues.ContainsKey(_object.name))
        {
            _instance.queues.Add(_object.name, new Queue());
            res = Instantiate(_object, _position, _rotation, _parent ? _parent : pooling.transform);
            res.name = res.name.Replace("(Clone)", "");
        }
        else
        {
            if (_instance.queues[_object.name].Count > 0)
            {
                res = _instance.queues[_object.name].Dequeue() as GameObject;
                res.transform.localPosition = _position;
                res.transform.localRotation = _rotation;
                res.transform.SetParent(_parent ? _parent : pooling.transform);
                res.SetActive(true);
            }
            else
            {
                res = Instantiate(_object, _position, _rotation, _parent ? _parent : pooling.transform);
                res.name = res.name.Replace("(Clone)", "");
            }
        }
        return res.GetComponent<T>();
    }

    public static void DestroyObject(GameObject _object, float timer = 0f)
    {
        Init();

        _instance.DestroyObjectWithTimer(_object, timer);
    }

    private void DestroyObjectWithTimer(GameObject _object, float timer)
    {
        if (_instance != null)
        {
            CoroutineUtils.PlayCoroutine(() =>
            {
                if (_instance == null)
                    return;

                if (!_instance.queues.ContainsKey(_object.name))
                {
                    _instance.queues.Add(_object.name, new Queue());
                    _instance.queues[_object.name].Enqueue(_object);
                    _object.SetActive(false);
                }
                else
                {
                    if (_instance.queues[_object.name].Count > _instance.max_pooling_cache)
                    {
                        Destroy(_object);
                    }
                    else
                    {
                        _instance.queues[_object.name].Enqueue(_object);
                        _object.SetActive(false);
                    }
                }
            }, timer);
        }
    }
}
