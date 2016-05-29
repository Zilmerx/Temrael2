using System;
using Server;

namespace Server.Items
{
	public class NorseHelm : BaseArmor
	{
        //public override int NiveauAttirail { get { return Ring_Niveau; } }

        public override double BasePhysicalResistance { get { return ArmorRingmail.resistance_Physique; } }
        public override double BaseMagicalResistance { get { return ArmorRingmail.resistance_Magique; } }

        public override int InitMinHits { get { return ArmorRingmail.min_Durabilite; } }
        public override int InitMaxHits { get { return ArmorRingmail.max_Durabilite; } }

        public override int BaseStrReq { get { return ArmorRingmail.force_Requise; } }
        public override int BaseDexBonus { get { return ArmorRingmail.malus_Dex; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public NorseHelm() : base( 0x140E )
		{
			Weight = 5.0;
		}

		public NorseHelm( Serial serial ) : base( serial )
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
				Weight = 5.0;
		}
	}
}