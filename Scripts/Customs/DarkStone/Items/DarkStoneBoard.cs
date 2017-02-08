// By Neon
// Improved By Dddie

using System; 
using Server; 
using Server.Network; 

namespace Server.Items 
{
   [FlipableAttribute(0x1BD7, 0x1BDA)]
   public class DarkStoneBoard : Item
   { 
        private DarkStoneWoodType woodType;

      [Constructable]
       public DarkStoneBoard(DarkStoneWoodType woodType) : base(0x1BD7) 
      {
            this.woodType = woodType;
            this.Name = woodType.ToString() + " Board";
            this.Weight = 1.0;
            this.Stackable = true;

            // pick hue
            switch (woodType)
            {
                case DarkStoneWoodType.Sprucewood:
                    this.Hue = 0x04EA;
                    break;
                default:
                    // nothing
                    break;
            }
      } 

      public DarkStoneBoard( Serial serial ) : base( serial ) 
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

       public override void OnDoubleClick( Mobile from ) 
      { 
         
      } 
   } 
}