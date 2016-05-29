﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Mobiles;

namespace Server.Misc.PVP
{
    class Teams : PVPTeamArrangement
    {
        private List<int> hueList = new List<int>
        {
            0x3, // Team 1
            0x21,// Team 2
            0x35,// Team 3
            0x44 // Team 4
        };



        public Teams(PVPEvent pvpevent)
            : base(pvpevent)
        {
        }

        public override int NbMaxEquipes
        {
            get { return 4; }
        }

        protected override bool InscrireDef(ScriptMobile mob, int teamNumber)
        {
            if (m_pvpevent.map != null)
            {
                if (teamNumber >= 0 && teamNumber < m_pvpevent.map.NbTeamSpawnpoints)
                {
                    if (!m_teams[teamNumber].Contains(mob))
                    {
                        try
                        {
                            m_teams[teamNumber].Add(mob);

                            return true;
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }

            return false;
        }

        protected override void SpawnDef(ScriptMobile m)
        {
            int cpt = 0;
            foreach (PVPTeam team in m_teams)
            {
                if (team.Contains(m))
                {
                    m.Location = m_pvpevent.map.SpawnPoints[cpt];
                    m.Map = m_pvpevent.map.Map;

                    PVPDossard.ForcePut(m, hueList[cpt]);

                    return;
                }
                ++cpt;
            }
        }

    }
}
