using System;
using System.Collections.Generic;
using Server;

namespace Server.Mobiles
{
	public class Bard : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		[Constructable]
		public Bard() : base( "Barde" )
		{
			//SetSkill( SkillName.Discordance, 64.0, 100.0 );
			SetSkill( SkillName.Musique, 64.0, 100.0 );
			//SetSkill( SkillName.Peacemaking, 65.0, 88.0 );
			//SetSkill( SkillName.Provocation, 60.0, 83.0 );
			SetSkill( SkillName.ArmeDistance, 36.0, 68.0 );
			SetSkill( SkillName.Epee, 36.0, 68.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBBard() );
		}

		public Bard( Serial serial ) : base( serial )
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