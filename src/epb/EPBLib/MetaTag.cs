﻿using System;
using System.IO;
using System.Threading.Tasks;

namespace EPBLib
{
    public class MetaTag
    {
        public MetaTagKey Key { get; protected set; }
        public MetaTagType TagType { get; protected set; }
        public virtual string ValueToString()
        {
            return ToString();
        }
        public override string ToString()
        {
            return $"{Key,-14}: Type={TagType}";
        }
    }

    public enum MetaTagKey
    {
        TerrainRemoval = 1,
        FlattensGround = 2,
        UnknownMetax03 = 3,
        UnknownMetax04 = 4,
        UnknownMetax05 = 5,
        UnknownMetax06 = 6,
        GroupName      = 7,
        BuildVersion   = 8,
        ChangedTime    = 9,
        CreatorName    = 10,
        CreatorId      = 11, // Shows up in the statistics tab in game
        ChangedByName  = 12,
        ChangedById    = 13,
        UnknownMetax0E = 14,
        UnknownMetax0F = 15, // In CV_Prefab_Tier1 and CV_Prefab_Tier1a this matches fuel fill percentage, in CV_Prefab_Tier5 it matches number of fuel tanks.
        SpawnName      = 16, // Shown in hover text
        GroundOffset   = 17,
        UnknownMetax12 = 18,
        KeepTopSoil    = 19
    }

    public enum MetaTagType
    {
        String     = 0x00000000,
        UInt16     = 0x01000000,
        Unknownx02 = 0x02000000,
        Unknownx03 = 0x03000000,
        Unknownx04 = 0x04000000,
        Unknownx05 = 0x05000000
    }

}
