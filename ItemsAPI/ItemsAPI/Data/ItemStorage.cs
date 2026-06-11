using ItemsApi.Models;

namespace ItemsApi.Data;

public static class ItemStorage
{
    public static List<Item> Items { get; } =
    [
        new Item
        {
            Id = 1,
            Name = "Keyboard",
            Price = 1200
        },

        new Item
        {
            Id = 2,
            Name = "Mouse",
            Price = 800
        }
    ];
}