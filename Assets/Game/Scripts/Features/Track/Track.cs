using System;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class Track : MonoBehaviour, IPausable
{
    [SerializeField] TrackConfig config;
    [SerializeField] Note.Directions noteType;
    [SerializeField] InputActionReference actionReference;
    [SerializeField] Transform hitLine;
    [SerializeField] TrackView view;


    public Action OnMiss;

    public Action<HitType> OnHit;
    public Note.Directions NoteType => noteType;

    public bool HasNotes
    {
        get
        {
            return conveyor.ObjectsCount > 0;
        }
    }

    public float Speed
    {
        get => conveyor.Speed;
        set => conveyor.Speed = value;
    }

    private InputAction Action => actionReference.action;

    private Conveyor<Note> conveyor;

    private void Awake()
    {
        conveyor = new(0f, Vector3.down);
        conveyor.SetMark(hitLine.position + Vector3.down * config.HitDistance, OnNotePassHitLine);
        conveyor.SetMark(hitLine.position + Vector3.down * config.DisappearOffset, OnNoteRichEnd);

        OnHit += view.OnHit;
        OnMiss += view.OnMiss;       
    }

    public void AddNote(Note note, float delay)
    {
        note.transform.position = hitLine.position + Vector3.up * Speed * delay;
        note.OnHit += OnHitNote;
        note.OnMiss += OnNoteMiss;

        conveyor.Add(note);
    }

    private void OnHitNote(HitType hitType)
    {
        OnHit?.Invoke(hitType);
    }

    private void OnNoteMiss()
    {
        OnMiss?.Invoke();
    }

    private void OnNotePassHitLine(Note note)
    {
        OnMiss?.Invoke();
    }

    private void OnNoteRichEnd(Note note)
    {
        conveyor.Remove(note);
        Destroy(note.gameObject);
    }

    private void OnEnable()
    {
        Action.Enable();
    }

    private void OnDisable()
    {
        Action.Disable();
    }

    private void Update()
    {
        conveyor.Move();
        ProcessInput();
    }

    private void ProcessInput()
    {
        bool wasPressed = Action.WasPressedThisFrame(), wasReleased = Action.WasReleasedThisFrame(), isPressed = Action.IsPressed();
        if (wasPressed || wasReleased || isPressed) 
        {
            if (conveyor.TryGetTheClosestTo(hitLine.position, out Note note))
            {
                if((wasPressed && note.TryPress(hitLine.position)) || 
                    (isPressed && note.TryHold(hitLine.position)) || 
                    (wasReleased && note.TryRelease(hitLine.position)))
                {
                    conveyor.Remove(note);
                }
            }
            else
            {
                if (wasPressed)
                    OnMiss?.Invoke();
            }
        }
    }

    public void Pause()
    {
        conveyor.IsStoped = true;
        OnDisable();
    }

    public void Resume()
    {
        conveyor.IsStoped = false;
        OnEnable();
    }
}
