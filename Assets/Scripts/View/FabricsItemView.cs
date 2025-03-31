using System;
using Items;
using TMPro;
using UnityEngine;

namespace View
{
    public class FabricsItemView : MonoBehaviour
    {
        [SerializeField] private TMP_Text itemName;
        [SerializeField] private TMP_Text itemCount;
        [SerializeField] private ItemFactory itemFactory;

        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main;
            itemFactory.OnUpdate += UpdateText;
        }
        
        private void OnDisable()
        {
            itemFactory.OnUpdate -= UpdateText;
        }

        private void UpdateText(string arg1, int arg2)
        {
            itemName.text = arg1;
            itemCount.text = arg2.ToString();
        }

        private void LateUpdate()
        {
            TextRotate(itemName);
            TextRotate(itemCount);
        }

        private void TextRotate(TMP_Text target)
        {
            target.transform.LookAt(_mainCamera.transform);
            target.transform.Rotate(0, 180, 0);
        }
    }
}