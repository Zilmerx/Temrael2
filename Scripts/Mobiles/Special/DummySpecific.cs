using System;
using Server.Items;
using Server.Mobiles;
using Server.Spells;

namespace Server.Mobiles
{
	/// <summary>
	/// This is a test creature
	/// You can set its value in game
	/// It die after 5 minutes, so your test server stay clean
	/// Create a macro to help your creation "[add Dummy 1 15 7 -1 0.5 2"
	/// 
	/// A iTeam of negative will set a faction at random
	/// 
	/// Say Kill if you want them to die
	/// 
	/// </summary>

	public class DummyMace : Dummy
	{

		[Constructable]
		public DummyMace() : base(AIType.AI_Melee, FightMode.Closest, 15, 1, 0.2, 0.6)
		{
			// A Dummy Macer
			int iHue = 20 + Team * 40;
			int jHue = 25 + Team * 40;

			// Skills and Stats
			this.InitStats( 125, 125, 90 );
			this.Skills[SkillName.ArmeContondante].Base = 120;
			//this.Skills[SkillName.Anatomy].Base = 120;
			this.Skills[SkillName.Soins].Base = 120;
			this.Skills[SkillName.Tactiques].Base = 120;


			// Name
			this.Name = "Macer";

			// Equip
			WarHammer war = new WarHammer();
			war.Movable = true;
			war.Crafter = this;
			war.Quality = WeaponQuality.Regular;
			AddItem( war );

			Boots bts = new Boots();
			bts.Hue = iHue;
			AddItem( bts );

			ChainChest cht = new ChainChest();
			cht.Movable = false;
			cht.LootType = LootType.Blessed;
			cht.Crafter = this;
			cht.Quality = ArmorQuality.Regular;
			AddItem( cht );

			ChainLegs chl = new ChainLegs();
			chl.Movable = false;
			chl.LootType = LootType.Blessed;
			chl.Crafter = this;
			chl.Quality = ArmorQuality.Regular;
			AddItem( chl );

			PlateArms pla = new PlateArms();
			pla.Movable = false;
			pla.LootType = LootType.Blessed;
			pla.Crafter = this;
			pla.Quality = ArmorQuality.Regular;
			AddItem( pla );

			Bandage band = new Bandage( 50 );
			AddToBackpack( band );
		}

		public DummyMace( Serial serial ) : base( serial )
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

	public class DummyFence : Dummy
	{

		[Constructable]
		public DummyFence() : base(AIType.AI_Melee, FightMode.Closest, 15, 1, 0.2, 0.6)
		{
			// A Dummy Fencer
			int iHue = 20 + Team * 40;
			int jHue = 25 + Team * 40;

			// Skills and Stats
			this.InitStats( 125, 125, 90 );
			this.Skills[SkillName.ArmePerforante].Base = 120;
			//this.Skills[SkillName.Anatomy].Base = 120;
			this.Skills[SkillName.Soins].Base = 120;
			this.Skills[SkillName.Tactiques].Base = 120;

			// Name
			this.Name = "Fencer";

			// Equip
			Spear ssp = new Spear();
			ssp.Movable = true;
			ssp.Crafter = this;
			ssp.Quality = WeaponQuality.Regular;
			AddItem( ssp );

			Boots snd = new Boots();
			snd.Hue = iHue;
			snd.LootType = LootType.Blessed;
			AddItem( snd );

			ChainChest cht = new ChainChest();
			cht.Movable = false;
			cht.LootType = LootType.Blessed;
			cht.Crafter = this;
			cht.Quality = ArmorQuality.Regular;
			AddItem( cht );

			ChainLegs chl = new ChainLegs();
			chl.Movable = false;
			chl.LootType = LootType.Blessed;
			chl.Crafter = this;
			chl.Quality = ArmorQuality.Regular;
			AddItem( chl );

			PlateArms pla = new PlateArms();
			pla.Movable = false;
			pla.LootType = LootType.Blessed;
			pla.Crafter = this;
			pla.Quality = ArmorQuality.Regular;
			AddItem( pla );

			Bandage band = new Bandage( 50 );
			AddToBackpack( band );
		}

		public DummyFence( Serial serial ) : base( serial )
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

