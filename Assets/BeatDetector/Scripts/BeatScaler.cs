using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM1.BeatDetector
{
    public class BeatScaler : MonoBehaviour
    {
        [Tooltip("基本サイズ"), SerializeField]
        float defaultScale = 1;
        [Tooltip("ビート時のサイズ"), SerializeField]
        float beatScale = 1.5f;
        [Tooltip("ビート減衰秒数"), SerializeField]
        float beatDampingSeconds = 0.1f;

        Transform quadTransform = default;

        /// <summary>
        /// ビートの経過秒数
        /// </summary>
        float beatTime = 0;

        /// <summary>
        /// ビート回数
        /// </summary>
        int beatCount = 0;

        private void Awake()
        {
            quadTransform = transform.GetChild(0);
        }

        void FixedUpdate()
        {
            var current = BeatDetector.CurrentTimeSamples;
            var nextBeat = BeatDetector.Instance.GetSamplesWithBeat(beatCount);

            // 現在のtimeSamlesが次のビートタイミングを越えているか
            if (current >= nextBeat)
            {
                beatTime = (current - nextBeat) / BeatDetector.Frequency;
                beatCount++;
            }

            // 大きさ調整
            var t = beatTime / beatDampingSeconds;
            var sc = Mathf.Lerp(beatScale, defaultScale, t);
            quadTransform.localScale = Vector3.one * sc;
            beatTime += Time.fixedDeltaTime;
        }

        private void OnGUI()
        {
            GUI.color = Color.red;
            GUI.Label(new Rect(20, 20, 1000, 30), $"{beatCount} {BeatDetector.Instance.GetSamplesWithBeat(beatCount)} {BeatDetector.CurrentTimeSamples} ");
            var t = beatTime - Time.time;
            GUI.Label(new Rect(20, 50, 1000, 30), $"{beatTime:0.00} {beatTime/beatDampingSeconds:0.00}");
        }
    }
}