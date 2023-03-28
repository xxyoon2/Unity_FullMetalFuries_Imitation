using UniRx;

namespace Model
{
    public class Model
    {
        public static readonly IntReactiveProperty hpData = new IntReactiveProperty();
        public static readonly StringReactiveProperty endingText = new StringReactiveProperty();

        public static void SetHp(int hp)
        {
            hpData.Value = hp;
        }

        public static void InitHP()
        {
            hpData.Value = 200;
        }

        public static void SetEndingText(string text)
        {
            endingText.Value = text;
        }
    }
}
