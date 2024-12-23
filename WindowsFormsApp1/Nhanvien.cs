using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace WindowsFormsApp1
{
    public partial class Nhanvien : Form
    {
        string sCon = "Data Source=LAPTOP-C3P8SLFA;Initial Catalog=QLBH;Integrated Security=True";
        public Nhanvien()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void Nhanvien_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("Hẹn gặp lại", "Thông báo");
        }

        private void BtThemmoi_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(sCon);
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối DB");
                return;
            }

            // Lấy tên khách hàng và số điện thoại từ giao diện
            string sTenNV = txtTen.Text;
            string sSoDienThoai = txtSDT.Text;

            // Kiểm tra nếu tên hoặc số điện thoại bị bỏ trống
            if (string.IsNullOrEmpty(sTenNV) || string.IsNullOrEmpty(sSoDienThoai))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gọi hàm tự động để lấy mã đặt bàn
            string sQuery = "SELECT DBO.NVID()";  // Giả sử bạn đã có hàm tự động tạo mã
            SqlCommand cmd = new SqlCommand(sQuery, con);

            // Lấy mã tự động
            string sMaNV = cmd.ExecuteScalar().ToString();


            // Thêm dữ liệu vào bảng ThongTinDatBan
            string sInsertQuery = "INSERT INTO Nhanvien (NV_ID, NV_TEN, NV_SDT) " +
                                    "VALUES (@id, @tennv, @sodienthoai)";

            SqlCommand insertCmd = new SqlCommand(sInsertQuery, con);
            insertCmd.Parameters.AddWithValue("@id", sMaNV);
            insertCmd.Parameters.AddWithValue("@tennv", sTenNV);
            insertCmd.Parameters.AddWithValue("@sodienthoai", sSoDienThoai);

            try
            {
                insertCmd.ExecuteNonQuery();
                MessageBox.Show("Thêm mới thành công!", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm mới khách hàng: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void Btxem_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sCon);
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối DB");
            }
            string sQuery = "select * from Nhanvien";
            SqlDataAdapter adapter = new SqlDataAdapter(sQuery, con);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Nhanvien");
            if (ds.Tables["Nhanvien"].Rows.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables["Nhanvien"];
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để hiển thị.");
            }
            dataGridView1.DataSource = ds.Tables["Nhanvien"];
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