	public class DummySword : Dummy
	{

		[Constructable]
		public DummySword() : base(AIType.AI_Melee, FightMode.Closest, 15, 1, 0.2, 0.6)
		{
			// A Dummy Swordsman
			int iHue = 20 + Team * 40;
			int jHue = 25 + Team * 40;

			// Skills and Stats
			this.InitStats( 125, 125, 90 );
			this.Skills[SkillName.Epee].Base = 120;
			//this.Skills[SkillName.Anatomy].Base = 120;
			this.Skills[SkillName.Soins].Base = 120;
			this.Skills[SkillName.Tactiques].Base = 120;
			this.Skills[SkillName.Parer].Base = 120;


			// Name
			this.Name = "Swordsman";

			Boots bts = new Boots();
			bts.Hue = iHue;
			AddItem( bts );

			ChainChest cht = new ChainChest();
			cht.Movable = false;
			cht.LootType = LootType.Blessed;
			cht.Crafter = this;
			cht.Quality = ArmorQuality.Regular;
			AddItem( cht );

			ChainLegs chl = new ChainLegs();
			chl.Movable = false;
			chl.LootType = LootType.Blessed;
			chl.Crafter = this;
			chl.Quality = ArmorQuality.Regular;
			AddItem( chl );

			PlateArms pla = new PlateArms();
			pla.Movable = false;
			pla.LootType = LootType.Blessed;
			pla.Crafter = this;
			pla.Quality = ArmorQuality.Regular;
			AddItem( pla );

			Bandage band = new Bandage( 50 );
			AddToBackpack( band );
		}

		public DummySword( Serial serial ) : base( serial )
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

	public class DummyNox : Dummy
	{

		[Constructable]
		public DummyNox() : base(AIType.AI_Mage, FightMode.Closest, 15, 1, 0.2, 0.6)
		{

			// A Dummy Nox or Pure Mage
			int iHue = 20 + Team * 40;
			int jHue = 25 + Team * 40;

			// Skills and Stats
			this.InitStats( 90, 90, 125 );
			this.Skills[SkillName.ArtMagique].Base = 120;
			//this.Skills[SkillName.EvalInt].Base = 120;
			this.Skills[SkillName.Inscription].Base = 100;
			this.Skills[SkillName.Anatomie].Base = 120;
			this.Skills[SkillName.Concentration].Base = 120;
			this.Skills[SkillName.Empoisonnement].Base = 100;


			// Name
			this.Name = "Nox Mage";

			// Equip
			Spellbook book = new Spellbook();
			book.Movable = false;
			book.LootType = LootType.Blessed;
			book.Content =0xFFFFFFFFFFFFFFFF;
			AddItem( book );

			Kilt kilt = new Kilt();
			kilt.Hue = jHue;
			AddItem( kilt );

			Sandals snd = new Sandals();
			snd.Hue = iHue;
			snd.LootType = LootType.Blessed;
			AddItem( snd );

			SkullCap skc = new SkullCap();
			skc.Hue = iHue;
			AddItem( skc );

			// Spells
			AddSpellAttack( typeof(Spells.MagicArrowSpell) );
			AddSpellAttack( typeof(Spells.WeakenSpell) );
			AddSpellAttack( typeof(Spells.FireballSpell) );
			AddSpellDefense( typeof(Spells.WallOfStoneSpell) );
			AddSpellDefense( typeof(Spells.HealSpell) );
		}

		public DummyNox( Serial serial ) : base( serial )
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

	public class DummyStun : Dummy
	{

