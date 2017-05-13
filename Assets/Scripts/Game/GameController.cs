﻿using System;
using UnityEngine;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        private static GameController gameController;

        public static GameController Instance
        {
            get
            {
                if (!gameController)
                {
                    gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
                    if (!gameController)
                    {
                        throw new ArgumentNullException("gameController", "GameController not found");
                    }
                }
                return gameController;
            }
        }

        public static GameMode GameMode
        {
            get
            {
                var gameMode = Instance.GetComponent<GameMode>();
                if (!gameMode)
                {
                    throw new ArgumentNullException("gameMode", "gameMode not found in GameController");
                }
                return gameMode;
            }
        }

        public static Prefs Prefs
        {
            get
            {
                var prefs = Instance.GetComponentInChildren<Prefs>();
                if (!prefs)
                {
                    throw new ArgumentNullException("prefs", "prefs not found in GameController");
                }
                return prefs;
            }
        }
    }
}