using System;
using Server;
using Server.Targeting;
using Server.Items;
using Server.Engines.Harvest;
using Server.Mobiles;
using Server.Engines.Quests;
using Server.Engines.Quests.Hag;

namespace Server.Targets
{
    public interface IdsToolTargetItem
    {
        bool onToolTarget(Mobile from, Item tool);
    }

	public class dsToolTarget : Target
	{
		public Item m_Item; // was private

		public dsToolTarget( Item item ) : base( 2, false, TargetFlags.None )
		{
            Console.WriteLine("New dsToolTarget");
			m_Item = item;
		}

        protected override void OnTarget(Mobile from, object targeted)
        {
            // need verify this really is a IdsToolTarget
            executeToolTarget(from, (IdsToolTargetItem)targeted);
        }

        protected void executeToolTarget(Mobile from, IdsToolTargetItem dsToolTargetItem)
        {
            dsToolTargetItem.onToolTarget(from, m_Item);
        }
	}
}