		[Constructable]
		public DummyStun() : base(AIType.AI_Mage, FightMode.Closest, 15, 1, 0.2, 0.6)
		{

			// A Dummy Stun Mage
			int iHue = 20 + Team * 40;
			int jHue = 25 + Team * 40;

			// Skills and Stats
			this.InitStats( 90, 90, 125 );
			this.Skills[SkillName.ArtMagique].Base = 100;
			//this.Skills[SkillName.EvalInt].Base = 120;
			//this.Skills[SkillName.Anatomy].Base = 80;
			this.Skills[SkillName.Anatomie].Base = 80;
			this.Skills[SkillName.Concentration].Base = 100;
			this.Skills[SkillName.Empoisonnement].Base = 100;


			// Name
			this.Name = "Stun Mage";

			// Equip
			Spellbook book = new Spellbook();
			book.Movable = false;
			book.LootType = LootType.Blessed;
			book.Content =0xFFFFFFFFFFFFFFFF;
			AddItem( book );

			LeatherArms lea = new LeatherArms();
			lea.Movable = false;
			lea.LootType = LootType.Blessed;
			lea.Crafter = this;
			lea.Quality = ArmorQuality.Regular;
			AddItem( lea );

			LeatherChest lec = new LeatherChest();
			lec.Movable = false;
			lec.LootType = LootType.Blessed;
			lec.Crafter = this;
			lec.Quality = ArmorQuality.Regular;
			AddItem( lec );

			LeatherGorget leg = new LeatherGorget();
			leg.Movable = false;
			leg.LootType = LootType.Blessed;
			leg.Crafter = this;
			leg.Quality = ArmorQuality.Regular;
			AddItem( leg );

			LeatherLegs lel = new LeatherLegs();
			lel.Movable = false;
			lel.LootType = LootType.Blessed;
			lel.Crafter = this;
			lel.Quality = ArmorQuality.Regular;
			AddItem( lel );

			Boots bts = new Boots();
			bts.Hue = iHue;
			AddItem( bts );

			Cap cap = new Cap();
			cap.Hue = iHue;
			AddItem( cap );

			// Spells
			AddSpellAttack( typeof(Spells.MagicArrowSpell) );
			AddSpellAttack( typeof(Spells.WeakenSpell) );
			AddSpellAttack( typeof(Spells.FireballSpell) );
			AddSpellDefense( typeof(Spells.WallOfStoneSpell) );
			AddSpellDefense( typeof(Spells.HealSpell) );
		}

		public DummyStun( Serial serial ) : base( serial )
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

	public class DummySuper : Dummy
	{

		[Constructable]
		public DummySuper() : base(AIType.AI_Mage, FightMode.Closest, 15, 1, 0.2, 0.6)
		{
			// A Dummy Super Mage
			int iHue = 20 + Team * 40;
			int jHue = 25 + Team * 40;

			// Skills and Stats
			this.InitStats( 125, 125, 125 );
			this.Skills[SkillName.ArtMagique].Base = 120;
			//this.Skills[SkillName.EvalInt].Base = 120;
			//this.Skills[SkillName.Anatomy].Base = 120;
			this.Skills[SkillName.Anatomie].Base = 120;
			this.Skills[SkillName.Concentration].Base = 120;
			this.Skills[SkillName.Empoisonnement].Base = 100;
			this.Skills[SkillName.Inscription].Base = 100;

			// Name
			this.Name = "Super Mage";

			// Equip
			Spellbook book = new Spellbook();
			book.Movable = false;
			book.LootType = LootType.Blessed;
			book.Content =0xFFFFFFFFFFFFFFFF;
			AddItem( book );

			LeatherArms lea = new LeatherArms();
			lea.Movable = false;
			lea.LootType = LootType.Blessed;
			lea.Crafter = this;
			lea.Quality = ArmorQuality.Regular;
			AddItem( lea );

			LeatherChest lec = new LeatherChest();
			lec.Movable = false;
			lec.LootType = LootType.Blessed;
			lec.Crafter = this;
			lec.Quality = ArmorQuality.Regular;
			AddItem( lec );

			LeatherGorget leg = new LeatherGorget();
			leg.Movable = false;
			leg.LootType = LootType.Blessed;
			leg.Crafter = this;
			leg.Quality = ArmorQuality.Regular;
			AddItem( leg );

			LeatherLegs lel = new LeatherLegs();
			lel.Movable = false;
			lel.LootType = LootType.Blessed;
			lel.Crafter = this;
			lel.Quality = ArmorQuality.Regular;
			AddItem( lel );

			Sandals snd = new Sandals();
			snd.Hue = iHue;
			snd.LootType = LootType.Blessed;
			AddItem( snd );

			JesterHat jhat = new JesterHat();
			jhat.Hue = iHue;
			AddItem( jhat );

			Doublet dblt = new Doublet();
			dblt.Hue = iHue;
			AddItem( dblt );

			// Spells
			AddSpellAttack( typeof(Spells.MagicArrowSpell) );
			AddSpellAttack( typeof(Spells.WeakenSpell) );
			AddSpellAttack( typeof(Spells.FireballSpell) );
			AddSpellDefense( typeof(Spells.WallOfStoneSpell) );
			AddSpellDefense( typeof(Spells.HealSpell) );
		}

