using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM1.BeatDetector
{
    public class BeatDetector : MonoBehaviour
    {
        public static BeatDetector Instance { get; private set; } = default;

        [Tooltip("BPM"), SerializeField]
        float bpm = 130;

        AudioSource audioSource = default;

        /// <summary>
        /// 現在のサンプル数を返します。
        /// </summary>
        public static int CurrentTimeSamples
        {
            get
            {
                return Instance.audioSource.timeSamples;
            }
        }

        /// <summary>
        /// FixedUpdateごとのサンプル数
        /// </summary>
        public static float SamplesPerFixedFrame { get; private set; } = default;

        /// <summary>
        /// 現在の曲の周波数
        /// </summary>
        public static float Frequency { get; private set; }

        private void Awake()
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            Frequency = audioSource.clip.frequency;
            SamplesPerFixedFrame = Frequency * Time.fixedDeltaTime;
        }

        /// <summary>
        /// 指定のビートの時のサンプル値を返します。
        /// </summary>
        /// <param name="beat"></param>
        /// <returns></returns>
        public float GetSamplesWithBeat(float beat)
        {
            return Frequency * (60f * beat / bpm);
        }
    }
}