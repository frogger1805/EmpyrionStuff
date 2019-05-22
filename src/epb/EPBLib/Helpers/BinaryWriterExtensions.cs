﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPBLib.Logic;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace EPBLib.Helpers
{
    public static class BinaryWriterExtensions
    {
        #region EpBlueprint
        public static readonly UInt32 EpbIdentifier = 0x78945245;
        public static UInt32 EpbVersion = 20;

        public static void Write(this BinaryWriter writer, EpBlueprint epb)
        {
            writer.Write(EpbIdentifier);
            writer.Write(EpbVersion);
            writer.Write((byte)epb.Type);
            writer.Write(epb.Width);
            writer.Write(epb.Height);
            writer.Write(epb.Depth);
            writer.Write((UInt16)1); //Unknown01
            writer.Write(epb.MetaTags);

            writer.Write((UInt16)0); //Unknown02
            writer.Write((UInt32)0); // TODO: Count the number of light blocks in the model
            writer.Write((UInt32)0); // UnknownCount01
            writer.Write((UInt32)0); // TODO: Count the number of devices in the model
            writer.Write((UInt32)0); // UnknownCount02
            writer.Write((UInt32)epb.Blocks.Count);
            writer.Write((UInt32)0); // UnknownCount03
            writer.Write((UInt32)0); // TODO: Count the number of triangles in the model

            Dictionary<EpbBlock.EpbBlockType, UInt32> blockCounts = new Dictionary<EpbBlock.EpbBlockType, uint>();
            foreach (EpbBlock block in epb.Blocks)
            {
                if (!blockCounts.ContainsKey(block.BlockType))
                {
                    blockCounts.Add(block.BlockType, 0);
                }
                blockCounts[block.BlockType]++;
            }
            writer.Write((UInt16)blockCounts.Count);
            foreach (EpbBlock.EpbBlockType type in blockCounts.Keys)
            {
                writer.Write(type);
                writer.Write(blockCounts[type]);
            }

            writer.Write((byte)0x04); // unknown05

            writer.Write(epb.DeviceGroups);
            writer.WriteEpbBlocks(epb);
        }
        #endregion EpBlueprint

        #region EpbBlocks

        public static void Write(this BinaryWriter writer, EpbBlock.EpbBlockType type)
        {
            writer.Write((UInt16)type.Id);
        }

        public static void WriteEpbBlocks(this BinaryWriter writer, EpBlueprint epb)
        {
            long byteCount = 0;
            List<byte> byteList = new List<byte>();

            byteCount = AddEpbBlockTypesToList(epb, byteCount, byteList);
            byteCount = AddDamageStatesToList(epb, byteCount, byteList);
            byteCount = AddUnknown02ToList(byteList, byteCount);
            byteCount = AddColourMatrixToList(epb, byteCount, byteList);
            byteCount = AddTextureMatrixToList(epb, byteCount, byteList);
            byteCount = AddTextureFlipMatrixToList(epb, byteCount, byteList);
            byteCount = AddSymbolMatrixToList(epb, byteCount, byteList);
            byteCount = AddSymbolRotationMatrixToList(epb, byteCount, byteList);
            byteCount = AddBlockTagsToList(epb, byteCount, byteList);
            byteCount = AddUnknown07ToList(epb, byteCount, byteList);
            byteCount = AddSignalSourcesToList(epb, byteCount, byteList);
            byteCount = AddSignalTargetsToList(epb, byteCount, byteList);
            byteCount = AddSignalOperatorsToList(epb, byteCount, byteList);
            byteCount = AddCustomNamesToList(epb, byteCount, byteList);
            byteCount = AddUnknown08ToList(epb, byteCount, byteList);
            byteCount = AddCustomPalettesToList(epb, byteCount, byteList);

            byte[] blockBuffer = byteList.ToArray();




            using (MemoryStream outputMemStream = new MemoryStream())
            {
                using (MemoryStream memStreamIn = new MemoryStream(blockBuffer))
                {
                    ZipOutputStream zipStream = new ZipOutputStream(outputMemStream);
                    ZipEntry newEntry = new ZipEntry("0");
                    newEntry.DateTime = DateTime.Now;
                    newEntry.CompressionMethod = CompressionMethod.Deflated;
                    newEntry.Size = blockBuffer.Length;         // If the size is not set, it will force zip64! Zip64 is not supported by Empyrion
                    zipStream.PutNextEntry(newEntry);
                    StreamUtils.Copy(memStreamIn, zipStream, new byte[4096]);
                    zipStream.CloseEntry();
                    zipStream.IsStreamOwner = false;    // False stops the Close also Closing the underlying stream.
                    zipStream.Close();                  // Must finish the ZipOutputStream before using outputMemStream.
                }
                outputMemStream.Position = 0;
                byte[] zipBuffer = outputMemStream.ToArray();
                zipBuffer[0] = 0x00;
                zipBuffer[1] = 0x00;
                writer.Write(zipBuffer);
            }
        }

        private static long AddEpbBlockTypesToList(EpBlueprint epb, long byteCount, List<byte> byteList)
        {
            byteCount += AddEpbMatrixToList(epb, byteList, (blueprint, block, list) =>
            {
                if (block == null)
                {
                    return false;
                }

                UInt32 data = (UInt32)block.BlockType.Id + (UInt32)((byte)block.Rotation << 11) +
                              (UInt32)(block.Unknown00 << 16) + (UInt32)(block.Variant << 25);
                list.AddRange(BitConverter.GetBytes(data));
                return true;
            });
            return byteCount;
        }

        private static long AddDamageStatesToList(EpBlueprint epb, long byteCount, List<byte> byteList)
        {
            byteCount += AddEpbMatrixToList(epb, byteList, (blueprint, block, list) => { return false; });
            return byteCount;
        }

        private static long AddUnknown02ToList(List<byte> byteList, long byteCount)
        {
            byteList.Add(0x01);
            byteCount += 1;
            byteList.Add(0x7f);
            byteCount += 1;
            return byteCount;
        }

        private static long AddColourMatrixToList(EpBlueprint epb, long byteCount, List<byte> byteList)
        {
            byteCount += AddEpbMatrixToList(epb, byteList, (blueprint, block, list) =>
            {
                if (block == null)
                {
                    return false;
                }

                UInt32 bits = 0;
                UInt32 factor = 1;
                for (int i = 0; i < 6; i++)
                {
                    bits += (byte)block.Colours[i] * factor;
                    factor = factor << 5;
                }

                if (bits == 0)
                {
                    return false;
                }

                list.AddRange(BitConverter.GetBytes(bits));
                return true;
            });
            return byteCount;
        }

        private static long AddTextureMatrixToList(EpBlueprint epb, long byteCount, List<byte> byteList)
        {
            byteCount += AddEpbMatrixToList(epb, byteList, (blueprint, block, list) =>
            {
                if (block == null)
                {
                    return false;
                }

                UInt64 bits = 0;
                UInt64 factor = 1;
                for (int i = 0; i < 6; i++)
                {
                    bits += block.Textures[i] * factor;
                    factor = factor << 6;
                }

                if (bits == 0)
                {
                    return false;
                }

                list.AddRange(BitConverter.GetBytes(bits));
                return true;
            });
            return byteCount;
        }

        private static long AddTextureFlipMatrixToList(EpBlueprint epb, long byteCount, List<byte> byteList)
        {
            byteCount += AddEpbMatrixToList(epb, byteList, (blueprint, block, list) =>
            {
                if (block == null)
                {
                    return false;
                }

                byte bits = 0;
                int factor = 1;
                for (int i = 0; i < 6; i++)
                {
                    bits += (byte)((block.TextureFlips[i] ? 1 : 0) * factor);
                    factor = factor << 1;
                }

                if (bits == 0)
                {
                    return false;
                }

                list.Add(bits);
                return true;
            });
            return byteCount;
        }

        private static long AddSymbolMatrixToList(EpBlueprint epb, long byteCount, List<byte> byteList)
        {
            byteCount += AddEpbMatrixToList(epb, byteList, (blueprint, block, list) =>
            {
                if (block == null)
                {
                    return false;
                }

                UInt32 bits = 0;
                UInt32 factor = 1;
                for (int i = 0; i < 6; i++)
                {
                    bits += block.Symbols[i] * factor;
                    factor = factor << 5;
                }

                bits += block.SymbolPage * factor;

                if (bits == 0)
                {
                    return false;
                }

                list.AddRange(BitConverter.GetBytes(bits));
                return true;
            });
            return byteCount;
        }

        private static long AddSymbolRotationMatrixToList(EpBlueprint epb, long byteCount, List<byte> byteList)
        {
            byteCount += AddEpbMatrixToList(epb, byteList, (blueprint, block, list) =>
            {
                if (block == null)
                {
                    return false;
                }

                UInt32 bits = 0;
                UInt32 factor = 1;
                for (int i = 0; i < 6; i++)
                {
                    bits += (byte)block.SymbolRotations[i] * factor;
                    factor = factor << 2;
                }

                if (bits == 0)
                {
                    return false;
                }

                list.AddRange(BitConverter.GetBytes(bits));
                return true;
            });
            return byteCount;
        }

        private static long AddBlockTagsToList(EpBlueprint epb, long byteCount, List<byte> byteList)
        {
            int nBlocks = 0;
            List<byte> l = new List<byte>();
            foreach (EpbBlock block in epb.Blocks)
            {
                if (block.Tags.Count != 0)
                {
                    byteCount += AddBlockPosToList(epb, l, block.Position);
                    l.Add(0); // Unknown06
                    byteCount += 1;
                    byteCount += AddBlockTagListToList(epb, l, block.Tags.Values.ToArray()); // TODO: Is the order of these significant?
                    nBlocks++;
                }
            }
            byteList.AddRange(BitConverter.GetBytes((UInt16)nBlocks));
            byteCount += 2;
            byteList.AddRange(l.ToArray());
            return byteCount;
        }

        private static long AddUnknown07ToList(EpBlueprint epb, long byteCount, List<byte> byteList)
        {
            //TODO: Save the actual thing
            UInt16 nUnknown07 = 0;
            byteList.AddRange(BitConverter.GetBytes(nUnknown07));
            return byteCount;
        }

        private static long AddSignalSourcesToList(EpBlueprint epb, long byteCount, List<byte> byteList)
        {
            UInt16 nSignalSources = (UInt16)epb.SignalSources.Count;
            byteList.AddRange(BitConverter.GetBytes(nSignalSources));
            foreach (EpbSignalSource source in epb.SignalSources)
            {
                byteList.Add(source.Unknown01);
                byteCount += AddBlockTagListToList(epb, byteList, source.Tags.Values.ToArray()); // TODO: Is the order of these significant?
            }
            return byteCount;
        }



        private static long AddSignalTargetsToList(EpBlueprint epb, long byteCount, List<byte> byteList)
        {
            //TODO: Save the actual thing
            UInt16 nSignalTargets = 0;
            byteList.AddRange(BitConverter.GetBytes(nSignalTargets));
            return byteCount;
        }

        private static long AddSignalOperatorsToList(EpBlueprint epb, long byteCount, List<byte> byteList)
        {
            //TODO: Save the actual thing
            UInt16 nSignalOperators = 0;
            byteList.AddRange(BitConverter.GetBytes(nSignalOperators));
            return byteCount;
        }

        private static long AddCustomNamesToList(EpBlueprint epb, long byteCount, List<byte> byteList)
        {
            //TODO: Save the actual thing
            UInt16 nCustom = 0;
            byteList.AddRange(BitConverter.GetBytes(nCustom));
            return byteCount;
        }

        private static long AddUnknown08ToList(EpBlueprint epb, long byteCount, List<byte> byteList)
        {
            //TODO: Save the actual thing
            UInt16 nUnknown08 = 0;
            byteList.AddRange(BitConverter.GetBytes(nUnknown08));
            return byteCount;
        }

        private static long AddCustomPalettesToList(EpBlueprint epb, long byteCount, List<byte> byteList)
        {
            return byteCount;
        }



        #region BlockTags
        private static long AddBlockTagListToList(EpBlueprint epb, List<byte> byteList, EpbBlockTag[] tags)
        {
            long byteCount = 0;
            byteList.AddRange(BitConverter.GetBytes((UInt16)tags.Length));
            foreach (EpbBlockTag tag in tags)
            {
                byteList.Add((byte)tag.BlockTagType);
                byteCount += 1;
                byteCount += AddStringToList(epb, byteList, tag.Name);
                switch (tag)
                {
                    case EpbBlockTagBool tagBool:
                        byteCount += AddBlockTagValueToList(epb, byteList, tagBool);
                        break;
                    case EpbBlockTagColour tagColour:
                        byteCount += AddBlockTagValueToList(epb, byteList, tagColour);
                        break;
                    case EpbBlockTagFloat tagFloat:
                        byteCount += AddBlockTagValueToList(epb, byteList, tagFloat);
                        break;
                    case EpbBlockTagPos tagPos:
                        byteCount += AddBlockTagValueToList(epb, byteList, tagPos);
                        break;
                    case EpbBlockTagString tagString:
                        byteCount += AddBlockTagValueToList(epb, byteList, tagString);
                        break;
                    case EpbBlockTagUInt32 tagUInt32:
                        byteCount += AddBlockTagValueToList(epb, byteList, tagUInt32);
                        break;
                }
            }
            return byteCount;
        }

        private static long AddBlockTagValueToList(EpBlueprint epb, List<byte> byteList, EpbBlockTagBool tag)
        {
            byteList.Add(tag.Value ? (byte)1 : (byte)0);
            return 1;
        }
        private static long AddBlockTagValueToList(EpBlueprint epb, List<byte> byteList, EpbBlockTagColour tag)
        {
            byteList.AddRange(BitConverter.GetBytes(tag.Value));
            return 4;
        }
        private static long AddBlockTagValueToList(EpBlueprint epb, List<byte> byteList, EpbBlockTagFloat tag)
        {
            byteList.AddRange(BitConverter.GetBytes(tag.Value));
            return 4;
        }
        private static long AddBlockTagValueToList(EpBlueprint epb, List<byte> byteList, EpbBlockTagPos tag)
        {
            return AddBlockPosToList(epb, byteList, tag.Value);
        }

        private static long AddBlockTagValueToList(EpBlueprint epb, List<byte> byteList, EpbBlockTagString tag)
        {
            return AddStringToList(epb, byteList, tag.Value);
        }

        private static long AddBlockTagValueToList(EpBlueprint epb, List<byte> byteList, EpbBlockTagUInt32 tag)
        {
            byteList.AddRange(BitConverter.GetBytes(tag.Value));
            return 4;
        }
        #endregion BlockTags



        public static long AddEpbMatrixToList(EpBlueprint epb, List<byte> list, Func<EpBlueprint, EpbBlock, List<byte>, bool> func)
        {
            bool[] m = new bool[epb.Width * epb.Height * epb.Depth];
            List<byte> tmpList = new List<byte>();

            for (UInt32 z = 0; z < epb.Depth; z++)
            {
                for (UInt32 y = 0; y < epb.Height; y++)
                {
                    for (UInt32 x = 0; x < epb.Width; x++)
                    {
                        EpbBlock block = epb.Blocks[(byte)x, (byte)y, (byte)z];
                        if (func(epb, block, tmpList))
                        {
                            EpbBlockPos pos = block.Position;
                            m[pos.Z * epb.Width * epb.Height + pos.Y * epb.Width + pos.X] = true;
                        }
                    }
                }
            }

            byte[] matrix = m.ToByteArray();
            UInt32 matrixLength = (UInt32)matrix.Length;
            list.AddRange(BitConverter.GetBytes(matrixLength));
            list.AddRange(matrix);
            list.AddRange(tmpList);
            return 4 + matrix.Length + tmpList.Count;
        }

        private static long AddBlockPosToList(EpBlueprint epb, List<byte> byteList, EpbBlockPos pos)
        {
            UInt32 data = ((UInt32)(pos.X  & 0xff) << 20)
                        + ((UInt32)(pos.Y  & 0xff) << 12)
                        + ((UInt32)(pos.Z  & 0xff) <<  0)
                        + ((UInt32)(pos.U1 & 0x0f) << 28)
                        + ((UInt32)(pos.U2 & 0x0f) <<  8);
            byteList.AddRange(BitConverter.GetBytes(data));
            return 4;
        }

        private static long AddStringToList(EpBlueprint epb, List<byte> byteList, string s)
        {
            long byteCount = 0;
            byte[] buf = System.Text.Encoding.UTF8.GetBytes(s);
            int len = buf.Length;
            do
            {
                byte b = (byte) (len & 0x7f);
                len >>= 7;
                if (len > 0)
                {
                    b += 0x80; // Set top bit to indicate more length bytes
                }
                byteList.Add(b);
                byteCount += 1;
            } while (len > 0);
            byteList.AddRange(buf);
            byteCount += buf.Length;
            return byteCount;
        }

        #endregion EpbBlocks

        #region EpbMetaTags
        public static void Write(this BinaryWriter writer, Dictionary<EpMetaTagKey, EpMetaTag> dictionary)
        {
            writer.Write((UInt16)dictionary.Count);
            foreach (EpMetaTag tag in dictionary.Values)
            {
                switch (tag.TagType)
                {
                    case EpMetaTagType.String:
                        writer.Write((EpMetaTagString)tag);
                        break;
                    case EpMetaTagType.UInt16:
                        writer.Write((EpMetaTagUInt16)tag);
                        break;
                    case EpMetaTagType.Unknownx02:
                        writer.Write((EpMetaTag02)tag);
                        break;
                    case EpMetaTagType.Unknownx03:
                        writer.Write((EpMetaTag03)tag);
                        break;
                    case EpMetaTagType.Unknownx04:
                        writer.Write((EpMetaTag04)tag);
                        break;
                    case EpMetaTagType.Unknownx05:
                        writer.Write((EpMetaTag05)tag);
                        break;
                }
            }
        }

        public static void Write(this BinaryWriter writer, EpMetaTagString tag)
        {
            writer.Write((EpMetaTag)tag);
            writer.WriteEpString(tag.Value);
        }
        public static void Write(this BinaryWriter writer, EpMetaTagUInt16 tag)
        {
            writer.Write((EpMetaTag)tag);
            writer.Write(tag.Value);
        }
        public static void Write(this BinaryWriter writer, EpMetaTag02 tag)
        {
            writer.Write((EpMetaTag)tag);
            writer.Write(tag.Value);
            writer.Write(tag.Unknown);
        }
        public static void Write(this BinaryWriter writer, EpMetaTag03 tag)
        {
            writer.Write((EpMetaTag)tag);
            writer.Write(tag.Value);
        }
        public static void Write(this BinaryWriter writer, EpMetaTag04 tag)
        {
            writer.Write((EpMetaTag)tag);
            writer.Write(tag.Value);
        }
        public static void Write(this BinaryWriter writer, EpMetaTag05 tag)
        {
            writer.Write((EpMetaTag)tag);
            writer.Write(tag.Value.ToBinary());
            writer.Write(tag.Unknown);
        }

        public static void Write(this BinaryWriter writer, EpMetaTag tag)
        {
            writer.Write((UInt32)tag.Key);
            writer.Write((UInt32)tag.TagType);
        }

        #endregion EpbMetaTags

        #region EpbDevices

        public static void Write(this BinaryWriter writer, List<EpbDeviceGroup> groups)
        {
            writer.Write((UInt16)groups.Count);
            foreach (var group in groups)
            {
                writer.Write(group);
            }
        }

        public static void Write(this BinaryWriter writer, EpbDeviceGroup group)
        {
            writer.WriteEpString(group.Name);
            writer.Write(group.DeviceGroupUnknown03);
            writer.Write(group.DeviceGroupUnknown01);
            writer.Write(group.Shortcut);
            writer.Write((UInt16)group.Entries.Count);
            foreach (var device in group.Entries)
            {
                writer.Write(device);
            }
        }

        public static void Write(this BinaryWriter writer, EpbDeviceGroupEntry entry)
        {
            writer.Write(entry.Pos);
            writer.WriteEpString(entry.Name);
        }
        #endregion EpbDevices

        public static void Write(this BinaryWriter writer, EpbBlockPos pos)
        {
            UInt32 data = (UInt32)(pos.U1 << 28 | pos.X << 20 | pos.Y << 12 | pos.U2 << 8 | pos.Z);
            writer.Write(data);
        }
        public static void WriteEpString(this BinaryWriter writer, string s)
        {
            byte[] buf = System.Text.Encoding.UTF8.GetBytes(s);
            int len = buf.Length;
            do
            {
                byte b = (byte)(len & 0x7f);
                len >>= 7;
                if (len > 0)
                {
                    b += 0x80; // Set top bit to indicate more length bytes
                }
                writer.Write(b);
            } while (len > 0);
            writer.Write(buf);
        }

    }
}
