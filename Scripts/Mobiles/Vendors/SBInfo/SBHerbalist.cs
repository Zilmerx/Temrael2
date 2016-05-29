using System; 
using System.Collections.Generic; 
using Server.Items; 

namespace Server.Mobiles 
{ 
	public class SBHerbalist : SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBHerbalist() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 
				Add( new GenericBuyInfo( typeof( Ginseng ), 3, 20, 0xF85, 0 ) ); 
				Add( new GenericBuyInfo( typeof( Garlic ), 3, 20, 0xF84, 0 ) ); 
				Add( new GenericBuyInfo( typeof( MandrakeRoot ), 3, 20, 0xF86, 0 ) ); 
				Add( new GenericBuyInfo( typeof( Nightshade ), 3, 20, 0xF88, 0 ) ); 
				Add( new GenericBuyInfo( typeof( Bloodmoss ), 5, 20, 0xF7B, 0 ) ); 
				//Add( new GenericBuyInfo( typeof( MortarPestle ), 20, 20, 0xE9B, 0 ) );
				Add( new GenericBuyInfo( typeof( Bottle ), 5, 20, 0xF0E, 0 ) ); 
 
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				Add( typeof( Bloodmoss ), 1 ); 
				Add( typeof( MandrakeRoot ), 1 ); 
				Add( typeof( Garlic ), 1 ); 
				Add( typeof( Ginseng ), 1 ); 
				Add( typeof( Nightshade ), 1 ); 
				Add( typeof( Bottle ), 1 ); 
				//Add( typeof( MortarPestle ), 1 ); 
			} 
		} 
	} 
}