﻿using System;
using Items;
using Kart;
using Player;
using UnityEngine;
using UnityEngine.Events;

public class PlayerRaceInfo
{
    private Item _item;

    public Item Item
    {
        get => _item;
        set
        {
            _item = value;
            onItemSet?.Invoke();
        }
    }

    public bool itemIsInUse;
    public bool ItemIsInUse
    {
        get { return itemIsInUse;}
        set
        {
            itemIsInUse = value;
            onItemUsed?.Invoke(itemIsInUse);
            if (!itemIsInUse) onItemUsed = null;
        }
    }

    private PlayerController _controller;

    public PlayerController Controller => _controller;

    private KartBase _kart;
    public KartBase kart
    {
        get { return _kart; }
        set
        {
            _kart = value;
            onKartChange?.Invoke();
        }
    }

    public int playerId; //on sait jamais
    private int _lap;

    public int lap
    {
        get { return _lap; }
        set
        {
            _lap = value;
            onNewLap?.Invoke();
        }
    } //the current lap

    private int _position;

    public int position
    {
        get { return _position; }
        set
        {
            _position = value;
            onPositionChange?.Invoke();
        }
    } //the kart's position in te race (1rst, 2nd etc)


    public int currentCheckpoint;//the previous checkpoint passed

    private float _bestLapTime;

    public float bestLapTime
    {
        get { return _bestLapTime; }
        set
        {
            _bestLapTime = value;
            onBestLapTimeChange?.Invoke();
        }
    }

    public float previousLapTime;
    public float currentLapStartTime;

    public event Action onPositionChange;
    public event Action onNewLap;
    public event Action onBestLapTimeChange;
    public event Action onKartChange;    
    public event Action onItemSet;    
    public event Action<bool> onItemUsed;    

    public PlayerRaceInfo(KartBase k, int id, IActions action)
    {
        bestLapTime = float.MaxValue;
        previousLapTime = float.MaxValue;
        kart = k;
        playerId = id;
        lap = 1;
        position = playerId;
        currentCheckpoint = 0;
        currentLapStartTime = 0f;
        _controller = new PlayerController(this, action);
        kart.GetPlayerID += () => playerId;
        ItemIsInUse = false;
    }

    
}