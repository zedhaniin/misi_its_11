using Microsoft.AspNetCore.Mvc;
using TokoApi.Helpers; 
using Microsoft.Data.SqlClient;
using System; 

namespace TokoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KategoriController : ControllerBase
    {
        private readonly koneksi _koneksi;

        public KategoriController(koneksi koneksi)
        {
            _koneksi = koneksi;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var list = new List<object>();
            try
            {
                using (SqlConnection conn = _koneksi.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Kategori", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new
                        {
                            Id = reader["Id"],
                            NamaKategori = reader["NamaKategori"]
                        });
                    }
                    reader.Close();
                }
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { pesan = "Error cuy!", detail = ex.Message });
            }
        }

        [HttpGet("test-connection")]
        public IActionResult TestConnection()
        {
            try
            {
                using var conn = _koneksi.GetConnection();
                conn.Open();
                return Ok(new { connected = true, message = "Koneksi Aman Jaya!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { connected = false, error = ex.Message });
            }
        }

        [HttpGet("produk-satu")]
        public IActionResult GetOneProduct()
        {
            try
            {
                object result = null;
                using (var conn = _koneksi.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT TOP 1 Id, Nama, Harga FROM Produk";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        result = new
                        {
                            id = reader["Id"],
                            nama = reader["Nama"],
                            harga = reader["Harga"]
                        };
                    }
                }
                return result != null ? Ok(result) : NotFound("Gak ada data produk");
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("produk-semua")]
        public IActionResult GetAllProducts()
        {
            var list = new List<object>();
            try
            {
                using (var conn = _koneksi.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT Id, Nama, Harga FROM Produk";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new
                        {
                            id = reader["Id"],
                            nama = reader["Nama"],
                            harga = reader["Harga"]
                        });
                    }
                }
                return Ok(list);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}