using System.Linq;
using System.Threading.Tasks;
using Alsein.Extensions;

namespace Cynthia.Card
{
    [CardEffectId("70168")]//齐齐摩战士
    public class KikimoreStalker : CardEffect
    {
        //吞噬己方牌组中1个战力不大于自身的非同名铜色单位牌，获得等同于其基础战力的增益
        public KikimoreStalker(GameCard card) : base(card) { }
        public override async Task<int> CardPlayEffect(bool isSpying, bool isReveal)
        {
            int buffvalue = 0;
            var deckcards = Game.PlayersDeck[PlayerIndex].Where(x => x.Status.CardId == Card.Status.CardId).ToList();
            var deadcards = Game.PlayersCemetery[PlayerIndex].Where(x => x.Status.CardId == Card.Status.CardId).ToList();
            if (deckcards.Count() != 0)
            {

                foreach (var deckcard in deckcards)
                {
                    buffvalue += deckcard.Status.Strength;
                    await deckcard.Effect.ToCemetery();             
                }
            }
            if (deadcards.Count() != 0)
            {
                foreach (var deadcard in deadcards)
                {
                    buffvalue += deadcard.Status.Strength;
                    await deadcard.Effect.Banish();             
                }
            }
            await Boost(buffvalue, Card);
            await Game.SendEvent(new AfterCardConsume(null, Card));
        return 0;
        }
    }
}
