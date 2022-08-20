namespace Everyday.Core.Dictionaries
{
    public enum AesType
    {
        AES128 = 0,
        AES192 = 1,
        AES256 = 2,
    }

    public enum AppModule
    {
        Purchases = 0,
        Alerts = 1,
        Inventory = 2,
        Finance = 4
    }

    public enum ItemCateoryType
    {
        Consumable = 1,
        Chemical = 2,
        Container = 3
    }

    public enum TrashType
    {
        Bio = 1,
        Mixed = 2,
        Paper = 3,
        Metal_and_plastic = 4,
        Gass = 5
    }

    public enum MeasureUnit
    {
        Gramme = 1,
        Milimeter = 2
    }
}
