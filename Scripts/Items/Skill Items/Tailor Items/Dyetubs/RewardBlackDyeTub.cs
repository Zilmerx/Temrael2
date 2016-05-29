using System;

namespace Server.Items
{
	public class RewardBlackDyeTub : DyeTub
	{
		public override int LabelNumber{ get{ return 1006008; } } // Black Dye Tub

		private bool m_IsRewardItem;

		[CommandProperty( AccessLevel.Batisseur )]
		public bool IsRewardItem
		{
			get{ return m_IsRewardItem; }
			set{ m_IsRewardItem = value; }
		}

		[Constructable]
		public RewardBlackDyeTub()
		{
			Hue = Hue = 0x0001;
			Redyable = false;
			LootType = LootType.Blessed;
		}

		public override void OnDoubleClick( Mobile from )
		{
			base.OnDoubleClick( from );
		}

		public RewardBlackDyeTub( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( Core.ML && m_IsRewardItem )
				list.Add( 1076217 ); // 1st Year Veteran Reward
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( (bool) m_IsRewardItem );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					m_IsRewardItem = reader.ReadBool();
					break;
				}
			}
		}
	}
}