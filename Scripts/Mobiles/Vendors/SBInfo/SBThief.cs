using System;
using System.Collections.Generic;
using Server.Items;
using Server.Engines.Identities;

namespace Server.Mobiles
{
	public class SBThief : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBThief()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( Backpack ), 15, 20, 0x9B2, 0 ) );
				Add( new GenericBuyInfo( typeof( Pouch ), 6, 20, 0xE79, 0 ) );
				Add( new GenericBuyInfo( typeof( Torch ), 8, 20, 0xF6B, 0 ) );
				Add( new GenericBuyInfo( typeof( Lantern ), 2, 20, 0xA25, 0 ) );
				//Add( new GenericBuyInfo( typeof( OilFlask ), 8, 20, 0x####, 0 ) );
				Add( new GenericBuyInfo( typeof( Lockpick ), 12, 20, 0x14FC, 0 ) );
				Add( new GenericBuyInfo( typeof( WoodenBox ), 14, 20, 0x9AA, 0 ) );
				Add( new GenericBuyInfo( typeof( Key ), 2, 20, 0x100E, 0 ) );
				Add( new GenericBuyInfo( typeof( HairDye ), 37, 20, 0xEFF, 0 ) );
                Add(new GenericBuyInfo(typeof(DeguisementKit), 50, 20, 0x1EBA, 0));
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( Backpack ), 1 );
				Add( typeof( Pouch ), 1 );
				Add( typeof( Torch ), 1 );
				Add( typeof( Lantern ), 1 );
				//Add( typeof( OilFlask ), 4 );
				Add( typeof( Lockpick ), 1 );
				Add( typeof( WoodenBox ), 1 );
				Add( typeof( HairDye ), 1 );
			}
		}
	}
}