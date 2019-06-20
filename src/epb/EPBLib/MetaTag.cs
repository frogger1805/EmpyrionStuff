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
        UnknownMetax01 = 0x01,
        UnknownMetax03 = 0x03,
        UnknownMetax04 = 0x04,
        UnknownMetax05 = 0x05,
        UnknownMetax06 = 0x06,
        BlueprintName  = 0x07,
        BuildVersion   = 0x08,
        CreationTime   = 0x09,
        CreatorName    = 0x0a,
        CreatorId      = 0x0b, // Shows up in the statistics tab in game
        OwnerName      = 0x0c,
        OwnerId        = 0x0d,
        UnknownMetax0E = 0x0e,
        UnknownMetax0F = 0x0f,
        DisplayName    = 0x10, // Shown in hover text
        UnknownMetax11 = 0x11,
        UnknownMetax12 = 0x12
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