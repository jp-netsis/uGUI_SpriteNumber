using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jp.netsis.Utility
{
    [CreateAssetMenu(fileName = "SpriteNumbersScriptableObject",menuName = "netsis/Utility/SpriteNumbers ScriptableObject")]
    public class SpriteNumbersScriptableObject : ScriptableObject
    {
        [SerializeField]
        private Sprite[] _sprites = new Sprite[15]; // 0-9 - . % ' ,
        public Sprite[] Sprites => _sprites;
        [SerializeField]
        private float _offset_width;
        public float OffsetWidth => _offset_width;
        [SerializeField]
        private float _space_width;
        public float SpaceWidth => _space_width;
    }
}
