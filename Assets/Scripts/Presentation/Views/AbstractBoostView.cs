using Presentation.Models;
using System.Collections;
using UnityEngine;
using Universal;

namespace Presentation.Views
{
    public abstract class AbstractBoostView : MonoBehaviour
    {
        [SerializeField]
        internal BoostType Type;

        private IEnumerator coroutine;

        internal virtual void OnGather()
        {
            PresentationViewModel.OnGather(this);
        }

        internal void ResumeCoroutine() => StartCoroutine(coroutine);

        internal void PauseCoroutine() => StopCoroutine(coroutine);

        private void Awake()
        {   
            coroutine = MoveDown();
            StartCoroutine(coroutine);
        }

        IEnumerator MoveDown()
        {
            while (true)
            {                
                transform.position += new Vector3(0f, -1f * Time.deltaTime, 0f);
                yield return null;
            }
        }
    }
}