using System.Linq;
using System.Threading.Tasks;
using Alsein.Extensions;

namespace Cynthia.Card
{
    [CardEffectId("62003")]//维伯约恩
    public class Vabjorn : CardEffect
    {//对1个单位造成2点伤害。若目标已受伤，则将其摧毁。
        public Vabjorn(GameCard card) : base(card) { }
        public override async Task<int> CardPlayEffect(bool isSpying, bool isReveal)
        {
            var result = await Game.GetSelectPlaceCards(Card, range: 1);
            if (result.Count <= 0) return 0;
                if (result.Status.HealthStatus >= 0)
                {
                    await target.Effect.Damage(2, Card);
                }
                else if (target.Status.HealthStatus < 0)
                {
                    await target.Effect.ToCemetery(CardBreakEffectType.Scorch);
                }
            return 0;
            // WIP
            {
            var result = await Game.GetSelectPlaceCards(Card, range: 1);
            if (result.Count <= 0) return 0; 
            while (result.Status.HealthStatus >= 0)           
                {
                    await target.Effect.Damage(2, Card, BulletType.FireBall);
                }
            return 0;
            }
        }

    }
}