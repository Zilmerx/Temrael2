﻿using Server.Engines.Races;
using Server.Items;
using System.Collections.Generic;

namespace Server.Mobiles.Vendeurs
{
    public class Pecheur2 : BaseVendor
    {
        [Constructable]
        public Pecheur2()
            : base("Pêcheur")
        {
            Name = "Damien";
        }


        public Pecheur2(Serial serial)
            : base(serial)
        {
        }

        public override void InitBody()
        {
            SpeechHue = Utility.RandomDyedHue();
            NameHue = 0x35;
            Body = 0x190; //male

            SetRace(new Capiceen(1002));
            HairItemID = 10298;
            HairHue = 1318;
            FacialHairItemID = 0;
            FacialHairHue = 0;
        }

        public override void InitOutfit()
        {
            AddItem(new Chandail(1830));
            AddItem(new ShortPants(1451));
            AddItem(new Shoes(1894));
            AddItem(new FishingPole(0));
        }

        private List<SBInfo> m_SBInfos = new List<SBInfo>();
        protected override List<SBInfo> SBInfos { get { return m_SBInfos; } }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBPoisson2());
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

    }

    public class SBPoisson2 : SBInfo
    {
        private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private IShopSellInfo m_SellInfo = new InternalSellInfo();

        public SBPoisson2()
        {
        }

        public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
        public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

        public class InternalBuyInfo : List<GenericBuyInfo>
        {
            public InternalBuyInfo()
            {
                Add(new GenericBuyInfo(typeof(Fish), 20, 0x9CE, 0)); // 9
                Add(new GenericBuyInfo(typeof(FishSteak), 20, 0x97B, 0)); // 3
                Add(new GenericBuyInfo(typeof(AnchoieFish), 20, 0xDD6, 0)); // 9
                Add(new GenericBuyInfo(typeof(HuitreFish), 20, 0x104F, 0)); // 9
                Add(new GenericBuyInfo(typeof(ColierCoquillages), 20, 0x3171, 0));
                Add(new GenericBuyInfo(typeof(FishingPole), 20, 0xDC0, 0)); // 9
                Add(new GenericBuyInfo(typeof(FishingNet), 20, 0xDCA, 0)); // 21
                Add(new GenericBuyInfo(typeof(Coquillage), 20, 0xFC7, 0)); // 6
                //Add(new GenericBuyInfo(typeof(Algue), 20, 0x2B30, 0)); // 9 à créer
                //Add(new GenericBuyInfo(typeof(Poisson), 20, 0x2B38, 0)); // 15 à créer
                //Add(new GenericBuyInfo(typeof(CoquillageGeant), 20, 0x3B12, 0)); // 27 à créer
                //Add(new GenericBuyInfo(typeof(CounchShell), 20, 0xFC4, 0)); // 21 à créer
                //Add(new GenericBuyInfo(typeof(EtoileDeMer), 20, 0x1C0E, 0)); // 21 à créer
            }
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
            }
        }
    }
}