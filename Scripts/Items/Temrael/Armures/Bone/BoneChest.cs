using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x144f, 0x1454 )]
	public class BoneChest : BaseArmor
	{

        public override double BasePhysicalResistance { get { return ArmorBone.resistance_Physique; } }
        public override double BaseMagicalResistance { get { return ArmorBone.resistance_Magique; } }

        public override int InitMinHits { get { return ArmorBone.min_Durabilite; } }
        public override int InitMaxHits { get { return ArmorBone.max_Durabilite; } }

        public override int BaseStrReq { get { return ArmorBone.force_Requise; } }
        public override int BaseDexBonus { get { return ArmorBone.malus_Dex; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Bone; } }
        public override CraftResource DefaultResource { get { return CraftResource.RegularBones; } }

		[Constructable]
		public BoneChest() : base( 0x144F )
		{
			Weight = 6.0;
            Layer = Layer.InnerTorso;
		}

		public BoneChest( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );

			if ( Weight == 1.0 )
				Weight = 6.0;

            Layer = Layer.InnerTorso;
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}