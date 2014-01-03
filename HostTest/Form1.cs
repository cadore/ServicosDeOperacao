using ServicoDeOperacaoClienteUsuarios;
using System;
using System.ServiceModel;
using System.Windows.Forms;

namespace HostTest
{
    public partial class Form1 : Form
    {
        private string status = "stopped";
        private GerenteDeOperacoes gdo = new GerenteDeOperacoes();
  
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if("stopped".Equals(status)){
                try
                {
                    gdo.startService();
                }catch(Exception ex){
                    lbStatus.Text = ex.Message;
                }
                status = "started";
                btnStartStop.Text = "Stop service";
                lbStatus.Text = "Service started successfully.";                
            }
            else
            {
                try
                {
                    gdo.stopService();
                }catch(Exception ex){
                    lbStatus.Text = ex.Message;
                }
                status = "stopped";
                btnStartStop.Text = "Start service";
                lbStatus.Text = "Service stopped successfully.";                
            }
        }        
    }
}
