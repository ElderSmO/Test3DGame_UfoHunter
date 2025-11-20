
namespace Assets.UFO_Logic
{
    /// <summary>
    /// Интерфейс описывающий врагов
    /// </summary>
    public interface IEnemy
    {
         float ChangeVectorTime { get; set; }
         bool StartMovingRight { get; set; }
         float Speed { get; set; }
         string NameEnemy { get; set; }
         int Hp { get; set; }

        void Move();
        void TakeDamage();
    }
}
