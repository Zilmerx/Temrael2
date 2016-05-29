using System;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Targeting;
using Server.Items;
using Server.Network;

using Server.Spells;
using Server.Commands;
using Server.Mobiles.GuardsVersion2;


namespace Server.SkillHandlers
{
	public class Stealing
	{
		public static void Initialize()
		{
			SkillInfo.Table[(int)SkillName.Vol].Callback = new SkillUseCallback( OnUse );
		}

		public static bool IsInnocentTo( Mobile from, Mobile to )
		{
			return ( Notoriety.Compute( from, (Mobile)to ) == Notoriety.Innocent );
		}

		private class StealingTarget : Target
		{
			private Mobile m_Thief;

			public StealingTarget( Mobile thief ) : base ( 1, false, TargetFlags.None )
			{
				m_Thief = thief;

				AllowNonlocal = true;
			}

			private Item TryStealItem( Item toSteal, ref bool caught )
			{
				Item stolen = null;

				object root = toSteal.RootParent;

				StealableArtifactsSpawner.StealableInstance si = null;
				if ( toSteal.Parent == null || !toSteal.Movable )
					si = StealableArtifactsSpawner.GetStealableInstance( toSteal );

                if (m_Thief is ScriptMobile)
                {
                    ScriptMobile mob = (ScriptMobile)m_Thief;
                    if (mob.PVPInfo != null)
                    {
                        if (!mob.PVPInfo.CurrentEvent.mode.AllowLoot())
                        {
                            return stolen;
                        }
                    }
                }

				if ( !IsEmptyHanded( m_Thief ) )
				{
					m_Thief.SendLocalizedMessage( 1005584 ); // Both hands must be free to steal.
				}
				/*else if ( root is Mobile && ((Mobile)root).Player && IsInnocentTo( m_Thief, (Mobile)root ) && !IsInGuild( m_Thief ) )
				{
					m_Thief.SendLocalizedMessage( 1005596 ); // You must be in the thieves guild to steal from other players.
				}
				else if ( SuspendOnMurder && root is Mobile && ((Mobile)root).Player && IsInGuild( m_Thief ) && m_Thief.Kills > 0 )
				{
					m_Thief.SendLocalizedMessage( 502706 ); // You are currently suspended from the thieves guild.
				}*/
				else if ( root is BaseVendor && ((BaseVendor)root).IsInvulnerable )
				{
					m_Thief.SendLocalizedMessage( 1005598 ); // You can't steal from shopkeepers.
				}
				else if ( root is PlayerVendor )
				{
					m_Thief.SendLocalizedMessage( 502709 ); // You can't steal from vendors.
				}
				else if ( !m_Thief.CanSee( toSteal ) )
				{
					m_Thief.SendLocalizedMessage( 500237 ); // Target can not be seen.
				}
				else if ( m_Thief.Backpack == null || !m_Thief.Backpack.CheckHold( m_Thief, toSteal, false, true ) )
				{
					m_Thief.SendLocalizedMessage( 1048147 ); // Your backpack can't hold anything else.
				}
				else if ( si == null && ( toSteal.Parent == null || !toSteal.Movable ) )
				{
					m_Thief.SendLocalizedMessage( 502710 ); // You can't steal that!
				}
				else if (toSteal.CheckBlessed( root ))
				{
					m_Thief.SendLocalizedMessage( 502710 ); // You can't steal that!
				}
				else if ( Core.AOS && si == null && toSteal is Container )
				{
					m_Thief.SendLocalizedMessage( 502710 ); // You can't steal that!
				}
				else if ( !m_Thief.InRange( toSteal.GetWorldLocation(), 1 ) )
				{
					m_Thief.SendLocalizedMessage( 502703 ); // You must be standing next to an item to steal it.
				}
				else if ( si != null && m_Thief.Skills[SkillName.Vol].Value < 100.0 )
				{
					m_Thief.SendLocalizedMessage( 1060025, "", 0x66D ); // You're not skilled enough to attempt the theft of this item.
				}
				else if ( toSteal.Parent is Mobile )
				{
					m_Thief.SendLocalizedMessage( 1005585 ); // You cannot steal items which are equiped.
				}
				else if ( root == m_Thief )
				{
					m_Thief.SendLocalizedMessage( 502704 ); // You catch yourself red-handed.
				}
				else if ( root is Mobile && ((Mobile)root).AccessLevel > AccessLevel.Player )
				{
					m_Thief.SendLocalizedMessage( 502710 ); // You can't steal that!
				}
				else if ( root is Mobile && !m_Thief.CanBeHarmful( (Mobile)root ) )
				{
				}
				else if ( root is Corpse )
				{
					m_Thief.SendLocalizedMessage( 502710 ); // You can't steal that!
				}
				else
				{
					double w = toSteal.Weight + toSteal.TotalWeight;

                    double max = 6;

					if ( w > max )
					{
						m_Thief.SendMessage( "That is too heavy to steal." );
					}
					else
					{
						if ( toSteal.Stackable && toSteal.Amount > 1 )
						{
							int maxAmount = (int)((m_Thief.Skills[SkillName.Vol].Value / 10.0) / toSteal.Weight);

							if ( maxAmount < 1 )
								maxAmount = 1;
							else if ( maxAmount > toSteal.Amount )
								maxAmount = toSteal.Amount;

							int amount = Utility.RandomMinMax( 1, maxAmount );

							if ( amount >= toSteal.Amount )
							{
								int pileWeight = (int)Math.Ceiling( toSteal.Weight * toSteal.Amount );
								pileWeight *= 10;

								if ( m_Thief.CheckTargetSkill( SkillName.Vol, toSteal, pileWeight - 22.5, pileWeight + 27.5 ) )
									stolen = toSteal;
							}
							else
							{
								int pileWeight = (int)Math.Ceiling( toSteal.Weight * amount );
								pileWeight *= 10;

								if ( m_Thief.CheckTargetSkill( SkillName.Vol, toSteal, pileWeight - 22.5, pileWeight + 27.5 ) )
								{
									stolen = Mobile.LiftItemDupe( toSteal, toSteal.Amount - amount );

									if ( stolen == null )
										stolen = toSteal;
								}
							}
						}
						else
						{
							int iw = (int)Math.Ceiling( w );
							iw *= 10;

							if ( m_Thief.CheckTargetSkill( SkillName.Vol, toSteal, iw - 22.5, iw + 27.5 ) )
								stolen = toSteal;
						}

						if ( stolen != null )
						{
                            GuardVer2.CheckOnSteal(m_Thief);
							m_Thief.SendLocalizedMessage( 502724 ); // You succesfully steal the item.

							if ( si != null )
							{
								toSteal.Movable = true;
								si.Item = null;
							}
                            caught = false;
						}
						else
						{
							m_Thief.SendLocalizedMessage( 502723 ); // You fail to steal the item.
                            caught = true;
						}

                        if (m_Thief is ScriptMobile)
                        {
                            ScriptMobile sm = (ScriptMobile)m_Thief;

                            IPooledEnumerable<Mobile> eable = m_Thief.GetMobilesInRange(3);
                            foreach (Mobile mob in eable)
                            {
                                sm.Detection.FaireJet(mob, 0.1);
                            }
                        }

						//caught = ( m_Thief.Skills[SkillName.Vol].Value < Utility.Random( 150 ) );
					}
				}

				return stolen;
			}

