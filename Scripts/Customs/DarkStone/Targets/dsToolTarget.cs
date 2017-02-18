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
			m_Item = item;
		}

        protected override void OnTarget(Mobile from, object targeted)
        {
            // verify this targfet really is an IdsToolTarget
            if (targeted is IdsToolTargetItem)
            {
                ((IdsToolTargetItem)targeted).onToolTarget(from, m_Item);
            }
        }
	}
}