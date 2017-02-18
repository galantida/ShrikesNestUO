using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Targets
{
    public class dsToolTargetTimer : Timer
    {
        private Mobile m_From;
        private Item m_Tool;

        public dsToolTargetTimer(Mobile from, Item tool)
            : base(TimeSpan.Zero, new TimeSpan(1000))
        {
            Console.WriteLine("new dsToolTargetTimer");
            m_From = from;
            m_Tool = tool;
        }

        protected override void OnTick()
        {
            Console.WriteLine("Tick");

            // check from target
            if (m_From.Target != null)
            {
                this.Stop();
                // we have a target (Assume its an item for now)
                //Item targetItem = new Item(m_From.Target.TargetID);

                // (assume its a dsToolTargetItem for now)
                //executeTool((IdsToolTargetItem)targetItem);
            }
        }

        protected void executeTool(IdsToolTargetItem target)
        {
            Console.WriteLine("ExecuteTool");
            target.onToolTarget(m_From, m_Tool);
        }
    }
}
