using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Reflection;

namespace ProgressCounter
{
    public class Counter : MonoBehaviour
    {
        TextMeshPro _mesh;
        AudioTimeSyncController _audioTimeSync;

        IEnumerator WaitForLoad()
        {
            bool loaded = false;
            while (!loaded)
            {
                _audioTimeSync = Resources.FindObjectsOfTypeAll<AudioTimeSyncController>().FirstOrDefault();

                if (_audioTimeSync == null)
                    yield return new WaitForSeconds(0.01f);
                else
                    loaded = true;
            }

            Init();
        }

        private void Awake()
        {
            StartCoroutine(WaitForLoad());
        }

        void Init()
        {
            _mesh = this.gameObject.AddComponent<TextMeshPro>();
            _mesh.text = "0%";
            _mesh.fontSize = 3.5f;
            _mesh.color = Color.white;
            _mesh.font = Resources.Load<TMP_FontAsset>("Teko-Medium SDF No Glow");
            _mesh.alignment = TextAlignmentOptions.Center;
            _mesh.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1f);
            _mesh.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1f);
            _mesh.rectTransform.position = new Vector3(-3.75f, 1.2f, 7f) - new Vector3(_mesh.rectTransform.offsetMin.x, _mesh.rectTransform.offsetMin.y);
        }

        void Update()
        {
            if (_audioTimeSync == false)
            {
                _audioTimeSync = Resources.FindObjectsOfTypeAll<AudioTimeSyncController>().FirstOrDefault();
                return;
            }

            var time = (_audioTimeSync.songTime / _audioTimeSync.songLength) * 100f;
            _mesh.text = time.ToString("0.0") + "%";
        }
    }
}
