using NPOI.SS.UserModel;

namespace TranQuiv.Lib.Extensions.NPOI;

public static class WorkbookExtensions
{
    public static CellStyleBuilder CellStyleBuilder(this IWorkbook wb)
    {
        return new CellStyleBuilder(wb);
    }

    public static FontBuilder FontBuilder(this IWorkbook wb)
    {
        return new FontBuilder(wb);
    }
}
