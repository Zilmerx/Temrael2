using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x13ec, 0x13ed )]
	public class RingmailChest : BaseArmor
	{
        //public override int NiveauAttirail { get { return Ring_Niveau; } }

        public override double BasePhysicalResistance { get { return ArmorRingmail.resistance_Physique; } }
        public override double BaseMagicalResistance { get { return ArmorRingmail.resistance_Magique; } }

        public override int InitMinHits { get { return ArmorRingmail.min_Durabilite; } }
        public override int InitMaxHits { get { return ArmorRingmail.max_Durabilite; } }

        public override int BaseStrReq { get { return ArmorRingmail.force_Requise; } }
        public override int BaseDexBonus { get { return ArmorRingmail.malus_Dex; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Ringmail; } }

		[Constructable]
		public RingmailChest() : base( 0x13EC )
		{
			Weight = 15.0;
		}

		public RingmailChest( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( Weight == 1.0 )
				Weight = 15.0;
		}
	}
}