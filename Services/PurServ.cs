using layering.Models;
using layering.Repository;

namespace layering.Services
{
    public class PurServ : IPurServ<Pur>
    {
        private readonly IPurRepocs<Pur> repobj;
        public PurServ(IPurRepocs<Pur> robj)
        {
            repobj = robj;
        }

        public void deletepur(int id)
        {
            repobj.deletepur(id);
        }

        public List<Pur> getallpurs()
        {
           return repobj.getallpurs();
        }


        public Pur getpur(int id)
        {
            return repobj.get(id);
        }

        public void postpur(Pur pur)
        {
            repobj.postpur(pur);
        }
    }
}
