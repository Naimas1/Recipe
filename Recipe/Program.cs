using System;
using System.Collections.Generic;

public class Recipe
{
    public string Name { get; set; }
    public List<string> Ingredients { get; set; }
    public string Instructions { get; set; }
    public string ImageBase64 { get; set; } // Base64 encoded image of the dish
}
