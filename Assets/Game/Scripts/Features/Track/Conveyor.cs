using System;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor<T> where T : MonoBehaviour, IBounds
{
    public float Speed { get; set; }
    public bool IsStoped { get; set; }
    public Vector3 Direction { get; set; }

    public int ObjectsCount => _conveyor.Count;

    private readonly List<T> _conveyor = new();
    private readonly List<Mark> _marks = new();

    public Conveyor(float speed, Vector3 direction, bool isStoped = false)
    {
        Speed = speed;
        Direction = direction;
        IsStoped = isStoped;
    }

    public void Add(T obj)
    {
        _conveyor.Add(obj);
        foreach (Mark mark in _marks)
        {
            mark.SpyOn(obj);
        }
    }

    public bool TryGetFirst(out T obj)
    {
        if(_conveyor.Count == 0)
        {
            obj = null;
            return false;
        }
        else
        {
            obj = _conveyor[0];
            return true;
        }
    }
    public bool TryGetTheClosestTo(Vector3 position, out T result)
    {
        result = null;
        if(_conveyor.Count == 0) return false;

        float minDist = Mathf.Infinity;
        foreach (var obj in _conveyor)
        {
            var objPos = obj.transform.position;
            var objBounds = obj.bounds;
            float distance = Vector3.Distance(position, objBounds.ClosestPoint(position));
            if (distance < minDist)
            {
                minDist = distance;
                result = obj;
            }
            else
            {
                break;
            }
        }
            
        return true;
    }

    public void RemoveFirst()
    {
        if (_conveyor.Count == 0)
            return;

        var obj = _conveyor[0];
        _conveyor.RemoveAt(0);

        foreach (Mark mark in _marks)
        {
            mark.StopSpyOn(obj);
        }
    }

    public void Remove(T obj)
    {
        _conveyor.Remove(obj);

        foreach (Mark mark in _marks)
        {
            mark.StopSpyOn(obj);
        }
    }

    public void SetMark(Vector3 position, Action<T> actionWhenPassMark)
    {
        var newMark = new Mark(position, actionWhenPassMark);
        foreach(var obj in _conveyor)
        {
            newMark.SpyOn(obj);
        }
        _marks.Add(newMark);
    }

    public void Move()
    {
        if (IsStoped)
            return;

        Vector3 move = Direction * Speed * Time.deltaTime;
        foreach (T obj in _conveyor)
        {
            obj.transform.position += move;
            foreach (var mark in _marks)
            {
                mark.Check(obj, Direction);
            }
        }

        foreach (var mark in _marks)
        {
            mark.InvokeActionsForPassedObjects();
        }
    }

    private class Mark
    {
        public Vector3 position;
        public Action<T> action;

        private HashSet<T> spyingObjects = new();
        private List<T> passedObjects = new();

        public Mark(Vector3 position, Action<T> action)
        {
            this.position = position;
            this.action = action;
        }

        public void SpyOn(T obj)
        {
            spyingObjects.Add(obj);
        }

        public void StopSpyOn(T obj)
        {
            spyingObjects.Remove(obj);
        }

        public void Check(T obj, Vector3 direction)
        {
            if(spyingObjects.Contains(obj) && !(Vector3.Dot(obj.transform.position - position, direction) < 0 || obj.bounds.Contains(position)))
            {
                spyingObjects.Remove(obj);
                passedObjects.Add(obj);
            }
        }

        public void InvokeActionsForPassedObjects()
        {
            foreach(var obj in passedObjects)
            {
                action?.Invoke(obj);
            }
            passedObjects.Clear();
        }
    }
}
