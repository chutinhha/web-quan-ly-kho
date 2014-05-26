using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using Nhansu_Lib.DB_Class;
using CrystalDecisions.Shared;
namespace QLCV.PageSystem
{
    public partial class ReportView : System.Web.UI.Page
    {
        string empAuthorityCode = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            // ---- KIEM TRA LOGIN -----
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/login.aspx");
            }
            // ---- END KT LOGIN -------
           // if(!IsPostBack)

            if (Request["M_Code"] == null)
                ViewReport();
            else
            {
                if (Request["E_Id"] != null)
                    ViewReport_CVInformation();
                else
                    ViewReport_Salary();
            }
        }
        #region "U_Function"

        private void ViewReport()
        {
            try
            {
                ReportDocument reportDocument = new ReportDocument();
                string sReportID = Request.QueryString["ID_Report"].ToString();
                cls_Catalog_Report objRe = new cls_Catalog_Report();
                DataTable _dtRe = objRe.GetReport(sReportID);
                DataTable _dtPara = Session["ReportPara"] == null ? null : (DataTable)(Session["ReportPara"]);
                DataTable _dtP = _dtPara.Clone();
                DataTable _dtPReport = _dtPara.Clone();
                DataTable _dtSource;
                string[] sArr = _dtRe.Rows[0]["SPName"].ToString().Split('@');
                cls_Report_User objUser = new cls_Report_User();
                if (sArr.Length > 1)
                {
                    for (int i = 0; i < _dtPara.Rows.Count; i++)
                    {
                        bool Exist = false;
                        for (int k = 1; k < sArr.Length; k++)
                            if (_dtPara.Rows[i]["ParaName"].ToString().ToUpper() == sArr[k].ToUpper())
                            {
                                Exist = true;
                                break;
                            }
                        if (Exist)
                        {
                            _dtP.ImportRow(_dtPara.Rows[i]);
                        }
                        else
                            _dtPReport.ImportRow(_dtPara.Rows[i]);
                    }
                  _dtSource = objUser.GetDataSourceReport(_dtP, sArr[0]);
                }
                else
                    _dtSource = objUser.GetSP(sArr[0]);
                //
                if (sArr.Length >= 1 && sArr[0].Length <= 0)
                    _dtPReport = _dtPara;

                string sPath = ConfigurationManager.AppSettings["Report_File"].ToString() + _dtRe.Rows[0]["File_Path"].ToString();
                reportDocument.Load(sPath);
                //tham so bao cao

                if (_dtSource != null)
                    reportDocument.SetDataSource(_dtSource);

                for (int k = 0; k < _dtP.Rows.Count; k++)
                {
                    for (int i = 0; i < reportDocument.ParameterFields.Count; i++)
                    {
                        if (reportDocument.ParameterFields[i].Name.ToUpper() == _dtP.Rows[k]["ParaName"].ToString().ToUpper())
                            reportDocument.SetParameterValue(reportDocument.ParameterFields[i].Name, _dtP.Rows[k]["ParaValue"]);
                    }
                }
                if (_dtPReport.Rows.Count > 0)
                    for (int i = 0; i < _dtPReport.Rows.Count; i++)
                        reportDocument.SetParameterValue(_dtPReport.Rows[i]["ParaName"].ToString(), _dtPReport.Rows[i]["ParaValue"]);

                CrystalReportViewer.ReportSource = reportDocument;

                CrystalReportViewer.DataBind();
            }
            catch (Exception ex)
            {
            }
        }
        private void ViewReport_CVInformation()
        {
            try
            {
                if(Request["M_Code"].ToString() =="Emp")
                {
                    //check quyền của người dùng
                    short canView = bindAuthority("Information");

                    if (canView > 0)
                    {
                        ReportDocument reportDocument = new ReportDocument();
                        string[] sArr = ConfigurationSettings.AppSettings["Report_Emp_CVSP"].Split(',');
                        DataSet _dtSource = new DataSet();
                        DataTable _dtPara = new DataTable();
                        _dtPara.Columns.Add("ParaName");
                        _dtPara.Columns.Add("ParaValue");
                        _dtPara.Rows.Add("@EmployeeID", Request["E_Id"].ToString());

                        cls_Report_User objUser = new cls_Report_User();
                        DataTable _dtTemp;

                        for (int i = 0; i < sArr.Count(); i++)
                        {
                            _dtTemp = objUser.GetDataSourceReport(_dtPara, sArr[i]);
                            _dtTemp.TableName = sArr[i];
                            _dtSource.Tables.Add(_dtTemp.Copy());
                        }

                        string sPath = ConfigurationManager.AppSettings["Report_Emp_CVFile"].ToString();
                        reportDocument.Load(sPath);

                        if (_dtSource != null) reportDocument.SetDataSource(_dtSource);

                        CrystalReportViewer.ReportSource = reportDocument;
                        CrystalReportViewer.DataBind();
                    }
                    else Response.Redirect("~/ErrorPermission.aspx");
                }
                else Response.Redirect("~/ErrorPermission.aspx");
            }
            catch (Exception ex)
            {
            }
        }
        private void ViewReport_Salary()
        {
            try
            {
                if ((Request["M_Code"].ToString() == "Emp")&& (Request["CompanyId"] != null)
                    && (Request["YEAR"] != null) && (Request["MONTH"] != null)
                    && (Request["S_Type"] != null))
                {
                    //check quyền của người dùng
                    short canView = bindAuthority("Salary");

                    if (canView > 0)
                    {
                        ReportDocument reportDocument = new ReportDocument();
                        string spName = ConfigurationSettings.AppSettings["Report_Emp_SalarySP"];
                        DataSet _dtSource = new DataSet();
                        DataTable _dtPara = new DataTable();
                        _dtPara.Columns.Add("ParaName");
                        _dtPara.Columns.Add("ParaValue");
                        _dtPara.Rows.Add("@CompanyId", Request["CompanyId"].ToString());
                        _dtPara.Rows.Add("@Formula_Type", Request["S_Type"].ToString());
                        _dtPara.Rows.Add("@Month", Request["MONTH"].ToString());
                        _dtPara.Rows.Add("@Year", Request["YEAR"].ToString());

                        cls_Report_User objUser = new cls_Report_User();
                        DataTable _dtTemp;
                        _dtTemp = objUser.GetDataSourceReport(_dtPara, spName);
                      //  _dtSource.Tables.Add(_dtTemp.Copy());

                        string sPath = ConfigurationManager.AppSettings["Report_Emp_SalaryFile"].ToString();
                        reportDocument.Load(sPath);

                        if (_dtSource != null) reportDocument.SetDataSource(_dtTemp);
                        //reportDocument.SetParameterValue("@CompanyId", Request["CompanyId"].ToString());
                        //reportDocument.SetParameterValue("@Formula_Type", Request["S_Type"].ToString());
                        //reportDocument.SetParameterValue("@Month", Request["MONTH"].ToString());
                        //reportDocument.SetParameterValue("@Year", Request["YEAR"].ToString());
                        reportDocument.SetParameterValue("Month", Request["MONTH"].ToString());
                        reportDocument.SetParameterValue("Year", Request["YEAR"].ToString());

                        CrystalReportViewer.ReportSource = reportDocument;
                        CrystalReportViewer.DataBind();
                    }
                    else Response.Redirect("~/ErrorPermission.aspx");

                }
                else Response.Redirect("~/ErrorPermission.aspx");
            }
            catch (Exception ex)
            {
            }
        }
        ReportDocument ReportSourceSetup(string sPath, ConnectionInfo crConnectionInfo)
        {
            ReportDocument crDoc = new ReportDocument();
            TableLogOnInfos crTableLogonInfos = new TableLogOnInfos();
            TableLogOnInfo crTableLogonInfo = new TableLogOnInfo();
            Tables crTables;

            crDoc.Load(sPath);

            // Each table in report needs to have logoninfo setup:
            crTables = crDoc.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
            {
                crTableLogonInfo = crTable.LogOnInfo;
                crTableLogonInfo.ConnectionInfo = crConnectionInfo;
                crTable.ApplyLogOnInfo(crTableLogonInfo);
            }

            return crDoc;
        }
        #endregion

        #region bindAuthority
        private short bindAuthority(string partCode)
        {
            try
            {
                string moduleCode = ConfigurationSettings.AppSettings["Emp_Module"];
                int userId = Convert.ToInt32(Session["UserId"]);
                cls_Module_Authority_Employee aEmp = new cls_Module_Authority_Employee();
                aEmp.EmployeeId = userId;

                DataTable dt = aEmp.SelectAuthority(moduleCode);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (partCode == "Information")
                    {
                        //Chi quyen Employee Full moi co quyen dieu chuyen nhan su
                        if (dt.Rows[i]["AuthorityCode"].ToString() == ConfigurationSettings.AppSettings["Emp_FullAuthority"])
                            empAuthorityCode = dt.Rows[i]["AuthorityCode"].ToString();

                        if ((dt.Rows[i]["AuthorityCode"].ToString() == ConfigurationSettings.AppSettings["Emp_PartAuthority"]) &&
                            (empAuthorityCode.Length <= 0))
                            empAuthorityCode = dt.Rows[i]["AuthorityCode"].ToString();

                        if ((dt.Rows[i]["AuthorityCode"].ToString() == ConfigurationSettings.AppSettings["Emp_LeaderAuthority"]) &&
                           (empAuthorityCode.Length <= 0))
                            empAuthorityCode = dt.Rows[i]["AuthorityCode"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[i]["AuthorityCode"].ToString() == ConfigurationSettings.AppSettings["SalaryAuthority"])
                            empAuthorityCode = dt.Rows[i]["AuthorityCode"].ToString();
                    }
                }
                short canView = 0;

                if (empAuthorityCode.Length > 0)
                {
                    if (partCode == "Information")
                    {
                        aEmp = new cls_Module_Authority_Employee();
                        aEmp.EmployeeId = userId;

                        int eId;
                        eId = Convert.ToInt32(Request["E_Id"]);
                        short canChange = aEmp.Check_EAuthority(eId, empAuthorityCode,
                                        ConfigurationSettings.AppSettings["Basic_EmpM"], out canView);
                    }
                    else canView = 1;
                }

                return canView;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        #endregion bindAuthority
    }
}
