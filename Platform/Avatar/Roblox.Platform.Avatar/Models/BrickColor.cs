using System.Collections.Generic;

namespace Roblox.Platform.Avatar
{
    public class BrickColorEntry
    {
        public int brickColorId { get; set; }
        public string hexColor { get; set; }
        public string name { get; set; }
    }

    public static class BrickColorList
    {
        public static List<BrickColorEntry> colorList = new()
        {
            new() { brickColorId = 361, hexColor = "#564236", name = "Dirt brown" },
            new() { brickColorId = 192, hexColor = "#694028", name = "Reddish brown" },
            new() { brickColorId = 217, hexColor = "#7C5C46", name = "Brown" },
            new() { brickColorId = 153, hexColor = "#957977", name = "Sand red" },
            new() { brickColorId = 359, hexColor = "#AF9483", name = "Linen" },
            new() { brickColorId = 352, hexColor = "#C7AC78", name = "Burlap" },
            new() { brickColorId = 5, hexColor = "#D7C59A", name = "Brick yellow" },
            new() { brickColorId = 101, hexColor = "#DA867A", name = "Medium red" },
            new() { brickColorId = 1007, hexColor = "#A34B4B", name = "Dusty Rose" },
            new() { brickColorId = 1014, hexColor = "#AA5500", name = "CGA brown" },
            new() { brickColorId = 38, hexColor = "#A05F35", name = "Dark orange" },
            new() { brickColorId = 18, hexColor = "#CC8E69", name = "Nougat" },
            new() { brickColorId = 125, hexColor = "#EAB892", name = "Light orange" },
            new() { brickColorId = 1030, hexColor = "#FFCC99", name = "Pastel brown" },
            new() { brickColorId = 133, hexColor = "#D5733D", name = "Neon orange" },
            new() { brickColorId = 106, hexColor = "#DA8541", name = "Bright orange" },
            new() { brickColorId = 105, hexColor = "#E29B40", name = "Br. yellowish orange" },
            new() { brickColorId = 1017, hexColor = "#FFAF00", name = "Deep orange" },
            new() { brickColorId = 24, hexColor = "#F5CD30", name = "Bright yellow" },
            new() { brickColorId = 334, hexColor = "#F8D96D", name = "Daisy orange" },
            new() { brickColorId = 226, hexColor = "#FDEA8D", name = "Cool yellow" },
            new() { brickColorId = 141, hexColor = "#27462D", name = "Earth green" },
            new() { brickColorId = 1021, hexColor = "#3A7D15", name = "Camo" },
            new() { brickColorId = 28, hexColor = "#287F47", name = "Dark green" },
            new() { brickColorId = 37, hexColor = "#4B974B", name = "Bright green" },
            new() { brickColorId = 310, hexColor = "#5B9A4C", name = "Shamrock" },
            new() { brickColorId = 317, hexColor = "#7C9C6B", name = "Moss" },
            new() { brickColorId = 119, hexColor = "#A4BD47", name = "Br. yellowish green" },
            new() { brickColorId = 1011, hexColor = "#002060", name = "Navy blue" },
            new() { brickColorId = 1012, hexColor = "#2154B9", name = "Deep blue" },
            new() { brickColorId = 1010, hexColor = "#0000FF", name = "Really blue" },
            new() { brickColorId = 23, hexColor = "#0D69AC", name = "Bright blue" },
            new() { brickColorId = 305, hexColor = "#527CAE", name = "Steel blue" },
            new() { brickColorId = 102, hexColor = "#6E99CA", name = "Medium blue" },
            new() { brickColorId = 45, hexColor = "#B4D2E4", name = "Light blue" },
            new() { brickColorId = 107, hexColor = "#008F9C", name = "Bright bluish green" },
            new() { brickColorId = 1018, hexColor = "#12EED4", name = "Teal" },
            new() { brickColorId = 1027, hexColor = "#9FF3E9", name = "Pastel blue-green" },
            new() { brickColorId = 1019, hexColor = "#00FFFF", name = "Toothpaste" },
            new() { brickColorId = 1013, hexColor = "#04AFEC", name = "Cyan" },
            new() { brickColorId = 11, hexColor = "#80BBDC", name = "Pastel Blue" },
            new() { brickColorId = 1024, hexColor = "#AFDDFF", name = "Pastel light blue" },
            new() { brickColorId = 104, hexColor = "#6B327C", name = "Bright violet" },
            new() { brickColorId = 1023, hexColor = "#8C5B9F", name = "Lavender" },
            new() { brickColorId = 321, hexColor = "#A75E9B", name = "Lilac" },
            new() { brickColorId = 1015, hexColor = "#AA00AA", name = "Magenta" },
            new() { brickColorId = 1031, hexColor = "#6225D1", name = "Royal purple" },
            new() { brickColorId = 1006, hexColor = "#B480FF", name = "Alder" },
            new() { brickColorId = 1026, hexColor = "#B1A7FF", name = "Pastel violet" },
            new() { brickColorId = 21, hexColor = "#C4281C", name = "Bright red" },
            new() { brickColorId = 1004, hexColor = "#FF0000", name = "Really red" },
            new() { brickColorId = 1032, hexColor = "#FF00BF", name = "Hot pink" },
            new() { brickColorId = 1016, hexColor = "#FF66CC", name = "Pink" },
            new() { brickColorId = 330, hexColor = "#FF98DC", name = "Carnation pink" },
            new() { brickColorId = 9, hexColor = "#E8BAC8", name = "Light reddish violet" },
            new() { brickColorId = 1025, hexColor = "#FFC9C9", name = "Pastel orange" },
            new() { brickColorId = 364, hexColor = "#5A4C42", name = "Dark taupe" },
            new() { brickColorId = 351, hexColor = "#BC9B5D", name = "Cork" },
            new() { brickColorId = 1008, hexColor = "#C1BE42", name = "Olive" },
            new() { brickColorId = 29, hexColor = "#A1C48C", name = "Medium green" },
            new() { brickColorId = 1022, hexColor = "#7F8E64", name = "Grime" },
            new() { brickColorId = 151, hexColor = "#789082", name = "Sand green" },
            new() { brickColorId = 135, hexColor = "#74869D", name = "Sand blue" },
            new() { brickColorId = 1020, hexColor = "#00FF00", name = "Lime green" },
            new() { brickColorId = 1028, hexColor = "#CCFFCC", name = "Pastel green" },
            new() { brickColorId = 1009, hexColor = "#FFFF00", name = "New Yeller" },
            new() { brickColorId = 1029, hexColor = "#FFFFCC", name = "Pastel yellow" },
            new() { brickColorId = 1003, hexColor = "#111111", name = "Really black" },
            new() { brickColorId = 26, hexColor = "#1B2A35", name = "Black" },
            new() { brickColorId = 199, hexColor = "#635F62", name = "Dark stone grey" },
            new() { brickColorId = 194, hexColor = "#A3A2A5", name = "Medium stone grey" },
            new() { brickColorId = 1002, hexColor = "#CDCDCD", name = "Mid gray" },
            new() { brickColorId = 208, hexColor = "#E5E4DF", name = "Light stone grey" },
            new() { brickColorId = 1, hexColor = "#F2F3F3", name = "White" },
            new() { brickColorId = 1001, hexColor = "#F8F8F8", name = "Institutional white" }
        };
    }
}