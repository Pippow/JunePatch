using System.Linq;
using System.Threading.Tasks;
using Alsein.Extensions;

namespace Cynthia.Card
{
    [CardEffectId("70168")]//Caranthir Golden Child - 
    public class CaranthirGoldenChild : CardEffect, IHandlesEvent<AfterCardHurt>
    {//boost self by 1 whenever a unit is destroyed by frost
        public CaranthirGoldenChild(GameCard card) : base(card) { }
        private GameCard WildHuntTarget = null;
        public async Task HandleEvent(AfterCardHurt @event) // boost self by 1 whenever a unit is destroyed by frost
        {
            if (@event.Target.PlayerIndex != Card.PlayerIndex && @event.Target.Status.Type == CardType.Unit && @event.Target.IsDead && @event.DamageType.IsHazard())
                {
                     await Card.Effect.Boost(1, Card);
                }
        }
        public override async Task<int> CardPlayEffect(bool isSpying, bool isReveal) // play a wild hunt unit with lower strength
        {
        var list = Game.PlayersCemetery[Card.PlayerIndex]
        .Where(x => (x.Status.Group == Group.Copper) && x.CardInfo().CardType == CardType.Unit && x.HasAnyCategorie(Categorie.WildHunt) && x.CardPoint() <= Card.CardPoint());
        if (list.Count() == 0)
        {
            return 0;
        }
        var result = await Game.GetSelectMenuCards(Card.PlayerIndex, list.ToList(), 1);
        if (result.Count() == 0) return 0;
        WildHuntTarget = result.First();
        await WildHuntTarget.Effect.Resurrect(new CardLocation() { RowPosition = RowPosition.MyStay, CardIndex = 0 }, Card);
        return 1;
        }
    }
}