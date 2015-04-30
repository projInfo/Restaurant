using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjInfo
{
    abstract class TableJumelable :Table
    {
        protected int _coteJumelable, _idJumele1=0, _idJumele2=0;

        public TableJumelable(int nbrPlace, XElement tableGen)
            : base(nbrPlace, tableGen)
        {
        }

        public TableJumelable(int id, int nbrPlace, XElement tableGen)
            : base(nbrPlace, id, tableGen)
        {
        }
        
        public bool JumelableAvec(TableJumelable T)
        {
            if ((T._idJumele1 == 0 || T._idJumele2 == 0) && (this._idJumele1 == 0 || this._idJumele2 == 0))
            {
                if (this._coteJumelable == T._coteJumelable)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public void JumeleAvec(TableJumelable T)
        {
            if(this._idJumele1==0)
            {
                if(T._idJumele1==0)
                {
                    this._idJumele1 = T._id;
                    T._idJumele1 = this._id;
                }
                else
                {
                    this._idJumele1 = T._id;
                    T._idJumele2 = this._id;
                }
            }
            else
            {
                if (T._idJumele1 == 0)
                {
                    this._idJumele2 = T._id;
                    T._idJumele1 = this._id;
                }
                else
                {
                    this._idJumele2 = T._id;
                    T._idJumele2 = this._id;
                }
            }
        }
    }
}
