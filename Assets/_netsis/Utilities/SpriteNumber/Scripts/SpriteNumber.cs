using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace jp.netsis.Utility
{
    [ExecuteAlways]
    public class SpriteNumber : MonoBehaviour
    {
        [SerializeField]
        private SpriteNumbersScriptableObject _spriteNumbersScriptableObject;

        private RectTransform _rectTransform;
        private RectTransform RectTransform {
            get
            {
                if (_rectTransform == null)
                {
                    _rectTransform = transform as RectTransform;
                }
                return _rectTransform;
            }
        }
        private List<GameObject> _gameObjectList = new();

        private void OnDisable()
        {
            if (0 < _gameObjectList.Count)
            {
                for (int n = _gameObjectList.Count - 1; n >= 0; --n)
                {
                    Destroy(_gameObjectList[n]);
                }
                _gameObjectList.Clear();
            }
        }

        public void Show(int parameter,string _cultureInfo = null, string format = null, bool isReverseDraw = false)
        {
            Rendering(string.Format(CultureInfo.GetCultureInfo(_cultureInfo),format,parameter),isReverseDraw);
        }
        public void Show(double parameter,string _cultureInfo = null, string format = null, bool isReverseDraw = false)
        {
            Rendering(string.Format(CultureInfo.GetCultureInfo(_cultureInfo),format,parameter),isReverseDraw);
        }
        public void Show(float parameter,string _cultureInfo = null, string format = null, bool isReverseDraw = false)
        {
            Rendering(string.Format(CultureInfo.GetCultureInfo(_cultureInfo),format,parameter),isReverseDraw);
        }

        void Rendering(string numText, bool isReverseDraw = false)
        {
            if (_spriteNumbersScriptableObject == null)
            {
                return;
            }

            float pos_x = 0f;
            int goIndex = 0;
            for (int n = 0; n < numText.Length; ++n)
            {
                var character = numText[n].ToString();
                var sprite = _spriteNumbersScriptableObject.Sprites.FirstOrDefault(x => x.name == character);
                if (sprite == null)
                {
                    if (string.IsNullOrWhiteSpace(character))
                    {
                        pos_x += _spriteNumbersScriptableObject.SpaceWidth + _spriteNumbersScriptableObject.OffsetWidth;
                    }
                    continue;
                }

                Generate(goIndex, sprite.name, out GameObject instance);
                SetSprite(sprite, instance, out Vector2 spriteSize);
                instance.transform.SetSiblingIndex(n);
                if (instance.transform is RectTransform rt)
                {
                    rt.anchoredPosition = new Vector2(pos_x, 0);
                    pos_x += spriteSize.x + _spriteNumbersScriptableObject.OffsetWidth;
                }

                goIndex++;
            }
            // unvisible other objects
            for (int n = goIndex; goIndex < _gameObjectList.Count; ++goIndex)
            {
                _gameObjectList[goIndex].SetActive(false); 
            }
            // draw 
            if (isReverseDraw)
            {
                int max = _gameObjectList.Count - 1;
                for (int n = 0; n < _gameObjectList.Count; ++n)
                {
                    _gameObjectList[n].transform.SetSiblingIndex(max - n);
                }
            }
        }

        void Generate(int n, string name, out GameObject instance)
        {
            if (n < _gameObjectList.Count)
            {
                _gameObjectList[n].name = name;
                instance = _gameObjectList[n];
                instance.SetActive(true);
                return;
            }
            
            instance = new GameObject(name);
            instance.AddComponent<RectTransform>();
            instance.AddComponent<Image>();
            instance.transform.SetParent(RectTransform);

            _gameObjectList.Add(instance);
        }

        void SetSprite(Sprite sprite, GameObject instance, out Vector2 spriteSize)
        {
            var rectTrans = instance.GetComponent<RectTransform>();
            var image = instance.GetComponent<Image>();
            instance.transform.localScale = Vector3.one;
            spriteSize.x = sprite.rect.width;
            spriteSize.y = sprite.rect.height;
            rectTrans.anchorMin = new Vector2(0f, 0f);
            rectTrans.anchorMax = new Vector2(0f, 0f);
            rectTrans.pivot = new Vector2(0f, 0f);
            rectTrans.sizeDelta = spriteSize;

            image.sprite = sprite;
            image.raycastTarget = false;
        }
    }
}