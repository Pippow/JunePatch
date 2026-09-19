using System.Linq;
using System.Threading.Tasks;
using Alsein.Extensions;

namespace Cynthia.Card
{
    [CardEffectId("62001")]//奥拉夫
    public class Olaf : CardEffect, IHandlesEvent<AfterUnitPlay>

    {//对自身造成10点伤害。本次对局己方每打出过1只“野兽”，伤害便减少2点。
        public Olaf(GameCard card) : base(card)
        {
            UpdateDamageDescription();
        }
        private int beastnum = 0;
        private int DamageAmount => beastnum <= 5 ? 12 - 2 * beastnum : 0;

        private void UpdateDamageDescription()
        {
            UpdateDescription("damage", DamageAmount);
        }

        public override async Task<int> CardPlayEffect(bool isSpying, bool isReveal)
        {
            UpdateDamageDescription();
            //下面代码基于://必须是我方打出的野兽，吉尔曼召唤这种不行。间谍野兽也不算（对面打出）
            if (beastnum <= 5)
            {
                await Card.Effect.Damage(DamageAmount, Card);
            }
            return 0;
        }
        public async Task HandleEvent(AfterUnitPlay @event)
        {
            //必须是我方打出，所以间谍野兽不算（对面打出）
            if (@event.PlayedCard.HasAllCategorie(Categorie.Beast) && @event.PlayedCard.PlayerIndex == Card.PlayerIndex && @event.PlayedCard.CardInfo().CardUseInfo == CardUseInfo.MyRow && @event.PlayedCard != Card)
            {
                beastnum++;
                UpdateDamageDescription();
                await Game.ShowSetCard(Card);
            }
            await Task.CompletedTask;
        }

    }
}