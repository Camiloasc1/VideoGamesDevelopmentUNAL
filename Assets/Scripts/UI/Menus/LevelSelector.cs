﻿using System;
using Game;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Menus
{
    public class LevelSelector : MonoBehaviour
    {
        public SceneLoader sceneLoader;
        public GameObject template;
        public LoadableLevel[] levels;

        private void Start()
        {
            FillLevels();
        }

        private void FillLevels()
        {
            foreach (var level in levels)
            {
                var levelButton = Instantiate(template);
                levelButton.transform.SetParent(transform);
                levelButton.GetComponent<Button>().onClick.AddListener(() => OnLevelButtonClick(level.scene));
                levelButton.GetComponent<Image>().sprite = level.sprite;
                levelButton.GetComponentInChildren<Text>().text = level.name;
                levelButton.SetActive(true);
            }
        }

        public void OnLevelButtonClick(int scene)
        {
            sceneLoader.LoadScene(scene);
        }
    }

    [Serializable]
    public struct LoadableLevel
    {
        public int scene;
        public string name;
        public Sprite sprite;
    }
}