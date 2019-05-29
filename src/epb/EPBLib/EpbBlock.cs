﻿
using System;
using System.Collections.Generic;
using System.Linq;
using EPBLib.BlockData;

namespace EPBLib
{
    public class EpbBlock
    {
        #region static

        public enum EpbBlockRotation
        {
            // FwdUp : P=Positive, N=Negative
            PzPy, PxPy, NzPy, NxPy, PzPx, PyPx, NzPx, NyPx, NzNy, NxNy, PzNy, PxNy, PzNx, PyNx, NzNx, NyNx, PyNz, PxNz, NyNz, NxNz, NxPz, NyPz, PxPz, PyPz
        }

        public class EpbBlockType
        {
            public UInt16 Id;
            public string Name;
            public string Category;
            public string Ref;

            public override string ToString()
            {
                return $"{Name} (0x{Id:x4}={Id})";
            }
        }

        public static readonly Dictionary<UInt16, EpbBlockType> BlockTypes = new Dictionary<UInt16, EpbBlockType>()
        {
            {   256, new EpbBlockType(){Id =   256, Name = "CapacitorMS"                  , Category = "Deco Blocks"                  , Ref = ""                             }},
            {   257, new EpbBlockType(){Id =   257, Name = "CockpitMS01"                  , Category = "Devices"                      , Ref = ""                             }},
            {   259, new EpbBlockType(){Id =   259, Name = "FuelTankMSSmall"              , Category = "Devices"                      , Ref = ""                             }},
            {   260, new EpbBlockType(){Id =   260, Name = "FuelTankMSLarge"              , Category = "Devices"                      , Ref = "FuelTankMSSmall"              }},
            {   261, new EpbBlockType(){Id =   261, Name = "ConsoleMS01"                  , Category = "Deco Blocks"                  , Ref = ""                             }},
            {   262, new EpbBlockType(){Id =   262, Name = "Antenna"                      , Category = "Deco Blocks"                  , Ref = ""                             }},
            {   263, new EpbBlockType(){Id =   263, Name = "OxygenTankMS"                 , Category = "Devices"                      , Ref = ""                             }},
            {   266, new EpbBlockType(){Id =   266, Name = "PassengerSeatMS"              , Category = "Devices"                      , Ref = ""                             }},
            {   267, new EpbBlockType(){Id =   267, Name = "CockpitMS02"                  , Category = "Devices"                      , Ref = ""                             }},
            {   270, new EpbBlockType(){Id =   270, Name = "MedicinelabMS"                , Category = "Devices"                      , Ref = ""                             }},
            {   272, new EpbBlockType(){Id =   272, Name = "RCSBlockSV"                   , Category = "Devices"                      , Ref = ""                             }},
            {   273, new EpbBlockType(){Id =   273, Name = "ContainerMS01"                , Category = "Devices"                      , Ref = ""                             }},
            {   278, new EpbBlockType(){Id =   278, Name = "GravityGeneratorMS"           , Category = "Devices"                      , Ref = ""                             }},
            {   279, new EpbBlockType(){Id =   279, Name = "LightSS01"                    , Category = "Devices"                      , Ref = ""                             }},
            {   280, new EpbBlockType(){Id =   280, Name = "LightMS01"                    , Category = "Devices"                      , Ref = ""                             }},
            {   281, new EpbBlockType(){Id =   281, Name = "DoorMS01"                     , Category = "Devices"                      , Ref = ""                             }},
            {   282, new EpbBlockType(){Id =   282, Name = "TurretTemplate"               , Category = "Weapons/Items"                , Ref = ""                             }},
            {   283, new EpbBlockType(){Id =   283, Name = "TurretMSMinigunRetract"       , Category = "Weapons/Items"                , Ref = "TurretTemplate"               }},
            {   284, new EpbBlockType(){Id =   284, Name = "TurretMSRocketRetract"        , Category = "Weapons/Items"                , Ref = "TurretTemplate"               }},
            {   287, new EpbBlockType(){Id =   287, Name = "TurretMSMinigun"              , Category = "Weapons/Items"                , Ref = "TurretMSMinigunRetract"       }},
            {   288, new EpbBlockType(){Id =   288, Name = "TurretMSRocket"               , Category = "Weapons/Items"                , Ref = "TurretMSRocketRetract"        }},
            {   289, new EpbBlockType(){Id =   289, Name = "TurretRadar"                  , Category = "Deco Blocks"                  , Ref = ""                             }},
            {   291, new EpbBlockType(){Id =   291, Name = "OxygenStation"                , Category = "Devices"                      , Ref = ""                             }},
            {   320, new EpbBlockType(){Id =   320, Name = "TurretDrillTemplate"          , Category = "Weapons/Items"                , Ref = ""                             }},
            {   321, new EpbBlockType(){Id =   321, Name = "TurretMSDrillRetract"         , Category = "Weapons/Items"                , Ref = "TurretDrillTemplate"          }},
            {   322, new EpbBlockType(){Id =   322, Name = "TurretMSToolRetract"          , Category = "Weapons/Items"                , Ref = "TurretDrillTemplate"          }},
            {   323, new EpbBlockType(){Id =   323, Name = "TurretMSPulseLaserRetract"    , Category = "Weapons/Items"                , Ref = "TurretTemplate"               }},
            {   324, new EpbBlockType(){Id =   324, Name = "TurretMSPlasmaRetract"        , Category = "Weapons/Items"                , Ref = "TurretTemplate"               }},
            {   325, new EpbBlockType(){Id =   325, Name = "TurretMSFlakRetract"          , Category = "Weapons/Items"                , Ref = "TurretTemplate"               }},
            {   326, new EpbBlockType(){Id =   326, Name = "TurretMSCannonRetract"        , Category = "Weapons/Items"                , Ref = "TurretTemplate"               }},
            {   327, new EpbBlockType(){Id =   327, Name = "TurretMSArtilleryRetract"     , Category = "Weapons/Items"                , Ref = "TurretTemplate"               }},
            {   328, new EpbBlockType(){Id =   328, Name = "NPCAlienTemplate"             , Category = "Deco Blocks"                  , Ref = ""                             }},
            {   329, new EpbBlockType(){Id =   329, Name = "NPCHumanTemplate"             , Category = "Deco Blocks"                  , Ref = ""                             }},
            {   330, new EpbBlockType(){Id =   330, Name = "AntennaBlocks"                , Category = "Deco Blocks"                  , Ref = ""                             }},
            {   331, new EpbBlockType(){Id =   331, Name = "ContainerUltraRare"           , Category = "Devices"                      , Ref = "ContainerMS01"                }},
            {   333, new EpbBlockType(){Id =   333, Name = "RailingDiagonal"              , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   334, new EpbBlockType(){Id =   334, Name = "RailingVert"                  , Category = "BuildingBlocks"               , Ref = "RailingDiagonal"              }},
            {   335, new EpbBlockType(){Id =   335, Name = "OfflineProtector"             , Category = "Devices"                      , Ref = ""                             }},
            {   336, new EpbBlockType(){Id =   336, Name = "PentaxidTank"                 , Category = "Devices"                      , Ref = ""                             }},
            {   339, new EpbBlockType(){Id =   339, Name = "SurvivalTent"                 , Category = "Devices"                      , Ref = ""                             }},
            {   380, new EpbBlockType(){Id =   380, Name = "HullSmallBlocks"              , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   381, new EpbBlockType(){Id =   381, Name = "HullFullSmall"                , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   382, new EpbBlockType(){Id =   382, Name = "HullThinSmall"                , Category = "BuildingBlocks"               , Ref = "HullFullSmall"                }},
            {   383, new EpbBlockType(){Id =   383, Name = "HullArmoredFullSmall"         , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   384, new EpbBlockType(){Id =   384, Name = "HullArmoredThinSmall"         , Category = "BuildingBlocks"               , Ref = "HullArmoredFullSmall"         }},
            {   393, new EpbBlockType(){Id =   393, Name = "HullArmoredSmallBlocks"       , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   396, new EpbBlockType(){Id =   396, Name = "WoodBlocks"                   , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   397, new EpbBlockType(){Id =   397, Name = "WoodFull"                     , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   398, new EpbBlockType(){Id =   398, Name = "WoodThin"                     , Category = "BuildingBlocks"               , Ref = "WoodFull"                     }},
            {   399, new EpbBlockType(){Id =   399, Name = "ConcreteBlocks"               , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   400, new EpbBlockType(){Id =   400, Name = "ConcreteFull"                 , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   401, new EpbBlockType(){Id =   401, Name = "ConcreteThin"                 , Category = "BuildingBlocks"               , Ref = "ConcreteFull"                 }},
            {   402, new EpbBlockType(){Id =   402, Name = "HullLargeBlocks"              , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   403, new EpbBlockType(){Id =   403, Name = "HullFullLarge"                , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   404, new EpbBlockType(){Id =   404, Name = "HullThinLarge"                , Category = "BuildingBlocks"               , Ref = "HullFullLarge"                }},
            {   405, new EpbBlockType(){Id =   405, Name = "HullArmoredLargeBlocks"       , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   406, new EpbBlockType(){Id =   406, Name = "HullArmoredFullLarge"         , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   407, new EpbBlockType(){Id =   407, Name = "HullArmoredThinLarge"         , Category = "BuildingBlocks"               , Ref = "HullArmoredFullLarge"         }},
            {   408, new EpbBlockType(){Id =   408, Name = "AlienBlocks"                  , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   409, new EpbBlockType(){Id =   409, Name = "AlienFull"                    , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   410, new EpbBlockType(){Id =   410, Name = "AlienThin"                    , Category = "BuildingBlocks"               , Ref = "AlienFull"                    }},
            {   411, new EpbBlockType(){Id =   411, Name = "HullCombatLargeBlocks"        , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   412, new EpbBlockType(){Id =   412, Name = "HullCombatFullLarge"          , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   413, new EpbBlockType(){Id =   413, Name = "HullCombatThinLarge"          , Category = "BuildingBlocks"               , Ref = "HullCombatFullLarge"          }},
            {   416, new EpbBlockType(){Id =   416, Name = "TrussCube"                    , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   417, new EpbBlockType(){Id =   417, Name = "LandinggearSV"                , Category = "Devices"                      , Ref = ""                             }},
            {   418, new EpbBlockType(){Id =   418, Name = "GeneratorSV"                  , Category = "Devices"                      , Ref = ""                             }},
            {   419, new EpbBlockType(){Id =   419, Name = "FuelTankSV"                   , Category = "Devices"                      , Ref = ""                             }},
            {   420, new EpbBlockType(){Id =   420, Name = "RCSBlockMS"                   , Category = "Devices"                      , Ref = ""                             }},
            {   422, new EpbBlockType(){Id =   422, Name = "OxygenTankSV"                 , Category = "Devices"                      , Ref = ""                             }},
            {   423, new EpbBlockType(){Id =   423, Name = "FridgeSV"                     , Category = "Devices"                      , Ref = ""                             }},
            {   428, new EpbBlockType(){Id =   428, Name = "WeaponSV01"                   , Category = "Weapons/Items"                , Ref = ""                             }},
            {   429, new EpbBlockType(){Id =   429, Name = "WeaponSV02"                   , Category = "Weapons/Items"                , Ref = ""                             }},
            {   430, new EpbBlockType(){Id =   430, Name = "WeaponSV03"                   , Category = "Weapons/Items"                , Ref = ""                             }},
            {   431, new EpbBlockType(){Id =   431, Name = "WeaponSV04"                   , Category = "Weapons/Items"                , Ref = ""                             }},
            {   432, new EpbBlockType(){Id =   432, Name = "WeaponSV05"                   , Category = "Weapons/Items"                , Ref = ""                             }},
            {   445, new EpbBlockType(){Id =   445, Name = "LandinggearMSHeavy"           , Category = "Devices"                      , Ref = ""                             }},
            {   446, new EpbBlockType(){Id =   446, Name = "Deprecated-RampSteep"         , Category = "BuildingBlocks"               , Ref = "Hull"                         }}, //Removed in A10.0.0-2446
            {   449, new EpbBlockType(){Id =   449, Name = "ThrusterSVRoundNormal"        , Category = "Devices"                      , Ref = ""                             }},
            {   450, new EpbBlockType(){Id =   450, Name = "ThrusterSVRoundArmored"       , Category = "Devices"                      , Ref = "ThrusterSVRoundNormal"        }},
            {   451, new EpbBlockType(){Id =   451, Name = "ThrusterSVRoundSlant"         , Category = "Devices"                      , Ref = "ThrusterSVRoundNormal"        }},
            {   453, new EpbBlockType(){Id =   453, Name = "ThrusterMSRoundArmored"       , Category = "Devices"                      , Ref = ""                             }},
            {   454, new EpbBlockType(){Id =   454, Name = "ThrusterMSRoundSlant"         , Category = "Devices"                      , Ref = "ThrusterMSRoundArmored"       }},
            {   455, new EpbBlockType(){Id =   455, Name = "ThrusterMSRoundNormal"        , Category = "Devices"                      , Ref = "ThrusterMSRoundArmored"       }},
            {   456, new EpbBlockType(){Id =   456, Name = "ThrusterSVDirectional"        , Category = "Devices"                      , Ref = ""                             }},
            {   457, new EpbBlockType(){Id =   457, Name = "ThrusterMSDirectional"        , Category = "Devices"                      , Ref = ""                             }},
            {   458, new EpbBlockType(){Id =   458, Name = "ThrusterMSRoundNormal2x2"     , Category = "Devices"                      , Ref = ""                             }},
            {   459, new EpbBlockType(){Id =   459, Name = "CockpitSV_ShortRange"         , Category = "Devices"                      , Ref = ""                             }},
            {   460, new EpbBlockType(){Id =   460, Name = "DoorSS01"                     , Category = "Devices"                      , Ref = ""                             }},
            {   461, new EpbBlockType(){Id =   461, Name = "StairsMS"                     , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   462, new EpbBlockType(){Id =   462, Name = "GrowingPot"                   , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   468, new EpbBlockType(){Id =   468, Name = "ElevatorMS"                   , Category = "Devices"                      , Ref = ""                             }},
            {   469, new EpbBlockType(){Id =   469, Name = "GeneratorMS"                  , Category = "Devices"                      , Ref = ""                             }},
            {   489, new EpbBlockType(){Id =   489, Name = "WeaponSV05Homing"             , Category = "Weapons/Items"                , Ref = "WeaponSV05"                   }},
            {   491, new EpbBlockType(){Id =   491, Name = "TurretIONCannon"              , Category = "Weapons/Items"                , Ref = ""                             }},
            {   492, new EpbBlockType(){Id =   492, Name = "TurretEnemyLaser"             , Category = "Weapons/Items"                , Ref = "TurretIONCannon"              }},
            {   497, new EpbBlockType(){Id =   497, Name = "ThrusterMSRoundNormal3x3"     , Category = "Devices"                      , Ref = ""                             }},
            {   498, new EpbBlockType(){Id =   498, Name = "GeneratorBA"                  , Category = "Devices"                      , Ref = ""                             }},
            {   514, new EpbBlockType(){Id =   514, Name = "ContainerSpecialEvent"        , Category = "Devices"                      , Ref = "ContainerMS01"                }},
            {   535, new EpbBlockType(){Id =   535, Name = "ContainerBlocks"              , Category = "Devices"                      , Ref = ""                             }},
            {   536, new EpbBlockType(){Id =   536, Name = "ThrusterSVRoundBlocks"        , Category = "Devices"                      , Ref = ""                             }},
            {   537, new EpbBlockType(){Id =   537, Name = "ThrusterMSRoundSlant2x2"      , Category = "Devices"                      , Ref = "ThrusterMSRoundNormal2x2"     }},
            {   538, new EpbBlockType(){Id =   538, Name = "ThrusterMSRoundArmored2x2"    , Category = "Devices"                      , Ref = "ThrusterMSRoundNormal2x2"     }},
            {   539, new EpbBlockType(){Id =   539, Name = "ThrusterMSRoundSlant3x3"      , Category = "Devices"                      , Ref = "ThrusterMSRoundNormal3x3"     }},
            {   540, new EpbBlockType(){Id =   540, Name = "ThrusterMSRoundArmored3x3"    , Category = "Devices"                      , Ref = "ThrusterMSRoundNormal3x3"     }},
            {   541, new EpbBlockType(){Id =   541, Name = "AlienContainer"               , Category = "Devices"                      , Ref = ""                             }},
            {   542, new EpbBlockType(){Id =   542, Name = "AlienContainerRare"           , Category = "Devices"                      , Ref = "AlienContainer"               }},
            {   543, new EpbBlockType(){Id =   543, Name = "AlienContainerVeryRare"       , Category = "Devices"                      , Ref = "AlienContainer"               }},
            {   544, new EpbBlockType(){Id =   544, Name = "AlienContainerUltraRare"      , Category = "Devices"                      , Ref = "AlienContainer"               }},
            {   545, new EpbBlockType(){Id =   545, Name = "WindowShutterLargeBlocks"     , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   554, new EpbBlockType(){Id =   554, Name = "OxygenGenerator"              , Category = "Devices"                      , Ref = ""                             }},
            {   555, new EpbBlockType(){Id =   555, Name = "TurretIONCannon2"             , Category = "Weapons/Items"                , Ref = ""                             }},
            {   556, new EpbBlockType(){Id =   556, Name = "SpotlightSSCube"              , Category = "Devices"                      , Ref = ""                             }},
            {   558, new EpbBlockType(){Id =   558, Name = "Core"                         , Category = "Devices"                      , Ref = ""                             }},
            {   560, new EpbBlockType(){Id =   560, Name = "CoreNPC"                      , Category = "Devices"                      , Ref = ""                             }},
            {   564, new EpbBlockType(){Id =   564, Name = "LightPlant01"                 , Category = "Devices"                      , Ref = ""                             }},
            {   565, new EpbBlockType(){Id =   565, Name = "SentryGun01"                  , Category = "Weapons/Items"                , Ref = ""                             }},
            {   566, new EpbBlockType(){Id =   566, Name = "SentryGun02"                  , Category = "Weapons/Items"                , Ref = ""                             }},
            {   567, new EpbBlockType(){Id =   567, Name = "SentryGun03"                  , Category = "Weapons/Items"                , Ref = ""                             }},
            {   569, new EpbBlockType(){Id =   569, Name = "LightWork"                    , Category = "Devices"                      , Ref = ""                             }},
            {   583, new EpbBlockType(){Id =   583, Name = "FridgeMS02"                   , Category = "Devices"                      , Ref = ""                             }},
            {   584, new EpbBlockType(){Id =   584, Name = "FridgeMS"                     , Category = "Devices"                      , Ref = "FridgeMS02"                   }},
            {   588, new EpbBlockType(){Id =   588, Name = "WaterGenerator"               , Category = "Devices"                      , Ref = ""                             }},
            {   589, new EpbBlockType(){Id =   589, Name = "ThrusterGVDirectional"        , Category = "Devices"                      , Ref = ""                             }},
            {   590, new EpbBlockType(){Id =   590, Name = "ThrusterGVRoundNormal"        , Category = "Devices"                      , Ref = ""                             }},
            {   603, new EpbBlockType(){Id =   603, Name = "HoverBooster"                 , Category = "Devices"                      , Ref = ""                             }},
            {   604, new EpbBlockType(){Id =   604, Name = "RCSBlockGV"                   , Category = "Devices"                      , Ref = ""                             }},
            {   612, new EpbBlockType(){Id =   612, Name = "Bed"                          , Category = "Furnishings (Deco)"           , Ref = "DecoTemplate"                 }},
            {   613, new EpbBlockType(){Id =   613, Name = "Sofa"                         , Category = "Furnishings (Deco)"           , Ref = "DecoTemplate"                 }},
            {   614, new EpbBlockType(){Id =   614, Name = "KitchenCounter"               , Category = "Furnishings (Deco)"           , Ref = "DecoTemplate"                 }},
            {   615, new EpbBlockType(){Id =   615, Name = "KitchenTable"                 , Category = "Furnishings (Deco)"           , Ref = "DecoTemplate"                 }},
            {   617, new EpbBlockType(){Id =   617, Name = "Bookshelf"                    , Category = "Furnishings (Deco)"           , Ref = "DecoTemplate"                 }},
            {   618, new EpbBlockType(){Id =   618, Name = "ControlStation"               , Category = "Furnishings (Deco)"           , Ref = "DecoTemplate"                 }},
            {   619, new EpbBlockType(){Id =   619, Name = "BathroomCounter"              , Category = "Furnishings (Deco)"           , Ref = "DecoTemplate"                 }},
            {   620, new EpbBlockType(){Id =   620, Name = "Toilet"                       , Category = "Furnishings (Deco)"           , Ref = "DecoTemplate"                 }},
            {   621, new EpbBlockType(){Id =   621, Name = "Shower"                       , Category = "Furnishings (Deco)"           , Ref = "DecoTemplate"                 }},
            {   622, new EpbBlockType(){Id =   622, Name = "LightInterior01"              , Category = "Devices"                      , Ref = "LightMS01"                    }},
            {   623, new EpbBlockType(){Id =   623, Name = "LightInterior02"              , Category = "Devices"                      , Ref = "LightMS01"                    }},
            {   629, new EpbBlockType(){Id =   629, Name = "IndoorPlant01"                , Category = "Deco Blocks"                  , Ref = ""                             }},
            {   630, new EpbBlockType(){Id =   630, Name = "IndoorPlant02"                , Category = "Deco Blocks"                  , Ref = "IndoorPlant01"                }},
            {   631, new EpbBlockType(){Id =   631, Name = "IndoorPlant03"                , Category = "Deco Blocks"                  , Ref = "IndoorPlant01"                }},
            {   632, new EpbBlockType(){Id =   632, Name = "CockpitSV02"                  , Category = "Devices"                      , Ref = ""                             }},
            {   633, new EpbBlockType(){Id =   633, Name = "CockpitSV05"                  , Category = "Devices"                      , Ref = "CockpitSV02"                  }},
            {   635, new EpbBlockType(){Id =   635, Name = "ConsoleSmallMS01"             , Category = "Deco Blocks"                  , Ref = ""                             }},
            {   636, new EpbBlockType(){Id =   636, Name = "ConsoleLargeMS01"             , Category = "Deco Blocks"                  , Ref = ""                             }},
            {   637, new EpbBlockType(){Id =   637, Name = "ConsoleLargeMS02"             , Category = "Deco Blocks"                  , Ref = "ConsoleLargeMS01"             }},
            {   638, new EpbBlockType(){Id =   638, Name = "ConsoleMapMS01"               , Category = "Deco Blocks"                  , Ref = "ConsoleLargeMS01"             }},
            {   646, new EpbBlockType(){Id =   646, Name = "WeaponMS01"                   , Category = "Weapons/Items"                , Ref = ""                             }},
            {   647, new EpbBlockType(){Id =   647, Name = "WeaponMS02"                   , Category = "Weapons/Items"                , Ref = ""                             }},
            {   648, new EpbBlockType(){Id =   648, Name = "TurretGVMinigun"              , Category = "Weapons/Items"                , Ref = ""                             }},
            {   649, new EpbBlockType(){Id =   649, Name = "TurretGVRocket"               , Category = "Weapons/Items"                , Ref = ""                             }},
            {   650, new EpbBlockType(){Id =   650, Name = "TurretGVPlasma"               , Category = "Weapons/Items"                , Ref = ""                             }},
            {   651, new EpbBlockType(){Id =   651, Name = "BunkBed"                      , Category = "Furnishings (Deco)"           , Ref = "DecoTemplate"                 }},
            {   652, new EpbBlockType(){Id =   652, Name = "LightWork02"                  , Category = "Devices"                      , Ref = ""                             }},
            {   653, new EpbBlockType(){Id =   653, Name = "Flare"                        , Category = "Devices"                      , Ref = ""                             }},
            {   658, new EpbBlockType(){Id =   658, Name = "EntitySpawner1"               , Category = "Devices"                      , Ref = ""                             }},
            {   668, new EpbBlockType(){Id =   668, Name = "EntitySpawnerPlateThin"       , Category = "Devices"                      , Ref = "EntitySpawner1"               }},
            {   669, new EpbBlockType(){Id =   669, Name = "SawAttachment"                , Category = "Weapons/Items"                , Ref = ""                             }},
            {   670, new EpbBlockType(){Id =   670, Name = "CockpitSV03"                  , Category = "Devices"                      , Ref = "CockpitSV02"                  }},
            {   671, new EpbBlockType(){Id =   671, Name = "CockpitSV07"                  , Category = "Devices"                      , Ref = "CockpitSV02"                  }},
            {   672, new EpbBlockType(){Id =   672, Name = "StairsWedge"                  , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   673, new EpbBlockType(){Id =   673, Name = "StairsWedgeLong"              , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   676, new EpbBlockType(){Id =   676, Name = "WalkwaySlope"                 , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   681, new EpbBlockType(){Id =   681, Name = "RailingL"                     , Category = "BuildingBlocks"               , Ref = "RailingDiagonal"              }},
            {   682, new EpbBlockType(){Id =   682, Name = "RailingRound"                 , Category = "BuildingBlocks"               , Ref = "RailingDiagonal"              }},
            {   683, new EpbBlockType(){Id =   683, Name = "DrillAttachment"              , Category = "Weapons/Items"                , Ref = ""                             }},
            {   684, new EpbBlockType(){Id =   684, Name = "TurretGVDrill"                , Category = "Weapons/Items"                , Ref = "TurretDrillTemplate"          }},
            {   685, new EpbBlockType(){Id =   685, Name = "TurretMSDrill"                , Category = "Weapons/Items"                , Ref = "TurretDrillTemplate"          }},
            {   686, new EpbBlockType(){Id =   686, Name = "ContainerPersonal"            , Category = "Devices"                      , Ref = ""                             }},
            {   688, new EpbBlockType(){Id =   688, Name = "CockpitSV07New"               , Category = "Devices"                      , Ref = "CockpitSV02"                  }},
            {   689, new EpbBlockType(){Id =   689, Name = "CockpitSV05New"               , Category = "Devices"                      , Ref = "CockpitSV02"                  }},
            {   690, new EpbBlockType(){Id =   690, Name = "CockpitSV02New"               , Category = "Devices"                      , Ref = "CockpitSV02"                  }},
            {   691, new EpbBlockType(){Id =   691, Name = "RailingSlopeLeft"             , Category = "BuildingBlocks"               , Ref = "RailingDiagonal"              }},
            {   692, new EpbBlockType(){Id =   692, Name = "RailingSlopeRight"            , Category = "BuildingBlocks"               , Ref = "RailingDiagonal"              }},
            {   694, new EpbBlockType(){Id =   694, Name = "ThrusterJetRound3x7x3"        , Category = "Devices"                      , Ref = ""                             }},
            {   695, new EpbBlockType(){Id =   695, Name = "ThrusterJetRound3x10x3"       , Category = "Devices"                      , Ref = "ThrusterJetRound3x7x3"        }},
            {   696, new EpbBlockType(){Id =   696, Name = "ThrusterJetRound3x13x3"       , Category = "Devices"                      , Ref = "ThrusterJetRound3x7x3"        }},
            {   697, new EpbBlockType(){Id =   697, Name = "ThrusterJetRound1x3x1"        , Category = "Devices"                      , Ref = "ThrusterJetRound3x7x3"        }},
            {   698, new EpbBlockType(){Id =   698, Name = "ThrusterJetRound2x5x2"        , Category = "Devices"                      , Ref = "ThrusterJetRound3x7x3"        }},
            {   700, new EpbBlockType(){Id =   700, Name = "TurretBaseFlak"               , Category = "Weapons/Items"                , Ref = ""                             }},
            {   701, new EpbBlockType(){Id =   701, Name = "TurretBasePlasma"             , Category = "Weapons/Items"                , Ref = ""                             }},
            {   702, new EpbBlockType(){Id =   702, Name = "TurretMSPlasma"               , Category = "Weapons/Items"                , Ref = ""                             }},
            {   706, new EpbBlockType(){Id =   706, Name = "OxygenHydrogenGenerator"      , Category = "Devices"                      , Ref = "OxygenGenerator"              }},
            {   711, new EpbBlockType(){Id =   711, Name = "ConstructorSurvival"          , Category = "Devices"                      , Ref = ""                             }},
            {   712, new EpbBlockType(){Id =   712, Name = "PassengerSeatSV"              , Category = "Devices"                      , Ref = ""                             }},
            {   714, new EpbBlockType(){Id =   714, Name = "ConstructorT2"                , Category = "Devices"                      , Ref = ""                             }},
            {   715, new EpbBlockType(){Id =   715, Name = "PassengerSeat2SV"             , Category = "Devices"                      , Ref = ""                             }},
            {   716, new EpbBlockType(){Id =   716, Name = "TurretGVArtillery"            , Category = "Weapons/Items"                , Ref = ""                             }},
            {   717, new EpbBlockType(){Id =   717, Name = "OxygenTankSmallMS"            , Category = "Devices"                      , Ref = ""                             }},
            {   720, new EpbBlockType(){Id =   720, Name = "WarpDrive"                    , Category = "Devices"                      , Ref = ""                             }},
            {   721, new EpbBlockType(){Id =   721, Name = "OxygenStationSV"              , Category = "Devices"                      , Ref = ""                             }},
            {   722, new EpbBlockType(){Id =   722, Name = "LandinggearShort"             , Category = "Devices"                      , Ref = ""                             }},
            {   723, new EpbBlockType(){Id =   723, Name = "LandinggearMSLight"           , Category = "Devices"                      , Ref = "LandinggearMSHeavy"           }},
            {   724, new EpbBlockType(){Id =   724, Name = "ContainerAmmoLarge"           , Category = "Devices"                      , Ref = ""                             }},
            {   727, new EpbBlockType(){Id =   727, Name = "ConsoleLargeMS01a"            , Category = "Deco Blocks"                  , Ref = "ConsoleLargeMS01"             }},
            {   728, new EpbBlockType(){Id =   728, Name = "ContainerAmmoSmall"           , Category = "Devices"                      , Ref = "ContainerAmmoLarge"           }},
            {   730, new EpbBlockType(){Id =   730, Name = "DockingPad"                   , Category = "Devices"                      , Ref = "LandinggearShort"             }},
            {   732, new EpbBlockType(){Id =   732, Name = "ContainerHarvest"             , Category = "Devices"                      , Ref = ""                             }},
            {   768, new EpbBlockType(){Id =   768, Name = "ThrusterGVRoundSlant"         , Category = "Devices"                      , Ref = "ThrusterGVRoundNormal"        }},
            {   769, new EpbBlockType(){Id =   769, Name = "TurretMSArtillery"            , Category = "Weapons/Items"                , Ref = "TurretMSArtilleryRetract"     }},
            {   770, new EpbBlockType(){Id =   770, Name = "Window_v1x1"                  , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   771, new EpbBlockType(){Id =   771, Name = "Window_s1x1"                  , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {   772, new EpbBlockType(){Id =   772, Name = "ThrusterMSRoundBlocks"        , Category = "Devices"                      , Ref = ""                             }},
            {   778, new EpbBlockType(){Id =   778, Name = "ThrusterMSRound2x2Blocks"     , Category = "Devices"                      , Ref = ""                             }},
            {   779, new EpbBlockType(){Id =   779, Name = "LandinggearSingle"            , Category = "Devices"                      , Ref = ""                             }},
            {   780, new EpbBlockType(){Id =   780, Name = "LandinggearDouble"            , Category = "Devices"                      , Ref = "LandinggearSingle"            }},
            {   781, new EpbBlockType(){Id =   781, Name = "CloneChamber"                 , Category = "Devices"                      , Ref = ""                             }},
            {   795, new EpbBlockType(){Id =   795, Name = "Window_v1x1Inv"               , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {   796, new EpbBlockType(){Id =   796, Name = "Window_v1x2"                  , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {   797, new EpbBlockType(){Id =   797, Name = "Window_v1x2Inv"               , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {   798, new EpbBlockType(){Id =   798, Name = "Window_v2x2"                  , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {   799, new EpbBlockType(){Id =   799, Name = "Window_v2x2Inv"               , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {   800, new EpbBlockType(){Id =   800, Name = "Window_s1x1Inv"               , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {   801, new EpbBlockType(){Id =   801, Name = "Window_s1x2"                  , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {   802, new EpbBlockType(){Id =   802, Name = "Window_s1x2Inv"               , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {   803, new EpbBlockType(){Id =   803, Name = "Window_sd1x1"                 , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {   804, new EpbBlockType(){Id =   804, Name = "Window_sd1x1Inv"              , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {   805, new EpbBlockType(){Id =   805, Name = "Window_sd1x2"                 , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {   806, new EpbBlockType(){Id =   806, Name = "Window_sd1x2Inv"              , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {   807, new EpbBlockType(){Id =   807, Name = "Window_c1x1"                  , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {   808, new EpbBlockType(){Id =   808, Name = "Window_c1x1Inv"               , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {   809, new EpbBlockType(){Id =   809, Name = "Window_c1x2"                  , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {   810, new EpbBlockType(){Id =   810, Name = "Window_c1x2Inv"               , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {   811, new EpbBlockType(){Id =   811, Name = "Window_cr1x1"                 , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {   812, new EpbBlockType(){Id =   812, Name = "Window_cr1x1Inv"              , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {   813, new EpbBlockType(){Id =   813, Name = "Window_crc1x1"                , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {   814, new EpbBlockType(){Id =   814, Name = "Window_crc1x1Inv"             , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {   815, new EpbBlockType(){Id =   815, Name = "Window_crsd1x1"               , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {   816, new EpbBlockType(){Id =   816, Name = "Window_crsd1x1Inv"            , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {   817, new EpbBlockType(){Id =   817, Name = "Window_sd1x2V2"               , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {   818, new EpbBlockType(){Id =   818, Name = "Window_sd1x2V2Inv"            , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {   819, new EpbBlockType(){Id =   819, Name = "RampTemplate"                 , Category = "Devices"                      , Ref = ""                             }},
            {   835, new EpbBlockType(){Id =   835, Name = "ThrusterMSRound3x3Blocks"     , Category = "Devices"                      , Ref = ""                             }},
            {   836, new EpbBlockType(){Id =   836, Name = "WindowSmallBlocks"            , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   837, new EpbBlockType(){Id =   837, Name = "TrussSmallBlocks"             , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   838, new EpbBlockType(){Id =   838, Name = "WalkwayBlocks"                , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   839, new EpbBlockType(){Id =   839, Name = "StairsBlocks"                 , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   884, new EpbBlockType(){Id =   884, Name = "WalkwayVertNew"               , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   885, new EpbBlockType(){Id =   885, Name = "WalkwaySlopeNew"              , Category = "BuildingBlocks"               , Ref = "WalkwayVertNew"               }},
            {   927, new EpbBlockType(){Id =   927, Name = "DecoBlocks"                   , Category = "Deco Blocks"                  , Ref = ""                             }},
            {   928, new EpbBlockType(){Id =   928, Name = "ConsoleBlocks"                , Category = "Deco Blocks"                  , Ref = ""                             }},
            {   929, new EpbBlockType(){Id =   929, Name = "IndoorPlants"                 , Category = "Deco Blocks"                  , Ref = ""                             }},
            {   934, new EpbBlockType(){Id =   934, Name = "RCSBlockMS_T2"                , Category = "Devices"                      , Ref = ""                             }},
            {   950, new EpbBlockType(){Id =   950, Name = "HoloScreen01"                 , Category = "Deco Blocks"                  , Ref = ""                             }},
            {   951, new EpbBlockType(){Id =   951, Name = "HoloScreen02"                 , Category = "Deco Blocks"                  , Ref = "HoloScreen01"                 }},
            {   952, new EpbBlockType(){Id =   952, Name = "HoloScreen03"                 , Category = "Deco Blocks"                  , Ref = "HoloScreen01"                 }},
            {   953, new EpbBlockType(){Id =   953, Name = "HoloScreen04"                 , Category = "Deco Blocks"                  , Ref = "HoloScreen01"                 }},
            {   954, new EpbBlockType(){Id =   954, Name = "HoloScreen05"                 , Category = "Deco Blocks"                  , Ref = "HoloScreen01"                 }},
            {   960, new EpbBlockType(){Id =   960, Name = "ConstructorT1V2"              , Category = "Devices"                      , Ref = ""                             }},
            {   962, new EpbBlockType(){Id =   962, Name = "FoodProcessorV2"              , Category = "Devices"                      , Ref = ""                             }},
            {   963, new EpbBlockType(){Id =   963, Name = "CockpitSV01"                  , Category = "Devices"                      , Ref = "CockpitSV02"                  }},
            {   964, new EpbBlockType(){Id =   964, Name = "OxygenGeneratorSmall"         , Category = "Devices"                      , Ref = ""                             }},
            {   965, new EpbBlockType(){Id =   965, Name = "DoorArmored"                  , Category = "Devices"                      , Ref = ""                             }},
            {   966, new EpbBlockType(){Id =   966, Name = "Window_v1x1Thick"             , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   967, new EpbBlockType(){Id =   967, Name = "Window_v1x2Thick"             , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {   968, new EpbBlockType(){Id =   968, Name = "Window_v2x2Thick"             , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {   969, new EpbBlockType(){Id =   969, Name = "WindowVertShutterArmored"     , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   970, new EpbBlockType(){Id =   970, Name = "WindowSlopedShutterArmored"   , Category = "BuildingBlocks"               , Ref = "WindowVertShutterArmored"     }},
            {   971, new EpbBlockType(){Id =   971, Name = "WindowSloped2ShutterArmored"  , Category = "BuildingBlocks"               , Ref = "WindowVertShutterArmored"     }},
            {   972, new EpbBlockType(){Id =   972, Name = "WindowVertShutterTransArmored", Category = "BuildingBlocks"               , Ref = "WindowVertShutterArmored"     }},
            {   973, new EpbBlockType(){Id =   973, Name = "WindowSlopedShutterTransArmored", Category = "BuildingBlocks"               , Ref = "WindowVertShutterArmored"     }},
            {   974, new EpbBlockType(){Id =   974, Name = "WindowArmoredSmallBlocks"     , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   975, new EpbBlockType(){Id =   975, Name = "HangarDoor10x5"               , Category = "Devices"                      , Ref = ""                             }},
            {   976, new EpbBlockType(){Id =   976, Name = "WindowShutterSmallBlocks"     , Category = "BuildingBlocks"               , Ref = ""                             }},
            {   977, new EpbBlockType(){Id =   977, Name = "Window_s1x1Thick"             , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {   978, new EpbBlockType(){Id =   978, Name = "Window_s1x2Thick"             , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {   979, new EpbBlockType(){Id =   979, Name = "Window_sd1x1Thick"            , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {   980, new EpbBlockType(){Id =   980, Name = "Window_sd1x2Thick"            , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {   981, new EpbBlockType(){Id =   981, Name = "Window_c1x1Thick"             , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {   982, new EpbBlockType(){Id =   982, Name = "Window_c1x2Thick"             , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {   983, new EpbBlockType(){Id =   983, Name = "Window_cr1x1Thick"            , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {   984, new EpbBlockType(){Id =   984, Name = "Window_crc1x1Thick"           , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {   985, new EpbBlockType(){Id =   985, Name = "Window_crsd1x1Thick"          , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {   986, new EpbBlockType(){Id =   986, Name = "Window_sd1x2V2Thick"          , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {   987, new EpbBlockType(){Id =   987, Name = "HangarDoor14x7"               , Category = "Devices"                      , Ref = "HangarDoor10x5"               }},
            {   988, new EpbBlockType(){Id =   988, Name = "HangarDoor6x3"                , Category = "Devices"                      , Ref = "HangarDoor10x5"               }},
            {   989, new EpbBlockType(){Id =   989, Name = "Window_v1x1ThickInv"          , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {   990, new EpbBlockType(){Id =   990, Name = "Window_v1x2ThickInv"          , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {   991, new EpbBlockType(){Id =   991, Name = "Window_v2x2ThickInv"          , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {   992, new EpbBlockType(){Id =   992, Name = "Window_s1x1ThickInv"          , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {   993, new EpbBlockType(){Id =   993, Name = "Window_s1x2ThickInv"          , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {   994, new EpbBlockType(){Id =   994, Name = "Window_sd1x1ThickInv"         , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {   995, new EpbBlockType(){Id =   995, Name = "Window_sd1x2ThickInv"         , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {   996, new EpbBlockType(){Id =   996, Name = "Window_c1x1ThickInv"          , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {   997, new EpbBlockType(){Id =   997, Name = "Window_c1x2ThickInv"          , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {   998, new EpbBlockType(){Id =   998, Name = "Window_cr1x1ThickInv"         , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {   999, new EpbBlockType(){Id =   999, Name = "Window_crc1x1ThickInv"        , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {  1000, new EpbBlockType(){Id =  1000, Name = "Window_crsd1x1ThickInv"       , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {  1001, new EpbBlockType(){Id =  1001, Name = "Window_sd1x2V2ThickInv"       , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {  1002, new EpbBlockType(){Id =  1002, Name = "DoorBlocks"                   , Category = "Devices"                      , Ref = ""                             }},
            {  1003, new EpbBlockType(){Id =  1003, Name = "DoorInterior01"               , Category = "Devices"                      , Ref = ""                             }},
            {  1004, new EpbBlockType(){Id =  1004, Name = "DoorInterior02"               , Category = "Devices"                      , Ref = "DoorInterior01"               }},
            {  1005, new EpbBlockType(){Id =  1005, Name = "HangarDoor5x3"                , Category = "Devices"                      , Ref = "HangarDoor10x5"               }},
            {  1006, new EpbBlockType(){Id =  1006, Name = "HangarDoor9x5"                , Category = "Devices"                      , Ref = "HangarDoor10x5"               }},
            {  1007, new EpbBlockType(){Id =  1007, Name = "HangarDoor13x7"               , Category = "Devices"                      , Ref = "HangarDoor10x5"               }},
            {  1008, new EpbBlockType(){Id =  1008, Name = "HangarDoorBlocks"             , Category = "Devices"                      , Ref = ""                             }},
            {  1009, new EpbBlockType(){Id =  1009, Name = "CockpitOpenSV"                , Category = "Devices"                      , Ref = ""                             }},
            {  1011, new EpbBlockType(){Id =  1011, Name = "ShutterDoor1x1"               , Category = "Devices"                      , Ref = ""                             }},
            {  1012, new EpbBlockType(){Id =  1012, Name = "ShutterDoor2x2"               , Category = "Devices"                      , Ref = "ShutterDoor1x1"               }},
            {  1013, new EpbBlockType(){Id =  1013, Name = "ShutterDoor3x3"               , Category = "Devices"                      , Ref = "ShutterDoor1x1"               }},
            {  1014, new EpbBlockType(){Id =  1014, Name = "ShutterDoor4x4"               , Category = "Devices"                      , Ref = "ShutterDoor1x1"               }},
            {  1015, new EpbBlockType(){Id =  1015, Name = "ShutterDoor5x5"               , Category = "Devices"                      , Ref = "ShutterDoor1x1"               }},
            {  1016, new EpbBlockType(){Id =  1016, Name = "ShutterDoorLargeBlocks"       , Category = "Devices"                      , Ref = ""                             }},
            {  1017, new EpbBlockType(){Id =  1017, Name = "ShutterDoor1x1SV"             , Category = "Devices"                      , Ref = ""                             }},
            {  1018, new EpbBlockType(){Id =  1018, Name = "ShutterDoor2x2SV"             , Category = "Devices"                      , Ref = "ShutterDoor1x1SV"             }},
            {  1019, new EpbBlockType(){Id =  1019, Name = "ShutterDoor3x3SV"             , Category = "Devices"                      , Ref = "ShutterDoor1x1SV"             }},
            {  1020, new EpbBlockType(){Id =  1020, Name = "ShutterDoorSmallBlocks"       , Category = "Devices"                      , Ref = ""                             }},
            {  1021, new EpbBlockType(){Id =  1021, Name = "ShutterDoor3x4SV"             , Category = "Devices"                      , Ref = "ShutterDoor1x1SV"             }},
            {  1022, new EpbBlockType(){Id =  1022, Name = "Ramp3x1x1"                    , Category = "Devices"                      , Ref = "RampTemplate"                 }},
            {  1023, new EpbBlockType(){Id =  1023, Name = "Ramp3x2x1"                    , Category = "Devices"                      , Ref = "RampTemplate"                 }},
            {  1024, new EpbBlockType(){Id =  1024, Name = "Ramp3x3x1"                    , Category = "Devices"                      , Ref = "RampTemplate"                 }},
            {  1025, new EpbBlockType(){Id =  1025, Name = "Ramp3x4x2"                    , Category = "Devices"                      , Ref = "RampTemplate"                 }},
            {  1026, new EpbBlockType(){Id =  1026, Name = "Ramp3x5x3"                    , Category = "Devices"                      , Ref = "RampTemplate"                 }},
            {  1027, new EpbBlockType(){Id =  1027, Name = "Ramp1x1x1"                    , Category = "Devices"                      , Ref = "RampTemplate"                 }},
            {  1028, new EpbBlockType(){Id =  1028, Name = "Ramp1x2x1"                    , Category = "Devices"                      , Ref = "RampTemplate"                 }},
            {  1029, new EpbBlockType(){Id =  1029, Name = "Ramp1x3x1"                    , Category = "Devices"                      , Ref = "RampTemplate"                 }},
            {  1030, new EpbBlockType(){Id =  1030, Name = "Ramp1x4x2"                    , Category = "Devices"                      , Ref = "RampTemplate"                 }},
            {  1031, new EpbBlockType(){Id =  1031, Name = "RampBlocks"                   , Category = "Devices"                      , Ref = ""                             }},
            {  1034, new EpbBlockType(){Id =  1034, Name = "GeneratorMST2"                , Category = "Devices"                      , Ref = "GeneratorMS"                  }},
            {  1035, new EpbBlockType(){Id =  1035, Name = "FuelTankMSLargeT2"            , Category = "Devices"                      , Ref = "FuelTankMSLarge"              }},
            {  1036, new EpbBlockType(){Id =  1036, Name = "ShutterDoor1x2"               , Category = "Devices"                      , Ref = "ShutterDoor1x1"               }},
            {  1037, new EpbBlockType(){Id =  1037, Name = "ShutterDoor2x3"               , Category = "Devices"                      , Ref = "ShutterDoor1x1"               }},
            {  1064, new EpbBlockType(){Id =  1064, Name = "LandinggearHeavySV"           , Category = "Devices"                      , Ref = "LandinggearSV"                }},
            {  1072, new EpbBlockType(){Id =  1072, Name = "ScifiBed"                     , Category = "Furnishings (Deco)"           , Ref = "DecoTemplate"                 }},
            {  1073, new EpbBlockType(){Id =  1073, Name = "ScifiLargeSofa"               , Category = "Furnishings (Deco)"           , Ref = "DecoTemplate"                 }},
            {  1074, new EpbBlockType(){Id =  1074, Name = "ScifiNightstand"              , Category = "Furnishings (Deco)"           , Ref = "DecoTemplate"                 }},
            {  1075, new EpbBlockType(){Id =  1075, Name = "TrussLargeBlocks"             , Category = "BuildingBlocks"               , Ref = ""                             }},
            {  1076, new EpbBlockType(){Id =  1076, Name = "ScifiSofa"                    , Category = "Furnishings (Deco)"           , Ref = "DecoTemplate"                 }},
            {  1077, new EpbBlockType(){Id =  1077, Name = "ScifiStorage"                 , Category = "Furnishings (Deco)"           , Ref = "DecoTemplate"                 }},
            {  1078, new EpbBlockType(){Id =  1078, Name = "ScifiTable"                   , Category = "Furnishings (Deco)"           , Ref = "DecoTemplate"                 }},
            {  1079, new EpbBlockType(){Id =  1079, Name = "ScifiShower"                  , Category = "Furnishings (Deco)"           , Ref = "DecoTemplate"                 }},
            {  1080, new EpbBlockType(){Id =  1080, Name = "ScifiPlant"                   , Category = "Deco Blocks"                  , Ref = "IndoorPlant01"                }},
            {  1081, new EpbBlockType(){Id =  1081, Name = "ScifiContainer1"              , Category = "Devices"                      , Ref = "ContainerMS01"                }},
            {  1082, new EpbBlockType(){Id =  1082, Name = "ScifiContainer2"              , Category = "Devices"                      , Ref = "ContainerMS01"                }},
            {  1083, new EpbBlockType(){Id =  1083, Name = "ScifiContainerEnergy"         , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1084, new EpbBlockType(){Id =  1084, Name = "ScifiContainerPower"          , Category = "Devices"                      , Ref = "ContainerMS01"                }},
            {  1085, new EpbBlockType(){Id =  1085, Name = "ScifiChair"                   , Category = "Furnishings (Deco)"           , Ref = "DecoTemplate"                 }},
            {  1086, new EpbBlockType(){Id =  1086, Name = "ScifiTableV2"                 , Category = "Furnishings (Deco)"           , Ref = "DecoTemplate"                 }},
            {  1087, new EpbBlockType(){Id =  1087, Name = "ScifiComputerTable"           , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1088, new EpbBlockType(){Id =  1088, Name = "ScifiMediaCenter"             , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1089, new EpbBlockType(){Id =  1089, Name = "HangarDoor7x5"                , Category = "Devices"                      , Ref = "HangarDoor10x5"               }},
            {  1090, new EpbBlockType(){Id =  1090, Name = "HangarDoor10x9"               , Category = "Devices"                      , Ref = "HangarDoor10x5"               }},
            {  1091, new EpbBlockType(){Id =  1091, Name = "CockpitMS03"                  , Category = "Devices"                      , Ref = "CockpitMS01"                  }},
            {  1092, new EpbBlockType(){Id =  1092, Name = "CockpitOpen2SV"               , Category = "Devices"                      , Ref = ""                             }},
            {  1093, new EpbBlockType(){Id =  1093, Name = "CockpitBlocksSV"              , Category = "Devices"                      , Ref = ""                             }},
            {  1094, new EpbBlockType(){Id =  1094, Name = "CockpitSV04"                  , Category = "Devices"                      , Ref = "CockpitSV02"                  }},
            {  1095, new EpbBlockType(){Id =  1095, Name = "LCDScreenBlocks"              , Category = "Devices"                      , Ref = ""                             }},
            {  1096, new EpbBlockType(){Id =  1096, Name = "LCDNoFrame1x1"                , Category = "Devices"                      , Ref = ""                             }},
            {  1097, new EpbBlockType(){Id =  1097, Name = "LCDFrame1x1"                  , Category = "Devices"                      , Ref = "LCDNoFrame1x1"                }},
            {  1098, new EpbBlockType(){Id =  1098, Name = "LCDNoFrame1x2"                , Category = "Devices"                      , Ref = "LCDNoFrame1x1"                }},
            {  1099, new EpbBlockType(){Id =  1099, Name = "LCDFrame1x2"                  , Category = "Devices"                      , Ref = "LCDNoFrame1x1"                }},
            {  1100, new EpbBlockType(){Id =  1100, Name = "LCDNoFrame05x1"               , Category = "Devices"                      , Ref = "LCDNoFrame1x1"                }},
            {  1101, new EpbBlockType(){Id =  1101, Name = "LCDNoFrame02x1"               , Category = "Devices"                      , Ref = "LCDNoFrame1x1"                }},
            {  1102, new EpbBlockType(){Id =  1102, Name = "LCDNoFrame05x05"              , Category = "Devices"                      , Ref = "LCDNoFrame1x1"                }},
            {  1103, new EpbBlockType(){Id =  1103, Name = "LCDNoFrame02x05"              , Category = "Devices"                      , Ref = "LCDNoFrame1x1"                }},
            {  1104, new EpbBlockType(){Id =  1104, Name = "TurretGVTool"                 , Category = "Weapons/Items"                , Ref = "TurretDrillTemplate"          }},
            {  1105, new EpbBlockType(){Id =  1105, Name = "TurretMSTool"                 , Category = "Weapons/Items"                , Ref = "TurretDrillTemplate"          }},
            {  1106, new EpbBlockType(){Id =  1106, Name = "ThrusterGVRoundArmored"       , Category = "Devices"                      , Ref = "ThrusterGVRoundNormal"        }},
            {  1107, new EpbBlockType(){Id =  1107, Name = "ThrusterGVRoundBlocks"        , Category = "Devices"                      , Ref = ""                             }},
            {  1108, new EpbBlockType(){Id =  1108, Name = "AutoMiningDeviceT1"           , Category = "Devices"                      , Ref = ""                             }},
            {  1109, new EpbBlockType(){Id =  1109, Name = "AutoMiningDeviceT2"           , Category = "Devices"                      , Ref = "AutoMiningDeviceT1"           }},
            {  1110, new EpbBlockType(){Id =  1110, Name = "AutoMiningDeviceT3"           , Category = "Devices"                      , Ref = "AutoMiningDeviceT1"           }},
            {  1111, new EpbBlockType(){Id =  1111, Name = "RepairBayBA"                  , Category = "Devices"                      , Ref = ""                             }},
            {  1112, new EpbBlockType(){Id =  1112, Name = "DoorArmoredBlocks"            , Category = "Devices"                      , Ref = ""                             }},
            {  1113, new EpbBlockType(){Id =  1113, Name = "DoorVertical"                 , Category = "Devices"                      , Ref = "DoorMS01"                     }},
            {  1114, new EpbBlockType(){Id =  1114, Name = "DoorVerticalGlass"            , Category = "Devices"                      , Ref = "DoorInterior01"               }},
            {  1115, new EpbBlockType(){Id =  1115, Name = "DoorVerticalArmored"          , Category = "Devices"                      , Ref = "DoorArmored"                  }},
            {  1116, new EpbBlockType(){Id =  1116, Name = "LandinggearSingleShort"       , Category = "Devices"                      , Ref = "LandinggearSV"                }},
            {  1117, new EpbBlockType(){Id =  1117, Name = "LandinggearDoubleShort"       , Category = "Devices"                      , Ref = "LandinggearSV"                }},
            {  1118, new EpbBlockType(){Id =  1118, Name = "LandinggearBlocksSV"          , Category = "Devices"                      , Ref = ""                             }},
            {  1119, new EpbBlockType(){Id =  1119, Name = "LandinggearBlocksHeavySV"     , Category = "Devices"                      , Ref = "LandinggearBlocksSV"          }},
            {  1120, new EpbBlockType(){Id =  1120, Name = "LandinggearBlocksCV"          , Category = "Devices"                      , Ref = ""                             }},
            {  1121, new EpbBlockType(){Id =  1121, Name = "LandinggearSingleCV"          , Category = "Devices"                      , Ref = "LandinggearMSHeavy"           }},
            {  1122, new EpbBlockType(){Id =  1122, Name = "LandinggearSingleShortCV"     , Category = "Devices"                      , Ref = "LandinggearMSHeavy"           }},
            {  1123, new EpbBlockType(){Id =  1123, Name = "LandinggearDoubleCV"          , Category = "Devices"                      , Ref = "LandinggearMSHeavy"           }},
            {  1124, new EpbBlockType(){Id =  1124, Name = "LandinggearDoubleShortCV"     , Category = "Devices"                      , Ref = "LandinggearMSHeavy"           }},
            {  1125, new EpbBlockType(){Id =  1125, Name = "StairShapes"                  , Category = "BuildingBlocks"               , Ref = ""                             }},
            {  1126, new EpbBlockType(){Id =  1126, Name = "StairShapesLong"              , Category = "BuildingBlocks"               , Ref = ""                             }},
            {  1127, new EpbBlockType(){Id =  1127, Name = "HoverEngineSmall"             , Category = "Devices"                      , Ref = ""                             }},
            {  1128, new EpbBlockType(){Id =  1128, Name = "WindowLargeBlocks"            , Category = "BuildingBlocks"               , Ref = ""                             }},
            {  1129, new EpbBlockType(){Id =  1129, Name = "WindowArmoredLargeBlocks"     , Category = "BuildingBlocks"               , Ref = ""                             }},
            {  1130, new EpbBlockType(){Id =  1130, Name = "HoverEngineLarge"             , Category = "Devices"                      , Ref = "HoverEngineSmall"             }},
            {  1131, new EpbBlockType(){Id =  1131, Name = "RepairBayCV"                  , Category = "Devices"                      , Ref = "RepairBayBA"                  }},
            {  1132, new EpbBlockType(){Id =  1132, Name = "Furnace"                      , Category = "Devices"                      , Ref = ""                             }},
            {  1133, new EpbBlockType(){Id =  1133, Name = "TradingStation"               , Category = "Devices"                      , Ref = ""                             }},
            {  1134, new EpbBlockType(){Id =  1134, Name = "ATM"                          , Category = "Devices"                      , Ref = ""                             }},
            {  1135, new EpbBlockType(){Id =  1135, Name = "WingBlocks"                   , Category = "BuildingBlocks"               , Ref = ""                             }},
            {  1139, new EpbBlockType(){Id =  1139, Name = "Wing6x9a"                     , Category = "BuildingBlocks"               , Ref = ""                             }},
            {  1140, new EpbBlockType(){Id =  1140, Name = "Wing6x5a"                     , Category = "BuildingBlocks"               , Ref = "Wing6x9a"                     }},
            {  1141, new EpbBlockType(){Id =  1141, Name = "Wing12x9a"                    , Category = "BuildingBlocks"               , Ref = "Wing6x9a"                     }},
            {  1142, new EpbBlockType(){Id =  1142, Name = "TurretMSPulseLaser"           , Category = "Weapons/Items"                , Ref = ""                             }},
            {  1143, new EpbBlockType(){Id =  1143, Name = "TurretBaseCannon"             , Category = "Weapons/Items"                , Ref = ""                             }},
            {  1144, new EpbBlockType(){Id =  1144, Name = "TurretBaseRocket"             , Category = "Weapons/Items"                , Ref = ""                             }},
            {  1145, new EpbBlockType(){Id =  1145, Name = "TurretMSCannon"               , Category = "Weapons/Items"                , Ref = ""                             }},
            {  1146, new EpbBlockType(){Id =  1146, Name = "TurretMSFlak"                 , Category = "Weapons/Items"                , Ref = ""                             }},
            {  1147, new EpbBlockType(){Id =  1147, Name = "TurretBaseMinigun"            , Category = "Weapons/Items"                , Ref = ""                             }},
            {  1148, new EpbBlockType(){Id =  1148, Name = "TurretBasePulseLaser"         , Category = "Weapons/Items"                , Ref = ""                             }},
            {  1149, new EpbBlockType(){Id =  1149, Name = "TurretBaseArtillery"          , Category = "Weapons/Items"                , Ref = ""                             }},
            {  1150, new EpbBlockType(){Id =  1150, Name = "Wing12x9b"                    , Category = "BuildingBlocks"               , Ref = "Wing6x9a"                     }},
            {  1151, new EpbBlockType(){Id =  1151, Name = "Wing12x9c"                    , Category = "BuildingBlocks"               , Ref = "Wing6x9a"                     }},
            {  1152, new EpbBlockType(){Id =  1152, Name = "Wing9x6a"                     , Category = "BuildingBlocks"               , Ref = "Wing6x9a"                     }},
            {  1153, new EpbBlockType(){Id =  1153, Name = "Wing9x6b"                     , Category = "BuildingBlocks"               , Ref = "Wing6x9a"                     }},
            {  1154, new EpbBlockType(){Id =  1154, Name = "Wing9x6c"                     , Category = "BuildingBlocks"               , Ref = "Wing6x9a"                     }},
            {  1155, new EpbBlockType(){Id =  1155, Name = "Wing6x9b"                     , Category = "BuildingBlocks"               , Ref = "Wing6x9a"                     }},
            {  1156, new EpbBlockType(){Id =  1156, Name = "Wing6x9c"                     , Category = "BuildingBlocks"               , Ref = "Wing6x9a"                     }},
            {  1157, new EpbBlockType(){Id =  1157, Name = "Wing6x5b"                     , Category = "BuildingBlocks"               , Ref = "Wing6x9a"                     }},
            {  1158, new EpbBlockType(){Id =  1158, Name = "Wing6x5c"                     , Category = "BuildingBlocks"               , Ref = "Wing6x9a"                     }},
            {  1159, new EpbBlockType(){Id =  1159, Name = "Wing6x5d"                     , Category = "BuildingBlocks"               , Ref = "Wing6x9a"                     }},
            {  1160, new EpbBlockType(){Id =  1160, Name = "Wing6x5e"                     , Category = "BuildingBlocks"               , Ref = "Wing6x9a"                     }},
            {  1161, new EpbBlockType(){Id =  1161, Name = "Wing6x9d"                     , Category = "BuildingBlocks"               , Ref = "Wing6x9a"                     }},
            {  1162, new EpbBlockType(){Id =  1162, Name = "Wing6x9e"                     , Category = "BuildingBlocks"               , Ref = "Wing6x9a"                     }},
            {  1163, new EpbBlockType(){Id =  1163, Name = "Wing12x9d"                    , Category = "BuildingBlocks"               , Ref = "Wing6x9a"                     }},
            {  1164, new EpbBlockType(){Id =  1164, Name = "Wing12x9e"                    , Category = "BuildingBlocks"               , Ref = "Wing6x9a"                     }},
            {  1165, new EpbBlockType(){Id =  1165, Name = "Wing9x6d"                     , Category = "BuildingBlocks"               , Ref = "Wing6x9a"                     }},
            {  1166, new EpbBlockType(){Id =  1166, Name = "Wing9x6e"                     , Category = "BuildingBlocks"               , Ref = "Wing6x9a"                     }},
            {  1183, new EpbBlockType(){Id =  1183, Name = "Window_3side1x1"              , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {  1184, new EpbBlockType(){Id =  1184, Name = "Window_3side1x1Inv"           , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {  1185, new EpbBlockType(){Id =  1185, Name = "Window_L1x1"                  , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {  1186, new EpbBlockType(){Id =  1186, Name = "Window_L1x1Inv"               , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {  1187, new EpbBlockType(){Id =  1187, Name = "Window_3side1x1Thick"         , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {  1188, new EpbBlockType(){Id =  1188, Name = "Window_3side1x1ThickInv"      , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {  1189, new EpbBlockType(){Id =  1189, Name = "Window_L1x1Thick"             , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {  1190, new EpbBlockType(){Id =  1190, Name = "Window_L1x1ThickInv"          , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {  1191, new EpbBlockType(){Id =  1191, Name = "RailingVertGlass"             , Category = "BuildingBlocks"               , Ref = ""                             }},
            {  1192, new EpbBlockType(){Id =  1192, Name = "RailingVertGlassInv"          , Category = "BuildingBlocks"               , Ref = "RailingVertGlass"             }},
            {  1193, new EpbBlockType(){Id =  1193, Name = "RailingRoundGlass"            , Category = "BuildingBlocks"               , Ref = "RailingVertGlass"             }},
            {  1194, new EpbBlockType(){Id =  1194, Name = "RailingRoundGlassInv"         , Category = "BuildingBlocks"               , Ref = "RailingVertGlass"             }},
            {  1195, new EpbBlockType(){Id =  1195, Name = "RailingLGlass"                , Category = "BuildingBlocks"               , Ref = "RailingVertGlass"             }},
            {  1196, new EpbBlockType(){Id =  1196, Name = "RailingLGlassInv"             , Category = "BuildingBlocks"               , Ref = "RailingVertGlass"             }},
            {  1197, new EpbBlockType(){Id =  1197, Name = "Window_crctw1x1"              , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {  1198, new EpbBlockType(){Id =  1198, Name = "Window_creA1x1"               , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {  1199, new EpbBlockType(){Id =  1199, Name = "Window_creB1x1"               , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {  1200, new EpbBlockType(){Id =  1200, Name = "Window_crl1x1"                , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {  1201, new EpbBlockType(){Id =  1201, Name = "Window_crse1x1"               , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {  1202, new EpbBlockType(){Id =  1202, Name = "Window_cc1x1"                 , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {  1203, new EpbBlockType(){Id =  1203, Name = "Window_crctw1x1Thick"         , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {  1204, new EpbBlockType(){Id =  1204, Name = "Window_creA1x1Thick"          , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {  1205, new EpbBlockType(){Id =  1205, Name = "Window_creB1x1Thick"          , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {  1206, new EpbBlockType(){Id =  1206, Name = "Window_crl1x1Thick"           , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {  1207, new EpbBlockType(){Id =  1207, Name = "Window_crse1x1Thick"          , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {  1208, new EpbBlockType(){Id =  1208, Name = "Window_cc1x1Thick"            , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {  1209, new EpbBlockType(){Id =  1209, Name = "Window_creA1x1Inv"            , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {  1210, new EpbBlockType(){Id =  1210, Name = "Window_crctw1x1Inv"           , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {  1211, new EpbBlockType(){Id =  1211, Name = "Window_creB1x1Inv"            , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {  1212, new EpbBlockType(){Id =  1212, Name = "Window_crl1x1Inv"             , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {  1213, new EpbBlockType(){Id =  1213, Name = "Window_crse1x1Inv"            , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {  1214, new EpbBlockType(){Id =  1214, Name = "Window_cc1x1Inv"              , Category = "BuildingBlocks"               , Ref = "Window_v1x1"                  }},
            {  1215, new EpbBlockType(){Id =  1215, Name = "Window_crctw1x1ThickInv"      , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {  1216, new EpbBlockType(){Id =  1216, Name = "Window_creA1x1ThickInv"       , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {  1217, new EpbBlockType(){Id =  1217, Name = "Window_creB1x1ThickInv"       , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {  1218, new EpbBlockType(){Id =  1218, Name = "Window_crl1x1ThickInv"        , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {  1219, new EpbBlockType(){Id =  1219, Name = "Window_crse1x1ThickInv"       , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {  1220, new EpbBlockType(){Id =  1220, Name = "Window_cc1x1ThickInv"         , Category = "BuildingBlocks"               , Ref = "Window_v1x1Thick"             }},
            {  1221, new EpbBlockType(){Id =  1221, Name = "RailingSlopeGlassRight"       , Category = "BuildingBlocks"               , Ref = "RailingVertGlass"             }},
            {  1222, new EpbBlockType(){Id =  1222, Name = "RailingSlopeGlassRightInv"    , Category = "BuildingBlocks"               , Ref = "RailingVertGlass"             }},
            {  1223, new EpbBlockType(){Id =  1223, Name = "RailingSlopeGlassLeft"        , Category = "BuildingBlocks"               , Ref = "RailingVertGlass"             }},
            {  1224, new EpbBlockType(){Id =  1224, Name = "RailingSlopeGlassLeftInv"     , Category = "BuildingBlocks"               , Ref = "RailingVertGlass"             }},
            {  1225, new EpbBlockType(){Id =  1225, Name = "RailingDiagonalGlass"         , Category = "BuildingBlocks"               , Ref = "RailingVertGlass"             }},
            {  1226, new EpbBlockType(){Id =  1226, Name = "RailingDiagonalGlassInv"      , Category = "BuildingBlocks"               , Ref = "RailingVertGlass"             }},
            {  1227, new EpbBlockType(){Id =  1227, Name = "LeverSV"                      , Category = "Devices"                      , Ref = ""                             }},
            {  1228, new EpbBlockType(){Id =  1228, Name = "LightBarrierSV"               , Category = "Devices"                      , Ref = ""                             }},
            {  1229, new EpbBlockType(){Id =  1229, Name = "MotionSensorSV"               , Category = "Devices"                      , Ref = ""                             }},
            {  1230, new EpbBlockType(){Id =  1230, Name = "SensorTriggerBlocksSV"        , Category = "Devices"                      , Ref = ""                             }},
            {  1231, new EpbBlockType(){Id =  1231, Name = "RepairBayBAT2"                , Category = "Devices"                      , Ref = "RepairBayBA"                  }},
            {  1232, new EpbBlockType(){Id =  1232, Name = "Closet"                       , Category = "Furnishings (Deco)"           , Ref = "DecoTemplate"                 }},
            {  1233, new EpbBlockType(){Id =  1233, Name = "DoorVerticalGlassArmored"     , Category = "Devices"                      , Ref = "DoorArmored"                  }},
            {  1234, new EpbBlockType(){Id =  1234, Name = "DoorInterior01Armored"        , Category = "Devices"                      , Ref = "DoorArmored"                  }},
            {  1235, new EpbBlockType(){Id =  1235, Name = "DoorInterior02Armored"        , Category = "Devices"                      , Ref = "DoorArmored"                  }},
            {  1236, new EpbBlockType(){Id =  1236, Name = "DrillAttachmentT2"            , Category = "Weapons/Items"                , Ref = "DrillAttachment"              }},
            {  1237, new EpbBlockType(){Id =  1237, Name = "CockpitSV06"                  , Category = "Devices"                      , Ref = "CockpitSV07"                  }},
            {  1238, new EpbBlockType(){Id =  1238, Name = "GrowingPotConcrete"           , Category = "BuildingBlocks"               , Ref = "GrowingPot"                   }},
            {  1239, new EpbBlockType(){Id =  1239, Name = "GrowingPotWood"               , Category = "BuildingBlocks"               , Ref = "GrowingPot"                   }},
            {  1240, new EpbBlockType(){Id =  1240, Name = "ScifiTableNPC2"               , Category = "Deco Blocks"                  , Ref = "NPCAlienTemplate"             }},
            {  1241, new EpbBlockType(){Id =  1241, Name = "ScifiTableNPC3"               , Category = "Deco Blocks"                  , Ref = "NPCAlienTemplate"             }},
            {  1242, new EpbBlockType(){Id =  1242, Name = "ScifiLargeSofaNPC"            , Category = "Deco Blocks"                  , Ref = "NPCAlienTemplate"             }},
            {  1243, new EpbBlockType(){Id =  1243, Name = "ConsoleSmallNPC"              , Category = "Deco Blocks"                  , Ref = "NPCAlienTemplate"             }},
            {  1244, new EpbBlockType(){Id =  1244, Name = "ScifiTableV2NPC"              , Category = "Deco Blocks"                  , Ref = "NPCAlienTemplate"             }},
            {  1245, new EpbBlockType(){Id =  1245, Name = "SofaNPC"                      , Category = "Deco Blocks"                  , Ref = "NPCAlienTemplate"             }},
            {  1246, new EpbBlockType(){Id =  1246, Name = "StandingNPC"                  , Category = "Deco Blocks"                  , Ref = "NPCAlienTemplate"             }},
            {  1247, new EpbBlockType(){Id =  1247, Name = "ControlStationNPC"            , Category = "Deco Blocks"                  , Ref = "NPCAlienTemplate"             }},
            {  1248, new EpbBlockType(){Id =  1248, Name = "ReceptionTableNPC"            , Category = "Deco Blocks"                  , Ref = "NPCAlienTemplate"             }},
            {  1249, new EpbBlockType(){Id =  1249, Name = "ScifiSofaNPC"                 , Category = "Deco Blocks"                  , Ref = "NPCAlienTemplate"             }},
            {  1250, new EpbBlockType(){Id =  1250, Name = "ScifiTableNPC"                , Category = "Deco Blocks"                  , Ref = "NPCAlienTemplate"             }},
            {  1251, new EpbBlockType(){Id =  1251, Name = "StandingNPC2"                 , Category = "Deco Blocks"                  , Ref = "NPCAlienTemplate"             }},
            {  1252, new EpbBlockType(){Id =  1252, Name = "ConsoleSmallHuman"            , Category = "Deco Blocks"                  , Ref = "NPCHumanTemplate"             }},
            {  1253, new EpbBlockType(){Id =  1253, Name = "CockpitBlocksCV"              , Category = "Devices"                      , Ref = ""                             }},
            {  1254, new EpbBlockType(){Id =  1254, Name = "AlienDeviceBlocks"            , Category = "Devices"                      , Ref = ""                             }},
            {  1257, new EpbBlockType(){Id =  1257, Name = "SensorTriggerBlocks"          , Category = "Devices"                      , Ref = ""                             }},
            {  1258, new EpbBlockType(){Id =  1258, Name = "TrapDoor"                     , Category = "Devices"                      , Ref = ""                             }},
            {  1259, new EpbBlockType(){Id =  1259, Name = "LightBarrier"                 , Category = "Devices"                      , Ref = ""                             }},
            {  1260, new EpbBlockType(){Id =  1260, Name = "MotionSensor"                 , Category = "Devices"                      , Ref = ""                             }},
            {  1262, new EpbBlockType(){Id =  1262, Name = "Lever"                        , Category = "Devices"                      , Ref = ""                             }},
            {  1263, new EpbBlockType(){Id =  1263, Name = "ExplosiveBlocks"              , Category = "Devices"                      , Ref = ""                             }},
            {  1264, new EpbBlockType(){Id =  1264, Name = "TrapDoorAnim"                 , Category = "Devices"                      , Ref = ""                             }},
            {  1265, new EpbBlockType(){Id =  1265, Name = "TriggerPlate"                 , Category = "Devices"                      , Ref = ""                             }},
            {  1272, new EpbBlockType(){Id =  1272, Name = "LightLantern"                 , Category = "Devices"                      , Ref = "LightMS01"                    }},
            {  1273, new EpbBlockType(){Id =  1273, Name = "LightMS01Corner"              , Category = "Devices"                      , Ref = "LightMS01"                    }},
            {  1274, new EpbBlockType(){Id =  1274, Name = "LightMS01Offset"              , Category = "Devices"                      , Ref = "LightMS01"                    }},
            {  1275, new EpbBlockType(){Id =  1275, Name = "LightMS02"                    , Category = "Devices"                      , Ref = "LightMS01"                    }},
            {  1276, new EpbBlockType(){Id =  1276, Name = "LightMS03"                    , Category = "Devices"                      , Ref = "LightMS01"                    }},
            {  1277, new EpbBlockType(){Id =  1277, Name = "LightMS04"                    , Category = "Devices"                      , Ref = "LightMS01"                    }},
            {  1278, new EpbBlockType(){Id =  1278, Name = "LightLargeBlocks"             , Category = "Devices"                      , Ref = ""                             }},
            {  1279, new EpbBlockType(){Id =  1279, Name = "ReceptionTable"               , Category = "Furnishings (Deco)"           , Ref = "DecoTemplate"                 }},
            {  1280, new EpbBlockType(){Id =  1280, Name = "SmallTable"                   , Category = "Furnishings (Deco)"           , Ref = "DecoTemplate"                 }},
            {  1281, new EpbBlockType(){Id =  1281, Name = "DecoBlocks2"                  , Category = "Deco Blocks"                  , Ref = "DecoBlocks"                   }},
            {  1282, new EpbBlockType(){Id =  1282, Name = "Level4Prop2"                  , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1283, new EpbBlockType(){Id =  1283, Name = "Level4Prop3"                  , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1284, new EpbBlockType(){Id =  1284, Name = "Freezer"                      , Category = "Devices"                      , Ref = "FridgeMS02"                   }},
            {  1285, new EpbBlockType(){Id =  1285, Name = "Level5FreezerOpened"          , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1286, new EpbBlockType(){Id =  1286, Name = "LabTable1"                    , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1287, new EpbBlockType(){Id =  1287, Name = "LabTable2"                    , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1288, new EpbBlockType(){Id =  1288, Name = "LabTable3"                    , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1289, new EpbBlockType(){Id =  1289, Name = "LockerWShelves"               , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1290, new EpbBlockType(){Id =  1290, Name = "OperationTableWDrawers"       , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1291, new EpbBlockType(){Id =  1291, Name = "Props6BoxLarge1"              , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1292, new EpbBlockType(){Id =  1292, Name = "Props6BoxLarge2"              , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1293, new EpbBlockType(){Id =  1293, Name = "Props6BoxMedium1"             , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1294, new EpbBlockType(){Id =  1294, Name = "ScannerBase1"                 , Category = "Devices"                      , Ref = "DecoTemplate"                 }},
            {  1295, new EpbBlockType(){Id =  1295, Name = "Scanner2"                     , Category = "Devices"                      , Ref = "DecoTemplate"                 }},
            {  1296, new EpbBlockType(){Id =  1296, Name = "Scanner3"                     , Category = "Devices"                      , Ref = "DecoTemplate"                 }},
            {  1297, new EpbBlockType(){Id =  1297, Name = "Tank1"                        , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1298, new EpbBlockType(){Id =  1298, Name = "Tank2"                        , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1299, new EpbBlockType(){Id =  1299, Name = "Console4"                     , Category = "Devices"                      , Ref = "DecoTemplate"                 }},
            {  1300, new EpbBlockType(){Id =  1300, Name = "DecoStoneTemplate"            , Category = "Deco Blocks"                  , Ref = ""                             }},
            {  1301, new EpbBlockType(){Id =  1301, Name = "StoneBarbarian"               , Category = "Deco Blocks"                  , Ref = "DecoStoneTemplate"            }},
            {  1302, new EpbBlockType(){Id =  1302, Name = "CelticCross"                  , Category = "Deco Blocks"                  , Ref = "DecoStoneTemplate"            }},
            {  1303, new EpbBlockType(){Id =  1303, Name = "DemonHead"                    , Category = "Deco Blocks"                  , Ref = "DecoStoneTemplate"            }},
            {  1305, new EpbBlockType(){Id =  1305, Name = "DemonicStatue"                , Category = "Deco Blocks"                  , Ref = "DecoStoneTemplate"            }},
            {  1306, new EpbBlockType(){Id =  1306, Name = "GothicFountain"               , Category = "Deco Blocks"                  , Ref = "DecoStoneTemplate"            }},
            {  1307, new EpbBlockType(){Id =  1307, Name = "GreekHead"                    , Category = "Deco Blocks"                  , Ref = "DecoStoneTemplate"            }},
            {  1308, new EpbBlockType(){Id =  1308, Name = "MayanStatueSnake"             , Category = "Deco Blocks"                  , Ref = "DecoStoneTemplate"            }},
            {  1309, new EpbBlockType(){Id =  1309, Name = "SnakeStatue"                  , Category = "Deco Blocks"                  , Ref = "DecoStoneTemplate"            }},
            {  1310, new EpbBlockType(){Id =  1310, Name = "StatueSkull"                  , Category = "Deco Blocks"                  , Ref = "DecoStoneTemplate"            }},
            {  1311, new EpbBlockType(){Id =  1311, Name = "TigerStatue"                  , Category = "Deco Blocks"                  , Ref = "DecoStoneTemplate"            }},
            {  1312, new EpbBlockType(){Id =  1312, Name = "AncientStatue"                , Category = "Deco Blocks"                  , Ref = "DecoStoneTemplate"            }},
            {  1313, new EpbBlockType(){Id =  1313, Name = "PlantDead"                    , Category = "Farming"                      , Ref = ""                             }},
            {  1314, new EpbBlockType(){Id =  1314, Name = "Trader"                       , Category = "NPC"                          , Ref = ""                             }},
            {  1316, new EpbBlockType(){Id =  1316, Name = "PlantDead2"                   , Category = "Farming"                      , Ref = "PlantDead"                    }},
            {  1318, new EpbBlockType(){Id =  1318, Name = "ExplosiveBlocks2"             , Category = "Devices"                      , Ref = "ExplosiveBlocks"              }},
            {  1319, new EpbBlockType(){Id =  1319, Name = "SpotlightSlope"               , Category = "Devices"                      , Ref = "SpotlightSSCube"              }},
            {  1320, new EpbBlockType(){Id =  1320, Name = "SpotlightSlopeHorizontal"     , Category = "Devices"                      , Ref = "SpotlightSSCube"              }},
            {  1321, new EpbBlockType(){Id =  1321, Name = "SpotlightBlocks"              , Category = "Devices"                      , Ref = "SpotlightSSCube"              }},
            {  1322, new EpbBlockType(){Id =  1322, Name = "ConcreteArmoredBlocks"        , Category = "BuildingBlocks"               , Ref = ""                             }},
            {  1323, new EpbBlockType(){Id =  1323, Name = "ConcreteArmoredFull"          , Category = "BuildingBlocks"               , Ref = ""                             }},
            {  1324, new EpbBlockType(){Id =  1324, Name = "ConcreteArmoredThin"          , Category = "BuildingBlocks"               , Ref = "ConcreteArmoredFull"          }},
            {  1330, new EpbBlockType(){Id =  1330, Name = "SproutDead"                   , Category = "Farming"                      , Ref = ""                             }},
            {  1331, new EpbBlockType(){Id =  1331, Name = "DemonicStatueSmall"           , Category = "Deco Blocks"                  , Ref = "DecoStoneTemplate"            }},
            {  1332, new EpbBlockType(){Id =  1332, Name = "MayanStatueSnakeSmall"        , Category = "Deco Blocks"                  , Ref = "DecoStoneTemplate"            }},
            {  1333, new EpbBlockType(){Id =  1333, Name = "SnakeStatueSmall"             , Category = "Deco Blocks"                  , Ref = "DecoStoneTemplate"            }},
            {  1334, new EpbBlockType(){Id =  1334, Name = "TigerStatueSmall"             , Category = "Deco Blocks"                  , Ref = "DecoStoneTemplate"            }},
            {  1335, new EpbBlockType(){Id =  1335, Name = "Runestone"                    , Category = "Deco Blocks"                  , Ref = "DecoStoneTemplate"            }},
            {  1336, new EpbBlockType(){Id =  1336, Name = "DecoStoneBlocks"              , Category = "Deco Blocks"                  , Ref = "DecoBlocks"                   }},
            {  1338, new EpbBlockType(){Id =  1338, Name = "ReceptionTableCorner"         , Category = "Furnishings (Deco)"           , Ref = "DecoTemplate"                 }},
            {  1360, new EpbBlockType(){Id =  1360, Name = "CoreNPCAdmin"                 , Category = "Devices"                      , Ref = "CoreNPC"                      }},
            {  1361, new EpbBlockType(){Id =  1361, Name = "CorePlayerAdmin"              , Category = "Devices"                      , Ref = "Core"                         }},
            {  1362, new EpbBlockType(){Id =  1362, Name = "Antenna01"                    , Category = "Deco Blocks"                  , Ref = "Antenna"                      }},
            {  1363, new EpbBlockType(){Id =  1363, Name = "Antenna02"                    , Category = "Deco Blocks"                  , Ref = "Antenna"                      }},
            {  1364, new EpbBlockType(){Id =  1364, Name = "Antenna03"                    , Category = "Deco Blocks"                  , Ref = "Antenna"                      }},
            {  1365, new EpbBlockType(){Id =  1365, Name = "Antenna04"                    , Category = "Deco Blocks"                  , Ref = "Antenna"                      }},
            {  1366, new EpbBlockType(){Id =  1366, Name = "Antenna05"                    , Category = "Deco Blocks"                  , Ref = "Antenna"                      }},
            {  1370, new EpbBlockType(){Id =  1370, Name = "ArmorLocker"                  , Category = "Devices"                      , Ref = ""                             }},
            {  1371, new EpbBlockType(){Id =  1371, Name = "Deconstructor"                , Category = "Devices"                      , Ref = ""                             }},
            {  1372, new EpbBlockType(){Id =  1372, Name = "RepairStation"                , Category = "Devices"                      , Ref = ""                             }},
            {  1373, new EpbBlockType(){Id =  1373, Name = "Portal"                       , Category = "Devices"                      , Ref = ""                             }},
            {  1374, new EpbBlockType(){Id =  1374, Name = "PlayerSpawner"                , Category = "Devices"                      , Ref = ""                             }},
            {  1375, new EpbBlockType(){Id =  1375, Name = "DoorBlocksSV"                 , Category = "Devices"                      , Ref = ""                             }},
            {  1376, new EpbBlockType(){Id =  1376, Name = "DoorInterior01SV"             , Category = "Devices"                      , Ref = "DoorSS01"                     }},
            {  1377, new EpbBlockType(){Id =  1377, Name = "Teleporter"                   , Category = "Devices"                      , Ref = ""                             }},
            {  1380, new EpbBlockType(){Id =  1380, Name = "ArmorLockerSV"                , Category = "Devices"                      , Ref = ""                             }},
            {  1385, new EpbBlockType(){Id =  1385, Name = "PlayerSpawnerPlateThin"       , Category = "Devices"                      , Ref = "PlayerSpawner"                }},
            {  1386, new EpbBlockType(){Id =  1386, Name = "HullLargeDestroyedBlocks"     , Category = "BuildingBlocks"               , Ref = ""                             }},
            {  1387, new EpbBlockType(){Id =  1387, Name = "HullFullLargeDestroyed"       , Category = "BuildingBlocks"               , Ref = ""                             }},
            {  1388, new EpbBlockType(){Id =  1388, Name = "HullThinLargeDestroyed"       , Category = "BuildingBlocks"               , Ref = "HullFullLargeDestroyed"       }},
            {  1389, new EpbBlockType(){Id =  1389, Name = "HullSmallDestroyedBlocks"     , Category = "BuildingBlocks"               , Ref = ""                             }},
            {  1390, new EpbBlockType(){Id =  1390, Name = "HullFullSmallDestroyed"       , Category = "BuildingBlocks"               , Ref = ""                             }},
            {  1391, new EpbBlockType(){Id =  1391, Name = "HullThinSmallDestroyed"       , Category = "BuildingBlocks"               , Ref = "HullFullSmallDestroyed"       }},
            {  1392, new EpbBlockType(){Id =  1392, Name = "ConcreteDestroyedBlocks"      , Category = "BuildingBlocks"               , Ref = ""                             }},
            {  1393, new EpbBlockType(){Id =  1393, Name = "ConcreteFullDestroyed"        , Category = "BuildingBlocks"               , Ref = ""                             }},
            {  1394, new EpbBlockType(){Id =  1394, Name = "ConcreteThinDestroyed"        , Category = "BuildingBlocks"               , Ref = "ConcreteFullDestroyed"        }},
            {  1395, new EpbBlockType(){Id =  1395, Name = "AlienLargeBlocks"             , Category = "BuildingBlocks"               , Ref = "AlienBlocks"                  }},
            {  1396, new EpbBlockType(){Id =  1396, Name = "AlienFullLarge"               , Category = "BuildingBlocks"               , Ref = "AlienFull"                    }},
            {  1397, new EpbBlockType(){Id =  1397, Name = "AlienThinLarge"               , Category = "BuildingBlocks"               , Ref = "AlienThin"                    }},
            {  1398, new EpbBlockType(){Id =  1398, Name = "ReceptionTableThin"           , Category = "Furnishings (Deco)"           , Ref = "DecoTemplate"                 }},
            {  1399, new EpbBlockType(){Id =  1399, Name = "ReceptionTableCornerThin"     , Category = "Furnishings (Deco)"           , Ref = "DecoTemplate"                 }},
            {  1405, new EpbBlockType(){Id =  1405, Name = "Ventilator"                   , Category = "Devices"                      , Ref = ""                             }},
            {  1406, new EpbBlockType(){Id =  1406, Name = "TrussWall"                    , Category = "BuildingBlocks"               , Ref = "TrussCube"                    }},
            {  1407, new EpbBlockType(){Id =  1407, Name = "TrussCylinder"                , Category = "BuildingBlocks"               , Ref = "TrussCube"                    }},
            {  1408, new EpbBlockType(){Id =  1408, Name = "TrussHalfRound"               , Category = "BuildingBlocks"               , Ref = "TrussCube"                    }},
            {  1409, new EpbBlockType(){Id =  1409, Name = "TrussQuarterRound"            , Category = "BuildingBlocks"               , Ref = "TrussCube"                    }},
            {  1410, new EpbBlockType(){Id =  1410, Name = "TrussQuarterRoundInv"         , Category = "BuildingBlocks"               , Ref = "TrussCube"                    }},
            {  1411, new EpbBlockType(){Id =  1411, Name = "TrussCurveOutSlope"           , Category = "BuildingBlocks"               , Ref = "TrussCube"                    }},
            {  1412, new EpbBlockType(){Id =  1412, Name = "TrussWedgeThin"               , Category = "BuildingBlocks"               , Ref = "TrussCube"                    }},
            {  1413, new EpbBlockType(){Id =  1413, Name = "TrussQuarterRoundThin"        , Category = "BuildingBlocks"               , Ref = "TrussCube"                    }},
            {  1414, new EpbBlockType(){Id =  1414, Name = "TrussCornerThin"              , Category = "BuildingBlocks"               , Ref = "TrussCube"                    }},
            {  1415, new EpbBlockType(){Id =  1415, Name = "TrussCornerRoundThin"         , Category = "BuildingBlocks"               , Ref = "TrussCube"                    }},
            {  1416, new EpbBlockType(){Id =  1416, Name = "TrussCornerRoundThin2"        , Category = "BuildingBlocks"               , Ref = "TrussCube"                    }},
            {  1417, new EpbBlockType(){Id =  1417, Name = "ThrusterGVJetRound1x3x1"      , Category = "Devices"                      , Ref = "ThrusterGVRoundNormal"        }},
            {  1418, new EpbBlockType(){Id =  1418, Name = "ElderberryBushDeco"           , Category = "Deco Blocks"                  , Ref = "IndoorPlant01"                }},
            {  1419, new EpbBlockType(){Id =  1419, Name = "ElderberryBushBlueDeco"       , Category = "Deco Blocks"                  , Ref = "IndoorPlant01"                }},
            {  1420, new EpbBlockType(){Id =  1420, Name = "AlienPalmTreeDeco"            , Category = "Deco Blocks"                  , Ref = "IndoorPlant01"                }},
            {  1421, new EpbBlockType(){Id =  1421, Name = "AlienTentacleDeco"            , Category = "Deco Blocks"                  , Ref = "IndoorPlant01"                }},
            {  1422, new EpbBlockType(){Id =  1422, Name = "HollywoodJuniperDeco"         , Category = "Deco Blocks"                  , Ref = "IndoorPlant01"                }},
            {  1423, new EpbBlockType(){Id =  1423, Name = "BallTreeDeco"                 , Category = "Deco Blocks"                  , Ref = "IndoorPlant01"                }},
            {  1424, new EpbBlockType(){Id =  1424, Name = "BallFlower01Deco"             , Category = "Deco Blocks"                  , Ref = "IndoorPlant01"                }},
            {  1425, new EpbBlockType(){Id =  1425, Name = "OnionFlowerDeco"              , Category = "Deco Blocks"                  , Ref = "IndoorPlant01"                }},
            {  1426, new EpbBlockType(){Id =  1426, Name = "FantasyPlant1Deco"            , Category = "Deco Blocks"                  , Ref = "IndoorPlant01"                }},
            {  1427, new EpbBlockType(){Id =  1427, Name = "AkuaFernDeco"                 , Category = "Deco Blocks"                  , Ref = "IndoorPlant01"                }},
            {  1428, new EpbBlockType(){Id =  1428, Name = "GlowTube01Deco"               , Category = "Deco Blocks"                  , Ref = "IndoorPlant01"                }},
            {  1429, new EpbBlockType(){Id =  1429, Name = "DoorInterior01SlimSV"         , Category = "Devices"                      , Ref = "DoorSS01"                     }},
            {  1430, new EpbBlockType(){Id =  1430, Name = "DoorSS01Slim"                 , Category = "Devices"                      , Ref = "DoorSS01"                     }},
            {  1435, new EpbBlockType(){Id =  1435, Name = "WarpDriveSV"                  , Category = "Devices"                      , Ref = "WarpDrive"                    }},
            {  1436, new EpbBlockType(){Id =  1436, Name = "LandClaimDevice"              , Category = "Devices"                      , Ref = ""                             }},
            {  1437, new EpbBlockType(){Id =  1437, Name = "PentaxidTankSV"               , Category = "Devices"                      , Ref = ""                             }},
            {  1440, new EpbBlockType(){Id =  1440, Name = "StairsBlocksConcrete"         , Category = "BuildingBlocks"               , Ref = "StairsBlocks"                 }},
            {  1441, new EpbBlockType(){Id =  1441, Name = "StairShapesShortConcrete"     , Category = "BuildingBlocks"               , Ref = "StairShapes"                  }},
            {  1442, new EpbBlockType(){Id =  1442, Name = "StairShapesLongConcrete"      , Category = "BuildingBlocks"               , Ref = "StairShapes"                  }},
            {  1443, new EpbBlockType(){Id =  1443, Name = "StairsBlocksWood"             , Category = "BuildingBlocks"               , Ref = "StairsBlocks"                 }},
            {  1444, new EpbBlockType(){Id =  1444, Name = "StairShapesShortWood"         , Category = "BuildingBlocks"               , Ref = "StairShapes"                  }},
            {  1445, new EpbBlockType(){Id =  1445, Name = "StairShapesLongWood"          , Category = "BuildingBlocks"               , Ref = "StairShapes"                  }},
            {  1446, new EpbBlockType(){Id =  1446, Name = "ConstructorSV"                , Category = "Devices"                      , Ref = "ConstructorSmallV2"           }},
            {  1447, new EpbBlockType(){Id =  1447, Name = "ConstructorHV"                , Category = "Devices"                      , Ref = "ConstructorSmallV2"           }},
            {  1453, new EpbBlockType(){Id =  1453, Name = "StandingHuman"                , Category = "Deco Blocks"                  , Ref = "NPCHumanTemplate"             }},
            {  1454, new EpbBlockType(){Id =  1454, Name = "StandingHuman2"               , Category = "Deco Blocks"                  , Ref = "NPCHumanTemplate"             }},
            {  1455, new EpbBlockType(){Id =  1455, Name = "ControlStationHuman"          , Category = "Deco Blocks"                  , Ref = "NPCHumanTemplate"             }},
            {  1456, new EpbBlockType(){Id =  1456, Name = "ReceptionTableHuman"          , Category = "Deco Blocks"                  , Ref = "NPCHumanTemplate"             }},
            {  1457, new EpbBlockType(){Id =  1457, Name = "ControlStationHuman2"         , Category = "Deco Blocks"                  , Ref = "NPCHumanTemplate"             }},
            {  1458, new EpbBlockType(){Id =  1458, Name = "ScifiTableHuman"              , Category = "Deco Blocks"                  , Ref = "NPCHumanTemplate"             }},
            {  1459, new EpbBlockType(){Id =  1459, Name = "ScifiLargeSofaHuman"          , Category = "Deco Blocks"                  , Ref = "NPCHumanTemplate"             }},
            {  1460, new EpbBlockType(){Id =  1460, Name = "TacticalOfficer"              , Category = "Deco Blocks"                  , Ref = "NPCHumanTemplate"             }},
            {  1461, new EpbBlockType(){Id =  1461, Name = "CommandingOfficer"            , Category = "Deco Blocks"                  , Ref = "NPCHumanTemplate"             }},
            {  1462, new EpbBlockType(){Id =  1462, Name = "SecurityGuard"                , Category = "Deco Blocks"                  , Ref = "NPCHumanTemplate"             }},
            {  1463, new EpbBlockType(){Id =  1463, Name = "OperatorPilot"                , Category = "Deco Blocks"                  , Ref = "NPCHumanTemplate"             }},
            {  1464, new EpbBlockType(){Id =  1464, Name = "EngineerMainStation"          , Category = "Deco Blocks"                  , Ref = "NPCHumanTemplate"             }},
            {  1465, new EpbBlockType(){Id =  1465, Name = "AlienNPCBlocks"               , Category = "Devices"                      , Ref = ""                             }},
            {  1466, new EpbBlockType(){Id =  1466, Name = "HumanNPCBlocks"               , Category = "Devices"                      , Ref = ""                             }},
            {  1467, new EpbBlockType(){Id =  1467, Name = "CommandingOfficer2"           , Category = "Deco Blocks"                  , Ref = "NPCHumanTemplate"             }},
            {  1468, new EpbBlockType(){Id =  1468, Name = "SecurityGuard2"               , Category = "Deco Blocks"                  , Ref = "NPCHumanTemplate"             }},
            {  1469, new EpbBlockType(){Id =  1469, Name = "CommandingOfficerAlien"       , Category = "Deco Blocks"                  , Ref = "NPCAlienTemplate"             }},
            {  1470, new EpbBlockType(){Id =  1470, Name = "SecurityGuardAlien"           , Category = "Deco Blocks"                  , Ref = "NPCAlienTemplate"             }},
            {  1472, new EpbBlockType(){Id =  1472, Name = "StandingAlienAssassin"        , Category = "Deco Blocks"                  , Ref = "NPCAlienTemplate"             }},
            {  1473, new EpbBlockType(){Id =  1473, Name = "StandingHexapod"              , Category = "Deco Blocks"                  , Ref = "NPCAlienTemplate"             }},
            {  1474, new EpbBlockType(){Id =  1474, Name = "DancingHuman1"                , Category = "Deco Blocks"                  , Ref = "NPCHumanTemplate"             }},
            {  1475, new EpbBlockType(){Id =  1475, Name = "DancingHuman2"                , Category = "Deco Blocks"                  , Ref = "NPCHumanTemplate"             }},
            {  1476, new EpbBlockType(){Id =  1476, Name = "DancingHuman3"                , Category = "Deco Blocks"                  , Ref = "NPCHumanTemplate"             }},
            {  1477, new EpbBlockType(){Id =  1477, Name = "DancingAlien1"                , Category = "Deco Blocks"                  , Ref = "NPCHumanTemplate"             }},
            {  1478, new EpbBlockType(){Id =  1478, Name = "PlasticSmallBlocks"           , Category = "BuildingBlocks"               , Ref = ""                             }},
            {  1479, new EpbBlockType(){Id =  1479, Name = "PlasticFullSmall"             , Category = "BuildingBlocks"               , Ref = ""                             }},
            {  1480, new EpbBlockType(){Id =  1480, Name = "PlasticThinSmall"             , Category = "BuildingBlocks"               , Ref = "PlasticFullSmall"             }},
            {  1481, new EpbBlockType(){Id =  1481, Name = "PlasticLargeBlocks"           , Category = "BuildingBlocks"               , Ref = ""                             }},
            {  1482, new EpbBlockType(){Id =  1482, Name = "PlasticFullLarge"             , Category = "BuildingBlocks"               , Ref = ""                             }},
            {  1483, new EpbBlockType(){Id =  1483, Name = "PlasticThinLarge"             , Category = "BuildingBlocks"               , Ref = "PlasticFullLarge"             }},
            {  1484, new EpbBlockType(){Id =  1484, Name = "HoverEngineThruster"          , Category = "Devices"                      , Ref = ""                             }},
            {  1485, new EpbBlockType(){Id =  1485, Name = "MobileAirCon"                 , Category = "Devices"                      , Ref = ""                             }},
            {  1486, new EpbBlockType(){Id =  1486, Name = "RepairBayCVT2"                , Category = "Devices"                      , Ref = "RepairBayCV"                  }},
            {  1487, new EpbBlockType(){Id =  1487, Name = "CaptainChair01"               , Category = "Devices"                      , Ref = ""                             }},
            {  1490, new EpbBlockType(){Id =  1490, Name = "RepairBayConsole"             , Category = "Devices"                      , Ref = ""                             }},
            {  1491, new EpbBlockType(){Id =  1491, Name = "LightCorner"                  , Category = "Devices"                      , Ref = "LightMS01"                    }},
            {  1492, new EpbBlockType(){Id =  1492, Name = "LightCorner02"                , Category = "Devices"                      , Ref = "LightMS01"                    }},
            {  1493, new EpbBlockType(){Id =  1493, Name = "BunkBed02"                    , Category = "Furnishings (Deco)"           , Ref = "DecoTemplate"                 }},
            {  1494, new EpbBlockType(){Id =  1494, Name = "SolarPanelBlocks"             , Category = "Devices"                      , Ref = ""                             }},
            {  1495, new EpbBlockType(){Id =  1495, Name = "SolarGenerator"               , Category = "Devices"                      , Ref = ""                             }},
            {  1496, new EpbBlockType(){Id =  1496, Name = "SolarPanelSlope"              , Category = "Devices"                      , Ref = ""                             }},
            {  1497, new EpbBlockType(){Id =  1497, Name = "SolarPanelHorizontal"         , Category = "Devices"                      , Ref = "SolarPanelSlope"              }},
            {  1498, new EpbBlockType(){Id =  1498, Name = "SolarPanelHorizontal2"        , Category = "Devices"                      , Ref = "SolarPanelSlope"              }},
            {  1499, new EpbBlockType(){Id =  1499, Name = "SolarPanelHorizontalMount"    , Category = "Devices"                      , Ref = "SolarPanelSlope"              }},
            {  1500, new EpbBlockType(){Id =  1500, Name = "ForcefieldEmitterBlocks"      , Category = "Devices"                      , Ref = ""                             }},
            {  1501, new EpbBlockType(){Id =  1501, Name = "ForcefieldEmitter1x1"         , Category = "Devices"                      , Ref = ""                             }},
            {  1502, new EpbBlockType(){Id =  1502, Name = "ForcefieldEmitter1x2"         , Category = "Devices"                      , Ref = "ForcefieldEmitter1x1"         }},
            {  1503, new EpbBlockType(){Id =  1503, Name = "ForcefieldEmitter1x3"         , Category = "Devices"                      , Ref = "ForcefieldEmitter1x1"         }},
            {  1504, new EpbBlockType(){Id =  1504, Name = "ForcefieldEmitter3x5"         , Category = "Devices"                      , Ref = "ForcefieldEmitter1x1"         }},
            {  1505, new EpbBlockType(){Id =  1505, Name = "ForcefieldEmitter3x9"         , Category = "Devices"                      , Ref = "ForcefieldEmitter1x1"         }},
            {  1506, new EpbBlockType(){Id =  1506, Name = "ForcefieldEmitter5x11"        , Category = "Devices"                      , Ref = "ForcefieldEmitter1x1"         }},
            {  1507, new EpbBlockType(){Id =  1507, Name = "ForcefieldEmitter7x14"        , Category = "Devices"                      , Ref = "ForcefieldEmitter1x1"         }},
            {  1510, new EpbBlockType(){Id =  1510, Name = "SolarPanelSlope2"             , Category = "Devices"                      , Ref = "SolarPanelSlope"              }},
            {  1511, new EpbBlockType(){Id =  1511, Name = "SolarPanelSlope3"             , Category = "Devices"                      , Ref = "SolarPanelSlope"              }},
            {  1512, new EpbBlockType(){Id =  1512, Name = "SolarPanelHorizontalStand"    , Category = "Devices"                      , Ref = "SolarPanelSlope"              }},
            {  1513, new EpbBlockType(){Id =  1513, Name = "SolarPanelSmallBlocks"        , Category = "Devices"                      , Ref = ""                             }},
            {  1514, new EpbBlockType(){Id =  1514, Name = "SolarPanelSlope3Small"        , Category = "Devices"                      , Ref = ""                             }},
            {  1515, new EpbBlockType(){Id =  1515, Name = "SolarPanelSlopeSmall"         , Category = "Devices"                      , Ref = "SolarPanelSlope3Small"        }},
            {  1516, new EpbBlockType(){Id =  1516, Name = "SolarPanelHorizontalSmall"    , Category = "Devices"                      , Ref = "SolarPanelSlope3Small"        }},
            {  1517, new EpbBlockType(){Id =  1517, Name = "TurretEnemyRocket"            , Category = "Weapons/Items"                , Ref = "TurretIONCannon"              }},
            {  1518, new EpbBlockType(){Id =  1518, Name = "TurretEnemyArtillery"         , Category = "Weapons/Items"                , Ref = "TurretIONCannon"              }},
            {  1535, new EpbBlockType(){Id =  1535, Name = "SurvivalTent01"               , Category = "Devices"                      , Ref = ""                             }},
            {  1549, new EpbBlockType(){Id =  1549, Name = "HeavyWindowBlocks"            , Category = "BuildingBlocks"               , Ref = ""                             }},
            {  1550, new EpbBlockType(){Id =  1550, Name = "HeavyWindowA"                 , Category = "BuildingBlocks"               , Ref = ""                             }},
            {  1551, new EpbBlockType(){Id =  1551, Name = "HeavyWindowB"                 , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1552, new EpbBlockType(){Id =  1552, Name = "HeavyWindowC"                 , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1553, new EpbBlockType(){Id =  1553, Name = "HeavyWindowD"                 , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1554, new EpbBlockType(){Id =  1554, Name = "HeavyWindowE"                 , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1555, new EpbBlockType(){Id =  1555, Name = "HeavyWindowF"                 , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1556, new EpbBlockType(){Id =  1556, Name = "HeavyWindowAInv"              , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1557, new EpbBlockType(){Id =  1557, Name = "HeavyWindowBInv"              , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1558, new EpbBlockType(){Id =  1558, Name = "HeavyWindowCInv"              , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1559, new EpbBlockType(){Id =  1559, Name = "HeavyWindowDInv"              , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1560, new EpbBlockType(){Id =  1560, Name = "HeavyWindowEInv"              , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1561, new EpbBlockType(){Id =  1561, Name = "HeavyWindowFInv"              , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1571, new EpbBlockType(){Id =  1571, Name = "MedicalStationBlocks"         , Category = "Devices"                      , Ref = ""                             }},
            {  1575, new EpbBlockType(){Id =  1575, Name = "DetectorHVT1"                 , Category = "Devices"                      , Ref = ""                             }},
            {  1576, new EpbBlockType(){Id =  1576, Name = "DetectorHVT2"                 , Category = "Devices"                      , Ref = "DetectorHVT1"                 }},
            {  1577, new EpbBlockType(){Id =  1577, Name = "ExplosiveBlockFull"           , Category = "Devices"                      , Ref = ""                             }},
            {  1578, new EpbBlockType(){Id =  1578, Name = "ExplosiveBlockThin"           , Category = "Devices"                      , Ref = "ExplosiveBlockFull"           }},
            {  1579, new EpbBlockType(){Id =  1579, Name = "ExplosiveBlock2Full"          , Category = "Devices"                      , Ref = ""                             }},
            {  1580, new EpbBlockType(){Id =  1580, Name = "ExplosiveBlock2Thin"          , Category = "Devices"                      , Ref = "ExplosiveBlock2Full"          }},
            {  1582, new EpbBlockType(){Id =  1582, Name = "DrillAttachmentCV"            , Category = "Weapons/Items"                , Ref = ""                             }},
            {  1583, new EpbBlockType(){Id =  1583, Name = "CloneChamberHV"               , Category = "Devices"                      , Ref = ""                             }},
            {  1584, new EpbBlockType(){Id =  1584, Name = "MedicStationHV"               , Category = "Devices"                      , Ref = ""                             }},
            {  1585, new EpbBlockType(){Id =  1585, Name = "ThrusterJetRound2x5x2V2"      , Category = "Devices"                      , Ref = "ThrusterJetRound2x5x2"        }},
            {  1586, new EpbBlockType(){Id =  1586, Name = "DroneSpawner"                 , Category = "BuildingBlocks"               , Ref = ""                             }},
            {  1587, new EpbBlockType(){Id =  1587, Name = "DroneSpawner2"                , Category = "BuildingBlocks"               , Ref = "DroneSpawner"                 }},
            {  1588, new EpbBlockType(){Id =  1588, Name = "FridgeBlocks"                 , Category = "Devices"                      , Ref = ""                             }},
            {  1589, new EpbBlockType(){Id =  1589, Name = "HoverEngineSmallDeco"         , Category = "Furnishings (Deco)"           , Ref = "DecoTemplate"                 }},
            {  1590, new EpbBlockType(){Id =  1590, Name = "HoverEngineLargeDeco"         , Category = "Furnishings (Deco)"           , Ref = "DecoTemplate"                 }},
            {  1591, new EpbBlockType(){Id =  1591, Name = "ThrusterJetRound3x10x3V2"     , Category = "Devices"                      , Ref = "ThrusterJetRound3x10x3"       }},
            {  1592, new EpbBlockType(){Id =  1592, Name = "ThrusterJetRound3x13x3V2"     , Category = "Devices"                      , Ref = "ThrusterJetRound3x13x3"       }},
            {  1594, new EpbBlockType(){Id =  1594, Name = "HullCombatSmallBlocks"        , Category = "BuildingBlocks"               , Ref = ""                             }},
            {  1595, new EpbBlockType(){Id =  1595, Name = "HullCombatFullSmall"          , Category = "BuildingBlocks"               , Ref = ""                             }},
            {  1596, new EpbBlockType(){Id =  1596, Name = "HullCombatThinSmall"          , Category = "BuildingBlocks"               , Ref = "HullCombatFullSmall"          }},
            {  1605, new EpbBlockType(){Id =  1605, Name = "ModularWingTaperedL"          , Category = "BuildingBlocks"               , Ref = ""                             }},
            {  1606, new EpbBlockType(){Id =  1606, Name = "ModularWingTaperedM"          , Category = "BuildingBlocks"               , Ref = "ModularWingTaperedL"          }},
            {  1607, new EpbBlockType(){Id =  1607, Name = "ModularWingTaperedS"          , Category = "BuildingBlocks"               , Ref = "ModularWingTaperedL"          }},
            {  1608, new EpbBlockType(){Id =  1608, Name = "ModularWingStraightL"         , Category = "BuildingBlocks"               , Ref = "ModularWingTaperedL"          }},
            {  1609, new EpbBlockType(){Id =  1609, Name = "ModularWingStraightM"         , Category = "BuildingBlocks"               , Ref = "ModularWingTaperedL"          }},
            {  1610, new EpbBlockType(){Id =  1610, Name = "ModularWingStraightS"         , Category = "BuildingBlocks"               , Ref = "ModularWingTaperedL"          }},
            {  1611, new EpbBlockType(){Id =  1611, Name = "ModularWingDeltaL"            , Category = "BuildingBlocks"               , Ref = "ModularWingTaperedL"          }},
            {  1612, new EpbBlockType(){Id =  1612, Name = "ModularWingDeltaM"            , Category = "BuildingBlocks"               , Ref = "ModularWingTaperedL"          }},
            {  1613, new EpbBlockType(){Id =  1613, Name = "ModularWingDeltaS"            , Category = "BuildingBlocks"               , Ref = "ModularWingTaperedL"          }},
            {  1614, new EpbBlockType(){Id =  1614, Name = "ModularWingSweptL"            , Category = "BuildingBlocks"               , Ref = "ModularWingTaperedL"          }},
            {  1615, new EpbBlockType(){Id =  1615, Name = "ModularWingSweptM"            , Category = "BuildingBlocks"               , Ref = "ModularWingTaperedL"          }},
            {  1616, new EpbBlockType(){Id =  1616, Name = "ModularWingSweptS"            , Category = "BuildingBlocks"               , Ref = "ModularWingTaperedL"          }},
            {  1617, new EpbBlockType(){Id =  1617, Name = "ModularWingLongL"             , Category = "BuildingBlocks"               , Ref = "ModularWingTaperedL"          }},
            {  1618, new EpbBlockType(){Id =  1618, Name = "ModularWingLongM"             , Category = "BuildingBlocks"               , Ref = "ModularWingTaperedL"          }},
            {  1619, new EpbBlockType(){Id =  1619, Name = "ModularWingLongS"             , Category = "BuildingBlocks"               , Ref = "ModularWingTaperedL"          }},
            {  1620, new EpbBlockType(){Id =  1620, Name = "ModularWingAngledTaperedL"    , Category = "BuildingBlocks"               , Ref = "ModularWingTaperedL"          }},
            {  1621, new EpbBlockType(){Id =  1621, Name = "ModularWingAngledTaperedM"    , Category = "BuildingBlocks"               , Ref = "ModularWingTaperedL"          }},
            {  1622, new EpbBlockType(){Id =  1622, Name = "ModularWingAngledTaperedS"    , Category = "BuildingBlocks"               , Ref = "ModularWingTaperedL"          }},
            {  1623, new EpbBlockType(){Id =  1623, Name = "ModularWingTConnectorL"       , Category = "BuildingBlocks"               , Ref = "ModularWingTaperedL"          }},
            {  1624, new EpbBlockType(){Id =  1624, Name = "ModularWingTConnectorM"       , Category = "BuildingBlocks"               , Ref = "ModularWingTaperedL"          }},
            {  1625, new EpbBlockType(){Id =  1625, Name = "ModularWingPylon"             , Category = "BuildingBlocks"               , Ref = "ModularWingTaperedL"          }},
            {  1626, new EpbBlockType(){Id =  1626, Name = "ModularWingBlocks"            , Category = "BuildingBlocks"               , Ref = ""                             }},
            {  1627, new EpbBlockType(){Id =  1627, Name = "RemoteConnection"             , Category = "Devices"                      , Ref = ""                             }},
            {  1628, new EpbBlockType(){Id =  1628, Name = "ConstructorT0"                , Category = "Devices"                      , Ref = "ConstructorT1V2"              }},
            {  1629, new EpbBlockType(){Id =  1629, Name = "HeavyWindowG"                 , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1630, new EpbBlockType(){Id =  1630, Name = "HeavyWindowGInv"              , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1631, new EpbBlockType(){Id =  1631, Name = "HeavyWindowH"                 , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1632, new EpbBlockType(){Id =  1632, Name = "HeavyWindowHInv"              , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1633, new EpbBlockType(){Id =  1633, Name = "HeavyWindowI"                 , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1634, new EpbBlockType(){Id =  1634, Name = "HeavyWindowIInv"              , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1635, new EpbBlockType(){Id =  1635, Name = "HeavyWindowJ"                 , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1636, new EpbBlockType(){Id =  1636, Name = "HeavyWindowJInv"              , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1637, new EpbBlockType(){Id =  1637, Name = "TurretMSProjectileBlocks"     , Category = "Weapons/Items"                , Ref = ""                             }},
            {  1638, new EpbBlockType(){Id =  1638, Name = "TurretMSRocketBlocks"         , Category = "Weapons/Items"                , Ref = ""                             }},
            {  1639, new EpbBlockType(){Id =  1639, Name = "TurretMSLaserBlocks"          , Category = "Weapons/Items"                , Ref = ""                             }},
            {  1640, new EpbBlockType(){Id =  1640, Name = "TurretMSToolBlocks"           , Category = "Weapons/Items"                , Ref = ""                             }},
            {  1641, new EpbBlockType(){Id =  1641, Name = "TurretMSArtilleryBlocks"      , Category = "Weapons/Items"                , Ref = ""                             }},
            {  1642, new EpbBlockType(){Id =  1642, Name = "DetectorSVT1"                 , Category = "Devices"                      , Ref = "DetectorHVT1"                 }},
            {  1645, new EpbBlockType(){Id =  1645, Name = "DancingAlien2"                , Category = "Deco Blocks"                  , Ref = "NPCHumanTemplate"             }},
            {  1646, new EpbBlockType(){Id =  1646, Name = "DancingAlien3"                , Category = "Deco Blocks"                  , Ref = "NPCHumanTemplate"             }},
            {  1647, new EpbBlockType(){Id =  1647, Name = "DancingAlien4"                , Category = "Deco Blocks"                  , Ref = "NPCHumanTemplate"             }},
            {  1648, new EpbBlockType(){Id =  1648, Name = "TurretBaseProjectileBlocks"   , Category = "Weapons/Items"                , Ref = ""                             }},
            {  1649, new EpbBlockType(){Id =  1649, Name = "TurretBaseRocketBlocks"       , Category = "Weapons/Items"                , Ref = ""                             }},
            {  1650, new EpbBlockType(){Id =  1650, Name = "TurretBaseLaserBlocks"        , Category = "Weapons/Items"                , Ref = ""                             }},
            {  1651, new EpbBlockType(){Id =  1651, Name = "TurretBaseArtilleryBlocks"    , Category = "Weapons/Items"                , Ref = ""                             }},
            {  1652, new EpbBlockType(){Id =  1652, Name = "TurretBaseFlakRetract"        , Category = "Weapons/Items"                , Ref = "TurretBaseFlak"               }},
            {  1653, new EpbBlockType(){Id =  1653, Name = "TurretBasePlasmaRetract"      , Category = "Weapons/Items"                , Ref = "TurretBasePlasma"             }},
            {  1654, new EpbBlockType(){Id =  1654, Name = "TurretBaseCannonRetract"      , Category = "Weapons/Items"                , Ref = "TurretBaseCannon"             }},
            {  1655, new EpbBlockType(){Id =  1655, Name = "TurretBaseRocketRetract"      , Category = "Weapons/Items"                , Ref = "TurretBaseRocket"             }},
            {  1656, new EpbBlockType(){Id =  1656, Name = "TurretBaseMinigunRetract"     , Category = "Weapons/Items"                , Ref = "TurretBaseMinigun"            }},
            {  1657, new EpbBlockType(){Id =  1657, Name = "TurretBasePulseLaserRetract"  , Category = "Weapons/Items"                , Ref = "TurretBasePulseLaser"         }},
            {  1658, new EpbBlockType(){Id =  1658, Name = "TurretBaseArtilleryRetract"   , Category = "Weapons/Items"                , Ref = "TurretBaseArtillery"          }},
            {  1659, new EpbBlockType(){Id =  1659, Name = "TurretGVMinigunBlocks"        , Category = "Weapons/Items"                , Ref = ""                             }},
            {  1660, new EpbBlockType(){Id =  1660, Name = "TurretGVRocketBlocks"         , Category = "Weapons/Items"                , Ref = ""                             }},
            {  1661, new EpbBlockType(){Id =  1661, Name = "TurretGVPlasmaBlocks"         , Category = "Weapons/Items"                , Ref = ""                             }},
            {  1662, new EpbBlockType(){Id =  1662, Name = "TurretGVArtilleryBlocks"      , Category = "Weapons/Items"                , Ref = ""                             }},
            {  1663, new EpbBlockType(){Id =  1663, Name = "TurretGVToolBlocks"           , Category = "Weapons/Items"                , Ref = ""                             }},
            {  1664, new EpbBlockType(){Id =  1664, Name = "TurretGVMinigunRetract"       , Category = "Weapons/Items"                , Ref = "TurretGVMinigun"              }},
            {  1665, new EpbBlockType(){Id =  1665, Name = "TurretGVRocketRetract"        , Category = "Weapons/Items"                , Ref = "TurretGVRocket"               }},
            {  1666, new EpbBlockType(){Id =  1666, Name = "TurretGVPlasmaRetract"        , Category = "Weapons/Items"                , Ref = "TurretGVPlasma"               }},
            {  1667, new EpbBlockType(){Id =  1667, Name = "TurretGVArtilleryRetract"     , Category = "Weapons/Items"                , Ref = "TurretGVArtillery"            }},
            {  1668, new EpbBlockType(){Id =  1668, Name = "TurretGVDrillRetract"         , Category = "Weapons/Items"                , Ref = "TurretGVDrill"                }},
            {  1669, new EpbBlockType(){Id =  1669, Name = "TurretGVToolRetract"          , Category = "Weapons/Items"                , Ref = "TurretGVTool"                 }},
            {  1670, new EpbBlockType(){Id =  1670, Name = "SentryGun03Retract"           , Category = "Weapons/Items"                , Ref = "SentryGun03"                  }},
            {  1671, new EpbBlockType(){Id =  1671, Name = "SentryGun05"                  , Category = "Weapons/Items"                , Ref = "SentryGun03"                  }},
            {  1672, new EpbBlockType(){Id =  1672, Name = "deprecated-SentryGun05Retract"           , Category = "Weapons/Items"                , Ref = "SentryGun03"                  }}, // Removed in A10.0.0-2446
            {  1673, new EpbBlockType(){Id =  1673, Name = "SentryGunBlocks"              , Category = "Weapons/Items"                , Ref = ""                             }},
            {  1674, new EpbBlockType(){Id =  1674, Name = "deprecated-SentryGun01Retract"           , Category = "Weapons/Items"                , Ref = "SentryGun01"                  }}, // Removed in A10.0.0-2446
            {  1675, new EpbBlockType(){Id =  1675, Name = "SentryGun02Retract"           , Category = "Weapons/Items"                , Ref = "SentryGun02"                  }},
            {  1676, new EpbBlockType(){Id =  1676, Name = "CargoContainerSmall"          , Category = "Devices"                      , Ref = ""                             }},
            {  1677, new EpbBlockType(){Id =  1677, Name = "CargoContainerMedium"         , Category = "Devices"                      , Ref = "CargoContainerSmall"          }},
            {  1678, new EpbBlockType(){Id =  1678, Name = "CargoContainerSV"             , Category = "Devices"                      , Ref = "CargoContainerSmall"          }},
            {  1682, new EpbBlockType(){Id =  1682, Name = "ContainerControllerLarge"     , Category = "Devices"                      , Ref = ""                             }},
            {  1683, new EpbBlockType(){Id =  1683, Name = "ContainerExtensionLarge"      , Category = "Devices"                      , Ref = ""                             }},
            {  1684, new EpbBlockType(){Id =  1684, Name = "ContainerControllerSmall"     , Category = "Devices"                      , Ref = "ContainerControllerLarge"     }},
            {  1685, new EpbBlockType(){Id =  1685, Name = "ContainerExtensionSmall"      , Category = "Devices"                      , Ref = "ContainerExtensionLarge"      }},
            {  1686, new EpbBlockType(){Id =  1686, Name = "ContainerHarvestControllerSmall", Category = "Devices"                      , Ref = "ContainerControllerLarge"     }},
            {  1687, new EpbBlockType(){Id =  1687, Name = "ContainerHarvestControllerLarge", Category = "Devices"                      , Ref = "ContainerControllerLarge"     }},
            {  1688, new EpbBlockType(){Id =  1688, Name = "ContainerAmmoControllerSmall" , Category = "Devices"                      , Ref = "ContainerControllerLarge"     }},
            {  1689, new EpbBlockType(){Id =  1689, Name = "ContainerAmmoControllerLarge" , Category = "Devices"                      , Ref = "ContainerControllerLarge"     }},
            {  1690, new EpbBlockType(){Id =  1690, Name = "WalkwaySmallBlocks"           , Category = "BuildingBlocks"               , Ref = ""                             }},
            {  1691, new EpbBlockType(){Id =  1691, Name = "WalkwayLargeBlocks"           , Category = "BuildingBlocks"               , Ref = ""                             }},
            {  1692, new EpbBlockType(){Id =  1692, Name = "RampLargeBlocks"              , Category = "Devices"                      , Ref = ""                             }},
            {  1693, new EpbBlockType(){Id =  1693, Name = "RampSmallBlocks"              , Category = "Devices"                      , Ref = ""                             }},
            {  1694, new EpbBlockType(){Id =  1694, Name = "LandingGearSVRetDouble"       , Category = "Devices"                      , Ref = "LandinggearSV"                }},
            {  1695, new EpbBlockType(){Id =  1695, Name = "LandingGearSVSideStrut"       , Category = "Devices"                      , Ref = "LandinggearSV"                }},
            {  1696, new EpbBlockType(){Id =  1696, Name = "LandingGearSVSingle"          , Category = "Devices"                      , Ref = "LandinggearSV"                }},
            {  1697, new EpbBlockType(){Id =  1697, Name = "LandingGearSVRetSkid"         , Category = "Devices"                      , Ref = "LandinggearSV"                }},
            {  1698, new EpbBlockType(){Id =  1698, Name = "LandingGearSVRetLargeDouble"  , Category = "Devices"                      , Ref = "LandinggearSV"                }},
            {  1699, new EpbBlockType(){Id =  1699, Name = "LandingGearSVRetLargeDoubleV2", Category = "Devices"                      , Ref = "LandinggearSV"                }},
            {  1700, new EpbBlockType(){Id =  1700, Name = "LandingGearSVRetLargeDoubleV3", Category = "Devices"                      , Ref = "LandinggearSV"                }},
            {  1701, new EpbBlockType(){Id =  1701, Name = "LandingGearSVRetLargeSingle"  , Category = "Devices"                      , Ref = "LandinggearSV"                }},
            {  1702, new EpbBlockType(){Id =  1702, Name = "LandingGearSVRetLargeSingleV2", Category = "Devices"                      , Ref = "LandinggearSV"                }},
            {  1703, new EpbBlockType(){Id =  1703, Name = "LandingGearSVRetLargeSingleV3", Category = "Devices"                      , Ref = "LandinggearSV"                }},
            {  1704, new EpbBlockType(){Id =  1704, Name = "LandingGearCVRetLargeDouble"  , Category = "Devices"                      , Ref = "LandinggearMSHeavy"           }},
            {  1705, new EpbBlockType(){Id =  1705, Name = "LandingGearCVRetLargeSingle"  , Category = "Devices"                      , Ref = "LandinggearMSHeavy"           }},
            {  1706, new EpbBlockType(){Id =  1706, Name = "BoardingRampBlocks"           , Category = "Devices"                      , Ref = ""                             }},
            {  1707, new EpbBlockType(){Id =  1707, Name = "BoardingRamp1x2x3"            , Category = "Devices"                      , Ref = "RampTemplate"                 }},
            {  1708, new EpbBlockType(){Id =  1708, Name = "BoardingRamp2x2x3"            , Category = "Devices"                      , Ref = "RampTemplate"                 }},
            {  1709, new EpbBlockType(){Id =  1709, Name = "BoardingRamp3x2x3"            , Category = "Devices"                      , Ref = "RampTemplate"                 }},
            {  1710, new EpbBlockType(){Id =  1710, Name = "BoardingRamp3x3x5"            , Category = "Devices"                      , Ref = "RampTemplate"                 }}, // Removed in A10.1.0-2470
            {  1711, new EpbBlockType(){Id =  1711, Name = "ContainerLargeBlocks"         , Category = "Devices"                      , Ref = ""                             }},
            {  1712, new EpbBlockType(){Id =  1712, Name = "ContainerSmallBlocks"         , Category = "Devices"                      , Ref = ""                             }},
            {  1713, new EpbBlockType(){Id =  1713, Name = "ContainerMS01Large"           , Category = "Devices"                      , Ref = "ContainerMS01"                }},
            {  1714, new EpbBlockType(){Id =  1714, Name = "ScifiContainer1Large"         , Category = "Devices"                      , Ref = "ContainerMS01"                }},
            {  1715, new EpbBlockType(){Id =  1715, Name = "ScifiContainer2Large"         , Category = "Devices"                      , Ref = "ContainerMS01"                }},
            {  1716, new EpbBlockType(){Id =  1716, Name = "ScifiContainerPowerLarge"     , Category = "Devices"                      , Ref = "ContainerMS01"                }},
            {  1717, new EpbBlockType(){Id =  1717, Name = "SentryGun01Retract"           , Category = "Weapons/Items"                , Ref = "SentryGun01RetractV2"         }},
            {  1718, new EpbBlockType(){Id =  1718, Name = "SentryGun05Retract"           , Category = "Weapons/Items"                , Ref = "SentryGun05RetractV2"         }},
            {  1719, new EpbBlockType(){Id =  1719, Name = "DecoVesselBlocks"             , Category = "Deco Blocks"                  , Ref = ""                             }},
            {  1720, new EpbBlockType(){Id =  1720, Name = "SVDecoAeroblister01"          , Category = "Deco Blocks"                  , Ref = ""                             }},
            {  1721, new EpbBlockType(){Id =  1721, Name = "SVDecoAirbrake01"             , Category = "Deco Blocks"                  , Ref = "SVDecoAeroblister01"          }},
            {  1722, new EpbBlockType(){Id =  1722, Name = "SVDecoAntenna01"              , Category = "Deco Blocks"                  , Ref = "SVDecoAeroblister01"          }},
            {  1723, new EpbBlockType(){Id =  1723, Name = "SVDecoAntenna02"              , Category = "Deco Blocks"                  , Ref = "SVDecoAeroblister01"          }},
            {  1724, new EpbBlockType(){Id =  1724, Name = "SVDecoArmor1x"                , Category = "Deco Blocks"                  , Ref = "SVDecoAeroblister01"          }},
            {  1725, new EpbBlockType(){Id =  1725, Name = "SVDecoArmor2x"                , Category = "Deco Blocks"                  , Ref = "SVDecoAeroblister01"          }},
            {  1726, new EpbBlockType(){Id =  1726, Name = "SVDecoFin01"                  , Category = "Deco Blocks"                  , Ref = "SVDecoAeroblister01"          }},
            {  1727, new EpbBlockType(){Id =  1727, Name = "SVDecoFin02"                  , Category = "Deco Blocks"                  , Ref = "SVDecoAeroblister01"          }},
            {  1728, new EpbBlockType(){Id =  1728, Name = "SVDecoFin03"                  , Category = "Deco Blocks"                  , Ref = "SVDecoAeroblister01"          }},
            {  1729, new EpbBlockType(){Id =  1729, Name = "SVDecoGreeble01"              , Category = "Deco Blocks"                  , Ref = "SVDecoAeroblister01"          }},
            {  1730, new EpbBlockType(){Id =  1730, Name = "SVDecoGreeble02"              , Category = "Deco Blocks"                  , Ref = "SVDecoAeroblister01"          }},
            {  1731, new EpbBlockType(){Id =  1731, Name = "SVDecoGreeble03"              , Category = "Deco Blocks"                  , Ref = "SVDecoAeroblister01"          }},
            {  1732, new EpbBlockType(){Id =  1732, Name = "SVDecoIntake01"               , Category = "Deco Blocks"                  , Ref = "SVDecoAeroblister01"          }},
            {  1733, new EpbBlockType(){Id =  1733, Name = "SVDecoIntake02"               , Category = "Deco Blocks"                  , Ref = "SVDecoAeroblister01"          }},
            {  1734, new EpbBlockType(){Id =  1734, Name = "SVDecoLightslot2x"            , Category = "Deco Blocks"                  , Ref = "SVDecoAeroblister01"          }},
            {  1735, new EpbBlockType(){Id =  1735, Name = "SVDecoLightslot3x"            , Category = "Deco Blocks"                  , Ref = "SVDecoAeroblister01"          }},
            {  1736, new EpbBlockType(){Id =  1736, Name = "SVDecoStrake01"               , Category = "Deco Blocks"                  , Ref = "SVDecoAeroblister01"          }},
            {  1737, new EpbBlockType(){Id =  1737, Name = "SVDecoStrake02"               , Category = "Deco Blocks"                  , Ref = "SVDecoAeroblister01"          }},
            {  1738, new EpbBlockType(){Id =  1738, Name = "SVDecoVent01"                 , Category = "Deco Blocks"                  , Ref = "SVDecoAeroblister01"          }},
            {  1739, new EpbBlockType(){Id =  1739, Name = "DecoTribalBlocks"             , Category = "Deco Blocks"                  , Ref = "DecoBlocks"                   }},
            {  1740, new EpbBlockType(){Id =  1740, Name = "TribalBarrels"                , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1741, new EpbBlockType(){Id =  1741, Name = "TribalBarrow"                 , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1742, new EpbBlockType(){Id =  1742, Name = "TribalBaskets"                , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1743, new EpbBlockType(){Id =  1743, Name = "TribalBed1"                   , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1744, new EpbBlockType(){Id =  1744, Name = "TribalBed2"                   , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1745, new EpbBlockType(){Id =  1745, Name = "TribalBookcase1"              , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1746, new EpbBlockType(){Id =  1746, Name = "TribalBookcase2"              , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1747, new EpbBlockType(){Id =  1747, Name = "TribalBuckets"                , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1748, new EpbBlockType(){Id =  1748, Name = "TribalCabinet1"               , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1749, new EpbBlockType(){Id =  1749, Name = "TribalCabinet2"               , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1750, new EpbBlockType(){Id =  1750, Name = "TribalCauldron"               , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1751, new EpbBlockType(){Id =  1751, Name = "TribalDryFish"                , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1752, new EpbBlockType(){Id =  1752, Name = "TribalLoom"                   , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1753, new EpbBlockType(){Id =  1753, Name = "TribalOven"                   , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1754, new EpbBlockType(){Id =  1754, Name = "TribalSacks"                  , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1755, new EpbBlockType(){Id =  1755, Name = "TribalTable1"                 , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1756, new EpbBlockType(){Id =  1756, Name = "TribalTable2"                 , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1757, new EpbBlockType(){Id =  1757, Name = "TribalWoodSaw"                , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1758, new EpbBlockType(){Id =  1758, Name = "TribalTrunkAxe"               , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1759, new EpbBlockType(){Id =  1759, Name = "TribalTorch"                  , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1760, new EpbBlockType(){Id =  1760, Name = "TribalFirepit"                , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1761, new EpbBlockType(){Id =  1761, Name = "TribalFirewood"               , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1762, new EpbBlockType(){Id =  1762, Name = "TribalBoat"                   , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1763, new EpbBlockType(){Id =  1763, Name = "TribalChair"                  , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1764, new EpbBlockType(){Id =  1764, Name = "TribalAnvil"                  , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1765, new EpbBlockType(){Id =  1765, Name = "TribalCauldron2"              , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1766, new EpbBlockType(){Id =  1766, Name = "TribalHearth"                 , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1767, new EpbBlockType(){Id =  1767, Name = "TribalTub"                    , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1768, new EpbBlockType(){Id =  1768, Name = "TribalBox"                    , Category = "Deco Blocks"                  , Ref = "DecoTemplate"                 }},
            {  1769, new EpbBlockType(){Id =  1769, Name = "ContainerMS01Small"           , Category = "Devices"                      , Ref = "ContainerMS01"                }},
            {  1770, new EpbBlockType(){Id =  1770, Name = "ScifiContainer1Small"         , Category = "Devices"                      , Ref = "ContainerMS01"                }},
            {  1771, new EpbBlockType(){Id =  1771, Name = "ScifiContainer2Small"         , Category = "Devices"                      , Ref = "ContainerMS01"                }},
            {  1772, new EpbBlockType(){Id =  1772, Name = "ScifiContainerPowerSmall"     , Category = "Devices"                      , Ref = "ContainerMS01"                }},
            {  1773, new EpbBlockType(){Id =  1773, Name = "TurretEnemyBallista"          , Category = "Weapons/Items"                , Ref = "TurretIONCannon"              }},
            {  1774, new EpbBlockType(){Id =  1774, Name = "ThrusterGVRoundNormalT2"      , Category = "Devices"                      , Ref = "ThrusterGVRoundNormal"        }},
            {  1775, new EpbBlockType(){Id =  1775, Name = "ThrusterGVRoundLarge"         , Category = "Devices"                      , Ref = "ThrusterGVRoundNormal"        }},
            {  1776, new EpbBlockType(){Id =  1776, Name = "ThrusterGVRoundLargeT2"       , Category = "Devices"                      , Ref = "ThrusterGVRoundNormal"        }},
            {  1777, new EpbBlockType(){Id =  1777, Name = "CargoPalette01"               , Category = "Devices"                      , Ref = "ContainerMS01"                }},
            {  1778, new EpbBlockType(){Id =  1778, Name = "CargoPalette02"               , Category = "Devices"                      , Ref = "ContainerMS01"                }},
            {  1779, new EpbBlockType(){Id =  1779, Name = "CargoPalette03"               , Category = "Devices"                      , Ref = "ContainerMS01"                }},
            {  1780, new EpbBlockType(){Id =  1780, Name = "CargoPalette04"               , Category = "Devices"                      , Ref = "ContainerMS01"                }},
            {  1782, new EpbBlockType(){Id =  1782, Name = "WoodExtended"                 , Category = "BuildingBlocks"               , Ref = "WoodFull"                     }},
            {  1783, new EpbBlockType(){Id =  1783, Name = "ConcreteExtended"             , Category = "BuildingBlocks"               , Ref = "ConcreteFull"                 }},
            {  1784, new EpbBlockType(){Id =  1784, Name = "ConcreteArmoredExtended"      , Category = "BuildingBlocks"               , Ref = "ConcreteArmoredFull"          }},
            {  1785, new EpbBlockType(){Id =  1785, Name = "PlasticExtendedLarge"         , Category = "BuildingBlocks"               , Ref = "PlasticFullLarge"             }},
            {  1786, new EpbBlockType(){Id =  1786, Name = "HullExtendedLarge"            , Category = "BuildingBlocks"               , Ref = "HullFullLarge"                }},
            {  1787, new EpbBlockType(){Id =  1787, Name = "HullArmoredExtendedLarge"     , Category = "BuildingBlocks"               , Ref = "HullArmoredFullLarge"         }},
            {  1788, new EpbBlockType(){Id =  1788, Name = "HullCombatExtendedLarge"      , Category = "BuildingBlocks"               , Ref = "HullCombatFullLarge"          }},
            {  1789, new EpbBlockType(){Id =  1789, Name = "AlienExtended"                , Category = "BuildingBlocks"               , Ref = "AlienFull"                    }},
            {  1790, new EpbBlockType(){Id =  1790, Name = "PlasticExtendedSmall"         , Category = "BuildingBlocks"               , Ref = "PlasticFullSmall"             }},
            {  1791, new EpbBlockType(){Id =  1791, Name = "HullExtendedSmall"            , Category = "BuildingBlocks"               , Ref = "HullFullSmall"                }},
            {  1792, new EpbBlockType(){Id =  1792, Name = "HullArmoredExtendedSmall"     , Category = "BuildingBlocks"               , Ref = "HullArmoredFullSmall"         }},
            {  1793, new EpbBlockType(){Id =  1793, Name = "HullCombatExtendedSmall"      , Category = "BuildingBlocks"               , Ref = "HullCombatFullSmall"          }},
            {  1794, new EpbBlockType(){Id =  1794, Name = "AlienExtendedLarge"           , Category = "BuildingBlocks"               , Ref = "AlienFullLarge"               }},
            {  1795, new EpbBlockType(){Id =  1795, Name = "ContainerMS02Large"           , Category = "Devices"                      , Ref = "ContainerMS01"                }},
            {  1796, new EpbBlockType(){Id =  1796, Name = "ContainerMS03Large"           , Category = "Devices"                      , Ref = "ContainerMS01"                }},
            {  1797, new EpbBlockType(){Id =  1797, Name = "ContainerMS04Large"           , Category = "Devices"                      , Ref = "ContainerMS01"                }},
            {  1798, new EpbBlockType(){Id =  1798, Name = "SVDecoVent02"                 , Category = "Deco Blocks"                  , Ref = "SVDecoAeroblister01"          }},
            {  1800, new EpbBlockType(){Id =  1800, Name = "CockpitBlocksSVT2"            , Category = "Devices"                      , Ref = ""                             }},
            {  1801, new EpbBlockType(){Id =  1801, Name = "CockpitSV01T2"                , Category = "Devices"                      , Ref = "CockpitSV01"                  }},
            {  1802, new EpbBlockType(){Id =  1802, Name = "CockpitSV_ShortRangeT2"       , Category = "Devices"                      , Ref = "CockpitSV_ShortRange"         }},
            {  1803, new EpbBlockType(){Id =  1803, Name = "CockpitSV02NewT2"             , Category = "Devices"                      , Ref = "CockpitSV02New"               }},
            {  1804, new EpbBlockType(){Id =  1804, Name = "CockpitSV04T2"                , Category = "Devices"                      , Ref = "CockpitSV04"                  }},
            {  1805, new EpbBlockType(){Id =  1805, Name = "CockpitSV05NewT2"             , Category = "Devices"                      , Ref = "CockpitSV05New"               }},
            {  1806, new EpbBlockType(){Id =  1806, Name = "CockpitSV06T2"                , Category = "Devices"                      , Ref = "CockpitSV06"                  }},
            {  1807, new EpbBlockType(){Id =  1807, Name = "CockpitSV07NewT2"             , Category = "Devices"                      , Ref = "CockpitSV07New"               }},
            {  1808, new EpbBlockType(){Id =  1808, Name = "ShieldGeneratorBA"            , Category = "Devices"                      , Ref = ""                             }},
            {  1809, new EpbBlockType(){Id =  1809, Name = "ShieldGeneratorCV"            , Category = "Devices"                      , Ref = "ShieldGeneratorBA"            }},
            {  1810, new EpbBlockType(){Id =  1810, Name = "ShieldGeneratorSV"            , Category = "Devices"                      , Ref = "ShieldGeneratorBA"            }},
            {  1811, new EpbBlockType(){Id =  1811, Name = "ShieldGeneratorCVT2"          , Category = "Devices"                      , Ref = "ShieldGeneratorCV"            }},
            {  1812, new EpbBlockType(){Id =  1812, Name = "ShieldGeneratorBAT2"          , Category = "Devices"                      , Ref = "ShieldGeneratorBA"            }},
            {  1813, new EpbBlockType(){Id =  1813, Name = "ShieldGeneratorPOI"           , Category = "Devices"                      , Ref = "ShieldGeneratorBA"            }},
            {  1814, new EpbBlockType(){Id =  1814, Name = "ContainerMS05Large"           , Category = "Devices"                      , Ref = "ContainerMS01"                }},
            {  1815, new EpbBlockType(){Id =  1815, Name = "DoorSingleLArmored"           , Category = "Devices"                      , Ref = "DoorArmored"                  }},
            {  1816, new EpbBlockType(){Id =  1816, Name = "DoorSingleGlassLArmored"      , Category = "Devices"                      , Ref = "DoorArmored"                  }},
            {  1817, new EpbBlockType(){Id =  1817, Name = "DoorSingleGlassFullLArmored"  , Category = "Devices"                      , Ref = "DoorArmored"                  }},
            {  1818, new EpbBlockType(){Id =  1818, Name = "BoardingRamp1x4x6"            , Category = "Devices"                      , Ref = "RampTemplate"                 }},
            {  1819, new EpbBlockType(){Id =  1819, Name = "BoardingRamp2x4x6"            , Category = "Devices"                      , Ref = "RampTemplate"                 }},
            {  1820, new EpbBlockType(){Id =  1820, Name = "BoardingRamp3x4x6"            , Category = "Devices"                      , Ref = "RampTemplate"                 }},
            {  1821, new EpbBlockType(){Id =  1821, Name = "BoardingRamp5x4x6"            , Category = "Devices"                      , Ref = "RampTemplate"                 }},
            {  1822, new EpbBlockType(){Id =  1822, Name = "BoardingRamp5x4x9"            , Category = "Devices"                      , Ref = "RampTemplate"                 }},
            {  1823, new EpbBlockType(){Id =  1823, Name = "BoardingRamp8x6x12"           , Category = "Devices"                      , Ref = "RampTemplate"                 }},
            {  1824, new EpbBlockType(){Id =  1824, Name = "WoodExtended2"                , Category = "BuildingBlocks"               , Ref = "WoodFull"                     }},
            {  1825, new EpbBlockType(){Id =  1825, Name = "ConcreteExtended2"            , Category = "BuildingBlocks"               , Ref = "ConcreteFull"                 }},
            {  1826, new EpbBlockType(){Id =  1826, Name = "ConcreteArmoredExtended2"     , Category = "BuildingBlocks"               , Ref = "ConcreteArmoredFull"          }},
            {  1827, new EpbBlockType(){Id =  1827, Name = "PlasticExtendedLarge2"        , Category = "BuildingBlocks"               , Ref = "PlasticFullLarge"             }},
            {  1828, new EpbBlockType(){Id =  1828, Name = "HullExtendedLarge2"           , Category = "BuildingBlocks"               , Ref = "HullFullLarge"                }},
            {  1829, new EpbBlockType(){Id =  1829, Name = "HullArmoredExtendedLarge2"    , Category = "BuildingBlocks"               , Ref = "HullArmoredFullLarge"         }},
            {  1830, new EpbBlockType(){Id =  1830, Name = "HullCombatExtendedLarge2"     , Category = "BuildingBlocks"               , Ref = "HullCombatFullLarge"          }},
            {  1831, new EpbBlockType(){Id =  1831, Name = "AlienExtended2"               , Category = "BuildingBlocks"               , Ref = "AlienFull"                    }},
            {  1832, new EpbBlockType(){Id =  1832, Name = "PlasticExtendedSmall2"        , Category = "BuildingBlocks"               , Ref = "PlasticFullSmall"             }},
            {  1833, new EpbBlockType(){Id =  1833, Name = "HullExtendedSmall2"           , Category = "BuildingBlocks"               , Ref = "HullFullSmall"                }},
            {  1834, new EpbBlockType(){Id =  1834, Name = "HullArmoredExtendedSmall2"    , Category = "BuildingBlocks"               , Ref = "HullArmoredFullSmall"         }},
            {  1835, new EpbBlockType(){Id =  1835, Name = "HullCombatExtendedSmall2"     , Category = "BuildingBlocks"               , Ref = "HullCombatFullSmall"          }},
            {  1836, new EpbBlockType(){Id =  1836, Name = "AlienExtendedLarge2"          , Category = "BuildingBlocks"               , Ref = "AlienFullLarge"               }},
            {  1837, new EpbBlockType(){Id =  1837, Name = "WoodExtended3"                , Category = "BuildingBlocks"               , Ref = "WoodFull"                     }},
            {  1838, new EpbBlockType(){Id =  1838, Name = "ConcreteExtended3"            , Category = "BuildingBlocks"               , Ref = "ConcreteFull"                 }},
            {  1839, new EpbBlockType(){Id =  1839, Name = "ConcreteArmoredExtended3"     , Category = "BuildingBlocks"               , Ref = "ConcreteArmoredFull"          }},
            {  1840, new EpbBlockType(){Id =  1840, Name = "PlasticExtendedLarge3"        , Category = "BuildingBlocks"               , Ref = "PlasticFullLarge"             }},
            {  1841, new EpbBlockType(){Id =  1841, Name = "HullExtendedLarge3"           , Category = "BuildingBlocks"               , Ref = "HullFullLarge"                }},
            {  1842, new EpbBlockType(){Id =  1842, Name = "HullArmoredExtendedLarge3"    , Category = "BuildingBlocks"               , Ref = "HullArmoredFullLarge"         }},
            {  1843, new EpbBlockType(){Id =  1843, Name = "HullCombatExtendedLarge3"     , Category = "BuildingBlocks"               , Ref = "HullCombatFullLarge"          }},
            {  1844, new EpbBlockType(){Id =  1844, Name = "AlienExtended3"               , Category = "BuildingBlocks"               , Ref = "AlienFull"                    }},
            {  1845, new EpbBlockType(){Id =  1845, Name = "PlasticExtendedSmall3"        , Category = "BuildingBlocks"               , Ref = "PlasticFullSmall"             }},
            {  1846, new EpbBlockType(){Id =  1846, Name = "HullExtendedSmall3"           , Category = "BuildingBlocks"               , Ref = "HullFullSmall"                }},
            {  1847, new EpbBlockType(){Id =  1847, Name = "HullArmoredExtendedSmall3"    , Category = "BuildingBlocks"               , Ref = "HullArmoredFullSmall"         }},
            {  1848, new EpbBlockType(){Id =  1848, Name = "HullCombatExtendedSmall3"     , Category = "BuildingBlocks"               , Ref = "HullCombatFullSmall"          }},
            {  1849, new EpbBlockType(){Id =  1849, Name = "AlienExtendedLarge3"          , Category = "BuildingBlocks"               , Ref = "AlienFullLarge"               }},
            {  1850, new EpbBlockType(){Id =  1850, Name = "WoodExtended4"                , Category = "BuildingBlocks"               , Ref = "WoodFull"                     }},
            {  1851, new EpbBlockType(){Id =  1851, Name = "ConcreteExtended4"            , Category = "BuildingBlocks"               , Ref = "ConcreteFull"                 }},
            {  1852, new EpbBlockType(){Id =  1852, Name = "ConcreteArmoredExtended4"     , Category = "BuildingBlocks"               , Ref = "ConcreteArmoredFull"          }},
            {  1853, new EpbBlockType(){Id =  1853, Name = "PlasticExtendedLarge4"        , Category = "BuildingBlocks"               , Ref = "PlasticFullLarge"             }},
            {  1854, new EpbBlockType(){Id =  1854, Name = "HullExtendedLarge4"           , Category = "BuildingBlocks"               , Ref = "HullFullLarge"                }},
            {  1855, new EpbBlockType(){Id =  1855, Name = "HullArmoredExtendedLarge4"    , Category = "BuildingBlocks"               , Ref = "HullArmoredFullLarge"         }},
            {  1856, new EpbBlockType(){Id =  1856, Name = "HullCombatExtendedLarge4"     , Category = "BuildingBlocks"               , Ref = "HullCombatFullLarge"          }},
            {  1857, new EpbBlockType(){Id =  1857, Name = "AlienExtended4"               , Category = "BuildingBlocks"               , Ref = "AlienFull"                    }},
            {  1858, new EpbBlockType(){Id =  1858, Name = "PlasticExtendedSmall4"        , Category = "BuildingBlocks"               , Ref = "PlasticFullSmall"             }},
            {  1859, new EpbBlockType(){Id =  1859, Name = "HullExtendedSmall4"           , Category = "BuildingBlocks"               , Ref = "HullFullSmall"                }},
            {  1860, new EpbBlockType(){Id =  1860, Name = "HullArmoredExtendedSmall4"    , Category = "BuildingBlocks"               , Ref = "HullArmoredFullSmall"         }},
            {  1861, new EpbBlockType(){Id =  1861, Name = "HullCombatExtendedSmall4"     , Category = "BuildingBlocks"               , Ref = "HullCombatFullSmall"          }},
            {  1862, new EpbBlockType(){Id =  1862, Name = "AlienExtendedLarge4"          , Category = "BuildingBlocks"               , Ref = "AlienFullLarge"               }},
            {  1863, new EpbBlockType(){Id =  1863, Name = "WoodExtended5"                , Category = "BuildingBlocks"               , Ref = "WoodFull"                     }},
            {  1864, new EpbBlockType(){Id =  1864, Name = "ConcreteExtended5"            , Category = "BuildingBlocks"               , Ref = "ConcreteFull"                 }},
            {  1865, new EpbBlockType(){Id =  1865, Name = "ConcreteArmoredExtended5"     , Category = "BuildingBlocks"               , Ref = "ConcreteArmoredFull"          }},
            {  1866, new EpbBlockType(){Id =  1866, Name = "PlasticExtendedLarge5"        , Category = "BuildingBlocks"               , Ref = "PlasticFullLarge"             }},
            {  1867, new EpbBlockType(){Id =  1867, Name = "HullExtendedLarge5"           , Category = "BuildingBlocks"               , Ref = "HullFullLarge"                }},
            {  1868, new EpbBlockType(){Id =  1868, Name = "HullArmoredExtendedLarge5"    , Category = "BuildingBlocks"               , Ref = "HullArmoredFullLarge"         }},
            {  1869, new EpbBlockType(){Id =  1869, Name = "HullCombatExtendedLarge5"     , Category = "BuildingBlocks"               , Ref = "HullCombatFullLarge"          }},
            {  1870, new EpbBlockType(){Id =  1870, Name = "AlienExtended5"               , Category = "BuildingBlocks"               , Ref = "AlienFull"                    }},
            {  1871, new EpbBlockType(){Id =  1871, Name = "PlasticExtendedSmall5"        , Category = "BuildingBlocks"               , Ref = "PlasticFullSmall"             }},
            {  1872, new EpbBlockType(){Id =  1872, Name = "HullExtendedSmall5"           , Category = "BuildingBlocks"               , Ref = "HullFullSmall"                }},
            {  1873, new EpbBlockType(){Id =  1873, Name = "HullArmoredExtendedSmall5"    , Category = "BuildingBlocks"               , Ref = "HullArmoredFullSmall"         }},
            {  1874, new EpbBlockType(){Id =  1874, Name = "HullCombatExtendedSmall5"     , Category = "BuildingBlocks"               , Ref = "HullCombatFullSmall"          }},
            {  1875, new EpbBlockType(){Id =  1875, Name = "AlienExtendedLarge5"          , Category = "BuildingBlocks"               , Ref = "AlienFullLarge"               }},
            {  1876, new EpbBlockType(){Id =  1876, Name = "DrillAttachmentLarge"         , Category = "Weapons/Items"                , Ref = "DrillAttachment"              }},
            {  1877, new EpbBlockType(){Id =  1877, Name = "Antenna06"                    , Category = "Deco Blocks"                  , Ref = "Antenna"                      }},
            {  1878, new EpbBlockType(){Id =  1878, Name = "Antenna07"                    , Category = "Deco Blocks"                  , Ref = "Antenna"                      }},
            {  1879, new EpbBlockType(){Id =  1879, Name = "Antenna08"                    , Category = "Deco Blocks"                  , Ref = "Antenna"                      }},
            {  1880, new EpbBlockType(){Id =  1880, Name = "Antenna09"                    , Category = "Deco Blocks"                  , Ref = "Antenna"                      }},
            {  1881, new EpbBlockType(){Id =  1881, Name = "Antenna10"                    , Category = "Deco Blocks"                  , Ref = "Antenna"                      }},
            {  1882, new EpbBlockType(){Id =  1882, Name = "Antenna11"                    , Category = "Deco Blocks"                  , Ref = "Antenna"                      }},
            {  1883, new EpbBlockType(){Id =  1883, Name = "Antenna12"                    , Category = "Deco Blocks"                  , Ref = "Antenna"                      }},
            {  1884, new EpbBlockType(){Id =  1884, Name = "Antenna13"                    , Category = "Deco Blocks"                  , Ref = "Antenna"                      }},
            {  1885, new EpbBlockType(){Id =  1885, Name = "DoorSingleRArmored"           , Category = "Devices"                      , Ref = "DoorArmored"                  }},
            {  1886, new EpbBlockType(){Id =  1886, Name = "DoorSingleGlassRArmored"      , Category = "Devices"                      , Ref = "DoorArmored"                  }},
            {  1887, new EpbBlockType(){Id =  1887, Name = "DoorSingleGlassFullRArmored"  , Category = "Devices"                      , Ref = "DoorArmored"                  }},
            {  1888, new EpbBlockType(){Id =  1888, Name = "ShieldGeneratorHV"            , Category = "Devices"                      , Ref = "ShieldGeneratorBA"            }},
            {  1889, new EpbBlockType(){Id =  1889, Name = "HangarDoor10x7"               , Category = "Devices"                      , Ref = "HangarDoor10x5"               }},
            {  1890, new EpbBlockType(){Id =  1890, Name = "HangarDoor5x4"                , Category = "Devices"                      , Ref = "HangarDoor10x5"               }},
            {  1891, new EpbBlockType(){Id =  1891, Name = "HangarDoor6x5"                , Category = "Devices"                      , Ref = "HangarDoor10x5"               }},
            {  1892, new EpbBlockType(){Id =  1892, Name = "HangarDoor7x6"                , Category = "Devices"                      , Ref = "HangarDoor10x5"               }},
            {  1893, new EpbBlockType(){Id =  1893, Name = "HangarDoor9x7"                , Category = "Devices"                      , Ref = "HangarDoor10x5"               }},
            {  1894, new EpbBlockType(){Id =  1894, Name = "ShutterDoor1x3"               , Category = "Devices"                      , Ref = "ShutterDoor1x1"               }},
            {  1895, new EpbBlockType(){Id =  1895, Name = "ShutterDoor1x4"               , Category = "Devices"                      , Ref = "ShutterDoor1x1"               }},
            {  1896, new EpbBlockType(){Id =  1896, Name = "ShutterDoor4x3"               , Category = "Devices"                      , Ref = "ShutterDoor1x1"               }},
            {  1897, new EpbBlockType(){Id =  1897, Name = "ShutterDoor1x5"               , Category = "Devices"                      , Ref = "ShutterDoor1x1"               }},
            {  1898, new EpbBlockType(){Id =  1898, Name = "ShutterDoor5x3"               , Category = "Devices"                      , Ref = "ShutterDoor1x1"               }},
            {  1899, new EpbBlockType(){Id =  1899, Name = "Ramp1x4x1"                    , Category = "Devices"                      , Ref = "RampTemplate"                 }},
            {  1900, new EpbBlockType(){Id =  1900, Name = "Ramp3x4x1"                    , Category = "Devices"                      , Ref = "RampTemplate"                 }},
            {  1901, new EpbBlockType(){Id =  1901, Name = "Ramp1x5x1"                    , Category = "Devices"                      , Ref = "RampTemplate"                 }},
            {  1902, new EpbBlockType(){Id =  1902, Name = "Ramp3x5x1"                    , Category = "Devices"                      , Ref = "RampTemplate"                 }},
            {  1903, new EpbBlockType(){Id =  1903, Name = "Ramp1x5x2"                    , Category = "Devices"                      , Ref = "RampTemplate"                 }},
            {  1904, new EpbBlockType(){Id =  1904, Name = "Ramp3x5x2"                    , Category = "Devices"                      , Ref = "RampTemplate"                 }},
            {  1905, new EpbBlockType(){Id =  1905, Name = "Ramp1x5x3"                    , Category = "Devices"                      , Ref = "RampTemplate"                 }},
            {  1906, new EpbBlockType(){Id =  1906, Name = "HeavyWindowK"                 , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1907, new EpbBlockType(){Id =  1907, Name = "HeavyWindowKInv"              , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1908, new EpbBlockType(){Id =  1908, Name = "HeavyWindowL"                 , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1909, new EpbBlockType(){Id =  1909, Name = "HeavyWindowLInv"              , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1910, new EpbBlockType(){Id =  1910, Name = "HeavyWindowM"                 , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1911, new EpbBlockType(){Id =  1911, Name = "HeavyWindowMInv"              , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1912, new EpbBlockType(){Id =  1912, Name = "HeavyWindowN"                 , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1913, new EpbBlockType(){Id =  1913, Name = "HeavyWindowNInv"              , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1914, new EpbBlockType(){Id =  1914, Name = "HeavyWindowO"                 , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1915, new EpbBlockType(){Id =  1915, Name = "HeavyWindowOInv"              , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1916, new EpbBlockType(){Id =  1916, Name = "HeavyWindowP"                 , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1917, new EpbBlockType(){Id =  1917, Name = "HeavyWindowPInv"              , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1918, new EpbBlockType(){Id =  1918, Name = "HeavyWindowQ"                 , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1919, new EpbBlockType(){Id =  1919, Name = "HeavyWindowQInv"              , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1920, new EpbBlockType(){Id =  1920, Name = "HeavyWindowR"                 , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1921, new EpbBlockType(){Id =  1921, Name = "HeavyWindowRInv"              , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1922, new EpbBlockType(){Id =  1922, Name = "HeavyWindowS"                 , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1923, new EpbBlockType(){Id =  1923, Name = "HeavyWindowSInv"              , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1924, new EpbBlockType(){Id =  1924, Name = "HeavyWindowT"                 , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1925, new EpbBlockType(){Id =  1925, Name = "HeavyWindowTInv"              , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1926, new EpbBlockType(){Id =  1926, Name = "HeavyWindowU"                 , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1927, new EpbBlockType(){Id =  1927, Name = "HeavyWindowUInv"              , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1942, new EpbBlockType(){Id =  1942, Name = "HeavyWindowPConnect"          , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1943, new EpbBlockType(){Id =  1943, Name = "HeavyWindowPConnectTwo"       , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1944, new EpbBlockType(){Id =  1944, Name = "HeavyWindowQConnect"          , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1945, new EpbBlockType(){Id =  1945, Name = "HeavyWindowQConnectTwo"       , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1946, new EpbBlockType(){Id =  1946, Name = "HeavyWindowQConnectThree"     , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1947, new EpbBlockType(){Id =  1947, Name = "HeavyWindowSConnect"          , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1948, new EpbBlockType(){Id =  1948, Name = "HeavyWindowTConnect"          , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1949, new EpbBlockType(){Id =  1949, Name = "HeavyWindowUConnect"          , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1950, new EpbBlockType(){Id =  1950, Name = "HeavyWindowPConnectInv"       , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1951, new EpbBlockType(){Id =  1951, Name = "HeavyWindowPConnectTwoInv"    , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1952, new EpbBlockType(){Id =  1952, Name = "HeavyWindowQConnectInv"       , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1953, new EpbBlockType(){Id =  1953, Name = "HeavyWindowQConnectTwoInv"    , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1954, new EpbBlockType(){Id =  1954, Name = "HeavyWindowQConnectThreeInv"  , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1955, new EpbBlockType(){Id =  1955, Name = "HeavyWindowSConnectInv"       , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1956, new EpbBlockType(){Id =  1956, Name = "HeavyWindowTConnectInv"       , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},
            {  1957, new EpbBlockType(){Id =  1957, Name = "HeavyWindowUConnectInv"       , Category = "BuildingBlocks"               , Ref = "HeavyWindowA"                 }},

            // -------- Manually added: ---------
            {    53, new EpbBlockType(){Id =    53, Name = "VoxelSathium"                 , Category = "Voxel/Materials"              , Ref = ""                             }},
            {    57, new EpbBlockType(){Id =    57, Name = "VoxelRock"                    , Category = "Voxel/Materials"              , Ref = ""                             }},
            {    79, new EpbBlockType(){Id =    79, Name = "VoxelCopper"                  , Category = "Voxel/Materials"              , Ref = ""                             }},
            {    80, new EpbBlockType(){Id =    80, Name = "VoxelPromethium"              , Category = "Voxel/Materials"              , Ref = ""                             }},
            {    81, new EpbBlockType(){Id =    81, Name = "VoxelIron"                    , Category = "Voxel/Materials"              , Ref = ""                             }},
            {    82, new EpbBlockType(){Id =    82, Name = "VoxelSilicon"                 , Category = "Voxel/Materials"              , Ref = ""                             }},
            {    83, new EpbBlockType(){Id =    83, Name = "VoxelNeodymium"               , Category = "Voxel/Materials"              , Ref = ""                             }},
            {    84, new EpbBlockType(){Id =    84, Name = "VoxelMagnesium"               , Category = "Voxel/Materials"              , Ref = ""                             }},
            {    85, new EpbBlockType(){Id =    85, Name = "VoxelCobalt"                  , Category = "Voxel/Materials"              , Ref = ""                             }},
            {    90, new EpbBlockType(){Id =    90, Name = "VoxelErestrum"                , Category = "Voxel/Materials"              , Ref = ""                             }},
            {    91, new EpbBlockType(){Id =    91, Name = "VoxelZascosium"               , Category = "Voxel/Materials"              , Ref = ""                             }},
            {    95, new EpbBlockType(){Id =    95, Name = "VoxelGold"                    , Category = "Voxel/Materials"              , Ref = ""                             }},
            {   114, new EpbBlockType(){Id =   114, Name = "VoxelPentaxid"                , Category = "Voxel/Materials"              , Ref = ""                             }},
            {  1681, new EpbBlockType(){Id =  1681, Name = "Filler"                       , Category = "Voxel/Materials"              , Ref = ""                             }},
        };

        /* These block types occur in Prefabs but are not listed in Config_Example.ecf:
BlockType=1
BlockType=1167
BlockType=1168
BlockType=1169
BlockType=1171
BlockType=1172
BlockType=1173
BlockType=1174
BlockType=1175
BlockType=1176
BlockType=1177
BlockType=1179
BlockType=1181
BlockType=1256
BlockType=1329
BlockType=1367
BlockType=1368
BlockType=1369
BlockType=1378
BlockType=1471
BlockType=1527
BlockType=1529
BlockType=1533
BlockType=1598
BlockType=1599
BlockType=1601
BlockType=1603
BlockType=1681
BlockType=264
BlockType=265
BlockType=269
BlockType=271
BlockType=274
BlockType=275
BlockType=276
BlockType=277
BlockType=285
BlockType=286
BlockType=290
BlockType=387
BlockType=388
BlockType=389
BlockType=390
BlockType=391
BlockType=392
BlockType=394
BlockType=395
BlockType=414
BlockType=415
BlockType=424
BlockType=425
BlockType=435
BlockType=437
BlockType=438
BlockType=441
BlockType=442
BlockType=443
BlockType=444
BlockType=463
BlockType=464
BlockType=465
BlockType=466
BlockType=467
BlockType=490
BlockType=494
BlockType=516
BlockType=517
BlockType=518
BlockType=519
BlockType=520
BlockType=521
BlockType=557
BlockType=568
BlockType=591
BlockType=592
BlockType=593
BlockType=594
BlockType=595
BlockType=596
BlockType=597
BlockType=598
BlockType=599
BlockType=600
BlockType=601
BlockType=602
BlockType=607
BlockType=608
BlockType=609
BlockType=611
BlockType=616
BlockType=624
BlockType=625
BlockType=626
BlockType=627
BlockType=628
BlockType=634
BlockType=639
BlockType=640
BlockType=641
BlockType=642
BlockType=643
BlockType=644
BlockType=645
BlockType=657
BlockType=659
BlockType=660
BlockType=662
BlockType=663
BlockType=664
BlockType=674
BlockType=675
BlockType=677
BlockType=679
BlockType=704
BlockType=705
BlockType=707
BlockType=709
BlockType=713
BlockType=780
BlockType=820
BlockType=821
BlockType=825
BlockType=826
BlockType=827
BlockType=828
BlockType=959
         */

        public static readonly Dictionary<UInt16, string[]> BlockVariants = new Dictionary<UInt16, String[]>()
        {
            { 381 /* HullFullSmall             */, new string[] {"Cube", "CutCornerE", "CutCornerB", "SlicedCornerA1", "CornerHalfB", "CornerSmallC", "CornerC", "CornerHalfA3", "RampCMedium", "RampA", "RampC", "CornerRoundB", "CornerRoundADouble", "RoundCornerA", "CubeRoundConnectorA", "EdgeRound", "Cylinder", "RampRoundFTriple", "RampRoundF", "SmallCornerRoundB", "SmallCornerRoundA", "SphereHalf", "Cone", "ConeB", "CutCornerC", "Cylinder6Way", "CornerRoundATriple", "CornerA", "CornerHalfA1", "CornerDoubleA3", "CornerSmallB", "PyramidA", }},
            { 382 /* HullThinSmall             */, new string[] {"Wall", "WallLShape", "WallSloped", "WallSloped3Corner", "WallSlopedC", "WallSlopedCMediumright", "WallSlopedAright", "WallSlopedCMediumleft", "WallSlopedAleft", "WallCornerRoundB", "WallSlopedRound", "WallEdgeRound", "WallEdgeRound3Way", "WallCornerRoundA", "WallCornerRoundC", "WallSloped3CornerLow", "WallCorner", "WallLow", "CubeHalf", "RampADouble", "RampCLow", "RampBMedium", "RampD", "CutCornerEMedium", "Beam", "CylinderThin", "CylinderThinTJoint", "CylinderL", "PipesFence", "FenceTop", "RampCHalf", "CornerHalfA3Medium", }},
            { 383 /* HullArmoredFullSmall      */, new string[] {"Cube", "CutCornerE", "CutCornerB", "SlicedCornerA1", "CornerHalfB", "CornerSmallC", "CornerC", "CornerHalfA3", "RampCMedium", "RampA", "RampC", "CornerRoundB", "CornerRoundADouble", "RoundCornerA", "CubeRoundConnectorA", "EdgeRound", "Cylinder", "RampRoundFTriple", "RampRoundF", "SmallCornerRoundB", "SmallCornerRoundA", "SphereHalf", "Cone", "ConeB", "CutCornerC", "Cylinder6Way", "CornerRoundATriple", "CornerA", "CornerHalfA1", "CornerDoubleA3", "CornerSmallB", "PyramidA", }},
            { 384 /* HullArmoredThinSmall      */, new string[] {"Wall", "WallLShape", "WallSloped", "WallSloped3Corner", "WallSlopedC", "WallSlopedCMediumright", "WallSlopedAright", "WallSlopedCMediumleft", "WallSlopedAleft", "WallCornerRoundB", "WallSlopedRound", "WallEdgeRound", "WallEdgeRound3Way", "WallCornerRoundA", "WallCornerRoundC", "WallSloped3CornerLow", "WallCorner", "WallLow", "CubeHalf", "RampADouble", "RampCLow", "RampBMedium", "RampD", "CutCornerEMedium", "Beam", "CylinderThin", "CylinderThinTJoint", "CylinderL", "PipesFence", "FenceTop", "RampCHalf", "CornerHalfA3Medium", }},
            { 397 /* WoodFull                  */, new string[] {"Cube", "CutCornerE", "CutCornerB", "SlicedCornerA1", "CornerHalfB", "CornerSmallC", "CornerC", "CornerHalfA3", "RampCMedium", "RampA", "RampC", "CornerRoundB", "CornerRoundADouble", "RoundCornerA", "CubeRoundConnectorA", "EdgeRound", "Cylinder", "RampRoundFTriple", "RampRoundF", "SmallCornerRoundB", "SmallCornerRoundA", "SphereHalf", "Cone", "ConeB", "CutCornerC", "Cylinder6Way", "CornerRoundATriple", "CornerA", "CornerHalfA1", "CornerDoubleA3", "CornerSmallB", "PyramidA", }},
            { 398 /* WoodThin                  */, new string[] {"Wall", "WallLShape", "WallSloped", "WallSloped3Corner", "WallSlopedC", "WallSlopedCMediumright", "WallSlopedAright", "WallSlopedCMediumleft", "WallSlopedAleft", "WallCornerRoundB", "WallSlopedRound", "WallEdgeRound", "WallEdgeRound3Way", "WallCornerRoundA", "WallCornerRoundC", "WallSloped3CornerLow", "WallCorner", "WallLow", "CubeHalf", "RampADouble", "RampCLow", "RampBMedium", "RampD", "CutCornerEMedium", "Beam", "CylinderThin", "CylinderThinTJoint", "CylinderL", "PipesFence", "FenceTop", "RampCHalf", "CornerHalfA3Medium", }},
            { 400 /* ConcreteFull              */, new string[] {"Cube", "CutCornerE", "CutCornerB", "SlicedCornerA1", "CornerHalfB", "CornerSmallC", "CornerC", "CornerHalfA3", "RampCMedium", "RampA", "RampC", "CornerRoundB", "CornerRoundADouble", "RoundCornerA", "CubeRoundConnectorA", "EdgeRound", "Cylinder", "RampRoundFTriple", "RampRoundF", "SmallCornerRoundB", "SmallCornerRoundA", "SphereHalf", "Cone", "ConeB", "CutCornerC", "Cylinder6Way", "CornerRoundATriple", "CornerA", "CornerHalfA1", "CornerDoubleA3", "CornerSmallB", "PyramidA", }},
            { 401 /* ConcreteThin              */, new string[] {"Wall", "WallLShape", "WallSloped", "WallSloped3Corner", "WallSlopedC", "WallSlopedCMediumright", "WallSlopedAright", "WallSlopedCMediumleft", "WallSlopedAleft", "WallCornerRoundB", "WallSlopedRound", "WallEdgeRound", "WallEdgeRound3Way", "WallCornerRoundA", "WallCornerRoundC", "WallSloped3CornerLow", "WallCorner", "WallLow", "CubeHalf", "RampADouble", "RampCLow", "RampBMedium", "RampD", "CutCornerEMedium", "Beam", "CylinderThin", "CylinderThinTJoint", "CylinderL", "PipesFence", "FenceTop", "RampCHalf", "CornerHalfA3Medium", }},
            { 403 /* HullFullLarge             */, new string[] {"Cube", "CutCornerE", "CutCornerB", "SlicedCornerA1", "CornerHalfB", "CornerSmallC", "CornerC", "CornerHalfA3", "RampCMedium", "RampA", "RampC", "CornerRoundB", "CornerRoundADouble", "RoundCornerA", "CubeRoundConnectorA", "EdgeRound", "Cylinder", "RampRoundFTriple", "RampRoundF", "SmallCornerRoundB", "SmallCornerRoundA", "SphereHalf", "Cone", "ConeB", "CutCornerC", "Cylinder6Way", "CornerRoundATriple", "CornerA", "CornerHalfA1", "CornerDoubleA3", "CornerSmallB", "PyramidA", }},
            { 404 /* HullThinLarge             */, new string[] {"Wall", "WallLShape", "WallSloped", "WallSloped3Corner", "WallSlopedC", "WallSlopedCMediumright", "WallSlopedAright", "WallSlopedCMediumleft", "WallSlopedAleft", "WallCornerRoundB", "WallSlopedRound", "WallEdgeRound", "WallEdgeRound3Way", "WallCornerRoundA", "WallCornerRoundC", "WallSloped3CornerLow", "WallCorner", "WallLow", "CubeHalf", "RampADouble", "RampCLow", "RampBMedium", "RampD", "CutCornerEMedium", "Beam", "CylinderThin", "CylinderThinTJoint", "CylinderL", "PipesFence", "FenceTop", "RampCHalf", "CornerHalfA3Medium", }},
            { 406 /* HullArmoredFullLarge      */, new string[] {"Cube", "CutCornerE", "CutCornerB", "SlicedCornerA1", "CornerHalfB", "CornerSmallC", "CornerC", "CornerHalfA3", "RampCMedium", "RampA", "RampC", "CornerRoundB", "CornerRoundADouble", "RoundCornerA", "CubeRoundConnectorA", "EdgeRound", "Cylinder", "RampRoundFTriple", "RampRoundF", "SmallCornerRoundB", "SmallCornerRoundA", "SphereHalf", "Cone", "ConeB", "CutCornerC", "Cylinder6Way", "CornerRoundATriple", "CornerA", "CornerHalfA1", "CornerDoubleA3", "CornerSmallB", "PyramidA", }},
            { 407 /* HullArmoredThinLarge      */, new string[] {"Wall", "WallLShape", "WallSloped", "WallSloped3Corner", "WallSlopedC", "WallSlopedCMediumright", "WallSlopedAright", "WallSlopedCMediumleft", "WallSlopedAleft", "WallCornerRoundB", "WallSlopedRound", "WallEdgeRound", "WallEdgeRound3Way", "WallCornerRoundA", "WallCornerRoundC", "WallSloped3CornerLow", "WallCorner", "WallLow", "CubeHalf", "RampADouble", "RampCLow", "RampBMedium", "RampD", "CutCornerEMedium", "Beam", "CylinderThin", "CylinderThinTJoint", "CylinderL", "PipesFence", "FenceTop", "RampCHalf", "CornerHalfA3Medium", }},
            { 409 /* AlienFull                 */, new string[] {"Cube", "CutCornerE", "CutCornerB", "SlicedCornerA1", "CornerHalfB", "CornerSmallC", "CornerC", "CornerHalfA3", "RampCMedium", "RampA", "RampC", "CornerRoundB", "CornerRoundADouble", "RoundCornerA", "CubeRoundConnectorA", "EdgeRound", "Cylinder", "RampRoundFTriple", "RampRoundF", "SmallCornerRoundB", "SmallCornerRoundA", "SphereHalf", "Cone", "ConeB", "CutCornerC", "Cylinder6Way", "CornerRoundATriple", "CornerA", "CornerHalfA1", "CornerDoubleA3", "CornerSmallB", "PyramidA", }},
            { 410 /* AlienThin                 */, new string[] {"Wall", "WallLShape", "WallSloped", "WallSloped3Corner", "WallSlopedC", "WallSlopedCMediumright", "WallSlopedAright", "WallSlopedCMediumleft", "WallSlopedAleft", "WallCornerRoundB", "WallSlopedRound", "WallEdgeRound", "WallEdgeRound3Way", "WallCornerRoundA", "WallCornerRoundC", "WallSloped3CornerLow", "WallCorner", "WallLow", "CubeHalf", "RampADouble", "RampCLow", "RampBMedium", "RampD", "CutCornerEMedium", "Beam", "CylinderThin", "CylinderThinTJoint", "CylinderL", "PipesFence", "FenceTop", "RampCHalf", "CornerHalfA3Medium", }},
            { 412 /* HullCombatFullLarge       */, new string[] {"Cube", "CutCornerE", "CutCornerB", "SlicedCornerA1", "CornerHalfB", "CornerSmallC", "CornerC", "CornerHalfA3", "RampCMedium", "RampA", "RampC", "CornerRoundB", "CornerRoundADouble", "RoundCornerA", "CubeRoundConnectorA", "EdgeRound", "Cylinder", "RampRoundFTriple", "RampRoundF", "SmallCornerRoundB", "SmallCornerRoundA", "SphereHalf", "Cone", "ConeB", "CutCornerC", "Cylinder6Way", "CornerRoundATriple", "CornerA", "CornerHalfA1", "CornerDoubleA3", "CornerSmallB", "PyramidA", }},
            { 413 /* HullCombatThinLarge       */, new string[] {"Wall", "WallLShape", "WallSloped", "WallSloped3Corner", "WallSlopedC", "WallSlopedCMediumright", "WallSlopedAright", "WallSlopedCMediumleft", "WallSlopedAleft", "WallCornerRoundB", "WallSlopedRound", "WallEdgeRound", "WallEdgeRound3Way", "WallCornerRoundA", "WallCornerRoundC", "WallSloped3CornerLow", "WallCorner", "WallLow", "CubeHalf", "RampADouble", "RampCLow", "RampBMedium", "RampD", "CutCornerEMedium", "Beam", "CylinderThin", "CylinderThinTJoint", "CylinderL", "PipesFence", "FenceTop", "RampCHalf", "CornerHalfA3Medium", }},
            {1125 /* StairShapes               */, new string[] {"Stairs1x1x1", "StairsCurved", "StairsCurvedLeft", }},
            {1126 /* StairShapesLong           */, new string[] {"Stairs1x2x1", }},
            {1323 /* ConcreteArmoredFull       */, new string[] {"Cube", "CutCornerE", "CutCornerB", "SlicedCornerA1", "CornerHalfB", "CornerSmallC", "CornerC", "CornerHalfA3", "RampCMedium", "RampA", "RampC", "CornerRoundB", "CornerRoundADouble", "RoundCornerA", "CubeRoundConnectorA", "EdgeRound", "Cylinder", "RampRoundFTriple", "RampRoundF", "SmallCornerRoundB", "SmallCornerRoundA", "SphereHalf", "Cone", "ConeB", "CutCornerC", "Cylinder6Way", "CornerRoundATriple", "CornerA", "CornerHalfA1", "CornerDoubleA3", "CornerSmallB", "PyramidA", }},
            {1324 /* ConcreteArmoredThin       */, new string[] {"Wall", "WallLShape", "WallSloped", "WallSloped3Corner", "WallSlopedC", "WallSlopedCMediumright", "WallSlopedAright", "WallSlopedCMediumleft", "WallSlopedAleft", "WallCornerRoundB", "WallSlopedRound", "WallEdgeRound", "WallEdgeRound3Way", "WallCornerRoundA", "WallCornerRoundC", "WallSloped3CornerLow", "WallCorner", "WallLow", "CubeHalf", "RampADouble", "RampCLow", "RampBMedium", "RampD", "CutCornerEMedium", "Beam", "CylinderThin", "CylinderThinTJoint", "CylinderL", "PipesFence", "FenceTop", "RampCHalf", "CornerHalfA3Medium", }},
            {1387 /* HullFullLargeDestroyed    */, new string[] {"CubeDestroyed", "CutCornerEDestroyed", "CutCornerBDestroyed", "SlicedCornerA1Destroyed", "CornerHalfBDestroyed", "CornerSmallCDestroyed", "CornerCDestroyed", "CornerHalfA3Destroyed", "RampCMediumDestroyed", "RampADestroyed", "RampCDestroyed", "CornerRoundBDestroyed", "CornerRoundADoubleDestroyed", "RoundCornerADestroyed", "CubeRoundConnectorADestroyed", "EdgeRoundDestroyed", "CylinderDestroyed", "RampRoundFTripleDestroyed", "RampRoundFDestroyed", "SmallCornerRoundBDestroyed", "SmallCornerRoundADestroyed", "SphereHalfDestroyed", "ConeDestroyed", "ConeBDestroyed", "CutCornerCDestroyed", "Cylinder6WayDestroyed", "CornerRoundATripleDestroyed", "CornerADestroyed", "CornerHalfA1Destroyed", "CornerDoubleA3Destroyed", "CornerSmallBDestroyed", "PyramidADestroyed", }},
            {1388 /* HullThinLargeDestroyed    */, new string[] {"WallDestroyed", "WallLShapeDestroyed", "WallSlopedDestroyed", "WallSloped3CornerDestroyed", "WallSlopedCDestroyed", "WallSlopedCMediumrightDestroyed", "WallSlopedArightDestroyed", "WallSlopedCMediumleftDestroyed", "WallSlopedAleftDestroyed", "WallCornerRoundBDestroyed", "WallSlopedRoundDestroyed", "WallEdgeRoundDestroyed", "WallEdgeRound3WayDestroyed", "WallCornerRoundADestroyed", "WallCornerRoundCDestroyed", "WallSloped3CornerLowDestroyed", "WallCornerDestroyed", "WallLowDestroyed", "CubeHalfDestroyed", "RampADoubleDestroyed", "RampCLowDestroyed", "RampBMediumDestroyed", "RampCHalfDestroyed", "CutCornerEMediumDestroyed", "BeamDestroyed", "CylinderThinDestroyed", "CylinderThinTJointDestroyed", "CylinderLDestroyed", "PipesFenceDestroyed", "FenceTopDestroyed", "RampCHalfDestroyed", }},
            {1390 /* HullFullSmallDestroyed    */, new string[] {"CubeDestroyed", "CutCornerEDestroyed", "CutCornerBDestroyed", "SlicedCornerA1Destroyed", "CornerHalfBDestroyed", "CornerSmallCDestroyed", "CornerCDestroyed", "CornerHalfA3Destroyed", "RampCMediumDestroyed", "RampADestroyed", "RampCDestroyed", "CornerRoundBDestroyed", "CornerRoundADoubleDestroyed", "RoundCornerADestroyed", "CubeRoundConnectorADestroyed", "EdgeRoundDestroyed", "CylinderDestroyed", "RampRoundFTripleDestroyed", "RampRoundFDestroyed", "SmallCornerRoundBDestroyed", "SmallCornerRoundADestroyed", "SphereHalfDestroyed", "ConeDestroyed", "ConeBDestroyed", "CutCornerCDestroyed", "Cylinder6WayDestroyed", "CornerRoundATripleDestroyed", "CornerADestroyed", "CornerHalfA1Destroyed", "CornerDoubleA3Destroyed", "CornerSmallBDestroyed", "PyramidADestroyed", }},
            {1391 /* HullThinSmallDestroyed    */, new string[] {"WallDestroyed", "WallLShapeDestroyed", "WallSlopedDestroyed", "WallSloped3CornerDestroyed", "WallSlopedCDestroyed", "WallSlopedCMediumrightDestroyed", "WallSlopedArightDestroyed", "WallSlopedCMediumleftDestroyed", "WallSlopedAleftDestroyed", "WallCornerRoundBDestroyed", "WallSlopedRoundDestroyed", "WallEdgeRoundDestroyed", "WallEdgeRound3WayDestroyed", "WallCornerRoundADestroyed", "WallCornerRoundCDestroyed", "WallSloped3CornerLowDestroyed", "WallCornerDestroyed", "WallLowDestroyed", "CubeHalfDestroyed", "RampADoubleDestroyed", "RampCLowDestroyed", "RampBMediumDestroyed", "RampCHalfDestroyed", "CutCornerEMediumDestroyed", "BeamDestroyed", "CylinderThinDestroyed", "CylinderThinTJointDestroyed", "CylinderLDestroyed", "PipesFenceDestroyed", "FenceTopDestroyed", "RampCHalfDestroyed", }},
            {1393 /* ConcreteFullDestroyed     */, new string[] {"CubeDestroyed", "CutCornerEDestroyed", "CutCornerBDestroyed", "SlicedCornerA1Destroyed", "CornerHalfBDestroyed", "CornerSmallCDestroyed", "CornerCDestroyed", "CornerHalfA3Destroyed", "RampCMediumDestroyed", "RampADestroyed", "RampCDestroyed", "CornerRoundBDestroyed", "CornerRoundADoubleDestroyed", "RoundCornerADestroyed", "CubeRoundConnectorADestroyed", "EdgeRoundDestroyed", "CylinderDestroyed", "RampRoundFTripleDestroyed", "RampRoundFDestroyed", "SmallCornerRoundBDestroyed", "SmallCornerRoundADestroyed", "SphereHalfDestroyed", "ConeDestroyed", "ConeBDestroyed", "CutCornerCDestroyed", "Cylinder6WayDestroyed", "CornerRoundATripleDestroyed", "CornerADestroyed", "CornerHalfA1Destroyed", "CornerDoubleA3Destroyed", "CornerSmallBDestroyed", "PyramidADestroyed", }},
            {1394 /* ConcreteThinDestroyed     */, new string[] {"WallDestroyed", "WallLShapeDestroyed", "WallSlopedDestroyed", "WallSloped3CornerDestroyed", "WallSlopedCDestroyed", "WallSlopedCMediumrightDestroyed", "WallSlopedArightDestroyed", "WallSlopedCMediumleftDestroyed", "WallSlopedAleftDestroyed", "WallCornerRoundBDestroyed", "WallSlopedRoundDestroyed", "WallEdgeRoundDestroyed", "WallEdgeRound3WayDestroyed", "WallCornerRoundADestroyed", "WallCornerRoundCDestroyed", "WallSloped3CornerLowDestroyed", "WallCornerDestroyed", "WallLowDestroyed", "CubeHalfDestroyed", "RampADoubleDestroyed", "RampCLowDestroyed", "RampBMediumDestroyed", "RampCHalfDestroyed", "CutCornerEMediumDestroyed", "BeamDestroyed", "CylinderThinDestroyed", "CylinderThinTJointDestroyed", "CylinderLDestroyed", "PipesFenceDestroyed", "FenceTopDestroyed", "RampCHalfDestroyed", }},
            {1396 /* AlienFullLarge            */, new string[] {"Cube", "CutCornerE", "CutCornerB", "SlicedCornerA1", "CornerHalfB", "CornerSmallC", "CornerC", "CornerHalfA3", "RampCMedium", "RampA", "RampC", "CornerRoundB", "CornerRoundADouble", "RoundCornerA", "CubeRoundConnectorA", "EdgeRound", "Cylinder", "RampRoundFTriple", "RampRoundF", "SmallCornerRoundB", "SmallCornerRoundA", "SphereHalf", "Cone", "ConeB", "CutCornerC", "Cylinder6Way", "CornerRoundATriple", "CornerA", "CornerHalfA1", "CornerDoubleA3", "CornerSmallB", "PyramidA", }},
            {1397 /* AlienThinLarge            */, new string[] {"Wall", "WallLShape", "WallSloped", "WallSloped3Corner", "WallSlopedC", "WallSlopedCMediumright", "WallSlopedAright", "WallSlopedCMediumleft", "WallSlopedAleft", "WallCornerRoundB", "WallSlopedRound", "WallEdgeRound", "WallEdgeRound3Way", "WallCornerRoundA", "WallCornerRoundC", "WallSloped3CornerLow", "WallCorner", "WallLow", "CubeHalf", "RampADouble", "RampCLow", "RampBMedium", "RampD", "CutCornerEMedium", "Beam", "CylinderThin", "CylinderThinTJoint", "CylinderL", "PipesFence", "FenceTop", "RampCHalf", "CornerHalfA3Medium", }},
            {1441 /* StairShapesShortConcrete  */, new string[] {"Stairs1x1x1", "StairsCurved", "StairsCurvedLeft", }},
            {1442 /* StairShapesLongConcrete   */, new string[] {"Stairs1x2x1", }},
            {1444 /* StairShapesShortWood      */, new string[] {"Stairs1x1x1", "StairsCurved", "StairsCurvedLeft", }},
            {1445 /* StairShapesLongWood       */, new string[] {"Stairs1x2x1", }},
            {1479 /* PlasticFullSmall          */, new string[] {"Cube", "CutCornerE", "CutCornerB", "SlicedCornerA1", "CornerHalfB", "CornerSmallC", "CornerC", "CornerHalfA3", "RampCMedium", "RampA", "RampC", "CornerRoundB", "CornerRoundADouble", "RoundCornerA", "CubeRoundConnectorA", "EdgeRound", "Cylinder", "RampRoundFTriple", "RampRoundF", "SmallCornerRoundB", "SmallCornerRoundA", "SphereHalf", "Cone", "ConeB", "CutCornerC", "Cylinder6Way", "CornerRoundATriple", "CornerA", "CornerHalfA1", "CornerDoubleA3", "CornerSmallB", "PyramidA", }},
            {1480 /* PlasticThinSmall          */, new string[] {"Wall", "WallLShape", "WallSloped", "WallSloped3Corner", "WallSlopedC", "WallSlopedCMediumright", "WallSlopedAright", "WallSlopedCMediumleft", "WallSlopedAleft", "WallCornerRoundB", "WallSlopedRound", "WallEdgeRound", "WallEdgeRound3Way", "WallCornerRoundA", "WallCornerRoundC", "WallSloped3CornerLow", "WallCorner", "WallLow", "CubeHalf", "RampADouble", "RampCLow", "RampBMedium", "RampD", "CutCornerEMedium", "Beam", "CylinderThin", "CylinderThinTJoint", "CylinderL", "PipesFence", "FenceTop", "RampCHalf", "CornerHalfA3Medium", }},
            {1482 /* PlasticFullLarge          */, new string[] {"Cube", "CutCornerE", "CutCornerB", "SlicedCornerA1", "CornerHalfB", "CornerSmallC", "CornerC", "CornerHalfA3", "RampCMedium", "RampA", "RampC", "CornerRoundB", "CornerRoundADouble", "RoundCornerA", "CubeRoundConnectorA", "EdgeRound", "Cylinder", "RampRoundFTriple", "RampRoundF", "SmallCornerRoundB", "SmallCornerRoundA", "SphereHalf", "Cone", "ConeB", "CutCornerC", "Cylinder6Way", "CornerRoundATriple", "CornerA", "CornerHalfA1", "CornerDoubleA3", "CornerSmallB", "PyramidA", }},
            {1483 /* PlasticThinLarge          */, new string[] {"Wall", "WallLShape", "WallSloped", "WallSloped3Corner", "WallSlopedC", "WallSlopedCMediumright", "WallSlopedAright", "WallSlopedCMediumleft", "WallSlopedAleft", "WallCornerRoundB", "WallSlopedRound", "WallEdgeRound", "WallEdgeRound3Way", "WallCornerRoundA", "WallCornerRoundC", "WallSloped3CornerLow", "WallCorner", "WallLow", "CubeHalf", "RampADouble", "RampCLow", "RampBMedium", "RampD", "CutCornerEMedium", "Beam", "CylinderThin", "CylinderThinTJoint", "CylinderL", "PipesFence", "FenceTop", "RampCHalf", "CornerHalfA3Medium", }},
            {1577 /* ExplosiveBlockFull        */, new string[] {"Cube", "CutCornerE", "CutCornerB", "SlicedCornerA1", "CornerHalfB", "CornerSmallC", "CornerC", "CornerHalfA3", "RampCMedium", "RampA", "RampC", "CornerRoundB", "CornerRoundADouble", "RoundCornerA", "CubeRoundConnectorA", "EdgeRound", "Cylinder", "RampRoundFTriple", "RampRoundF", "SmallCornerRoundB", "SmallCornerRoundA", "SphereHalf", "Cone", "ConeB", "CutCornerC", "Cylinder6Way", "CornerRoundATriple", "CornerA", "CornerHalfA1", "CornerDoubleA3", "CornerSmallB", "PyramidA", }},
            {1578 /* ExplosiveBlockThin        */, new string[] {"Wall", "WallLShape", "WallSloped", "WallSloped3Corner", "WallSlopedC", "WallSlopedCMediumright", "WallSlopedAright", "WallSlopedCMediumleft", "WallSlopedAleft", "WallCornerRoundB", "WallSlopedRound", "WallEdgeRound", "WallEdgeRound3Way", "WallCornerRoundA", "WallCornerRoundC", "WallSloped3CornerLow", "WallCorner", "WallLow", "CubeHalf", "RampADouble", "RampCLow", "RampBMedium", "RampD", "CutCornerEMedium", "Beam", "CylinderThin", "CylinderThinTJoint", "CylinderL", "PipesFence", "FenceTop", "RampCHalf", "CornerHalfA3Medium", }},
            {1579 /* ExplosiveBlock2Full       */, new string[] {"Cube", "CutCornerE", "CutCornerB", "SlicedCornerA1", "CornerHalfB", "CornerSmallC", "CornerC", "CornerHalfA3", "RampCMedium", "RampA", "RampC", "CornerRoundB", "CornerRoundADouble", "RoundCornerA", "CubeRoundConnectorA", "EdgeRound", "Cylinder", "RampRoundFTriple", "RampRoundF", "SmallCornerRoundB", "SmallCornerRoundA", "SphereHalf", "Cone", "ConeB", "CutCornerC", "Cylinder6Way", "CornerRoundATriple", "CornerA", "CornerHalfA1", "CornerDoubleA3", "CornerSmallB", "PyramidA", }},
            {1580 /* ExplosiveBlock2Thin       */, new string[] {"Wall", "WallLShape", "WallSloped", "WallSloped3Corner", "WallSlopedC", "WallSlopedCMediumright", "WallSlopedAright", "WallSlopedCMediumleft", "WallSlopedAleft", "WallCornerRoundB", "WallSlopedRound", "WallEdgeRound", "WallEdgeRound3Way", "WallCornerRoundA", "WallCornerRoundC", "WallSloped3CornerLow", "WallCorner", "WallLow", "CubeHalf", "RampADouble", "RampCLow", "RampBMedium", "RampD", "CutCornerEMedium", "Beam", "CylinderThin", "CylinderThinTJoint", "CylinderL", "PipesFence", "FenceTop", "RampCHalf", "CornerHalfA3Medium", }},
            {1595 /* HullCombatFullSmall       */, new string[] {"Cube", "CutCornerE", "CutCornerB", "SlicedCornerA1", "CornerHalfB", "CornerSmallC", "CornerC", "CornerHalfA3", "RampCMedium", "RampA", "RampC", "CornerRoundB", "CornerRoundADouble", "RoundCornerA", "CubeRoundConnectorA", "EdgeRound", "Cylinder", "RampRoundFTriple", "RampRoundF", "SmallCornerRoundB", "SmallCornerRoundA", "SphereHalf", "Cone", "ConeB", "CutCornerC", "Cylinder6Way", "CornerRoundATriple", "CornerA", "CornerHalfA1", "CornerDoubleA3", "CornerSmallB", "PyramidA", }},
            {1596 /* HullCombatThinSmall       */, new string[] {"Wall", "WallLShape", "WallSloped", "WallSloped3Corner", "WallSlopedC", "WallSlopedCMediumright", "WallSlopedAright", "WallSlopedCMediumleft", "WallSlopedAleft", "WallCornerRoundB", "WallSlopedRound", "WallEdgeRound", "WallEdgeRound3Way", "WallCornerRoundA", "WallCornerRoundC", "WallSloped3CornerLow", "WallCorner", "WallLow", "CubeHalf", "RampADouble", "RampCLow", "RampBMedium", "RampD", "CutCornerEMedium", "Beam", "CylinderThin", "CylinderThinTJoint", "CylinderL", "PipesFence", "FenceTop", "RampCHalf", "CornerHalfA3Medium", }},
            {1683 /* ContainerExtensionLarge   */, new string[] {"Cube", "CutCornerE", "CutCornerB", "SlicedCornerA1", "CornerHalfB", "CornerSmallC", "CornerC", "CornerHalfA3", "RampCMedium", "RampA", "RampC", "CornerRoundB", "CornerRoundADouble", "RoundCornerA", "CubeRoundConnectorA", "EdgeRound", "Cylinder", "SphereHalf", "Cone", "ConeB", "CutCornerC", "Cylinder6Way", "CornerRoundATriple", "CornerA", "CornerHalfA1", "CornerDoubleA3", "CornerSmallB", "PyramidA", "CubeHalf", "RampBMedium", "RampD", "CutCornerEMedium", }},
            {1685 /* ContainerExtensionSmall   */, new string[] {"Cube", "CutCornerE", "CutCornerB", "SlicedCornerA1", "CornerHalfB", "CornerSmallC", "CornerC", "CornerHalfA3", "RampCMedium", "RampA", "RampC", "CornerRoundB", "CornerRoundADouble", "RoundCornerA", "CubeRoundConnectorA", "EdgeRound", "Cylinder", "SphereHalf", "Cone", "ConeB", "CutCornerC", "Cylinder6Way", "CornerRoundATriple", "CornerA", "CornerHalfA1", "CornerDoubleA3", "CornerSmallB", "PyramidA", "CubeHalf", "RampBMedium", "RampD", "CutCornerEMedium", }},
            {1782 /* WoodExtended              */, new string[] {"CornerCMedium", "CornerSmallCMedium", "CornerSmallA", "SmallCornerRoundC", "RampRoundDDouble", "NotchedC", "NotchedA", "NotchedCMedium", "CubeQuarter", "CylinderThinXJoint", "CornerDoubleA1", "CutCornerA", "CornerDoubleB3", "SlicedCornerD", "EdgeRoundMedium", "RampB", "CornerRoundBMedium", "CornerRoundBLow", "CornerRoundAMedium", "CornerRoundALow", "EdgeRoundMediumHalf", "EdgeRoundLow", "PipesFenceDiagonal", "FenceTopDiagonal", "CubeRoundConnectorBleft", "CubeRoundConnectorBright", "CylinderRoundTransition", "WallEdge", "RampRoundConnectorBleft", "RampRoundConnectorBright", "RampRoundConnectorAleft", "RampRoundConnectorAright", }},
            {1783 /* ConcreteExtended          */, new string[] {"CornerCMedium", "CornerSmallCMedium", "CornerSmallA", "SmallCornerRoundC", "RampRoundDDouble", "NotchedC", "NotchedA", "NotchedCMedium", "CubeQuarter", "CylinderThinXJoint", "CornerDoubleA1", "CutCornerA", "CornerDoubleB3", "SlicedCornerD", "EdgeRoundMedium", "RampB", "CornerRoundBMedium", "CornerRoundBLow", "CornerRoundAMedium", "CornerRoundALow", "EdgeRoundMediumHalf", "EdgeRoundLow", "PipesFenceDiagonal", "FenceTopDiagonal", "CubeRoundConnectorBleft", "CubeRoundConnectorBright", "CylinderRoundTransition", "WallEdge", "RampRoundConnectorBleft", "RampRoundConnectorBright", "RampRoundConnectorAleft", "RampRoundConnectorAright", }},
            {1784 /* ConcreteArmoredExtended   */, new string[] {"CornerCMedium", "CornerSmallCMedium", "CornerSmallA", "SmallCornerRoundC", "RampRoundDDouble", "NotchedC", "NotchedA", "NotchedCMedium", "CubeQuarter", "CylinderThinXJoint", "CornerDoubleA1", "CutCornerA", "CornerDoubleB3", "SlicedCornerD", "EdgeRoundMedium", "RampB", "CornerRoundBMedium", "CornerRoundBLow", "CornerRoundAMedium", "CornerRoundALow", "EdgeRoundMediumHalf", "EdgeRoundLow", "PipesFenceDiagonal", "FenceTopDiagonal", "CubeRoundConnectorBleft", "CubeRoundConnectorBright", "CylinderRoundTransition", "WallEdge", "RampRoundConnectorBleft", "RampRoundConnectorBright", "RampRoundConnectorAleft", "RampRoundConnectorAright", }},
            {1785 /* PlasticExtendedLarge      */, new string[] {"CornerCMedium", "CornerSmallCMedium", "CornerSmallA", "SmallCornerRoundC", "RampRoundDDouble", "NotchedC", "NotchedA", "NotchedCMedium", "CubeQuarter", "CylinderThinXJoint", "CornerDoubleA1", "CutCornerA", "CornerDoubleB3", "SlicedCornerD", "EdgeRoundMedium", "RampB", "CornerRoundBMedium", "CornerRoundBLow", "CornerRoundAMedium", "CornerRoundALow", "EdgeRoundMediumHalf", "EdgeRoundLow", "PipesFenceDiagonal", "FenceTopDiagonal", "CubeRoundConnectorBleft", "CubeRoundConnectorBright", "CylinderRoundTransition", "WallEdge", "RampRoundConnectorBleft", "RampRoundConnectorBright", "RampRoundConnectorAleft", "RampRoundConnectorAright", }},
            {1786 /* HullExtendedLarge         */, new string[] {"CornerCMedium", "CornerSmallCMedium", "CornerSmallA", "SmallCornerRoundC", "RampRoundDDouble", "NotchedC", "NotchedA", "NotchedCMedium", "CubeQuarter", "CylinderThinXJoint", "CornerDoubleA1", "CutCornerA", "CornerDoubleB3", "SlicedCornerD", "EdgeRoundMedium", "RampB", "CornerRoundBMedium", "CornerRoundBLow", "CornerRoundAMedium", "CornerRoundALow", "EdgeRoundMediumHalf", "EdgeRoundLow", "PipesFenceDiagonal", "FenceTopDiagonal", "CubeRoundConnectorBleft", "CubeRoundConnectorBright", "CylinderRoundTransition", "WallEdge", "RampRoundConnectorBleft", "RampRoundConnectorBright", "RampRoundConnectorAleft", "RampRoundConnectorAright", }},
            {1787 /* HullArmoredExtendedLarge  */, new string[] {"CornerCMedium", "CornerSmallCMedium", "CornerSmallA", "SmallCornerRoundC", "RampRoundDDouble", "NotchedC", "NotchedA", "NotchedCMedium", "CubeQuarter", "CylinderThinXJoint", "CornerDoubleA1", "CutCornerA", "CornerDoubleB3", "SlicedCornerD", "EdgeRoundMedium", "RampB", "CornerRoundBMedium", "CornerRoundBLow", "CornerRoundAMedium", "CornerRoundALow", "EdgeRoundMediumHalf", "EdgeRoundLow", "PipesFenceDiagonal", "FenceTopDiagonal", "CubeRoundConnectorBleft", "CubeRoundConnectorBright", "CylinderRoundTransition", "WallEdge", "RampRoundConnectorBleft", "RampRoundConnectorBright", "RampRoundConnectorAleft", "RampRoundConnectorAright", }},
            {1788 /* HullCombatExtendedLarge   */, new string[] {"CornerCMedium", "CornerSmallCMedium", "CornerSmallA", "SmallCornerRoundC", "RampRoundDDouble", "NotchedC", "NotchedA", "NotchedCMedium", "CubeQuarter", "CylinderThinXJoint", "CornerDoubleA1", "CutCornerA", "CornerDoubleB3", "SlicedCornerD", "EdgeRoundMedium", "RampB", "CornerRoundBMedium", "CornerRoundBLow", "CornerRoundAMedium", "CornerRoundALow", "EdgeRoundMediumHalf", "EdgeRoundLow", "PipesFenceDiagonal", "FenceTopDiagonal", "CubeRoundConnectorBleft", "CubeRoundConnectorBright", "CylinderRoundTransition", "WallEdge", "RampRoundConnectorBleft", "RampRoundConnectorBright", "RampRoundConnectorAleft", "RampRoundConnectorAright", }},
            {1789 /* AlienExtended             */, new string[] {"CornerCMedium", "CornerSmallCMedium", "CornerSmallA", "SmallCornerRoundC", "RampRoundDDouble", "NotchedC", "NotchedA", "NotchedCMedium", "CubeQuarter", "CylinderThinXJoint", "CornerDoubleA1", "CutCornerA", "CornerDoubleB3", "SlicedCornerD", "EdgeRoundMedium", "RampB", "CornerRoundBMedium", "CornerRoundBLow", "CornerRoundAMedium", "CornerRoundALow", "EdgeRoundMediumHalf", "EdgeRoundLow", "PipesFenceDiagonal", "FenceTopDiagonal", "CubeRoundConnectorBleft", "CubeRoundConnectorBright", "CylinderRoundTransition", "WallEdge", "RampRoundConnectorBleft", "RampRoundConnectorBright", "RampRoundConnectorAleft", "RampRoundConnectorAright", }},
            {1790 /* PlasticExtendedSmall      */, new string[] {"CornerCMedium", "CornerSmallCMedium", "CornerSmallA", "SmallCornerRoundC", "RampRoundDDouble", "NotchedC", "NotchedA", "NotchedCMedium", "CubeQuarter", "CylinderThinXJoint", "CornerDoubleA1", "CutCornerA", "CornerDoubleB3", "SlicedCornerD", "EdgeRoundMedium", "RampB", "CornerRoundBMedium", "CornerRoundBLow", "CornerRoundAMedium", "CornerRoundALow", "EdgeRoundMediumHalf", "EdgeRoundLow", "PipesFenceDiagonal", "FenceTopDiagonal", "CubeRoundConnectorBleft", "CubeRoundConnectorBright", "CylinderRoundTransition", "WallEdge", "RampRoundConnectorBleft", "RampRoundConnectorBright", "RampRoundConnectorAleft", "RampRoundConnectorAright", }},
            {1791 /* HullExtendedSmall         */, new string[] {"CornerCMedium", "CornerSmallCMedium", "CornerSmallA", "SmallCornerRoundC", "RampRoundDDouble", "NotchedC", "NotchedA", "NotchedCMedium", "CubeQuarter", "CylinderThinXJoint", "CornerDoubleA1", "CutCornerA", "CornerDoubleB3", "SlicedCornerD", "EdgeRoundMedium", "RampB", "CornerRoundBMedium", "CornerRoundBLow", "CornerRoundAMedium", "CornerRoundALow", "EdgeRoundMediumHalf", "EdgeRoundLow", "PipesFenceDiagonal", "FenceTopDiagonal", "CubeRoundConnectorBleft", "CubeRoundConnectorBright", "CylinderRoundTransition", "WallEdge", "RampRoundConnectorBleft", "RampRoundConnectorBright", "RampRoundConnectorAleft", "RampRoundConnectorAright", }},
            {1792 /* HullArmoredExtendedSmall  */, new string[] {"CornerCMedium", "CornerSmallCMedium", "CornerSmallA", "SmallCornerRoundC", "RampRoundDDouble", "NotchedC", "NotchedA", "NotchedCMedium", "CubeQuarter", "CylinderThinXJoint", "CornerDoubleA1", "CutCornerA", "CornerDoubleB3", "SlicedCornerD", "EdgeRoundMedium", "RampB", "CornerRoundBMedium", "CornerRoundBLow", "CornerRoundAMedium", "CornerRoundALow", "EdgeRoundMediumHalf", "EdgeRoundLow", "PipesFenceDiagonal", "FenceTopDiagonal", "CubeRoundConnectorBleft", "CubeRoundConnectorBright", "CylinderRoundTransition", "WallEdge", "RampRoundConnectorBleft", "RampRoundConnectorBright", "RampRoundConnectorAleft", "RampRoundConnectorAright", }},
            {1793 /* HullCombatExtendedSmall   */, new string[] {"CornerCMedium", "CornerSmallCMedium", "CornerSmallA", "SmallCornerRoundC", "RampRoundDDouble", "NotchedC", "NotchedA", "NotchedCMedium", "CubeQuarter", "CylinderThinXJoint", "CornerDoubleA1", "CutCornerA", "CornerDoubleB3", "SlicedCornerD", "EdgeRoundMedium", "RampB", "CornerRoundBMedium", "CornerRoundBLow", "CornerRoundAMedium", "CornerRoundALow", "EdgeRoundMediumHalf", "EdgeRoundLow", "PipesFenceDiagonal", "FenceTopDiagonal", "CubeRoundConnectorBleft", "CubeRoundConnectorBright", "CylinderRoundTransition", "WallEdge", "RampRoundConnectorBleft", "RampRoundConnectorBright", "RampRoundConnectorAleft", "RampRoundConnectorAright", }},
            {1794 /* AlienExtendedLarge        */, new string[] {"CornerCMedium", "CornerSmallCMedium", "CornerSmallA", "SmallCornerRoundC", "RampRoundDDouble", "NotchedC", "NotchedA", "NotchedCMedium", "CubeQuarter", "CylinderThinXJoint", "CornerDoubleA1", "CutCornerA", "CornerDoubleB3", "SlicedCornerD", "EdgeRoundMedium", "RampB", "CornerRoundBMedium", "CornerRoundBLow", "CornerRoundAMedium", "CornerRoundALow", "EdgeRoundMediumHalf", "EdgeRoundLow", "PipesFenceDiagonal", "FenceTopDiagonal", "CubeRoundConnectorBleft", "CubeRoundConnectorBright", "CylinderRoundTransition", "WallEdge", "RampRoundConnectorBleft", "RampRoundConnectorBright", "RampRoundConnectorAleft", "RampRoundConnectorAright", }},
            {1824 /* WoodExtended2             */, new string[] {"WallLShapeMedium", "WallLShapeLow", "RampDLow", "RampE", "RampCMediumQuarter", "RampConnectorBleft", "RampConnectorBright", "CubeEighth", "SlicedCornerA2", "SlicedCornerA1Medium", "SlicedCornerDMedium", "NotchedB", "BeamQuarter", "WallCornerSloped", "RampADoubleHalf", "RampBDoubleHalf", "WallUShape", "WallDouble", "WallSlopedCDoubleMedium", "WallSlopedCDoubleLow", "CubeDummy", "CubeDummy", "RampCMediumHalfright", "RampCMediumHalfleft", "WallSlopedCDouble", "WallSlopedBDouble", "WallSlopedBDoubleMedium", "WallSlopedADouble", "RampAHalfright", "RampAHalfleft", "CylinderFramed", "CubeFramed", }},
            {1825 /* ConcreteExtended2         */, new string[] {"WallLShapeMedium", "WallLShapeLow", "RampDLow", "RampE", "RampCMediumQuarter", "RampConnectorBleft", "RampConnectorBright", "CubeEighth", "SlicedCornerA2", "SlicedCornerA1Medium", "SlicedCornerDMedium", "NotchedB", "BeamQuarter", "WallCornerSloped", "RampADoubleHalf", "RampBDoubleHalf", "WallUShape", "WallDouble", "WallSlopedCDoubleMedium", "WallSlopedCDoubleLow", "CubeDummy", "CubeDummy", "RampCMediumHalfright", "RampCMediumHalfleft", "WallSlopedCDouble", "WallSlopedBDouble", "WallSlopedBDoubleMedium", "WallSlopedADouble", "RampAHalfright", "RampAHalfleft", "CylinderFramed", "CubeFramed", }},
            {1826 /* ConcreteArmoredExtended2  */, new string[] {"WallLShapeMedium", "WallLShapeLow", "RampDLow", "RampE", "RampCMediumQuarter", "RampConnectorBleft", "RampConnectorBright", "CubeEighth", "SlicedCornerA2", "SlicedCornerA1Medium", "SlicedCornerDMedium", "NotchedB", "BeamQuarter", "WallCornerSloped", "RampADoubleHalf", "RampBDoubleHalf", "WallUShape", "WallDouble", "WallSlopedCDoubleMedium", "WallSlopedCDoubleLow", "CubeDummy", "CubeDummy", "RampCMediumHalfright", "RampCMediumHalfleft", "WallSlopedCDouble", "WallSlopedBDouble", "WallSlopedBDoubleMedium", "WallSlopedADouble", "RampAHalfright", "RampAHalfleft", "CylinderFramed", "CubeFramed", }},
            {1827 /* PlasticExtendedLarge2     */, new string[] {"WallLShapeMedium", "WallLShapeLow", "RampDLow", "RampE", "RampCMediumQuarter", "RampConnectorBleft", "RampConnectorBright", "CubeEighth", "SlicedCornerA2", "SlicedCornerA1Medium", "SlicedCornerDMedium", "NotchedB", "BeamQuarter", "WallCornerSloped", "RampADoubleHalf", "RampBDoubleHalf", "WallUShape", "WallDouble", "WallSlopedCDoubleMedium", "WallSlopedCDoubleLow", "CubeDummy", "CubeDummy", "RampCMediumHalfright", "RampCMediumHalfleft", "WallSlopedCDouble", "WallSlopedBDouble", "WallSlopedBDoubleMedium", "WallSlopedADouble", "RampAHalfright", "RampAHalfleft", "CylinderFramed", "CubeFramed", }},
            {1828 /* HullExtendedLarge2        */, new string[] {"WallLShapeMedium", "WallLShapeLow", "RampDLow", "RampE", "RampCMediumQuarter", "RampConnectorBleft", "RampConnectorBright", "CubeEighth", "SlicedCornerA2", "SlicedCornerA1Medium", "SlicedCornerDMedium", "NotchedB", "BeamQuarter", "WallCornerSloped", "RampADoubleHalf", "RampBDoubleHalf", "WallUShape", "WallDouble", "WallSlopedCDoubleMedium", "WallSlopedCDoubleLow", "CubeDummy", "CubeDummy", "RampCMediumHalfright", "RampCMediumHalfleft", "WallSlopedCDouble", "WallSlopedBDouble", "WallSlopedBDoubleMedium", "WallSlopedADouble", "RampAHalfright", "RampAHalfleft", "CylinderFramed", "CubeFramed", }},
            {1829 /* HullArmoredExtendedLarge2 */, new string[] {"WallLShapeMedium", "WallLShapeLow", "RampDLow", "RampE", "RampCMediumQuarter", "RampConnectorBleft", "RampConnectorBright", "CubeEighth", "SlicedCornerA2", "SlicedCornerA1Medium", "SlicedCornerDMedium", "NotchedB", "BeamQuarter", "WallCornerSloped", "RampADoubleHalf", "RampBDoubleHalf", "WallUShape", "WallDouble", "WallSlopedCDoubleMedium", "WallSlopedCDoubleLow", "CubeDummy", "CubeDummy", "RampCMediumHalfright", "RampCMediumHalfleft", "WallSlopedCDouble", "WallSlopedBDouble", "WallSlopedBDoubleMedium", "WallSlopedADouble", "RampAHalfright", "RampAHalfleft", "CylinderFramed", "CubeFramed", }},
            {1830 /* HullCombatExtendedLarge2  */, new string[] {"WallLShapeMedium", "WallLShapeLow", "RampDLow", "RampE", "RampCMediumQuarter", "RampConnectorBleft", "RampConnectorBright", "CubeEighth", "SlicedCornerA2", "SlicedCornerA1Medium", "SlicedCornerDMedium", "NotchedB", "BeamQuarter", "WallCornerSloped", "RampADoubleHalf", "RampBDoubleHalf", "WallUShape", "WallDouble", "WallSlopedCDoubleMedium", "WallSlopedCDoubleLow", "CubeDummy", "CubeDummy", "RampCMediumHalfright", "RampCMediumHalfleft", "WallSlopedCDouble", "WallSlopedBDouble", "WallSlopedBDoubleMedium", "WallSlopedADouble", "RampAHalfright", "RampAHalfleft", "CylinderFramed", "CubeFramed", }},
            {1831 /* AlienExtended2            */, new string[] {"WallLShapeMedium", "WallLShapeLow", "RampDLow", "RampE", "RampCMediumQuarter", "RampConnectorBleft", "RampConnectorBright", "CubeEighth", "SlicedCornerA2", "SlicedCornerA1Medium", "SlicedCornerDMedium", "NotchedB", "BeamQuarter", "WallCornerSloped", "RampADoubleHalf", "RampBDoubleHalf", "WallUShape", "WallDouble", "WallSlopedCDoubleMedium", "WallSlopedCDoubleLow", "CubeDummy", "CubeDummy", "RampCMediumHalfright", "RampCMediumHalfleft", "WallSlopedCDouble", "WallSlopedBDouble", "WallSlopedBDoubleMedium", "WallSlopedADouble", "RampAHalfright", "RampAHalfleft", "CylinderFramed", "CubeFramed", }},
            {1832 /* PlasticExtendedSmall2     */, new string[] {"WallLShapeMedium", "WallLShapeLow", "RampDLow", "RampE", "RampCMediumQuarter", "RampConnectorBleft", "RampConnectorBright", "CubeEighth", "SlicedCornerA2", "SlicedCornerA1Medium", "SlicedCornerDMedium", "NotchedB", "BeamQuarter", "WallCornerSloped", "RampADoubleHalf", "RampBDoubleHalf", "WallUShape", "WallDouble", "WallSlopedCDoubleMedium", "WallSlopedCDoubleLow", "CubeDummy", "CubeDummy", "RampCMediumHalfright", "RampCMediumHalfleft", "WallSlopedCDouble", "WallSlopedBDouble", "WallSlopedBDoubleMedium", "WallSlopedADouble", "RampAHalfright", "RampAHalfleft", "CylinderFramed", "CubeFramed", }},
            {1833 /* HullExtendedSmall2        */, new string[] {"WallLShapeMedium", "WallLShapeLow", "RampDLow", "RampE", "RampCMediumQuarter", "RampConnectorBleft", "RampConnectorBright", "CubeEighth", "SlicedCornerA2", "SlicedCornerA1Medium", "SlicedCornerDMedium", "NotchedB", "BeamQuarter", "WallCornerSloped", "RampADoubleHalf", "RampBDoubleHalf", "WallUShape", "WallDouble", "WallSlopedCDoubleMedium", "WallSlopedCDoubleLow", "CubeDummy", "CubeDummy", "RampCMediumHalfright", "RampCMediumHalfleft", "WallSlopedCDouble", "WallSlopedBDouble", "WallSlopedBDoubleMedium", "WallSlopedADouble", "RampAHalfright", "RampAHalfleft", "CylinderFramed", "CubeFramed", }},
            {1834 /* HullArmoredExtendedSmall2 */, new string[] {"WallLShapeMedium", "WallLShapeLow", "RampDLow", "RampE", "RampCMediumQuarter", "RampConnectorBleft", "RampConnectorBright", "CubeEighth", "SlicedCornerA2", "SlicedCornerA1Medium", "SlicedCornerDMedium", "NotchedB", "BeamQuarter", "WallCornerSloped", "RampADoubleHalf", "RampBDoubleHalf", "WallUShape", "WallDouble", "WallSlopedCDoubleMedium", "WallSlopedCDoubleLow", "CubeDummy", "CubeDummy", "RampCMediumHalfright", "RampCMediumHalfleft", "WallSlopedCDouble", "WallSlopedBDouble", "WallSlopedBDoubleMedium", "WallSlopedADouble", "RampAHalfright", "RampAHalfleft", "CylinderFramed", "CubeFramed", }},
            {1835 /* HullCombatExtendedSmall2  */, new string[] {"WallLShapeMedium", "WallLShapeLow", "RampDLow", "RampE", "RampCMediumQuarter", "RampConnectorBleft", "RampConnectorBright", "CubeEighth", "SlicedCornerA2", "SlicedCornerA1Medium", "SlicedCornerDMedium", "NotchedB", "BeamQuarter", "WallCornerSloped", "RampADoubleHalf", "RampBDoubleHalf", "WallUShape", "WallDouble", "WallSlopedCDoubleMedium", "WallSlopedCDoubleLow", "CubeDummy", "CubeDummy", "RampCMediumHalfright", "RampCMediumHalfleft", "WallSlopedCDouble", "WallSlopedBDouble", "WallSlopedBDoubleMedium", "WallSlopedADouble", "RampAHalfright", "RampAHalfleft", "CylinderFramed", "CubeFramed", }},
            {1836 /* AlienExtendedLarge2       */, new string[] {"WallLShapeMedium", "WallLShapeLow", "RampDLow", "RampE", "RampCMediumQuarter", "RampConnectorBleft", "RampConnectorBright", "CubeEighth", "SlicedCornerA2", "SlicedCornerA1Medium", "SlicedCornerDMedium", "NotchedB", "BeamQuarter", "WallCornerSloped", "RampADoubleHalf", "RampBDoubleHalf", "WallUShape", "WallDouble", "WallSlopedCDoubleMedium", "WallSlopedCDoubleLow", "CubeDummy", "CubeDummy", "RampCMediumHalfright", "RampCMediumHalfleft", "WallSlopedCDouble", "WallSlopedBDouble", "WallSlopedBDoubleMedium", "WallSlopedADouble", "RampAHalfright", "RampAHalfleft", "CylinderFramed", "CubeFramed", }},
            {1837 /* WoodExtended3             */, new string[] {"CubeSliced", "CubeStepped", "RampConnectorDleft", "RampConnectorDright", "RampConnectorEleft", "RampConnectorFright", "CubeDummy", "CubeDummy", "CornerDoubleA2", "CornerDoubleB2", "CornerDoubleB1", "CornerSmallBMedium", "CornerSmallBLow", "CornerHalfA3Low", "CornerSmallCLow", "CornerB", "CornerHalfA2", "CornerCLow", "CornerHalfC", "CornerRoundBMediumQuarter", "CornerRoundBLowQuarter", "PipesX", "PipesFenceKinked", "CubeDummy", "PipesL", "PipesT", "CylinderThin6Way", "CornerRoundAMediumQuarter", "CornerRoundALowQuarter", "EdgeRoundLowHalf", "EdgeRoundLowQuarter", "EdgeRoundLowEighth", }},
            {1838 /* ConcreteExtended3         */, new string[] {"CubeSliced", "CubeStepped", "RampConnectorDleft", "RampConnectorDright", "RampConnectorEleft", "RampConnectorFright", "CubeDummy", "CubeDummy", "CornerDoubleA2", "CornerDoubleB2", "CornerDoubleB1", "CornerSmallBMedium", "CornerSmallBLow", "CornerHalfA3Low", "CornerSmallCLow", "CornerB", "CornerHalfA2", "CornerCLow", "CornerHalfC", "CornerRoundBMediumQuarter", "CornerRoundBLowQuarter", "PipesX", "PipesFenceKinked", "CubeDummy", "PipesL", "PipesT", "CylinderThin6Way", "CornerRoundAMediumQuarter", "CornerRoundALowQuarter", "EdgeRoundLowHalf", "EdgeRoundLowQuarter", "EdgeRoundLowEighth", }},
            {1839 /* ConcreteArmoredExtended3  */, new string[] {"CubeSliced", "CubeStepped", "RampConnectorDleft", "RampConnectorDright", "RampConnectorEleft", "RampConnectorFright", "CubeDummy", "CubeDummy", "CornerDoubleA2", "CornerDoubleB2", "CornerDoubleB1", "CornerSmallBMedium", "CornerSmallBLow", "CornerHalfA3Low", "CornerSmallCLow", "CornerB", "CornerHalfA2", "CornerCLow", "CornerHalfC", "CornerRoundBMediumQuarter", "CornerRoundBLowQuarter", "PipesX", "PipesFenceKinked", "CubeDummy", "PipesL", "PipesT", "CylinderThin6Way", "CornerRoundAMediumQuarter", "CornerRoundALowQuarter", "EdgeRoundLowHalf", "EdgeRoundLowQuarter", "EdgeRoundLowEighth", }},
            {1840 /* PlasticExtendedLarge3     */, new string[] {"CubeSliced", "CubeStepped", "RampConnectorDleft", "RampConnectorDright", "RampConnectorEleft", "RampConnectorFright", "CubeDummy", "CubeDummy", "CornerDoubleA2", "CornerDoubleB2", "CornerDoubleB1", "CornerSmallBMedium", "CornerSmallBLow", "CornerHalfA3Low", "CornerSmallCLow", "CornerB", "CornerHalfA2", "CornerCLow", "CornerHalfC", "CornerRoundBMediumQuarter", "CornerRoundBLowQuarter", "PipesX", "PipesFenceKinked", "CubeDummy", "PipesL", "PipesT", "CylinderThin6Way", "CornerRoundAMediumQuarter", "CornerRoundALowQuarter", "EdgeRoundLowHalf", "EdgeRoundLowQuarter", "EdgeRoundLowEighth", }},
            {1841 /* HullExtendedLarge3        */, new string[] {"CubeSliced", "CubeStepped", "RampConnectorDleft", "RampConnectorDright", "RampConnectorEleft", "RampConnectorFright", "CubeDummy", "CubeDummy", "CornerDoubleA2", "CornerDoubleB2", "CornerDoubleB1", "CornerSmallBMedium", "CornerSmallBLow", "CornerHalfA3Low", "CornerSmallCLow", "CornerB", "CornerHalfA2", "CornerCLow", "CornerHalfC", "CornerRoundBMediumQuarter", "CornerRoundBLowQuarter", "PipesX", "PipesFenceKinked", "CubeDummy", "PipesL", "PipesT", "CylinderThin6Way", "CornerRoundAMediumQuarter", "CornerRoundALowQuarter", "EdgeRoundLowHalf", "EdgeRoundLowQuarter", "EdgeRoundLowEighth", }},
            {1842 /* HullArmoredExtendedLarge3 */, new string[] {"CubeSliced", "CubeStepped", "RampConnectorDleft", "RampConnectorDright", "RampConnectorEleft", "RampConnectorFright", "CubeDummy", "CubeDummy", "CornerDoubleA2", "CornerDoubleB2", "CornerDoubleB1", "CornerSmallBMedium", "CornerSmallBLow", "CornerHalfA3Low", "CornerSmallCLow", "CornerB", "CornerHalfA2", "CornerCLow", "CornerHalfC", "CornerRoundBMediumQuarter", "CornerRoundBLowQuarter", "PipesX", "PipesFenceKinked", "CubeDummy", "PipesL", "PipesT", "CylinderThin6Way", "CornerRoundAMediumQuarter", "CornerRoundALowQuarter", "EdgeRoundLowHalf", "EdgeRoundLowQuarter", "EdgeRoundLowEighth", }},
            {1843 /* HullCombatExtendedLarge3  */, new string[] {"CubeSliced", "CubeStepped", "RampConnectorDleft", "RampConnectorDright", "RampConnectorEleft", "RampConnectorFright", "CubeDummy", "CubeDummy", "CornerDoubleA2", "CornerDoubleB2", "CornerDoubleB1", "CornerSmallBMedium", "CornerSmallBLow", "CornerHalfA3Low", "CornerSmallCLow", "CornerB", "CornerHalfA2", "CornerCLow", "CornerHalfC", "CornerRoundBMediumQuarter", "CornerRoundBLowQuarter", "PipesX", "PipesFenceKinked", "CubeDummy", "PipesL", "PipesT", "CylinderThin6Way", "CornerRoundAMediumQuarter", "CornerRoundALowQuarter", "EdgeRoundLowHalf", "EdgeRoundLowQuarter", "EdgeRoundLowEighth", }},
            {1844 /* AlienExtended3            */, new string[] {"CubeSliced", "CubeStepped", "RampConnectorDleft", "RampConnectorDright", "RampConnectorEleft", "RampConnectorFright", "CubeDummy", "CubeDummy", "CornerDoubleA2", "CornerDoubleB2", "CornerDoubleB1", "CornerSmallBMedium", "CornerSmallBLow", "CornerHalfA3Low", "CornerSmallCLow", "CornerB", "CornerHalfA2", "CornerCLow", "CornerHalfC", "CornerRoundBMediumQuarter", "CornerRoundBLowQuarter", "PipesX", "PipesFenceKinked", "CubeDummy", "PipesL", "PipesT", "CylinderThin6Way", "CornerRoundAMediumQuarter", "CornerRoundALowQuarter", "EdgeRoundLowHalf", "EdgeRoundLowQuarter", "EdgeRoundLowEighth", }},
            {1845 /* PlasticExtendedSmall3     */, new string[] {"CubeSliced", "CubeStepped", "RampConnectorDleft", "RampConnectorDright", "RampConnectorEleft", "RampConnectorFright", "CubeDummy", "CubeDummy", "CornerDoubleA2", "CornerDoubleB2", "CornerDoubleB1", "CornerSmallBMedium", "CornerSmallBLow", "CornerHalfA3Low", "CornerSmallCLow", "CornerB", "CornerHalfA2", "CornerCLow", "CornerHalfC", "CornerRoundBMediumQuarter", "CornerRoundBLowQuarter", "PipesX", "PipesFenceKinked", "CubeDummy", "PipesL", "PipesT", "CylinderThin6Way", "CornerRoundAMediumQuarter", "CornerRoundALowQuarter", "EdgeRoundLowHalf", "EdgeRoundLowQuarter", "EdgeRoundLowEighth", }},
            {1846 /* HullExtendedSmall3        */, new string[] {"CubeSliced", "CubeStepped", "RampConnectorDleft", "RampConnectorDright", "RampConnectorEleft", "RampConnectorFright", "CubeDummy", "CubeDummy", "CornerDoubleA2", "CornerDoubleB2", "CornerDoubleB1", "CornerSmallBMedium", "CornerSmallBLow", "CornerHalfA3Low", "CornerSmallCLow", "CornerB", "CornerHalfA2", "CornerCLow", "CornerHalfC", "CornerRoundBMediumQuarter", "CornerRoundBLowQuarter", "PipesX", "PipesFenceKinked", "CubeDummy", "PipesL", "PipesT", "CylinderThin6Way", "CornerRoundAMediumQuarter", "CornerRoundALowQuarter", "EdgeRoundLowHalf", "EdgeRoundLowQuarter", "EdgeRoundLowEighth", }},
            {1847 /* HullArmoredExtendedSmall3 */, new string[] {"CubeSliced", "CubeStepped", "RampConnectorDleft", "RampConnectorDright", "RampConnectorEleft", "RampConnectorFright", "CubeDummy", "CubeDummy", "CornerDoubleA2", "CornerDoubleB2", "CornerDoubleB1", "CornerSmallBMedium", "CornerSmallBLow", "CornerHalfA3Low", "CornerSmallCLow", "CornerB", "CornerHalfA2", "CornerCLow", "CornerHalfC", "CornerRoundBMediumQuarter", "CornerRoundBLowQuarter", "PipesX", "PipesFenceKinked", "CubeDummy", "PipesL", "PipesT", "CylinderThin6Way", "CornerRoundAMediumQuarter", "CornerRoundALowQuarter", "EdgeRoundLowHalf", "EdgeRoundLowQuarter", "EdgeRoundLowEighth", }},
            {1848 /* HullCombatExtendedSmall3  */, new string[] {"CubeSliced", "CubeStepped", "RampConnectorDleft", "RampConnectorDright", "RampConnectorEleft", "RampConnectorFright", "CubeDummy", "CubeDummy", "CornerDoubleA2", "CornerDoubleB2", "CornerDoubleB1", "CornerSmallBMedium", "CornerSmallBLow", "CornerHalfA3Low", "CornerSmallCLow", "CornerB", "CornerHalfA2", "CornerCLow", "CornerHalfC", "CornerRoundBMediumQuarter", "CornerRoundBLowQuarter", "PipesX", "PipesFenceKinked", "CubeDummy", "PipesL", "PipesT", "CylinderThin6Way", "CornerRoundAMediumQuarter", "CornerRoundALowQuarter", "EdgeRoundLowHalf", "EdgeRoundLowQuarter", "EdgeRoundLowEighth", }},
            {1849 /* AlienExtendedLarge3       */, new string[] {"CubeSliced", "CubeStepped", "RampConnectorDleft", "RampConnectorDright", "RampConnectorEleft", "RampConnectorFright", "CubeDummy", "CubeDummy", "CornerDoubleA2", "CornerDoubleB2", "CornerDoubleB1", "CornerSmallBMedium", "CornerSmallBLow", "CornerHalfA3Low", "CornerSmallCLow", "CornerB", "CornerHalfA2", "CornerCLow", "CornerHalfC", "CornerRoundBMediumQuarter", "CornerRoundBLowQuarter", "PipesX", "PipesFenceKinked", "CubeDummy", "PipesL", "PipesT", "CylinderThin6Way", "CornerRoundAMediumQuarter", "CornerRoundALowQuarter", "EdgeRoundLowHalf", "EdgeRoundLowQuarter", "EdgeRoundLowEighth", }},
            {1850 /* WoodExtended4             */, new string[] {"EdgeRoundThin", "EdgeRoundMediumHalfDouble", "RampRoundE", "EdgeRoundThin", "EdgeRoundMediumHalfDouble", "CubeRoundTransitionleft", "CubeDummy", "CubeDummy", "PipesL", "CubeDummy", "CubeRoundTransitionright", "EdgeRoundHalf", "EdgeRoundDoubleA", "EdgeRoundDoubleAHalf", "EdgeRoundMediumQuarter", "RampEHalf", "RampRoundADouble", "RampRoundADoubleHalf", "RampRoundBDouble", "RampRoundC", "RampConnectorAleft", "RampConnectorAright", "SlicedCornerA1Low", "SlicedCornerB1", "SlicedCornerB2", "SlicedCornerB1Medium", "SlicedCornerB2Medium", "CylinderThin3Way", "CylinderThin4Way", "CylinderThin5Way", "CubeDummy", }},
            {1851 /* ConcreteExtended4         */, new string[] {"EdgeRoundThin", "EdgeRoundMediumHalfDouble", "RampRoundE", "EdgeRoundThin", "EdgeRoundMediumHalfDouble", "CubeRoundTransitionleft", "CubeDummy", "CubeDummy", "PipesL", "CubeDummy", "CubeRoundTransitionright", "EdgeRoundHalf", "EdgeRoundDoubleA", "EdgeRoundDoubleAHalf", "EdgeRoundMediumQuarter", "RampEHalf", "RampRoundADouble", "RampRoundADoubleHalf", "RampRoundBDouble", "RampRoundC", "RampConnectorAleft", "RampConnectorAright", "SlicedCornerA1Low", "SlicedCornerB1", "SlicedCornerB2", "SlicedCornerB1Medium", "SlicedCornerB2Medium", "CylinderThin3Way", "CylinderThin4Way", "CylinderThin5Way", "CubeDummy", }},
            {1852 /* ConcreteArmoredExtended4  */, new string[] {"EdgeRoundThin", "EdgeRoundMediumHalfDouble", "RampRoundE", "EdgeRoundThin", "EdgeRoundMediumHalfDouble", "CubeRoundTransitionleft", "CubeDummy", "CubeDummy", "PipesL", "CubeDummy", "CubeRoundTransitionright", "EdgeRoundHalf", "EdgeRoundDoubleA", "EdgeRoundDoubleAHalf", "EdgeRoundMediumQuarter", "RampEHalf", "RampRoundADouble", "RampRoundADoubleHalf", "RampRoundBDouble", "RampRoundC", "RampConnectorAleft", "RampConnectorAright", "SlicedCornerA1Low", "SlicedCornerB1", "SlicedCornerB2", "SlicedCornerB1Medium", "SlicedCornerB2Medium", "CylinderThin3Way", "CylinderThin4Way", "CylinderThin5Way", "CubeDummy", }},
            {1853 /* PlasticExtendedLarge4     */, new string[] {"EdgeRoundThin", "EdgeRoundMediumHalfDouble", "RampRoundE", "EdgeRoundThin", "EdgeRoundMediumHalfDouble", "CubeRoundTransitionleft", "CubeDummy", "CubeDummy", "PipesL", "CubeDummy", "CubeRoundTransitionright", "EdgeRoundHalf", "EdgeRoundDoubleA", "EdgeRoundDoubleAHalf", "EdgeRoundMediumQuarter", "RampEHalf", "RampRoundADouble", "RampRoundADoubleHalf", "RampRoundBDouble", "RampRoundC", "RampConnectorAleft", "RampConnectorAright", "SlicedCornerA1Low", "SlicedCornerB1", "SlicedCornerB2", "SlicedCornerB1Medium", "SlicedCornerB2Medium", "CylinderThin3Way", "CylinderThin4Way", "CylinderThin5Way", "CubeDummy", }},
            {1854 /* HullExtendedLarge4        */, new string[] {"EdgeRoundThin", "EdgeRoundMediumHalfDouble", "RampRoundE", "EdgeRoundThin", "EdgeRoundMediumHalfDouble", "CubeRoundTransitionleft", "CubeDummy", "CubeDummy", "PipesL", "CubeDummy", "CubeRoundTransitionright", "EdgeRoundHalf", "EdgeRoundDoubleA", "EdgeRoundDoubleAHalf", "EdgeRoundMediumQuarter", "RampEHalf", "RampRoundADouble", "RampRoundADoubleHalf", "RampRoundBDouble", "RampRoundC", "RampConnectorAleft", "RampConnectorAright", "SlicedCornerA1Low", "SlicedCornerB1", "SlicedCornerB2", "SlicedCornerB1Medium", "SlicedCornerB2Medium", "CylinderThin3Way", "CylinderThin4Way", "CylinderThin5Way", "CubeDummy", }},
            {1855 /* HullArmoredExtendedLarge4 */, new string[] {"EdgeRoundThin", "EdgeRoundMediumHalfDouble", "RampRoundE", "EdgeRoundThin", "EdgeRoundMediumHalfDouble", "CubeRoundTransitionleft", "CubeDummy", "CubeDummy", "PipesL", "CubeDummy", "CubeRoundTransitionright", "EdgeRoundHalf", "EdgeRoundDoubleA", "EdgeRoundDoubleAHalf", "EdgeRoundMediumQuarter", "RampEHalf", "RampRoundADouble", "RampRoundADoubleHalf", "RampRoundBDouble", "RampRoundC", "RampConnectorAleft", "RampConnectorAright", "SlicedCornerA1Low", "SlicedCornerB1", "SlicedCornerB2", "SlicedCornerB1Medium", "SlicedCornerB2Medium", "CylinderThin3Way", "CylinderThin4Way", "CylinderThin5Way", "CubeDummy", }},
            {1856 /* HullCombatExtendedLarge4  */, new string[] {"EdgeRoundThin", "EdgeRoundMediumHalfDouble", "RampRoundE", "EdgeRoundThin", "EdgeRoundMediumHalfDouble", "CubeRoundTransitionleft", "CubeDummy", "CubeDummy", "PipesL", "CubeDummy", "CubeRoundTransitionright", "EdgeRoundHalf", "EdgeRoundDoubleA", "EdgeRoundDoubleAHalf", "EdgeRoundMediumQuarter", "RampEHalf", "RampRoundADouble", "RampRoundADoubleHalf", "RampRoundBDouble", "RampRoundC", "RampConnectorAleft", "RampConnectorAright", "SlicedCornerA1Low", "SlicedCornerB1", "SlicedCornerB2", "SlicedCornerB1Medium", "SlicedCornerB2Medium", "CylinderThin3Way", "CylinderThin4Way", "CylinderThin5Way", "CubeDummy", }},
            {1857 /* AlienExtended4            */, new string[] {"EdgeRoundThin", "EdgeRoundMediumHalfDouble", "RampRoundE", "EdgeRoundThin", "EdgeRoundMediumHalfDouble", "CubeRoundTransitionleft", "CubeDummy", "CubeDummy", "PipesL", "CubeDummy", "CubeRoundTransitionright", "EdgeRoundHalf", "EdgeRoundDoubleA", "EdgeRoundDoubleAHalf", "EdgeRoundMediumQuarter", "RampEHalf", "RampRoundADouble", "RampRoundADoubleHalf", "RampRoundBDouble", "RampRoundC", "RampConnectorAleft", "RampConnectorAright", "SlicedCornerA1Low", "SlicedCornerB1", "SlicedCornerB2", "SlicedCornerB1Medium", "SlicedCornerB2Medium", "CylinderThin3Way", "CylinderThin4Way", "CylinderThin5Way", "CubeDummy", }},
            {1858 /* PlasticExtendedSmall4     */, new string[] {"EdgeRoundThin", "EdgeRoundMediumHalfDouble", "RampRoundE", "EdgeRoundThin", "EdgeRoundMediumHalfDouble", "CubeRoundTransitionleft", "CubeDummy", "CubeDummy", "PipesL", "CubeDummy", "CubeRoundTransitionright", "EdgeRoundHalf", "EdgeRoundDoubleA", "EdgeRoundDoubleAHalf", "EdgeRoundMediumQuarter", "RampEHalf", "RampRoundADouble", "RampRoundADoubleHalf", "RampRoundBDouble", "RampRoundC", "RampConnectorAleft", "RampConnectorAright", "SlicedCornerA1Low", "SlicedCornerB1", "SlicedCornerB2", "SlicedCornerB1Medium", "SlicedCornerB2Medium", "CylinderThin3Way", "CylinderThin4Way", "CylinderThin5Way", "CubeDummy", }},
            {1859 /* HullExtendedSmall4        */, new string[] {"EdgeRoundThin", "EdgeRoundMediumHalfDouble", "RampRoundE", "EdgeRoundThin", "EdgeRoundMediumHalfDouble", "CubeRoundTransitionleft", "CubeDummy", "CubeDummy", "PipesL", "CubeDummy", "CubeRoundTransitionright", "EdgeRoundHalf", "EdgeRoundDoubleA", "EdgeRoundDoubleAHalf", "EdgeRoundMediumQuarter", "RampEHalf", "RampRoundADouble", "RampRoundADoubleHalf", "RampRoundBDouble", "RampRoundC", "RampConnectorAleft", "RampConnectorAright", "SlicedCornerA1Low", "SlicedCornerB1", "SlicedCornerB2", "SlicedCornerB1Medium", "SlicedCornerB2Medium", "CylinderThin3Way", "CylinderThin4Way", "CylinderThin5Way", "CubeDummy", }},
            {1860 /* HullArmoredExtendedSmall4 */, new string[] {"EdgeRoundThin", "EdgeRoundMediumHalfDouble", "RampRoundE", "EdgeRoundThin", "EdgeRoundMediumHalfDouble", "CubeRoundTransitionleft", "CubeDummy", "CubeDummy", "PipesL", "CubeDummy", "CubeRoundTransitionright", "EdgeRoundHalf", "EdgeRoundDoubleA", "EdgeRoundDoubleAHalf", "EdgeRoundMediumQuarter", "RampEHalf", "RampRoundADouble", "RampRoundADoubleHalf", "RampRoundBDouble", "RampRoundC", "RampConnectorAleft", "RampConnectorAright", "SlicedCornerA1Low", "SlicedCornerB1", "SlicedCornerB2", "SlicedCornerB1Medium", "SlicedCornerB2Medium", "CylinderThin3Way", "CylinderThin4Way", "CylinderThin5Way", "CubeDummy", }},
            {1861 /* HullCombatExtendedSmall4  */, new string[] {"EdgeRoundThin", "EdgeRoundMediumHalfDouble", "RampRoundE", "EdgeRoundThin", "EdgeRoundMediumHalfDouble", "CubeRoundTransitionleft", "CubeDummy", "CubeDummy", "PipesL", "CubeDummy", "CubeRoundTransitionright", "EdgeRoundHalf", "EdgeRoundDoubleA", "EdgeRoundDoubleAHalf", "EdgeRoundMediumQuarter", "RampEHalf", "RampRoundADouble", "RampRoundADoubleHalf", "RampRoundBDouble", "RampRoundC", "RampConnectorAleft", "RampConnectorAright", "SlicedCornerA1Low", "SlicedCornerB1", "SlicedCornerB2", "SlicedCornerB1Medium", "SlicedCornerB2Medium", "CylinderThin3Way", "CylinderThin4Way", "CylinderThin5Way", "CubeDummy", }},
            {1862 /* AlienExtendedLarge4       */, new string[] {"EdgeRoundThin", "EdgeRoundMediumHalfDouble", "RampRoundE", "EdgeRoundThin", "EdgeRoundMediumHalfDouble", "CubeRoundTransitionleft", "CubeDummy", "CubeDummy", "PipesL", "CubeDummy", "CubeRoundTransitionright", "EdgeRoundHalf", "EdgeRoundDoubleA", "EdgeRoundDoubleAHalf", "EdgeRoundMediumQuarter", "RampEHalf", "RampRoundADouble", "RampRoundADoubleHalf", "RampRoundBDouble", "RampRoundC", "RampConnectorAleft", "RampConnectorAright", "SlicedCornerA1Low", "SlicedCornerB1", "SlicedCornerB2", "SlicedCornerB1Medium", "SlicedCornerB2Medium", "CylinderThin3Way", "CylinderThin4Way", "CylinderThin5Way", "CubeDummy", }},
            {1863 /* WoodExtended5             */, new string[] {"CubeLShape", "CubeRoundTransitionleft", "CubeRoundTransitionright", "RampEHalf", "CutCornerDLow", "RampConnectorCleft", "RampConnectorCright", "NotchedBMedium", "NotchedCLow", "CutCornerD", "CutCornerDMedium", "CorridorWallA", "CorridorEdgeA", "CorridorPillarA", "CorridorWallB", "CorridorEdgeB", "CorridorEdgeC", "CorridorPillarB", "CorridorPillarC", "CorridorRoof", "CorridorRoofCorner", "CorridorRoofCornerRound", "CorridorBulkyWallA", "CorridorBulkyWallAWindowed", "CorridorBulkyWallB", "CorridorBulkyWallBWindowed", "CorridorRampA", "CorridorRampB", "DoorframeA", "DoorframeB", "DoorframeC", "CorridorRoofCornerInverted", }},
            {1864 /* ConcreteExtended5         */, new string[] {"CubeLShape", "CubeRoundTransitionleft", "CubeRoundTransitionright", "RampEHalf", "CutCornerDLow", "RampConnectorCleft", "RampConnectorCright", "NotchedBMedium", "NotchedCLow", "CutCornerD", "CutCornerDMedium", "CorridorWallA", "CorridorEdgeA", "CorridorPillarA", "CorridorWallB", "CorridorEdgeB", "CorridorEdgeC", "CorridorPillarB", "CorridorPillarC", "CorridorRoof", "CorridorRoofCorner", "CorridorRoofCornerRound", "CorridorBulkyWallA", "CorridorBulkyWallAWindowed", "CorridorBulkyWallB", "CorridorBulkyWallBWindowed", "CorridorRampA", "CorridorRampB", "DoorframeA", "DoorframeB", "DoorframeC", "CorridorRoofCornerInverted", }},
            {1865 /* ConcreteArmoredExtended5  */, new string[] {"CubeLShape", "CubeRoundTransitionleft", "CubeRoundTransitionright", "RampEHalf", "CutCornerDLow", "RampConnectorCleft", "RampConnectorCright", "NotchedBMedium", "NotchedCLow", "CutCornerD", "CutCornerDMedium", "CorridorWallA", "CorridorEdgeA", "CorridorPillarA", "CorridorWallB", "CorridorEdgeB", "CorridorEdgeC", "CorridorPillarB", "CorridorPillarC", "CorridorRoof", "CorridorRoofCorner", "CorridorRoofCornerRound", "CorridorBulkyWallA", "CorridorBulkyWallAWindowed", "CorridorBulkyWallB", "CorridorBulkyWallBWindowed", "CorridorRampA", "CorridorRampB", "DoorframeA", "DoorframeB", "DoorframeC", "CorridorRoofCornerInverted", }},
            {1866 /* PlasticExtendedLarge5     */, new string[] {"CubeLShape", "CubeRoundTransitionleft", "CubeRoundTransitionright", "RampEHalf", "CutCornerDLow", "RampConnectorCleft", "RampConnectorCright", "NotchedBMedium", "NotchedCLow", "CutCornerD", "CutCornerDMedium", "CorridorWallA", "CorridorEdgeA", "CorridorPillarA", "CorridorWallB", "CorridorEdgeB", "CorridorEdgeC", "CorridorPillarB", "CorridorPillarC", "CorridorRoof", "CorridorRoofCorner", "CorridorRoofCornerRound", "CorridorBulkyWallA", "CorridorBulkyWallAWindowed", "CorridorBulkyWallB", "CorridorBulkyWallBWindowed", "CorridorRampA", "CorridorRampB", "DoorframeA", "DoorframeB", "DoorframeC", "CorridorRoofCornerInverted", }},
            {1867 /* HullExtendedLarge5        */, new string[] {"CubeLShape", "CubeRoundTransitionleft", "CubeRoundTransitionright", "RampEHalf", "CutCornerDLow", "RampConnectorCleft", "RampConnectorCright", "NotchedBMedium", "NotchedCLow", "CutCornerD", "CutCornerDMedium", "CorridorWallA", "CorridorEdgeA", "CorridorPillarA", "CorridorWallB", "CorridorEdgeB", "CorridorEdgeC", "CorridorPillarB", "CorridorPillarC", "CorridorRoof", "CorridorRoofCorner", "CorridorRoofCornerRound", "CorridorBulkyWallA", "CorridorBulkyWallAWindowed", "CorridorBulkyWallB", "CorridorBulkyWallBWindowed", "CorridorRampA", "CorridorRampB", "DoorframeA", "DoorframeB", "DoorframeC", "CorridorRoofCornerInverted", }},
            {1868 /* HullArmoredExtendedLarge5 */, new string[] {"CubeLShape", "CubeRoundTransitionleft", "CubeRoundTransitionright", "RampEHalf", "CutCornerDLow", "RampConnectorCleft", "RampConnectorCright", "NotchedBMedium", "NotchedCLow", "CutCornerD", "CutCornerDMedium", "CorridorWallA", "CorridorEdgeA", "CorridorPillarA", "CorridorWallB", "CorridorEdgeB", "CorridorEdgeC", "CorridorPillarB", "CorridorPillarC", "CorridorRoof", "CorridorRoofCorner", "CorridorRoofCornerRound", "CorridorBulkyWallA", "CorridorBulkyWallAWindowed", "CorridorBulkyWallB", "CorridorBulkyWallBWindowed", "CorridorRampA", "CorridorRampB", "DoorframeA", "DoorframeB", "DoorframeC", "CorridorRoofCornerInverted", }},
            {1869 /* HullCombatExtendedLarge5  */, new string[] {"CubeLShape", "CubeRoundTransitionleft", "CubeRoundTransitionright", "RampEHalf", "CutCornerDLow", "RampConnectorCleft", "RampConnectorCright", "NotchedBMedium", "NotchedCLow", "CutCornerD", "CutCornerDMedium", "CorridorWallA", "CorridorEdgeA", "CorridorPillarA", "CorridorWallB", "CorridorEdgeB", "CorridorEdgeC", "CorridorPillarB", "CorridorPillarC", "CorridorRoof", "CorridorRoofCorner", "CorridorRoofCornerRound", "CorridorBulkyWallA", "CorridorBulkyWallAWindowed", "CorridorBulkyWallB", "CorridorBulkyWallBWindowed", "CorridorRampA", "CorridorRampB", "DoorframeA", "DoorframeB", "DoorframeC", "CorridorRoofCornerInverted", }},
            {1870 /* AlienExtended5            */, new string[] {"CubeLShape", "CubeRoundTransitionleft", "CubeRoundTransitionright", "RampEHalf", "CutCornerDLow", "RampConnectorCleft", "RampConnectorCright", "NotchedBMedium", "NotchedCLow", "CutCornerD", "CutCornerDMedium", "CorridorWallA", "CorridorEdgeA", "CorridorPillarA", "CorridorWallB", "CorridorEdgeB", "CorridorEdgeC", "CorridorPillarB", "CorridorPillarC", "CorridorRoof", "CorridorRoofCorner", "CorridorRoofCornerRound", "CorridorBulkyWallA", "CorridorBulkyWallAWindowed", "CorridorBulkyWallB", "CorridorBulkyWallBWindowed", "CorridorRampA", "CorridorRampB", "DoorframeA", "DoorframeB", "DoorframeC", "CorridorRoofCornerInverted", }},
            {1871 /* PlasticExtendedSmall5     */, new string[] {"CubeLShape", "CubeRoundTransitionleft", "CubeRoundTransitionright", "RampEHalf", "CutCornerDLow", "RampConnectorCleft", "RampConnectorCright", "NotchedBMedium", "NotchedCLow", "CutCornerD", "CutCornerDMedium", }},
            {1872 /* HullExtendedSmall5        */, new string[] {"CubeLShape", "CubeRoundTransitionleft", "CubeRoundTransitionright", "RampEHalf", "CutCornerDLow", "RampConnectorCleft", "RampConnectorCright", "NotchedBMedium", "NotchedCLow", "CutCornerD", "CutCornerDMedium", }},
            {1873 /* HullArmoredExtendedSmall5 */, new string[] {"CubeLShape", "CubeRoundTransitionleft", "CubeRoundTransitionright", "RampEHalf", "CutCornerDLow", "RampConnectorCleft", "RampConnectorCright", "NotchedBMedium", "NotchedCLow", "CutCornerD", "CutCornerDMedium", }},
            {1874 /* HullCombatExtendedSmall5  */, new string[] {"CubeLShape", "CubeRoundTransitionleft", "CubeRoundTransitionright", "RampEHalf", "CutCornerDLow", "RampConnectorCleft", "RampConnectorCright", "NotchedBMedium", "NotchedCLow", "CutCornerD", "CutCornerDMedium", }},
            {1875 /* AlienExtendedLarge5       */, new string[] {"CubeLShape", "CubeRoundTransitionleft", "CubeRoundTransitionright", "RampEHalf", "CutCornerDLow", "RampConnectorCleft", "RampConnectorCright", "NotchedBMedium", "NotchedCLow", "CutCornerD", "CutCornerDMedium", "CorridorWallA", "CorridorEdgeA", "CorridorPillarA", "CorridorWallB", "CorridorEdgeB", "CorridorEdgeC", "CorridorPillarB", "CorridorPillarC", "CorridorRoof", "CorridorRoofCorner", "CorridorRoofCornerRound", "CorridorBulkyWallA", "CorridorBulkyWallAWindowed", "CorridorBulkyWallB", "CorridorBulkyWallBWindowed", "CorridorRampA", "CorridorRampB", "DoorframeA", "DoorframeB", "DoorframeC", "CorridorRoofCornerInverted", }},
        };
        
        // TrussLargeBlocks (Parent: 1075 =0x433)
        // In the block count list "Truss Blocks" variants all count as block type 0x433!
        //{416,  new string[] {"Cube" }},
        //{705,  new string[] {"Corner" }},
        //{704,  new string[] {"Slope" }},
        //{1411, new string[] {"Curved Corner" }},
        //{1408, new string[] {"Round Corner" }},
        //{1409, new string[] {"Round Slope" }},
        //{1407, new string[] {"Cylinder" }},
        //{1410, new string[] {"Inward Round Slope" }},
        //{1406, new string[] {"Wall" }},
        //{1412, new string[] {"Thin Slope" }},
        //{1413, new string[] {"Round Slope Thin" }},
        //{1414, new string[] {"Thin Corner" }},
        //{1415, new string[] {"Round Slope Thin" }},
        //{1416, new string[] {"Corner Round Thin" }},

        // Window Large Blocks (Parent: 1128 =0x468)
        // TODO: In the block count list "Window Blocks L" variants all count as block type 0x468!
        //{770,  new string[] {"Unknown", "Vertical 1x1" }},
        //{796,  new string[] {"Unknown", "Vertical 1x2" }},
        //{798,  new string[] {"Unknown", "Vertical 2x2" }},
        //{771,  new string[] {"Unknown", "Slope 1x1" }},
        //{801,  new string[] {"Unknown", "Slope 1x2" }},
        //{803,  new string[] {"Unknown", "Side 1x1" }},
        //{805,  new string[] {"Unknown", "Side 1x2" }},
        //{817,  new string[] {"Unknown", "Side 2 1x2" }},
        //{807,  new string[] {"Unknown", "Corner 1x1" }},
        //{809,  new string[] {"Unknown", "Corner 1x2" }},
        //{811,  new string[] {"Unknown", "Round Vertical" }},
        //{813,  new string[] {"Unknown", "Round Corner" }},
        //{815,  new string[] {"Unknown", "Round Side" }},
        //{1185, new string[] {"Unknown", "Vertical L-Shape" }},
        //{1183, new string[] {"Unknown", "Vertical Corner" }},
        //{1197, new string[] {"Unknown", "Round Corner Thin" }},
        //{1198, new string[] {"Unknown", "Connector A" }},
        //{1199, new string[] {"Unknown", "Connector B" }},
        //{1200, new string[] {"Unknown", "Round Corner Long" }},
        //{1201, new string[] {"Unknown", "Round Corner Edge" }},
        //{1202, new string[] {"Unknown", "Corner Thin" }},

        public enum FaceIndex
        {
            All    = -1,
            Top    = 0,
            Bottom = 1,
            Front  = 2,
            Left   = 3,
            Back   = 4,
            Right  = 5
        }
        public enum SymbolRotation
        {
            Up, Left, Down, Right
        }

        public static EpbBlockType GetBlockType(UInt16 id)
        {
            EpbBlockType t = BlockTypes.Values.FirstOrDefault(d => d.Id == id);
            if (t == null)
            {
                t = new EpbBlockType(){Id = id, Name = $"{id}"};
            }
            return t;
        }
        public static EpbBlockType GetBlockType(string name, string variantName)
        {
            EpbBlockType t = BlockTypes.Values.FirstOrDefault(d => d.Name == name && BlockVariants.ContainsKey(d.Id) && Array.FindIndex(BlockVariants[d.Id], vName => vName == variantName) != -1);
            if (t == null)
            {
                t = BlockTypes.Values.FirstOrDefault(d => d.Name == name);
            }
            return t;
        }
        public static string GetBlockTypeName(UInt16 id)
        {
            string s = "";
            if (BlockTypes.ContainsKey(id))
            {
                s = $"\"{BlockTypes[id].Name}\"";
            }
            else
            {
                s = $"{id.ToString()}";
            }

            return $"{s} (0x{(UInt16)id:x4}={(UInt16)id})";

        }

        public static byte GetVariant(UInt16 blockTypeId, string variantName)
        {
            if (!BlockVariants.ContainsKey(blockTypeId))
            {
                return 0x00;
            }

            int i = Array.FindIndex(BlockVariants[blockTypeId], s => s == variantName);
            if (i == -1)
            {
                return 0x00;
            }

            return (byte)i;
        }

        public static string GetVariantName(UInt16 id, byte variant)
        {
            string s = "";
            if (BlockVariants.ContainsKey(id) && variant < BlockVariants[id].Length)
            {
                s = $"{BlockVariants[id][variant]}";
            }
            return s;
        }

        #endregion static

        public EpbBlockPos Position { get; protected set; }

        public EpbBlockType BlockType { get; set; }
        public EpbBlockRotation Rotation { get; set; }
        public UInt16 Unknown00 { get; set; }
        public byte Variant { get; set; }
        public string VariantName
        {
            get => GetVariantName(BlockType.Id, Variant);
            set => Variant = GetVariant(BlockType.Id, value);
        }

        public UInt16 DamageState {get; set;}
        
        public EpbColourIndex[] Colours = new EpbColourIndex[6];     // 5 bit colour index
        public byte[] Textures = new byte[6];        // 6 bit texture index
        public bool[] TextureFlips = new bool[6];
        public byte   SymbolPage { get; set; }       // 2 bit page index
        public byte[] Symbols = new byte[6];         // 5 bit symbol index
        public SymbolRotation[] SymbolRotations = new SymbolRotation[6]; // 2 bit symbol rotation


        public void SetColour(EpbColourIndex colour, FaceIndex face = FaceIndex.All)
        {
            if ((int)face < -1 || (int)face >= 6 || (byte)colour > 0x1f)
            {
                return;
            }

            if (face == FaceIndex.All)
            {
                for (int i = 0; i < 6; i++)
                {
                    Colours[i] = colour;
                }
            }
            else
            {
                Colours[(byte)face] = colour;
            }
        }
        public void SetTexture(byte texture, bool flip = false, FaceIndex face = FaceIndex.All)
        {
            if ((int)face < -1 || (int)face >= 6 || texture > 0x3f)
            {
                return;
            }

            if (face == FaceIndex.All)
            {
                for (int i = 0; i < 6; i++)
                {
                    Textures[i] = texture;
                    TextureFlips[i] = flip;
                }
            }
            else
            {
                Textures[(byte)face] = texture;
                TextureFlips[(byte)face] = flip;
            }
        }
        public void SetSymbol(byte symbol, SymbolRotation rotation = SymbolRotation.Up, FaceIndex face = FaceIndex.All)
        {
            if ((int)face < -1 || (int)face >= 6 || symbol > 0x1f)
            {
                return;
            }

            if (face == FaceIndex.All)
            {
                for (int i = 0; i < 6; i++)
                {
                    Symbols[i] = symbol;
                    SymbolRotations[i] = rotation;
                }
            }
            else
            {
                Symbols[(byte)face] = symbol;
                SymbolRotations[(byte)face] = rotation;
            }
        }


        public Dictionary<string, EpbBlockTag> Tags = new Dictionary<string, EpbBlockTag>();

        public void AddTag(EpbBlockTag tag)
        {
            Tags.Add(tag.Name, tag);
        }

        public EpbBlockTag GetTag(string name)
        {
            return Tags.ContainsKey(name) ? Tags[name] : null;
        }

        public EpbBlock(EpbBlockPos position)
        {
            Position = position;
            BlockType = GetBlockType("HullFullLarge", "Cube");
            Variant = GetVariant(BlockType.Id, "Cube");
            Rotation = EpbBlock.EpbBlockRotation.PzPy;
            Unknown00 = 0x0000;
        }

    }
}
