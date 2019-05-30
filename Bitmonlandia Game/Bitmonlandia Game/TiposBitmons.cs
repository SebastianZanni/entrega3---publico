using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitmonlandia_Game
{
  
      public class TiposBitmons
    {
        public Dictionary<int, string> tipo;

        public TiposBitmons()
        {
            tipo = new Dictionary<int, string>();
            tipo.Add(0, "Dorvalo");
            tipo.Add(1, "Doti");
            tipo.Add(2, "Ent");
            tipo.Add(3, "Gofue");
            tipo.Add(4, "Taplan");
            tipo.Add(5, "Wetar");

        }
    }

}
