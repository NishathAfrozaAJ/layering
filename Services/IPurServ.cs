using layering.Models;

namespace layering.Services
{
    public interface IPurServ<Pur>
    {
        List<Pur> getallpurs();
        Pur getpur(int id);

        void postpur(Pur pur);
        void deletepur(int id);

    }
}
