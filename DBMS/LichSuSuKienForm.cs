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

namespace DBMS
{
    public partial class LichSuSuKienForm : Form
    {
        private string connectionString = "Server=.;Database=vc;Integrated Security=true;";

        public LichSuSuKienForm()
        {
            InitializeComponent();
        }

        private void LichSuSuKienForm_Load(object sender, EventArgs e)
        {
            LoadLichSuSuKien();
        }

        private void LoadLichSuSuKien()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM v_LichSuSuKien";
                    
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    
                    dataGridView1.DataSource = dataTable;
                    
                    // Thiết lập header cho các cột
                    if (dataGridView1.Columns["SuKienID"] != null)
                        dataGridView1.Columns["SuKienID"].HeaderText = "ID";
                    if (dataGridView1.Columns["ThoiGian"] != null)
                        dataGridView1.Columns["ThoiGian"].HeaderText = "Thời gian";
                    if (dataGridView1.Columns["NoiDung"] != null)
                        dataGridView1.Columns["NoiDung"].HeaderText = "Nội dung";
                    
                    // Thiết lập độ rộng cột
                    if (dataGridView1.Columns["SuKienID"] != null)
                        dataGridView1.Columns["SuKienID"].Width = 80;
                    if (dataGridView1.Columns["ThoiGian"] != null)
                        dataGridView1.Columns["ThoiGian"].Width = 150;
                    if (dataGridView1.Columns["NoiDung"] != null)
                        dataGridView1.Columns["NoiDung"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải lịch sử sự kiện: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadLichSuSuKien();
        }
    }
}