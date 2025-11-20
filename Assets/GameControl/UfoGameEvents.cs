using System;
using UnityEngine;

namespace Assets.GameControl
{
    /// <summary>
    /// Игровые события
    /// </summary>
    public static class UfoGameEvents
    {
        public static event Action<GameObject> EnemyInDeadZone;
        public static event Action EnemyIsDead;
        public static event Action EnemyIsLose;
        public static event Action UpdateTimeChange;
        public static event Action LoseGameAction;
        public static event Action StartGameAction;
        //public static event Action<string> ButtonShoot;

        

        /// <summary>
        /// при Старте игры
        /// </summary>
        public static void OnStartGame()
        {
            StartGameAction?.Invoke();
        }

        /// <summary>
        /// При проигрыше игры
        /// </summary>
        public static void onLoseGame()
        {
            LoseGameAction?.Invoke();
        }


        /// <summary>
        /// При обновлении времени
        /// </summary>
        public static void OnUpdateTime()
        {
            UpdateTimeChange?.Invoke();
        }


        /// <summary>
        /// При смерти Enemy
        /// </summary>
        public static void OnEnemyDead()
        {
            EnemyIsDead?.Invoke();
        }

        /// <summary>
        /// При пропуске врага в мертвую зону
        /// </summary>
        public static void OnEnemyLose()
        {
            EnemyIsLose?.Invoke();
        }


        /// <summary>
        /// При пропуске врага в мертвую зону <GameObj>
        /// </summary>
        public static void OnEnemyInDeadZone(GameObject enemy)
        {
            EnemyInDeadZone?.Invoke(enemy);
        }
    }
}
