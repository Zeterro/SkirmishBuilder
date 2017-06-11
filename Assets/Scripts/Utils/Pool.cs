using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    private Stack<GameObject> _objects = new Stack<GameObject>();
    private GameObject _prefab;
    private GameObject _container;

    public Pool(GameObject prefab, GameObject parent, int size)
    {
        _container = new GameObject("Pool " + prefab.ToString());
        _container.transform.SetParent(parent.transform);
        _prefab = prefab;

        for (int i = 0; i < size; i++)
        {
            _objects.Push(InstantiatePrefab());
        }
    }

    public GameObject GetGameObject()
    {
        if (_objects.Count > 0)
        {
            GameObject gameObject = _objects.Pop();
            gameObject.SetActive(true);
            return gameObject;
        }

        GameObject go = InstantiatePrefab();
        go.SetActive(true);
        return go;
    }

    public void ReturnGameObject(GameObject gameObject)
    {
        gameObject.transform.SetParent(_container.transform);
        gameObject.SetActive(false);
        _objects.Push(gameObject);
    }

    private GameObject InstantiatePrefab()
    {
        GameObject instance = GameObject.Instantiate(_prefab);
        instance.transform.SetParent(_container.transform);
        instance.SetActive(false);
        return instance;
    }
}