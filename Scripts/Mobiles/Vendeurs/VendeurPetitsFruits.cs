﻿using Server.Engines.Races;
using Server.Items;
using System.Collections.Generic;

namespace Server.Mobiles.Vendeurs
{
    public class VendeurPetitsFruits : BaseVendor
    {
        [Constructable]
        public VendeurPetitsFruits()
            : base("Vendeur de petits fruits")
        {
            Name = "Armando";
        }


        public VendeurPetitsFruits(Serial serial)
            : base(serial)
        {
        }

        public override void InitBody()
        {
            SpeechHue = Utility.RandomDyedHue();
            NameHue = 0x35;
            Body = 0x190; //male

            SetRace(new Nomade(1142));
            HairItemID = 10216;
            HairHue = 1109;
            FacialHairItemID = 10315;
            FacialHairHue = 1109;
        }

        public override void InitOutfit()
        {
            AddItem(new ChemiseAmple());
            AddItem(new GoldEarrings());
            AddItem(new LeatherLegs());
            AddItem(new BottesVoyage(2044));
            AddItem(new CapeDecore(2044));
            AddItem(new FourreauDague());
        }

		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBPetitsFruits() );
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

    public class SBPetitsFruits : SBInfo
    {
        private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private IShopSellInfo m_SellInfo = new InternalSellInfo();

        public SBPetitsFruits()
        {
        }

        public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
        public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

        public class InternalBuyInfo : List<GenericBuyInfo>
        {
            public InternalBuyInfo()
            {
                Add(new GenericBuyInfo(typeof(Banana), 20, 0x171f, 0)); // 3
                Add(new GenericBuyInfo(typeof(Apple), 20, 0x9D0, 0)); // 3
                Add(new GenericBuyInfo(typeof(Grapes), 20, 0x9D1, 0)); // 3
                Add(new GenericBuyInfo(typeof(Peach), 20, 0x9D2, 0)); // 3
                Add(new GenericBuyInfo(typeof(Pear), 20, 0x994, 0)); // 3
                Add(new GenericBuyInfo(typeof(Squash), 20, 0xc72, 0)); // 3
                Add(new GenericBuyInfo(typeof(Pumpkin), 20, 0xc64, 0)); // 3
                Add(new GenericBuyInfo(typeof(Carrot), 20, 0xc78, 0)); // 3
                Add(new GenericBuyInfo(typeof(Onion), 20, 0xc62, 0)); // 3
                Add(new GenericBuyInfo(typeof(Lettuce), 20, 0xc70, 0)); // 3
                Add(new GenericBuyInfo(typeof(Cabbage), 20, 0xc7b, 0)); // 3
                Add(new GenericBuyInfo(typeof(Watermelon), 20, 0xc5c, 0)); // 3
            }
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
                // Add(typeof(Banana));
            }
        }
    }
}
