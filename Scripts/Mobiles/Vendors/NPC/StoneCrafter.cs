using System;
using System.Collections.Generic;
using Server;

namespace Server.Mobiles
{
	[TypeAlias( "Server.Mobiles.GargoyleStonecrafter" )]
	public class StoneCrafter : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		[Constructable]
		public StoneCrafter() : base( "the stone crafter" )
		{
			SetSkill( SkillName.Menuiserie, 85.0, 100.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBStoneCrafter() );
			m_SBInfos.Add( new SBStavesWeapon() );
			m_SBInfos.Add( new SBCarpenter() );
			m_SBInfos.Add( new SBWoodenShields() );
		}

		public StoneCrafter( Serial serial ) : base( serial )
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

			if ( Title == "the stonecrafter" )
				Title = "the stone crafter";
		}
	}
}