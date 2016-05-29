using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public class JewelcrafterTool : BaseTool
	{
        public override int GoldValue { get { return 15; } }

        public override CraftSystem CraftSystem { get { return DefJewelcrafting.CraftSystem; } }

		[Constructable]
		public JewelcrafterTool()
			: base(0x1EBC)
		{
            Weight = 2.0;
            Name = "Outil d'orf�vrerie (Bijoux)";
		}

		[Constructable]
		public JewelcrafterTool(int uses)
			: base(uses, 0x1EBC)
		{
            Weight = 2.0;
		}

        public JewelcrafterTool(Serial serial)
			: base(serial)
		{
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
}