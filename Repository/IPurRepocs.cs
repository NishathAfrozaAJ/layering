namespace layering.Repository
{
    public interface IPurRepocs<Pur>
    {
        List<Pur> getallpurs();
        Pur get(int id);
        
        void postpur(Pur pur);
        void deletepur(int id);

    }
}
