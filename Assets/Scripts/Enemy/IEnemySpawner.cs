using System;

namespace Enemy
{
    public interface IEnemySpawner
    {
        event Action EnemiesDied;
        event Action AllEnemiesDied;
    }
}