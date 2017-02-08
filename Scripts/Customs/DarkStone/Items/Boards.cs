// By Neon
// Improved By Dddie

using System; 
using Server; 
using Server.Network; 

namespace Server.Items 
{
   [FlipableAttribute(0x1BD7, 0x1BDA)]
   public class SprucewoodBoard : Item
   { 
      [Constructable]
       public SprucewoodBoard() : base(0x1BD7) 
      { 
         this.Name = "Sprucewood Board"; 
         this.Weight = 1.0; 
         this.Stackable = true;
         this.Hue = 0x04EA;
         
      } 

      public override void OnDoubleClick( Mobile from ) 
      { 
         
      } 

      public SprucewoodBoard( Serial serial ) : base( serial ) 
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
   } 
}