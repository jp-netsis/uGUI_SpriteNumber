using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using jp.netsis.Utility;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace jp.netsis
{
    [RequireComponent(typeof(SpriteNumber))]
    public class ExampleSpriteNumber : MonoBehaviour
    {
        public double _showNumberDummy;
        public string _format;
        public string _cultureInfo;
        public bool _isReverseDraw;

        public bool _update;

        private SpriteNumber _spriteNumber;

        private void Awake()
        {
            _spriteNumber = GetComponent<SpriteNumber>();
        }

        private void OnEnable()
        {
            _spriteNumber.Show(_showNumberDummy,_cultureInfo,_format);
        }

        private void Update()
        {
            if (_update)
            {
                _update = false;
                _spriteNumber.Show(_showNumberDummy,_cultureInfo,_format,_isReverseDraw);
            }
        }
    }
}