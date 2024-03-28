namespace Eagles_Website.Repository.IRepository
{
    public interface IUnitOFWork
    {
        ICategoryRepository CategoryRepo { get; }
        IProductRepository ProductRepo { get; }
        ICartRepository CartRepo { get; }
        ICartItemRepository CartItemRepo { get; }
        IOrderRepository OrderRepo { get; }
        IOrderDetailRepository OrderDetailRepo { get; }




    }
}
