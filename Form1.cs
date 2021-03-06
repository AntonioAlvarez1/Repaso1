using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace Repaso1
{
    public partial class Form1 : Form
    {
        List<Informacion> empleados = new List<Informacion>();
        List<Trabajo> asistencias = new List<Trabajo>();
        List<Sueldo> sueldos = new List<Sueldo>();

        public Form1()
        {
            InitializeComponent();
        }
        private void CargaInformacion(string filename)
        {
            FileStream stream = new FileStream(filename, FileMode.OpenOrCreate , FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            while(reader.Peek()>-1)
            {
               Informacion empleado= new Informacion();
                empleado.Numero_empleado =Convert.ToInt32(reader.ReadLine());
                empleado.Nombre=reader.ReadLine();
                empleado.Sueldo = Convert.ToInt32(reader.ReadLine());
                empleados.Add(empleado);
            }
            reader.Close();
        }
        private void CargaAsistencia(string filename)
        {
            FileStream stream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);            

            while(reader.Peek()>-1)
            {
                Trabajo trabajo = new Trabajo();
                trabajo.No_empleado= Convert.ToInt32(reader.ReadLine());
                trabajo.Horas = Convert.ToInt32(reader.ReadLine());
                trabajo.Mes= reader.ReadLine();
                asistencias.Add(trabajo);
            }
            reader.Close();
        }
        private void buttoncarga_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < empleados.Count; i++)
            {
                for (int j=0; j<asistencias.Count; j++)
                {
                    if(empleados[i].Numero_empleado==asistencias[j].No_empleado )
                    {
                        Sueldo sueldo = new Sueldo();
                        sueldo.NoEmpleado = empleados[i].Numero_empleado;
                        sueldo.Nombre = empleados[i].Nombre;
                        sueldo.SueldoM = empleados[i].Sueldo * asistencias[j].Horas;
                        sueldo.Mes = asistencias[j].Mes;

                        sueldos.Add(sueldo);
                    }

                }
            }
            dataGridView3.DataSource = sueldos;
            dataGridView3.Refresh();
        }
        private void cargarGrid()
        {
            
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            dataGridView1.DataSource = empleados;
            dataGridView1.Refresh();
            dataGridHorastra.DataSource = null;
            dataGridHorastra.Refresh();
            dataGridHorastra.DataSource = asistencias;
            dataGridHorastra.Refresh();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
            CargaInformacion("Personal.txt");
            CargaAsistencia("Asistencia.txt");
            cargarGrid();
        }
    }
}
