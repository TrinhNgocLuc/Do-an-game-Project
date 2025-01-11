using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected List<Transform> objPrefabs;
    [SerializeField] protected List<Transform> poolObjs;
    public Transform holder { get; protected set; }
    protected void Reset()
    {
        LoadPrefabsAndHolder();
    }
    protected virtual void Start()
    {
        LoadPrefabsAndHolder();
    }
    protected void LoadPrefabsAndHolder()
    {
        holder = transform.Find("Holder");
        objPrefabs.Clear();
        Transform prefab = transform.Find("Prefab");
        foreach (Transform tran in prefab)
        {
            objPrefabs.Add(tran);
        }
    }
    protected Transform GetObjByName(string name)
    {
        foreach(Transform tran in poolObjs)
        {
            if(tran.name == name+"(Clone)")
            {
                poolObjs.Remove(tran);
                return tran;
            }
        }
        return null;
    }
    public Transform Spawn(string objName, Vector3 pos, Quaternion rot)
    {
        Transform obj = GetObjByName(objName);
        if(obj == null)
        {
            foreach (Transform tran in objPrefabs)
            {
                if (tran.name == objName)
                {
                    obj = Instantiate(tran, pos, rot, holder);
                    Debug.Log("Spawned new racoon");
                }
            }
        } else
        {
            obj.SetPositionAndRotation(pos, rot);
            obj.parent = holder;
        }
        obj.gameObject.SetActive(true);
        return obj;
    }
    public void Despawn(Transform go)
    {
        go.gameObject.SetActive(false);
        go.parent = holder;
        //if(!poolObjs.Contains(go))
            poolObjs.Add(go);
    }
}
