﻿/***
 * Author: Yunhan Li 
 * Any issue please contact yunhn.lee@gmail.com
 ***/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;

namespace VRKeyboard.Utils {
    public class KeyboardManager : MonoBehaviour {
        #region Public Variables
        [Header("User defined")]
        [Tooltip("If the character is uppercase at the initialization")]
        public bool isUppercase = false;
        public int maxInputLength;
        
        [Header("UI Elements")]
        public Text inputText;

        [Header("Essentials")]
        public Transform characters;
        #endregion

        #region Private Variables
        private string Input {
            get { return inputText.text;  }
            set { inputText.text = value;  }
        }

        Action<KeyCode> cbKeyPressed;

        private Dictionary<GameObject, Text> keysDictionary = new Dictionary<GameObject, Text>();

        private bool capslockFlag;
        #endregion

        #region Monobehaviour Callbacks
        private void Awake() {
            
            for (int i = 0; i < characters.childCount; i++) {
                GameObject key = characters.GetChild(i).gameObject;
                Text _text = key.GetComponentInChildren<Text>();
                keysDictionary.Add(key, _text);
                KeyCode thisKeyCode;
                if (key != null)
                {
                    thisKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), key.name);
                    cbKeyPressed += ((kC) => { if (kC == thisKeyCode) { Keystroke(key); } });
                }
           
                
                key.GetComponent<Button>().onClick.AddListener(() => {
                    GenerateInput(_text.text);
                });
            }

            capslockFlag = isUppercase;
            CapsLock();
        }
        #endregion

        #region Public Methods
        public void Backspace() {
            if (Input.Length > 0) {
                Input = Input.Remove(Input.Length - 1);
            } else {
                return;
            }
        }

        public void Clear() {
            Input = "";
        }

        public void CapsLock() {
            if (capslockFlag) {
                foreach (var pair in keysDictionary) {
                    pair.Value.text = ToUpperCase(pair.Value.text);
                }
            } else {
                foreach (var pair in keysDictionary) {
                    pair.Value.text = ToLowerCase(pair.Value.text);
                }
            }
            capslockFlag = !capslockFlag;
        }
        #endregion

        #region Private Methods
        public void GenerateInput(string s) {
            if (Input.Length > maxInputLength) { return; }
            Input += s;
        }

        private string ToLowerCase(string s) {
            return s.ToLower();
        }

        private string ToUpperCase(string s) {
            return s.ToUpper();
        }
        #endregion

        private void Update()
        {
            if (UnityEngine.Input.anyKeyDown)
            {
                foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (UnityEngine.Input.GetKeyDown(vKey))
                    {
                        cbKeyPressed(vKey);
                    }
                }
            }
        }

        void Keystroke(GameObject go)
        {
            var pointer = new PointerEventData(EventSystem.current);
            ExecuteEvents.Execute(go, pointer, ExecuteEvents.pointerClickHandler);
        }
    }
}