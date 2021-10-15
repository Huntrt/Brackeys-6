using System;

///Entity stats
[Serializable] public class EntityStats {public float speed, attackSpeed;}

///Attacker stats
[Serializable] public class BarrageStats {public int amount;}

///Attack stats
[Serializable] public class CombatStats {public float damage, velocity, range;}
[Serializable] public class PiercingStats {public int amount;}

[Serializable] public class ExplosionStats {public bool use; public float scaleDamage, size;}

[Serializable] public class HomingStats {public bool use; public float accuracy, release;}

[Serializable] public class RicochetStats {public bool use; public float range;}