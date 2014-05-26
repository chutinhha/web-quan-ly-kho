using System.Data;
namespace QLCV.code.common
{
    public class Paper
    {
        public Paper()
        {
        }

        public static DataTable MakeDataPaper(int totalPages, int currPages, int recordPerPages)
        {
            DataTable dtRet = new DataTable("DataPaper");
            dtRet.Columns.Add("ID", typeof(string));
            dtRet.Columns.Add("CssClass", typeof(string));
            dtRet.Columns.Add("Text", typeof(string));
            dtRet.Columns.Add("Page", typeof(int));
            dtRet.Columns.Add("Type", typeof(int));
            if (totalPages > 1)
            {
                int ranges = currPages / recordPerPages;
                if (currPages % recordPerPages > 0)
                    ranges += 1;
                int start = recordPerPages * (ranges - 1) + 1;
                int end = start + recordPerPages;
                if (end > totalPages)
                    end = totalPages + 1;
                DataRow dr;
                if (currPages > 1)
                {
                    //Trang dau tien
                    dr = dtRet.NewRow();
                    dr["ID"] = "first";
                    dr["CssClass"] = "pager";
                    dr["Text"] = "Đầu";
                    dr["Page"] = "1";
                    dr["Type"] = 0;
                    dtRet.Rows.Add(dr);
                    //Quay lai
                    dr = dtRet.NewRow();
                    dr["ID"] = "back";
                    dr["CssClass"] = "pager";
                    dr["Text"] = "trước";
                    dr["Page"] = (currPages - 1).ToString();
                    dr["Type"] = 0;
                    dtRet.Rows.Add(dr);
                }
                //
                for (int i = start; i < end; i++)
                {
                    if (i == currPages)
                    {
                        dr = dtRet.NewRow();
                        dr["ID"] = i.ToString();
                        dr["CssClass"] = "pager-current";
                        dr["Text"] = i.ToString();
                        dr["Page"] = i.ToString();
                        dr["Type"] = 0;
                        dtRet.Rows.Add(dr);
                    }
                    else
                    {
                        dr = dtRet.NewRow();
                        dr["ID"] = i.ToString();
                        dr["CssClass"] = "pager";
                        dr["Text"] = i.ToString();
                        dr["Page"] = i.ToString();
                        dr["Type"] = 0;
                        dtRet.Rows.Add(dr);
                    }
                }
                if (currPages < totalPages)
                {
                    //Trang tiep theo
                    dr = dtRet.NewRow();
                    dr["ID"] = "next";
                    dr["CssClass"] = "pager";
                    dr["Text"] = "sau";
                    dr["Page"] = (currPages + 1).ToString();
                    dr["Type"] = 0;
                    dtRet.Rows.Add(dr);
                    //Trang cuoi cung
                    dr = dtRet.NewRow();
                    dr["ID"] = "last";
                    dr["CssClass"] = "pager";
                    dr["Text"] = "Cuối";
                    dr["Page"] = totalPages.ToString();
                    dr["Type"] = 0;
                    dtRet.Rows.Add(dr);
                }
                dtRet.AcceptChanges();
            }
            return dtRet;
        }
        /*public static DataTable MakeDataPaper(int totalPages, int currPages, int recordPerPages)
        {
            DataTable dtRet = new DataTable("DataPaper");
            dtRet.Columns.Add("ID", typeof(string));
            dtRet.Columns.Add("CssClass", typeof(string));
            dtRet.Columns.Add("Text", typeof(string));
            dtRet.Columns.Add("Page", typeof(int));
            dtRet.Columns.Add("Type", typeof(int));
            if (totalPages > 1)
            {
                int ranges = currPages / recordPerPages;
                if (currPages % recordPerPages > 0)
                    ranges += 1;
                int start = recordPerPages * (ranges - 1) + 1;
                int end = start + recordPerPages;
                if (end > totalPages)
                    end = totalPages + 1;
                DataRow dr = null;
                if (currPages > 1)
                {
                    //Trang dau tien
                    dr = dtRet.NewRow();
                    dr["ID"] = "first";
                    dr["CssClass"] = "pager";
                    dr["Text"] = "trangdau.gif";
                    dr["Page"] = "1";
                    dr["Type"] = 1;
                    dtRet.Rows.Add(dr);
                    //Quay lai
                    dr = dtRet.NewRow();
                    dr["ID"] = "back";
                    dr["CssClass"] = "pager";
                    dr["Text"] = "trangtruoc.gif";
                    dr["Page"] = (currPages - 1).ToString();
                    dr["Type"] = 1;
                    dtRet.Rows.Add(dr);
                }
                //
                for (int i = start; i < end; i++)
                {
                    if (i == currPages)
                    {
                        dr = dtRet.NewRow();
                        dr["ID"] = i.ToString();
                        dr["CssClass"] = "pager-current";
                        dr["Text"] = i.ToString();
                        dr["Page"] = i.ToString();
                        dr["Type"] = 0;
                        dtRet.Rows.Add(dr);
                    }
                    else
                    {
                        dr = dtRet.NewRow();
                        dr["ID"] = i.ToString();
                        dr["CssClass"] = "pager";
                        dr["Text"] = i.ToString();
                        dr["Page"] = i.ToString();
                        dr["Type"] = 0;
                        dtRet.Rows.Add(dr);
                    }
                }
                if (currPages < totalPages)
                {
                    //Trang tiep theo
                    dr = dtRet.NewRow();
                    dr["ID"] = "next";
                    dr["CssClass"] = "pager";
                    dr["Text"] = "trangsau.gif";
                    dr["Page"] = (currPages + 1).ToString();
                    dr["Type"] = 1;
                    dtRet.Rows.Add(dr);
                    //Trang cuoi cung
                    dr = dtRet.NewRow();
                    dr["ID"] = "last";
                    dr["CssClass"] = "pager";
                    dr["Text"] = "trangcuoi.gif";
                    dr["Page"] = totalPages.ToString();
                    dr["Type"] = 1;
                    dtRet.Rows.Add(dr);
                }
                dtRet.AcceptChanges();
            }
            return dtRet;
        }*/
        public static DataTable MakeDataPaper1(int totalPages, int currPages, int recordPerPages)
        {
            DataTable dtRet = new DataTable("DataPaper");
            dtRet.Columns.Add("ID", typeof(string));
            dtRet.Columns.Add("CssClass", typeof(string));
            dtRet.Columns.Add("Text", typeof(string));
            dtRet.Columns.Add("Page", typeof(int));
            dtRet.Columns.Add("Type", typeof(int));
            if (totalPages > 1)
            {
                int ranges = currPages / recordPerPages;
                if (currPages % recordPerPages > 0)
                    ranges += 1;
                int start = recordPerPages * (ranges - 1) + 1;
                int end = start + recordPerPages;
                if (end > totalPages)
                    end = totalPages + 1;
                DataRow dr;
               
                for (int i = start; i < end; i++)
                {
                    if (i == currPages)
                    {
                        dr = dtRet.NewRow();
                        dr["ID"] = i.ToString();
                        dr["CssClass"] = "pager-current";
                        dr["Text"] = i.ToString();
                        dr["Page"] = i.ToString();
                        dr["Type"] = 0;
                        dtRet.Rows.Add(dr);
                    }
                    else
                    {
                        dr = dtRet.NewRow();
                        dr["ID"] = i.ToString();
                        dr["CssClass"] = "pager";
                        dr["Text"] = i.ToString();
                        dr["Page"] = i.ToString();
                        dr["Type"] = 0;
                        dtRet.Rows.Add(dr);
                    }
                }
               
                dtRet.AcceptChanges();
            }
            return dtRet;
        }
    }
}
