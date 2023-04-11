namespace layering.Repository
{
    public interface IStockistRepo<Stockist>
    {
        List<Stockist> GetAll();    
    }
}
