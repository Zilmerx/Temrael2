﻿using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Items;
using Server.Network;
using System.Reflection;
using Server.HuePickers;
using System.Collections.Generic;

namespace Server.Gumps.Fiche
{
    public class FicheCaracteristiqueGump : BaseFicheGump
    {
        public FicheCaracteristiqueGump(PlayerMobile from)
            : base(from, "Caractéristiques", 560, 622, 3)
        {
            int x = XBase;
            int y = YBase;
            int line = 0;
            int scale = 25;

            int space = 80;

            int lineStart = line;

            int StatTotal = from.RawStr + from.RawDex + from.RawInt;
            int attente = from.StatCap - StatTotal;

            AddSection(x, y + line * scale, 539, 60, "Caractéristiques", "Les caractéristiques influencent les attributs (vitalité, stamina & mana) ainsi que le système de combat et de magie. Un bonus est appliqué lorsqu'une caractéristique atteint le chiffre de 100. Vous regagnez les points indisponibles à chaque niveau.", new string[] { "<basefont color=#5A4A31>Disponible: | Indisponible: " + attente.ToString() + "<basefont>" });
            line += 6;

            AddButton(x, (y + line * scale) + 20, 1436, 1436, 3, GumpButtonType.Reply, 0);
            AddTooltip(3001037);
            AddSection(x + 110, y + line * scale, 426, 60, "Force", "Augmente le poids maximum pouvant être porté, la regénération de la vie, les dégâts physiques et le maximum de points de vitalité. Bonus: +25 Vitalité, +25 Stones max.", new string[] { "<basefont color=#5A4A31>      Force: " + from.RawStr + "<basefont>" });

            if (Statistiques.CanRaise(from, StatType.Str))
                AddButton(x + 130, 105 + y + line * scale, 9770, 9770, 13, GumpButtonType.Reply, 0);
            if (Statistiques.CanLower(from, StatType.Str))
                AddButton(x + 130 + 20, 105 + y + line * scale, 9771, 9771, 14, GumpButtonType.Reply, 0);
            line += 5;

            AddButton(x, (y + line * scale) + 20, 1437, 1437, 3, GumpButtonType.Reply, 0);
            AddTooltip(3001037);
            AddSection(x + 110, y + line * scale, 426, 60, "Dexterité", "Augmente la regénération de la stamina, la vitesse de frappe, la vitesse de lancer de sort et le maximum de points de stamina. Bonus: +25 Stamina", new string[] { "<basefont color=#5A4A31>      Dexterite: " + from.RawDex + "<basefont>" });
            if (Statistiques.CanRaise(from, StatType.Dex))
                AddButton(x + 130, 105 + y + line * scale, 9770, 9770, 17, GumpButtonType.Reply, 0);
            if (Statistiques.CanLower(from, StatType.Dex))
                AddButton(x + 130 + 20, 105 + y + line * scale, 9771, 9771, 18, GumpButtonType.Reply, 0);
            line += 5;

            AddButton(x, (y + line * scale) + 20, 1438, 1438, 3, GumpButtonType.Reply, 0);
            AddTooltip(3001037);
            AddSection(x + 110, y + line * scale, 426, 60, "Intelligence", "Augmente la regénération de la mana, la puissance des sorts et le maximum de points de mana. Bonus: +25 Mana", new string[] { "<basefont color=#5A4A31>      Intelligence: " + from.RawInt + "<basefont>" });
            if (Statistiques.CanRaise(from, StatType.Int))
                AddButton(x + 130, 105 + y + line * scale, 9770, 9770, 21, GumpButtonType.Reply, 0);
            if (Statistiques.CanLower(from, StatType.Int))
                AddButton(x + 130 + 20, 105 + y + line * scale, 9771, 9771, 22, GumpButtonType.Reply, 0);
        }
        public override void OnResponse(NetState sender, RelayInfo info)
        {
            PlayerMobile from = (PlayerMobile)sender.Mobile;

            if (from.Deleted || !from.Alive)
                return;

            if (info.ButtonID < 8)
            {
                base.OnResponse(sender, info);
                return;
            }

            switch (info.ButtonID)
            {
                //case 13:
                //    if (Statistiques.CanRaise(from, StatType.Str))
                //    {
                //        from.RawStr += 5;
                //        //from.StatistiquesLibres -= 5;
                //    }
                //    from.SendGump(new FicheCaracteristiqueGump(from));
                //    break;
                //case 14:
                //    if (Statistiques.CanLower(from, StatType.Str))
                //    {
                //        from.RawStr -= 5;
                //    }
                //    from.SendGump(new FicheCaracteristiqueGump(from));
                //    break;
                //case 17:
                //    if (Statistiques.CanRaise(from, StatType.Dex))
                //    {
                //        from.RawDex += 5;
                //        //from.StatistiquesLibres -= 5;
                //    }
                //    from.SendGump(new FicheCaracteristiqueGump(from));
                //    break;
                //case 18:
                //    if (Statistiques.CanLower(from, StatType.Dex))
                //    {
                //        from.RawDex -= 5;
                //    }
                //    from.SendGump(new FicheCaracteristiqueGump(from));
                //    break;
                //case 21:
                //    if (Statistiques.CanRaise(from, StatType.Int))
                //    {
                //        from.RawInt += 5;
                //        //from.StatistiquesLibres -= 5;
                //    }
                //    from.SendGump(new FicheCaracteristiqueGump(from));
                //    break;
                //case 22:
                //    if (Statistiques.CanLower(from, StatType.Int))
                //    {
                //        from.RawInt -= 5;
                //    }
                //    from.SendGump(new FicheCaracteristiqueGump(from));
                //    break;
            }
        }
    }
}