			protected override void OnTarget( Mobile from, object target )
			{
				//from.RevealingAction();

				Item stolen = null;
				object root = null;
				bool caught = false;

				if ( target is Item )
				{
					root = ((Item)target).RootParent;
					stolen = TryStealItem( (Item)target, ref caught );
				} 
				else if ( target is Mobile )
				{
					Container pack = ((Mobile)target).Backpack;

					if ( pack != null && pack.Items.Count > 0 )
					{
						int randomIndex = Utility.Random( pack.Items.Count );

						root = target;
						stolen = TryStealItem( pack.Items[randomIndex], ref caught );
					}
				} 
				else 
				{
					m_Thief.SendLocalizedMessage( 502710 ); // You can't steal that!
				}

				if ( stolen != null )
				{
					from.AddToBackpack( stolen );

					StolenItem.Add( stolen, m_Thief, root as Mobile );
				}

				if ( caught )
				{
                    if (root is Mobile)
					{
						Mobile mobRoot = (Mobile)root;

                        string message = String.Format("Vous appercevez un personnage tenter d'en voler un autre.");

                        from.RevealingAction();

                        from.NonlocalOverheadMessage(MessageType.Emote, 0, true, message);

						/*foreach ( NetState ns in m_Thief.GetClientsInRange( 8 ) )
						{
							if ( ns.Mobile != m_Thief )
								ns.Mobile.SendMessage( message );
						}*/
					}
				}

                CommandLogging.WriteLine(from, "{4} {2} � voler {0} appartenant � {1} en �tant {3}", 
                    target, root, stolen != null ? "r�ussit" : "�choue", from.Hidden ? "cach�" : "� d�couvert", from);
			}
		}