		public DummySuper( Serial serial ) : base( serial )
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

	public class DummyHealer : Dummy
	{

		[Constructable]
		public DummyHealer() : base(AIType.AI_Healer, FightMode.Closest, 15, 1, 0.2, 0.6)
		{
			// A Dummy Healer Mage
			int iHue = 20 + Team * 40;
			int jHue = 25 + Team * 40;

			// Skills and Stats
			this.InitStats( 125, 125, 125 );
			this.Skills[SkillName.ArtMagique].Base = 120;
			//this.Skills[SkillName.EvalInt].Base = 120;
			//this.Skills[SkillName.Anatomy].Base = 120;
			this.Skills[SkillName.Anatomie].Base = 120;
			this.Skills[SkillName.Concentration].Base = 120;
			this.Skills[SkillName.Soins].Base = 100;

			// Name
			this.Name = "Healer";

			// Equip
			Spellbook book = new Spellbook();
			book.Movable = false;
			book.LootType = LootType.Blessed;
			book.Content =0xFFFFFFFFFFFFFFFF;
			AddItem( book );

			LeatherArms lea = new LeatherArms();
			lea.Movable = false;
			lea.LootType = LootType.Blessed;
			lea.Crafter = this;
			lea.Quality = ArmorQuality.Regular;
			AddItem( lea );

			LeatherChest lec = new LeatherChest();
			lec.Movable = false;
			lec.LootType = LootType.Blessed;
			lec.Crafter = this;
			lec.Quality = ArmorQuality.Regular;
			AddItem( lec );

			LeatherGorget leg = new LeatherGorget();
			leg.Movable = false;
			leg.LootType = LootType.Blessed;
			leg.Crafter = this;
			leg.Quality = ArmorQuality.Regular;
			AddItem( leg );

			LeatherLegs lel = new LeatherLegs();
			lel.Movable = false;
			lel.LootType = LootType.Blessed;
			lel.Crafter = this;
			lel.Quality = ArmorQuality.Regular;
			AddItem( lel );

			Sandals snd = new Sandals();
			snd.Hue = iHue;
			snd.LootType = LootType.Blessed;
			AddItem( snd );

			Cap cap = new Cap();
			cap.Hue = iHue;
			AddItem( cap );

			Robe robe = new Robe();
			robe.Hue = iHue;
			AddItem( robe );

		}

		public DummyHealer( Serial serial ) : base( serial )
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

	public class DummyAssassin : Dummy
	{

