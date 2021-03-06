#!/bin/sh

## Extracts a list of allowed blocktypes from epb dumps.
##
## Use this script to generate source code for a method that initialises the
## AllowedIn arrays for every block type. This is done by identifying all the
## blueprint types of all blocks used in the input files. The input files must
## contain concatenated output by the epb tool, for example generated by the
## listAll script.
##

gawk '\
BEGIN \
{
    split("", types);
    bpType="";
}
/^Type:/ \
{
    bpType = $2;
}
/Type=.*Variant=/ \
{
    match($9,/=([0-9]+)\)/, m);
    blockType = int(m[1]);
    if (types[blockType] == "")
    {
        types[blockType] = "BlueprintType." bpType;
    }
    else
    {
        if (index(types[blockType], bpType) == 0)
        {
            types[blockType] = sprintf("%s, BlueprintType.%s", types[blockType], bpType);
        }
    }
}
END \
{
    printf ("        public static void InitAllowedIn()\n");
    printf ("        {\n");
    n = asorti(types, sortedIndices, "@ind_num_asc");
    for (i = 1; i <=n; i++)
    {
        blockType = sortedIndices[i];
        printf ("            if (BlockTypes.ContainsKey(%s)) BlockTypes[%s].AllowedIn = new BlueprintType[] {%s};\n", blockType, blockType, types[blockType]);
    }
    printf ("        }\n");
}
' "$1" "$2" "$3" "$4" "$5" "$6"
