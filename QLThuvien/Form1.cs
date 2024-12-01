using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLThuvien
{
    public partial class Form1 : Form
    {
        Nhanvien nv = new Nhanvien();
        BangCap bc = new BangCap();
        bool themmoi = false;

        public Form1()
        {
            InitializeComponent();
        }

        void HienThiDSNhanVien()
        {
            dataGridView1.DataSource = nv.LayDSNhanvien();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            setNull();
            setButton(true);
            HienThiDSNhanVien();
            HienthiBangcap();
        }

        void SetEnabled(bool val)
        {
            textBox1.Enabled = val;
            dateTimePicker1.Enabled = val;
            textBox2.Enabled = val;
            textBox3.Enabled = val;
            comboBox1.Enabled = val;

        }
        void setNull()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }
        void setButton(bool val)
        {
            btnThem.Enabled = val;
            btnXoa.Enabled = val;
            btnSua.Enabled = val;
            btnThoat.Enabled = val;
            btnLuu.Enabled = !val;
            btnHuy.Enabled = !val;
        }
        void HienthiBangcap()
        {
            DataTable dt = bc.LayBangcap();
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "TenBangcap";
            comboBox1.ValueMember = "MaBangcap";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            themmoi = true;
            setButton(false);
            SetEnabled(true);
            textBox1.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Chắc chắn xóa không", "Xác nhận xóa", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    nv.XoaNhanVien(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                    HienThiDSNhanVien();
                    setNull();
                    MessageBox.Show("Xóa thành công!!!!!");
                }

            }
            else
            {
                MessageBox.Show("Hãy chọn nhân viên cần xóa!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                themmoi = false;
                setButton(false);
                SetEnabled(true);
            }
            else
            {
                MessageBox.Show("Hãy chọn nhân viên cần sửa!");
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) || comboBox1.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin trước khi lưu.");
                return;
            }

            string ngay = dateTimePicker1.Value.ToString("yyyy-MM-dd");

            try
            {
                if (themmoi)
                {
                    nv.ThemNhanVien(textBox1.Text, ngay, textBox2.Text, textBox3.Text, comboBox1.SelectedValue.ToString());
                    MessageBox.Show("Thêm mới thành công");
                    HienThiDSNhanVien();
                    setNull();
                }
                else
                {
                    int i = dataGridView1.CurrentRow.Index;
                    nv.CapNhatNhanVien(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()), textBox1.Text, ngay, textBox2.Text, textBox3.Text, comboBox1.SelectedValue.ToString());
                    HienThiDSNhanVien();
                    dataGridView1.Rows[i].Selected = true;
                    MessageBox.Show("Cập nhật thành công");
                }

                SetEnabled(false);
                setButton(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            setButton(true);
            SetEnabled(false);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
            {
                DataGridViewRow r = dataGridView1.CurrentRow;
                textBox1.Text = r.Cells[1].Value.ToString();
                dateTimePicker1.Text = r.Cells[2].Value.ToString();
                textBox2.Text = r.Cells[3].Value.ToString();
                textBox3.Text = r.Cells[4].Value.ToString();
                comboBox1.Text = r.Cells[5].Value.ToString();
            }
        }
    }
}
