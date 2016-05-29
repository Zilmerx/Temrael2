﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items
{
    public abstract class BaseGem : Item, IExtractable
    {
        public string getName
        {
            get { return Name; }
        }
        public int getHue
        {
            get { return m_Couleur; }
        }
        public double getSkillReq
        {
            get { return m_SkillReq; }
        }

        public abstract double m_SkillReq
        {
            get;
        }

        public abstract int m_Couleur
        {
            get;
        }

        public override double DefaultWeight
        {
            get { return 0.1; }
        }

        public BaseGem(int itemID)
            : base(itemID)
        { }

        public BaseGem(Serial serial)
            : base(serial)
        { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
