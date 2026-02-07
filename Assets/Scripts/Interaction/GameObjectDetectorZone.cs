using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(Collider))]
public class GameObjectDetectorZone : MonoBehaviour
{
    [SerializeField] private float validationInterval = 0.25f;
    [ShowInInspector, DisplayAsString] public int ObjectCount
    {
        get
        {
            if (_objects == null) return 0;
            return _objects.Count;
        }
    }
    private HashSet<GameObject> _objects;
    private float timer;
    private Collider col;
    public IReadOnlyCollection<GameObject> Objects => _objects;
    public event System.Action<GameObject> OnObjectEntered;
    public event System.Action<GameObject> OnObjectExited;

    private void Awake()
    {
        col = GetComponent<Collider>();
        col.isTrigger = true;
        _objects = new HashSet<GameObject>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > validationInterval)
        {
            timer = 0;
            ValidateObjects();
        }
    }

    private void ValidateObjects()
    {
        List<GameObject> invalid = _objects.Where(go => !IsObjectValid(go)).ToList();
        foreach (var item in invalid)
        {
            ExitObject(item);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        TryAdd(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        ExitObject(other.gameObject);
    }

    private bool IsObjectValid(GameObject gameObject)
    {
        return gameObject != null && gameObject.activeInHierarchy && IsApproximatelyInside(gameObject);
    }

    private bool IsApproximatelyInside(GameObject gameObject)
    {
        //not shape accurate, but in most cases, the shape of the trigger itself will handle precision
        //this is mainly a cheap sanity check for edge cases
        return col.bounds.Intersects(gameObject.GetComponent<Collider>().bounds);
    }

    private void TryAdd(GameObject gameObject)
    {
        if (!gameObject.activeInHierarchy || _objects.Contains(gameObject)) return;

        _objects.Add(gameObject);
        OnObjectEntered?.Invoke(gameObject);
    }

    private void ExitObject(GameObject gameObject)
    {
        if (!_objects.Contains(gameObject)) return;

        _objects.Remove(gameObject);
        OnObjectEntered?.Invoke(gameObject);
    }
}
