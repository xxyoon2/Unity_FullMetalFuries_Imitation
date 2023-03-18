using UniRx;

namespace Model
{
    public class BattleModel
    {
        public static readonly IntReactiveProperty hpData = new IntReactiveProperty();

        public static void SetHp(int hp)
        {
            hpData.Value = hp;
        }

        public static void InitHP()
        {
            hpData.Value = 100;
        }
    }
}
