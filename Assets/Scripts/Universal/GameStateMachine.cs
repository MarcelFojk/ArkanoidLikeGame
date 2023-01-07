using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using AsyncOperation = UnityEngine.AsyncOperation;
using Debug = UnityEngine.Debug;

namespace Universal
{
    public class GameStateMachine<T> where T : Enum
    {
        static List<StateTransitions> _stateTransitions = new();
        static List<StateEntry> _stateEntry = new();

        static Dictionary<StateTransitions, StateEntry> _statesContainer = new();

        static T _currentState;

        public GameStateMachine(IReadOnlyList<(T fromState, T toState, string[] statesToLoad, string[] statesToUnload)> transitions,
            IReadOnlyList<(T entryState, Action onEntry, Action onExit)> stateEntry, T initialState)
        {
            _currentState = initialState;

            for (int i = 0; i < transitions.Count; i++)
            {
                (T from, T to, string[] statesToLoad, string[] statesToUnload) = transitions[i];
                _stateTransitions.Add(new StateTransitions(from, to, statesToLoad, statesToUnload));
            }

            for (int i = 0; i < stateEntry.Count; i++)
            {
                (T entry, Action onEntry, Action onExit) = stateEntry[i];
                _stateEntry.Add(new StateEntry(entry, onEntry, onExit));
            }

            for (int i = 0; i < _stateTransitions.Count; i++)
                for (int j = 0; j < _stateEntry.Count; j++)
                    if (_stateTransitions[i].ToState.Equals(_stateEntry[j].EntryState))
                        _statesContainer.Add(_stateTransitions[i], _stateEntry[j]);
        }

        public static async void ChangeGameState(T gameState)
        {
            List<StateTransitions> transitions = _stateTransitions.FindAll(t => Equal(t.FromState, _currentState) && Equal(t.ToState, gameState));

            for (int i = 0; i < transitions.Count; i++)
            {
                await LoadScenes(transitions[i]);
                _statesContainer[transitions[i]].OnEntry?.Invoke();
            }

            _currentState = gameState;

            for (int i = 0; i < transitions.Count; i++)
                await UnloadScenes(transitions[i]);
        }

        private static async Task LoadScenes(StateTransitions transition)
        {
            AsyncOperation[] asyncOperations = new AsyncOperation[transition.StatesToLoad.Length];

            for (int i = 0; i < transition.StatesToLoad.Length; i++)
                asyncOperations[i] = SceneManager.LoadSceneAsync(transition.StatesToLoad[i], LoadSceneMode.Additive);

            while (!asyncOperations.All(t => t.isDone))
                await Task.Delay(1);
        }

        private static async Task UnloadScenes(StateTransitions transition)
        {
            if (transition.StatesToUnload.Length == 0)
                return;

            AsyncOperation[] asyncOperations = new AsyncOperation[transition.StatesToUnload.Length];

            for (int i = 0; i < transition.StatesToUnload.Length; i++)
            {
                asyncOperations[i] = SceneManager.UnloadSceneAsync(transition.StatesToUnload[i]);

                _stateEntry.Find(t => t.EntryState.ToString() == transition.StatesToUnload[i]).OnExit?.Invoke();
            }

            while (!asyncOperations.All(t => t.isDone))
                await Task.Delay(1);
        }

        static bool Equal(Enum a, Enum b) => Enum.GetName(a.GetType(), a) == Enum.GetName(b.GetType(), b);

        struct StateTransitions
        {
            public T FromState;
            public T ToState;
            public string[] StatesToLoad;
            public string[] StatesToUnload;

            public StateTransitions(T fromState, T toState, string[] statesToLoad, string[] statesToUnload)
            {
                FromState = fromState;
                ToState = toState;
                StatesToLoad = statesToLoad;
                StatesToUnload = statesToUnload;
            }
        }

        struct StateEntry
        {
            public T EntryState;
            public Action OnEntry;
            public Action OnExit;

            public StateEntry(T entryState, Action onEntry, Action onExit)
            {
                EntryState = entryState;
                OnEntry = onEntry;
                OnExit = onExit;
            }
        }
    }
}