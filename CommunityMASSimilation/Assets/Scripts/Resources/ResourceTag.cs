using System;

public enum ResourceTag
{
    Food,
    BasicNecessities,
    Luxurious,
    Ingredience
}

public static class ResourceTagExtension
{
    public static bool IsUsable(this ResourceTag tag)
    {
        return tag == ResourceTag.Food || tag == ResourceTag.BasicNecessities || tag == ResourceTag.Luxurious;
    }

    public static string GetText(this ResourceTag tag)
    {
        switch(tag)
        {
            case ResourceTag.BasicNecessities:
                return "Podstawowe potrzeby";
            case ResourceTag.Food:
                return "Pożywienie";
            case ResourceTag.Luxurious:
                return "Artykuły luksusowe";
            case ResourceTag.Ingredience:
                return "Zasoby produkcyjne";
            default:
                throw new InvalidCastException();
        }
    }
}

