using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreynQuiv.Lib.EntityFrameworkCore;

public interface IEntity
{

}

public interface IIdentityEntity : IEntity
{
    public dynamic Id { get; set; }
}
