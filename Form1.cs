using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BaiMau_TT5
{
    public partial class Form1 : Form
    {
        private DataTable dataTable; // DataTable lưu trữ dữ liệu
        private DataTable dataTable2;
        private DataTable dataTable3;
        private DataTable dataTable4;
        private int currentPage = 1; // Trang hiện tại
        private int pageSize = 4; // Số dòng trên mỗi trang  
        private int currentPage2 = 1; // Trang hiện tại
        private int pageSize2 = 4; // Số dòng trên mỗi trang
        private int currentPage3 = 1; // Trang hiện tại
        private int pageSize3 = 4; // Số dòng trên mỗi trang
        private int currentPage4 = 1; // Trang hiện tại
        private int pageSize4 = 4; // Số dòng trên mỗi trang

        private Timer timer,timer2; // Timer để tự động chuyển trang
        private int intervalInSeconds = 5; // Khoảng thời gian giữa các trang (5 giây)


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            LoadTime();
            LoadData();
            LoadData2();
            Loaddata3();
            LoadData4();
            Loaddata5();
            Loaddata6();
            Loaddata7();
            LoadData8();
            LoadDataPage(currentPage);
            LoadData9();
            LoadData10();
            LoadData11();
            LoadData12();

            Time();

            LoadTime0();
            LoadDataPage0(currentPage2);
            LoadTime2();
            LoadDataPage2(currentPage3);
            LoadTime3();
            LoadDataPage3(currentPage4);

        }

       

        private void LoadData()
        {
           // string connectionString = "Data Source=192.168.10.185;Initial Catalog=DataQMS;User Id=sa;Password=abc321;";
             string connectionString = $"DSN=SQLQMS;Uid=sa;Pwd=abc321;";
          

            try
            {
                using (OdbcConnection connection = new OdbcConnection(connectionString))
                {
                    connection.Open();

                    // string query = "SELECT * FROM DICHVU";

                    string query = @"
                        SELECT SQMS,QUAY
                        FROM DICHVU
                        WHERE dichvu = 'dv1' AND PHUCVU = 'true'
                    ";

                    //string query = @"
                    //  SELECT TOP 4 SQMS,QUAY
                    //  FROM DICHVU
                    //  WHERE dichvu = 'dv1' AND PHUCVU = 'true'
                    //  ORDER BY SQMS DESC ";


                    using (OdbcCommand command = new OdbcCommand(query, connection))
                    {

                        dataTable = new DataTable();
                        // Sử dụng OdbcDataAdapter để lấy dữ liệu từ Command và đổ vào DataTable
                        using (OdbcDataAdapter adapter = new OdbcDataAdapter(command))
                        {


                            adapter.Fill(dataTable);


                        }
                        // Gán DataTable vào DataGridView
                        dataGridView1.DataSource = dataTable;
                        dataGridView1.Columns["SQMS"].HeaderText = "STT";
                        dataGridView1.Columns["QUAY"].HeaderText = "QUẦY";

                        // Hiển thị dữ liệu trang đầu tiên
                        //UpdateDataGridView();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu từ SQL Server thông qua ODBC: " + ex.Message);
            }



        }

        private void LoadData2()
        {
           

            string connectionString = $"DSN=SQLQMS;Uid=sa;Pwd=abc321;";
            string query = "SELECT TenDV,TenDV FROM Descriptionservices where id=62 "; // Thay "Description services" bằng tên bảng trong cơ sở dữ liệu của bạn


            try
            {
                using (OdbcConnection connection = new OdbcConnection(connectionString))
                {
                    connection.Open();


                    using (OdbcCommand command = new OdbcCommand(query, connection))
                    {

                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            string tenDichVu = result.ToString();
                            lblTenDichVu.Text = tenDichVu;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu từ SQL Server thông qua ODBC: " + ex.Message);
            }

        }

        private void Loaddata3() 
        {
           
            string connectionString = $"DSN=SQLQMS;Uid=sa;Pwd=abc321;";
            string query = @"
                SELECT SUM(CASE WHEN dichvu = 'dv1' AND PHUCVU = 'false' THEN 1 ELSE 0 END) AS TongSoLuotCho
                FROM DICHVU
            ";

            try
            {
                using (OdbcConnection connection = new OdbcConnection(connectionString))
                {
                    connection.Open();


                    using (OdbcCommand command = new OdbcCommand(query, connection))
                    {

                        //Sử dụng ExecuteScalar để lấy giá trị tổng từ câu lệnh truy vấn
                          object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            // Hiển thị kết quả trên TextBox
                            //textBox1.Text = result.ToString();

                            label6.Text = result.ToString();

                            label1.Text = "Lượt chờ :";
                        }
                        else
                        {
                            //textBox1.Text = "Không có dữ liệu thỏa mãn yêu cầu.";
                            label6.Text = "Không có dữ liệu thỏa mãn yêu cầu.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu từ SQL Server thông qua ODBC: " + ex.Message);
            }


        }

        private void LoadData4() 
        {
            


            string connectionString = $"DSN=SQLQMS;Uid=sa;Pwd=abc321;";
            string query = "SELECT TenDV,TenDV FROM Descriptionservices where id=63 "; // Thay "Description services" bằng tên bảng trong cơ sở dữ liệu của bạn


            try
            {
                using (OdbcConnection connection = new OdbcConnection(connectionString))
                {
                    connection.Open();


                    using (OdbcCommand command = new OdbcCommand(query, connection))
                    {

                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            string tenDichVu = result.ToString();
                            lblTenDichVu2.Text = tenDichVu;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu từ SQL Server thông qua ODBC: " + ex.Message);
            }

        }

        private void Loaddata5() 
        {
            

            string connectionString = $"DSN=SQLQMS;Uid=sa;Pwd=abc321;";
            string query = @"
                SELECT SUM(CASE WHEN dichvu = 'dv2' AND PHUCVU = 'false' THEN 1 ELSE 0 END) AS TongSoLuotCho
                FROM DICHVU
            ";

            try
            {
                using (OdbcConnection connection = new OdbcConnection(connectionString))
                {
                    connection.Open();


                    using (OdbcCommand command = new OdbcCommand(query, connection))
                    {

                        //Sử dụng ExecuteScalar để lấy giá trị tổng từ câu lệnh truy vấn
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            // Hiển thị kết quả trên TextBox
                            //textBox2.Text = result.ToString();
                            label7.Text = result.ToString();

                            label3.Text = "Lượt chờ :";
                        }
                        else
                        {
                            // textBox2.Text = "Không có dữ liệu thỏa mãn yêu cầu.";

                            label7.Text = "Không có dữ liệu thỏa mãn yêu cầu.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu từ SQL Server thông qua ODBC: " + ex.Message);
            }

        }

        private void Loaddata6() 
        {
            

            string connectionString = $"DSN=SQLQMS;Uid=sa;Pwd=abc321;";
            string query = "SELECT TenDV,TenDV FROM Descriptionservices where id=64 "; // Thay "Description services" bằng tên bảng trong cơ sở dữ liệu của bạn


            try
            {
                using (OdbcConnection connection = new OdbcConnection(connectionString))
                {
                    connection.Open();


                    using (OdbcCommand command = new OdbcCommand(query, connection))
                    {

                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            string tenDichVu = result.ToString();
                            lblTenDichVu3.Text = tenDichVu;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu từ SQL Server thông qua ODBC: " + ex.Message);
            }


        }
        private void Loaddata7() 
        {
            

            string connectionString = $"DSN=SQLQMS;Uid=sa;Pwd=abc321;";
            string query = @"
                SELECT SUM(CASE WHEN dichvu = 'dv3' AND PHUCVU = 'false' THEN 1 ELSE 0 END) AS TongSoLuotCho
                FROM DICHVU
            ";

            try
            {
                using (OdbcConnection connection = new OdbcConnection(connectionString))
                {
                    connection.Open();


                    using (OdbcCommand command = new OdbcCommand(query, connection))
                    {

                        //Sử dụng ExecuteScalar để lấy giá trị tổng từ câu lệnh truy vấn
                          object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            // Hiển thị kết quả trên TextBox
                            // textBox3.Text = result.ToString();

                            label8.Text = result.ToString();

                            label4.Text = "Lượt chờ :";
                        }
                        else
                        {
                            // textBox3.Text = "Không có dữ liệu thỏa mãn yêu cầu.";
                            label8.Text = "Không có dữ liệu thỏa mãn yêu cầu.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu từ SQL Server thông qua ODBC: " + ex.Message);
            }

        }

        private void LoadData8() 
        {
           

            string connectionString = $"DSN=SQLQMS;Uid=sa;Pwd=abc321;";
            string query = @"
                SELECT SQMS,QUAY
                FROM DICHVU 
                WHERE dichvu = 'dv2' AND PHUCVU = 'true'
                
            "
            ;


            //string query = @"
            //  SELECT TOP 4 SQMS,QUAY
            //  FROM DICHVU
            //  WHERE dichvu = 'dv2' AND PHUCVU = 'true'
            //  ORDER BY SQMS DESC ";


            try
            {
                using (OdbcConnection connection = new OdbcConnection(connectionString))
                {
                    connection.Open();


                    using (OdbcCommand command = new OdbcCommand(query, connection))
                    {
                        using (OdbcDataAdapter adapter = new OdbcDataAdapter(command))
                        {
                            dataTable2 = new DataTable();
                            adapter.Fill(dataTable2);
                        }
                        // Gán DataTable vào DataGridView
                        dataGridView2.DataSource = dataTable2;
                        dataGridView2.Columns["SQMS"].HeaderText = "STT";
                        dataGridView2.Columns["QUAY"].HeaderText = "QUẦY";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu từ SQL Server thông qua ODBC: " + ex.Message);
            }


        }

        private void LoadData9() 
        {
          

            string connectionString = $"DSN=SQLQMS;Uid=sa;Pwd=abc321;";
            string query = @"
                SELECT SQMS,QUAY
                FROM DICHVU 
                WHERE dichvu = 'dv3' AND PHUCVU = 'true'
            "
            ;


            //string query = @"
            //  SELECT TOP 4 SQMS,QUAY
            //  FROM DICHVU
            //  WHERE dichvu = 'dv3' AND PHUCVU = 'true'
            //  ORDER BY SQMS DESC ";


            try
            {
                using (OdbcConnection connection = new OdbcConnection(connectionString))
                {
                    connection.Open();


                    using (OdbcCommand command = new OdbcCommand(query, connection))
                    {
                        using (OdbcDataAdapter adapter = new OdbcDataAdapter(command))
                        {
                            dataTable3 = new DataTable();
                            adapter.Fill(dataTable3);
                        }
                        // Gán DataTable vào DataGridView
                        dataGridView3.DataSource = dataTable3;
                        dataGridView3.Columns["SQMS"].HeaderText = "STT";
                        dataGridView3.Columns["QUAY"].HeaderText = "QUẦY";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu từ SQL Server thông qua ODBC: " + ex.Message);
            }

        }

        private void LoadData10() 
        {
            string connectionString = $"DSN=SQLQMS;Uid=sa;Pwd=abc321;";
            string query = "SELECT TenDV,TenDV FROM Descriptionservices where id=65 "; // Thay "Description services" bằng tên bảng trong cơ sở dữ liệu của bạn


            try
            {
                using (OdbcConnection connection = new OdbcConnection(connectionString))
                {
                    connection.Open();


                    using (OdbcCommand command = new OdbcCommand(query, connection))
                    {

                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            string tenDichVu = result.ToString();
                            lblTenDichVu4.Text = tenDichVu;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu từ SQL Server thông qua ODBC: " + ex.Message);
            }
        }

        private void LoadData11() 
        {
            string connectionString = $"DSN=SQLQMS;Uid=sa;Pwd=abc321;";
            string query = @"
                SELECT SUM(CASE WHEN dichvu = 'dv4' AND PHUCVU = 'false' THEN 1 ELSE 0 END) AS TongSoLuotCho
                FROM DICHVU
            ";

            try
            {
                using (OdbcConnection connection = new OdbcConnection(connectionString))
                {
                    connection.Open();


                    using (OdbcCommand command = new OdbcCommand(query, connection))
                    {

                        //Sử dụng ExecuteScalar để lấy giá trị tổng từ câu lệnh truy vấn
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            // Hiển thị kết quả trên TextBox
                            //textBox4.Text = result.ToString();

                            label9.Text = result.ToString();

                            label5.Text = "Lượt chờ :";
                        }
                        else
                        {
                            // textBox4.Text = "Không có dữ liệu thỏa mãn yêu cầu.";
                            label9.Text = "Không có dữ liệu thỏa mãn yêu cầu.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu từ SQL Server thông qua ODBC: " + ex.Message);
            }

        }

        private void LoadData12() 
        {
            string connectionString = $"DSN=SQLQMS;Uid=sa;Pwd=abc321;";
            string query = @"
                SELECT SQMS,QUAY
                FROM DICHVU 
                WHERE dichvu = 'dv4' AND PHUCVU = 'true'
            "
            ;



            try
            {
                using (OdbcConnection connection = new OdbcConnection(connectionString))
                {
                    connection.Open();


                    using (OdbcCommand command = new OdbcCommand(query, connection))
                    {
                        using (OdbcDataAdapter adapter = new OdbcDataAdapter(command))
                        {
                            dataTable4 = new DataTable();
                            adapter.Fill(dataTable4);
                        }
                        // Gán DataTable vào DataGridView
                        dataGridView4.DataSource = dataTable4;
                        dataGridView4.Columns["SQMS"].HeaderText = "STT";
                        dataGridView4.Columns["QUAY"].HeaderText = "QUẦY";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu từ SQL Server thông qua ODBC: " + ex.Message);
            }


        }

        private void Time() 
        {
            timer2 = new Timer();
            timer2.Interval = 1000; // 1000 ms = 1 second
            timer2.Tick += Timer_Tick4;
            timer2.Start();
        }

        private void Timer_Tick4(object sender, EventArgs e) 
        {
            
            label10.Text = DateTime.Now.ToString("dddd, dd/MM/yyyy", new System.Globalization.CultureInfo("vi-VN"));
            label11.Text ="Giờ :"+ DateTime.Now.ToString("HH:mm:ss");
        }


        private void LoadTime()
        {
            timer = new Timer();
            timer.Interval = intervalInSeconds * 1000; // Chuyển đổi sang mili giây
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //Tự động chuyển sang trang tiếp theo khi Timer kích hoạt
            //currentPage++;
            //UpdateDataGridView();
            LoadDataPage(currentPage);
            currentPage++;

            //Nếu đã đạt đến trang cuối, quay lại trang đầu
           
            int totalPage = (int)Math.Ceiling((double)dataTable2.Rows.Count / pageSize);
            if (currentPage > totalPage)
            {
                //currentPage = 1;
                currentPage = totalPage;
            }

            //currentPage = totalPage;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LoadDataPage(int page)
        {
            if (dataTable2 != null)
            {
                DataTable pageTable = dataTable2.Clone(); // Tạo DataTable với cùng cấu trúc nhưng không có dữ liệu
                int startRow = (page - 1) * pageSize;

                // Thêm dòng vào pageTable từ dataTable
                for (int i = startRow; i < startRow + pageSize && i < dataTable2.Rows.Count; i++)
                {
                    pageTable.Rows.Add(dataTable2.Rows[i].ItemArray);
                }

                // Gán DataTable vào DataGridView
                dataGridView2.DataSource = pageTable;
            }
            else
            {
                MessageBox.Show("Không có dữ liệu");
            }
        }

        private void LoadTime0()
        {
            timer = new Timer();
            timer.Interval = intervalInSeconds * 1000; // Chuyển đổi sang mili giây
            timer.Tick += Timer_Tick0;
            timer.Start();
        }

        private void Timer_Tick0(object sender, EventArgs e)
        {
            //Tự động chuyển sang trang tiếp theo khi Timer kích hoạt
            //currentPage++;
            //UpdateDataGridView();
            LoadDataPage0(currentPage2);
            currentPage2++;

            //Nếu đã đạt đến trang cuối, quay lại trang đầu

            int totalPage2 = (int)Math.Ceiling((double)dataTable.Rows.Count / pageSize2);
            if (currentPage2 > totalPage2)
            {
                //currentPage = 1;
                currentPage2 = totalPage2;
            }

            //currentPage = totalPage;

        }

        private void LoadDataPage0(int page)
        {
            if (dataTable != null)
            {
                DataTable pageTable = dataTable.Clone(); // Tạo DataTable với cùng cấu trúc nhưng không có dữ liệu
                int startRow = (page - 1) * pageSize2;

                // Thêm dòng vào pageTable từ dataTable
                for (int i = startRow; i < startRow + pageSize2 && i < dataTable.Rows.Count; i++)
                {
                    pageTable.Rows.Add(dataTable.Rows[i].ItemArray);
                }

                // Gán DataTable vào DataGridView
                dataGridView1.DataSource = pageTable;
            }
            else
            {
                MessageBox.Show("Không có dữ liệu");
            }
        }

        private void LoadTime2()
        {
            timer = new Timer();
            timer.Interval = intervalInSeconds * 1000; // Chuyển đổi sang mili giây
            timer.Tick += Timer_Tick2;
            timer.Start();
        }

        private void Timer_Tick2(object sender, EventArgs e)
        {
            //Tự động chuyển sang trang tiếp theo khi Timer kích hoạt
            //currentPage++;
            //UpdateDataGridView();
            LoadDataPage2(currentPage3);
            currentPage3++;

            //Nếu đã đạt đến trang cuối, quay lại trang đầu

            int totalPage3 = (int)Math.Ceiling((double)dataTable3.Rows.Count / pageSize2);
            if (currentPage3 > totalPage3)
            {
                //currentPage = 1;
                currentPage3 = totalPage3;
            }

            //currentPage = totalPage;

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoadDataPage2(int page)
        {
            if (dataTable3 != null)
            {
                DataTable pageTable = dataTable3.Clone(); // Tạo DataTable với cùng cấu trúc nhưng không có dữ liệu
                int startRow = (page - 1) * pageSize3;

                // Thêm dòng vào pageTable từ dataTable
                for (int i = startRow; i < startRow + pageSize3 && i < dataTable3.Rows.Count; i++)
                {
                    pageTable.Rows.Add(dataTable3.Rows[i].ItemArray);
                }

                // Gán DataTable vào DataGridView
                dataGridView3.DataSource = pageTable;
            }
            else
            {
                MessageBox.Show("Không có dữ liệu");
            }
        }


        private void LoadTime3()
        {
            timer = new Timer();
            timer.Interval = intervalInSeconds * 1000; // Chuyển đổi sang mili giây
            timer.Tick += Timer_Tick3;
            timer.Start();
        }

        private void Timer_Tick3(object sender, EventArgs e)
        {
            //Tự động chuyển sang trang tiếp theo khi Timer kích hoạt
            //currentPage++;
            //UpdateDataGridView();
            LoadDataPage3(currentPage4);
            currentPage4++;

            //Nếu đã đạt đến trang cuối, quay lại trang đầu

            int totalPage4 = (int)Math.Ceiling((double)dataTable4.Rows.Count / pageSize4);
            if (currentPage4 > totalPage4)
            {
                //currentPage = 1;
                currentPage4 = totalPage4;
            }

            //currentPage = totalPage;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void LoadDataPage3(int page)
        {
            if (dataTable4 != null)
            {
                DataTable pageTable = dataTable4.Clone(); // Tạo DataTable với cùng cấu trúc nhưng không có dữ liệu
                int startRow = (page - 1) * pageSize4;

                // Thêm dòng vào pageTable từ dataTable
                for (int i = startRow; i < startRow + pageSize4 && i < dataTable4.Rows.Count; i++)
                {
                    pageTable.Rows.Add(dataTable4.Rows[i].ItemArray);
                }

                // Gán DataTable vào DataGridView
                dataGridView4.DataSource = pageTable;
            }
            else
            {
                MessageBox.Show("Không có dữ liệu");
            }
        }


        //private void UpdateDataGridView()
        //{
        //    // Tính toán số trang dựa trên tổng số dòng và số dòng trên mỗi trang
        //    int totalRows = dataTable2.Rows.Count;
        //    int totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

        //    // Kiểm tra và điều chỉnh trang hiện tại
        //    if (currentPage < 1)
        //        currentPage = 1;
        //    else if (currentPage > totalPages)
        //        currentPage = totalPages;

        //    // Tính toán chỉ số bắt đầu và chỉ số kết thúc của dữ liệu trên trang hiện tại
        //    int startIndex = (currentPage - 1) * pageSize;
        //    int endIndex = startIndex + pageSize - 1;

        //    // Kiểm tra và điều chỉnh chỉ số kết thúc
        //    if (endIndex >= totalRows)
        //        endIndex = totalRows - 1;

        //    // Tạo một DataTable mới để lưu trữ dữ liệu trên trang hiện tại
        //    DataTable currentPageData = dataTable2.Clone();

        //    // Lặp qua dữ liệu trang hiện tại và thêm vào DataTable mới
        //    for (int i = startIndex; i <= endIndex; i++)
        //    {
        //        currentPageData.ImportRow(dataTable2.Rows[i]);
        //    }

        //    // Gán DataTable mới vào DataGridView
        //    dataGridView2.DataSource = currentPageData;

        //    // Hiển thị thông tin về trang hiện tại và tổng số trang
        //    lblPageInfo.Text = "Trang " + currentPage + "/" + totalPages;
        //}




    }

}
