using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreynQuiv.Lib.Database;

namespace TreynQuiv.Lib.Console;

public class TestEntity : IEntity
{
    public TestEntity()
    {
    }

    public int Val { get; set; }
}
