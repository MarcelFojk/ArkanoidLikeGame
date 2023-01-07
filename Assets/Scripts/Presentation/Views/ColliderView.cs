using Presentation.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal;

namespace Presentation.Views
{
    class ColliderView : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Ball"))
                if (PresentationViewModel.OnBallMiss(collider.GetComponent<BallView>()))
                    Signals.BallMissed?.Invoke();
        }
    }
}