		public static bool IsEmptyHanded( Mobile from )
		{
			if ( from.FindItemOnLayer( Layer.OneHanded ) != null )
				return false;

			if ( from.FindItemOnLayer( Layer.TwoHanded ) != null )
				return false;

			return true;
		}

		public static TimeSpan OnUse( Mobile m )
		{
			if ( !IsEmptyHanded( m ) )
			{
				m.SendLocalizedMessage( 1005584 ); // Both hands must be free to steal.
			}
			else
			{
				m.Target = new Stealing.StealingTarget( m );
				//m.RevealingAction();

				m.SendLocalizedMessage( 502698 ); // Which item do you want to steal?
			}

			return TimeSpan.FromSeconds( 10.0 );
		}
	}

	public class StolenItem
	{
		public static readonly TimeSpan StealTime = TimeSpan.FromMinutes( 2.0 );

		private Item m_Stolen;
		private Mobile m_Thief;
		private Mobile m_Victim;
		private DateTime m_Expires;

		public Item Stolen{ get{ return m_Stolen; } }
		public Mobile Thief{ get{ return m_Thief; } }
		public Mobile Victim{ get{ return m_Victim; } }
		public DateTime Expires{ get{ return m_Expires; } }

		public bool IsExpired{ get{ return ( DateTime.Now >= m_Expires ); } }

		public StolenItem( Item stolen, Mobile thief, Mobile victim )
		{
			m_Stolen = stolen;
			m_Thief = thief;
			m_Victim = victim;

			m_Expires = DateTime.Now + StealTime;
		}

		private static Queue m_Queue = new Queue();

		public static void Add( Item item, Mobile thief, Mobile victim )
		{
			Clean();

			m_Queue.Enqueue( new StolenItem( item, thief, victim ) );
		}

		public static bool IsStolen( Item item )
		{
			Mobile victim = null;

			return IsStolen( item, ref victim );
		}

		public static bool IsStolen( Item item, ref Mobile victim )
		{
			Clean();

			foreach ( StolenItem si in m_Queue )
			{
				if ( si.m_Stolen == item && !si.IsExpired )
				{
					victim = si.m_Victim;
					return true;
				}
			}

			return false;
		}

		public static void ReturnOnDeath( Mobile killed, Container corpse )
		{
			Clean();

			foreach ( StolenItem si in m_Queue )
			{
				if ( si.m_Stolen.RootParent == corpse && si.m_Victim != null && !si.IsExpired )
				{
					if ( si.m_Victim.AddToBackpack( si.m_Stolen ) )
						si.m_Victim.SendLocalizedMessage( 1010464 ); // the item that was stolen is returned to you.
					else
						si.m_Victim.SendLocalizedMessage( 1010463 ); // the item that was stolen from you falls to the ground.

					si.m_Expires = DateTime.Now; // such a hack
				}
			}
		}

		public static void Clean()
		{
			while ( m_Queue.Count > 0 )
			{
				StolenItem si = (StolenItem) m_Queue.Peek();

				if ( si.IsExpired )
					m_Queue.Dequeue();
				else
					break;
			}
		}
	}
}