		[Constructable]
		public DummyAssassin() : base(AIType.AI_Melee, FightMode.Closest, 15, 1, 0.2, 0.6)
		{
			// A Dummy Hybrid Assassin
			int iHue = 20 + Team * 40;
			int jHue = 25 + Team * 40;

			// Skills and Stats
			this.InitStats( 105, 105, 105 );
			this.Skills[SkillName.ArtMagique].Base = 120;
			//this.Skills[SkillName.EvalInt].Base = 120;
			this.Skills[SkillName.Epee].Base = 120;
			this.Skills[SkillName.Tactiques].Base = 120;
			this.Skills[SkillName.Concentration].Base = 120;
			this.Skills[SkillName.Empoisonnement].Base = 100;

			// Name
			this.Name = "Hybrid Assassin";

			// Equip
			Spellbook book = new Spellbook();
			book.Movable = false;
			book.LootType = LootType.Blessed;
			book.Content =0xFFFFFFFFFFFFFFFF;
			AddToBackpack( book );

			LeatherArms lea = new LeatherArms();
			lea.Movable = false;
			lea.LootType = LootType.Blessed;
			lea.Crafter = this;
			lea.Quality = ArmorQuality.Regular;
			AddItem( lea );

			LeatherChest lec = new LeatherChest();
			lec.Movable = false;
			lec.LootType = LootType.Blessed;
			lec.Crafter = this;
			lec.Quality = ArmorQuality.Regular;
			AddItem( lec );

			LeatherGorget leg = new LeatherGorget();
			leg.Movable = false;
			leg.LootType = LootType.Blessed;
			leg.Crafter = this;
			leg.Quality = ArmorQuality.Regular;
			AddItem( leg );

			LeatherLegs lel = new LeatherLegs();
			lel.Movable = false;
			lel.LootType = LootType.Blessed;
			lel.Crafter = this;
			lel.Quality = ArmorQuality.Regular;
			AddItem( lel );

			Sandals snd = new Sandals();
			snd.Hue = iHue;
			snd.LootType = LootType.Blessed;
			AddItem( snd );

			Cap cap = new Cap();
			cap.Hue = iHue;
			AddItem( cap );

			Robe robe = new Robe();
			robe.Hue = iHue;
			AddItem( robe );

			DeadlyPoisonPotion pota = new DeadlyPoisonPotion();
			pota.LootType = LootType.Blessed;
			AddToBackpack( pota );

			DeadlyPoisonPotion potb = new DeadlyPoisonPotion();
			potb.LootType = LootType.Blessed;
			AddToBackpack( potb );

			DeadlyPoisonPotion potc = new DeadlyPoisonPotion();
			potc.LootType = LootType.Blessed;
			AddToBackpack( potc );

			DeadlyPoisonPotion potd = new DeadlyPoisonPotion();
			potd.LootType = LootType.Blessed;
			AddToBackpack( potd );

			Bandage band = new Bandage( 50 );
			AddToBackpack( band );

		}

		public DummyAssassin( Serial serial ) : base( serial )
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

	public class DummyTheif : Dummy
	{

		[Constructable]
		public DummyTheif() : base(AIType.AI_Thief, FightMode.Closest, 15, 1, 0.2, 0.6)
		{
			// A Dummy Hybrid Theif
			int iHue = 20 + Team * 40;
			int jHue = 25 + Team * 40;

			// Skills and Stats
			this.InitStats( 105, 105, 105 );
			this.Skills[SkillName.Soins].Base = 120;
			//this.Skills[SkillName.Anatomy].Base = 120;
			this.Skills[SkillName.Vol].Base = 120;
			//this.Skills[SkillName.ArmsLore].Base = 100;
			this.Skills[SkillName.Concentration].Base = 120;
			this.Skills[SkillName.Anatomie].Base = 120;

			// Name
			this.Name = "Hybrid Theif";

			// Equip
			Spellbook book = new Spellbook();
			book.Movable = false;
			book.LootType = LootType.Blessed;
			book.Content =0xFFFFFFFFFFFFFFFF;
			AddItem( book );

			LeatherArms lea = new LeatherArms();
			lea.Movable = false;
			lea.LootType = LootType.Blessed;
			lea.Crafter = this;
			lea.Quality = ArmorQuality.Regular;
			AddItem( lea );

			LeatherChest lec = new LeatherChest();
			lec.Movable = false;
			lec.LootType = LootType.Blessed;
			lec.Crafter = this;
			lec.Quality = ArmorQuality.Regular;
			AddItem( lec );

			LeatherGorget leg = new LeatherGorget();
			leg.Movable = false;
			leg.LootType = LootType.Blessed;
			leg.Crafter = this;
			leg.Quality = ArmorQuality.Regular;
			AddItem( leg );

			LeatherLegs lel = new LeatherLegs();
			lel.Movable = false;
			lel.LootType = LootType.Blessed;
			lel.Crafter = this;
			lel.Quality = ArmorQuality.Regular;
			AddItem( lel );

			Sandals snd = new Sandals();
			snd.Hue = iHue;
			snd.LootType = LootType.Blessed;
			AddItem( snd );

			Cap cap = new Cap();
			cap.Hue = iHue;
			AddItem( cap );

			Robe robe = new Robe();
			robe.Hue = iHue;
			AddItem( robe );

			Bandage band = new Bandage( 50 );
			AddToBackpack( band );
		}

		public DummyTheif( Serial serial ) : base( serial )
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