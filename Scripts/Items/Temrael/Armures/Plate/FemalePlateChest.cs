using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x1c04, 0x1c05 )]
	public class FemalePlateChest : BaseArmor
	{
        //public override int NiveauAttirail { get { return Plaque_Niveau; } }

        public override Layer Layer
        {
            get
            {
                return Server.Layer.MiddleTorso;
            }
            set
            {
                base.Layer = value;
            }
        }

        public override double BasePhysicalResistance { get { return ArmorPlaque.resistance_Physique; } }
        public override double BaseMagicalResistance { get { return ArmorPlaque.resistance_Magique; } }

        public override int InitMinHits { get { return ArmorPlaque.min_Durabilite; } }
        public override int InitMaxHits { get { return ArmorPlaque.max_Durabilite; } }

        public override int BaseStrReq { get { return ArmorPlaque.force_Requise; } }
        public override int BaseDexBonus { get { return ArmorPlaque.malus_Dex; } }

		public override bool AllowMaleWearer{ get{ return false; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public FemalePlateChest() : base( 0x1C04 )
		{
			Weight = 4.0;
		}

		public FemalePlateChest( Serial serial ) : base( serial )
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
				Weight = 4.0;
		}
	}
}