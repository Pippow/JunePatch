using System.Linq;
using System.Threading.Tasks;
using Alsein.Extensions;

namespace Cynthia.Card
{
    [CardEffectId("23011")]//尼斯里拉 Nithral
    public class Nithral : CardEffect
    {//Deals X damage to 1 enemy unit. For each Wild Hunt unit card in your hand, increase damage by 1 point and for each wild hunt card on the board boost self by 1
        public Nithral(GameCard card) : base(card) { }
        public override async Task<int> CardPlayEffect(bool isSpying, bool isReveal)
        {
            //Boost self by 1 for every wild hunt on the board
            var Boostnum = Game.GetPlaceCards(Card.PlayerIndex).FilterCards(filter: x => x.HasAllCategorie(Categorie.WildHunt) && x != Card).ToList().Count();
            await Card.Effect.Boost(Boostnum, Card);
            //Damage an enemy by X, increase by 1 for each wild hunt card in your deck
            var damagepoint = Game.PlayersHandCard[PlayerIndex].Where(x => x.HasAllCategorie(Categorie.WildHunt)).Count();
            var result = (await Game.GetSelectPlaceCards(Card, selectMode: SelectModeType.EnemyRow));
            if (result.Count != 0) await result.Single().Effect.Damage(7 + damagepoint, Card);
            return 0;
        }
    }
}