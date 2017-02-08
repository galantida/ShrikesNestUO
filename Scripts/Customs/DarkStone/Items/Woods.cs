// By Shrike 2/5/17

using System; 
using Server; 
using Server.Network; 

namespace Server.Items 
{
   [FlipableAttribute(0x1bdd, 0x1be0)]
   public class SprucewoodLog : Item , IAxe
   { 
      [Constructable]
       public SprucewoodLog() : base(0x1bdd) 
      { 
         this.Name = "Sprucewood Log"; 
         this.Weight = 2.0; 
         this.Stackable = true;
         this.Hue = 0x04EA; 
      } 

      public override void OnDoubleClick( Mobile from ) 
      { 
         
      } 

      public SprucewoodLog( Serial serial ) : base( serial ) 
      { 
      } 

      public override void Serialize( GenericWriter writer ) 
      { 
         base.Serialize( writer ); 
         writer.Write( (int) 0 ); // version 
      } 

      public override void Deserialize( GenericReader reader ) 
      { 
         base.Deserialize( reader ); 
         int version = reader.ReadInt(); 
      }

      public virtual bool Axe(Mobile from, BaseAxe axe)
      {
          if (!TryCreateBoards(from, 0, new SprucewoodBoard()))
              return false;

          return true;
      }

      public virtual bool TryCreateBoards(Mobile from, double skill, Item item)
      {
          if (Deleted || !from.CanSee(this))
              return false;
          else if (from.Skills.Carpentry.Value < skill &&
              from.Skills.Lumberjacking.Value < skill)
          {
              item.Delete();
              from.SendLocalizedMessage(1072652); // You cannot work this strange and unusual wood.
              return false;
          }
          base.ScissorHelper(from, item, 1, false);
          return true;
      }
   } 
}