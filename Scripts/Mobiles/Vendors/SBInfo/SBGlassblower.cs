using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBGlassblower : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBGlassblower()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				/*Add( new GenericBuyInfo( typeof( RefreshPotion ), 15, 10, 0xF0B, 0 ) );
				Add( new GenericBuyInfo( typeof( AgilityPotion ), 15, 10, 0xF08, 0 ) );
				Add( new GenericBuyInfo( typeof( NightSightPotion ), 15, 10, 0xF06, 0 ) );
				Add( new GenericBuyInfo( typeof( LesserHealPotion ), 15, 10, 0xF0C, 0 ) );
				Add( new GenericBuyInfo( typeof( StrengthPotion ), 15, 10, 0xF09, 0 ) );
				Add( new GenericBuyInfo( typeof( LesserPoisonPotion ), 15, 10, 0xF0A, 0 ) );
 				Add( new GenericBuyInfo( typeof( LesserCurePotion ), 15, 10, 0xF07, 0 ) );
				Add( new GenericBuyInfo( typeof( LesserExplosionPotion ), 21, 10, 0xF0D, 0 ) );
                */
				//Add( new GenericBuyInfo( typeof( MortarPestle ), 20, 10, 0xE9B, 0 ) );

				Add( new GenericBuyInfo( typeof( BlackPearl ), 5, 20, 0xF7A, 0 ) );
				Add( new GenericBuyInfo( typeof( Bloodmoss ), 5, 20, 0xF7B, 0 ) );
				Add( new GenericBuyInfo( typeof( Garlic ), 3, 20, 0xF84, 0 ) );
				Add( new GenericBuyInfo( typeof( Ginseng ), 3, 20, 0xF85, 0 ) );
				Add( new GenericBuyInfo( typeof( MandrakeRoot ), 3, 20, 0xF86, 0 ) );
				Add( new GenericBuyInfo( typeof( Nightshade ), 3, 20, 0xF88, 0 ) );
				Add( new GenericBuyInfo( typeof( SpidersSilk ), 3, 20, 0xF8D, 0 ) );
				Add( new GenericBuyInfo( typeof( SulfurousAsh ), 3, 20, 0xF8C, 0 ) );

				Add( new GenericBuyInfo( typeof( Bottle ), 5, 100, 0xF0E, 0 ) ); 

				Add( new GenericBuyInfo( typeof( HeatingStand ), 2, 100, 0x1849, 0 ) ); 

				Add( new GenericBuyInfo( "Crafting Glass With Glassblowing", typeof( GlassblowingBook ), 10637, 30, 0xFF4, 0 ) );
				Add( new GenericBuyInfo( "Finding Glass-Quality Sand", typeof( SandMiningBook ), 10637, 30, 0xFF4, 0 ) );
				Add( new GenericBuyInfo( "1044608", typeof( Blowpipe ), 21, 100, 0xE8A, 0x3B9 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( BlackPearl ), 1 ); 
				Add( typeof( Bloodmoss ), 1 ); 
				Add( typeof( MandrakeRoot ), 1 ); 
				Add( typeof( Garlic ), 1 ); 
				Add( typeof( Ginseng ), 1 ); 
				Add( typeof( Nightshade ), 1 ); 
				Add( typeof( SpidersSilk ), 1 ); 
				Add( typeof( SulfurousAsh ), 1 ); 
				Add( typeof( Bottle ), 1 );
				//Add( typeof( MortarPestle ), 1 );

				Add( typeof( NightSightPotion ), 2 );
				Add( typeof( AgilityPotion ), 2 );
				Add( typeof( StrengthPotion ), 2 );
				Add( typeof( RefreshPotion ), 2 );
				Add( typeof( LesserCurePotion ), 2 );
				Add( typeof( LesserHealPotion ), 2 );
				Add( typeof( LesserPoisonPotion ), 2 );
				Add( typeof( LesserExplosionPotion ), 2 );

				/*Add( typeof( GlassblowingBook ), 5000 );
				Add( typeof( SandMiningBook ), 5000 );*/
				Add( typeof( Blowpipe ), 1 );
			}
		}
	}
}