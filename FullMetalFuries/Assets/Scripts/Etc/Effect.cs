using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField] private GameObject _effectPrefab;
    [SerializeField] private int _effectInitCount = 5;

    Queue<GameObject> _effectQueue = new Queue<GameObject>();

    private void Awake()
    {
        Initialize();

        //GameManager.Instance.playerHit.AddListener(EnemyAttackEffect);
        //GameManager.Instance.playerAttack.AddListener(PlayerAttackEffect);
    }

    private void Initialize()
    {
        for (int i = 0; i < _effectInitCount; i++)
        {
            GameObject temp = Instantiate(_effectPrefab);
            temp.transform.SetParent(this.transform);
            temp.SetActive(false);
            _effectQueue.Enqueue(temp);
        }
    }

    private void OnDisable()
    {
        foreach(GameObject effect in _effectQueue)
        {
            Destroy(effect);
        }
    }

    private void EnemyAttackEffect()
    {
        //GameObject effect = _effectQueue.Dequeue();
        //effect.transform.position = pos;
        //effect.SetActive(true);
        //effect.SetActive(false);
        //_effectQueue.Enqueue(effect);
        ////애니메이션 재생
        ////애니메이션 끝나면 큐에 넣어놓

    }

    private void PlayerAttackEffect()
    {
        //GameObject effect = _effectQueue.Dequeue();
        //effect.transform.position = pos;
        //effect.SetActive(true);
        //effect.SetActive(false);
        //_effectQueue.Enqueue(effect);
    }
}

    //private Bullet CreateNewObject()
    //{
    //    var newObj = Instantiate(_effectQueue).GetComponent<Bullet>();
    //    newObj.gameObject.SetActive(false);
    //    newObj.transform.SetParent(transform);
    //    return newObj;
    //}

    //public static Bullet GetObject()
    //{
    //    if (Instance._effectQueue.Count > 0)
    //    {
    //        var obj = Instance._effectQueue.Dequeue();
    //        obj.transform.SetParent(null);
    //        obj.gameObject.SetActive(true);
    //        return obj;
    //    }
    //    else
    //    {
    //        var newObj = Instance.CreateNewObject();
    //        newObj.gameObject.SetActive(true);
    //        newObj.transform.SetParent(null);
    //        return newObj;
    //    }
    //}

    //public static void ReturnObject(Bullet obj)
    //{
    //    obj.gameObject.SetActive(false);
    //    obj.transform.SetParent(Instance.transform);
    //    Instance._effectQueue.Enqueue(obj);
    //}
