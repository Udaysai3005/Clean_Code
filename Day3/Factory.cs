namespace hamaraBasket;

public class Factory
{
    public List<Item> PrepareItemList(string name, int sellIn, int qualityIn)
    {
        return new List<Item>
        {
            new Item { Name = name, SellIn = sellIn, Quality = qualityIn },
        };
    }
    public void UpdateQualityHelper(List<Item> Items)
    {
        HamaraBasket app = new HamaraBasket(Items);
        app.UpdateQuality();
    }
}
