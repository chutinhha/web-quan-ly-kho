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
using Nhansu_Lib.DB_Class;
using System.Collections.Generic;
using Telerik.Web.UI;

namespace QLCV.DanhMuc
{
    public partial class Position_Detail : System.Web.UI.Page
    {
        public static DataTable dtPosition = null;

        private static short option = 0;
        private static int PositionId;

        private int EditIndex = -1;
        protected DataSet cboBelongPosition = null;
        protected DataSet cboEduMajor = null;
        protected DataSet cboEduLevel = null;
        protected DataSet cboEngLevel = null;
        protected DataSet dsPRoles = null;

        private static DataTable dtPosRoles = null;
        private static int countPosRoles = 0;
        private static bool stateInsertbtn;
        private static bool stateUpdatebtn;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // ---- KIEM TRA LOGIN -----
                Session["UserId"] = '1';

                //--- SET TRANG THAI THAO TAC CHO FORM --
                if (Request.QueryString["P_Option"] != null)
                    option = Convert.ToInt16(Request.QueryString["P_Option"]);

                stateInsertbtn = true;
                stateUpdatebtn = true;

                bindRadList();
                bindData(-1);
            }
        }

        private void bindRadList()
        {
            // ---- BIND DU LIEU VAO LISTVIEW --
            dtPosition = new cls_Catalog_Position().SelectListDT();
            RadGrid1.DataSource = dtPosition;
            RadGrid1.DataBind();

            if (dtPosition.Rows.Count > 0)
                PositionId = Convert.ToInt32(dtPosition.Rows[0]["Id"]);
        }

        protected void RadGrid1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PositionId = Convert.ToInt32(RadGrid1.SelectedValue.ToString());
            stateUpdatebtn = true;
            stateInsertbtn = true;

            btnDelete.Enabled = true;
            btnUpdate.Enabled = true;
            btnInsert.Text = "Thêm";
            btnInsert1.Text = "Thêm";
            btnUpdate.Text = "Sửa";
            btnUpdate1.Text = "Sửa";
            ltlAnnouncement.Text = "";

            option = 0;                                                      //CHUYEN QUA TRANG THAI VIEW
            bindData(-1);
        }

        private void bindData(int index)
        {
            // ---- BIND DU LIEU VAO CAC DROP LIST
            cboBelongPosition = new cls_Catalog_Position().SelectList();
            cboEduMajor = new cls_Catalog_EducationMajor().Select();
            cboEngLevel = new cls_Catalog_EducationLangLevel().SelectEnglish();
            cboEduLevel = new cls_Catalog_EducationLevel().Select();

            dlBelongName.DataSource = cboBelongPosition.Tables[0];
            dlEngLevel.DataSource = cboEngLevel;
            dlEngLevel.DataTextField = "LanguageLevel";
            dlEngLevel.DataValueField = "Id";

            dlBelongName.DataBind();
            dlEduLevel.DataBind();
            dlEduMajor.DataBind();
            dlEngLevel.DataBind();

            if(option != 1)                                                         // KHONG PHAI TRANG THAI INSERT
            {
                if (dtPosition.Rows.Count > 0)
                {
                    try
                    {
                        //LAY THONG TIN VTCV TUONG UNG
                        cls_Catalog_Position cPosition = new cls_Catalog_Position();
                        cPosition.Id = PositionId;
                        cPosition = cPosition.Select();

                        //VIEW DU LIEU LEN
                        txtPositionName.Text = cPosition.PositionName;
                        dlBelongName.SelectedValue = cPosition.Belong_PositionId.ToString();
                        txtSalaryLevel.Text = cPosition.Salary_Level.ToString();
                        txtDescrible.Text = cPosition.Describle;
                        dlEduMajor.SelectedValue = cPosition.Education_MajorId.ToString();
                        dlEduLevel.SelectedValue = cPosition.Education_LevelId.ToString();
                        dlEngLevel.SelectedValue = cPosition.English_LevelId.ToString();
                        txtITLevel.Text = cPosition.Informatic_Level;
                        txtMLevel.Text = cPosition.Management_Level;
                        txtPLevel.Text = cPosition.Politics_Level;
                        txtSkills.Text = cPosition.Skills;
                        txtOther.Text = cPosition.Other_Requires;

                        //LAY DANH SACH NHIEM VU CAC CONG VIEC
                        cls_Catalog_PositionRoles cPRoles = new cls_Catalog_PositionRoles();
                        cPRoles.Positionid = PositionId;
                        dsPRoles = cPRoles.Select();

                        m_grid.EditIndex = index;


                        if ((dsPRoles.Tables[0] == null) || (dsPRoles.Tables[0].Rows.Count <= 0))
                        {
                            dsPRoles.Tables[0].Rows.Add(dsPRoles.Tables[0].NewRow());
                            m_grid.DataSource = dsPRoles.Tables[0];
                            m_grid.DataBind();
                            m_grid.Rows[0].Visible = false;
                        }
                        else
                        {
                            m_grid.DataSource = dsPRoles.Tables[0];
                            m_grid.DataBind();
                        }

                        bool optionStatus;
                        if ((option == 1) || (option == 2))                           // TRANG THAI INSERT HOAC UPDATE
                            optionStatus = true;
                        else
                            optionStatus = false;

                        if (m_grid.Rows.Count > 0)
                        {
                            string confirm = "return confirm('Bạn thật sự muốn xóa row này?')";

                            for (int i = 0; i < m_grid.Rows.Count; i++)
                            {
                                GridViewRow row = m_grid.Rows[i];
                                LinkButton lbutton1 = (LinkButton)row.FindControl("cmdDelete");
                                if (lbutton1 != null)
                                {
                                    lbutton1.Visible = optionStatus;
                                    lbutton1.Attributes.Add("onclick", confirm);
                                }

                                LinkButton lbutton2 = (LinkButton)row.FindControl("cmdEdit");
                                if (lbutton2 != null)
                                    lbutton2.Visible = optionStatus;
                            }
                        }
                        //XET QUYEN CHO CHUC NANG INSERT POSITION ROLES
                        GridViewRow row1 = m_grid.FooterRow;
                        if (row1 != null)
                        {
                            LinkButton lbutton3 = (LinkButton)row1.FindControl("cmdInsert");
                            if (lbutton3 != null)
                                lbutton3.Visible = optionStatus;
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else
                {
                    option = 1;                                             //CHUYEN SANG TRANG THAI INSERT
                    PositionId = 0;
                    btnDelete.Enabled = false;
                    btnUpdate.Enabled = false;
                    btnInsert.Text = "Ghi";
                    btnInsert1.Text = "Ghi";
                    btnUpdate.Text = "Sửa";
                    btnUpdate1.Text = "Sửa";
                    stateInsertbtn = false;

                    bindData(-1);
                }
            }
            else                                                            //TRANG THAI INSERT
            {
                if (dtPosRoles == null)
                {
                    dtPosRoles = new DataTable();
                    dtPosRoles.Columns.Add("Id", typeof(int));
                    dtPosRoles.Columns.Add("Prior_Level", typeof(short));
                    dtPosRoles.Columns.Add("WorkName", typeof(string));
                }

                if (dtPosRoles.Rows.Count <= 0)
                    dtPosRoles.Rows.Add(dtPosRoles.NewRow());

                m_grid.EditIndex = index;
                m_grid.DataSource = dtPosRoles;
                m_grid.DataBind();

                if (countPosRoles == 0)
                {
                    m_grid.Rows[0].Visible = false;

                    //VIEW DU LIEU LEN
                    txtPositionName.Text = "";
                    txtSalaryLevel.Text = "";
                    txtDescrible.Text = "";
                    txtITLevel.Text = "";
                    txtMLevel.Text = "";
                    txtPLevel.Text = "";
                    txtSkills.Text = "";
                    txtOther.Text = "";
                }
            }
            setAuthorization();
        }

        protected void m_grid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            EditIndex = e.NewEditIndex;
            bindData(EditIndex);
        }

        protected void m_grid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            bindData(-1);
        }

        protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = e.RowIndex;
                m_grid.EditIndex = id;

                int delId = (int)m_grid.DataKeys[e.RowIndex].Value;
                if (delId != 0)
                {
                    if (option != 1)
                    {
                        cls_Catalog_PositionRoles cPRoles = new cls_Catalog_PositionRoles();
                        cPRoles.Id = delId;

                        int status_Delete = cPRoles.Delete();
                        if (status_Delete == 1)
                        {
                            ltlAnnouncement.Text = "Xóa thành công nhiệm vụ CV!";
                        }
                        if (status_Delete <= 0)
                        {
                            ltlAnnouncement.Text = "Không xóa được nhiệm vụ CV đã chọn.";
                        }
                    }
                    else
                    {
                        dtPosRoles.Rows[id].Delete();
                        dtPosRoles.AcceptChanges();

                        bindData(-1);
                    }
                }
            }
            catch (Exception ex)
            {
                if(option != 1)
                    ltlAnnouncement.Text = "Lỗi trong quá trình xóa nhiệm vụ CV: " + ex.ToString();
            } 
            bindData(-1);
        }

        protected void m_grid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int id = e.RowIndex;
                m_grid.EditIndex = id;
                GridViewRow row = m_grid.Rows[id];

                string wName = ((TextBox)row.FindControl("txtUWorkName")).Text;
                string prior = ((TextBox)row.FindControl("txtUPrior_Level")).Text;

                int updateId = Int32.Parse(m_grid.DataKeys[id].Value.ToString());
                if (updateId > 0)
                {
                    if (option != 1)
                    {
                        cls_Catalog_PositionRoles cPRoles = new cls_Catalog_PositionRoles();

                        cPRoles.Id = updateId;
                        cPRoles.Positionid = PositionId;
                        cPRoles.WorkName = wName;
                        cPRoles.Prior_Level = Convert.ToInt16(prior);
                        cPRoles.CreatedTime = DateTime.Now;

                        int _result = cPRoles.Update();
                        if (_result == 1)
                        {
                            ltlAnnouncement.Text = "Sửa nhiệm vụ CV '" + wName + "' thành công!";
                        }
                        if (_result < 0)
                        {
                            ltlAnnouncement.Text = "Có lỗi trong quá trình sửa dữ liệu!";
                        }
                    }
                    else
                    {
                        dtPosRoles.Rows[id]["WorkName"] = wName;
                        dtPosRoles.Rows[id]["Prior_Level"] = prior;
                    }
                }
            }
            catch (Exception ex)
            {
                if(option != 1)
                    ltlAnnouncement.Text = "Khong cap nhat duoc du lieu. Vui long lien he voi quan tri<br>" + ex.Message;
            }

            bindData(-1);
        }

        protected void m_grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string commandName = e.CommandName;
                GridViewRow row = m_grid.FooterRow;
                if (commandName == "Insert")
                {
                    string wName = ((TextBox)row.FindControl("txtIWorkName")).Text;
                    string prior = ((TextBox)row.FindControl("txtIPrior_Level")).Text;

                    if (option != 1)
                    {
                        cls_Catalog_PositionRoles cPRoles = new cls_Catalog_PositionRoles();
                        cPRoles.Positionid = PositionId;
                        cPRoles.WorkName = wName;
                        cPRoles.Prior_Level = Convert.ToInt16(prior);
                        cPRoles.CreatedTime = DateTime.Now;

                        int _result = cPRoles.Insert();
                        if (_result >= 1)
                        {
                            ltlAnnouncement.Text = "Thêm mới nhiệm vụ CV '" + wName + "' thành công!";
                        }
                        if (_result == -1)
                        {
                            ltlAnnouncement.Text = "Nhiệm vụ CV '" + wName + "' đã tồn tại, bạn nên kiểm tra lại!";
                        }
                        if (_result == 0)
                        {
                            ltlAnnouncement.Text = "Có lỗi trong quá trình thêm mới!";
                        }
                    }
                    else
                    {
                        countPosRoles++;
                        if (countPosRoles == 1)
                            dtPosRoles.Rows[0].Delete();
                        DataRow rowDT = dtPosRoles.NewRow();
                        rowDT["Id"] = countPosRoles;
                        rowDT["WorkName"] = wName;
                        rowDT["Prior_Level"] = prior;

                        dtPosRoles.Rows.Add(rowDT);
                    }
                    bindData(-1);
                }
            }
            catch (Exception ex)
            {
                if(option != 1)
                    ltlAnnouncement.Text = "Khong them moi duoc du lieu. Vui long lien he voi quan tri<br>" + ex.Message;
            }

        }

        private void setAuthorization()
        {
            if (option != 0)                             //TRANG THAI XEM
            {
                txtDescrible.ReadOnly = false;
                txtITLevel.ReadOnly = false;
                txtMLevel.ReadOnly = false;
                txtOther.ReadOnly = false;
                txtPLevel.ReadOnly = false;
                txtPositionName.ReadOnly = false;
                txtSalaryLevel.ReadOnly = false;
                txtSkills.ReadOnly = false;

                dlBelongName.Enabled = true;
                dlEduLevel.Enabled = true;
                dlEduMajor.Enabled = true;
                dlEngLevel.Enabled = true;
            }
            else
            {
                txtDescrible.ReadOnly = true;
                txtITLevel.ReadOnly = true;
                txtMLevel.ReadOnly = true;
                txtOther.ReadOnly = true;
                txtPLevel.ReadOnly = true;
                txtPositionName.ReadOnly = true;
                txtSalaryLevel.ReadOnly = true;
                txtSkills.ReadOnly = true;

                dlBelongName.Enabled = false;
                dlEduLevel.Enabled = false;
                dlEduMajor.Enabled = false;
                dlEngLevel.Enabled = false;
            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            if (stateInsertbtn == true)
            {
                ltlAnnouncement.Text = "";
                option = 1;                                         // TRANG THAI INSERT;
                PositionId = 0;
                countPosRoles = 0;
                dtPosRoles = null;

                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;
                btnInsert.Text = "Ghi";
                btnInsert1.Text = "Ghi";
                btnUpdate.Text = "Sửa";
                btnUpdate1.Text = "Sửa";
                stateInsertbtn = false;
                stateUpdatebtn = true;

                bindData(-1);
            }
            else
            {
                try
                {
                    cls_Catalog_Position cPosition = new cls_Catalog_Position();
                    cPosition.Informatic_Level = txtITLevel.Text;
                    cPosition.Management_Level = txtMLevel.Text;
                    cPosition.Other_Requires = txtOther.Text;
                    cPosition.Politics_Level = txtPLevel.Text;
                    cPosition.PositionName = txtPositionName.Text;
                    cPosition.Salary_Level = (float)Convert.ToDouble(txtSalaryLevel.Text);
                    cPosition.Skills = txtSkills.Text;
                    cPosition.Describle = txtDescrible.Text;
                    cPosition.English_LevelId = Convert.ToInt32(dlEngLevel.SelectedValue);
                    cPosition.Education_LevelId = Convert.ToInt16(dlEduLevel.SelectedValue);
                    cPosition.Education_MajorId = Convert.ToInt16(dlEduMajor.SelectedValue);
                    cPosition.Belong_PositionId = Convert.ToInt32(dlBelongName.SelectedValue);
                    cPosition.CreatedTime = DateTime.Now;

                    int posId = cPosition.Insert();

                    if (posId > 0)
                    {
                        ltlAnnouncement.Text = "Cập nhật thông tin VTCV " + txtPositionName.Text + " thành công.";

                        //THEM THONG TIN VE CAC VAI TRO CUA VTCV
                        if (dtPosRoles.Rows.Count > 0)
                        {
                            int index = 0;
                            cls_Catalog_PositionRoles cPosRoles;
                            while (index < dtPosRoles.Rows.Count)
                            {
                                DataRow row = dtPosRoles.Rows[index];
                                cPosRoles = new cls_Catalog_PositionRoles();
                                cPosRoles.Positionid = posId;
                                cPosRoles.Prior_Level = Convert.ToInt16(row["Prior_Level"]);
                                cPosRoles.WorkName = row["WorkName"].ToString();
                                cPosRoles.CreatedTime = DateTime.Now;

                                int result = cPosRoles.Insert();
                                if (result > 0)
                                {
                                    bindRadList();
                                    ltlAnnouncement.Text += "<br/> Thêm thông tin Vai trò: " + cPosRoles.WorkName + " thành công.";
                                }
                                else
                                    ltlAnnouncement.Text += "<br/>Không thêm được thông tin vai trò: " + cPosRoles.WorkName + ".";

                                index++;
                            }
                        }
                    }
                    else if (posId == 0)
                        ltlAnnouncement.Text = "Không cập nhật được thông tin VTCV " + txtPositionName.Text + ".";
                    else
                        ltlAnnouncement.Text = "Trùng tên VTCV " + txtPositionName.Text + ", không thể cập nhật.";
                }
                catch (Exception ex)
                {
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                stateUpdatebtn = true;

                cls_Catalog_Position cPosition = new cls_Catalog_Position();
                cPosition.Id = PositionId;
                int result = cPosition.Delete();

                bindRadList();
                bindData(-1);

                if (result > 0)
                    ltlAnnouncement.Text = "Xóa thông tin VTCV thành công.";
                else
                    ltlAnnouncement.Text = "Không xóa được thông tin VTCV !!!";
            }
            catch (Exception ex)
            {
                ltlAnnouncement.Text = "Có lỗi khi xóa thông tin VTCV: "+ex.ToString();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (stateUpdatebtn == true)
            {
                ltlAnnouncement.Text = "";
                option = 2;                                         // TRANG THAI UPDATE;
                btnInsert.Text = "Thêm";
                btnInsert1.Text = "Thêm";
                btnUpdate.Text = "Ghi";
                btnUpdate1.Text = "Ghi";
                stateUpdatebtn = false;

                bindData(-1);
            }
            else
            {
                try
                {
                    cls_Catalog_Position cPosition = new cls_Catalog_Position();
                    cPosition.Id = PositionId;
                    cPosition.Informatic_Level = txtITLevel.Text;
                    cPosition.Management_Level = txtMLevel.Text;
                    cPosition.Other_Requires = txtOther.Text;
                    cPosition.Politics_Level = txtPLevel.Text;
                    cPosition.PositionName = txtPositionName.Text;
                    cPosition.Salary_Level = (float)Convert.ToDouble(txtSalaryLevel.Text);
                    cPosition.Skills = txtSkills.Text;
                    cPosition.Describle = txtDescrible.Text;
                    cPosition.English_LevelId = Convert.ToInt32(dlEngLevel.SelectedValue);
                    cPosition.Education_LevelId = Convert.ToInt16(dlEduLevel.SelectedValue);
                    cPosition.Education_MajorId = Convert.ToInt16(dlEduMajor.SelectedValue);
                    cPosition.Belong_PositionId = Convert.ToInt32(dlBelongName.SelectedValue);
                    cPosition.CreatedTime = DateTime.Now;

                    int result = cPosition.Update();

                    if (result > 0)
                        ltlAnnouncement.Text = "Cập nhật thông tin VTCV " + txtPositionName.Text + " thành công.";
                    else if (result == 0)
                        ltlAnnouncement.Text = "Không cập nhật được thông tin VTCV " + txtPositionName.Text + ".";
                    else
                        ltlAnnouncement.Text = "Trùng tên VTCV " + txtPositionName.Text + ", không thể cập nhật.";
                }
                catch (Exception ex)
                {
                    ltlAnnouncement.Text = "Có lỗi trong quá trình cập nhật thông tin VTCV " + txtPositionName.Text + ": " + ex.ToString();
                }
            }
        }
    }
}
