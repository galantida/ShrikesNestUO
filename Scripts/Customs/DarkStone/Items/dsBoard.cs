// By Neon
// Improved By Dddie

using System; 
using Server; 
using Server.Network;
using System.Collections.Generic;

namespace Server.Items 
{
   [FlipableAttribute(0x1BD7, 0x1BDA)]
    public class dsBoard : Item
   { 
      private DarkStone.WoodType woodType;

      [Constructable]
       public dsBoard(DarkStone.WoodType woodType) : base(0x1BD7) 
      {
        Dictionary<DarkStone.WoodType, int> hues = new Dictionary<DarkStone.WoodType, int>();

        // red colored woods
        hues.Add(DarkStone.WoodType.Rosewood, 39); 
        hues.Add(DarkStone.WoodType.Redwood, 40);
        hues.Add(DarkStone.WoodType.Cedarwood, 140);
        hues.Add(DarkStone.WoodType.Cherrywood, 240);
          
          // brown colored woods
        hues.Add(DarkStone.WoodType.Mahogany, 842);
        hues.Add(DarkStone.WoodType.Walnut, 742);
        hues.Add(DarkStone.WoodType.Teakwood, 642);
        hues.Add(DarkStone.WoodType.Oakwood, 542);
        hues.Add(DarkStone.WoodType.Yew, 442);
        hues.Add(DarkStone.WoodType.Hickory, 342);

        // yellow colored woods
        hues.Add(DarkStone.WoodType.Maplewood, 51);
        hues.Add(DarkStone.WoodType.Birchwood, 151);
        hues.Add(DarkStone.WoodType.Ashwood, 251);
        hues.Add(DarkStone.WoodType.Pinewood, 351);
        hues.Add(DarkStone.WoodType.Firwood, 451);
        hues.Add(DarkStone.WoodType.Sprucewood, 551);
        hues.Add(DarkStone.WoodType.Cypress, 651);
        hues.Add(DarkStone.WoodType.Hemlock, 751);


        this.woodType = woodType;
        this.Name = woodType.ToString() + " Board";
        this.Weight = 1.0;
        this.Stackable = true;
        this.Hue = hues[woodType];
      } 

      public dsBoard( Serial serial ) : base( serial ) 
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